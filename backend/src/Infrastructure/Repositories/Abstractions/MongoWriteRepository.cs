using System.Threading.Tasks;
using MongoDB.Bson;
using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Mongo;

namespace Infrastructure.Repositories.Abstractions
{
    public class MongoWriteRepository<T> : IWriteRepository<T>
        where T : Entity
    {
        private readonly MongoConnectionFactory _context;

        public MongoWriteRepository(MongoConnectionFactory context)
        {
            _context = context;
        }

        public async Task<Entity> CreateAsync(T entity)
        {
            await _context.Collection<T>().InsertOneAsync(entity);

            return entity;
        }

        public async Task<Entity> UpdateAsync(T entity)
        {
            ObjectId oid = ObjectId.Parse(entity.Id);
            BsonDocument filter = new BsonDocument(new BsonElement("_id", new BsonObjectId(oid)));
            await _context.Collection<T>().ReplaceOneAsync(filter, entity);

            return entity;
        }

        public async Task DeleteAsync(string id)
        {
            ObjectId oid = ObjectId.Parse(id);
            BsonDocument filter = new BsonDocument(new BsonElement("_id", new BsonObjectId(oid)));

            await _context.Collection<T>().DeleteOneAsync(filter);
        }
    }
}
