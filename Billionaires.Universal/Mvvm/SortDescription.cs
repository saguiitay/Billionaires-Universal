using System;

namespace Billionaires.Universal.Mvvm
{
    public class SortDescription
    {
        public SortDescription(Func<object, object> valueGetter, ListSortDirection direction = ListSortDirection.Ascending)
        {
            //PropertyName = propertyName;
            ValueGetter = valueGetter;
            Direction = direction;
        }
        //public string PropertyName { get; set; }
        public Func<object, object> ValueGetter { get; set; }
        public ListSortDirection Direction { get; set; }
    }
}

