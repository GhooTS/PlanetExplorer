public interface IPlacingValidator
{
    string InvalideMessage { get; }
    bool Validate();
}
