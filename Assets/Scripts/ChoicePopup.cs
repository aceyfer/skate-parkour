using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ChoicePopup : MonoBehaviour
{
    public static ChoicePopup Instance { get; private set; }

    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Button yesButton;
    [SerializeField] private Button noButton;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
        
        // Buttons are no longer used for the automatic flow
        if (yesButton != null) yesButton.gameObject.SetActive(false);
        if (noButton != null) noButton.gameObject.SetActive(false);
    }

    public void Show(string prompt, float duration = 4f)
    {
        StopAllCoroutines();
        promptText.text = prompt;
        panel.SetActive(true);
        StartCoroutine(HideAfterDelay(duration));
    }

    private IEnumerator HideAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(false);
    }
}
