using UnityEngine;
using System.Collections;

public class VisualizationSmoothScript : MonoBehaviour
{

    //private Vector3 lastPos;
    //private Vector3 parentPos;
    //public Vector3 ParentPos
    //{
    //    get
    //    {
    //        return parentPos;
    //    }
    //    set
    //    {
    //        if (value != parentPos)
    //        {
    //            lastPos = transform.position;
    //            parentPos = value;
    //            //Tock();
    //        }
    //        else
    //        {
    //            //AntiTock();
    //        }
    //        valDTC = (timerDTC * (.2f)) + (valDTC * (0.8f));
    //        timerDTC = 0;
    //        lerpIntv = 0;
    //    }
    //}

    //// value: Delta Time Calculation - amount of time between Photon updates
    //private float valDTC;
    //// timer: Delta Time Calculation - timer used to mark time between Photon updates
    //private float timerDTC;

    //// value fed to Lerp's T value
    //private float lerpIntv;
    //// value derived from the ratio of fixedUpdate interval to Photon's update interval
    //private float lerpStep;

    // Use this for initialization

    public Vector3 targetPos;
    public Quaternion targetRot;

    void Start()
    {
        transform.position = Vector3.up * 100;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, .09f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, .09f);
        //ParentPos = transform.parent.position;

        //timerDTC += Time.deltaTime;
        ////Debug.Log(timerDTC);

        //lerpStep = Time.deltaTime / valDTC;
        //lerpIntv += lerpStep;

        ////Debug.Log(lerpStep + " " + lerpIntv + " " + Time.fixedDeltaTime);

        //transform.position = Vector3.Lerp(lastPos, ParentPos, lerpIntv);



    }

    //void Tock()
    //{
    //    Debug.Log("tock " + ParentPos);
    //}
    //void AntiTock()
    //{
    //    Debug.Log("anti-tock");
    //}
}
