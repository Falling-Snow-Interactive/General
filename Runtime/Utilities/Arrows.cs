using System;
using UnityEditor;
using UnityEngine;

namespace Fsi.General.Utilities
{
	public class Arrows : MonoBehaviour
	{
		[SerializeField]
		private ArrowDirection directions;
		
		[SerializeField]
		private float size = 1f;
		
		[Header("Colors")]
		
		[SerializeField]
		private Color forwardColor = Color.blue;
		
		[SerializeField]
		private Color rightColor = Color.red;
		
		[SerializeField]
		private Color upColor = Color.green;
		
		[SerializeField]
		private Color xColor = Color.blue;
		
		[SerializeField]
		private Color yColor = Color.red;
		
		[SerializeField]
		private Color zColor = Color.green;

		#if UNITY_EDITOR
		private void OnDrawGizmos()
		{
			if (directions.HasFlag(ArrowDirection.Forward))
			{
				Handles.color = forwardColor;
				DrawArrow(transform.position, transform.forward);
			}
			
			if (directions.HasFlag(ArrowDirection.Right))
			{
				Handles.color = rightColor;
				DrawArrow(transform.position, transform.right);
			}
			
			if (directions.HasFlag(ArrowDirection.Up))
			{
				Handles.color = upColor;
				DrawArrow(transform.position, transform.up);
			}
		}

		private void DrawArrow(Vector3 position, Vector3 forward)
		{
			Handles.DrawLine(position, position + forward * size, size);
			Handles.ConeHandleCap(-1, 
				position + forward * size, 
				Quaternion.LookRotation(forward), 
				size / 5f,
				EventType.Repaint);
		}
		#endif
	}

	[Flags]
	public enum ArrowDirection
	{
		None = 0,
		
		// Local Space
		Forward = 1 << 0,
		Right = 1 << 1,
		Up = 1 << 2,
		
		// World Space
		X = 1 << 3,
		Y = 1 << 4,
		Z = 1 << 5,
		
		Everything = ~0,
	}
}