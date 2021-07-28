using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : Singleton<Dialog>
{
    [SerializeField] Animator animDialog;
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] string[] sentences;    
    [SerializeField] float typingSpeed = 0.02f;
    [SerializeField] Button nextDialogButton;

    public void PlayNeedDialog(int index)
    {
        if (index > sentences.Length - 1)
            return;

        StopAllCoroutines();
        //animDialog.SetTrigger("Change");

        textDisplay.text = "";
        StartCoroutine(Type(index));
    }

    IEnumerator Type(int index)
    {
        foreach (var letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }        
        animDialog.SetTrigger("Idle");
        yield return new WaitForSeconds(3f);
        animDialog.SetTrigger("Change");
        yield return new WaitForSeconds(0.5f);
        textDisplay.text = "";
        textDisplay.alpha = 1;
    }


}
