using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
 
public class ObjectLockWindow : EditorWindow
{
	[MenuItem ("Window/Object Lock")]
	static void Init()
	{
		ObjectLockWindow lockWin = (ObjectLockWindow) EditorWindow.GetWindow(typeof(ObjectLockWindow));
		lockWin.title = "Object Lock";
	}
 
	bool doIgnoreLocked = false;
 
	void OnEnable()
	{
		doIgnoreLocked = false;
	}
 
	void OnSelectionChange()
	{
		Repaint();
	}
 
	void OnGUI()
	{
		GUILayout.BeginHorizontal();
 
		string hierarchyText = "Lock Selection";
		string soloText = "Lock Selected Only";
 
		bool selectionLocked = ObjectIsLocked(Selection.activeGameObject);
 
		if (selectionLocked)
		{
			hierarchyText = "Unlock Selection";
			soloText = "Unlock Selected Only";
		}
 
		bool doHierarchy = GUILayout.Button(hierarchyText);
		bool doSolo = GUILayout.Button(soloText);
 
		GUILayout.EndHorizontal();
 
		doIgnoreLocked = EditorGUIExtension.ToggleButton(doIgnoreLocked, "Ignore Locked Objects");
 
		if (Selection.activeGameObject != null)
		{
			if (doHierarchy || doSolo)
			{
				foreach (GameObject targetObject in Selection.gameObjects)
				{
					if (selectionLocked)
						UnlockObject(targetObject, doHierarchy);
					else
						LockObject(targetObject, doHierarchy);
				}
			}
		}
	}
 
	void Update()
	{
		if (Selection.activeGameObject != null && doIgnoreLocked)
		{
			List<Object> newSelectedObjects = new List<Object>();
 
			foreach (Object testObject in Selection.objects)
			{
				bool doAdd = true;
 
				// Check if selected object is a game object and then if it is locked
				if (testObject is GameObject)
				{
					if (ObjectIsLocked(testObject as GameObject))
						doAdd = false;
				}
 
				if (doAdd)
					newSelectedObjects.Add(testObject);
			}
 
			Selection.objects = newSelectedObjects.ToArray();
		}
	}
 
	void LockObject(GameObject targetObject, bool recursive)
	{
		targetObject.hideFlags = targetObject.hideFlags | HideFlags.NotEditable;
		if (recursive)
		{
			foreach (Transform child in targetObject.transform)
				LockObject(child.gameObject, true);
		}
	}
 
	void UnlockObject(GameObject targetObject, bool recursive)
	{
		targetObject.hideFlags = targetObject.hideFlags & ~HideFlags.NotEditable;
		if (recursive)
		{
			foreach (Transform child in targetObject.transform)
				UnlockObject(child.gameObject, true);
		}
	}
 
	bool ObjectIsLocked(GameObject testObject)
	{
		if (testObject == null)
			return false;
		return ((int) testObject.hideFlags & (int)HideFlags.NotEditable) != 0;
	}
}