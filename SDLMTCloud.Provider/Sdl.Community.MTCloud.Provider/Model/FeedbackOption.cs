﻿using Sdl.Community.MTCloud.Provider.ViewModel;

namespace Sdl.Community.MTCloud.Provider.Model
{
	public class FeedbackOption : BaseViewModel
	{
		private bool _isChecked;
		private string _studioActionId;
		private string _tooltip;

		public bool IsChecked
		{
			get => _isChecked;
			set
			{
				if (_isChecked == value) return;
				_isChecked = value;
				OnPropertyChanged(nameof(IsChecked));
			}
		}

		public string OptionName { get; set; }

		public string StudioActionId
		{
			get => _studioActionId;
			set
			{
				if (_studioActionId == value) return;
				_studioActionId = value;
				OnPropertyChanged(nameof(StudioActionId));
			}
		}
		public string Tooltip
		{
			get => _tooltip;
			set
			{
				if (_tooltip == value) return;
				_tooltip = value;
				OnPropertyChanged(nameof(Tooltip));
			}
		}
	}
}
