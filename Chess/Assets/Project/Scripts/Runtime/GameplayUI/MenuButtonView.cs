using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class MenuButtonView : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Sprite menuSprite;
    [SerializeField] private Sprite exitSprite;
    
    private GameplayUIController gameplayUIController;

    
    [Inject]
    private void Construct(GameplayUIController gameplayUIController)
    {
        this.gameplayUIController = gameplayUIController;
    }
    void Start()
    {
        menuButton.image.sprite = menuSprite;
        menuButton.onClick.AddListener(gameplayUIController.MenuView);
    }

    public void ChangeButtonSprite(bool isActive)
    {
        menuButton.image.sprite = (isActive) ? exitSprite : menuSprite;
    }
}