using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Racket : MonoBehaviour
{
    [Header("Ref")]
    public Rigidbody2D rb;
    public Text scoreText;
    public Text itemText;

    [Header("Status")]
    public float moveSpeed;
    public float defaultMoveSpeed;
    public float itemMoveSpeed;
    public string AxesName;
    public int Score { get; private set; }
    public bool isScoreIncreased = false;
    [SerializeField] private ItemType myItemType;

    private Coroutine itemTextCoroutine;
    private void Start()
    {
        InitializeItemEffect();
        itemText.text = $"None : 0.0 / 5.0";
        itemText.color = Color.white;
    }
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
        myItemType = itemType;
        CancelInvoke(nameof(InitializeItemEffect));
        InitializeItemEffect();
        if (itemTextCoroutine != null)
        {
            StopCoroutine(itemTextCoroutine);
            itemTextCoroutine = null;
        }
        
        switch (itemType)
        {
            case ItemType.SpeedItem:
                SpeedUp();
                itemText.color = Color.red;
                break;
            case ItemType.ScoreItem:
                ScoreUp();
                itemText.color = Color.green;
                break;
            case ItemType.SizeItem:
                SizeUp();
                itemText.color= Color.yellow;
                break;
            default:
                break;
        }
        itemTextCoroutine = StartCoroutine(itemDurationTextControl());
        Invoke("InitializeItemEffect", 5f);
    }
    private void SpeedUp()
    {
        moveSpeed = itemMoveSpeed;
    }
    private void ScoreUp()
    {
        isScoreIncreased = true;
    }
    private void SizeUp()
    {
        this.transform.localScale = new Vector3(0.47f, 4f, 1);
    }
    private void InitializeItemEffect()
    {
        moveSpeed = defaultMoveSpeed;
        isScoreIncreased = false;
        this.transform.localScale = new Vector3(0.47f, 2.5f, 1);
    }
    private IEnumerator itemDurationTextControl()
    {
        yield return null;
        int itemDuration = 5;
        while (itemDuration >= 0)
        {
            if (itemDuration == 0)
            {
                itemText.text = $"None : 0.0 / 5.0";
                itemText.color = Color.white;
            }
            else
                itemText.text = $"{myItemType.ToString()} : {itemDuration}/5.0";

            itemDuration--;
            yield return new WaitForSeconds(1f);
        }
    }

}
