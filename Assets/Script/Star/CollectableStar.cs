using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CollectableStar : MonoBehaviour
{
    public static CollectableStar Instance;

    private int stars;
    [SerializeField] private TMP_Text starsDisplay;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadStars();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateStarsDisplay();
    }

    public void ChangeStars(int amount)
    {
        stars += amount;
        SaveStars();
        UpdateStarsDisplay();
    }

    private void UpdateStarsDisplay()
    {
        if (starsDisplay == null)
        {
            // Sahne yeniden y�klendiyse starsDisplay ��esini tekrar bul
            starsDisplay = GameObject.Find("StarsDisplay").GetComponent<TMP_Text>();
        }
        starsDisplay.text = stars.ToString();
    }

    private void SaveStars()
    {
        PlayerPrefs.SetInt("Stars", stars);
        PlayerPrefs.Save();
    }

    private void LoadStars()
    {
        if (PlayerPrefs.HasKey("Stars"))
        {
            stars = PlayerPrefs.GetInt("Stars");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sahne y�klendi�inde y�ld�z say�s�n� s�f�rla
        ResetStars();
    }

    public void ResetStars()
    {
        stars = 0;
        SaveStars();
        UpdateStarsDisplay();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
