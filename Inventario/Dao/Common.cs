using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Inventario.Dao
{
    class Common
    {
    }

    public class AvailableTemplates : ObservableCollection<DataItem>
    {
        public AvailableTemplates()
        {
            Add(new DataItem("Excel", "", "", @"/Inventario45;component/Resources/excel.png", ""));
            Add(new DataItem("XML", "", "", @"/Inventario45;component/Resources/xml.png", ""));
            //Add(new DataItem("Recent    templates", "", "", @"/RibbonView;component/Images/RibbonView/FirstLook/Backstage/DocTemplateRecent.png", ""));
            //Add(new DataItem("Sample    templates", "", "", @"/RibbonView;component/Images/RibbonView/FirstLook/Backstage/DocTemplateSamples.png", ""));
            //Add(new DataItem("My templates", "", "", @"/RibbonView;component/Images/RibbonView/Backstage/FirstLook/DocTemplateMy.png", ""));
            //Add(new DataItem("New from    existing", "", "", @"/RibbonView;component/Images/RibbonView/FirstLook/Backstage/DocTemplateNewBasedOn.png", ""));
        }
    }

    public class DataItem
    {
        public string Header
        { get; set; }

        public string Description
        { get; set; }

        public string Folder
        { get; set; }

        public string ImageSource
        { get; set; }

        public string NavigateUri
        { get; set; }

        public DataItem(string header, string folder, string description, string imageSource, string navigateUri)
        {
            this.Header = header;
            this.Description = description;
            this.Folder = folder;
            this.ImageSource = imageSource;
            this.NavigateUri = navigateUri;
        }
    }
}
