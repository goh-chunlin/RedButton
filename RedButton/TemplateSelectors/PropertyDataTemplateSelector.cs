using RedButton.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RedButton.TemplateSelectors
{
    public class PropertyDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// Gets or sets the text property.
        /// </summary>
        public DataTemplate TextPropertyTemplate { get; set; }

        /// <summary>
        /// Gets or sets the boolean property.
        /// </summary>
        public DataTemplate BooleanPropertyTemplate { get; set; }

        /// <summary>
        /// Gets or sets the int property.
        /// </summary>
        public DataTemplate NumericPropertyTemplate { get; set; }

        /// <summary>
        /// Gets or sets the color property.
        /// </summary>
        public DataTemplate ColorPropertyTemplate { get; set; }

        /// <summary>
        /// Gets or sets the blank.
        /// </summary>
        public DataTemplate BlankTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, Windows.UI.Xaml.DependencyObject container)
        {
            if (item is null) return BlankTemplate;

            var customObject = (item as PropertyObjectViewModel);
            var type = customObject.PropertyValue;

            if (type is null) return BlankTemplate;
            if (type is string) return TextPropertyTemplate;
            if (type is bool) return BooleanPropertyTemplate;
            if (type is int) return NumericPropertyTemplate;
            if (type is double) return NumericPropertyTemplate;
            if (type is float) return NumericPropertyTemplate;
            if (type is Color) return ColorPropertyTemplate;

            return BlankTemplate;
        }
    }
}
