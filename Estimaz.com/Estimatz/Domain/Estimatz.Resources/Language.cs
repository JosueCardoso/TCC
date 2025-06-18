using Estimatz.Resources.Languages;

namespace Estimatz.Resources
{
    public static class Language
    {
        private static LanguageManager _languageResource;

        public static void SetCulture(string culture)
        {
            _languageResource = new LanguageManager(culture);
        }

        public static string GetString(string text) => _languageResource.GetString(text);
        public static string GetSelectedCulture() => _languageResource.GetSelectedCulture();
        public static string GetLogoName()
        {
            var selectedCulture = _languageResource.GetSelectedCulture();

            if (selectedCulture == "pt-br")
                return "logotipo-pt.png";

            if (selectedCulture == "en-us")
                return "logotipo-en.png";

            return "logotipo-es.png";
        }
    }
}
