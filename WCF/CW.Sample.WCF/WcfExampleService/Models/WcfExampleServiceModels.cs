namespace Shared.WcfExampleService.Models
{
    public class SimpleTaskMethodResponseModel
    {
        public byte[] Result { get; set; }
    }

    public class SimpleTaskMethodRequestModel
    {
        public int Size { get; set; }
    }

    public class SimpleSyncMethodRequestModel
    {
        public string Message { get; set; }
    }

    public class SimpleSyncMethodResponseModel
    {
        public string Message { get; set; }
    }
}