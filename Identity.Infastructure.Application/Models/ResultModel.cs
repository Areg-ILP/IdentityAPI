namespace Identity.Infastructure.Application.Models
{
    public sealed class ResultModel<T>
    {
        public bool IsSuccessed { get; private set; }
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }

        public void Done(T data)
        {
            Data = data;
            IsSuccessed = true;
        }

        public void Failed(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccessed = false;
        }
    }
}
