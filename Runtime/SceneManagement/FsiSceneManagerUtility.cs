using Fsi.General.SceneManagement.Settings;

namespace Fsi.General.SceneManagement
{
	public static class FsiSceneManagerUtility
	{
		private static FsiSceneManagerSettings settings;
		public static FsiSceneManagerSettings Settings => settings ??= FsiSceneManagerSettings.GetOrCreateSettings();
	}
}