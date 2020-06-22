using UnityEngine;
using UnityEngine.Events;

public class Validator : MonoBehaviour, IValidator
{
    public UnityEvent<string> invalide;
    IGameObjectValidator objectValidator;

    public bool StartValidation<T>(T objectToValidate) where T : MonoBehaviour
    {
        if (objectToValidate.TryGetComponent(out objectValidator))
        {
            objectValidator.StartValidation();
            return true;
        }

        return false;
    }

    public bool ChceckIfInValidePlace()
    {
        if (objectValidator == null || objectValidator.Valid)
        {
            return true;
        }

        invalide?.Invoke(objectValidator.LastValidationMessage);

        return false;
    }

    public void EndValidation()
    {
        objectValidator?.StopValidation();
    }


}