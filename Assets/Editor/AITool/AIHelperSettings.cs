using UnityEngine;
using UnityEditor;

namespace AIHelper {

[FilePath("UserSettings/AIHelperSettings.asset",
          FilePathAttribute.Location.ProjectFolder)]
public sealed class AIHelperSettings : ScriptableSingleton<AIHelperSettings>
{
    public string apiKey = null;
    public void Save() => Save(true);
    void OnDisable() => Save();
}

sealed class AIHelperSettingsProvider : SettingsProvider
{
    public AIHelperSettingsProvider()
      : base("Project/ChatAI", SettingsScope.Project) {}

    public override void OnGUI(string search)
    {
        var settings = AIHelperSettings.instance;
        var key = settings.apiKey;
        EditorGUI.BeginChangeCheck();
        key = EditorGUILayout.TextField("API Key", key);
        if (EditorGUI.EndChangeCheck())
        {
            settings.apiKey = key;
            settings.Save();
        }
    }

    [SettingsProvider]
    public static SettingsProvider CreateCustomSettingsProvider()
      => new AIHelperSettingsProvider();
}

} // namespace AIHelper
