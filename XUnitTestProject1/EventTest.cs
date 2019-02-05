using System;
using System.Collections.Generic;
using System.Text;
using WW2Weekend.Classes;
using Xunit;
using MongoDB.Bson;

namespace WW2WeekendTest
{
    public class EventTest
    {
        private Event TestEvent = new Event();

        [Theory]
        [MemberData("GetObjects")]
        public void TestEventId(ObjectId input)
        {
            TestEvent.Id = input;

            Assert.Equal(TestEvent.Id.ToString(), input.ToString());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("Test Name", "Test Name")]
        [InlineData("This is a really long name to 1243876|)(@&*$*&^ test that it works", "This is a really long name to 1243876|)(@&*$*&^ test that it works")]
        public void TestEventName(string input, string expected)
        {
            TestEvent.Name = input;

            Assert.Equal(expected, TestEvent.Name);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("This is a test description for running a test.", "This is a test description for running a test.")]
        [InlineData("This is a really long description to 1243876|)(@&*$*&^ test that it works. And hopefully it does, because there is no length of string that should break this!", "This is a really long description to 1243876|)(@&*$*&^ test that it works. And hopefully it does, because there is no length of string that should break this!")]
        public void TestEventDescription(string input, string expected)
        {
            TestEvent.Description = input;

            Assert.Equal(expected, TestEvent.Description);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("My House", "My House")]
        [InlineData("This is a really long name to 1243876|)(@&*$*&^ test that it works", "This is a really long name to 1243876|)(@&*$*&^ test that it works")]
        public void TestEventLocation(string input, string expected)
        {
            TestEvent.Location = input;

            Assert.Equal(expected, TestEvent.Location);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("TODAY 2/3/2019 at 4:34pm", "TODAY 2/3/2019 at 4:34pm")]
        [InlineData("This is a really long string to 1243876|)(@&*$*&^ test that it works", "This is a really long string to 1243876|)(@&*$*&^ test that it works")]
        public void TestEventDateTime(string input, string expected)
        {
            TestEvent.Datetime = input;

            Assert.Equal(expected, TestEvent.Datetime);
        }

        public static IEnumerable<object[]> GetObjects
        {
            get
            {
                return new[]
                {
                    new object[] { new ObjectId() }
                };
            }
        }
    }
}
