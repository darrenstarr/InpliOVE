﻿namespace InpliOVE.UWP.App.Converters
{
    using System;
    using System.Linq;
    using Windows.UI.Xaml.Data;

    public class SiteIdItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is Guid))
                return null;

            return Model.Context.Current.Sites.SingleOrDefault(x => x.Id == (Guid)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (value == null) ? Guid.Empty : (value as Model.Site).Id;
        }
    }
}
