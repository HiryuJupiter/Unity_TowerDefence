using UnityEngine;
using System.Collections;

public class BulletSpark : MonoBehaviour
{
    const float shrinkSpeed = 5f;

    //Variations of the spark sprite
    [SerializeField] Sprite[] imageOptions;

    SpriteRenderer spriteRenderer;

    IEnumerator Start()
    {
        //Reference sprite renderer then give it a random sprite
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = imageOptions[Random.Range(0, imageOptions.Length)];

        //Shrinks this over time and then destroy it after it is small enough
        for (float t = 1f; t > 0.1f; t -= Time.deltaTime * shrinkSpeed)
        {
            transform.localScale = new Vector3(t, t, 1f);
            yield return null;
        }
        Destroy(gameObject);
    }
}