using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using SmiteAssistPortable;
using Parse;
using Windows.Storage;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace SmiteAssistWin8
{
    public static class GlobalFunctions
    {
        public static async void messageBox(string message)
        {
            MessageDialog dlg = new MessageDialog(message);
            await dlg.ShowAsync();
        }
        public static async void downloadImages(bool fullUpdate)
        {
            List<ParseObject> gods = UniversalData.godsList;
            var collision = CreationCollisionOption.FailIfExists;
            if (fullUpdate)
                collision = CreationCollisionOption.ReplaceExisting;
            List<string> filesInUse = new List<string>();
            StorageFolder godPortraits = await ApplicationData.Current.LocalFolder.CreateFolderAsync("god_portraits", CreationCollisionOption.OpenIfExists);
            
            //thumbnails
            for (int i = 0; i < gods.Count; i++)
            {
                StorageFile destinationFile;
                try
                {
                    destinationFile = await godPortraits.CreateFileAsync(Convert.ToString(gods[i]["GodId"]) + ".jpg", collision);
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("0x80070005"))
                    {
                        filesInUse.Add(Convert.ToString(gods[i]["GodId"]) + ".jpg");
                    }
                    continue;
                }
                try
                {
                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/god_portraits/" + Convert.ToString(gods[i]["GodId"]) + ".jpg"), destinationFile);
                    await download.StartAsync();
                    continue;
                }
                catch (Exception e)
                {
                    filesInUse.Add(Convert.ToString(gods[i]["GodId"]) + ".jpg");
                }
                await destinationFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }

            //cards
            for (int i = 0; i < gods.Count; i++)
            {
                StorageFile destinationFile;
                try
                {
                    destinationFile = await godPortraits.CreateFileAsync(Convert.ToString(gods[i]["GodId"]) + "c.jpg", collision);

                }
                catch (Exception e)
                {
                    if (e.Message.Contains("0x80070005"))
                    {
                        filesInUse.Add(Convert.ToString(gods[i]["GodId"]) + "c.jpg");
                    }
                    continue;
                }
                try
                {
                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/god_portraits/" + Convert.ToString(gods[i]["GodId"]) + "c.jpg"), destinationFile);
                    await download.StartAsync();
                    continue;
                }
                catch (Exception e)
                {
                    filesInUse.Add(Convert.ToString(gods[i]["GodId"]) + "c.jpg");
                }
                await destinationFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            //abilities
            StorageFolder abilities = await ApplicationData.Current.LocalFolder.CreateFolderAsync("abilities", CreationCollisionOption.OpenIfExists);
            for (int i = 0; i < gods.Count; i++)
            {
                for (int j = 1; j <= 5; j++)
                {
                    StorageFile destinationFile;
                    try
                    {
                        destinationFile = await abilities.CreateFileAsync(Convert.ToString(gods[i]["GodId"]) + "_" + Convert.ToString(gods[i]["AbilityId" + Convert.ToString(j)]) + ".jpg", collision);
                    }
                    catch (Exception e)
                    {
                        if (e.Message.Contains("0x80070005"))
                        {
                            filesInUse.Add(Convert.ToString(gods[i]["GodId"]) + "_" + Convert.ToString(gods[i]["AbilityId" + Convert.ToString(j)]) + ".jpg");
                        }
                        continue;
                    }
                    try
                    {
                        BackgroundDownloader downloader = new BackgroundDownloader();
                        DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/abilities/" + Convert.ToString(gods[i]["GodId"]) + "_" + Convert.ToString(gods[i]["AbilityId" + Convert.ToString(j)]) + ".jpg"), destinationFile);
                        await download.StartAsync();
                        continue;
                    }
                    catch (Exception e)
                    {
                        filesInUse.Add(Convert.ToString(gods[i]["GodId"]) + "_" + Convert.ToString(gods[i]["AbilityId" + Convert.ToString(j)]) + ".jpg");
                    }
                    await destinationFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
                }
            }
            //items
            StorageFolder itemsFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("items", CreationCollisionOption.OpenIfExists);
            List<ParseObject> items = UniversalData.itemsList;
            for (int i = 0; i < items.Count; i++)
            {
                StorageFile destinationFile;
                try
                {
                    destinationFile = await itemsFolder.CreateFileAsync(items[i]["Name"] + ".jpg", collision);
                }
                catch (Exception e)
                {
                    if (e.Message.Contains("0x80070005"))
                    {
                        filesInUse.Add("i"+items[i]["Name"] + ".jpg");
                    }
                    continue;
                }
                try
                {
                    BackgroundDownloader downloader = new BackgroundDownloader();
                    DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/items/" + items[i]["Name"] + ".jpg"), destinationFile);
                    await download.StartAsync();
                    continue;
                }
                catch (Exception e)
                {
                    filesInUse.Add("i" + items[i]["Name"] + ".jpg");
                }
                await destinationFile.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
            //download failed files
            StorageFolder miscFolder = await ApplicationData.Current.LocalFolder.CreateFolderAsync("misc", CreationCollisionOption.OpenIfExists);
            StorageFile failedImagesFile=await miscFolder.CreateFileAsync("failedImages.txt", CreationCollisionOption.OpenIfExists);
            IList<string> data = await FileIO.ReadLinesAsync(failedImagesFile);
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].Contains("i"))
                {
                    try
                    {
                        StorageFile failedItem = await itemsFolder.CreateFileAsync(data[i].Substring(1), CreationCollisionOption.ReplaceExisting);
                        BackgroundDownloader downloader = new BackgroundDownloader();
                        DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/items/" + data[i].Substring(1)), failedItem);
                        await download.StartAsync();
                    }
                    catch (Exception e)
                    {
                        if (!filesInUse.Contains(data[i]))
                        {
                            filesInUse.Add(data[i]);
                        }
                    }
                }
                else if (data[i].Contains("_"))
                {
                    try
                    {
                        StorageFile failedSkill = await abilities.CreateFileAsync(data[i], CreationCollisionOption.ReplaceExisting);
                        BackgroundDownloader downloader = new BackgroundDownloader();
                        DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/abilities/" + data[i]), failedSkill);
                        await download.StartAsync();
                    }
                    catch (Exception e)
                    {
                        if (!filesInUse.Contains(data[i]))
                        {
                            filesInUse.Add(data[i]);
                        }
                    }
                }
                else
                {
                    try
                    {
                        StorageFile failedPortrait = await godPortraits.CreateFileAsync(data[i], CreationCollisionOption.ReplaceExisting);
                        BackgroundDownloader downloader = new BackgroundDownloader();
                        DownloadOperation download = downloader.CreateDownload(new Uri("https://dl.dropboxusercontent.com/u/113462511/smite_icons/god_portraits/" + data[i]), failedPortrait);
                        await download.StartAsync();
                    }
                    catch (Exception e)
                    {
                        if (!filesInUse.Contains(data[i]))
                        {
                            filesInUse.Add(data[i]);
                        }
                    }
                }
            }
            if (filesInUse != null && filesInUse.Count > 0)
            {
                await FileIO.WriteLinesAsync(failedImagesFile, filesInUse);
            }
            filesInUse.Clear();
        }
        public static async Task<bool> doesFileExistAsync(string fName)
        {
            try
            {
                await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fName);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
