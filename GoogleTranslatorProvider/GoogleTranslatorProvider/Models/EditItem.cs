﻿using System.Xml.Serialization;

namespace GoogleCloudTranslationProvider.Models
{
	public class EditItem
	{
		public string FindText { get; set; }

		public string ReplaceText { get; set; }

		[XmlAttribute(AttributeName = "Enabled")]
		public bool Enabled { get; set; }

		[XmlAttribute(AttributeName = "EditItemType")]
		public EditItemType Type { get; set; }
	}
}