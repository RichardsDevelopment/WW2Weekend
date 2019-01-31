using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Core.Clusters;
using MongoDB.Driver.Core.Servers;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using WW2Weekend.Classes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WW2Weekend
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Event> collection;
        private List<Event> batch;

        /*
        public class Event
        {
            [BsonId]
            public ObjectId id { get; set; }
            [BsonElement("Name")]
            public string name { get; set; }
            [BsonElement("Description")]
            public string description { get; set; }
            [BsonElement("Location")]
            public string location { get; set; }
            [BsonElement("DateTime")]
            public DateTime datetime { get; set; }
        }
        */
        public EventsPage ()
		{
            InitializeComponent ();

            MainAsync();
        }

        private async Task MainAsync()
        {
            if (await GetConnectStatus())
            {
                if (await tryServer())
                {
                    if (await connectToDb())
                    {
                        await pullDocuments();

                        Console.WriteLine("Pulled documents...");
                        EventsListView.ItemsSource = batch;
                    }
                }
            }
            else
            {
                Console.WriteLine("Connection to web failed!");
            }

        }
       
        private async Task<bool> GetConnectStatus()
        {

            bool result = false;

            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    // Connection available
                    Console.WriteLine("Internet connection detected.");
                    result = true;
                }
                else
                {
                    // No internet connection available
                    Console.WriteLine("No internet connection detected!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return result;
        }

        private async Task<bool> tryServer()
        {
            bool response = false;

            try
            {
                client = new MongoClient("mongodb://dbUser:LJ9UaQsS4ZxDYSG@cluster0-shard-00-00-lnyfj.mongodb.net:27017,cluster0-shard-00-01-lnyfj.mongodb.net:27017,cluster0-shard-00-02-lnyfj.mongodb.net:27017/test?ssl=true&replicaSet=Cluster0-shard-0&authSource=admin&retryWrites=true");
                var databases = client.ListDatabasesAsync().Result;
                await databases.MoveNextAsync(); // Force MongoDB to connect to the database.

                if (client.Cluster.Description.State == ClusterState.Connected)
                {
                    // Database is connected.
                    Console.WriteLine("Mongo server is reachable.");
                    response = true;
                }
                else
                {
                    Console.WriteLine("Mongo unreachable");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return response;
        }

        private async Task<bool> connectToDb()
        {
            Console.WriteLine("Connecting to Database...");
            db = client.GetDatabase("MWM");

            bool status = (client.Cluster.Description.State == ClusterState.Connected);

            if (status)
            {
                collection = db.GetCollection<Event>("Events");

                Console.WriteLine("Got table...");
            }

            return status;
        }

        private async Task pullDocuments()
        {
            Console.WriteLine("Pulling Documents...");

            var filter = new FilterDefinitionBuilder<Event>().Empty;
            batch = await collection.Find(FilterDefinition<Event>.Empty).ToListAsync<Event>();

            Console.WriteLine("Returning to main...");

            return;
        }

    }
}