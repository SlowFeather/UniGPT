namespace AIHelper.OpenAI
{
    public static class Api
    {
        //https://api.openai.com/v1/completions
        //https://api.openai.com/v1/chat/completions
        public const string Url = "https://api.openai.com/v1/chat/completions";
    }

    [System.Serializable]
    public struct ResponseMessage
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public struct ResponseChoice
    {
        public int index;
        public ResponseMessage message;
    }

    [System.Serializable]
    public struct Response
    {
        public string id;
        public ResponseChoice[] choices;
    }

    [System.Serializable]
    public struct RequestMessage
    {
        public string role;
        public string content;
    }

    //[System.Serializable]
    //public class RequestMessage
    //{
    //    public string role;
    //    public string content;
    //}

    [System.Serializable]
    public struct Request
    {
        public string model;
        public RequestMessage[] messages;
    }
    //[System.Serializable]
    //public class Request
    //{
    //    public string model;
    //    public RequestMessage[] messages;
    //}
}
