using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

public static class KeyMouseReader
{
	public static KeyboardState keyState, oldKeyState = Keyboard.GetState();
	public static MouseState mouseState, oldMouseState = Mouse.GetState();

    public static GamePadState gamePad = GamePad.GetState(PlayerIndex.One);
    public static GamePadState oldGamePad;

	public static bool KeyPressed(Keys key) {
		return keyState.IsKeyDown(key) && oldKeyState.IsKeyUp(key);
	}

    public static bool ButtonPressed(Buttons button)
    {
        return gamePad.IsButtonDown(button) && oldGamePad.IsButtonUp(button);
    }

	//Should be called at beginning of Update in Game
	public static void Update() {
        oldGamePad = gamePad;
		oldKeyState = keyState;
		keyState = Keyboard.GetState();
		oldMouseState = mouseState;
		mouseState = Mouse.GetState();
        gamePad = GamePad.GetState(PlayerIndex.One);
	}

    public static bool DropDown()
    {
        return (keyState.IsKeyDown(Keys.S) && oldKeyState.IsKeyDown(Keys.S) && KeyPressed(Keys.Space));
    }
}