using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;
using ps2ls.Forms;
using ps2ls.Graphics.Materials;

namespace ps2ls.Assets.Pack
{
    class AssetManager
    {
        #region Singleton
        private static AssetManager instance = null;

        public static void CreateInstance()
        {
            instance = new AssetManager();
        }

        public static void DeleteInstance()
        {
            instance = null;
        }

        public static AssetManager Instance { get { return instance; } }
        #endregion

        public List<Pack> Packs { get; private set; }
        public Dictionary<Asset.Types, List<Asset>> AssetsByType { get; private set; }

        // Internal cache to check whether a pack has already been loaded
        private Dictionary<Int32, Pack> packLookupCache = new Dictionary<Int32, Pack>();

        private GenericLoadingForm loadingForm;
        private BackgroundWorker loadBackgroundWorker;
        private BackgroundWorker extractAllBackgroundWorker;
        private BackgroundWorker extractSelectionBackgroundWorker;

        private AssetManager()
        {
            Packs = new List<Pack>();
            AssetsByType = new Dictionary<Asset.Types, List<Asset>>();

            loadBackgroundWorker = new BackgroundWorker();
            loadBackgroundWorker.WorkerReportsProgress = true;
            loadBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(loadProgressChanged);
            loadBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(loadRunWorkerCompleted);
            loadBackgroundWorker.DoWork += new DoWorkEventHandler(loadDoWork);

            extractAllBackgroundWorker = new BackgroundWorker();
            extractAllBackgroundWorker.WorkerReportsProgress = true;
            extractAllBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(extractAllProgressChanged);
            extractAllBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(extractAllRunWorkerCompleted);
            extractAllBackgroundWorker.DoWork += new DoWorkEventHandler(extractAllDoWork);

            extractSelectionBackgroundWorker = new BackgroundWorker();
            extractSelectionBackgroundWorker.WorkerReportsProgress = true;
            extractSelectionBackgroundWorker.ProgressChanged += new ProgressChangedEventHandler(extractSelectionProgressChanged);
            extractSelectionBackgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(extractSelectionRunWorkerCompleted);
            extractSelectionBackgroundWorker.DoWork += new DoWorkEventHandler(extractSelectionDoWork);
        }

        public void LoadBinaryFromDirectory(string directory)
        {
            IEnumerable<string> files = Directory.EnumerateFiles(Properties.Settings.Default.AssetDirectory, "*.pack", SearchOption.TopDirectoryOnly);

            LoadBinaryFromPaths(files);
        }

        public void LoadBinaryFromPaths(IEnumerable<string> paths)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            loadBackgroundWorker.RunWorkerAsync(paths);
        }

        private void loadRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();

            if (AssetBrowser.Instance != null)
            {
                AssetBrowser.Instance.RefreshPacksListBox();
            }

            if (ModelBrowser.Instance != null)
            {
                ModelBrowser.Instance.Refresh();
            }

            if (MaterialBrowser.Instance != null)
            {
                MaterialBrowser.Instance.Refresh();
            }
        }

        private void loadProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void loadDoWork(object sender, DoWorkEventArgs args)
        {
            loadBinaryFromPaths(sender, args.Argument);
        }

        private void loadBinaryFromPaths(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            IEnumerable<string> paths = (IEnumerable<string>)arg;

            for (Int32 i = 0; i < paths.Count(); ++i)
            {
                String path = paths.ElementAt(i);
                Pack pack = null;

                if (packLookupCache.TryGetValue(path.GetHashCode(), out pack) == false)
                {
                    pack = Pack.LoadBinary(path);

                    if (pack != null)
                    {
                        packLookupCache.Add(path.GetHashCode(), pack);
                        Packs.Add(pack);

                        foreach (Asset asset in pack.Assets)
                        {
                            if (false == AssetsByType.ContainsKey(asset.Type))
                            {
                                AssetsByType.Add(asset.Type, new List<Asset>());
                            }

                            AssetsByType[asset.Type].Add(asset);
                        }
                    }
                }

                Single percent = (Single)(i + 1) / (Single)paths.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(path));
            }
        }

        public void ExtractAllToDirectory(string directory)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            extractAllBackgroundWorker.RunWorkerAsync(directory);
        }

        private void extractAllToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            String directory = String.Empty;

            try
            {
                directory = (String)arg;
            }
            catch (InvalidCastException) { return; }

            for (Int32 i = 0; i < Packs.Count; ++i)
            {
                Pack pack = Packs.ElementAt(i);

                pack.ExtractAllAssetsToDirectory(directory);

                Single percent = (Single)(i + 1) / (Single)Packs.Count;
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(pack.Path));
            }
        }

        private void extractAllRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();
        }

        private void extractAllProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void extractAllDoWork(object sender, DoWorkEventArgs args)
        {
            extractAllToDirectory(sender, args.Argument);
        }

        public void ExtractAssetsByNamesToDirectory(IEnumerable<String> names, String directory)
        {
            foreach (Pack pack in Packs)
            {
                foreach (String name in names)
                {
                    pack.ExtractAssetsByNameToDirectory(names, directory);
                }
            }
        }

        public void ExtractByAssetsToDirectoryAsync(IEnumerable<Asset> assets, string directory)
        {
            loadingForm = new GenericLoadingForm();
            loadingForm.Show();

            object[] args = new object[] { assets, directory };

            extractSelectionBackgroundWorker.RunWorkerAsync(args);
        }

        private void extractByAssetsToDirectory(object sender, object arg)
        {
            BackgroundWorker backgroundWorker = (BackgroundWorker)sender;
            object[] args = (object[])arg;
            IEnumerable<Asset> assets = (IEnumerable<Asset>)args[0];
            String directory = (String)args[1];

            for (Int32 i = 0; i < assets.Count(); ++i)
            {
                Asset file = assets.ElementAt(i);

                file.Pack.ExtractAssetByNameToDirectory(file.Name, directory);

                Single percent = (Single)(i + 1) / (Single)assets.Count();
                backgroundWorker.ReportProgress((Int32)(percent * 100.0f), System.IO.Path.GetFileName(file.Name));
            }
        }

        private void extractSelectionRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs args)
        {
            loadingForm.Close();
        }

        private void extractSelectionProgressChanged(object sender, ProgressChangedEventArgs args)
        {
            loadingForm.SetProgressBarPercent(args.ProgressPercentage);
            loadingForm.SetLabelText((String)args.UserState);
        }

        private void extractSelectionDoWork(object sender, DoWorkEventArgs args)
        {
            extractByAssetsToDirectory(sender, args.Argument);
        }

        public MemoryStream CreateAssetMemoryStreamByName(String name)
        {
            MemoryStream memoryStream = null;

            foreach (Pack pack in Packs)
            {
                memoryStream = pack.CreateAssetMemoryStreamByName(name);

                if (memoryStream != null)
                {
                    break;
                }
            }

            return memoryStream;
        }
    }
}
