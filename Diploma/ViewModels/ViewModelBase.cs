using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using DevExpress.Mvvm;
using Diploma.Helpers;

namespace Diploma.ViewModels
{
    /// <summary>
    /// Defines base class for all view models.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        #region Events

        /// <summary>
        /// ViewModel property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Fields

        /// <summary>
        /// Flag that indicates if view model was disposed.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// Reference to <see cref="IMessenger"/> interface.
        /// </summary>
        protected IMessenger DefaultMessenger;

        /// <summary>
        /// Properties dependencies graph.
        /// </summary>
        private readonly Dictionary<string, string[]> _propertyDependenciesGraph;

        #endregion

        #region _ctors

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messenger">Reference to <see cref="IMessenger"/> interface.</param>
        public ViewModelBase(IMessenger messenger)
        {
            _propertyDependenciesGraph = BuildPropertyDependenciesGraph();

            DefaultMessenger = messenger;
        }
    
        /// <summary>
        /// Constructor.
        /// </summary>
        public ViewModelBase() : this(HttpClient.Factory.DefaultMessenger.Instance) { }

        #endregion

        #region Methods

        /// <summary>
        /// Sets property by it's backend field.
        /// </summary>
        /// <typeparam name="T">Property type.</typeparam>
        /// <param name="backendProperty">Reference to property's backend field.</param>
        /// <param name="value">New property value.</param>
        /// <param name="propertyName">Property name.</param>
        protected void SetProperty<T>(ref T backendProperty, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backendProperty, value)) return;

            backendProperty = value;
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Builds properties dependency graph.
        /// </summary>
        /// <returns>Properties dependency graph.</returns>
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

        /// <summary>
        /// Handler for property changed event.
        /// </summary>
        /// <param name="propertyName">Property name.</param>
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

        /// <summary>
        /// Disposing of instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose any managed resources.
        /// </summary>
        /// <param name="disposing"></param>
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