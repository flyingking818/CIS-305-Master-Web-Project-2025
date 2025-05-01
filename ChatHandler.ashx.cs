using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace CIS_305_Master_Web_Project
{
    /// <summary>
    /// Handles incoming chat messages and routes them to OpenAI's API.
    /// </summary>
    public class ChatHandler : IHttpHandler
    {
        // Main method called on each HTTP request to this handler
        public void ProcessRequest(HttpContext context)
        {
            // Set the content type of the response
            context.Response.ContentType = "text/plain";

            // Read the raw JSON request body
            string jsonString;
            using (var reader = new StreamReader(context.Request.InputStream))
            {
                jsonString = reader.ReadToEnd();
            }

            // Deserialize the JSON input into a ChatRequest object
            var data = new JavaScriptSerializer().Deserialize<ChatRequest>(jsonString);
            string userInput = data.userInput;

            // Call OpenAI's API with the user input and get the reply
            string reply = GetGPTResponse(userInput);

            // Write the assistant's reply back to the response
            context.Response.Write(reply);
        }

        // Indicates whether this handler can be reused across requests
        public bool IsReusable => false;

        // Sends the user's message to OpenAI's chat completions API and returns the reply
        private string GetGPTResponse(string userInput)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("https://api.openai.com/v1/chat/completions");
                request.Method = "POST";
                request.ContentType = "application/json";

                string apiKey = System.Configuration.ConfigurationManager.AppSettings["OpenAI_API_Key"];
                request.Headers["Authorization"] = $"Bearer {apiKey}";

                var payload = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] {
                new { role = "system", content = "You are Ask Saint, an assistant trained on Flagler College catalog." },
                new { role = "user", content = userInput }
            }
                };

                string json = new JavaScriptSerializer().Serialize(payload);
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    streamWriter.Write(json);
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    var obj = new JavaScriptSerializer().Deserialize<dynamic>(result);
                    return obj["choices"][0]["message"]["content"];
                }
            }
            catch (WebException ex)
            {
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    int statusCode = (int)errorResponse.StatusCode;
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        string errDetails = reader.ReadToEnd();

                        // 🔍 Output full API error to Visual Studio Output window
                        System.Diagnostics.Debug.WriteLine("OpenAI API error response:");
                        System.Diagnostics.Debug.WriteLine(errDetails);

                        return $"API error {statusCode}: {errDetails}";
                    }                                     

                }

                return $"Unexpected network error: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"General error: {ex.Message}";
            }
        }



        // Represents the structure of the incoming user input JSON
        public class ChatRequest
        {
            public string userInput { get; set; }
        }
    }
}
