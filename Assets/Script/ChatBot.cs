using ChatBot.Widget;
using ChatGPT;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VoiceVox;

namespace ChatBot
{
    public class ChatBot : MonoBehaviour
    {
        /// <summary>
        /// MessageControlWidget
        /// </summary>
        [SerializeField] private MessageControlWidget _messageControlWidget;
        
        /// <summary>
        /// VOICEVOX
        /// </summary>
        [SerializeField] private VOICEVOX _voicevox;
        
        /// <summary>
        /// CHATGPT
        /// </summary>
        private CHATGPT _chatGpt;

        /// <summary>
        /// チャットのテキストフィールド
        /// </summary>
       [SerializeField] private InputField _chatTextField;
        
        /// <summary>
        /// メッセージの送信ボタン
        /// </summary>
        [SerializeField] private Button _executeButton;

        private async void Start()
        {
            _chatGpt = new CHATGPT();

            await Bind();
        }

        /// <summary>
        /// Bind処理
        /// </summary>
        private async UniTask Bind()
        {
            _executeButton.OnClickAsObservable().Subscribe(async _ =>
            {
                //伝えたい内容を取得
                var message = _chatTextField.text;
                //伝えたい内容を基にMessageオブジェクトを作成
                _messageControlWidget.CreateMessage(message, ChatRole.MINE);
                
                //伝えたい内容から返信を作る
                var reply = await GetGeneratedReply(message);
                //返答を基に話す
                await Speak(reply);
                //返答を基にMessageオブジェクトを作成
                _messageControlWidget.CreateMessage(reply, ChatRole.AI);
            }).AddTo(this.gameObject);
        }

        /// <summary>
        /// 返信を受け取る
        /// </summary>
        /// <param name="message">伝えたい内容</param>
        /// <returns></returns>
        private async UniTask<string> GetGeneratedReply(string message)
        {
            //受け取った文字から適切な続きを生成する（補完する）
            var result = await _chatGpt.CreateCompletion(message);
            //返答を返す
            return result.choices[0].message.content;
        }

        /// <summary>
        /// 話す
        /// </summary>
        /// <param name="message">伝えたい内容</param>
        private async UniTask Speak(string message)
        {
            //伝えたい内容から音声を生成する
            var audioClip = await _voicevox.CreateVoice(8, message);
            //音声を再生する
            _voicevox.Play(audioClip);
        }
    }
}