﻿using System.Windows;
using System.Windows.Controls;

namespace MicrosoftTranslatorProvider.Helpers
{
	public class PasswordHelper
	{
		public static readonly DependencyProperty PasswordProperty = 
			DependencyProperty.RegisterAttached(
				"Password",
				typeof(string),
				typeof(PasswordHelper), 
				new FrameworkPropertyMetadata(string.Empty, OnPasswordPropertyChanged));

		public static readonly DependencyProperty AttachProperty = 
			DependencyProperty.RegisterAttached(
				"Attach",
				typeof(bool), 
				typeof(PasswordHelper), 
				new PropertyMetadata(false, Attach));

		private static readonly DependencyProperty IsUpdatingProperty =
			DependencyProperty.RegisterAttached(
				"IsUpdating", 
				typeof(bool),
				typeof(PasswordHelper));

		public static void SetAttach(DependencyObject dp, bool value)
		{
			dp.SetValue(AttachProperty, value);
		}

		public static bool GetAttach(DependencyObject dp)
		{
			return (bool)dp.GetValue(AttachProperty);
		}

		public static void SetPassword(DependencyObject dp, string value)
		{
			dp.SetValue(PasswordProperty, value);
		}

		public static string GetPassword(DependencyObject dp)
		{
			return (string)dp.GetValue(PasswordProperty);
		}

		private static void SetIsUpdating(DependencyObject dp, bool value)
		{
			dp.SetValue(IsUpdatingProperty, value);
		}

		private static bool GetIsUpdating(DependencyObject dp)
		{
			return (bool)dp.GetValue(IsUpdatingProperty);
		}

		private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			var passwordBox = sender as PasswordBox;
			passwordBox.PasswordChanged -= PasswordChanged;
			if (!GetIsUpdating(passwordBox))
			{
				passwordBox.Password = (string)e.NewValue;
			}

			passwordBox.PasswordChanged += PasswordChanged;
		}

		private static void Attach(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (!(sender is PasswordBox passwordBox))
			{
				return;
			}

			if ((bool)e.OldValue)
			{
				passwordBox.PasswordChanged -= PasswordChanged;
			}

			if ((bool)e.NewValue)
			{
				passwordBox.PasswordChanged += PasswordChanged;
			}
		}

		private static void PasswordChanged(object sender, RoutedEventArgs e)
		{
			var passwordBox = sender as PasswordBox;
			SetIsUpdating(passwordBox, true);
			SetPassword(passwordBox, passwordBox?.Password);
			SetIsUpdating(passwordBox, false);
		}
	}
}