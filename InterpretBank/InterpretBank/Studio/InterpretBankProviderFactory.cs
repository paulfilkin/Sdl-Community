﻿using System;
using System.Data.SQLite;
using InterpretBank.GlossaryService;
using InterpretBank.GlossaryService.Interface;
using InterpretBank.SettingsService;
using InterpretBank.TerminologyService;
using InterpretBank.Wrappers;
using Sdl.Terminology.TerminologyProvider.Core;

namespace InterpretBank.Studio
{
	[TerminologyProviderFactory(Id = "My_Terminology_Provider_Id",
								Name = "My_Terminology_Provider_Name",
								Description = "My_Terminology_Provider_Description")]
	public class InterpretBankProviderFactory : ITerminologyProviderFactory
	{
		//private static InterpretBankDataContext _interpretBankDataContext;

		//public static InterpretBankDataContext InterpretBankDataContext
		//{
		//	get => _interpretBankDataContext ??= new InterpretBankDataContext();
		//	set => _interpretBankDataContext = value;
		//}

		//public static InterpretBankProvider GetInterpretBankProvider()
		//{
		//	var settingsService = new SettingsService.ViewModel.SettingsService(new OpenFileDialog(), InterpretBankDataContext);

		//	var termSearchService = new TerminologyService.TerminologyService(InterpretBankDataContext);
		//	return new InterpretBankProvider(termSearchService, settingsService);
		//}

		public ITerminologyProvider CreateTerminologyProvider(Uri terminologyProviderUri, ITerminologyProviderCredentialStore credentials)
		{
			var settings = PersistenceService.PersistenceService.GetSettings(terminologyProviderUri.Scheme);

			var interpretBankDataContext = new InterpretBankDataContext();
			interpretBankDataContext.Setup(settings.DatabaseFilepath);

			var termSearchService = new TerminologyService.TerminologyService(interpretBankDataContext);

			var interpretBankProvider = new InterpretBankProvider(termSearchService, settings);

			return interpretBankProvider;
		}

		public bool SupportsTerminologyProviderUri(Uri terminologyProviderUri) =>
			PersistenceService.PersistenceService.GetSettings(terminologyProviderUri.Scheme) is not null;
	}
}