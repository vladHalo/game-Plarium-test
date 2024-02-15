using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] private Button[] _buttons;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource[] _soundEffects;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        _buttons[0].onClick.AddListener(() => SetVolume(Str.Music, _musicSource));
        _buttons[1].onClick.AddListener(() => SetVolume(Str.Sound, _soundEffects));

        if (PlayerPrefs.HasKey(Str.Music))
        {
            _musicSource.volume = PlayerPrefs.GetInt(Str.Music);
        }

        if (PlayerPrefs.HasKey(Str.Sound))
        {
            var value = PlayerPrefs.GetInt(Str.Sound);
            _soundEffects.ForEach(x => x.volume = value);
        }
    }

    public void PlaySoundEffect(SoundType soundType)
    {
        _soundEffects[(int)soundType].Play();
    }

    private void SetVolume(string musicSound, params AudioSource[] manager)
    {
        int indexVolume = 1;
        if (PlayerPrefs.HasKey(musicSound))
            indexVolume = PlayerPrefs.GetInt(musicSound);
        indexVolume = (indexVolume == 0) ? 1 : 0;
        manager.ForEach(x => x.volume = indexVolume);
        PlayerPrefs.SetInt(musicSound, indexVolume);
    }
}