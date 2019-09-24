using System.Collections.Generic;

namespace DesktopUploader
{
    public class UploaderSettings
    {
        public string Url { get; set; }
        public int SizeLimit { get; set; }
        public List<string> Extensions { get; set; }

        public UploaderSettings()
        {
            Url = "http://127.0.0.1/uploader.php";
            Extensions = new List<string> { ".jpeg", ".jpg", ".txt", ".doc", ".docx" };
            SizeLimit = 500 * 1024;
        }
    }
}
