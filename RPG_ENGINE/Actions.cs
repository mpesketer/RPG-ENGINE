using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_ENGINE
{
    public class Actions
    {
        public enum ActionTypes
        {
            ShowText = 0,
            BaseStateUpdate = 1,
            ObjectStateUpdate = 2,
            MoveObject = 3,
            ShowText_BaseStateUpdate_MoveObject = 4,
            ShowText_BaseStateUpdate = 5,
            ShowText_BaseStateUpdate_ObjectStateUpdate = 6,
            ShowText_ObjectStateUpdate = 7
        }

        public Actions(string[] data)
        {
            Name = data[0];
            ActionType = (ActionTypes)int.Parse(data[1]);
            Text = data[2];
            BaseStateIndex = int.Parse(data[3]);
            ObjectStateIndex = int.Parse(data[4]);
            StateObjectName = data[5];
            OffsetX = int.Parse(data[6]);
            OffsetY = int.Parse(data[7]);
            MoveObjectName = data[8];
        }

        public string Name { get; set; }
        public ActionTypes ActionType { get; set; }
        public string StateObjectName { get; set; }
        public string Text { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public int BaseStateIndex { get; set; }
        public int ObjectStateIndex { get; set; }
        public string MoveObjectName { get; set; }
    }
}
