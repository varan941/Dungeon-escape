using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class Ads_Manager : MonoBehaviour
{
    public void ShowRewardedAd() //функ показывающая рекламу, привязываем к UI кнопке
    {
        Debug.Log("Showing reward. ОПА РЕКЛАМА");
        //cheak if advertisments ready (rewardedVideo)
        //Show(rewardedVideo)

        if (Advertisement.IsReady("rewardedVideo")) //пров. готово ли rewardedVideo
        {
            var options = new ShowOptions // добавл. переменную для просмотра результата 
            {
                resultCallback = HandleShowResults // вызвыаем результат метода HandleShowResults
            };
            Advertisement.Show("rewardedVideo", options);
        }
    }

    // каието делигаты и ивенты, хз
    void HandleShowResults(ShowResult result) // в метод добавляем типо пропустил или просмотрел
    {
        switch (result)
        {
            case ShowResult.Finished: // за это, мы получаем 100г
                Debug.Log("Уважение");
                GameManager.I.Player.AddGems(100); // получаем 100г
                UI_Manger.I.UpdateGemcount(GameManager.I.Player.diamonds); // перепис в ЮА
                UI_Manger.I.OpenShop(GameManager.I.Player.diamonds);
                break;
            case ShowResult.Skipped: // кинуть меня решил
                Debug.Log("Кинуть меня решил? У матери проверь алмазы");
                break;
            case ShowResult.Failed: // кинуть меня решил
                Debug.Log("Кинуть меня решил? У матери проверь алмазы");
                break;
        }
    }

}
