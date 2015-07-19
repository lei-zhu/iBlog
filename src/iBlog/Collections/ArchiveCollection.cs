// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArchiveCollection.cs" company="iBlog">
//   (C) 2015 iBlog. All rights reserved.
// </copyright>
// <summary>
//   The archive collection.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace iBlog.Collections
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using iBlog.Domain.Entities;

    /// <summary>
    /// The archive collection.
    /// </summary>
    public class ArchiveCollection : IEnumerator, IEnumerable
    {
        #region Fields

        /// <summary>
        /// The archives.
        /// </summary>
        private readonly List<Archive> archives = new List<Archive>();

        /// <summary>
        /// The posts list.
        /// </summary>
        private readonly List<PostEntity> postsList;

        /// <summary>
        /// The current.
        /// </summary>
        private int current = -1;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ArchiveCollection"/> class.
        /// </summary>
        /// <param name="postsList">
        /// The posts list.
        /// </param>
        public ArchiveCollection(List<PostEntity> postsList)
        {
            this.postsList = postsList;
            this.AddArchives();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the current.
        /// </summary>
        public object Current
        {
            get
            {
                return this.archives[this.current];
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerator"/>.
        /// </returns>
        public IEnumerator GetEnumerator()
        {
            return this.archives.GetEnumerator();
        }

        /// <summary>
        /// The move next.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool MoveNext()
        {
            this.current++;
            return this.current < this.archives.Count;
        }

        /// <summary>
        /// The reset.
        /// </summary>
        public void Reset()
        {
            this.current = -1;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The add archives.
        /// </summary>
        private void AddArchives()
        {
            var dateTimeFormatInfo = new DateTimeFormatInfo();
            var group =
                this.postsList.GroupBy(p => new { p.CreateTime.Year, p.CreateTime.Month })
                    .OrderByDescending(g => g.Key.Year)
                    .ThenByDescending(g => g.Key.Month);
            List<Archive> list =
                @group.Select(
                    g =>
                    new Archive
                        {
                            MonthYear =
                                string.Format(
                                    "{0} {1} ({2})", 
                                    dateTimeFormatInfo.GetMonthName(g.Key.Month), 
                                    g.Key.Year, 
                                    g.Count()), 
                            Year = g.Key.Year.ToString(CultureInfo.InvariantCulture), 
                            Month = g.Key.Month.ToString("00")
                        }).ToList();

            this.archives.AddRange(list);
        }

        #endregion
    }
}