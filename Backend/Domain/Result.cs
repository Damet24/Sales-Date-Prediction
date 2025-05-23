namespace Domain;

public class Result<T>
{
    public T? Value { get; }
    public string? Error { get; }
    public bool IsSuccess { get; }

    protected Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    protected Result(string error)
    {
        Error = error;
        IsSuccess = false;
    }

    public static Result<T> Success(T value) => new (value);
    public static Result<T> Failure(string error) => new (error);

    public void Deconstruct(out object customername, out object orders)
    {
        throw new NotImplementedException();
    }
}