using Newtonsoft.Json;
using System.Collections;

namespace DrinkVendingMachine.API.ExtensionsMethods
{
    public static class SessionExtensions
    {
        public static void SetCollectionAsJson<T>(this ISession session, string key, T collection) where T : ICollection
          =>  session.SetString(key, JsonConvert.SerializeObject(collection));

        public static T GetCollectionFromJson<T>(this ISession session, string key) where T : ICollection
        {
            var value = session.GetString(key);

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
