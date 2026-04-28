namespace Starbucks.Application.Execptions;

public sealed record ValidationError(
    string PropertyName,
    string ErrorMessage
);