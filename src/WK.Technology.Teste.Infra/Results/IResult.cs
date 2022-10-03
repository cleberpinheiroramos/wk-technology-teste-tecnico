namespace WK.Technology.Teste.Infra.Results
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
