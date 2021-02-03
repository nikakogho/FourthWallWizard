using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideMaster : MonoBehaviour
{
    [SerializeField] Text interactText, guideText, darkWizardText;
    [SerializeField] string usernameTag = "@USERNAME@", displayNameTag = "@DISPLAYNAME@";
    [SerializeField, TextArea(3, 20)] string fullGuideText, fullDarkWizardText;

    public static GuideMaster instance;

    [SerializeField]float characterWriteDelta = 0.1f;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartGuideTextRoutine();
    }

    public void TweakInteractText(bool state)
    {
        interactText.enabled = state;
    }
    
    IEnumerator TextRoutine(Text text, string fullText, bool isDarkWizard = false)
    {
        text.enabled = true;
        foreach(char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(characterWriteDelta);
        }

        yield return new WaitForSeconds(4);

        if(!isDarkWizard) StartDarkWizardTextRoutine();
    }

    public void StartGuideTextRoutine()
    {
        string text = fullGuideText.Replace(usernameTag, GetUsername.username);

        StartCoroutine(TextRoutine(guideText, text));
    }

    void StartDarkWizardTextRoutine()
    {
        string text = fullDarkWizardText.Replace(usernameTag, GetUsername.username);
        text = text.Replace(displayNameTag, GetUsername.DisplayName);
        guideText.enabled = false;
        StartCoroutine(TextRoutine(darkWizardText, text, true));
    }
}
