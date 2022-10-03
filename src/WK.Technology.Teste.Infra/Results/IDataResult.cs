namespace WK.Technology.Teste.Infra.Results
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
