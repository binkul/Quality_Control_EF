namespace Quality_Control_EF.Forms.Navigation
{
    internal interface INavigation
    {
        int DgRowIndex { get; set; }

        int GetRowCount { get; }

        void Refresh();
    }
}
