using Neo4jClient;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace neo4jConsole
{
    public class Person
    {
        public string name;
        public string title;
    }

    class Program
    {
        static void Main(string[] args)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/data/db"), "username", "password");

            client.Connect();

            var results = client.Cypher
                .Match("(a:Person) ")
                .Set("a:Person")
                .Return((person) => new
                {
                    Person = person.As<Person>(),

                }).Results;

            foreach (var item in results)
            {
                Console.WriteLine(item.Person.name);
            }
        }
    }
}
    

