// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Dispatches input and delivers it to an <see cref="IInputReceivable"/>.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Input
{
    using System;
    using System.Threading;
    using System.Windows;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Dispatches input and delivers it to an <see cref="IInputReceivable"/>.
    /// </summary>
    public class KeyboardDispatcher
    {
        /// <summary>
        /// The <see cref="IInputReceivable"/> which receives the input dispatched by this <see cref="KeyboardDispatcher"/>.
        /// </summary>
        private IInputReceivable receiver;

        /// <summary>
        /// A value indicating the paste result.
        /// </summary>
        private string pasteResult = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardDispatcher"/> class.
        /// </summary>
        /// <param name="window">
        /// The <see cref="GameWindow"/> needed for visualizing.
        /// </param>
        public KeyboardDispatcher(GameWindow window)
        {
            EventInput.Initialize(window);
            EventInput.CharEntered += new CharEnteredHandler(this.EventInput_CharEntered);
            EventInput.KeyDown += new KeyEventHandler(this.EventInput_KeyDown);
        }

        /// <summary>
        /// Gets or sets the <see cref="IInputReceivable"/> which receives the input dispatched by this <see cref="KeyboardDispatcher"/>.
        /// </summary>
        public IInputReceivable Receiver
        {
            get
            {
                return this.receiver;
            }

            set
            {
                if (this.receiver != null)
                {
                    this.receiver.Focused = false;
                }

                this.receiver = value;
                if (value != null)
                {
                    value.Focused = true;
                }
            }
        }

        /// <summary>
        /// Triggered if a <see cref="Key"/> was entered.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="object"/> which triggered the event.
        /// </param>
        /// <param name="e">
        /// The specific <see cref="KeyEventArgs"/>.
        /// </param>
        private void EventInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.receiver == null)
            {
                return;
            }

            this.receiver.RecieveSpecialInput(e.KeyCode);
        }

        /// <summary>
        /// Triggered if a <see cref="char"/> was entered.
        /// </summary>
        /// <param name="sender">
        /// The <see cref="object"/> which triggered the event.
        /// </param>
        /// <param name="e">
        /// The specific <see cref="CharacterEventArgs"/>.
        /// </param>
        private void EventInput_CharEntered(object sender, CharacterEventArgs e)
        {
            if (this.receiver == null)
            {
                return;
            }

            if (char.IsControl(e.Character))
            {
                // ctrl+v
                if (e.Character == 0x16)
                {
                    // XNA runs in Multiple Thread Apartment state, which cannot receive clipboard
                    Thread thread = new Thread(this.PasteThread);
                    thread.SetApartmentState(ApartmentState.STA);
                    thread.Start();
                    thread.Join();
                    this.receiver.RecieveTextInput(this.pasteResult);
                }
                else
                {
                    this.receiver.RecieveCommandInput(e.Character);
                }
            }
            else
            {
                this.receiver.RecieveTextInput(e.Character);
            }
        }

        /// <summary>
        /// Receives the text stored in the <see cref="Clipboard"/>. Needs to be in Single Thread Apartment state in order to receive it.
        /// </summary>
        [STAThread]
        private void PasteThread()
        {
            if (Clipboard.ContainsText())
            {
                this.pasteResult = Clipboard.GetText();
            }
            else
            {
                this.pasteResult = string.Empty;
            }
        }
    }
}
