/// <summary>
/// 1 - property name in EF model
/// 2 - field name in DataBase
/// 3 - header name in DataGrid
/// 4 - DataGrid column index
/// 5 - DataGrid column width
/// 6 - DataGrid column IsSortable
/// 7 - DataGrid column IsReadOnly
/// 8 - DataGrid column IsAlwaysVisible
/// 9 - DataGrid column IsNumber (double)
/// 10 - DataGrid column for IsNumber - value format
/// 11 - DataGrid cell accuracy - number of decimal points
/// </summary>

namespace Quality_Control_EF.Commons
{
    public class QualityDataColumn
    {
        public string EFColumnName { get; set; }
        public string DBColumnName { get; set; }
        public string ColumnHeader { get; set; }
        public int ColumnIndex { get; set; }
        public double ColumnWidth { get; set; }
        public bool CanUserSort { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsAlwaysVisible { get; set; }
        public bool IsValue { get; set; }
        public string ValueFormat { get; set; }
        public int ValueAccuracy { get; set; }

        public QualityDataColumn(string eFColumnName, string dBColumnName, string columnHeader, int columnIndex, double columnWidth, 
            bool canUserSort, bool isReadOnly, bool isAlwaysVisible, bool isValue, string valueFormat, int valueAccuracy)
        {
            EFColumnName = eFColumnName;
            DBColumnName = dBColumnName;
            ColumnHeader = columnHeader;
            ColumnIndex = columnIndex;
            ColumnWidth = columnWidth;
            CanUserSort = canUserSort;
            IsReadOnly = isReadOnly;
            IsAlwaysVisible = isAlwaysVisible;
            IsValue = isValue;
            ValueFormat = valueFormat;
            ValueAccuracy = valueAccuracy;
        }
    }
}
