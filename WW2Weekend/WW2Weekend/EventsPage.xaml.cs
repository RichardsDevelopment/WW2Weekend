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
        private Event batch;

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

        public EventsPage ()
		{
            InitializeComponent ();

            MainAsync().Wait();
        }

        private async Task MainAsync()
        {
            //bool connection = await GetConnectStatus();

            if (await GetConnectStatus())
            {
                //bool mongoPing = await tryServer();

                if (await tryServer())
                {
                    if (await connectToDb())
                    {
                        await pullDocuments();

                        Console.WriteLine("Pulled documents...");
                                            
                        docsLabel.Text = "Name: "+ batch.name;
                        Console.WriteLine();                      
                    }
                }
            }
            else
            {
                Console.WriteLine("Connection to web failed!");
            }

        }
        /*
        private async Task<bool> TestConnectionToInternet()
        {
            Task<bool> getStatus = GetConnectStatus();

            bool result = await getStatus;

            return result;
        }
        */
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

                    intConButton.BackgroundColor = Color.Green;
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

        /*
         *  CHECKING CONNECTION TO MONGODBSERVER 
         
        private async Task<bool> PingMongoCluster()
        {
            bool result = false;

            Task<bool> getMongoStatus = tryServer();
            result = await getMongoStatus;
            
            return result;
        }
        */
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

                    monConButton.BackgroundColor = Color.Green;
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
            /*
            using (IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(new BsonDocument()))
            {
                while (await cursor.MoveNextAsync())
                {
                    batch = cursor.Current;

                    foreach (BsonDocument document in batch)
                    {
                        Console.WriteLine(document);
                        Console.WriteLine();
                    }
                }
            }
            */
            Console.WriteLine("Pulling Documents...");

            var filter = new FilterDefinitionBuilder<Event>().Empty;
            batch = collection.Find(FilterDefinition<Event>.Empty).Single();

            Console.WriteLine("Returning to main...");

            return;
        }

    }
}