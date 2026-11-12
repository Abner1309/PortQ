using System;
using UnityEngine;

public static class Theme
{
    private const string Key = "ColorPalette";
    private const string ContrastValue = "Contrast";

    private static ThemeConfig config;
    private static bool loaded;
    private static bool highContrast;

    public static event Action Changed;

    private static void Load()
    {
        if (loaded) return;
        config = Resources.Load<ThemeConfig>("ThemeConfig");
        highContrast = PlayerPrefs.GetString(Key, "Default") == ContrastValue;
        loaded = true;
    }

    public static bool HighContrast
    {
        get { Load(); return highContrast; }
    }

    public static ColorPalette Current
    {
        get
        {
            Load();
            if (config == null) return null;
            if (highContrast && config.contrastPalette != null) return config.contrastPalette;
            return config.defaultPalette;
        }
    }

    public static void SetHighContrast(bool value)
    {
        Load();
        if (highContrast == value) return;

        highContrast = value;
        PlayerPrefs.SetString(Key, value ? ContrastValue : "Default");
        PlayerPrefs.Save();
        Changed?.Invoke();
    }
}