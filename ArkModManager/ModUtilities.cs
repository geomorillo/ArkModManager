namespace ArkModManager
{
    public static class ModUtilities
    {
        public static List<Mods> GetSubscribedMods(Dictionary<string, ACF_Struct> modList)
        {
            var listMods = new List<Mods>();
            ModMetaDataReader modMetadata;

            foreach (var item in modList)
            {
                var modId = item.Key;
                var modFileName = modId + ".mod";
                modMetadata = new ModMetaDataReader(modFileName);
                listMods.Add(new Mods
                {
                    Id = modId,
                    Name = modMetadata.GetName(),
                    FileName = modFileName
                });
            }

            return listMods;
        }

        public static List<Mods> GetUnsuscribedMods(List<Mods> subscribedmods)
        {
            List<Mods> unsuscribedmods = new List<Mods>();
            try
            {
                var modsPath = Path.Combine(Configuration.steamLibraryPath, @"steamapps\common\ARK\ShooterGame\Content\Mods");
                var modsFiles = Directory.EnumerateFiles(modsPath, "*", new EnumerationOptions { RecurseSubdirectories = false })
                          .Select(Path.GetFileName);
                List<Mods> filesMods = new List<Mods>();

                foreach (var modfilename in modsFiles)
                {
                    filesMods.Add(new Mods
                    {
                        FileName = modfilename,
                        Id = modfilename.Replace(".mod", "")
                    });
                }

                unsuscribedmods = filesMods.Where(x => !subscribedmods.Any(z => z.FileName == x.FileName)).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }

            return unsuscribedmods;
        }

        public static void CleanUnsubscribedMods(List<Mods> unsubscribedmods)
        {
            string modsPath = Path.Combine(Configuration.steamLibraryPath, @"steamapps\common\ARK\ShooterGame\Content\Mods\");


            foreach (var mod in unsubscribedmods)
            {
                string modPath = Path.Combine(modsPath, mod.FileName);
                File.Delete(modPath);
                string directoryModPath = Path.Combine(modsPath, mod.Id);
                FileUtilities.DeleteDirectory(directoryModPath);
            }
        }
    }
}