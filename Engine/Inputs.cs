using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureGame.Engine
{
    public class Inputs
    {

        public static InputMethod keyboard = new InputMethod(
            up: KeyboardInput.W,
            down: KeyboardInput.S,
            left: KeyboardInput.A,
            right: KeyboardInput.D,
            button1: null,
            button2: null,
            button3: null,
            button4: null,
            button5: null,
            button6: null,
            button7: KeyboardInput.Q,
            button8: KeyboardInput.E
        );

        public static InputMethod controller = new InputMethod(
            up: ControllerInput.LeftThumbUp,
            down: ControllerInput.LeftThumbDown,
            left: ControllerInput.LeftThumbLeft,
            right: ControllerInput.LeftThumbRight,
            button1: null,
            button2: null,
            button3: null,
            button4: null,
            button5: null,
            button6: null,
            button7: ControllerInput.LeftShoulder,
            button8: ControllerInput.RightShoulder
        );

    }

}
