namespace Identity.Infastructure.Application.Models
{
    public sealed class ResultModel<T>
    {
        public bool IsSuccessed { get; private set; }
        public T Data { get; private set; }
        public string Message { get; private set; }

        public static ResultModel<T> Done(T data)
        {
            return new ResultModel<T>()
            {
                Data = data,
                IsSuccessed = true
            };
        }

        public static ResultModel<T> NoData(string message)
        {
            return new ResultModel<T>()
            {
                IsSuccessed = true,
                Message = message
            };
        }

        public static ResultModel<T> Failed(string errorMessage)
        {
            return new ResultModel<T>()
            {
                IsSuccessed = false,
                Message = errorMessage
            };
        }
        
    }
}
