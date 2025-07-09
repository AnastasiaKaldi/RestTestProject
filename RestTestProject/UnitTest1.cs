using RestSharp;

namespace RestTestProject
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 1;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response);

            //assert
            Assert.Equal("{\"data\":{\"id\":1,\"email\":\"george.bluth@reqres.in\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"},\"support\":{\"url\":\"https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral\",\"text\":\"Tired of writing endless social media content? Let Content Caddy generate it for you.\"}}", response.Content);




        }
    }
}