using System;

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

        public QualityDataColumn(string eFColumnName, string dBColumnName, string columnHeader, int columnIndex, double columnWidth, 
            bool canUserSort, bool isReadOnly, bool isAlwaysVisible, bool isValue, string valueFormat)
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
        }
    }
}
