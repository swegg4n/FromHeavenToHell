using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using Assets.Classes;

namespace Assets.Classes
{
    static public class StatTracker
    {
        static public int DemonDamageDealtToEnemies { set; get; }
        static public int AngelDamageDealtToEnemies { set; get; }
        static public int DemonDamageTaken { set; get; }
        static public int AngelDamageTaken { set; get; }
        static public int DemonEnemiesKilled { set; get; }
        static public int AngelEnemiesKilled { set; get; }
        static public int DemonDamageDealtToAngel { set; get; }
        static public int AngelDamageDealtToDemon { set; get; }
        static public int DemonSelfDamage { set; get; }
        static public int AngelSelfDamage { set; get; }


    }
}
