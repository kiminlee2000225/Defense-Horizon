using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHealthUI : MonoBehaviour
{
    public Text wall1;
    public Text wall2;
    public Text wall3;
    public Text wall4;
    public Text wall5;
    public Text wall6;
    public Text wall7;
    public Text wall8;
    public Text wall9;
    public Text wall10;
    public Text wall11;
    public Text wall12;
    public Text wall13;
    public Text wall14;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var w1 = GameObject.Find("Wall1");
        var w2 = GameObject.Find("Wall2");
        var w3 = GameObject.Find("Wall3");
        var w4 = GameObject.Find("Wall4");
        var w5 = GameObject.Find("Wall5");
        var w6 = GameObject.Find("Wall6");
        var w7 = GameObject.Find("Wall7");
        var w8 = GameObject.Find("Wall8");
        var w9 = GameObject.Find("Wall9");
        var w10 = GameObject.Find("Wall10");
        var w11 = GameObject.Find("Wall11");
        var w12 = GameObject.Find("Wall12");
        var w13 = GameObject.Find("Wall13");
        var w14 = GameObject.Find("Wall14");
        wall1.text =  w1.name + "'s Health: " + w1.GetComponent<WallBehavior>().health.ToString();
        wall2.text = w2.name + "'s Health: " + w2.GetComponent<WallBehavior>().health.ToString();
        wall3.text = w3.name + "'s Health: " + w3.GetComponent<WallBehavior>().health.ToString();
        wall4.text = w4.name + "'s Health: " + w4.GetComponent<WallBehavior>().health.ToString();
        wall5.text = w5.name + "'s Health: " + w5.GetComponent<WallBehavior>().health.ToString();
        wall6.text = w6.name + "'s Health: " + w6.GetComponent<WallBehavior>().health.ToString();
        wall7.text = w7.name + "'s Health: " + w7.GetComponent<WallBehavior>().health.ToString();
        wall8.text = w8.name + "'s Health: " + w8.GetComponent<WallBehavior>().health.ToString();
        wall9.text = w9.name + "'s Health: " + w9.GetComponent<WallBehavior>().health.ToString();
        wall10.text = w10.name + "'s Health: " + w10.GetComponent<WallBehavior>().health.ToString();
        wall11.text = w11.name + "'s Health: " + w11.GetComponent<WallBehavior>().health.ToString();
        wall12.text = w12.name + "'s Health: " + w12.GetComponent<WallBehavior>().health.ToString();
        wall13.text = w13.name + "'s Health: " + w13.GetComponent<WallBehavior>().health.ToString();
        wall14.text = w14.name + "'s Health: " + w14.GetComponent<WallBehavior>().health.ToString();
    }
}
