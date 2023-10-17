using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuScreen : MonoBehaviour
{
	[SerializeField] UIDocument _document;
	[SerializeField] StyleSheet _styleSheet;
	
	[Header("Elements")]
	[SerializeField] Sprite imageLogo;
	
	[SerializeField] Sprite playImage;
	[SerializeField] Sprite settingsImage;
	[SerializeField] Sprite quitImage;
	
	public static event Action PlayButtonClick;
	public static event Action SettingsButtonClick;
	public static event Action QuitButtonClick;
	
	
	void Start()
	{
		StartCoroutine(Generate());
	}
	
	void OnValidate()
	{
		if(Application.isPlaying) return;
		StartCoroutine(Generate());
	}
	
	
	IEnumerator Generate()
	{
		yield return null;
		var root = _document.rootVisualElement;
		root.Clear();
		
		root.styleSheets.Add(_styleSheet);
		
		var logoContainer = Create("logo-container");
		var image = Create<Image>("logo");
		image.sprite = imageLogo;
		logoContainer.Add(image);
		root.Add(logoContainer);
		
		var buttonContainer = Create("button-container");
		
		var playButton = Create<Button>("menu-button");
		var playDiv = Create<Image>("button-icon");
		playDiv.sprite = playImage;
		playButton.clicked += PlayButtonClick;
		playButton.Add(playDiv);
		
		var settingsButton = Create<Button>("menu-button");
		var settingsDiv = Create<Image>("button-icon");
		settingsDiv.sprite = settingsImage;
		settingsButton.clicked += SettingsButtonClick;
		settingsButton.Add(settingsDiv);
		
		var quitButton = Create<Button>("menu-button");
		var quitDiv = Create<Image>("button-icon");
		quitDiv.sprite = quitImage;
		quitButton.clicked += QuitButtonClick;
		quitButton.Add(quitDiv);
		
		buttonContainer.Add(playButton);
		buttonContainer.Add(settingsButton);
		buttonContainer.Add(quitButton);
		
		root.Add(buttonContainer);
		
		if (Application.isPlaying) // Play Animations
		{
			logoContainer.experimental.animation.Start(0,1,3000, (e, v) => e.style.opacity = new StyleFloat(v));
			buttonContainer.experimental.animation.Start(0,1,3000, (e, v) => e.style.opacity = new StyleFloat(v));
		}
	}

	
	VisualElement Create(params string[] classNames)
	{
		return Create<VisualElement>(classNames);
	}
	
	T Create<T>(params string[] classNames) where T : VisualElement, new()
	{
		var element = new T();
		foreach (string className in classNames)
		{
			element.AddToClassList(className);
		}
		return element;
	}
}
