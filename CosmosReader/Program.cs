using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;

namespace CosmosReader
{
    class Program
    {
        static void Main(string[] args)
        {
            CosmosClient cosmosClient;
            Database database;
            Container container;

            cosmosClient = new CosmosClient("AccountEndpoint=https://cosmosdb-ime-demo.documents.azure.com:443/;AccountKey=HEPSeOSgBqAng0VpzlbAOXJVDiDAVDj0CoMXhiM2TBoOl9my5iu4GsMlRNB4nWW2BkDkdSMT3wNN8t4KhHhRZA==;");
            database = cosmosClient.GetDatabase("destinationdb01");
            container = database.GetContainer("People");

            var sqlQueryText = "SELECT * FROM c WHERE c.city = 'Medellin'";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQueryText);
            FeedIterator<Person> queryResultSetIterator = container.GetItemQueryIterator<Person>(queryDefinition);

            List<Person> peopleInMedellin = new List<Person>();

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Person> currentResultSet = queryResultSetIterator.ReadNextAsync().Result;
                foreach (Person person in currentResultSet)
                {
                    peopleInMedellin.Add(person);
                    Console.WriteLine($"Hi {person.Name}");
                }
            }
        }
    }
}
