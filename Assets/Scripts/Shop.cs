using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentSelectedItem; // выбран. предм.
    public int currentItemCost; //его стоимость

    private Player _player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag=="Player")
        {
            _player = other.GetComponent<Player>();
            if (_player!=null)
            {
                UI_Manger.I.OpenShop(_player.diamonds); //принимает знач. diamonds игрокаи отправ. в  UI_Manger
            }
            shopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            shopPanel.SetActive(false);
        }

    }

    public void Select_Item(int item)
    {
        //0 - sword
        //1 - boots
        //3 - key
        Debug.Log("Selected Item "+ item);

        switch (item)
        {
            case 0:
                UI_Manger.I.UpdateSelection(85);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                UI_Manger.I.UpdateSelection(-25);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                UI_Manger.I.UpdateSelection(-135);
                currentSelectedItem = 2;
                currentItemCost = 1000;
                break;
        }
    }

    public void BuyItem()
    {
        if (_player.diamonds>=currentItemCost)
        {
            _player.diamonds -= currentItemCost;

            if (currentSelectedItem==2)// если чел купил ключ, GameManager получает эти данные
            {
                GameManager.I.HasKeyToCastle = true;
            }
        }

        else
        {
            Debug.Log("You don`t have enough gems. Loser");
        }
    }



}
