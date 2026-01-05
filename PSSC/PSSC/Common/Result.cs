using System;
using System.Collections.Generic;
using System.Linq;

namespace PSSC.Common
{
    // Clasa generică pentru rezultate
    public class Result<TValue>
    {
        public bool IsSuccess { get; }
        public TValue Value { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        protected internal Result(TValue value, bool isSuccess, string error)
        {
            if (isSuccess && error != null)
                throw new InvalidOperationException();
            if (!isSuccess && error == null)
                throw new InvalidOperationException();

            Value = value;
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<TValue> Success(TValue value) => new(value, true, null);
        public static Result<TValue> Failure(string error) => new(default, false, error);

        public void Match(Action<TValue> onSuccess, Action<string> onFailure)
        {
            if (IsSuccess) onSuccess(Value);
            else onFailure(Error);
        }

        // Metodă de tip "Match" pentru a forța tratarea ambelor cazuri (similar cu pattern matching-ul din F#)
        public TResult Match<TResult>(
            Func<TValue, TResult> onSuccess,
            Func<string, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value) : onFailure(Error);
        }
    }

    // Extensii pentru "Railway Oriented Programming" (pentru compoziția operațiilor)
    public static class ResultExtensions
    {
        public static async Task<Result<TNext>> Bind<TCurrent, TNext>(
            this Task<Result<TCurrent>> resultTask,
            Func<TCurrent, Task<Result<TNext>>> nextStep)
        {
            var result = await resultTask;
            return result.IsSuccess ? await nextStep(result.Value) : Result<TNext>.Failure(result.Error);
        }

        public static Result<TNext> Bind<TCurrent, TNext>(
            this Result<TCurrent> result,
            Func<TCurrent, Result<TNext>> nextStep)
        {
            return result.IsSuccess ? nextStep(result.Value) : Result<TNext>.Failure(result.Error);
        }
    }
}