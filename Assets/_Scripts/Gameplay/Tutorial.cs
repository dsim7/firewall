using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public BoolVariableSO seenTutorial;
    public BoolVariableSO inTutorial;
    public TutorialScript script;
    [Space]
    public Image tutorialPanel;
    public Text tutorialText;
    public Image bufferPointer;
    public Image firewallPointer;
    public Image inactiveBufferPointer;
    bool tutorialWaiting;
    [Space]
    public TunnelSpawner spawner;
    public SaveLoad saveLoad;

    float tutorialDelayTime = 1f;
    float tutorialDelayTimer = 0f;
    bool canCloseTutorial;

    void Update()
    {
        if (tutorialWaiting && canCloseTutorial &&
            (Input.GetMouseButtonDown(0) ||
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)))
        {
            TutorialClose();
        }

        if (!canCloseTutorial)
        {
            HelperMethods.UpdateTimerUnscaled(ref tutorialDelayTimer, tutorialDelayTime, AllowTutorialClose);
        }
    }

    public void DoTutorialIfHavent()
    {
        inTutorial.Value = false;
        if (!seenTutorial.Value)
        {
            seenTutorial.Value = true;
            saveLoad.Save();

            spawner.SpawnTutorial();
            StartCoroutine(TutorialCoroutine());
        }
    }

    IEnumerator TutorialCoroutine()
    {
        inTutorial.Value = true;
        yield return new WaitForSeconds(1.2f);
        TutorialOpen(script.texts[0]);
        yield return new WaitUntil(() => tutorialWaiting == false);
        yield return new WaitForSeconds(3f);
        TutorialOpen(script.texts[1]);
        bufferPointer.gameObject.SetActive(true);
        yield return new WaitUntil(() => tutorialWaiting == false);
        yield return new WaitForSeconds(3f);
        TutorialOpen(script.texts[2]);
        firewallPointer.gameObject.SetActive(true);
        yield return new WaitUntil(() => tutorialWaiting == false);
        yield return new WaitForSeconds(3f);
        TutorialOpen(script.texts[3]);
        inactiveBufferPointer.gameObject.SetActive(true);
        yield return new WaitUntil(() => tutorialWaiting == false);
        yield return new WaitForSeconds(2f);
        TutorialOpen(script.texts[4]);
        firewallPointer.gameObject.SetActive(true);
        inTutorial.Value = false;
    }

    void TutorialOpen(string text)
    {
        Time.timeScale = 0f;
        tutorialPanel.gameObject.SetActive(true);
        tutorialText.text = text;
        tutorialWaiting = true;
        canCloseTutorial = false;
    }

    void TutorialClose()
    {
        Time.timeScale = 1;
        tutorialPanel.gameObject.SetActive(false);
        bufferPointer.gameObject.SetActive(false);
        firewallPointer.gameObject.SetActive(false);
        inactiveBufferPointer.gameObject.SetActive(false);
        tutorialWaiting = false;
    }

    void AllowTutorialClose()
    {
        canCloseTutorial = true;
    }
}
