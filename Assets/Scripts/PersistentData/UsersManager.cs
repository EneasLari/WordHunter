using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.PersistentData {

    /*
        The basic Class for storing user setting data
        Contains all methods for serializing and deserializing user's data in persistent way
    */
    /// <summary>
    /// The basic Class for storing user setting data
    /// Contains all methods for serializing and deserializing user's data in persistent way
    /// </summary>
    /// <remarks>
    /// This class can Serialize and Deserialize(Calling <c>GetUsersManager</c>) userSettings.
    /// </remarks>
    public class UsersManager {
        private List<User> _users = null;
        private int _currentUserIndex = 0;
            

        public List<User> Users {
            get {
                if (_users == null) {
                    _users = new List<User>();
                }
                return _users; }
        }

        public int CurrentUserIndex { 
            get {
                return _currentUserIndex;
            } set {
                _currentUserIndex = value;
            }
        }

        public bool AddNewUser(User newuser) {
            if (_users.Exists(x => x.Name.Equals(newuser.Name))) {
                return false;
            } else {
                newuser.DateCreated = DateTime.UtcNow; ;
                _users.Add(newuser);
                return true;
            }

        }

        public bool DeleteUser(User existinguser) {
            User usertodelete = _users.Find(x => x.Name.Equals(existinguser.Name));
            if (usertodelete == null) {
                return false;
            } else {
                return _users.Remove(usertodelete);
            }
        }

        public User AuthenticateUser(string name,string password) {
            User usertoauthenticate = _users.Find(x => x.Name.Equals(name));
            if (usertoauthenticate == null) {
                return null;
            } else {
                if (EncryptDecrypt.Decrypt(usertoauthenticate.Password).Equals(password)) {
                    return usertoauthenticate;
                } else {
                    return null;
                }
            }
        }

        public bool ChangePassword(string name, string oldpassword,string newpassword) {
            User usertoauthenticate = _users.Find(x => x.Name.Equals(name));
            if (usertoauthenticate == null) {
                return false;
            } else {
                if (EncryptDecrypt.Decrypt(usertoauthenticate.Password).Equals(oldpassword)) {
                    usertoauthenticate.Password = newpassword;
                    return true;
                } else {
                    return false;
                }
            }
        }

        internal string userSettingFile = "UsersManager.xml";

        [XmlElement(ElementName = "userSettingsPath")]
        public string UserSettingsPath {
            get {
                return System.IO.Path.Combine(Application.persistentDataPath,@"EducationalGames\WordHunter");//TODO
            }
        }

        //save data(variables of a class) to a xml
        public void Serialize() {
            try {
                if (!System.IO.Directory.Exists(UserSettingsPath))
                    System.IO.Directory.CreateDirectory(UserSettingsPath);
                string filename = System.IO.Path.Combine(UserSettingsPath, userSettingFile);
                XmlSerializer serializer = new XmlSerializer(typeof(UsersManager));
                System.IO.TextWriter writer = new System.IO.StreamWriter(filename);
                serializer.Serialize(writer, this);
            } catch (Exception ex) {
                //XtraMessageBox.Show("" + ex, "Error Serializing UserPreferences", MessageBoxButtons.OK, MessageBoxIcon.Error);//TODO
            }
        }

        //retrieve data from xml to initilize object/s
        private void Deserialize() {
            string filename = System.IO.Path.Combine(UserSettingsPath, userSettingFile);
            if (!System.IO.File.Exists(filename))
                return;
            UsersManager usersets = new UsersManager();
            XmlSerializer serializer = new XmlSerializer(typeof(UsersManager));
            using (System.IO.FileStream fileStream = new System.IO.FileStream(filename, System.IO.FileMode.Open)) {
                usersets = (UsersManager)serializer.Deserialize(fileStream);
                this._users = usersets._users;
                this._currentUserIndex = usersets._currentUserIndex;
            }
        }

        /// <summary>
        /// Get an static <c>UsersManager</c> object from a xml file(Deserialize)
        /// </summary>
        public static UsersManager GetUsersManager() {
            UsersManager usersets = new UsersManager();
            usersets.Deserialize();
            if (usersets.Users.Count == 0) {
                User firstuser = new User();
                usersets.AddNewUser(firstuser);
            }
            return usersets;
        }

    }
}
