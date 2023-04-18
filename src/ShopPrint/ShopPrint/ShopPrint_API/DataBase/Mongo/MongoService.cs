using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Security.Authentication;
using System.Xml.Xsl;

namespace ShopPrint_API.DataBase.Mongo
{
    public class MongoService
    {
        public readonly IMongoDatabase _iMongoDatabase;
        public MongoService(IOptions<MongoSettings> mongoSettings)
        {
            MongoClientSettings settings = new MongoClientSettings();
            settings.Server = new MongoServerAddress(mongoSettings.Value.Host, Convert.ToInt32(mongoSettings.Value.Port));
            settings.UseTls = false;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            MongoIdentity identity = new MongoInternalIdentity(mongoSettings.Value.DatabaseName, mongoSettings.Value.User);
            MongoIdentityEvidence evidence = new PasswordEvidence(mongoSettings.Value.Password);
            settings.Credential = new MongoCredential(mongoSettings.Value.MongoDbAuthMechanism, identity, evidence);

            MongoClient client = new MongoClient(settings);
            _iMongoDatabase = client.GetDatabase(mongoSettings.Value.DatabaseName);
        }
    }
}
