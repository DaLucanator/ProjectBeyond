using UnityEngine;
//Assets/Scripts/0_Scripts/ReadOnlyAttribute
//Assets/Editor/ReadOnlyDrawer.cs
//these scripts add a ReadOnly property to variables for the Unity Editor
//full credit: https://www.patrykgalach.com/2020/01/20/readonly-attribute-in-unity-editor/

/// <summary>
/// Read Only attribute.
/// Attribute is use only to mark ReadOnly properties.
/// </summary
public class ReadOnlyAttribute : PropertyAttribute { }