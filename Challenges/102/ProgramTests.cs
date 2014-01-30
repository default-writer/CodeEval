#region

using Challenges;
using NUnit.Framework;

#endregion

namespace CodeEval
{
    [TestFixture]
    internal class ProgramTests
    {
        [TestCase]
        public void Test1()
        {
            Assert.AreEqual(Json.ParseEelement(""), null);
        }

        [TestCase]
        public void Test2()
        {
            Assert.AreEqual(Json.ParseEelement(" "), null);
        }

        [TestCase]
        public void Test3()
        {
            Assert.AreEqual(Json.ParseEelement("\r"), null);
        }

        [TestCase]
        public void Test4()
        {
            Assert.AreEqual(Json.ParseEelement("\n"), null);
        }

        [TestCase]
        public void Test5()
        {
            Assert.AreEqual(Json.ParseEelement("\t"), null);
        }

        [TestCase]
        public void Test6()
        {
            Assert.AreEqual(Json.ParseEelement("q"), null);
        }

        [TestCase]
        public void Test7()
        {
            Assert.AreEqual(Json.ParseEelement("{}"), null);
        }

        [TestCase]
        public void Test8()
        {
            Assert.AreEqual(Json.ParseEelement("{\"}"), null);
        }

        [TestCase]
        public void Test81()
        {
            Assert.AreEqual(Json.ParseEelement("{\"}}"), null);
        }

        [TestCase]
        public void Test9()
        {
            Assert.AreEqual(Json.ParseEelement("{\"\"}"), null);
        }

        [TestCase]
        public void Test91()
        {
            Assert.AreEqual(Json.ParseEelement("{\"\"\"}"), null);
        }

        [TestCase]
        public void Test10()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\"}"), null);
        }

        [TestCase]
        public void Test11()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\":}"), null);
        }

        [TestCase]
        public void Test12()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": }"), null);
        }

        [TestCase]
        public void Test13()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": {}"), null);
        }

        [TestCase]
        public void Test14()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": {\"\"\"}"), null);
        }

        [TestCase]
        public void Test15()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": 1"), null);
        }

        [TestCase]
        public void Test16()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": 1}").Text, "{\"a\": 1}");
        }

        [TestCase]
        public void Test17()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": 1}}"), null);
        }

        [TestCase]
        public void Test18()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": \"1\"}").Text, "{\"a\": \"1\"}");
        }

        [TestCase]
        public void Test181()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": 1,\"b\": 2}").Text, "{\"a\": 1, \"b\": 2}");
        }

        [TestCase]
        public void Test19()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": \"1\"}}"), null);
        }

        [TestCase]
        public void Test20()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": ["), null);
        }

        [TestCase]
        public void Test21()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [ "), null);
        }

        [TestCase]
        public void Test22()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": []"), null);
        }

        [TestCase]
        public void Test23()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": []"), null);
        }

        [TestCase]
        public void Test24()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [ ]"), null);
        }

        [TestCase]
        public void Test241()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": []}"), null);
        }

        [TestCase]
        public void Test242()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [ ]}"), null);
        }

        [TestCase]
        public void Test25()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [ ]"), null);
        }

        [TestCase]
        public void Test26()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{}]}"), null);
        }

        [TestCase]
        public void Test28()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{\"id\", 81}]"), null);
        }

        [TestCase]
        public void Test29()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{\"id\", 81}]}"), null);
        }

        [TestCase]
        public void Test291()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{\"id\": 81}]}").Text, "{\"a\": [{\"id\": 81}]}");
        }

        [TestCase]
        public void Test292()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{\"id\": 81}, {\"id\": 81}]}").Text, "{\"a\": [{\"id\": 81}, {\"id\": 81}]}");
        }

        [TestCase]
        public void Test201()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [null]}").Text, "{\"a\": [null]}");
        }

        [TestCase]
        public void Test202()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [null]"), null);
        }

        [TestCase]
        public void Test203()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [null"), null);
        }

        [TestCase]
        public void Test204()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [nul"), null);
        }

        [TestCase]
        public void Test2051()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [nu"), null);
        }

        [TestCase]
        public void Test2052()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [n"), null);
        }

        [TestCase]
        public void Test2053()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": ["), null);
        }

        [TestCase]
        public void Test206()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [n]"), null);
        }

        [TestCase]
        public void Test207()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [nu]"), null);
        }

        [TestCase]
        public void Test208()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [nul]}"), null);
        }

        [TestCase]
        public void Test205()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": []}"), null);
        }

        [TestCase]
        public void Test2922()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [null, null]}").Text, "{\"a\": [null, null]}");
        }

        [TestCase]
        public void Test293()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{\"id\": 81}, null]}").Text, "{\"a\": [{\"id\": 81}, null]}");
        }

        [TestCase]
        public void Test294()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": \"b\"}").Text, "{\"a\": \"b\"}");
        }

        [TestCase]
        public void Test2931()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": [{\"id\": 81}, null, {\"id\": 81}]}").Text, "{\"a\": [{\"id\": 81}, null, {\"id\": 81}]}");
        }

        [TestCase]
        public void Test221()
        {
            Assert.AreEqual(Json.ParseEelement("{\"a\": {\"b\": 2, \"c\": 3}}").Text, "{\"a\": {\"b\": 2, \"c\": 3}}");
        }

        [TestCase]
        public void Test_1()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}");
        }

        [TestCase]
        public void Test_2()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}");
        }


        [TestCase]
        public void Test_3()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }

        [TestCase]
        public void Test_11()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\":  {\"header\":  \"menu\",  \"items\":  [{\"id\":  27},  {\"id\":  0,  \"label\":  \"Label 0\"},  null,  {\"id\":  93},  {\"id\":  85},  {\"id\":  54},  null,  {\"id\":  46,  \"label\":  \"Label 46\"}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}");
        }

        [TestCase]
        public void Test_21()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\":  {\"header\":  \"menu\",  \"items\":  [{\"id\":  81}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}");
        }


        [TestCase]
        public void Test_31()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\":  {\"header\":  \"menu\",  \"items\":  [{\"id\":  70,  \"label\":  \"Label 70\"},  {\"id\":  85,  \"label\":  \"Label 85\"},  {\"id\":  93,  \"label\":  \"Label 93\"},  {\"id\":  2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }

        [TestCase]
        public void Test_12()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\":{\"header\":\"menu\",\"items\":[{\"id\":27},{\"id\":0,\"label\":\"Label 0\"},null,{\"id\":93},{\"id\":85},{\"id\":54},null,{\"id\":46,\"label\":\"Label 46\"}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}");
        }

        [TestCase]
        public void Test_22()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\":{\"header\":\"menu\",\"items\":[{\"id\":81}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}");
        }


        [TestCase]
        public void Test_32()
        {
            Assert.AreEqual(Json.ParseEelement("{\"menu\":{\"header\":\"menu\",\"items\":[{\"id\":70,\"label\":\"Label 70\"},{\"id\":85,\"label\":\"Label 85\"},{\"id\":93,\"label\":\"Label 93\"},{\"id\":2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }
    }
}