using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assets.Scripts.PersistentData.Dictionary;

namespace Assets.Scripts.PersistentData {
    public class User {
        private string _name=null;
        private string _password = null;
        private int _score=0;
        private int _coins = 0;
        private string _skinSelected =null;
        private string _sceneSelected = null;
        private float _timePlaying=0;
        private DateTime _dateCreated=DateTime.MinValue;
        private List<Word> _wordsCollected = new List<Word>();
        private DateTime _startTimer= DateTime.MinValue;
        

        public string Name {
            get {
                if (_name==null) {
                    _name = "Default";
                }
                return _name; }
            set { _name = value; }
        }

        public string Password {
            get { return _password; }
            set { _password = EncryptDecrypt.Encrypt(value); }
        }

        public int Score {
            get { return _score; }
            set { _score = value; }
        }

        public int Coins {
            get { return _coins; }
            set { _coins = value; }
        }

        public string SkinSelected {
            get {
                if (_skinSelected==null) {
                    _skinSelected = "Default";
                }
                return _skinSelected; }
            set { _skinSelected = value; }
        }

        public string SceneSelected { 
            get {
                if (_sceneSelected == null) {
                    _sceneSelected = "Default";
                }
                return _sceneSelected; } 
            set { _sceneSelected = value; } 
        }

        public float TimePlaying {
            get { return _timePlaying; }
            set { _timePlaying = value; }
        }
        public DateTime DateCreated {
            get {
                if (_dateCreated == DateTime.MinValue) {
                    _dateCreated = DateTime.UtcNow;
                }
                return _dateCreated; 
            }
            set { 
                _dateCreated = value; 
            }
        }

        public List<Word> WordsCollected { 
            get { return _wordsCollected; }
            set { _wordsCollected = value; }
        }

        public void StartTimer() {
            _startTimer = DateTime.Now;
        }

        public double GetTimePassed() {
            DateTime starttime=_startTimer;
            _startTimer = DateTime.MinValue;
            if (_startTimer!=DateTime.MinValue) {
                return (DateTime.Now - starttime).TotalSeconds;
            }
            return 0;
        }

    }
}
