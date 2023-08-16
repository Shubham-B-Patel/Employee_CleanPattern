namespace Employee_CleanPattern.Helper
{
    public class UploadUserProfile
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UploadUserProfile(IWebHostEnvironment environment)
        {
            _webHostEnvironment = environment;
        }

        public string Upload(int user_Id, IFormFile file)
        {
            var contentPath = this._webHostEnvironment.WebRootPath;
            var path = Path.Combine(contentPath, "Users/Profile");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(file.FileName);
            var newName = DateTime.Now.Ticks.ToString();
            var newFileName = user_Id + newName + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            file.CopyTo(stream);
            stream.Close();

            var newPath = "https://localhost:7071/" + "/Users/Profile/" + newFileName;
            return newPath;
        }
    }
}