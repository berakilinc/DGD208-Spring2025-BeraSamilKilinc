using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjectFile
{
    public class GameSetup
    {
        public Menu<string> MainMenuSetup() 
        {
            var menuItems = new List<string>
            {
                "Adopt a Pet",
                "View Pets",
                "Use Item",
                "Credit",
                "Exit"
            };
            return new Menu<string>("Main Menu", menuItems, item => item);
        }
    }
}
