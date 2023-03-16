namespace Catalog.WebApi.Data;

public class Connection
{

/*

   From docs: https://www.mongodb.com/docs/manual/reference/connection-string/

   mongodb://[username:password@]host1[:port1][,...hostN[:portN]][/[defaultauthdb][?options]]

*/

    public static string GetConnectionString(ConfigurationManager configuration)
    {
        var user = configuration["MONGO_USER"];
        var password = configuration["MONGO_PASS"];
        return $"mongodb://{user}:{password}@localhost:27017";
        // return "mongodb://localhost:27017";
    }
}
