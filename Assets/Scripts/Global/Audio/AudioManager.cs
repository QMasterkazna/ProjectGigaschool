
using System.Collections.Generic;
using SceneManagment;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Global.Audio
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioGlobalSource;
        [SerializeField] private AudioSource _audioSource;
        public const string MetaPath = "Audio/Meta";
        public const string GamePath = "Audio/Game";
        public const string GlobalPath = "Audio/Global";

        private Dictionary<string, AudioClip> _sceneClips;
        private Dictionary<string, AudioClip> _globalClips;
        public void LoadOnce()
        {
            _globalClips = LoadAudioClips(GlobalPath);
        }
        
        public void Load(string scene)
        {
            if (SceneManager.GetActiveScene().name == scene) return;
            UnloadAssets();
            _sceneClips = scene switch
            {
                Scenes.LevelScene => LoadAudioClips(GamePath),
                Scenes.MetaScene => LoadAudioClips(MetaPath),
                _ => _sceneClips
            };
        }

        public void PlayClip(string clipName, bool isGlobal = false)
        {
            AudioClip clip;
            if (isGlobal)
            {
                clip = _globalClips[clipName];
                _audioGlobalSource.Stop();
                _audioGlobalSource.clip = clip;
                _audioGlobalSource.Play();
            }
            else
            {
                clip = _sceneClips[clipName];
                _audioSource.Stop();
                _audioSource.clip = clip;
                _audioSource.Play();
            }
        }
        private void UnloadAssets()
        {
            if (_sceneClips == null) return;
            foreach (var sceneClip in _sceneClips)
            {
                Resources.UnloadAsset(sceneClip.Value);
            }
        }

        private Dictionary<string, AudioClip> LoadAudioClips(string path)
        {
            var clips = Resources.LoadAll<AudioClip>(path);
            var clipsDictionary = new Dictionary<string, AudioClip>();
            foreach (var audioClip in clips)
            {
                clipsDictionary.Add(audioClip.name, audioClip);
            }
            return clipsDictionary;
        }
    }
}
