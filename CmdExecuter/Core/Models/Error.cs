namespace CmdExecuter.Core.Models {
    internal class Error {
        /// <summary>
        /// Inner message
        /// </summary>
        public readonly string Message;

        /// <summary>
        /// Holds the value of whether the error was internal or caused by the user
        /// </summary>
        public readonly bool IsUserError;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="isUserError">If the error was caused by the user, set this value to <c>true</c>, by default the value is <c>false</c></param>
        public Error(string message, bool isUserError = false) {
            Message = message;
            IsUserError = isUserError;
        }

        /// <summary>
        /// Overridden <c>Equals</c> method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>If the two objects are equal</returns>
        public override bool Equals(object obj) {
            if (obj is not Error other) {
                return false;
            }

            return other is not null && other.Message == Message && other.IsUserError == IsUserError;
        }

        /// <summary>
        /// Overridden <c>GetHashCode</c> method
        /// </summary>
        /// <returns>Hash code of the inner message</returns>
        public override int GetHashCode() {
            return Message.GetHashCode() + IsUserError.GetHashCode();
        }
    }
}
