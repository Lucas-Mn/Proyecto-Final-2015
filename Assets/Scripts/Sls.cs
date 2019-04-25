using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Sls : MonoBehaviour {

    public GameObject gm;
    public void Ch ()
    {
        gm.transform.GetChild(0).GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("RightArrow");
    }

    /*GameObject is a class.
    gameObject is a property you can call on a MonoBehaviour to the the GameObject instance that Monobehaviour instance is attached to.
    this is the instance of the class the code is in. */
}
