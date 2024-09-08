using UnityEngine.UI;
using UnityEngine;

public class candyBullet : MonoBehaviour
{
    public float speed;
    public float destroyAfterSeconds;
    public Color[] colors;

    private int currentColorIndex;
    private Image imageComponent;

    void Start()
    {
        Invoke("DestroyObject", destroyAfterSeconds);

        // Get the Image component attached to this GameObject
        imageComponent = GetComponent<Image>();

        // Set a random initial color
        if (colors.Length > 0)
        {
            currentColorIndex = Random.Range(0, colors.Length);
            imageComponent.color = colors[currentColorIndex];
        }
    }

    void Update()
    {
        // Move the RectTransform
        GetComponent<RectTransform>().anchoredPosition += Vector2.left * speed * Time.deltaTime * 1000;
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
