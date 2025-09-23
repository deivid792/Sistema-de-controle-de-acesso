using System;

namespace VisitorService.Domain.Shared.results
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
        }

        private Result(string error)
        {
            IsSuccess = false;
            Error = error;
        }
        private Result(bool success, string? error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(value);
        public static Result<T> Fail(string error) => new Result<T>(error);
    }

    // Classe para retorno VOID
    public class Result
    {
        public bool IsSuccess { get; }
        public string? Error { get; }

        private Result(bool success, string? error)
        {
            IsSuccess = success;
            Error = error;
        }

        public static Result Success() => new Result(true, null);
        public static Result Fail(string error) => new Result(false, error);

    }

}
