using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementIconEffect : MonoBehaviour {

    [SerializeField]
    private Player player;
    [SerializeField]
    public Sprite myFirstImage;
    [SerializeField]
    public Sprite mySecondImage;
    [SerializeField]
    private float lerptime = 1.0f;

    private Image magicTime;

    private float time;

    private float timeE;
    private bool timeFlag = true;

    private bool newMagic1 = false;
    private bool newMagic2 = false;

    private float currentFill;

    // Use this for initialization
    void Start () {
        magicTime = GetComponent<Image>();
        time = player.magicTime;
        timeE = player.magicTime;
    }
	
	// Update is called once per frame
	void Update () {

        /*
         Thats looks much, but the function of Ice is the same as for Fire
         */

        if (player.activeMagic.ToString() == "FIRE" )
        {
            //this is a flag, for the case, when player change the element, while the current element effect is not finish
            newMagic1 = true;

            //When chaning from ice to fire, set time back, deactivate ice particalt and set eementchange flag back
            if (newMagic2 != false)
            {
                timeE = time;
                newMagic2 = false;
                player.iceParticles.SetActive(false);
            }

            //When element is activated and the currentelement is the same reset duration time
            if (player.ReFreshMagic != false)
            {
                timeE = time;
                player.ReFreshMagic = false;
            }

            //activate image, partical and effects
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = myFirstImage;
            player.fireParticles.SetActive(true);

            //This is only a time counter
            if (timeFlag != false)
            {
                 StartCoroutine(duration());
                 //Invoke("duration", 1);
            }
            else if (timeE < 1)
            {
                //When time is beow 0 deactivate everything 
                magicTime.fillAmount = 1;
                this.GetComponent<Image>().enabled = false;
                player.beNeutral();
                timeE = time;
                newMagic1 = false;
                player.fireParticles.SetActive(false);
            }
        }
        else if (player.activeMagic.ToString() == "ICE")
        {
            newMagic2 = true;

            if (newMagic1 != false) {
                timeE = time;
                newMagic1 = false;
                player.fireParticles.SetActive(false);
            }

            if (player.ReFreshMagic != false) {
                timeE = time;
                player.ReFreshMagic = false;
            }
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Image>().sprite = mySecondImage;
            player.iceParticles.SetActive(true);

            if (timeFlag != false)
            {
                StartCoroutine(duration());
            }
            else if (timeE < 1)
            {
                magicTime.fillAmount = 1;
                this.GetComponent<Image>().enabled = false;
                player.beNeutral();
                timeE = time;
                newMagic2 = false;
                player.iceParticles.SetActive(false);
            }
        }
        
    }

    //The time counter change the image and wait one sek.
    IEnumerator duration()
    {
        magicTime.fillAmount = ((timeE - 1) / time);
        timeE -= 1;
        timeFlag = false;
        yield return new WaitForSeconds(1);
        timeFlag = true;
    }
}
