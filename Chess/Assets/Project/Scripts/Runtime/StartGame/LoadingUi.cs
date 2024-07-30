using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingUi : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadProgressText;
    [SerializeField] private Image loadProgressImage;

    [SerializeField] private Animator animator;

    private readonly int activePanelHash = Animator.StringToHash("ActiveLoadPanel");
    private readonly int closingPanelHash = Animator.StringToHash("ClosingLoadPanel");
    public float loadProgress
    {
        set
        {
            loadProgressText.text = $"Loading {value * 100}%";
            loadProgressImage.fillAmount = value;
        }
    }
    public event Action endActivePanel;
    public event Action endClosingPanel;

    private IEnumerator WaitActiveAnimEnd()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float animationLength = clipInfo[0].clip.length;

        yield return new WaitForSeconds(animationLength);

        loadProgressText.gameObject.SetActive(true);
        loadProgressImage.gameObject.SetActive(true);
        endActivePanel?.Invoke();
    }
    private IEnumerator WaitClosingAnimEnd()
    {
        
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        float animationLength = clipInfo[0].clip.length;

        yield return new WaitForSeconds(animationLength);

        endClosingPanel?.Invoke();
    }
    public void StartLoadAnim()
    {
        animator.Play(activePanelHash);
        StartCoroutine(WaitActiveAnimEnd());
    }

    public void StartClosingAnim()
    {
        animator.Play(closingPanelHash);
        StartCoroutine(WaitClosingAnimEnd());

        loadProgressText.gameObject.SetActive(false);
        loadProgressImage.gameObject.SetActive(false);
    }

}
