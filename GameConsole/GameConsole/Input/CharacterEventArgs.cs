// --------------------------------------------------------------------------------------------------------------------
// <summary>
//   Containing event data for a <see cref="char"/> event.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GameConsole.Input
{
    using System;

    /// <summary>
    /// Containing event data for a <see cref="char"/> event.
    /// </summary>
    public class CharacterEventArgs : EventArgs
    {
        /// <summary>
        /// A value indicating the <see cref="char"/> associated with this <see cref="CharacterEventArgs"/>.
        /// </summary>
        private readonly char character;

        /// <summary>
        /// Additional information of this <see cref="CharacterEventArgs"/>.
        /// </summary>
        private readonly int lParam;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterEventArgs"/> class.
        /// </summary>
        /// <param name="character">
        /// A <see cref="char"/> associated to this <see cref="CharacterEventArgs"/>.
        /// </param>
        /// <param name="lParam">
        /// A value specifying the <see cref="CharacterEventArgs"/>.
        /// </param>
        public CharacterEventArgs(char character, int lParam)
        {
            this.character = character;
            this.lParam = lParam;
        }

        /// <summary>
        /// Gets the <see cref="char"/> associated with this <see cref="CharacterEventArgs"/>.
        /// </summary>
        public char Character
        {
            get { return this.character; }
        }

        /// <summary>
        /// Gets the additional information of this <see cref="CharacterEventArgs"/>.
        /// </summary>
        public int Param
        {
            get { return this.lParam; }
        }

        /// <summary>
        /// Gets a value indicating the count of repeats of a specific <see cref="char"/>.
        /// </summary>
        public int RepeatCount
        {
            get { return this.lParam & 0xffff; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Character"/> is an extended Key.
        /// </summary>
        public bool ExtendedKey
        {
            get { return (this.lParam & (1 << 24)) > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether the ALT-<see cref="char"/> was pressed.
        /// </summary>
        public bool AltPressed
        {
            get { return (this.lParam & (1 << 29)) > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Character"/> was in a previous state.
        /// </summary>
        public bool PreviousState
        {
            get { return (this.lParam & (1 << 30)) > 0; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Character"/> was in a transition state.
        /// </summary>
        public bool TransitionState
        {
            get { return (this.lParam & (1 << 31)) > 0; }
        }
    }
}
