using Core.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml.Linq;

namespace Core.Models
{

    /// <summary>
    /// 
    /// </summary>
    public class GoogleChromeProfileModel
    {

        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Vorname { get; set; } = string.Empty;
        public string DisplayText { get; set; } = "invalid";
        public string Profilename { get; set; } = string.Empty;
        public string Pictureurl { get; set; } = string.Empty;

        public string ProfileFilePath { get; set; } = string.Empty;

        public bool IsValid { get; set; } = false;



        [Obsolete("This property is obsolete. Use oogleChromeProfileModel(string path) instead.", false)]
        /// <summary>
        /// Default constructor
        /// </summary>
        public GoogleChromeProfileModel() { }

        /// <summary>
        /// bekommt einen kompletten Pfadmanem auf die preferencess datei und erstett darau sein komplettes profiledata objekt für uns 
        /// </summary>
        /// <param name="fullyQualifiedFilename"></param>
    

        public GoogleChromeProfileModel(string fullyQualifiedFilename)
        {
            ProfileFilePath = fullyQualifiedFilename;
            string fileContents = File.ReadAllText(fullyQualifiedFilename);

            var result = JsonConvert.DeserializeObject<Profil>(fileContents);

            if (result != null && result.AccountInfo != null && result.AccountInfo.Length > 0)
            {
                var accountInfo = result.AccountInfo[0];

                Email = accountInfo.Email;
                Name = accountInfo.FullName;
                Vorname = accountInfo.GivenName;
                Pictureurl = accountInfo.Pictureurl;

                    Profilename = result.Profile.Name;
                    string displayInfo = $"{Vorname} ({Profilename})";
                    DisplayText = displayInfo;
                    IsValid = true;
               
            }
        }

    }
}