using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MonoGamePlus.Resources;
public abstract class ResourceManager<TResource>
{
    private readonly bool lazy;

    private const string rootContentFolder = "Content";

    private readonly Dictionary<string, TResource> resources = new();

    private readonly string contentFolder;

    protected MGPGame Game { get; private set; }

    public TResource this[string name]
    {
        get
        {
            if (lazy && !resources.ContainsKey(name))
            {
                string file = GetFile(name);
                TResource resource = Load(file, name);

                resources.Add(name, resource);
            }

            return resources[name];
        }
    }

    public ResourceManager(MGPGame game, string contentFolder, bool lazy = false)
    {
        Game = game;
        this.contentFolder = contentFolder;
        this.lazy = lazy;
    }

    internal void LoadAll()
    {
        if (lazy)
            return;

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

    protected virtual string GetFile(string name)
    {
        return Directory.GetFiles(
            Path.Combine(rootContentFolder, contentFolder),
            $"{name}.*",
            SearchOption.AllDirectories
         ).First();
    }

    public abstract TResource Load(string path, string name);
}
