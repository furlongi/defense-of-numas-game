using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class DialogueLoader
{
    
    public static NpcTemplate LoadXML(TextAsset file)
    {
        var serializer = new XmlSerializer(typeof(NpcTemplate));
        using (var stream = new StringReader(file.text))
        {
            return serializer.Deserialize(stream) as NpcTemplate;
        }
    }
}
