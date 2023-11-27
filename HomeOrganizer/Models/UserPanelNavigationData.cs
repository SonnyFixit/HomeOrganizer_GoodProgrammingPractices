namespace HomeOrganizer.Models
{
    public struct UserPanelNavigationData
    {
        public string Text;
        public object Value;

        public UserPanelNavigationData(string text, object value)
        {
            Text = text;
            Value = value;
        }
    }
}
