namespace MyEducationCenter.Core;
public class StaticFileConst
{
    public class ExcelTemplate
    {
        private static string OWN_FOLDER = "appdata";
        private static string FOLDER = "exceltemplates";

        public static readonly string InventoryMovementReportByOrganization = "InventoryMovementReportByOrganization.xlsx";

        public static string GetFileName(string templateName) => Path.Combine(OWN_FOLDER, FOLDER, templateName);
    }
    public static MemoryStream GetMomoryStreamFromPath(string path)
    {
        using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);

                memoryStream.Position = 0; // Reset the position to the beginning of the stream
                byte[] data = new byte[memoryStream.Length];
                memoryStream.Read(data, 0, data.Length);
                return memoryStream;
            }
        }
    }
}