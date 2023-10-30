using Cysharp.Threading.Tasks;
using UnityEngine;

namespace VoiceVox
{
    [RequireComponent(typeof(AudioSource))]
    public class VOICEVOX : MonoBehaviour
    {
        /// <summary>
        /// AudioSource
        /// </summary>
        [SerializeField] private AudioSource _audioSource;
        
        /// <summary>
        /// サーバー立ち上げ先のURL
        /// </summary>
        [SerializeField] private string _voicevoxEngineURL = "localhost:50021";
        
        /// <summary>
        /// VoiceVoxとの通信を行う
        /// </summary>
        private VoiceVoxConnections _voiceVox;
        
        private void Awake()
        {
            //AudioSourceがなければ追加
            _audioSource = gameObject.GetComponent<AudioSource>() 
                           ?? gameObject.AddComponent<AudioSource>();

            _voiceVox = new VoiceVoxConnections(_voicevoxEngineURL);
        }

        /// <summary>
        /// 音声データを作る
        /// </summary>
        /// <param name="speakerId">話者</param>
        /// <param name="message">音声にする内容</param>
        /// <returns></returns>
        public async UniTask<AudioClip> CreateVoice(int speakerId, string message)
        {
            return await _voiceVox.CreateVoiceRequestAsync(speakerId, message);
        }
        
        /// <summary>
        /// 音声を流す
        /// </summary>
        /// <param name="audioClip">音声データ</param>
        public void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }
    }
}