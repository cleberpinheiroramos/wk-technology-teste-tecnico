namespace WK.Technology.Teste.Infra.Results
{
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(success: true, message)
        {
        }

        public SuccessResult() : base(success: true)
        {
        }
    }
}
