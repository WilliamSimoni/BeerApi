namespace Domain.Common.Errors.Base
{
    public interface IError
    {
        public string Message { get; }
        public string Code { get; }
        public int Number { get; }
    }

}
