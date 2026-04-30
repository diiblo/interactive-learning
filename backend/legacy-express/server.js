const express = require('express');
const cors = require('cors');
const { exec } = require('child_process');
const fs = require('fs/promises');
const path = require('path');
const { v4: uuidv4 } = require('uuid');

const app = express();
app.use(cors());
app.use(express.json());

const PORT = 3001;

app.post('/execute', async (req, res) => {
    const { code } = req.body;
    if (!code) {
        return res.status(400).json({ error: "Code is required" });
    }

    const sessionId = uuidv4();
    const tempDir = path.join(__dirname, 'temp_exec', sessionId);

    try {
        await fs.mkdir(tempDir, { recursive: true });

        // Wrap the code if it doesn't contain 'class Program' or top-level statements
        // Actually, with net8.0 and top-level statements, raw code works perfectly!
        const programPath = path.join(tempDir, 'Program.cs');
        await fs.writeFile(programPath, code);

        // Write the csproj file
        const csprojPath = path.join(tempDir, 'Temp.csproj');
        const csprojContent = `<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>
</Project>`;
        await fs.writeFile(csprojPath, csprojContent);

        // Run Docker container
        // We copy /src to /app inside the container so that bin/ and obj/ are not written to the host filesystem
        const dockerCmd = `docker run --rm --memory=256m --cpus=0.5 --net=none -v ${tempDir}:/src:ro mcr.microsoft.com/dotnet/sdk:8.0 bash -c "mkdir -p /app && cp -a /src/. /app/ && cd /app && dotnet run"`;

        exec(dockerCmd, { timeout: 15000 }, async (error, stdout, stderr) => {
            // Clean up host directory
            await fs.rm(tempDir, { recursive: true, force: true });

            if (error) {
                if (error.killed) {
                    return res.json({ success: false, output: "Exécution interrompue (dépassement du temps limite de 15s ou boucle infinie).", error: true });
                }
                
                // Extract useful error message from stdout/stderr
                let errorOutput = stdout || stderr;
                return res.json({ success: false, output: errorOutput, error: true });
            }

            res.json({ success: true, output: stdout, error: false });
        });

    } catch (err) {
        console.error(err);
        try { await fs.rm(tempDir, { recursive: true, force: true }); } catch(e){}
        res.status(500).json({ error: "Server error during execution" });
    }
});

app.listen(PORT, () => {
    console.log(`Backend server running on http://localhost:${PORT}`);
});
