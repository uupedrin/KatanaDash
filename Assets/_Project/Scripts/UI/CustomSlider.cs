using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CustomSlider : MonoBehaviour
{
	[SerializeField] int sliderIndex;
	
	VisualElement m_Root;
	VisualElement m_Slider;
	VisualElement m_Dragger;
	VisualElement m_Bar;
	VisualElement m_NewDragger;

	IEnumerator Start()
	{
		yield return new WaitForSeconds(.1f);
		m_Root = GetComponent<UIDocument>().rootVisualElement;
		m_Slider = m_Root.Query<Slider>("customSlider").AtIndex(sliderIndex);
		m_Dragger = m_Root.Query<VisualElement>("unity-dragger").AtIndex(sliderIndex);

		AddElements();
		
		m_Slider.RegisterCallback<ChangeEvent<float>>(SliderValueChanged);
		
		m_Slider.RegisterCallback<GeometryChangedEvent>(SliderInit);
		
	}
	
	void AddElements()
	{
		m_Bar = new VisualElement();
		m_Dragger.Add(m_Bar);
		m_Bar.name = "Bar";
		m_Bar.AddToClassList("bar");
		
		m_NewDragger = new VisualElement();
		m_Slider.Add(m_NewDragger);
		m_NewDragger.name = "NewDragger";
		m_NewDragger.AddToClassList("newdragger");
		m_NewDragger.pickingMode = PickingMode.Ignore;
	}
	
	void SliderValueChanged(ChangeEvent<float> value)
	{	
		Vector2 dist = new Vector2((m_NewDragger.layout.width - m_Dragger.layout.width) / 2, (m_NewDragger.layout.height - m_Dragger.layout.height) / 2);
		Vector2 pos = m_Dragger.parent.LocalToWorld(m_Dragger.transform.position);
		m_NewDragger.transform.position = m_NewDragger.parent.WorldToLocal(pos - dist);
	}
	
	void SliderInit(GeometryChangedEvent evt)
	{
		Vector2 dist = new Vector2((m_NewDragger.layout.width - m_Dragger.layout.width) / 2, (m_NewDragger.layout.height - m_Dragger.layout.height) / 2);
		Vector2 pos = m_Dragger.parent.LocalToWorld(m_Dragger.transform.position);
		m_NewDragger.transform.position = m_NewDragger.parent.WorldToLocal(pos - dist);
	}
}

