// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidMonthException.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The invalid month exception.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Exceptions
{
    using System;

    /// <summary>
    /// The invalid month exception.
    /// </summary>
    public class InvalidMonthException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMonthException"/> class.
        /// </summary>
        public InvalidMonthException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidMonthException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        public InvalidMonthException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
        }

        #endregion
    }
}