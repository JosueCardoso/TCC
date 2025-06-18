namespace Estimatz.UI.Extensions
{
    public static class MenuManager
    {
        private static string _activeMenu = "dashboard";

        public static void SetMenuActive(string menuName)
        {
            _activeMenu = menuName;
        }

        public static string MenuIsActive(string menuName)
        {
            return _activeMenu == menuName ? "active" : string.Empty;
        }
    }
}
