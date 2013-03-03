using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class InputEx {
	private static Dictionary<string, KeyCode> Keys;
	
	static InputEx() {
		Keys = new Dictionary<string, KeyCode>();
		Keys["P1Left"] = KeyCode.A;
		Keys["P1Right"] = KeyCode.D;
		Keys["P1Jump"] = KeyCode.Space;
		Keys["P1Shoot"] = KeyCode.F;
		Keys["P1Grappling"] = KeyCode.G;
		Keys["P2Left"] = KeyCode.LeftArrow;
		Keys["P2Right"] = KeyCode.RightArrow;
		Keys["P2Jump"] = KeyCode.Keypad0;
		Keys["P2Shoot"] = KeyCode.Keypad1;
		Keys["P2Grappling"] = KeyCode.Keypad2;
		Keys["null"] = KeyCode.None;
	}
	
	public static bool GetButtonDown(string buttonName) {
		if (Keys.ContainsKey(buttonName) && Keys[buttonName] != KeyCode.None) {
			return Input.GetKeyDown(Keys[buttonName]);
		}
		
		return false;
	}
	
	public static bool GetButton(string buttonName) {
		if (Keys.ContainsKey(buttonName) && Keys[buttonName] != KeyCode.None) {
			return Input.GetKey(Keys[buttonName]);
		}
		
		return false;
	}
	
	public static bool GetButtonDownUp(string buttonName) {
		if (Keys.ContainsKey(buttonName) && Keys[buttonName] != KeyCode.None) {
			return Input.GetKeyUp(Keys[buttonName]);
		}
		
		return false;
	}
}
