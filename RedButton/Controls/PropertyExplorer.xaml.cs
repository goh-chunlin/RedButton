using RedButton.Helpers;
using RedButton.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Telerik.Data.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace RedButton.Controls
{
    public sealed partial class PropertyExplorer : UserControl
    {
        public PropertyCollection Properties
        {
            get { return (PropertyCollection)GetValue(PropertiesProperty); }
            set { SetValue(PropertiesProperty, value); }
        }

        public static readonly DependencyProperty PropertiesProperty =
            DependencyProperty.Register("Properties",
                typeof(PropertyCollection),
                typeof(PropertyExplorer),
                new PropertyMetadata(null, (d, e) =>
                {
                    var ctrl = d as PropertyExplorer;
                    if (e.NewValue is null) return;

                    var data = e.NewValue as PropertyCollection;

                    var list = new ObservableCollection<PropertyObjectViewModel>();
                    foreach (PropertyObjectViewModel item in data.Values)
                    {
                        list.Add(item);
                    }
                    ctrl.propertyGrid.ItemsSource = list.OrderBy(p => p.PropertyName);
                }));

        public PropertyExplorer()
        {
            this.InitializeComponent();
        }

        public event RoutedEventHandler ValuesChanged;

        private void ArrangedBySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (propertyGrid == null) return;

            var comboBox = sender as ComboBox;
            var selectedItem = ((ComboBoxItem)comboBox.SelectedItem).Content as string;

            propertyGrid.GroupDescriptors.Clear();

            if (selectedItem == "Category")
            {
                var descriptor = new PropertyGroupDescriptor();
                descriptor.PropertyName = "HeaderGroupName";
                propertyGrid.GroupDescriptors.Add(descriptor);
            }

        }

        private void SearchClicked(object sender, RoutedEventArgs e)
        {
            FindProperty();
        }

        private void SearchBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            FindProperty();
        }


        private void FindProperty()
        {
            var descriptor = new TextFilterDescriptor();
            propertyGrid.FilterDescriptors.Clear();
            descriptor.PropertyName = "PropertyName";
            descriptor.IsCaseSensitive = false;
            descriptor.Value = SearchBox.Text;
            descriptor.Operator = TextOperator.Contains;
            propertyGrid.FilterDescriptors.Add(descriptor);
            if (SearchBox.Text.Length > 0)
            {
                Search.Visibility = Visibility.Collapsed;
            }
            else
            {
                Search.Visibility = Visibility.Visible;
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValuesChanged?.Invoke(this, new PropertyWindowValuesChangedArgs());
        }
    }
}
