using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteHolder : MonoBehaviour
{
    private Image connectedImage;

    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite deactivatedSprite;

    private void Start()
    {
        connectedImage = GetComponent<Image>();
    }

    public void SetActivated()
    {
        connectedImage.sprite = activeSprite;
    }
    public void SetDeactivated()
    {
        connectedImage.sprite = deactivatedSprite;
    }
}
