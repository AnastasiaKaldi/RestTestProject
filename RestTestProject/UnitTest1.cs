using RestSharp;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RestTestProject
{
    public class UnitTest1
    {
        [Fact]
        //[GET]
        public async void Happy_path_for_fetching_user_id()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 1;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal("{\"data\":{\"id\":1,\"email\":\"george.bluth@reqres.in\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"},\"support\":{\"url\":\"https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral\",\"text\":\"Tired of writing endless social media content? Let Content Caddy generate it for you.\"}}", response.Content);




        }

        [Fact]
        //[GET]
        public async void Unhappy_path_for_fetching_user_id_when_id_is_an_invalid_number()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);




        }



        [Fact]
        // [GET]
        public async void Checks_for_fetch_of_user_id() {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            RestRequest request = new RestRequest("api/users", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act
            var response = await client.ExecuteAsync(request);
            
            //assert
            Assert.Equal("{\"page\":1,\"per_page\":6,\"total\":12,\"total_pages\":2,\"data\":[{\"id\":1,\"email\":\"george.bluth@reqres.in\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"},{\"id\":2,\"email\":\"janet.weaver@reqres.in\",\"first_name\":\"Janet\",\"last_name\":\"Weaver\",\"avatar\":\"https://reqres.in/img/faces/2-image.jpg\"},{\"id\":3,\"email\":\"emma.wong@reqres.in\",\"first_name\":\"Emma\",\"last_name\":\"Wong\",\"avatar\":\"https://reqres.in/img/faces/3-image.jpg\"},{\"id\":4,\"email\":\"eve.holt@reqres.in\",\"first_name\":\"Eve\",\"last_name\":\"Holt\",\"avatar\":\"https://reqres.in/img/faces/4-image.jpg\"},{\"id\":5,\"email\":\"charles.morris@reqres.in\",\"first_name\":\"Charles\",\"last_name\":\"Morris\",\"avatar\":\"https://reqres.in/img/faces/5-image.jpg\"},{\"id\":6,\"email\":\"tracey.ramos@reqres.in\",\"first_name\":\"Tracey\",\"last_name\":\"Ramos\",\"avatar\":\"https://reqres.in/img/faces/6-image.jpg\"}],\"support\":{\"url\":\"https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral\",\"text\":\"Tired of writing endless social media content? Let Content Caddy generate it for you.\"}}", response.Content);


        }

        [Fact]
        // [DELETE]
        public async void Checks_for_deleting_a_user()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Delete).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);




        }

        [Fact (Skip = "this test should not fail normally, but there are issues with the API")]
        // [DELETE]
        public async void Checks_unhappy_path_for_deleting_a_user_when_the_user_id_is_invalid()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Delete).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);




        }


        [Fact]
        //  [PATCH]
        public async void Checks_happy_path_for_updating_a_user()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Patch).AddHeader("x-api-key", "reqres-free-v1");
            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);




        }

        [Fact (Skip = "This test should not fail normally, but there are issues with the API")]
        //  [PATCH] 
        public async void checks_unhappy_path_for_updating_a_user_when_the_user_id_is_invalid()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Patch).AddHeader("x-api-key", "reqres-free-v1");
            
            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);

            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            




        }

        [Fact]
        //  [PATCH]
        public async void Checks_that_the_response_contains_the_changed_parameters()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Patch).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);
            var data = JsonSerializer.Deserialize<JsonNode>(response.Content);
            string actual = $"{data["first name"]}";
            

            //assert
            Assert.Equal("george", actual);




        }

        [Fact]
        // [PUT]
        public async void Happy_path_for_updating_a_user()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);


            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);




        }


        [Fact]
        // [PUT]
        public async void Checks_if_the_updated_data_is_contained_in_the_API_response()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"email\":\"george.bluth@gmail.com\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);
                 var data = JsonSerializer.Deserialize<JsonNode>(response.Content);
           
                        string actual = $"{data[response.Content]}";
           

                        //assert
                        Assert.Contains("\"email\":\"george.bluth@gmail.com\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"", response.Content);








        }


        [Fact (Skip = "This test should not fail normally, but there issues with the API")]
        //  [PUT]
        public async void Checks_unhappy_path_for_updating_a_user_when_there_are_not_enough_parameters_provided()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);


            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);




        }

        [Fact(Skip = "This test should not fail normally, but there issues with the API")]
        // [PUT]
        public async void checks_unhappy_path_for_updating_a_user_when_there_are_not_enough_parameters_provided()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            const string input = "{\"email\":\"george.bluth@gmail.com\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);


            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);




        }


    }




}