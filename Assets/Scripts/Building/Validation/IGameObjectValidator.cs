public interface IGameObjectValidator
{
    string LastValidationMessage { get; }
    bool Valid { get; }
    void StartValidation();
    void StopValidation();
}
