using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manger : Singleton<UI_Manger>
{
    public Text playerGemCountText;
    public Image selection_Img;
    public Text gemcountText;
    public Image[] healthBars; // колл. нашего хп 
    [SerializeField] Text textAttemptsAt;

    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = ""+ gemCount+ "G"; //пишет сколько у нас алмазов
    }   

    public void UpdateSelection(int yPos) // метод меняющий положение Selection картинки
    {
        selection_Img.rectTransform.anchoredPosition = new Vector2(selection_Img.rectTransform.anchoredPosition.x, yPos);
    }

    public void UpdateGemcount(int count)
    {
        gemcountText.text = "" + count;
    }

    public void UpdateLives(int liveRemaining)
    {
        if (healthBars.Length < liveRemaining) // для теста когда много жизней
            return;
        
        for (int i = 0; i <= liveRemaining; i++)
        {
            //do nothing till we hit the max
            if (i==liveRemaining)
            {
                //hide this one
                healthBars[i].enabled = false;
            }
        }

    }

    public void UpdateAttemptsAttack(string str)
    {
        textAttemptsAt.text = str;
    }


}
