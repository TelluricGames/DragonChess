using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;

public class Texts : MonoBehaviour {
	static SystemLanguage[] supportedLanguages = new SystemLanguage[] {
		SystemLanguage.English,
		SystemLanguage.Russian
	};
	public static SystemLanguage[] SupportedLanguages {
		get { return supportedLanguages; }
	}
	public static TextAsset locFile;

	static SystemLanguage language = SystemLanguage.English;
	public static SystemLanguage Language {
		get { return language; }
		set {
			bool supported = false;
			foreach (var lang in supportedLanguages) {
				if (lang == value) {
					language = value;
					supported = true;
				}
			}

			if (!supported)
				language = SystemLanguage.English;

			SetLanguageKey ();
		}
	}

	static string languageKey = "EN";
	static XmlDocument xmldoc;

	/*
	static Texts () {
		//var path = Path.Combine (Application.dataPath, "Localization.txt");
		var path = "Assets/Localization.txt";
		FileInfo fi = new FileInfo (path);
		if (!fi.Exists)
			throw new DragonChessException ("Localization file wasn't found");
		locFile = Resources.Load ("Localization.txt") as TextAsset;

		xmldoc = new XmlDocument ();
		//using (var fr = new FileStream(path, FileMode.Open, FileAccess.Read)) {
		using (var fr = new StringReader(locFile.text)) {
			xmldoc.Load (fr);
		}
	}
	*/

	void Awake () {
		Texts.Language = Application.systemLanguage;
		locFile = Resources.Load ("Localization") as TextAsset;

		xmldoc = new XmlDocument ();
		//using (var fr = new FileStream(path, FileMode.Open, FileAccess.Read)) {
		using (var fr = new StringReader(locFile.text)) {
			xmldoc.Load (fr);
		}
	}

	static void SetLanguageKey () {
		switch (language) {
		case SystemLanguage.English:
			languageKey = "EN";
			break;
		case SystemLanguage.Russian:
			languageKey = "RU";
			break;
		}
	}

	public static string GetString (string key) {
		var textKey = GetTextKey (key);

		foreach (XmlNode node in textKey.ChildNodes) {
			if (node.Name == languageKey) {
				return node.InnerText;
			}
		}

		throw new DragonChessException ("Required language key wasn't found in localization TextKey element document: " + languageKey);
	}

	static XmlNode GetTextKey (string key) {
		foreach (XmlNode node in xmldoc.DocumentElement.ChildNodes) {
			if (node.Name == "TextKey") {
				foreach (XmlAttribute attr in node.Attributes) {
					if (attr.Name == "name") {
						if (attr.Value == key) {
							return node;
						}
					}
				}
			}
		}

		throw new DragonChessException ("Required key wasn't found in localization document: " + key);
	}
}
