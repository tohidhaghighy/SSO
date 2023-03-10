namespace SSO.Core.Common
{
    public class BaseModel<T>
    {
        public T Id { get; set; }
        public DateTime InsertDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string InsertUserName { get; set; }
    }
}
