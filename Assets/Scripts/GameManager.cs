using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject[] objectsWithDecorations;
    List<IDecoration> _decorations = new List<IDecoration>();


    public bool HasKeyToCastle { get; set; }

    private void Awake() 
    {
        foreach (var item in objectsWithDecorations)
        {
            if(item.GetComponent<IDecoration>()!=null)
                _decorations.Add(item.GetComponent<IDecoration>());
        }       

        Player = Player.I;
    }

    public Player Player { get; private set; } //private set - неможем передобавить Player

    public void InvokeDecorations()
    {
        foreach (var decoration in _decorations)
        {
            decoration.InvokeDecoration();
        }
    }
}
