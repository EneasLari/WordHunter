using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDictionary : MonoBehaviour {
    //3000 english words
    private static List<string> englishwords = new List<string>() { "good", "bad", "black", "white",  
                                                                    "abandon", "ability", "able", "abortion", "about", "above", "abroad", "absence", "absolute", "absolutely", "absorb", "abuse",
                                                                    "academic", "accept", "access", "accident", "accompany", "accomplish", "according", "account", "accurate", "accuse", "achieve", "achievement", "acid", "acknowledge", "acquire", "across", "act", 
                                                                    "action", "active", "activist", "activity", "actor", "actress", "actual", "actually", "ad", "adapt", "add", "addition", "additional", "address", "adequate", "adjust", "adjustment", "administration",
                                                                    "administrator", "admire", "admission", "admit", "adolescent", "adopt", "adult", "advance", "advanced", "advantage", "adventure", "advertising", "advice", "advise", "adviser", "advocate", "affair", 
                                                                    "affect", "afford", "afraid", "African", "African-American", "after", "afternoon", "again", "against", "age", "agency", "agenda", "agent", "aggressive", "ago", "agree", "agreement", "agricultural", 
                                                                    "ah", "ahead", "aid", "aide","aim", "air", "aircraft", "airline", "airport", "album", "alcohol", "alive", "all", "alliance", "allow", "ally", "almost", "alone", "along", "already", "also",
                                                                    "alter", "alternative", "although", "always", "AM", "amazing", "American", "among", "amount", "analysis", "analyst", "analyze", "ancient", "and", "anger", "angle", "angry", "animal", "anniversary", 
                                                                    "announce", "annual", "another", "answer", "anticipate", "anxiety", "any", "anybody", "anymore", "anyone", "anything", "anyway", "anywhere", "apart", "apartment", "apparent", "apparently", "appeal",
                                                                    "appear", "appearance", "apple", "application", "apply", "appoint", "appointment", };

    private static List<string> greekwords = new List<string>() { "παπι", "πιτα", "πατατα", "τοπι", "ελα", "πανι", "παμε",
                                                                  "εχει", "οχι", "ειναι", "ενα", "μια", "μαμα", "και", "κηπος", "σακος", "λιμνη","πετανε", "κοτα", "τρενο", "καπελο", "λενε", "σακακι", "παππους",
                                                                  "σπαθι", "απο", "μυτη", "σκηνη", "μηλο", "γιατι", "ζυμη", "καλαθι", "λαχανο", "γατα","λεμονι"};


    public static List<string> Vocabulary { get; private set; }

    public static void setVocabulary(string language) {
        if (language.Equals("English")) {
            Vocabulary = englishwords;
        }
        else if (language.Equals("Greek")) {
            Vocabulary = greekwords;
        }
    }
}
