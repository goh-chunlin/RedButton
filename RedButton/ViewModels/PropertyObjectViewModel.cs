using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RedButton.ViewModels
{
    public class PropertyObjectViewModel : INotifyPropertyChanged
    {
        private object _propertyValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyObjectViewModel"/> class.
        /// </summary>
        /// <param name="headerGroupName">The name.</param>
        public PropertyObjectViewModel(string headerGroupName)
        {
            HeaderGroupName = headerGroupName;
        }

        /// <summary>
        /// Gets or sets the header group.
        /// </summary>
        public string HeaderGroupName { get; set; }

        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the property description.
        /// </summary>
        public string PropertyDescription { get; set; }

        /// <summary>
        /// Gets or sets the property value.
        /// </summary>
        public object PropertyValue
        {
            get => _propertyValue;
            set
            {
                _propertyValue = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
