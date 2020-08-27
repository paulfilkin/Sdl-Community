﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Schema;
using Sdl.Community.MtEnhancedProvider.Annotations;
using Sdl.Community.MtEnhancedProvider.Commands;
using Sdl.Community.MtEnhancedProvider.Helpers;
using Sdl.Community.MtEnhancedProvider.Model;
using Sdl.Community.MtEnhancedProvider.Model.Interface;
using Sdl.Community.MtEnhancedProvider.ViewModel.Interface;
using Sdl.LanguagePlatform.Core;
using Sdl.LanguagePlatform.TranslationMemoryApi;

namespace Sdl.Community.MtEnhancedProvider.ViewModel
{
	public class ProviderControlViewModel : ModelBase, IProviderControlViewModel
	{
		private readonly IMtTranslationOptions _options;
		private readonly ITranslationProviderCredentialStore _credentialStore;
		private readonly List<LanguagePair> _correspondingLanguages;
		private readonly  Uri _microsoftProviderUri= new Uri(PluginResources.UriMs);
		private readonly Uri _googleProviderUri = new Uri(PluginResources.UriGt);
		private TranslationOption _selectedTranslationOption;
		private GoogleApiVersion _selectedGoogleApiVersion;
		private bool _isMicrosoftSelected;
		private bool _useCatId;
		private bool _isV2Checked;
		private bool _persistGoogleKey;
		private bool _persistMicrosoftKey;
		private bool _isTellMeAction;
		private string _catId;
		private string _apiKey;
		private string _clientId;
		private string _jsonFilePath;
		private string _projectName;

		public ProviderControlViewModel(IMtTranslationOptions options, ITranslationProviderCredentialStore credentialStore, List<LanguagePair> correspondingLanguages)
		{
			ViewModel = this;
			_options = options;
			_credentialStore = credentialStore;
			_correspondingLanguages = correspondingLanguages ?? new List<LanguagePair>();

			InitializeComponent();
		}

		////TODO: If is tell me action hide back button(first page) we need to show only the settings page
		//public ProviderControlViewModel(IMtTranslationOptions options,bool isTellMeAction)
		//{
		//	_options = options;
		//	_isTellMeAction = isTellMeAction;
		//	InitializeComponent();
			
		//}

		private void InitializeComponent()
		{
			TranslationOptions = new List<TranslationOption>
			{
				new TranslationOption
				{
					Name = PluginResources.Microsoft,
					ProviderType = MtTranslationOptions.ProviderType.MicrosoftTranslator
				},
				new TranslationOption
				{
					Name = PluginResources.Google,
					ProviderType = MtTranslationOptions.ProviderType.GoogleTranslate
				}
			};

			GoogleApiVersions = new List<GoogleApiVersion>
			{
				new GoogleApiVersion
				{
					Name = "V2 - Basic Translation",
					Version = Enums.GoogleApiVersion.V2
				},
				new GoogleApiVersion
				{
					Name = "V3 - Advanced Translation",
					Version = Enums.GoogleApiVersion.V3
				}
			};

			if (_options != null)
			{
				ClientId = _options.ClientId;
				PersistMicrosoftKey = _options.PersistMicrosoftCreds;
				UseCatId = _options.UseCatID;
				CatId = _options.CatId;

				ApiKey = _options.ApiKey;
				PersistGoogleKey = _options.PersistGoogleKey;
				JsonFilePath = _options.JsonFilePath;
				ProjectName = _options.ProjectName;
			}

			SetTranslationOption();

			SetGoogleApiVersion();
		}

		public ModelBase ViewModel { get; set; }
		public ICommand ShowSettingsCommand { get; set; }
		public List<TranslationOption> TranslationOptions { get; set; }
		public List<GoogleApiVersion> GoogleApiVersions { get; set; }

		public GoogleApiVersion SelectedGoogleApiVersion
		{
			get => _selectedGoogleApiVersion;
			set
			{
				_selectedGoogleApiVersion = value;
				IsV2Checked = _selectedGoogleApiVersion.Version == Enums.GoogleApiVersion.V2;
				OnPropertyChanged(nameof(SelectedGoogleApiVersion));
			}
		}

		public TranslationOption SelectedTranslationOption
		{
			get => _selectedTranslationOption;
			set
			{
				_selectedTranslationOption = value;
				IsMicrosoftSelected = value.ProviderType == MtTranslationOptions.ProviderType.MicrosoftTranslator;
				OnPropertyChanged(nameof(SelectedTranslationOption));
			}
		}

		public bool IsMicrosoftSelected
		{
			get => _isMicrosoftSelected;
			set
			{
				if (_isMicrosoftSelected == value) return;
				_isMicrosoftSelected = value;
				if (_isMicrosoftSelected)
				{
					IsV2Checked = false;
				}
				OnPropertyChanged(nameof(IsMicrosoftSelected));
			}
		}

		public bool IsV2Checked
		{
			get => _isV2Checked;
			set
			{
				if (_isV2Checked == value) return;
				_isV2Checked = value;
				OnPropertyChanged(nameof(IsV2Checked));
			}
		}

		public string ApiKey
		{
			get => _apiKey;
			set
			{
				if (_apiKey == value) return;
				_apiKey = value;
				OnPropertyChanged(nameof(ApiKey));
			}
		}

		public string ClientId
		{
			get => _clientId;
			set
			{
				if (_clientId == value) return;
				_clientId = value;
				OnPropertyChanged(nameof(ClientId));
			}
		}

		public string JsonFilePath
		{
			get => _jsonFilePath;
			set
			{
				if (_jsonFilePath == value) return;
				_jsonFilePath = value;
				OnPropertyChanged(nameof(JsonFilePath));
			}
		}

		public string ProjectName
		{
			get => _projectName;
			set
			{
				if (_projectName == value) return;
				_projectName = value;
				OnPropertyChanged(nameof(ProjectName));
			}
		}

		public bool UseCatId
		{
			get => _useCatId;
			set
			{
				if (_useCatId == value) return;
				_useCatId = value;
				if (!value)
				{
					CatId = string.Empty;
				}
				OnPropertyChanged(nameof(UseCatId));
			}
		}

		public bool PersistGoogleKey
		{
			get => _persistGoogleKey;
			set
			{
				if (_persistGoogleKey == value) return;
				_persistGoogleKey = value;
				OnPropertyChanged(nameof(PersistGoogleKey));
			}
		}

		public bool PersistMicrosoftKey
		{
			get => _persistMicrosoftKey;
			set
			{
				if (_persistMicrosoftKey == value) return;
				_persistMicrosoftKey = value;
				OnPropertyChanged(nameof(PersistMicrosoftKey));
			}
		}

		public bool IsTellMeAction
		{
			get => _isTellMeAction;
			set
			{
				if (_isTellMeAction == value) return;
				_isTellMeAction = value;
				OnPropertyChanged(nameof(IsTellMeAction));
			}
		}

		public string CatId
		{
			get => _catId;
			set
			{
				if (_catId == value) return;
				_catId = value;
				OnPropertyChanged(nameof(CatId));
			}
		}

		private void SetTranslationOption()
		{
			if (_options?.SelectedProvider != null)
			{
				var selectedProvider = TranslationOptions.FirstOrDefault(t => t.ProviderType.Equals(_options.SelectedProvider));

				if (selectedProvider == null)
				{
					SelectMicrosoftTranslation();
				}
				else
				{
					switch (selectedProvider.ProviderType)
					{
						case MtTranslationOptions.ProviderType.GoogleTranslate:
							IsMicrosoftSelected = false;
							break;
						case MtTranslationOptions.ProviderType.MicrosoftTranslator:
							IsMicrosoftSelected = true;
							break;
						case MtTranslationOptions.ProviderType.None:
							IsMicrosoftSelected = false;
							break;
					}
					SelectedTranslationOption = selectedProvider;
				}
			}
			else
			{
				//Bydefault we'll select Microsoft translator option
				SelectMicrosoftTranslation();
			}
		}

		//TODO: Check if the google version is persisted correctly in the provider settings
		private void SetGoogleApiVersion()
		{
			if (_options?.SelectedGoogleVersion != null)
			{
				var selectedVersion = GoogleApiVersions.FirstOrDefault(v => v.Version.Equals(_options.SelectedGoogleVersion));
				if (selectedVersion != null)
				{
					SelectedGoogleApiVersion = selectedVersion;
				}
				else
				{
					SelectGoogleV2();
				}
			}
			else
			{
				//Bydefault we'll select Google V2 version - which is the basic one
				SelectGoogleV2();
			}
		}

		private void SelectMicrosoftTranslation()
		{
			SelectedTranslationOption = TranslationOptions[0];
			IsMicrosoftSelected = true;
		}

		private void SelectGoogleV2()
		{
			SelectedGoogleApiVersion = GoogleApiVersions[0];
			IsV2Checked = true;
		}
	}
}
