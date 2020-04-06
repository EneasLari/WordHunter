using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.PersistentData {

    public class Globals {
        private static UsersManager _usersManager = null;
        private static Dictionary _persistentDictionary = null;

        //static object to manipulate usersettings as  object in order to serialize later
        public static UsersManager UsersManager {
            get {
                if (_usersManager==null) {
                    _usersManager = UsersManager.GetUsersManager();
                }
                return _usersManager;
            }
        }

        public static Dictionary PersistentDictionary {
            get {
                if (_persistentDictionary == null) {
                    _persistentDictionary = Dictionary.GetDictionary();
                }
                return _persistentDictionary;
            }
        }

        public static void SerializeAll() {
            UsersManager.Serialize();
            PersistentDictionary.Serialize();
        }
    }
}
