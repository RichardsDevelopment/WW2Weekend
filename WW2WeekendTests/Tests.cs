using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using WW2WeekendTests;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace WW2WeekendUITest
{
    [TestFixture(Platform.Android)]
    //[TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        static readonly Func<AppQuery, AppQuery> MapButton = c => c.Marked("MainMapButton");
        static readonly Func<AppQuery, AppQuery> EventsButton = c => c.Marked("MainEventsButton");
        static readonly Func<AppQuery, AppQuery> DonateButton = c => c.Marked("MainDonateButton");

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Repl();

            AppResult[] results = app.Query(MapButton);
            Assert.IsTrue(results.Any());

            results = app.Query(EventsButton);
            Assert.IsTrue(results.Any());

            results = app.Query(DonateButton);
            Assert.IsTrue(results.Any());
        }
    }
}