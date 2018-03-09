using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.ReadModel.Repositories
{
    public class BaseRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        private readonly string _namespace;

        public BaseRepository(IConnectionMultiplexer redisConnection, string @namespace)
        {
            _redisConnection = redisConnection;
            _namespace = @namespace;
        }

        public async Task<T> Get<T>(int id)
        {
            return await Get<T>(id.ToString());
        }

        public async Task<T> Get<T>(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = await database.StringGetAsync(key);
            if (serializedObject.IsNullOrEmpty)
                return default(T);

            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public List<T> GetMultiple<T>(List<int> ids)
        {
            var database = _redisConnection.GetDatabase();
            List<RedisKey> keys = new List<RedisKey>();
            foreach (var id in ids)
            {
                keys.Add(MakeKey(id));
            }

            var serializedItems = database.StringGet(keys.ToArray(), CommandFlags.None);
            List<T> items = new List<T>();
            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public bool Exists(int id)
        {
            return Exists(id.ToString());
        }

        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNull;
        }

        public async Task Save(int id, object entity)
        {
            await Save(id.ToString(), entity);
        }

        public async Task Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            await database.StringSetAsync(key, JsonConvert.SerializeObject(entity));
        }

        private string MakeKey(int id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
                return _namespace + ":" + keySuffix;

            return keySuffix;
        }
    }
}
