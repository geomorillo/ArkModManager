using System.Text;

namespace ArkModManager
{
    public class ModMetaDataReader
    {
        public ModMetaDataReader(string fileName)
        {
            var modsPath = Path.Combine(Configuration.steamLibraryPath, @"steamapps\common\ARK\ShooterGame\Content\Mods");
            string FileLocation= Path.Combine(modsPath, fileName);
            if (File.Exists(FileLocation))
                FilePath = FileLocation;
            else
                throw new FileNotFoundException("Error", FileLocation);
             
        }

        private string FilePath { get; }

        public string GetName()
        {
            byte[] readText = File.ReadAllBytes(FilePath);

            int cont = 0;
            List<byte> buffer = new List<byte>();
            foreach (byte s in readText)
            {
                if (cont == 7 && s != 0)
                {
                    buffer.Add(s);
                }
                else if (cont == 8)
                {
                    break;
                }
                if (s == 0) { cont++; }
            }

            string name = Encoding.UTF8.GetString(buffer.ToArray());
            return name;
        }
    }
}