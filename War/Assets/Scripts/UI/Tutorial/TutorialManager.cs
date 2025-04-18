using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    [SerializeField] GameObject[] Tutorials; 
    int _page;
    int beforePage = -1;
    public int Page {  get { return _page; }
        set
        {
            _page = value;
            if(_page < Tutorials.Length)
            {
                Tutorials[beforePage].SetActive(false);
                Tutorials[_page].SetActive(true);
                beforePage = _page;
            }
            else
            {
                FindFirstObjectByType<PlayerScript>().isTutorialOpen = false;
                FindFirstObjectByType<PlayerScript>()?.SaveTheData();
                Instance = null;
                Destroy(gameObject);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        beforePage = 0;
        foreach (GameObject tut in Tutorials)
        {
            tut.SetActive(false);
        }
        Page = 0;
    }
}
