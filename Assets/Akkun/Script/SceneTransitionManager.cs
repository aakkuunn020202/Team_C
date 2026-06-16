using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance { get; private set; }

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1.0f;

    private bool isFading = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            transform.SetParent(null);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 起動時はフェード画像を表示しつつ、クリックはすり抜けるようにする
        if (fadeImage != null)
        {
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0f);
            fadeImage.raycastTarget = false; // クリックをすり抜けさせる
        }
    }

    public void ChangeScene(string sceneName)
    {
        if (isFading) return;
        StartCoroutine(TransitionRoutine(sceneName));
    }

    private IEnumerator TransitionRoutine(string sceneName)
    {
        isFading = true;
        fadeImage.raycastTarget = true; // フェード開始時にクリックをブロック（連打防止）

        // 1. フェードアウト
        yield return StartCoroutine(Fade(1.0f));

        // 2. シーン読み込み
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // 3. フェードイン
        yield return StartCoroutine(Fade(0.0f));

        fadeImage.raycastTarget = false; // フェード完了後に再びクリックをすり抜けさせる
        isFading = false;
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlpha);
            yield return null;
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, targetAlpha);
    }
}