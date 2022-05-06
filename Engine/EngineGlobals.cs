﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureGame.Engine
{
    public class EngineGlobals
    {
        public static ComponentManager componentManager;
        public static EntityManager entityManager;
        public static List<System> systems = new List<System>();
        public static bool DEBUG = true;
    }

}
