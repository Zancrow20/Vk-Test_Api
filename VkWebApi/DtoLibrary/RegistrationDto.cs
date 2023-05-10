namespace DtoLibrary;

public class RegistrationDto
{
    public string ErrorMessage { get; set; }
    public bool IsSuccessful { get; set; }

    public RegistrationDto(string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsSuccessful = false;
    }

    public RegistrationDto()
    {
        IsSuccessful = true;
    }
}