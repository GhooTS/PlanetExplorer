using UnityEngine;

public interface IValidator
{
    bool StartValidation<T>(T objectToValidate)
        where T : MonoBehaviour;
    void EndValidation();
    bool ChceckIfInValidePlace();
}
