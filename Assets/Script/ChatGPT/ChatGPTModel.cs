namespace ChatGPT
{
    public class ChatGPTModel
    {
        /// <summary>
        /// APIのレスポンスを受け取る型
        /// </summary>
        [System.Serializable]
        public class ResponseModel
        {
            /// <summary>
            /// リクエストの一意の識別子
            /// </summary>
            public string id;

            /// <summary>
            /// レスポンスの種類を表す文字列（chat.completion）
            /// </summary>
            public string @object;

            /// <summary>
            /// レスポンスが作成されたタイムスタンプ
            /// </summary>
            public int created;

            /// <summary>
            /// GPTモデルの種類
            /// </summary>
            public string model;

            /// <summary>
            /// 応答に関する情報のリスト
            /// </summary>
            public Choice[] choices;

            /// <summary>
            /// リクエストに対する使用状況
            /// </summary>
            public Usage usage;

            /// <summary>
            /// 応答に関する情報（通常は１つの要素しか生成されない）
            /// </summary>
            [System.Serializable]
            public class Choice
            {
                /// <summary>
                /// レスポンスのインデックス（通常は０）
                /// </summary>
                public int index;

                /// <summary>
                /// 対話のコンテキスト
                /// </summary>
                public Message message;

                /// <summary>
                /// モデルが終了した理由
                /// </summary>
                public string finish_reason;
            }

            /// <summary>
            /// リクエストに対する使用状況
            /// </summary>
            [System.Serializable]
            public class Usage
            {
                /// <summary>
                /// 入力プロンプトのトークン数
                /// </summary>
                public int prompt_tokens;

                /// <summary>
                /// 出力応答のトークン数
                /// </summary>
                public int completion_tokens;

                /// <summary>
                /// 総トークン数（prompt_tokensとcompletion_tokensの合計）
                /// </summary>
                public int total_tokens;
            }
        }

        /// <summary>
        /// 対話のコンテキスト
        /// </summary>
        [System.Serializable]
        public class Message
        {
            /// <summary>
            /// 発信者
            /// </summary>
            public string role;

            /// <summary>
            /// メッセージのテキストコンテンツ
            /// </summary>
            public string content;
        }

        /// <summary>
        /// APIにリクエストを出す型
        /// </summary>
        [System.Serializable]
        public class RequestModel
        {
            /// <summary>
            /// GPTモデルの種類
            /// </summary>
            public string model;

            /// <summary>
            /// 対話のコンテキストのリスト
            /// </summary>
            public Message[] messages;
        }
    }
}