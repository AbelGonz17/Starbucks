namespace Starbucks.Application.Abstractions;

public class Result<T> : ResultGlobal
{
    public T? Value { get; init; }
    public static Result<T> Success(T value) => new Result<T>
    {
        IsSuccess = true,
        Value = value
    };

    public static Result<T> Failure(params Error[] errors) => new Result<T>
    {
        IsSuccess = false,
        Errors = errors.ToList()
    };

}
public class Result : ResultGlobal
{
    public static Result Success() => new Result
    {
        IsSuccess = true,
    };

    public static Result Failure(params Error[] errors) => new Result
    {
        IsSuccess = false,
        Errors = errors.ToList()
    };
}

public abstract class ResultGlobal
{
     public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public List<Error> Errors { get; init; } = new();

    protected ResultGlobal() { }
}