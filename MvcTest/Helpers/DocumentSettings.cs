using System.Net;

namespace Mvc.PAL.Helpers
{
    public static class DocumentSettings
    {
        public static string UploadFile (IFormFile file, string folderName)
        {
            //1.Get located folder path
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);
            //2.get file name and make it unique
            string fileName = $"{Guid.NewGuid()}{file.FileName}";
            //3. get file path 
            string filePath = Path.Combine(folderPath, fileName);

                //have the file path complete

            //4.save file as streams
            using var Fs = new FileStream(filePath, FileMode.Create);
            file.CopyTo(Fs);
            //5. return fielName
            return fileName;
        }


        public static void DeleteFile(string FileName, string FolderName)
        {
            //1. get file path 
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\Files",
                FolderName,FileName);
            //2. check if exists 
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
