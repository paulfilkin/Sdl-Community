﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Wordprocessing;
using InterpretBank.GlossaryService;
using InterpretBank.SettingsService.UI;
using Sdl.Desktop.IntegrationApi.Extensions;
using Sdl.Desktop.IntegrationApi;
using Sdl.TranslationStudioAutomation.IntegrationApi.Presentation.DefaultLocations;
using InterpretBank.SettingsService.ViewModel;
using InterpretBank.Wrappers;

namespace InterpretBank.Studio
{
	[RibbonGroup("InterpretBankRibbonGroup", Name = "InterpretBank")]
	[RibbonGroupLayout(LocationByType = typeof(TranslationStudioDefaultRibbonTabs.AddinsRibbonTabLocation))]
	public class InterpretBankRibbonGroup : AbstractRibbonGroup
	{
	}

	[Action("InterpretBankAction", Icon = "logo", Name = "InterpretBank database settings",
		Description = "InterpretBank terminology provider database settings")]
	[ActionLayout(typeof(InterpretBankRibbonGroup), 10, DisplayType.Large)]
	public class InterpretBankAction : AbstractAction
	{
		protected override void Execute()
		{
			var interpretBankDataContext = new InterpretBankDataContext();
			var glossarySetupViewModel = new GlossarySetupViewModel(new OpenFileDialog())
			{
				InterpretBankDataContext = interpretBankDataContext
			};
			var glossarySetup = new GlossarySetup { DataContext = glossarySetupViewModel };
			

			glossarySetup.ShowDialog();
		}
	}
}
