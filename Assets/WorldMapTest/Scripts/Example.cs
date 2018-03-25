using System;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class Example : MonoBehaviour
{
	public TransformGesture m_gesture;
	public Transform m_map;
	public Vector2 m_limitMin;
	public Vector2 m_limitMax;
	public float m_scaleMin;
	public float m_scaleMax;
	private float m_scale = 1;

	private void OnEnable()
	{
		m_gesture.Transformed += OnTransformed;
	}

	private void OnDisable()
	{
		m_gesture.Transformed -= OnTransformed;
	}

	private void OnTransformed( object sender, EventArgs e )
	{
		Scaling( m_gesture.DeltaScale );
		Move( m_gesture.DeltaPosition );
	}

	private void Scaling( float deltaScale )
	{
		m_scale += deltaScale - 1;
		m_scale = Mathf.Clamp( m_scale, m_scaleMin, m_scaleMax );
		m_map.localScale = new Vector3( m_scale, m_scale, 1 );
	}

	private void Move( Vector3 deltaPosition )
	{
		var localPosition = m_map.localPosition;
		var t = Mathf.InverseLerp( m_scaleMin, m_scaleMax, m_scale );
		var lx = Mathf.Lerp( m_limitMin.x, m_limitMax.x, t );
		var ly = Mathf.Lerp( m_limitMin.y, m_limitMax.y, t );
		localPosition += new Vector3( deltaPosition.x, deltaPosition.y, 0 );
		localPosition.x = Mathf.Clamp( localPosition.x, -lx, lx );
		localPosition.y = Mathf.Clamp( localPosition.y, -ly, ly );
		m_map.localPosition = localPosition;
	}
}
