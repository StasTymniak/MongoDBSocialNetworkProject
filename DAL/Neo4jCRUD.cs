using Neo4jClient;
using Neo4jClient.Cypher;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace DAL
{
    public class Person
    {

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }
    public class Neo4jCRUD
    {
        public GraphClient client;
        public Neo4jCRUD()
        {
            client = new GraphClient(new Uri("http://localhost:7474/db/data/"), "user", "pass");

            client.Connect();
        }

        public void CreateRelation(string user1Id, string user2Id)
        {
            client.Cypher
              .Match("(user1:Person {userId: {user1Id}})", "(user2:Person {userId: {user2Id}})")
              .WithParam("user1Id", user1Id)
              .WithParam("user2Id", user2Id)
              .Create("(user1)-[:Following]->(user2)")
              .ExecuteWithoutResults();
        }

        public int PathLength(string user1Id, string user2Id)
        {

            try
            {
                var path = client.Cypher
           .Match("(u1:Person {userId:{user1Id}} )",
                  "(u2: Person { userId:{user2Id}})",
                  "p = shortestPath((u1) -[:Following *]->(u2))")
           .WithParam("user1Id", user1Id)
           .WithParam("user2Id", user2Id)
           .Return(p => Return.As<int>("length(p)"))
           .Results;
                return path.First();
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public void DeleteRelation(string user1Id, string user2Id)
        {

            client.Cypher
            .Match("(user1:Person{userId:{user1Id}})-[r:Following]->(user2:Person{userId:{user2Id}})")
            .WithParam("user1Id", user1Id)
            .WithParam("user2Id", user2Id)
            .Delete("r")
            .ExecuteWithoutResults();
        }
    }
}
