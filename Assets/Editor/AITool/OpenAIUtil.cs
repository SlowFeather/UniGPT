using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

namespace AIHelper
{

    static class OpenAIUtil
    {
        static string CreateChatRequestBody(string prompt)
        {
            var msg = new OpenAI.RequestMessage();
            msg.role = "user";
            msg.content = prompt;

            var req = new OpenAI.Request();
            req.model = "gpt-3.5-turbo";
            req.messages = new[] { msg };

            return JsonUtility.ToJson(req,true);
        }




        public static string InvokeChat(string prompt)
        {
            // POST
            using var postJob = new UnityWebRequest(OpenAI.Api.Url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(CreateChatRequestBody(prompt));
            postJob.SetRequestHeader("Authorization", "Bearer " + AIHelperSettings.instance.apiKey);
            postJob.SetRequestHeader("Content-Type", "application/json");
            postJob.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            postJob.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            var req = postJob.SendWebRequest();

            // Progress bar (Totally fake! Don't try this at home.)
            for (var progress = 0.0f; !req.isDone; progress += 0.01f)
            {
                EditorUtility.DisplayProgressBar
                  ("AI Shader Importer", "Generating...", progress);
                System.Threading.Thread.Sleep(20);
                progress += 0.01f;
            }
            EditorUtility.ClearProgressBar();

            // Response extraction
            var json = postJob.downloadHandler.text;
            //Debug.Log (json);
            var data = JsonUtility.FromJson<OpenAI.Response>(json);
            return data.choices[0].message.content;
        }
    }

} // namespace AIShader
