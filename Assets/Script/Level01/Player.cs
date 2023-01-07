using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int mang;
    public int diem;
    public int maxScore = 72;


    public bool endGame = false;

    public float speed = 3f;
    private Transform tran;
    public GameObject GDiem;
    public GameObject GMang;
    public GameObject mess;
    public GameObject messNN;

    private Text tDiem;
    private Text tMang;
    private Text tMess;
    
    void Awake()
    {
        tran = GetComponent<Transform>();
        tDiem = GDiem.GetComponent<Text>();
        tMang = GMang.GetComponent<Text>();
        tMess = mess.GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        diem = 0;
        mang = 3;

        tDiem.text = "" + diem;
        tMang.text = "" + mang;

        mess.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!endGame)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            tran.position += new Vector3(speed * h * Time.deltaTime, speed * v * Time.deltaTime, 0);
        }
    }

    void Init()
    {
        mang = 3;
        diem = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "monster")
        {
            mang--;
            tMang.text = "" + mang;

            tran.position = new Vector3(-6.0f, 0, 380.6967f);
            if (mang == 0)
            {
                tMess.text = "Loss";
                mess.SetActive(true);
                endGame = true;
            }
            
        }
        if (collision.gameObject.tag == "exit")
        {
            if (diem == maxScore)
            {
                tMess.text = "Win";
                mess.SetActive(true);
                endGame = true;
            } else
            {
                GameObject tempMess = GameObject.Instantiate(messNN);
                GameObject.Destroy(tempMess, 2);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            diem++;
            tDiem.text = "" + diem;
            GameObject.Destroy(collision.gameObject);
        }
    }

}
