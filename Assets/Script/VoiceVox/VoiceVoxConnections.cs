using System;
using System.Text;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace VoiceVox
{
    /// <summary>
    /// VoiceVoxとの通信を行う
    /// </summary>
    public class VoiceVoxConnections
    {
        /// <summary>
        /// サーバー立ち上げ先のURL
        /// </summary>
        private string _voicevoxEngineURL;
        
        /// <summary>
        /// 音声合成用のクエリバイトデータ
        /// </summary>
        private byte[] _audioQueryBytes;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="voicevoxEngineURL"></param>
        public VoiceVoxConnections(string voicevoxEngineURL = "localhost:50021")
        {
            _voicevoxEngineURL = voicevoxEngineURL;
            _audioQueryBytes = null;
        }
        
        /// <summary>
        /// 音声を作成する
        /// </summary>
        /// <param name="speakerId">話者</param>
        /// <param name="message">音声にする内容</param>
        /// <returns></returns>
        public async UniTask<AudioClip> CreateVoiceRequestAsync(int speakerId, string message)
        {
            _audioQueryBytes  = await GetAudioQueryRequestAsync(speakerId, message);
            
            return await PostSynthesis(speakerId, _audioQueryBytes);
        }
        
        /// <summary>
        /// 音声合成用のクエリを作成
        /// </summary>
        /// <param name="speakerId">話者</param>
        /// <param name="text">音声にする内容</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async UniTask<byte[]> GetAudioQueryRequestAsync(int speakerId, string text)
        {
            //呼び出しURL
            string apiUrl = $"{_voicevoxEngineURL}/audio_query?speaker={speakerId}&text={UnityWebRequest.EscapeURL(text,Encoding.UTF8)}";
            
            //リクエストを作成
            using UnityWebRequest request = new UnityWebRequest(apiUrl, "POST")
            {
                downloadHandler = new DownloadHandlerBuffer()
            };

            //リクエストを送信
            await request.SendWebRequest();

            //通信が上手くいったら、レスポンスを受け取って、音声合成用のクエリを返す
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
                throw new Exception();
            }
            else
            {
                Debug.Log("AudioQuery:" + request.downloadHandler.text);
                return (request.downloadHandler.data);
            }
        }

        /// <summary>
        /// 音声合成
        /// </summary>
        /// <param name="speakerId">話者</param>
        /// <param name="audioQuery">音声合成用のクエリ</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async UniTask<AudioClip> PostSynthesis(int speakerId, byte[] audioQuery)
        {
            //呼び出しURL
            string apiUrl = $"{_voicevoxEngineURL}/synthesis?speaker={speakerId}";

            //リクエストを作成
            using var request = UnityWebRequestMultimedia.GetAudioClip(apiUrl, AudioType.WAV);
            //リクエストメソッドを設定
            request.method = "POST";
            //リクエストボディを設定
            request.uploadHandler = new UploadHandlerRaw(audioQuery);
            //リクエストヘッダーを設定
            request.SetRequestHeader("Content-Type", "application/json");
            ((DownloadHandlerAudioClip) request.downloadHandler).streamAudio = true;

            //リクエストを送信
            await request.SendWebRequest();

            //通信が上手くいったら、レスポンスを受け取って、AudioClipに変換した音声を返す
            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError ||
                request.result == UnityWebRequest.Result.DataProcessingError)
            {
                Debug.LogError(request.error);
                throw new Exception();
            }
            else
            {
                return DownloadHandlerAudioClip.GetContent(request);
            }
        }
    }
}