﻿namespace Farmhand.Content.Injectors.NPCs
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Farmhand.API.NPCs;
    using Farmhand.Logging;

    using StardewValley;

    internal class DialogueLoader : IContentInjector
    {
        public List<string> DialoguesExceptions
            =>
                Directory.GetFiles($"{Game1.content.RootDirectory}\\Characters\\Dialogue")
                    .Select(file => file?.Replace("Content\\", string.Empty).Replace(".xnb", string.Empty))
                    .ToList();

        #region IContentInjector Members

        public bool IsLoader => true;

        public bool IsInjector => true;

        public bool HandlesAsset(Type type, string assetName)
        {
            var baseName = assetName.Replace("Characters\\Dialogue\\", string.Empty);
            return Npc.Npcs.ContainsKey(baseName);
        }

        public T Load<T>(ContentManager contentManager, string assetName)
        {
            var baseName = assetName.Replace("Characters\\Dialogue\\", string.Empty);
            var dialogues = Npc.Npcs[baseName].Item1.Dialogues.BuildBaseDialogues();

            return (T)Convert.ChangeType(dialogues, typeof(T));
        }

        public void Inject<T>(T obj, string assetName, ref object output)
        {
            Log.Error("You shouldn't be here!");
        }

        #endregion
    }
}