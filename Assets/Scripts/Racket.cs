using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Racket : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float defaultMoveSpeed;
    public float itemMoveSpeed;
    public string AxesName;
    public bool isScoreIncreased = false;
    public int Score { get; private set; }
    public Text scoreText;

    void FixedUpdate()
    {
        Movement();

        #region AnotherMovementOption
        //if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow))
        //{
        //    rb.transform.position += Vector3.up;
        //}

        //if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow))
        //{
        //    rb.transform.position += Vector3.down;
        //}
        #endregion

    }

    protected abstract void Movement();

    public void GetScore()
    {
        if (isScoreIncreased)
            Score += 2;
        else
            Score++;

        scoreText.text = Score.ToString();
    }

    public void ApplyItemEffectToRacket(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.SpeedItem:
                SpeedUp();
                Invoke("SpeedDown", 5.0f);
                break;
            case ItemType.ScoreItem:
                ScoreUp();
                Invoke("ScoreDown", 5.0f);
                break;
            case ItemType.SizeItem:
                SizeUp();
                Invoke("SizeDown", 5.0f);
                break;
            default:
                break;
        }
    }

    private void SpeedUp()
    {
        moveSpeed = itemMoveSpeed;
    }
    private void SpeedDown()
    {
        moveSpeed = defaultMoveSpeed;
    }
    private void ScoreUp()
    {
        isScoreIncreased = true;
    }
    private void ScoreDown()
    {
        isScoreIncreased = false;
    }
    private void SizeUp()
    {
        this.transform.localScale = new Vector3(0.47f, 4f, 1);
    }
    private void SizeDown()
    {
        this.transform.localScale = new Vector3(0.47f, 2.5f, 1);
    }

}
