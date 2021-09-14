namespace CmdExecuter.Core.Models {
    public class Success {
        /// <summary>
        /// Inner message
        /// </summary>
        public readonly string Message;

        /// <summary>
        /// Default constructor
        /// </summary>
        public Success(string message) {
            Message = message;
        }

        /// <summary>
        /// Overridden <c>Equals</c> method
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>If the two objects are equal</returns>
        public override bool Equals(object obj) {
            if (obj is not Success other) {
                return false;
            }

            return other is not null && other.Message == Message;
        }

        /// <summary>
        /// Overridden <c>GetHashCode</c> method
        /// </summary>
        /// <returns>Hash code of the inner message</returns>
        public override int GetHashCode() {
            return Message.GetHashCode();
        }
    }
}
