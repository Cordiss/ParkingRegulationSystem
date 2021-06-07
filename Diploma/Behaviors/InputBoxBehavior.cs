using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using DevExpress.Mvvm.UI.Interactivity;

namespace Diploma.Behaviors
{
    /// <summary>
    /// Defines InputBox behavior.
    /// </summary>
    public class InputBoxBehavior : Behavior<TextBox>
    {
        /// <summary>
        /// Dependency property that indicates current state of InputBox.
        /// </summary>
        public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register(
            "IsValid", typeof(bool), typeof(InputBoxBehavior),
            new PropertyMetadata(default(bool), PropertyChangedCallback));

        /// <summary>
        /// Dependency property that gets validation error text.
        /// </summary>
        public static readonly DependencyProperty ErrorTextProperty = DependencyProperty.Register(
            "ErrorText", typeof(string), typeof(InputBoxBehavior),
            new PropertyMetadata(default(string), ErrorTextPropertyChanged));

        /// <summary>
        /// Callback.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Event arguments.</param>
        private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// Callback.
        /// </summary>
        /// <param name="d">Dependency object.</param>
        /// <param name="e">Event arguments.</param>
        private static void ErrorTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { }

        /// <summary>
        /// Property that indicates current state of InputBox.
        /// </summary>
        public bool IsValid
        {
            get => (bool)GetValue(IsValidProperty);
            set => SetValue(IsValidProperty, value);
        }

        /// <summary>
        /// Property that gets validation error text.
        /// </summary>
        public string ErrorText
        {
            get => (string) GetValue(ErrorTextProperty);
            private set => SetValue(ErrorTextProperty, value);
        }

        /// <summary>
        /// Performs initialization actions on InputBox loaded.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            IsValid = true;
            AssociatedObject.TextChanged += AssociatedObjectOnTextChanged;
            AssociatedObject.MouseDown += AssociatedObjectOnLostFocus;
        }

        /// <summary>
        /// Performs de-initialization actions on InputBox unloaded.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.TextChanged -= AssociatedObjectOnTextChanged;
            AssociatedObject.MouseDown -= AssociatedObjectOnLostFocus;
        }

        /// <summary>
        /// Handles text changed event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void AssociatedObjectOnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                IsValid = textBox.Text.Validate(out var errorText);
                ErrorText = errorText;

                if (!IsValid)
                {
                    textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(232, 44, 75));

                    if (FindChild(textBox, "ValidationPopup") is Border border)
                    {
                        border.Visibility = Visibility.Visible;

                        if (VisualTreeHelper.GetChild(border, 0) is TextBlock textBlock)
                        {
                            textBlock.Text = ErrorText;
                        }
                    }
                }
                else
                {
                    textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(23, 212, 133));
                    if (FindChild(textBox, "ValidationPopup") is Border border)
                    {
                        border.Visibility = Visibility.Collapsed;

                        if (VisualTreeHelper.GetChild(border, 0) is TextBlock textBlock)
                        {
                            textBlock.Text = null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Handles lost focus event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event arguments.</param>
        private void AssociatedObjectOnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox
                && !string.IsNullOrEmpty(textBox.Text)
                && !textBox.IsMouseOver)
            {
                textBox.BorderBrush = IsValid
                    ? new SolidColorBrush(Color.FromRgb(23, 212, 133))
                    : new SolidColorBrush(Color.FromRgb(232, 44, 75));

                if (FindChild(textBox, "ValidationPopup") is Border border)
                {
                    border.Visibility = Visibility.Collapsed;

                    if (VisualTreeHelper.GetChild(border, 0) is TextBlock textBlock)
                    {
                        textBlock.Text = null;
                    }
                }
            }
        }

        /// <summary>
        /// Tries to find child of dependency object by name.
        /// </summary>
        /// <param name="dependencyObject">Parent dependency object.</param>
        /// <param name="objectName">Child name.</param>
        /// <returns>Child dependency object.</returns>
        private DependencyObject FindChild(DependencyObject dependencyObject, string objectName)
        {
            var grid = VisualTreeHelper.GetChild(dependencyObject, 0);
            var child = VisualTreeHelper.GetChild(grid, 1);

            if (child is FrameworkElement frameworkElement)
            {
                if (frameworkElement.Name.Equals(objectName))
                {
                    return child;
                }
            }

            return null;
        }
    }
}