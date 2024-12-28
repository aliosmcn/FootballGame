using System;
using Action = Unity.Android.Gradle.Manifest.Action;

namespace Scripts.Events
{
    public static class Events
    {
        
        #region GeneralEvents
        
        public static Action GameStarted;
        public static Action GameEnded;

        #endregion GeneralEvents
        
        #region Score Events

        public static Action<int> OnLeftScoreChanged;
        public static Action<int> OnRightScoreChanged;

        #endregion Score Events

    }
}