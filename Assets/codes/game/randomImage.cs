using UnityEngine.UI;
using UnityEngine;

public class randomImage : MonoBehaviour
{
    public Sprite[] images; // Array to store the images

    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        ChangeImage();
    }

    void ChangeImage()
    {
        if (images.Length == 0) return;

        int randomIndex = Random.Range(0, images.Length);
        imageComponent.sprite = images[randomIndex];
    }
}
