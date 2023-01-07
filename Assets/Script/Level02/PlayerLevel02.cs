using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerLevel02 : MonoBehaviour
{
    private float rotationSpeed = 90f;
    private float speed = .5f;

    public int levelSpeed = 1;
    int MaxLevelSpeed = 5;

    int direction = 0;// 0 thang, -1 xuong, 1 len

    public float time = 6.0f;
    public GameObject objMess;
    public GameObject objTime;
    public GameObject objTextSpeed;

    private Text textMess;
    private Text textTime;
    private Text textSpeed;

    private bool endGame = false;
    private bool startGame = false;

    private void Awake()
    {
        /*        objMess = GameObject.Find("Mess");
                objTime = GameObject.Find("Time");*/
    }

    // Start is called before the first frame update
    void Start()
    {
        textTime = objTime.GetComponent<Text>();
        textMess = objMess.GetComponent<Text>();
        textSpeed = objTextSpeed.GetComponent<Text>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        if (!endGame && startGame)
        {
            Move();
        }

        if (time >= 1)
        {
            int tempTime = (int)time;
            textTime.text = "" + tempTime;
        }

        if (time < 1 && !startGame)
        {
            startGame = true;
            objTime.SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {


    }

    private void Move()
    {
        float v = Input.GetAxisRaw("Vertical");
        if (v == 1)
        {
            if (direction != 1)
            {
                Debug.Log("1");
                direction = 1;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -35));
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else if (v == 0)
        {
            if (direction != 0)
            {

            }
        }
        else
        {
            if (direction != -1)
            {
                Debug.Log("-1");
                direction = -1;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -135));
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("0");
            direction = 0;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -90));
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        float curentSpeed = speed + (levelSpeed - 1) * 0.2f;
        transform.position += new Vector3(curentSpeed * Time.deltaTime, 0f, 0f);

        if (direction == -1)
        {
            transform.position += new Vector3(0f, -curentSpeed * Time.deltaTime, 0f);
        }
        else if (direction == 1)
        {
            transform.position += new Vector3(0f, curentSpeed * Time.deltaTime, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            textMess.text = "Loss";
        }

        if (collision.gameObject.tag == "exit")
        {
            textMess.text = "Win";
        }
        endGame = true;
        objMess.SetActive(true);
    }

#pragma warning disable CS0618 // Type or member is obsolete
    public void BackButtonClick() => Application.LoadLevel("LevelMenu");
    public void ResetButtonClick() => Application.LoadLevel("Level02");
#pragma warning restore CS0618 // Type or member is obsolete

    public void UpSpeedClick()
    {
        if(!startGame && levelSpeed  < MaxLevelSpeed)
        {
            levelSpeed++;
            textSpeed.text = "" + levelSpeed;
        }
    }
    public void DownSpeedClick()
    {
        if (!startGame && levelSpeed < MaxLevelSpeed)
        {
            levelSpeed++;
            textSpeed.text = "" + levelSpeed;
        }
    }
}
