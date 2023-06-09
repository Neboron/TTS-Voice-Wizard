using OSCVRCWiz.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace OSCVRCWiz.NikoraAssistant
{
    public class OpenAI_Main
    {
        public struct conversation
        {
            public String request;
            public String response;

            public conversation(String newRequest, String newResponse)
            {
                request = newRequest;
                response = newResponse;
            }
        }

        private static List<conversation> lastConversation = new List<conversation>();

        // initialize the API
        public static void Setup()
        {
            OpenAI_API.APIAuthentication.Default = new OpenAI_API.APIAuthentication("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");

            var api = new OpenAI_API.OpenAIAPI("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");
        }

        public async static Task<String> NikoraRequest(String request)
        {
            var api = new OpenAI_API.OpenAIAPI("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");
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("You are VRChat player, not AI, your name is Nikora, you like watch anime, and read manga" +
                "you like electronics, you know a lot about VR");
            chat.AppendUserInput("Nicora");
            chat.AppendExampleChatbotOutput("Hi master!");

            for (int i = 0; i < lastConversation.Count; ++i)
            {
                chat.AppendUserInput(lastConversation[i].request);
                chat.AppendExampleChatbotOutput(lastConversation[i].response);
            }

            chat.AppendUserInput(request);
            string response = await chat.GetResponseFromChatbotAsync();

            while (lastConversation.Count >= 4)
            {
                lastConversation.RemoveAt(0);
            }
            lastConversation.Add(new conversation(request, response));
            //OutputText.outputLog("[Last conversation: " + lastConversation[0].request + "]", Color.YellowGreen);

            System.Diagnostics.Debug.WriteLine(response);
            return response;
        }
    }
}
