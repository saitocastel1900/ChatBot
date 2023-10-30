using UnityEngine;

namespace ChatBot.Widget
{
    public class MessageControlWidget : MonoBehaviour
    {
        /// <summary>
        /// MessagePrefab
        /// </summary>
        [SerializeField] private GameObject _messagePrefab;

        /// <summary>
        /// MessageThread
        /// </summary>
        [SerializeField] private GameObject _messageThread;

        /// <summary>
        /// Messageオブジェクトを作成する
        /// </summary>
        /// <param name="message">伝えたい言葉</param>
        /// <param name="role">話している人物</param>
        public void CreateMessage(string message, ChatRole role)
        {
            //Messageオブジェクトを生成して、表示する文字と位置を設定する
            Instantiate(_messagePrefab, _messageThread.transform, false)
                .gameObject.GetComponent<MessageWidget>().Initialize(message, role);
        }
    }
}