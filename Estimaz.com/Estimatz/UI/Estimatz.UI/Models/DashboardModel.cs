namespace Estimatz.UI.Models
{
    public class DashboardModel
    {
        public List<IndicatorModel> Indicators { get; set; } = new List<IndicatorModel>();
    }

    public class IndicatorModel
    {
        public string Description { get; set; }
        public string Value { get; set; }
    }
}
