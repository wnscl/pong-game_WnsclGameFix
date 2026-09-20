using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Racket LeftRacket, RightRacket;
    public Rigidbody2D ballRb;
    public float moveSpeed;

    [Header("Item")]
    [SerializeField] private ItemType myItemType;

    void Start()
    {

        ballRb.linearVelocity = new Vector2(1, 1) * moveSpeed;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TagMenager tagMenager = collision.gameObject.GetComponent<TagMenager>();

        GetComponent<AudioSource>().Play();

        if (tagMenager == null)
        {
            return;
        }

        Tag tag = tagMenager.wallTag;

        if (tag.Equals(Tag.leftWall))
        {
            RightRacket.GetScore();
        }

        if (tag.Equals(Tag.rightWall))
        {
            LeftRacket.GetScore();
        }
        if (tag.Equals(Tag.leftRacket))
        {
            if (myItemType != ItemType.Length)
            {
                LeftRacket.ApplyItemEffectToRacket(myItemType);
                RemoveItem();
            }
            wayBall(collision, 1);
        }
        if (tag.Equals(Tag.rightRacket))
        {
            if (myItemType != ItemType.Length)
            {
                RightRacket.ApplyItemEffectToRacket(myItemType);
                RemoveItem();
            }
            wayBall(collision, -1);
        }
    }

    private void wayBall(Collision2D collision, int x)
    {
        float a = transform.position.y - collision.gameObject.transform.position.y;
        float b = collision.collider.bounds.size.y;
        float y = a / b;
        ballRb.linearVelocity = new Vector2(x, y)*moveSpeed;
    }
    public void TakeItem(ItemType type)
    {
        myItemType = type;
        ChangeBallColor();
    }
    public void RemoveItem()
    {
        myItemType = ItemType.Length;
        ChangeBallColor();
    }
    private void ChangeBallColor()
    {
        SpriteRenderer sprite = this.gameObject.GetComponent<SpriteRenderer>();

        switch (myItemType)
        {
            case ItemType.SpeedItem:
                sprite.color = Color.red;
                break;
            case ItemType.ScoreItem:
                sprite.color = Color.green;
                break;
            case ItemType.SizeItem:
                sprite.color = Color.yellow;
                break;
            default:
                sprite.color = Color.white;
                break;
        }
    }
}
