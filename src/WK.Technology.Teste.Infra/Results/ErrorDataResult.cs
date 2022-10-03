namespace WK.Technology.Teste.Infra.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data, bool success, string message) : base(data, success: false, message)
        {
        }

        public ErrorDataResult(T data, string message) : base(data, success: false, message)
        {
        }

        public ErrorDataResult(T data) : base(data, success: false)
        {
        }

        public ErrorDataResult(string message) : base(default, success: false, message)
        {
        }

        public ErrorDataResult() : base(default, success: false)
        {
        }
    }
}
