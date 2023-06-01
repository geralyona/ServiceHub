using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;

namespace ServiceHub.Presentation.Behaviors
{
    public class WindowClosedBehavior : Behavior<UserControl>
    {
        private Window? _associatedWindow;

        public static readonly DependencyProperty ClosedCommandProperty =
        DependencyProperty.Register(nameof(ClosedCommand),typeof(ICommand), typeof(WindowClosedBehavior));

        public ICommand ClosedCommand
        {
            get => (ICommand)GetValue(ClosedCommandProperty);
            set => SetValue(ClosedCommandProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            if (_associatedWindow != null)
            {
                _associatedWindow.Closed -= _associatedWindow_Closed;
                _associatedWindow = null;
            }
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(AssociatedObject);
            while (parent is not Window)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            _associatedWindow = (Window)parent;
            _associatedWindow.Closed += _associatedWindow_Closed;
        }

        private void _associatedWindow_Closed(object? sender, EventArgs e)
        {
            if(ClosedCommand != null && ClosedCommand.CanExecute(null))
                ClosedCommand.Execute(null);
        }
    }
}
