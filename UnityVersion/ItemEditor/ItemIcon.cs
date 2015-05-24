using System.IO;
using System.Windows.Media;

namespace ItemEditor
{
    public class ItemIcon
    {
        public ItemIcon(ImageSource piImage)
        {
            Image = piImage;
        }

        public ImageSource Image { get; set; }

        public string Name 
        {
            get { return Path.GetFileNameWithoutExtension(Image.ToString()); }
        }
    }
}
