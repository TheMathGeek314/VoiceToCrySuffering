using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Modding;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using Satchel;
using SFCWavUtils = SFCore.Utils.WavUtils;

namespace VoiceToCrySuffering {
    public class VoiceToCrySuffering: Mod {
        new public string GetName() => "VoiceToCrySuffering";
        public override string GetVersion() => "1.0.0.1";
        public override void Initialize(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects) {
            On.PlayMakerFSM.OnEnable += editFSM;
        }

        private void editFSM(On.PlayMakerFSM.orig_OnEnable orig, PlayMakerFSM self) {
            orig(self);
            if(self.gameObject.name == "HK Prime" && self.FsmName == "Control") {
                FsmState roar = self.GetValidState("Intro Roar");
                AudioPlayerOneShotSingle yell = new();
                AudioPlayerOneShotSingle toCopy = roar.Actions[8] as AudioPlayerOneShotSingle;
                yell.audioPlayer = toCopy.audioPlayer;
                yell.spawnPoint = toCopy.spawnPoint;
                yell.pitchMin = yell.pitchMax = yell.volume = 1;
                yell.delay = 0;
                yell.audioClip = SFCWavUtils.ToAudioClip(Assembly.GetExecutingAssembly().GetManifestResourceStream("VoiceToCrySuffering.Sounds.Scream.wav"));
                yell.storePlayer = new();
                roar.InsertAction(yell, 0);
            }
        }
    }
}