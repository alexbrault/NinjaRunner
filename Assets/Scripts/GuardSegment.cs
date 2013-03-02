using UnityEngine;
using System.Collections;

public class GuardSegment : MonoBehaviour {
	private bool IsVisible;
	private static GuardSegment _instance;
	public static GuardSegment Instance {
		get { return _instance; }
	}
	
	private void Start() {
		if (_instance == null) {
			_instance = this;
		}
	}
	
	private void Destroy() {
		if (_instance == this) {
			_instance = null;
		}
	}
	private void Update() {
		if (IsVisible) {
			var width = SegmentLoader.Instance.AddSegmentToLevel();
			transform.Translate(width, 0, 0);
		}
	}
	
	private void OnBecameInvisible() {
		IsVisible = false;
	}
	
	private void OnBecameVisible() {
		IsVisible = true;
	}
}
