using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Scripts.PersistentData {
    public class Dictionary {

        private List<Word> _dictionary;
        private Language _selectedLanguage;
        public enum Language { Greek,English };

        public List<Word> DictionaryList {
            get {
                if (_dictionary == null) {
                    _dictionary=new List<Word>();
                }
                return _dictionary;
            }
            set { _dictionary = value; }
        }

        public Language SelectedLanguage { 
            get { return _selectedLanguage; }
            set {
                _selectedLanguage = value;
            } 
        }

        internal string dictionaryFile = "DictionaryData.xml";

        [XmlElement(ElementName = "DictionaryPath")]
        public string DictionaryPath {
            get {
                return System.IO.Path.Combine(Application.persistentDataPath, @"EducationalGames\WordHunter");//TODO
            }
        }
        //save data(variables of a class) to a xml
        public void Serialize() {
            try {
                if (!System.IO.Directory.Exists(DictionaryPath))
                    System.IO.Directory.CreateDirectory(DictionaryPath);
                string filename = System.IO.Path.Combine(DictionaryPath, dictionaryFile);
                XmlSerializer serializer = new XmlSerializer(typeof(Dictionary));
                System.IO.TextWriter writer = new System.IO.StreamWriter(filename);
                serializer.Serialize(writer, this);
            } catch (Exception ex) {
                //XtraMessageBox.Show("" + ex, "Error Serializing UserPreferences", MessageBoxButtons.OK, MessageBoxIcon.Error);//TODO
            }
        }

        //retrieve data from xml to initilize object/s
        private void Deserialize() {
            string filename = System.IO.Path.Combine(DictionaryPath, dictionaryFile);
            if (!System.IO.File.Exists(filename))
                return;
            Dictionary vocab = new Dictionary();
            XmlSerializer serializer = new XmlSerializer(typeof(Dictionary));
            using (System.IO.FileStream fileStream = new System.IO.FileStream(filename, System.IO.FileMode.Open)) {
                vocab = (Dictionary)serializer.Deserialize(fileStream);
                this._selectedLanguage = vocab._selectedLanguage;
                this._dictionary = vocab._dictionary;
            }
        }

        /// <summary>
        /// Get an static <c>Dictionary</c> object from a xml file(Deserialize)
        /// </summary>
        public static Dictionary GetDictionary() {
            Dictionary vocab = new Dictionary();
            vocab.Deserialize();
            return vocab;
        }

        public class Word {
            private Language _wordLanguage;
            private string _word =null;

            public Language WordLanguage { 
                get { return _wordLanguage; } 
                set { _wordLanguage = value; } 
            }
            public string Wordstr { 
                get {
                    if (_word == null) {
                        _word = "";
                    }
                    return _word; } 
                set { _word = value; } 
            }
        }
    }
}
