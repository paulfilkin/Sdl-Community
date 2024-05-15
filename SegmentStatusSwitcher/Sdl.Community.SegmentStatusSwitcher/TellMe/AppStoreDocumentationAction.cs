﻿using System.Diagnostics;
using System.Drawing;
using Sdl.TellMe.ProviderApi;

namespace Sdl.Community.SegmentStatusSwitcher.TellMe
{
	public class AppStoreDocumentationAction : AbstractTellMeAction
	{
		public override bool IsAvailable => true;

		public override string Category => string.Format(PluginResources.TellMe_Provider_Results, PluginResources.Plugin_Name);

		public override Icon Icon => PluginResources.TellmeDocumentation;

		public AppStoreDocumentationAction()
        {
            Name = string.Format("{0} documentation", PluginResources.Plugin_Name);
        }

		public override void Execute()
		{
			Process.Start("https://appstore.rws.com/Plugin/44?tab=documentation");
		}
	}
}