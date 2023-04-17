namespace ShopPrint_API.DataBase.Mongo
{
    public class MongoSettings
    {
        public string Host { get; set; } = null!;
        public string Port { get; set; } = null!;

        public string User { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string MongoDbAuthMechanism { get; set; } = null!;
    }
}
