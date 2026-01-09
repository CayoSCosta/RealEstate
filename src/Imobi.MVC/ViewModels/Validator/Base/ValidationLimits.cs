namespace Imobi.MVC.ViewModels.Validator.Base
{

    public static class ValidationLimits
    {
        // Limites para o tipo TITLE
        public const int TitleMin = 3;
        public const int TitleMax = 100;

        // Limites para o tipo TEXT (Geral)
        public const int TextMax = 50;

        // Limites para o tipo TEXT BOX (Geral)
        public const int TextBoxMax = 500;

        // Limites para o tipo NÚMERO
        public const int NumberMaxDigits = 12;
    }

}
