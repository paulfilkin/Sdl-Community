﻿using System.Collections.Generic;
using System.Data.SQLite;
using InterpretBank.GlossaryService;
using InterpretBank.SettingsService;
using InterpretBank.Studio;
using InterpretBank.TerminologyService;
using InterpretBank.TerminologyService.Interface;
using InterpretBank.Wrappers.Interface;
using NSubstitute;
using Sdl.Terminology.TerminologyProvider.Core;
using Xunit;

namespace InterpretBankTests
{
	public class InterpretBankProviderTests
	{
		public InterpretBankProviderTests()
		{
			//var mockGenerator = new MockGenerator();
		}

		private ITerminologyService TermSearchService { get; }

		[Fact]
		public void SearchTermTest()
		{
			//var termSearchService = Substitute.For<ITermSearchService>();
			var language = Substitute.For<ILanguage>();

			//termSearchService
			//	.GetFuzzyTerms(default, default, default)
			//	.ReturnsForAnyArgs(new List<string> { "firstTerm", "secondTerm" });

			var openFileDialog = Substitute.For<IOpenFileDialog>();

			var filepath = "C:\\Code\\RWS Community\\InterpretBank\\InterpretBankTests\\Resources\\InterpretBankDatabaseV6.db";
			var sqLiteConnection = new SQLiteConnection($"Data Source={filepath}");

			var ibContext = new InterpretBankDataContext(sqLiteConnection);

			var termSearchService = new TerminologyService(ibContext);

			var sut = new InterpretBankProvider(termSearchService, new SettingsService(openFileDialog, ibContext));

			var results = sut.Search("This is a text", language, language, 100, SearchMode.Fuzzy, true);
		}
	}
}