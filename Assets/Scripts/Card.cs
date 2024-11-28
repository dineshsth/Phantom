using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{
    public bool isCardFrontFaceActive;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }
    private void Start()
    {
        button.onClick.AddListener(() => { ShowFace(); });
    }

    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Reset();
    }

    private void ShowFace()
    {
        if (isCardFrontFaceActive) return;

        StartCoroutine(CalculateFlip());
    }
    private void Reset()
    {
        StartCoroutine(CalculateFlip(1));
    }
    private IEnumerator CalculateFlip(int rotStepMultiplier = 1)
    {
        int rotSpeed = 5 * rotStepMultiplier;
        for (int i = 0; i < 180; i += rotSpeed)
        {
            yield return new WaitForSeconds(0.001f);
            transform.Rotate(Vector3.up * rotSpeed);

            if (i == 90)            
                Flip();            
        }
        yield return new WaitForSeconds(1);
    }

    private void Flip()
    {
        isCardFrontFaceActive = !isCardFrontFaceActive;
    }
}
