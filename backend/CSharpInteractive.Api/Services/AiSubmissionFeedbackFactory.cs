using CSharpInteractive.Api.Contracts;

namespace CSharpInteractive.Api.Services;

public static class AiSubmissionFeedbackFactory
{
    public static SubmissionFeedbackDto Create(AiValidationResult result) =>
        new(
            result.Passed,
            result.Passed ? "La validation IA accepte la reponse." : result.Feedback,
            result.Passed ? ["Solution equivalente acceptee par l'IA."] : [],
            result.Passed ? [] : [result.Feedback],
            result.Passed ? SubmissionErrorCategory.Unknown : SubmissionErrorCategory.PartialSolution,
            result.Passed ? [] : result.Hints,
            [],
            []);
}
