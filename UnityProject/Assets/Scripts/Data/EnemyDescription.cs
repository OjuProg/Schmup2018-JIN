﻿// <copyright file="EnemyDescription.cs" company="AAllard">Copyright AAllard. All rights reserved.</copyright>

namespace Data
{
    using System.Xml.Serialization;
    using UnityEngine;

    [XmlRoot("EnemyDescription")]
    [XmlType("EnemyDescription")]
    public class EnemyDescription
    {
        [XmlElement]
        public float SpawnDate
        {
            get;
            private set;
        }

        [XmlElement]
        public Vector2 SpawnPosition
        {
            get;
            private set;
        }

        [XmlElement]
        public EnemyType Type
        {
            get;
            private set;
        }
    }
}
