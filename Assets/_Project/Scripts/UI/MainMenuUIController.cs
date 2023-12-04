using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{	
	// Main elements
	UIDocument _doc;
	TemplateContainer _mainMenuUXML;
	TemplateContainer _playMenuUXML;
	
	[SerializeField] StyleSheet[] styleSheets;
	
	Button _playButton;
	VisualElement _overlayContainer;
	
	Button _settingsButton, _settingsReturnButton;
	VisualElement _settingsModal;
	
	Button _achievementsButton, _achievementsReturnButton;
	VisualElement _achievementsModal;
	
	Button _creditsButton, _creditsReturnButton;
	VisualElement _creditsModal;
	
	// Play elements
	VisualElement _transit;
	Button _playReturnButton, _newGameButton, _continueButton;
	
	
	private void Start()
	{
		_mainMenuUXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_Project/UI/UXMLs/MainMenu.uxml").CloneTree();
		_mainMenuUXML.AddToClassList("cover");
		_playMenuUXML = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/_Project/UI/UXMLs/PlayMenu.uxml").CloneTree();
		_playMenuUXML.AddToClassList("cover");
		_doc = GetComponent<UIDocument>();
		
		_doc.rootVisualElement.Clear();
		_doc.rootVisualElement.Add(_mainMenuUXML);
		
		foreach (StyleSheet ss in styleSheets)
		{
			_doc.rootVisualElement.styleSheets.Add(ss);
		}
		
		GetReferences();
		RegisterEvents();
		
	}
	
	private void GetReferences()
	{
		
		
		_playButton = RQ<Button>("btnPlay");
		_overlayContainer = RQ<VisualElement>("playTransition");
		
		
		_settingsButton = RQ<Button>("btnSettings");
		_settingsModal = RQ<VisualElement>("settingsModal");
		_settingsReturnButton = RQ<Button>("settingsReturnButton");
		
		_achievementsButton = RQ<Button>("btnAchievements");
		_achievementsModal = RQ<VisualElement>("achievementsModal");
		_achievementsReturnButton = RQ<Button>("achievementsReturnButton");
		
		_creditsButton = RQ<Button>("btnCredits");
		_creditsModal = RQ<VisualElement>("creditsModal");
		_creditsReturnButton = RQ<Button>("creditsReturnButton");
		
	}
	
	private void RegisterEvents()
	{
		_playButton.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
		_settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
		_settingsReturnButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
		_achievementsButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClicked);
		_achievementsReturnButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClicked);
		_creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClicked);
		_creditsReturnButton.RegisterCallback<ClickEvent>(OnCreditsButtonClicked);
		
		_overlayContainer.RegisterCallback<TransitionEndEvent>(OnPlayTransitionEnd);
	}
	
	private void OnPlayButtonClicked(ClickEvent e)
	{
		_overlayContainer.ToggleInClassList("playClosed");
	}
	
	private void OnPlayTransitionEnd(TransitionEndEvent e)
	{
		if(!_overlayContainer.ClassListContains("playClosed"))
		{
			StartCoroutine(ChangeUXML());
		}
	}
	
	private IEnumerator ChangeUXML()
	{
		VisualElement root = _doc.rootVisualElement;
		root.Clear();
		root.Add(_playMenuUXML);
		GetPlayReferences();
		yield return new WaitForSeconds(.2f);
		_transit.AddToClassList("transition--off");
		yield return new WaitForSeconds(.4f);
		_transit.style.display = DisplayStyle.None;
		
	}
	
	private void GetPlayReferences()
	{
		_transit = RQ<VisualElement>("transition");
		_playReturnButton = RQ<Button>("playReturnButton");
		_newGameButton = RQ<Button>("newGame");
		_continueButton = RQ<Button>("continue");
		
		
		//Events
		_playReturnButton.RegisterCallback<ClickEvent>(OnPlayReturnPress);
		_newGameButton.RegisterCallback<ClickEvent>(OnNewGamePress);
		_continueButton.RegisterCallback<ClickEvent>(OnContinuePress);
	}
	
	private void OnSettingsButtonClicked(ClickEvent e)
	{
		_settingsModal.ToggleInClassList("modal-div--closed");
	}
	
	private void OnAchievementsButtonClicked(ClickEvent e)
	{
		_achievementsModal.ToggleInClassList("modal-div--closed");
	}
	
	private void OnCreditsButtonClicked(ClickEvent e)
	{
		_creditsModal.ToggleInClassList("modal-div--closed");
	}
	
	private void OnPlayReturnPress(ClickEvent e)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
	private void OnNewGamePress(ClickEvent e)
	{
		SceneManager.LoadScene("Game");
		//GameManager.manager.Achievements();
	}
	private void OnContinuePress(ClickEvent e)
	{
		Debug.Log("Continue");
	}
	
	private T RQ<T>(string elementName) where T : VisualElement
	{
		VisualElement root = _doc.rootVisualElement;
		return root.Q<T>(elementName);
	}
}