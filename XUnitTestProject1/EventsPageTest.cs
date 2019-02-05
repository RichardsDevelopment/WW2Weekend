using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using WW2Weekend;
using Xunit;

namespace TestWW2Weekend
{
    public class EventsPageTest
    {
        EventsPage ep = new EventsPage();

        [Fact]
        public void Test_GetConnectStatus()
        {
            Assert.Equal(CrossConnectivity.Current.IsConnected, ep.GetConnectStatus());
        }

        [Fact]
        public void Test_TryServer()
        {
            MongoClient client = new MongoClient();

            try
            {
                client = new MongoClient("mongodb://dbUser:LJ9UaQsS4ZxDYSG@cluster0-shard-00-00-lnyfj.mongodb.net:27017,cluster0-shard-00-01-lnyfj.mongodb.net:27017,cluster0-shard-00-02-lnyfj.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true");
                var databases = client.ListDatabasesAsync().Result;
                databases.MoveNextAsync().Wait(); // Force MongoDB to connect to the database.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            bool result = client.Cluster.Description.State == ClusterState.Connected;

            Assert.Equal(result, ep.TryServer());
        }
    }
}
