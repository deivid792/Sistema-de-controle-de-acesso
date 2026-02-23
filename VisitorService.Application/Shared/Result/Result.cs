using VisitorService.Domain.Shared;

namespace VisitorService.Application.Shared.results
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public IEnumerable<Error> Errors { get; }

        private Result(T value)
        {
            IsSuccess = true;
            Value = value;
            Errors = Enumerable.Empty<Error>();
        }

        private Result(IEnumerable<Error> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        public static Result<T> Success(T value)
            => new Result<T>(value);

        public static Result<T> Fail(Error error)
        => new Result<T>(new[] { error });

        public static Result<T> Fail(IEnumerable<Error> errors)
            => new Result<T>(errors);

        public static Result<T> Fail(IEnumerable<NotificationItem> errors)
            => new Result<T>(errors.Select(e => (Error)e));
    }

    public class Result
    {
        public bool IsSuccess { get; }
        public IEnumerable<Error> Errors { get; }

        private Result(bool success, IEnumerable<Error> errors)
        {
            IsSuccess = success;
            Errors = errors;
        }
        public static Result Success()
        => new Result(true, Enumerable.Empty<Error>());
        public static Result Fail(Error error)
        => new Result(false, new[] { error });

        public static Result Fail(IEnumerable<Error> errors)
            => new Result(false, errors);

        public static Result Fail(IEnumerable<NotificationItem> errors)
            => new Result(false, errors.Select(e => (Error)e));
    }
}
