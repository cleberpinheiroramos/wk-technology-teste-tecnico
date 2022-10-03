namespace WK.Technology.Teste.Infra.Results
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(success: false, message)
        {
        }

        public ErrorResult() : base(success: false)
        {
        }
    }
}
