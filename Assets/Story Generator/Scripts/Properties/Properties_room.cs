using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace StoryGenerator.RoomProperties
{

	public class Properties_room : MonoBehaviour
	{
		public string roomName;
		public Bounds bounds;

		// Use Awake since we need the values almost right away.
		void Awake ()
		{
			roomName = ParseName();
			bounds = CreateBounds();
		}
		
		string ParseName()
		{
			// const string PATTERN = @"PRE_ROO_(\w)+_\d{2}";
			const string PATTERN = @"PRE_ROO_(\w+)_\d{2}";
			Regex regex = new Regex(PATTERN);
			Match match = regex.Match(name);

			if (match.Success)
			{
				return match.Groups[1].Value;
			}
			else
			{
				// Debug.LogWarning("Failed to extract room name from " + name + ". Using object name as room name.");
				return name;
			}
		}

		Bounds CreateBounds()
		{
			OcclusionArea oa = GetComponent<OcclusionArea> ();
			// All rooms must have OcclusionArea component for correct occlusion culling
			// for non-static objects.
			Debug.Assert(oa != null);
			Vector3 center = transform.TransformPoint(oa.center);

			return new Bounds(center, oa.size);
		}
	}

}