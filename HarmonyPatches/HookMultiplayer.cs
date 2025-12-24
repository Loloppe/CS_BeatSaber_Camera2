using Camera2.Managers;
using HarmonyLib;

namespace Camera2.HarmonyPatches {
	[HarmonyPatch(typeof(MultiplayerSessionManager<NetworkMessageType, BeatSaberConnectedPlayerManager, IBeatSaberConnectedPlayer, BeatSaberConnectedPlayer, BeatSaberPlayerIdentityPacketData>), "UpdateConnectionState")]
	static class HookMultiplayer {
		private static MultiplayerSessionManager<NetworkMessageType, BeatSaberConnectedPlayerManager, IBeatSaberConnectedPlayer, BeatSaberConnectedPlayer, BeatSaberPlayerIdentityPacketData> _instance;
		public static MultiplayerSessionManager<NetworkMessageType, BeatSaberConnectedPlayerManager, IBeatSaberConnectedPlayer, BeatSaberConnectedPlayer, BeatSaberPlayerIdentityPacketData> instance => _instance == null ? null : _instance;
		static void Postfix(MultiplayerSessionManager<NetworkMessageType, BeatSaberConnectedPlayerManager, IBeatSaberConnectedPlayer, BeatSaberConnectedPlayer, BeatSaberPlayerIdentityPacketData> __instance) {
#if DEBUG
			Plugin.Log.Info($"Multiplayer connection state changed. Connected: {__instance.isConnected}");
#endif
			_instance = __instance;
			ScenesManager.ActiveSceneChanged();
		}
	}

	[HarmonyPatch(typeof(MultiplayerSpectatorController), nameof(MultiplayerSpectatorController.Start))]
	static class HookMultiplayerSpectatorController {
		private static MultiplayerSpectatorController _instance;
		public static MultiplayerSpectatorController instance => _instance == null || !_instance.isActiveAndEnabled ? null : _instance;
		static void Postfix(MultiplayerSpectatorController __instance) {
#if DEBUG
			Plugin.Log.Info($"MultiplayerSpectatorController.Start()");
#endif
			_instance = __instance;
			ScenesManager.ActiveSceneChanged();
		}
	}
}
