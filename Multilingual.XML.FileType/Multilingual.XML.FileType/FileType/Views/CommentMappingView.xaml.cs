﻿using System.Windows.Controls;
using Multilingual.XML.FileType.FileType.Settings;
using Sdl.Desktop.IntegrationApi.Interfaces;
using Sdl.FileTypeSupport.Framework.Core.Settings;

namespace Multilingual.XML.FileType.FileType.Views
{
	/// <summary>
	/// Interaction logic for LanguageMappingView.xaml
	/// </summary>
	public partial class CommentMappingView : UserControl, IUISettingsControl, IFileTypeSettingsAware<CommentMappingSettings>
	{
		public CommentMappingView()
		{
			InitializeComponent();
		}

		public CommentMappingSettings Settings { get; set; }

		
		public bool ValidateChildren()
		{
			return true;
		}

		public void Dispose()
		{
		}
	}
}
