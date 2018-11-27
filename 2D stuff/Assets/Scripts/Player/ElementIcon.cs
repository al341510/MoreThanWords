using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIcon : MonoBehaviour {

    [SerializeField]
    private Player player;
    [SerializeField]
    public Sprite myFirstImage;
    [SerializeField]
    public Sprite mySecondImage;

    private bool change = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.activeMagic.ToString() == "FIRE")
        {
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = myFirstImage;
            change = true;
        }
        else if (player.activeMagic.ToString() == "ICE")
        {
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = mySecondImage;
            change = true;
        }
        /*
         TO-DO add requiment for neutral
         */
    }
}
