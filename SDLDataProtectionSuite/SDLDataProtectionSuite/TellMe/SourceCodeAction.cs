﻿using System.Diagnostics;
using System.Drawing;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SdlDataProtectionSuite.TellMe
{
	internal class SourceCodeAction : AbstractTellMeAction
	{
		public SourceCodeAction()
		{
			Name = string.Format("{0} Source Code", PluginResources.Plugin_Name);
		}

		public override string Category => string.Format(PluginResources.TellMe_Provider_Results, PluginResources.Plugin_Name);
		public override Icon Icon => PluginResources.SourceCode;
		public override bool IsAvailable => true;

		public override void Execute()
		{
			Process.Start("https://github.com/RWS/Sdl-Community/tree/master/SDLDataProtectionSuite");
		}
	}
}