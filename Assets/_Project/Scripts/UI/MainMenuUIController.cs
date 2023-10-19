using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUIController : MonoBehaviour
{
	Button _playButton;
	
	Button _settingsButton;
	VisualElement _settingsModal;
	Button _settingsReturnButton;
	
	Button _achievementsButton;
	VisualElement _achievementsModal;
	Button _achievementsReturnButton;
	Button _creditsButton;
	
	private void Start()
	{
		GetReferences();
		RegisterEvents();
	}
	
	private void GetReferences()
	{
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		
		_playButton = root.Q<Button>("btnPlay");
		
		_settingsButton = root.Q<Button>("btnSettings");
		_settingsModal = root.Q<VisualElement>("settingsModal");
		_settingsReturnButton = root.Q<Button>("settingsReturnButton");
		
		_achievementsButton = root.Q<Button>("btnAchievements");
		_achievementsModal = root.Q<VisualElement>("achievementsModal");
		_achievementsReturnButton = root.Q<Button>("achievementsReturnButton");
		
		_creditsButton = root.Q<Button>("btnCredits");
	}
	
	private void RegisterEvents()
	{
		_playButton.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
		_settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
		_settingsReturnButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
		_achievementsButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClicked);
		_achievementsReturnButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClicked);
		_creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClicked);
	}
	
	private void OnPlayButtonClicked(ClickEvent e)
	{
		Debug.Log("Play!");
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
		Debug.Log("Credits!");
	}
}
