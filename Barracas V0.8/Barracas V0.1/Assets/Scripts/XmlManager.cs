using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class XmlManager : MonoBehaviour {

    public static XmlManager ins;

    void Awake()
    {
        ins = this;
    }
}

public class Languages
{

}
