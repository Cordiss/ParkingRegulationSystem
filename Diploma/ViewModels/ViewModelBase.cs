using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using DevExpress.Mvvm;
using Diploma.Helpers;

namespace Diploma.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        private bool _isDisposed;

        protected IMessenger DefaultMessenger;

        private readonly Dictionary<string, string[]> _propertyDependenciesGraph;

        #endregion

        #region _ctors

        public ViewModelBase(IMessenger messenger)
        {
            _propertyDependenciesGraph = BuildPropertyDependenciesGraph();

            DefaultMessenger = messenger;
        }
    
        public ViewModelBase() : this(HttpClient.Factory.DefaultMessenger.Instance) { }

        #endregion

        #region Methods

        protected void SetProperty<T>(ref T backendProperty, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backendProperty, value)) return;

            backendProperty = value;
            OnPropertyChanged(propertyName);
        }

        private Dictionary<string, string[]> BuildPropertyDependenciesGraph()
        {
            return (from dependentProp in GetType().GetProperties()
                    from basePropName in
                        from attribute in dependentProp.GetCustomAttributes(typeof(DependsOnPropertyAttribute), true)
                            .Cast<DependsOnPropertyAttribute>()
                        select attribute.Property
                    group dependentProp.Name by basePropName
                    into graphArc
                    select new {BaseProp = graphArc.Key, DependentProps = graphArc})
                .ToDictionary(pair => pair.BaseProp, pair => pair.DependentProps.ToArray());
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (_propertyDependenciesGraph.TryGetValue(propertyName, out string[] dependentPropertyNames))
            {
                foreach (var dependentPropertyName in dependentPropertyNames)
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(dependentPropertyName));
                }
            }
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) return;

            _isDisposed = true;

            if (disposing)
            {
                // Dispose any managed resources
            }
        }

        #endregion
    }
}