using GTVariable;
using System.Collections;
using UnityEngine;

public class PlaceableValidator : MonoBehaviour, IGameObjectValidator
{
    public ColorVariable validColor;
    public ColorVariable invalidColor;
    public bool Valid { get; private set; }
    public string LastValidationMessage { get; private set; }
    private IPlacingValidator[] placingValidator;
    [SerializeField]
    private SpriteRenderer[] spriteRenderers;
    private Color[] spriteColor;



    public void StartValidation()
    {
        placingValidator = GetComponents<IPlacingValidator>();
        spriteColor = new Color[spriteRenderers.Length];
        SnapDefaultSpritesColor();
        StartCoroutine(nameof(RunValidation));
    }

    private bool IsValide()
    {
        for (int i = 0; i < placingValidator.Length; i++)
        {
            if (placingValidator[i].Validate() == false)
            {
                LastValidationMessage = placingValidator[i].InvalideMessage;
                return false;
            }
        }

        return true;
    }

    public void StopValidation()
    {
        StopAllCoroutines();
        SetDefaultSpritesColor();
    }

    private IEnumerator RunValidation()
    {
        while (true)
        {
            if (IsValide())
            {

                SetSpritesColor(validColor);
                Valid = true;
            }
            else
            {
                SetSpritesColor(invalidColor);
                Valid = false;
            }

            yield return null;
        }
    }



    private void SnapDefaultSpritesColor()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteColor[i] = spriteRenderers[i].color;
        }
    }

    private void SetDefaultSpritesColor()
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = spriteColor[i];
        }
    }

    private void SetSpritesColor(Color color)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = color;
        }
    }
}
