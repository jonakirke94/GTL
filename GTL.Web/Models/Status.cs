namespace GTL.Web.Models
{
    public class Status
    {
        public Type Type { get; set; }

        public string Message { get; set; }
    }

    public enum Type
    {
        success, info, warning, danger
    }
}
