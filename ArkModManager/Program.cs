
using ArkModManager;

Configuration.Init();
var workshopFileName = "appworkshop_346110.acf";

var workshopPath = @"steamapps\workshop"; //Path.Combine(Environment.CurrentDirectory, workshopFileName);
var workshopCompletePath = Path.Combine(Configuration.steamLibraryPath, workshopPath, workshopFileName);
AcfReader acfReader = new AcfReader(workshopCompletePath);
var integrity = acfReader.CheckIntegrity();
List<Mods> subscribedmods = new();
if (integrity)
{
    var acf = acfReader.ACFFileToStruct();
    ACF_Struct? worksop;
    acf.SubACF.TryGetValue("AppWorkshop", out worksop);

    string? appid;
    if (worksop != null)
    {
        worksop.SubItems.TryGetValue("appid", out appid);

        if (appid == "346110") //es ark?
        {
            ACF_Struct? mods;
            worksop.SubACF.TryGetValue("WorkshopItemsInstalled", out mods);
            if (mods != null)
            {
                subscribedmods = ModUtilities.GetSubscribedMods(mods.SubACF);
                if(subscribedmods!=null && subscribedmods.Count() > 0)
                {
                    foreach (var mod in subscribedmods)
                    {
                        Console.WriteLine(mod.Id +": "+mod.Name);
                    }
                    var unsubscribedmods = ModUtilities.GetUnsuscribedMods(subscribedmods);
                    if (unsubscribedmods != null && unsubscribedmods.Count > 0)
                    {
                        ModUtilities.CleanUnsubscribedMods(unsubscribedmods);
                    }
                    else
                    {
                        Console.WriteLine("No cleanup needed!!");
                    }
                }
                else
                {
                    Console.WriteLine("You have no subscribed mods");
                }

            }
        }
    }
}
else
{
    throw new Exception("Damaged workshop Info please check Ark integrity before running again");
}