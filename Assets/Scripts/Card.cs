using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    private CardDataSO cardDataSO;
    public GameObject backFace;

    public Image img;
    private Button button;

    private bool isCardFrontFaceActive;
    private bool isRotating;
    private bool cardFlipped;
    private float duration = 1f;
    private int cardPositionIndex;

    private void Start()
    {
        backFace.SetActive(true);
        button = GetComponent<Button>();
        button.onClick.AddListener(() => { ShowCardFace(); });
    }
    private void OnDisable()
    {
        button.onClick.RemoveListener(() => { ShowCardFace(); });
    }

    public void Initilize(CardDataSO data, float rotDuration, int index)
    {
        cardDataSO = data;
        img.sprite = cardDataSO.sprite;
        duration = rotDuration;
        cardPositionIndex = index;
    }
    public CardDataSO GetCardData() => cardDataSO;
    public int CardPositionIndex() => cardPositionIndex;

    private void ShowCardFace()
    {
        if (isCardFrontFaceActive || isRotating) return;

        StartCoroutine(PlayShowAnimation());
    }
    public void HideCardFace()
    {
        StartCoroutine(PlayHideAnimation());
    }
    private IEnumerator PlayShowAnimation()
    {
        GameManager.instance.PlayFlipCardSound();
        yield return StartFlipAnimation();
        GameManager.instance.OnCardSelected(this);
    }
    private IEnumerator PlayHideAnimation()
    {
        yield return StartFlipAnimation();
    }
    private IEnumerator StartFlipAnimation()
    {
        isRotating = true;
        cardFlipped = false;

        // Get the starting Y rotation
        float startYRotation = transform.eulerAngles.y;
        float targetYRotation = startYRotation + 180f; // Rotate by 180 degrees

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float currentYRotation = Mathf.Lerp(startYRotation, targetYRotation, t);
            transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);

            if (!cardFlipped) Flip(currentYRotation);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(0f, targetYRotation, 0f);
        isRotating = false;
    }

    private void Flip(float currentYRotation)
    {
        if (currentYRotation >= 90f && currentYRotation < 95f || currentYRotation >= 270 && currentYRotation < 275f)
        {
            isCardFrontFaceActive = !isCardFrontFaceActive;
            backFace.SetActive(!isCardFrontFaceActive);
            cardFlipped = true;
        }
    }
}