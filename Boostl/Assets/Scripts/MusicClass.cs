using UnityEngine;

public class MusicClass : MonoBehaviour
{
    [SerializeField]
    private AudioSource audio;
    private GameObject[] music;

    void Start()
    {
        music = GameObject.FindGameObjectsWithTag("Music");

        if (music.Length > 1)
            Destroy(music[1]);
        else
            return;
    }

    void Awake()
    {
        audio.Play();
        DontDestroyOnLoad(transform.gameObject);
    }

    private void Update()
    {
        audio.volume = OptionsMenu.volumeLevel;
    }
}