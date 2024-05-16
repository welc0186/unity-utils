using UnityEngine;
using System.Reflection;
using System.Collections.Generic;
using System;

// Note: use public fields as properties (easier with Unity's inspector)
public interface IComponentProperties
{
    Type ComponentType();
    PropertyInfo[] GetProperties();
}

public static class GameObjectExtensions
{
    public static void AddComponentProperties(this GameObject gameObject, IComponentProperties componentProperties)
    {
        var newComponent = gameObject.AddComponent(componentProperties.ComponentType());
        foreach(PropertyInfo copyProperty in componentProperties.GetProperties())
        {
            foreach(PropertyInfo newProperty in newComponent.GetType().GetProperties())
            {
                if(newProperty.Name.Equals(copyProperty.Name))
                {
                    newProperty.SetValue(newComponent, copyProperty.GetValue(componentProperties));
                }
            }
        }
    }

    public static void RemoveAddComponent(this GameObject gameObject, IComponentProperties componentProperties)
    {
        var component = gameObject.GetComponent(componentProperties.ComponentType());
        if (component != null)
            GameObject.Destroy(component);
        gameObject.AddComponentProperties(componentProperties);
    }

    public static void SetParent(this GameObject gameObject, Transform parent, bool worldPositionStays = true)
    {
        gameObject.transform.SetParent(parent, worldPositionStays);
    }

    public static void SetParent(this List<GameObject> gameObjects, Transform parent, bool worldPositionStays = true)
    {
        foreach(GameObject child in gameObjects)
        {
            child.SetParent(parent, worldPositionStays);
        }
    }

}
