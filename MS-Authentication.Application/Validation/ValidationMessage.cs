namespace MS_Authentication.Application.Validation;

public static class ValidationMessage
{
    public static string requiredField =
        "O campo {PropertyName} é obrigatório.";  
    public static string NotFound =
        "O campo {PropertyName} não foi encontrado em nossa base de dados."; 
    public static string InvalidEmail =
        "Formato do email invalído.";
    public static string MinimumMaximumCharacters =
        "O nome deve ter entre 3 e 255 caracteres.";
}
