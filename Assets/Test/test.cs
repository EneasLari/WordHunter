using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Test {
    public class test : MonoBehaviour {
        void Start() {
            var input = gameObject.GetComponent<InputField>();
            var se = new InputField.SubmitEvent();
            se.AddListener(SubmitName);
            input.onEndEdit = se;

            //or simply use the line below, 
            //input.onEndEdit.AddListener(SubmitName);  // This also works
        }

        private void SubmitName(string arg0) {
            Debug.Log(arg0);
        }
    }
}
