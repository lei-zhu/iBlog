// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extension.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The extension.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Domain
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The extension.
    /// </summary>
    public static class Extension
    {
        #region Public Methods and Operators

        /// <summary>
        /// The get m d 5 hash.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetMD5Hash(this string value)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(value));

            var stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        /// <summary>
        /// The to error entity.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// The <see cref="ErrorEntity"/>.
        /// </returns>
        public static ErrorEntity ToErrorEntity(this Exception exception)
        {
            var errorEntity = new ErrorEntity
            {
                ThrowTime = DateTime.Now,
                Message = exception.Message.Replace("'", "''"),
                Description = exception.ToString().Replace("'", "''")
            };

            return errorEntity;
        }

        #endregion
    }
}