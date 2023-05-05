using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoGamePlus.Resources;
public abstract class ResourceManager<TResource>
{
    private const string rootContentFolder = "Content";

    private readonly Dictionary<string, TResource> resources = new();

    private readonly string contentFolder;

    protected MGPGame Game { get; private set; }

    public TResource this[string name] => resources[name];

    public ResourceManager(MGPGame game, string contentFolder)
    {
        Game = game;
        this.contentFolder = contentFolder;
    }

    internal void LoadAll()
    {
        string[] files = Directory.GetFiles(
            Path.Combine(rootContentFolder, contentFolder),
            "*",
            SearchOption.AllDirectories);

        foreach (string file in files)
        {
            string name = file.Split('/', '\\').Last().Split('.').First();
            TResource resource = Load(file, name);

            resources.Add(name, resource);
        }

    }

    public abstract TResource Load(string path, string name);
}
