using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Post
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public ObjectId userId { get; set; }
        public string Text { get; set; }
        public DateTime insertTime { get; set; }
        public List<ObjectId> CommentsId { get; set; }
        public List<ObjectId> UserLikesId { get; set; }

    }
}
