// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserIdentity.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The user identity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Service
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security;
    using System.Security.Principal;
    using System.Web.Security;

    /// <summary>
    /// The user identity.
    /// </summary>
    [Serializable]
    public class UserIdentity : IIdentity, IUserInfo, ISerializable, IPrincipal
    {
        #region Fields

        /// <summary>
        /// The ticket.
        /// </summary>
        private readonly FormsAuthenticationTicket ticket;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UserIdentity"/> class.
        /// </summary>
        /// <param name="ticket">
        /// The ticket.
        /// </param>
        public UserIdentity(FormsAuthenticationTicket ticket)
        {
            this.ticket = ticket;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the authentication type.
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "User";
            }
        }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        public IIdentity Identity
        {
            get
            {
                return this;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is authenticated.
        /// </summary>
        public bool IsAuthenticated
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.ticket.Name;
            }
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public string UserId
        {
            get
            {
                return this.ticket.UserData.Split(':').First();
            }
        }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        public string UserToken
        {
            get
            {
                return this.ticket.UserData.Split(':').Last();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get object data.
        /// </summary>
        /// <param name="info">
        /// The info.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        [SecurityCritical]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (context.State != StreamingContextStates.CrossAppDomain)
            {
                throw new InvalidOperationException("Serialization not supported");
            }

            var identity = new GenericIdentity(this.Name, this.AuthenticationType);
            info.SetType(identity.GetType());

            MemberInfo[] serializableMembers = FormatterServices.GetSerializableMembers(identity.GetType());
            object[] serializableValues = FormatterServices.GetObjectData(identity, serializableMembers);

            for (int i = 0; i < serializableMembers.Length; i++)
            {
                info.AddValue(serializableMembers[i].Name, serializableValues[i]);
            }
        }

        /// <summary>
        /// The is in role.
        /// </summary>
        /// <param name="role">
        /// The role.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsInRole(string role)
        {
            return Roles.IsUserInRole(this.Name);
        }

        #endregion
    }
}