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
            Assert.AreEqual(Json.Parse(""), null);
        }

        [TestCase]
        public void Test2()
        {
            Assert.AreEqual(Json.Parse(" "), null);
        }

        [TestCase]
        public void Test3()
        {
            Assert.AreEqual(Json.Parse("\r"), null);
        }

        [TestCase]
        public void Test4()
        {
            Assert.AreEqual(Json.Parse("\n"), null);
        }

        [TestCase]
        public void Test5()
        {
            Assert.AreEqual(Json.Parse("\t"), null);
        }

        [TestCase]
        public void Test6()
        {
            Assert.AreEqual(Json.Parse("q"), null);
        }

        [TestCase]
        public void Test7()
        {
            Assert.AreEqual(Json.Parse("{}"), null);
        }

        [TestCase]
        public void Test8()
        {
            Assert.AreEqual(Json.Parse("{\"}"), null);
        }

        [TestCase]
        public void Test81()
        {
            Assert.AreEqual(Json.Parse("{\"}}"), null);
        }

        [TestCase]
        public void Test9()
        {
            Assert.AreEqual(Json.Parse("{\"\"}"), null);
        }

        [TestCase]
        public void Test91()
        {
            Assert.AreEqual(Json.Parse("{\"\"\"}"), null);
        }

        [TestCase]
        public void Test10()
        {
            Assert.AreEqual(Json.Parse("{\"a\"}"), null);
        }

        [TestCase]
        public void Test11()
        {
            Assert.AreEqual(Json.Parse("{\"a\":}"), null);
        }

        [TestCase]
        public void Test12()
        {
            Assert.AreEqual(Json.Parse("{\"a\": }"), null);
        }
        [TestCase]
        public void Test121()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [\"b\": ]}"), null);
        }

        [TestCase]
        public void Test13()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {}"), null);
        }

        [TestCase]
        public void Test14()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"\"\"}"), null);
        }

        [TestCase]
        public void Test15()
        {
            Assert.AreEqual(Json.Parse("{\"a\": 1"), null);
        }

        [TestCase]
        public void Test16()
        {
            Assert.AreEqual(Json.Parse("{\"a\": 1}").Text, "{\"a\": 1}");
        }

        [TestCase]
        public void Test17()
        {
            Assert.AreEqual(Json.Parse("{\"a\": 1}}"), null);
        }

        [TestCase]
        public void Test18()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"1\"}").Text, "{\"a\": \"1\"}");
        }

        [TestCase]
        public void Test181()
        {
            Assert.AreEqual(Json.Parse("{\"a\": 1,\"b\": 2}").Text, "{\"a\": 1, \"b\": 2}");
        }

        [TestCase]
        public void Test19()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"1\"}}"), null);
        }

        [TestCase]
        public void Test20()
        {
            Assert.AreEqual(Json.Parse("{\"a\": ["), null);
        }

        [TestCase]
        public void Test21()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [ "), null);
        }

        [TestCase]
        public void Test22()
        {
            Assert.AreEqual(Json.Parse("{\"a\": []"), null);
        }

        [TestCase]
        public void Test23()
        {
            Assert.AreEqual(Json.Parse("{\"a\": []"), null);
        }

        [TestCase]
        public void Test24()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [ ]"), null);
        }

        [TestCase]
        public void Test241()
        {
            Assert.AreEqual(Json.Parse("{\"a\": []}"), null);
        }

        [TestCase]
        public void Test242()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [ ]}"), null);
        }

        [TestCase]
        public void Test25()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [ ]"), null);
        }

        [TestCase]
        public void Test26()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{}]}"), null);
        }

        [TestCase]
        public void Test28()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{\"id\", 81}]"), null);
        }
        [TestCase]
        public void Test2812()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [\"id\"]"), null);
        }
        [TestCase]
        public void Test2813()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [\"id\"]"), null);
        }
        [TestCase]
        public void Test281()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [\"id\", null, {\"id\": 81}]"), null);
        }
        [TestCase]
        public void Test282()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [\"id\", null, {\"id: 81}]"), null);
        }
        [TestCase]
        public void Test283()
        {
            Assert.AreEqual(Json.Parse("{a\": [\"id\", null, {\"id: 81}]"), null);
        }
        [TestCase]
        public void Test284()
        {
            Assert.AreEqual(Json.Parse("{\"a: [\"id\", null, {\"id: 81}]"), null);
        }

        [TestCase]
        public void Test29()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{\"id\", 81}]}"), null);
        }

        [TestCase]
        public void Test291()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{\"id\": 81}]}").Text, "{\"a\": [{\"id\": 81}]}");
        }

        [TestCase]
        public void Test292()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{\"id\": 81}, {\"id\": 81}]}").Text, "{\"a\": [{\"id\": 81}, {\"id\": 81}]}");
        }

        [TestCase]
        public void Test201()
        {
            IElement element = Json.Parse("{\"a\": [null]}");
            Assert.AreNotEqual(null, element);
            Assert.AreEqual("{\"a\": [null]}", element.Text);
        }

        [TestCase]
        public void Test202()
        {
            IElement element = Json.Parse("{\"a\": [null]");
            Assert.AreEqual(null, element);
        }

        [TestCase]
        public void Test203()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [null"), null);
        }

        [TestCase]
        public void Test204()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [nul"), null);
        }

        [TestCase]
        public void Test2051()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [nu"), null);
        }

        [TestCase]
        public void Test2052()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [n"), null);
        }

        [TestCase]
        public void Test2053()
        {
            Assert.AreEqual(Json.Parse("{\"a\": ["), null);
        }

        [TestCase]
        public void Test206()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [n]"), null);
        }

        [TestCase]
        public void Test207()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [nu]"), null);
        }

        [TestCase]
        public void Test208()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [nul]}"), null);
        }

        [TestCase]
        public void Test205()
        {
            Assert.AreEqual(Json.Parse("{\"a\": []}"), null);
        }

        [TestCase]
        public void Test2922()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [null, null]}").Text, "{\"a\": [null, null]}");
        }

        [TestCase]
        public void Test293()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{\"id\": 81}, null]}").Text, "{\"a\": [{\"id\": 81}, null]}");
        }

        [TestCase]
        public void Test294()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\"}").Text, "{\"a\": \"b\"}");
        }

        [TestCase]
        public void Test2931()
        {
            Assert.AreEqual(Json.Parse("{\"a\": [{\"id\": 81}, null, {\"id\": 81}]}").Text, "{\"a\": [{\"id\": 81}, null, {\"id\": 81}]}");
        }

        [TestCase]
        public void Test221()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 2, \"c\": 3}}").Text, "{\"a\": {\"b\": 2, \"c\": 3}}");
        }

        [TestCase]
        public void Test2211()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 2. \"c\": 3}}"), null);
        }
        [TestCase]
        public void Test2212()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 2. \"c\": 3"), null);
        }
        [TestCase]
        public void Test2213()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 2, \"c\": 3}"), null);
        }
        [TestCase]
        public void Test22131()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\""), null);
        }
        [TestCase]
        public void Test2214()
        {
            Assert.AreEqual(Json.Parse("{\"a\": "), null);
        }
        [TestCase]
        public void Test22111()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\""), null);
        }
        [TestCase]
        public void Test22112()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\" "), null);
        }
        [TestCase]
        public void Test221111()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\","), null);
        }
        public void Test2211111()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\" ,"), null);
        }
        [TestCase]
        public void Test221112()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\"."), null);
        }
        [TestCase]
        public void Test2211121()
        {
            Assert.AreEqual(Json.Parse("{\"a\": \"b\" ."), null);
        }
        [TestCase]
        public void Test221122()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 1}"), null);
        }
        [TestCase]
        public void Test221123()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 1} "), null);
        }
        [TestCase]
        public void Test2211141()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 1},"), null);
        }
        [TestCase]
        public void Test2211142()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 1} ,"), null);
        }
        [TestCase]
        public void Test221115()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 1}."), null);
        }
        [TestCase]
        public void Test2211151()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 1} ."), null);
        }
        [TestCase]
        public void Test2215()
        {
            Assert.AreEqual(Json.Parse("{\"a\""), null);
        }
        [TestCase]
        public void Test2216()
        {
            Assert.AreEqual(Json.Parse("{\"a\": {\"b\": 2, \"c\": 3"), null);
        }

        [TestCase]
        public void Test_1()
        {
            Assert.AreEqual(Json.Parse("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}");
        }

        [TestCase]
        public void Test_2()
        {
            Assert.AreEqual(Json.Parse("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}");
        }


        [TestCase]
        public void Test_3111()
        {
            Assert.AreEqual(Json.Parse("{\"items\": [{\"id\": 70}]}").Text, "{\"items\": [{\"id\": 70}]}");
        }
        [TestCase]
        public void Test_31111()
        {
            Assert.AreEqual(Json.Parse("{\"items\": [{\"id\": 70}, {\"id\": 70}]}").Text, "{\"items\": [{\"id\": 70}, {\"id\": 70}]}");
        }
        [TestCase]
        public void Test_311111()
        {
            Assert.AreEqual(Json.Parse("{\"items\": [\"id\", \"id\"]}").Text, "{\"items\": [\"id\", \"id\"]}");
        }
        [TestCase]
        public void Test_311112()
        {
            Assert.AreEqual(Json.Parse("{\"items\": [0, 1, 2]}").Text, "{\"items\": [0, 1, 2]}");
        }
        [TestCase]
        public void Test_31112()
        {
            Assert.AreEqual(Json.Parse("{\"items\": \"id\", \"id\": 70}").Text, "{\"items\": \"id\", \"id\": 70}");
        }
        [TestCase]
        public void Test_311()
        {
            Assert.AreEqual(Json.Parse("{\"items\": [{\"id\": 70}, {\"id\": 85, \"label\": \"Label 85\"}]}").Text, "{\"items\": [{\"id\": 70}, {\"id\": 85, \"label\": \"Label 85\"}]}");
        }
        [TestCase]
        public void Test_321()
        {
            Assert.AreEqual(Json.Parse("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }
        [TestCase]
        public void Test_331()
        {
            Assert.AreEqual(Json.Parse("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }
        [TestCase]
        public void Test_3()
        {
            Assert.AreEqual(Json.Parse("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }

        [TestCase]
        public void Test_11()
        {
            Assert.AreEqual(Json.Parse("{\"menu\":  {\"header\":  \"menu\",  \"items\":  [{\"id\":  27},  {\"id\":  0,  \"label\":  \"Label 0\"},  null,  {\"id\":  93},  {\"id\":  85},  {\"id\":  54},  null,  {\"id\":  46,  \"label\":  \"Label 46\"}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}");
        }

        [TestCase]
        public void Test_21()
        {
            Assert.AreEqual(Json.Parse("{\"menu\":  {\"header\":  \"menu\",  \"items\":  [{\"id\":  81}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}");
        }


        [TestCase]
        public void Test_31()
        {
            Assert.AreEqual(Json.Parse("{\"menu\":  {\"header\":  \"menu\",  \"items\":  [{\"id\":  70,  \"label\":  \"Label 70\"},  {\"id\":  85,  \"label\":  \"Label 85\"},  {\"id\":  93,  \"label\":  \"Label 93\"},  {\"id\":  2}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}");
        }

        [TestCase]
        public void Test_12()
        {
            Assert.AreEqual(Json.Parse("{\"menu\":{\"header\":\"menu\",\"items\":[{\"id\":27},{\"id\":0,\"label\":\"Label 0\"},null,{\"id\":93},{\"id\":85},{\"id\":54},null,{\"id\":46,\"label\":\"Label 46\"}]}}").Text, "{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 27}, {\"id\": 0, \"label\": \"Label 0\"}, null, {\"id\": 93}, {\"id\": 85}, {\"id\": 54}, null, {\"id\": 46, \"label\": \"Label 46\"}]}}");
        }

        [TestCase]
        public void Test_22()
        {
            IElement element = Json.Parse("{\"menu\":{\"header\":\"menu\",\"items\":[{\"id\":81}]}}");
            Assert.AreEqual("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 81}]}}", element == null ? "" : element.Text);
        }


        [TestCase]
        public void Test_32()
        {
            IElement element = Json.Parse("{\"menu\":{\"header\":\"menu\",\"items\":[{\"id\":70,\"label\":\"Label 70\"},{\"id\":85,\"label\":\"Label 85\"},{\"id\":93,\"label\":\"Label 93\"},{\"id\":2}]}}");
            Assert.AreEqual("{\"menu\": {\"header\": \"menu\", \"items\": [{\"id\": 70, \"label\": \"Label 70\"}, {\"id\": 85, \"label\": \"Label 85\"}, {\"id\": 93, \"label\": \"Label 93\"}, {\"id\": 2}]}}", element == null ? "" : element.Text);
        }
        [TestCase]
        public void Test_0()
        {
            IElement element = Json.Parse("{\"securityToken\":[\"77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il81NjE0ZTE1Ny1lZGYyLTQ0ZTItODZjYy00YTUzNjc1NzI3MmQtNDUwRjc1QjEzOUNGNTVGMTIxMDc2QkZEREYwODY5OTgiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6NTU3MTJmNzUtNjdkYi00N2U1LTk5ZjUtM2FmZWUwMzI1Y2I0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+QjVDMFNWMC9tOUlMeGp1aSs0MUlzWXhibFJmN21qSDVnNlppSW9UVjlyc0FBUUFBZUVLQy9YOHBXeWV6RWN0RTdnQWRZV0kydDNQd3NkcW5YUGg2WUNFbit0THZXekJkNTdZaitYYnpwWS9NcEt2NDIveThXM203V2VKaVRJb3FsTG41cnFEZmxvWWd4NjdNKzIxV1ZnNjV2Q0x5R2tjV1p3NkwzL1gyN1dJM1FaTGtoTzhKZ1pBcXFkamsxOWNnWlJ5MElEUkhmbzlIVDM5ZWtpdTBSNmlHWjQxb2dqK0RQeUExOEFhNHlLeUNFMi9ESHZKMzROV3hVd1JTQUpqMkpVcmdCaEdJc2VsdDRadUVkcmJ6SytpcFlQTEU1RTFld3Y1QXZXYWI1aDl0eXlJRHpoQS9HT3ZBYldtUGVrcUZjZmZueEtjUVowZDZxVy9Gb2N4TGdFUFBXV0NGNHkxRGVKUXU2ZGpTZUtiV0h6YmtlaktCVWwrWUNtMEt5b3JETmtSZGhDQURBQUEwUE1QbTVQZXdVcVJ5bnBqWEl4ZjZvSzhzVHJNQjRtb3VCOE16SUpNVVE2Z3pjUFVlcGZaZTdaOXUrR2YzalB2b3dxS1BGYVR3cUNNdVpiU0tscnRMKzM0RzVWb25UeTlrcE5ycVBvMHVKaFdySzlqWTBDN3VxY2hXV3hmNU5jWmVKcWt5Y0NFZXZ2TGdOTFVsY0R6S3d2VEpZQTRydVhoUjRZMUZZTFhnMlJOMUxOMUp4ZXQ0OHhobVlCZ3FJUmc0a0tZZFUyV1pycnZyYUc1eHNkWW5IVkFJeXVOaVBwZG02WlZNVURoWE90eUQybHBzMGtTWmIrTHkzcTQwUXBsdlRYQmI1a3k1Sm4zeXByRzZnV0FUR3BGZ1FCRjRVNDhRSDRCRVJOaFNCZDJrb0dnbGR3Z2Q0VHhFNEg4MnR5TXJudCsxb2RicHlodTFPZlRaeFJkSCt4L2FZN2ZNY1hFQ1JtdktQa3V2MjBPczUrZEZEOXFqOTZkT3Q5dlRzYzFqZ2lRdEZmVjY1b0JxSlJrblc0R2VIdXBRSWwxQlk0akc3aWdHN29tOFR3SWRCRDV5OGM4L1g1KzREYWRuN2dlcFk3Si96UlZXOHcvSTAxUnFrQ2JTWkFVcVVBWjVleTF3THZCNzBjb3kzMG43cjJMcTV4UXFjMjdqVGhUTTFBdlNKWVNILzVxZ1owR1I1d29kZ3h3SWtZNzhTN2ZPbW1kWUxESVFvNjZkY0VKN1ZUOXlKSTB2dzFQdjBySVl6cWNYQ3g4ZC9DL0ZjblhoL3ZVM29DNjgyTDBDeEQwWGQ3TU5TVEw3NVZyN0hoYzFjT2VYcmFpb05VYTVFaWp6L25mQWUzS3dpdXBPdjZGdzRxcStLWlBhNFo0\",\"c3V5dkFGcE5UbWlDNGtPcURYQzBoTWNGKzdiOXNXd05sU0tQTzhGbVhUV2lNUk02K1NURGZvdytFVG91ZG92OTJTZVU2NGNjN0tIOTNmL0Y3T250aDRycmI1cUpDVVUxNk5IcC9Ka09SeGtRMmVBRHRzdHgwanJSSnZRc09saVZpR291bEs0bStoSVBmT0ZTeTZaQUIwejdiUWt6Y05WZTJHL2dteVVJcHZ1OHE1d3I1N3AzWVVKU2hhVWNoUU5ZaEFGZ2IwK2pZU1dxampvQ1Q5Z1d4THBING55dG5CaWxSVjVtenBoQlZFY2p1eFNXbURuTVRnZ1Y5cTcrRlhmdkh5L0xvam1WTHVFM01LeTRnTVFJVElxOU5RYU9ncE11bmFiYmRHbEQyT1VDd2N6QVFSN0RqcStpazNPaUNVSldCQWVMU1NTTHE4eXFadVA5bjM3T0VreFU4Y1A4OGZ5VCtvL28zU0ozb1JuT0VXZytaTEJkRWEzTWNRajRvb0laTTVkZUNiUTA0cGplaFRQanpXZz09PC9Db29raWU+PC9TZWN1cml0eUNvbnRleHRUb2tlbj4=\"]}");
            Assert.AreEqual("{\"securityToken\": [\"77u/PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0idXRmLTgiPz48U2VjdXJpdHlDb250ZXh0VG9rZW4gcDE6SWQ9Il81NjE0ZTE1Ny1lZGYyLTQ0ZTItODZjYy00YTUzNjc1NzI3MmQtNDUwRjc1QjEzOUNGNTVGMTIxMDc2QkZEREYwODY5OTgiIHhtbG5zOnAxPSJodHRwOi8vZG9jcy5vYXNpcy1vcGVuLm9yZy93c3MvMjAwNC8wMS9vYXNpcy0yMDA0MDEtd3NzLXdzc2VjdXJpdHktdXRpbGl0eS0xLjAueHNkIiB4bWxucz0iaHR0cDovL2RvY3Mub2FzaXMtb3Blbi5vcmcvd3Mtc3gvd3Mtc2VjdXJlY29udmVyc2F0aW9uLzIwMDUxMiI+PElkZW50aWZpZXI+dXJuOnV1aWQ6NTU3MTJmNzUtNjdkYi00N2U1LTk5ZjUtM2FmZWUwMzI1Y2I0PC9JZGVudGlmaWVyPjxDb29raWUgeG1sbnM9Imh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwNi8wNS9zZWN1cml0eSI+QjVDMFNWMC9tOUlMeGp1aSs0MUlzWXhibFJmN21qSDVnNlppSW9UVjlyc0FBUUFBZUVLQy9YOHBXeWV6RWN0RTdnQWRZV0kydDNQd3NkcW5YUGg2WUNFbit0THZXekJkNTdZaitYYnpwWS9NcEt2NDIveThXM203V2VKaVRJb3FsTG41cnFEZmxvWWd4NjdNKzIxV1ZnNjV2Q0x5R2tjV1p3NkwzL1gyN1dJM1FaTGtoTzhKZ1pBcXFkamsxOWNnWlJ5MElEUkhmbzlIVDM5ZWtpdTBSNmlHWjQxb2dqK0RQeUExOEFhNHlLeUNFMi9ESHZKMzROV3hVd1JTQUpqMkpVcmdCaEdJc2VsdDRadUVkcmJ6SytpcFlQTEU1RTFld3Y1QXZXYWI1aDl0eXlJRHpoQS9HT3ZBYldtUGVrcUZjZmZueEtjUVowZDZxVy9Gb2N4TGdFUFBXV0NGNHkxRGVKUXU2ZGpTZUtiV0h6YmtlaktCVWwrWUNtMEt5b3JETmtSZGhDQURBQUEwUE1QbTVQZXdVcVJ5bnBqWEl4ZjZvSzhzVHJNQjRtb3VCOE16SUpNVVE2Z3pjUFVlcGZaZTdaOXUrR2YzalB2b3dxS1BGYVR3cUNNdVpiU0tscnRMKzM0RzVWb25UeTlrcE5ycVBvMHVKaFdySzlqWTBDN3VxY2hXV3hmNU5jWmVKcWt5Y0NFZXZ2TGdOTFVsY0R6S3d2VEpZQTRydVhoUjRZMUZZTFhnMlJOMUxOMUp4ZXQ0OHhobVlCZ3FJUmc0a0tZZFUyV1pycnZyYUc1eHNkWW5IVkFJeXVOaVBwZG02WlZNVURoWE90eUQybHBzMGtTWmIrTHkzcTQwUXBsdlRYQmI1a3k1Sm4zeXByRzZnV0FUR3BGZ1FCRjRVNDhRSDRCRVJOaFNCZDJrb0dnbGR3Z2Q0VHhFNEg4MnR5TXJudCsxb2RicHlodTFPZlRaeFJkSCt4L2FZN2ZNY1hFQ1JtdktQa3V2MjBPczUrZEZEOXFqOTZkT3Q5dlRzYzFqZ2lRdEZmVjY1b0JxSlJrblc0R2VIdXBRSWwxQlk0akc3aWdHN29tOFR3SWRCRDV5OGM4L1g1KzREYWRuN2dlcFk3Si96UlZXOHcvSTAxUnFrQ2JTWkFVcVVBWjVleTF3THZCNzBjb3kzMG43cjJMcTV4UXFjMjdqVGhUTTFBdlNKWVNILzVxZ1owR1I1d29kZ3h3SWtZNzhTN2ZPbW1kWUxESVFvNjZkY0VKN1ZUOXlKSTB2dzFQdjBySVl6cWNYQ3g4ZC9DL0ZjblhoL3ZVM29DNjgyTDBDeEQwWGQ3TU5TVEw3NVZyN0hoYzFjT2VYcmFpb05VYTVFaWp6L25mQWUzS3dpdXBPdjZGdzRxcStLWlBhNFo0\", \"c3V5dkFGcE5UbWlDNGtPcURYQzBoTWNGKzdiOXNXd05sU0tQTzhGbVhUV2lNUk02K1NURGZvdytFVG91ZG92OTJTZVU2NGNjN0tIOTNmL0Y3T250aDRycmI1cUpDVVUxNk5IcC9Ka09SeGtRMmVBRHRzdHgwanJSSnZRc09saVZpR291bEs0bStoSVBmT0ZTeTZaQUIwejdiUWt6Y05WZTJHL2dteVVJcHZ1OHE1d3I1N3AzWVVKU2hhVWNoUU5ZaEFGZ2IwK2pZU1dxampvQ1Q5Z1d4THBING55dG5CaWxSVjVtenBoQlZFY2p1eFNXbURuTVRnZ1Y5cTcrRlhmdkh5L0xvam1WTHVFM01LeTRnTVFJVElxOU5RYU9ncE11bmFiYmRHbEQyT1VDd2N6QVFSN0RqcStpazNPaUNVSldCQWVMU1NTTHE4eXFadVA5bjM3T0VreFU4Y1A4OGZ5VCtvL28zU0ozb1JuT0VXZytaTEJkRWEzTWNRajRvb0laTTVkZUNiUTA0cGplaFRQanpXZz09PC9Db29raWU+PC9TZWN1cml0eUNvbnRleHRUb2tlbj4=\"]}", element == null ? "" : element.Text);
        }    
    }
}