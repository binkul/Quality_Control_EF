using System;

namespace Quality_Control_EF.Commons
{
    public class QualityDataColumn
    {
        public string DBColumnName { get; set; }
        public string ColumnHeader { get; set; }
        public int ColumnIndex { get; set; }
        public double ColumnWidth { get; set; }
        public bool CanUserSort { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsValue { get; set; }
        public string ValueFormat { get; set; }

        public QualityDataColumn(string dBColumnName, string columnHeader, int columnIndex, double columnWidth, 
            bool canUserSort, bool isReadOnly, bool isValue, string valueFormat)
        {
            DBColumnName = dBColumnName;
            ColumnHeader = columnHeader;
            ColumnIndex = columnIndex;
            ColumnWidth = columnWidth;
            CanUserSort = canUserSort;
            IsReadOnly = isReadOnly;
            IsValue = IsValue;
            ValueFormat = valueFormat;
        }
    }
}
