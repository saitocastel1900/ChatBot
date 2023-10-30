using System;
using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace ChatGPT
{
    /// <summary>
    /// ChatGPTとの通信を行う
    /// </summary>
    public class ChatGPTConnection
    {
        /// <summary>
        /// APIキー
        /// </summary>
        private string _apiKey;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="apikey">APIキー</param>
        public ChatGPTConnection(string apikey)
        {
            _apiKey = apikey;
        }

        /// <summary>
        /// メッセージの内容を補完する
        /// </summary>
        /// <param name="messages">伝える内容</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async UniTask<ChatGPTModel.ResponseModel> CreateCompletionRequestAsync(
            ChatGPTModel.Message[] messages)
        {
            //呼び出しURL
            var apiUrl = "https://api.openai.com/v1/chat/completions";

            //リクエストボディ
            var jsonBody = JsonConvert.SerializeObject(new ChatGPTModel.RequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = messages
            });

            //リクエストヘッダー
            var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + _apiKey},
                {"Content-type", "application/json"},
                {"X-Slack-No-Retry", "1"}
            };

            //リクエストを作成
            using var request = new UnityWebRequest(apiUrl, "POST")
            {
                //リクエストボディを設定
                uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonBody)),
                downloadHandler = new DownloadHandlerBuffer()
            };

            //リクエストヘッダーを設定
            foreach (var header in headers)
            {
                request.SetRequestHeader(header.Key, header.Value);
            }

            //リクエストを送信
            await request.SendWebRequest();

            //通信が上手くいったら、レスポンスを受け取って、オブジェクトに変換したものを返す
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
                throw new Exception();
            }
            else
            {
                return JsonConvert.DeserializeObject<ChatGPTModel.ResponseModel>(request.downloadHandler.text);
            }
        }
    }
}