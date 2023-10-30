using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace ChatGPT
{
    public class CHATGPT
    {
        /// <summary>
        /// APIキー
        /// </summary>
        private readonly string _apiKey = "";

        /// <summary>
        /// Messageのリスト
        /// </summary>
        private readonly List<ChatGPTModel.Message> _messageList;

        /// <summary>
        /// ChatGPTとの通信を行う
        /// </summary>
        private readonly ChatGPTConnection _chatGPTConnection;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">対話の文脈を制御する情報</param>
        public CHATGPT(string content = "ツンデレ口調で反して")
        {
            _chatGPTConnection = new ChatGPTConnection(_apiKey);

            //対話の文脈を制御する情報を設定
            _messageList = new List<ChatGPTModel.Message>
                {new ChatGPTModel.Message() {role = "system", content = content}};
        }

        /// <summary>
        /// メッセージの内容を補完する
        /// </summary>
        /// <param name="content">伝える内容</param>
        /// <returns></returns>
        public async UniTask<ChatGPTModel.ResponseModel> CreateCompletion(string content)
        {
            //伝える内容を追加
            _messageList.Add(new ChatGPTModel.Message {role = "user", content = content});
            return await _chatGPTConnection.CreateCompletionRequestAsync(_messageList.ToArray());
        }
    }
}