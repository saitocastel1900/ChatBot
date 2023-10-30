namespace VoiceVox
{
    public class VoiceVoxModel
    {
        /// <summary>
        /// APIのレスポンスを受け取る型
        /// </summary>
        [System.Serializable]
        public class ResponseModel
        {
            /// <summary>
            /// アクセント句のリスト
            /// </summary>
            public AccentPhrase[] accent_phrases;
            
            /// <summary>
            /// 全体の話速
            /// </summary>
            public float speedScale;
            
            /// <summary>
            /// 全体の音高
            /// </summary>
            public float pitchScale;
            
            /// <summary>
            /// 全体の抑揚
            /// </summary>
            public float intonationScale;
            
            /// <summary>
            /// 全体の音量
            /// </summary>
            public float volumeScale;
            
            /// <summary>
            /// 音声前の無音時間
            /// </summary>
            public float prePhonemeLength;
            
            /// <summary>
            /// 音声後の無音時間
            /// </summary>
            public float postPhonemeLength;
            
            /// <summary>
            /// 音声データの出力サンプリングレート
            /// </summary>
            public int outputSamplingRate;
            
            /// <summary>
            /// 音声データをステレオ出力するかのフラグ
            /// </summary>
            public bool outputStereo;

            /// <summary>
            /// 読みかな
            /// </summary>
            public string kana;
            
            /// <summary>
            /// アクセント句 
            /// </summary>
            [System.Serializable]
            public class AccentPhrase
            {
                /// <summary>
                /// モーラのリスト
                /// </summary>
                public Mora[] moras;
                
                /// <summary>
                /// アクセント箇所
                /// </summary>
                public int accent;
                
                /// <summary>
                /// モーラ（子音＋母音）ごとの情報
                /// </summary>
                public PauseMora pause_mora;
                
                /// <summary>
                /// 疑問系かのフラグ
                /// </summary>
                public bool is_interrogative;

                
                /// <summary>
                /// モーラ
                /// </summary>
                [System.Serializable]
                public class Mora
                {
                    /// <summary>
                    /// 文字
                    /// </summary>
                    public string text;
                   
                    /// <summary>
                    /// 子音の音素
                    /// </summary>
                    public string consonant;

                    /// <summary>
                    /// 子音の音長
                    /// </summary>
                    public float consonant_length;
                    
                    /// <summary>
                    /// 母音の音素
                    /// </summary>
                    public string vowel;
                    
                    /// <summary>
                    /// 母音の音長
                    /// </summary>
                    public float vowel_length;
                    
                    /// <summary>
                    /// 母音の音長
                    /// </summary>
                    public float pitch;
                }

                /// <summary>
                /// モーラ（子音＋母音）ごとの情報
                /// </summary>
                [System.Serializable]
                public class PauseMora
                {
                    /// <summary>
                    /// 文字
                    /// </summary>
                    public string text;
                    
                    /// <summary>
                    /// 母音の音素
                    /// </summary>
                    public string vowel;
                    
                    /// <summary>
                    /// 母音の音長
                    /// </summary>
                    public float vowel_length;
                    
                    /// <summary>
                    /// 母音の音長
                    /// </summary>
                    public float pitch;
                }
            }
        }
    }
}