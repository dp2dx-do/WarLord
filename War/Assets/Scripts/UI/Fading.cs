using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public static Fading Instance;
    public Image fadeImg;
    public Vector3 pos, BeforePos;
    private PlayerScript player;
    public int TargetScene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        fadeImg.gameObject.SetActive(false);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void Fade(int scene)
    {
        TargetScene = scene;
        StartCoroutine(Fades());
    }
    public IEnumerator Fades()
    {
        if (player != null)
        {
            player.gameObject.SetActive(false);
        }
        float time = 0;
        fadeImg.gameObject.SetActive (true);
        while (time < 0.25f)
        {
            time += Time.deltaTime;
            fadeImg.color = Color.Lerp(Color.clear, Color.black, time * 4f);
            yield return null;
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex != 1)
        {
            StartCoroutine(FadesR());
            if (TutorialManager.Instance!=null)
            {
                TutorialManager.Instance.Page++;
            }
        }
        else
        {
            fadeImg.color = Color.clear;
        }
    }
    public IEnumerator FadesR()
    {
        if(TargetScene != 10)
        {
            Camera.main.transform.position = pos + new Vector3(0, 8, -5.5f);
            if (BasicCanvas.Instance == null)
            {
                Instantiate(Resources.Load("UI/Canvas/BasicCanvas"));
            }
            if(player == null)
            {
                if (GameData.Instance.playerData.charClass == CharClass.Knight)
                    player = Instantiate(Resources.Load<PlayerScript>("Player"));
                else player = Instantiate(Resources.Load<PlayerScript>("PlayerMage"));
            }
            while (player == null)
            {
                yield return null;
            }
            player.transform.position = pos;
            float time = 0;
            while (time < 0.2f)
            {
                time += Time.deltaTime;
                fadeImg.color = Color.Lerp(Color.black, Color.clear, time * 5f);
                yield return null;
            }
            fadeImg.gameObject.SetActive(false);
        
            player.gameObject.SetActive(true);
            if (SceneData.instance != null && SceneData.instance.BgmClip!=null)
            {
                SoundManager.instance.Bgm.clip = SceneData.instance.BgmClip;
                SoundManager.instance.Bgm.Play();
            }
            if (ChaosDungeon.Instance != null)
            {
                ChaosDungeon.Instance.EnterPlayer(player);
            }
            if (BossMap.instance != null)
            {
                BossMap.instance.EnterPlayer(player);
            }
            if (!player.Live)
            {
                player.Revive();
            }
            player.SaveTheData();
        }
        else
        {
            float time = 0;
            while (time < 0.2f)
            {
                time += Time.deltaTime;
                fadeImg.color = Color.Lerp(Color.black, Color.clear, time * 5f);
                yield return null;
            }
            fadeImg.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
