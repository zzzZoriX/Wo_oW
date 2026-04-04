using System.Collections.Generic;

namespace KillTypes {
    public enum KillType {
        Ability,
        Default,
        Falling,
        Moving
    }

    public static class PlayerState {
        public static bool Moving;
        public static bool Falling;

        public static List<KillType> ConvertStateToKillTypes() {
            var ktypes = new List<KillType>();
            
            if(PlayerState.Falling)     ktypes.Add(KillType.Falling);
            if(PlayerState.Moving)      ktypes.Add(KillType.Moving);

            return ktypes;
        }
    }
}