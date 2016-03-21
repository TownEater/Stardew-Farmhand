﻿using Revolution.Attributes;
using Revolution.Events.Arguments;
using System;

namespace Revolution.Events
{
    public static class GameEvents
    {
        public static EventHandler<EventArgsOnGameInitialise> OnBeforeGameInitialised = delegate { };
        public static EventHandler<EventArgsOnGameInitialised> OnAfterGameInitialised = delegate { };
        public static event EventHandler OnBeforeLoadContent = delegate { };
        public static event EventHandler OnAfterLoadedContent = delegate { };
        public static event EventHandler OnBeforeUnoadContent = delegate { };
        public static event EventHandler OnAfterUnloadedContent = delegate { };
        public static event EventHandler OnBeforeUpdateTick = delegate { };
        public static event EventHandler OnAfterUpdateTick = delegate { };
        
        [Hook(HookType.Entry, "StardewValley.Game1", "Initialize")]
        internal static void InvokeBeforeGameInitialise()
        {
            OnBeforeGameInitialised.Invoke(null, new EventArgsOnGameInitialise());
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "Initialize")]
        internal static void InvokeAfterGameInitialise()
        {
            OnAfterGameInitialised.Invoke(null, new EventArgsOnGameInitialised());
        }

        [Hook(HookType.Entry, "StardewValley.Game1", "LoadContent")]
        internal static void InvokeBeforeLoadContent()
        {
            OnBeforeLoadContent.Invoke(null, EventArgs.Empty);
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "LoadContent")]
        internal static void InvokeAfterLoadedContent()
        {
            OnAfterLoadedContent.Invoke(null, EventArgs.Empty);
        }

        [Hook(HookType.Entry, "StardewValley.Game1", "UnloadContent")]
        internal static void InvokeBeforeUnloadContent()
        {
            OnBeforeUnoadContent.Invoke(null, EventArgs.Empty);
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "UnloadContent")]
        internal static void InvokeAfterUnloadedContent()
        {
            OnAfterUnloadedContent.Invoke(null, EventArgs.Empty);
        }

        [Hook(HookType.Entry, "StardewValley.Game1", "Update")]
        internal static void InvokeBeforeUpdate()
        {
            OnBeforeUpdateTick.Invoke(null, EventArgs.Empty);
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "Update")]
        internal static void InvokeAfterUpdate()
        {
            OnAfterUpdateTick.Invoke(null, EventArgs.Empty);
        }
    }
}