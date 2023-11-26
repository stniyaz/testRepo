namespace PustokSliderCRUD.Extensions
{
    public static class Helper
    {
        public static string FileSave(string root,string folderPath,IFormFile imageFile)
        {
            string newName = imageFile.FileName.Length > 64 ? imageFile.FileName.Substring(imageFile.FileName.Length-64,64) : imageFile.FileName;
            newName = Guid.NewGuid().ToString() + newName;
            string path = Path.Combine(root,folderPath,newName);
            using (FileStream straem = new FileStream(path,FileMode.Create))
            {
                imageFile.CopyTo(straem);
            }
            return newName;
        }
    }
}
