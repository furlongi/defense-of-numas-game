using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot (ElementName = "root")]
public class NpcTemplate
{
    [XmlElement (ElementName = "world")]
    public WorldTemplate World;
}

[XmlRoot (ElementName = "world")]
public class WorldTemplate
{
    [XmlElement (ElementName = "event")]
    public List<EventTemplate> EventType;

    [XmlAttribute("category")]
    public string WorldName;
}

[XmlRoot (ElementName = "event")]
public class EventTemplate
{
    [XmlAttribute("category")] 
    public string EventFlag;
    
    [XmlElement ("text")]
    public List<string> Text = new List<string>();
}




