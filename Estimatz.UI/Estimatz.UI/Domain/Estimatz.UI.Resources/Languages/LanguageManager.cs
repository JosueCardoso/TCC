using System.Reflection;
using System.Resources;

namespace Estimatz.UI.Resources.Languages
{
    public class LanguageManager
    {     
        private ResourceManager _resourceManager;
        private string _selectedCulture;

        public LanguageManager(string culture)
        {            
            _resourceManager = new ResourceManager($"Estimatz.UI.Resources.Languages.{culture}", Assembly.GetExecutingAssembly());
            _selectedCulture = culture;
        }

        public string GetString(string text) => _resourceManager.GetString(text);
        public string GetSelectedCulture() => _selectedCulture;
    }
}
