using UnityEngine;
using UnityEngine.UI;

namespace ChatBot.Widget
{
    public class MessageWidget : MonoBehaviour
    {
        /// <summary>
        /// MessageオブジェクトのHorizontalLayoutGroup
        /// </summary>
        [SerializeField] private HorizontalLayoutGroup _horizontalLayoutGroup;
        
        /// <summary>
        /// MessageオブジェクトのText
        /// </summary>
        [SerializeField] private Text _chatMessage;

        /// <summary>
        /// 初期化処理
        /// </summary>
        /// <param name="message">伝える言葉</param>
        /// <param name="role">話している人物</param>
        public void Initialize(string message,ChatRole role)
        {
            //伝える言葉を設定する
            _chatMessage.text = message;
         
            //話している人物に応じて、表示位置を変更する
            if (role == ChatRole.MINE)
                _horizontalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            
            else if (role == ChatRole.AI)
                _horizontalLayoutGroup.childAlignment = TextAnchor.UpperRight;
        }
    }
}