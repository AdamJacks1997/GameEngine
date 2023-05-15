using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace GameEngine.Handlers
{
    public class TextureHandler
    {
        private readonly ContentManager _content;

        private static readonly Dictionary<string, Texture2D> Textures = new();
        private static readonly Dictionary<string, HashSet<Texture2D>> TextureGroups = new();

        public TextureHandler(ContentManager content)
        {
            _content = content;
        }

        public void Load(string name)
        {
            Textures.Add(name, _content.Load<Texture2D>(name));
        }

        public void LoadGroup(string name, string route)
        {
            DirectoryInfo dir = new DirectoryInfo(_content.RootDirectory + "/" + route);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException();
            }

            FileInfo[] files = dir.GetFiles("*.*");

            TextureGroups.Add(name, new HashSet<Texture2D>());

            foreach (FileInfo file in files)
            {
                string key = Path.GetFileNameWithoutExtension(file.Name);

                Textures.Add(key, _content.Load<Texture2D>(route + "/" + key));
                TextureGroups[name].Add(_content.Load<Texture2D>(route + "/" + key));
            }
        }

        public Texture2D Get(string name)
        {
            return Textures[name];
        }

        public HashSet<Texture2D> GetGroup(string name)
        {
            return TextureGroups[name];
        }
    }
}
