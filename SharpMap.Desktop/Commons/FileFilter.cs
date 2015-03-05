using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpMap.Desktop
{
    public enum FileFilterType
    {
        File,
        Folder,
        DataBase,
        Protocol
    }

    public class FileFilterIdentifier
    {
        public FileFilterType FilterType;
        public string Name;
        public string Description;
        public string FileExtensions;

        public FileFilterIdentifier()
        {

        }

        public FileFilterIdentifier(FileFilterType filterType, string name, string description, string fileExtension)
        {
            FilterType = filterType;
            Name = name;
            Description = description;
            FileExtensions = fileExtension;
        }

        public string Filter
        {
            get
            {
                return Description + "|" + FileExtensions;
            }
        }

    }

    public class FileFilterList : List<FileFilterIdentifier>
    {
        public void InitializeRaster()
        {

            SharpMap.GdalConfiguration.ConfigureGdal();


            int NDriveGDal = OSGeo.GDAL.Gdal.GetDriverCount();

            //List<string> drvsGDal = new List<string>();

            for (int i = 0; i < NDriveGDal; i++)
            {
                OSGeo.GDAL.Driver d = OSGeo.GDAL.Gdal.GetDriver(i);

                string Description = d.GetDescription() + " " + d.LongName;
                string Name = "GDAL." + d.ShortName;
                string ext = d.GetMetadataItem("DMD_EXTENSION", "");

                if (ext != null & ext != "")
                {
                    Add(new FileFilterIdentifier(FileFilterType.File, Name, Description + " (*." + ext + ")", "*." + ext));
                }
                else
                {
                    Add(new FileFilterIdentifier(FileFilterType.Folder, Name, Description, ext));
                }

                //drvsGDal.Add(d.ShortName + " " + d.LongName + "\r\n\r\n\t" + d.GetMetadata("").Aggregate((a, b) => a + "\r\n\t" + b) + "\r\n\r\n");
            }

        }

        public void InitializeVector()
        {

            Add(new FileFilterIdentifier(FileFilterType.File, "SharpMap.shapefile", "Esri shapefile (*.shp)", "*.shp"));
            Add(new FileFilterIdentifier(FileFilterType.File, "OGR.MapInfo", "MapInfo File (*.tab)", "*.tab"));
            Add(new FileFilterIdentifier(FileFilterType.File, "OGR.KML", "Keyhole Markup Language (*.kml)", "*.kml"));
            Add(new FileFilterIdentifier(FileFilterType.File, "OGR.DXF", "AutoDESK Drawing Interchange Format (*.dxf)", "*.dxf"));
            Add(new FileFilterIdentifier(FileFilterType.File, "OGR.GPX", "GPS eXchange Format (*.gpx)", "*.gpx"));
            Add(new FileFilterIdentifier(FileFilterType.Folder, "OGR.AVCBin", "ArcInfo Vector coverage (foolder)", ""));


        }


    }

}
