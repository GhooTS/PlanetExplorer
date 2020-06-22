public struct ValidationInformation
{
    public bool isInValidePlace;
    public string validationMessage;

    public ValidationInformation(bool isInValidePlace, string validationMessage)
    {
        this.isInValidePlace = isInValidePlace;
        this.validationMessage = validationMessage;
    }
}
