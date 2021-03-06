﻿using Farmhand.Attributes;
using System;
using Microsoft.Xna.Framework;
using Farmhand.Events.Arguments.GameEvents;

namespace Farmhand.Events
{
    /// <summary>
    /// Contains events relating to the main game state
    /// </summary>
    public static class GameEvents
    {
        public static event EventHandler<EventArgsOnGameInitialise> OnBeforeGameInitialised = delegate { };
        public static event EventHandler<EventArgsOnGameInitialised> OnAfterGameInitialised = delegate { };
        public static event EventHandler OnBeforeLoadContent = delegate { };
        public static event EventHandler OnAfterLoadedContent = delegate { };
        public static event EventHandler OnBeforeUnloadContent = delegate { };
        public static event EventHandler OnAfterUnloadedContent = delegate { };
        public static event EventHandler<EventArgsOnBeforeGameUpdate> OnBeforeUpdateTick = delegate { };
        public static event EventHandler OnAfterUpdateTick = delegate { };
        public static event EventHandler<EventArgsOnAfterGameLoaded> OnAfterGameLoaded = delegate { };
        public static event EventHandler OnHalfSecondTick = delegate { };

        [Hook(HookType.Entry, "StardewValley.Game1", "Initialize")]
        internal static void InvokeBeforeGameInitialise([ThisBind] object @this)
        {
            EventCommon.SafeInvoke(OnBeforeGameInitialised, @this, new EventArgsOnGameInitialise());
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "Initialize")]
        internal static void InvokeAfterGameInitialise([ThisBind] object @this)
        {
            EventCommon.SafeInvoke(OnAfterGameInitialised, @this, new EventArgsOnGameInitialised());
        }

        [Hook(HookType.Entry, "StardewValley.Game1", "LoadContent")]
        internal static void InvokeBeforeLoadContent([ThisBind] object @this)
        {
            EventCommon.SafeInvoke(OnBeforeLoadContent, @this);
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "LoadContent")]
        internal static void InvokeAfterLoadedContent([ThisBind] object @this)
        {
            EventCommon.SafeInvoke(OnAfterLoadedContent, @this);
        }
                
        [Hook(HookType.Entry, "StardewValley.Game1", "UnloadContent")]
        internal static void InvokeBeforeUnloadContent([ThisBind] object @this)
        {
            EventCommon.SafeInvoke(OnBeforeUnloadContent, @this);
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "UnloadContent")]
        internal static void InvokeAfterUnloadedContent([ThisBind] object @this)
        {
            EventCommon.SafeInvoke(OnAfterUnloadedContent, @this);
        }

        // Invoked by property watcher
        internal static void InvokeOnHalfSecondTick()
        {
            EventCommon.SafeInvoke(OnHalfSecondTick, null);
        }

        [Hook(HookType.Entry, "StardewValley.Game1", "Update")]
        internal static bool InvokeBeforeUpdate(
            [ThisBind] object @this, 
            [InputBind(typeof(GameTime), "gameTime")] GameTime gt)
        {
            return EventCommon.SafeCancellableInvoke(OnBeforeUpdateTick, @this, new EventArgsOnBeforeGameUpdate(gt));
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "Update")]
        internal static void InvokeAfterUpdate([ThisBind] object @this)
        {
            TimeEvents.DidShouldTimePassCheckThisFrame = false;
            EventCommon.SafeInvoke(OnAfterUpdateTick, @this);
        }

        [Hook(HookType.Exit, "StardewValley.Game1", "loadForNewGame")]
        internal static void InvokeAfterGameLoaded(
            [InputBind(typeof(bool), "loadedGame")] bool loadedGame)
        {
            EventCommon.SafeInvoke(OnAfterGameLoaded, null, new EventArgsOnAfterGameLoaded(loadedGame));
        }
    }
}
