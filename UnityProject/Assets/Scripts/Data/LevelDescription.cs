namespace Data
{
    using System.Xml.Serialization;

    [XmlRoot("LevelDescription")]
    [XmlType("LevelDescription")]
    public class LevelDescription
    {
        [XmlAttribute]
        public float Duration
        {
            get;
            private set;
        }

        [XmlElement("EnemyDescription", typeof(EnemyDescription))]
        public EnemyDescription[] Enemies
        {
            get;
            private set;
        }

        [XmlElement]
        public string Scene
        {
            get;
            private set;
        }

        [XmlAttribute]
        public string Name
        {
            get;
            private set;
        }
    }
}