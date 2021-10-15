using Neo4jClient;
using System;
using SocialNetworkWPF;
using DTO;
using DAL;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json;
using Neo4jClient.Cypher;
using System.Linq;

namespace ConsoleApp1
{
    public class Person
    {

      [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data/"), "user", "pass");

            client.Connect();

            MongoCRUD db = new MongoCRUD("SocialNetwork");
            Neo4jCRUD graph = new Neo4jCRUD();
            List<User> users = db.ReadEntity<User>("Users");

            /*foreach (User user in users)
            {
                Person person = new Person
                {
                    UserId = user.Id
                };

                client.Cypher
                    .Create("(user:Person {newPerson})")
                    .WithParam("newPerson", person)
                    .ExecuteWithoutResults();
            }*/
            /*foreach (User user in users)
            {
                foreach (ObjectId user2Id in user.FollowingsId)
                {
                    graph.CreateRelation(Convert.ToString(user.Id), Convert.ToString(user2Id));
                }

            }*/


            /*client.Cypher
              .Match("(user1:Person {userId: '6154ccec9904cf027a0729d2'})", "(user2:Person {userId:'6156dcfb04479cf81e4c9662'})")
              .Create("(user1)-[:Following]->(user2)")
              .ExecuteWithoutResults();
*/
            /* var actorAndMovies = client.Cypher
                   .Match("(user1:Person {UserId: {user1Id}})-[:Following]->(user2:Person)")
                   .WithParam("user1Id", ObjectId.Parse("6154ccec9904cf027a0729d2"))
                   .Return((user1) => new
                   {
                       user1 = user1.As<Person>()
                   })
                   .Results;*/


            /*client.Cypher
              .Match("(user1:Person {UserId: {user1Id}})", "(user2:Person {UserId: {user2Id}})")
              .WithParam("user1Id",ObjectId.Parse("6154ccec9904cf027a0729d2"))
              .WithParam("user2Id",ObjectId.Parse("6156dcfb04479cf81e4c9662"))
              .Create("(user1)-[:Following]->(user2)")
              .ExecuteWithoutResults();*/

            //Return path
            /*            var path = client.Cypher
                            .Match("p=(a:Person{name:'Stas'})-[:Following *]->(b:Person{name:'Vasya'})")
                            .Return(p => new
                            {
                                *//*shortestPath = Return.As<IEnumerable<Node<Person>>>("nodes(p)")*//*
                                length = Return.As<int>("length(p)")
                            }).Results;

                        Console.WriteLine(path.ToList()[0].length);
            */
            /*     //Delete relationship
                 client.Cypher
                 .Match("(a:Person{name:{1Name}})-[r:Following]->(b:Person{name:{2Name}})")
                 .WithParam("1Name", "Stas")
                 .WithParam("2Name", "Petro")
                 .Delete("r")
                 .ExecuteWithoutResults();
     */
            int a = graph.PathLength("6156dcfb04479cf81e4c9662", "6154ccec9904cf027a0729d2");
            var path = client.Cypher
                            .Match("(u1:Person {userId: '6156dcfb04479cf81e4c9662'} )",
                                    "(u2: Person { userId: '6154ccec9904cf027a0729d2'})",
                                    "p = shortestPath((u1) -[:Following *]->(u2))")
                            .Return(p => new
                            {
                                length = Return.As<int>("length(p)")
                            }).Results;


            Console.WriteLine(a);
                
        }
    }
}
