using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileBook.Services
{
   public interface ISettingsManager
    {
        

       int AutorizatedUserId { get; set; }
       int SelectedSortMethode { get; set; }
        

    }
}
