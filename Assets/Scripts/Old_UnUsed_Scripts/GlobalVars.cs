using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using UnityEngine;

public static class GlobalVars
{
    public static int playerskinSelected = 0;
    public static int EnvironmentSelected = 0;
    public static int scenetolad=0;
    public static int score = 0;
    public static int correctAnswers = 0;
    public static int coinsCollected = 0;
    public static string wordsCollected = null;
    

    public static void Serialize() {
        SerializeHelper bs =new SerializeHelper();
        XmlSerializer xs = new XmlSerializer(typeof(SerializeHelper));
        string path = Application.persistentDataPath;
        TextWriter txtWriter = new StreamWriter(path + "/" + "SerializationEneas.xml");
        xs.Serialize(txtWriter, bs);
        txtWriter.Close();
    }
    public static void DeSerialize() {
        XmlSerializer deserializer = new XmlSerializer(typeof(SerializeHelper));
        string path = Application.persistentDataPath;
        if (File.Exists(path + "/" + "SerializationEneas.xml")) {
            TextReader reader = new StreamReader(path + "/" + "SerializationEneas.xml");
            object obj = deserializer.Deserialize(reader);
            SerializeHelper XmlData = (SerializeHelper)obj;

            playerskinSelected = XmlData._playerskinSelected;
            EnvironmentSelected = XmlData._EnvironmentSelected;
            scenetolad = XmlData._scenetolad;
            correctAnswers = XmlData._correctAnswers;
            wordsCollected = XmlData._wordsCollected;
            coinsCollected = XmlData._coinsCollected;
            score = XmlData._score;
            reader.Close();
        }
    }


    public class SerializeHelper {

        public  int _playerskinSelected = 0;
        public  int _EnvironmentSelected = 0;
        public  int _scenetolad = 0;
        public  int _correctAnswers = 0;
        public  string _wordsCollected = "";
        public  int _coinsCollected=0;
        public  int _score;

        public SerializeHelper() {

            _playerskinSelected = playerskinSelected;
            _EnvironmentSelected = EnvironmentSelected;
            _scenetolad = scenetolad;
            _correctAnswers = correctAnswers;
            _wordsCollected = wordsCollected;
            _coinsCollected = coinsCollected;
            _score = score;

        }
    }
}
