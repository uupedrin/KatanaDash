using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuEventHandler : MonoBehaviour
{
	Button _playButton;
	Button _settingsButton;
	Button _achievementsButton;
	Button _creditsButton;
	Button[] _returnButton;
	
	private void Start()
	{
		VisualElement root = GetComponent<UIDocument>().rootVisualElement;
		
		_playButton = root.Q<Button>("btnPlay");
		_playButton.RegisterCallback<ClickEvent>(OnPlayButtonClicked);
		
		_settingsButton = root.Q<Button>("btnSettings");
		_settingsButton.RegisterCallback<ClickEvent>(OnSettingsButtonClicked);
		
		_achievementsButton = root.Q<Button>("btnAchievements");
		_achievementsButton.RegisterCallback<ClickEvent>(OnAchievementsButtonClicked);
		
		_creditsButton = root.Q<Button>("btnCredits");
		_creditsButton.RegisterCallback<ClickEvent>(OnCreditsButtonClicked);
	}
	
	private void OnPlayButtonClicked(ClickEvent e)
	{
		Debug.Log("Play!");
	}
	private void OnSettingsButtonClicked(ClickEvent e)
	{
		Debug.Log("Settings!");
	}
	private void OnAchievementsButtonClicked(ClickEvent e)
	{
		Debug.Log("Achievements!");
	}
	private void OnCreditsButtonClicked(ClickEvent e)
	{
		Debug.Log("Credits!");
	}
}
