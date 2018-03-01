// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Receives the Windows32 input via events.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Input
{
    using System;
    using System.Runtime.InteropServices;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// An <see cref="EventHandler"/> for <see cref="EventInput.CharEntered"/>.
    /// </summary>
    /// <param name="sender">
    /// The <see cref="object"/> which triggered the event.
    /// </param>
    /// <param name="e">
    /// The specific <see cref="CharacterEventArgs"/>.
    /// </param>
    public delegate void CharEnteredHandler(object sender, CharacterEventArgs e);

    /// <summary>
    /// An <see cref="EventHandler"/> for <see cref="EventInput.KeyDown"/> and <see cref="EventInput.KeyUp"/>.
    /// </summary>
    /// <param name="sender">
    /// The <see cref="object"/> which triggered the event.
    /// </param>
    /// <param name="e">
    /// The specific <see cref="CharacterEventArgs"/>.
    /// </param>
    public delegate void KeyEventHandler(object sender, KeyEventArgs e);

    /// <summary>
    /// Receives the Windows32 input via events.
    /// </summary>
    public static class EventInput
    {

        /// <summary>
        /// A value indicating a Win32-constant needed for WndProc.
        /// </summary>
        private const int GWL_WNDPROC = -4;

        /// <summary>
        /// A value indicating a Win32-constant associated with a entered key.
        /// </summary>
        private const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// A value indicating a Win32-constant associated with a released key.
        /// </summary>
        private const int WM_KEYUP = 0x101;

        /// <summary>
        /// A value indicating a Win32-constant associated with a pressed <see cref="char"/>.
        /// </summary>
        private const int WM_CHAR = 0x102;

        /// <summary>
        /// A value indicating a random Win32-constant.
        /// </summary>
        private const int WM_IME_SETCONTEXT = 0x0281;

        /// <summary>
        /// A value indicating a Win32-constant associated with a change of the input language.
        /// </summary>
        private const int WM_INPUTLANGCHANGE = 0x51;

        /// <summary>
        /// A value indicating a random Win32-constant.
        /// </summary>
        private const int WM_GETDLGCODE = 0x87;

        /// <summary>
        /// A value indicating a Win32-constant associated with the composition of something.
        /// </summary>
        private const int WM_IME_COMPOSITION = 0x10f;

        /// <summary>
        /// A value indicating a Win32-constant which wants all keys.
        /// </summary>
        private const int DLGC_WANTALLKEYS = 4;

        /// <summary>
        /// A value indicating whether <see cref="EventInput"/> was initialized.
        /// </summary>
        private static bool initialized;

        /// <summary>
        /// A value indicating the previous <see cref="WndProc"/>.
        /// </summary>
        private static IntPtr prevWndProc;

        /// <summary>
        /// The delegate used to hook on to the WndProc.
        /// </summary>
        private static WndProc hookProcDelegate;

        /// <summary>
        /// A value which has a really good name.
        /// </summary>
        private static IntPtr hIMC;

        /// <summary>
        /// The Wnd-Proc.
        /// </summary>
        /// <param name="hWnd">
        /// The window.
        /// </param>
        /// <param name="msg">
        /// The message.
        /// </param>
        /// <param name="wParam">
        /// A parameter.
        /// </param>
        /// <param name="lParam">
        /// Another parameter.
        /// </param>
        /// <returns>
        /// An <see cref="IntPtr"/>.
        /// </returns>
        private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// The event raised when a <see cref="char"/> has been entered.
        /// </summary>
        public static event CharEnteredHandler CharEntered;

        /// <summary>
        /// The event raised when a <see cref="Key"/> has been pressed down. May fire multiple times due to keyboard repeat.
        /// </summary>
        public static event KeyEventHandler KeyDown;

        /// <summary>
        /// The event raised when a <see cref="Key"/> has been released.
        /// </summary>
        public static event KeyEventHandler KeyUp;

        /// <summary>
        /// Initializes the <see cref="EventInput"/> with the given <see cref="GameWindow"/>.
        /// </summary>
        /// <param name="window">
        /// A <see cref="GameWindow"/> to which text input should be linked.
        /// </param>
        public static void Initialize(GameWindow window)
        {
            if (initialized)
            {
                throw new InvalidOperationException("EventInput.Initialize(GameWindow) can only be called once!");
            }

            EventInput.hookProcDelegate = new WndProc(HookProc);
            EventInput.prevWndProc = (IntPtr)EventInput.SetWindowLong(window.Handle, EventInput.GWL_WNDPROC, (int)Marshal.GetFunctionPointerForDelegate(EventInput.hookProcDelegate));

            EventInput.hIMC = ImmGetContext(window.Handle);
            EventInput.initialized = true;
        }

        /// <summary>
        /// Gets the context of something.
        /// </summary>
        /// <param name="hWnd">
        /// Something WndProcish.
        /// </param>
        /// <returns>
        /// An <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        /// <summary>
        /// Associates the context of something.
        /// </summary>
        /// <param name="hWnd">
        /// Something WndProcish.
        /// </param>
        /// <param name="hIMC">
        /// Something WndProcish?
        /// </param>
        /// <returns>
        /// An <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("Imm32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        /// <summary>
        /// Calls the WndProc?
        /// </summary>
        /// <param name="lpPrevWndFunc">
        /// I love the names of Win32-variables.
        /// </param>
        /// <param name="hWnd">
        /// Really.
        /// </param>
        /// <param name="Msg">
        /// The WndProc-message.
        /// </param>
        /// <param name="wParam">
        /// They expose so much information through their names.
        /// </param>
        /// <param name="lParam">
        /// Like, that they are random chains of <see cref="char"/>s.
        /// </param>
        /// <returns>
        /// An <see cref="IntPtr"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// Sets the window long. Wait ... what?
        /// </summary>
        /// <param name="hWnd">
        /// Another Win32-variable.
        /// </param>
        /// <param name="nIndex">
        /// This variable is nearly readable.
        /// </param>
        /// <param name="dwNewLong">
        /// da window New Long.
        /// </param>
        /// <returns>
        /// An <see cref="int"/>.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        /// <summary>
        /// Hooks on the WndProc and reads input data.
        /// </summary>
        /// <param name="hWnd">
        /// The window.
        /// </param>
        /// <param name="msg">
        /// The message.
        /// </param>
        /// <param name="wParam">
        /// A parameter.
        /// </param>
        /// <param name="lParam">
        /// Another parameter.
        /// </param>
        /// <returns>
        /// An <see cref="IntPtr"/>.
        /// </returns>
        private static IntPtr HookProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            IntPtr returnCode = EventInput.CallWindowProc(prevWndProc, hWnd, msg, wParam, lParam);

            switch (msg)
            {
                case EventInput.WM_GETDLGCODE:
                    returnCode = (IntPtr)(returnCode.ToInt32() | EventInput.DLGC_WANTALLKEYS);
                    break;
                case EventInput.WM_KEYDOWN:
                    if (EventInput.KeyDown != null)
                    {
                        EventInput.KeyDown(null, new KeyEventArgs((Keys)wParam));
                    }

                    break;
                case EventInput.WM_KEYUP:
                    if (EventInput.KeyUp != null)
                    {
                        EventInput.KeyUp(null, new KeyEventArgs((Keys)wParam));
                    }

                    break;
                case EventInput.WM_CHAR:
                    if (EventInput.CharEntered != null)
                    {
                        EventInput.CharEntered(null, new CharacterEventArgs((char)wParam, lParam.ToInt32()));
                    }

                    break;

                case EventInput.WM_IME_SETCONTEXT:
                    if (wParam.ToInt32() == 1)
                    {
                        EventInput.ImmAssociateContext(hWnd, EventInput.hIMC);
                    }

                    break;

                case EventInput.WM_INPUTLANGCHANGE:
                    EventInput.ImmAssociateContext(hWnd, EventInput.hIMC);
                    returnCode = (IntPtr)1;
                    break;
            }

            return returnCode;
        }
    }
}
