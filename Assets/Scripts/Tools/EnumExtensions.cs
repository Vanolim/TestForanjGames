using System;

public static class EnumExtensions
{
    public static T GetRandomEnumValue<T>() where T : Enum
    {
        Array enumComponents = Enum.GetValues(typeof(T));
        return (T)enumComponents.GetValue(UnityEngine.Random.Range(0, enumComponents.Length));
    }
}