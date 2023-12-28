using GameEngine.Constants;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Handlers
{
    public static class TextureHandler
    {
        private static readonly ContentManager _content = Globals.Globals.ContentManager;

        private static readonly Dictionary<string, Texture2D> Textures = new();
        private static readonly Dictionary<string, List<Texture2D>> TextureGroups = new();

        public static void Load(string name, string path)
        {
            Textures.Add(name, _content.Load<Texture2D>(path));
        }

        public static void LoadGroup(string name, string path)
        {
            DirectoryInfo dir = new DirectoryInfo(_content.RootDirectory + "/" + path);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            FileInfo[] files = dir.GetFiles("*.*");

            TextureGroups.Add(name, new List<Texture2D>());

            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                Textures.Add(key, _content.Load<Texture2D>(path + "/" + key));
                TextureGroups[name].Add(_content.Load<Texture2D>(path + "/" + key));
            }
        }

        public static Texture2D Get(string name)
        {
            return Textures[name];
        }

        public static List<Texture2D> GetGroup(string name)
        {
            return TextureGroups[name];
        }
    }
}
