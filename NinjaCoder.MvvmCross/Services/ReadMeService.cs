﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ReadMeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    /// Defines the ReadMeService type.
    /// </summary>
    public class ReadMeService : IReadMeService
    {
        /// <summary>
        /// The application version.
        /// </summary>
        private string applicationVersion;

        /// <summary>
        /// The MMVM cross version.
        /// </summary>
        private string mmvmCrossVersion;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        /// <param name="appVersion">The app version.</param>
        /// <param name="mvxVersion">The MVX version.</param>
        public void Initialize(
            string appVersion,
            string mvxVersion)
        {
            this.applicationVersion = appVersion;
            this.mmvmCrossVersion = mvxVersion;
        }

        /// <summary>
        /// Adds the lines.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="lines">The lines.</param>
        public void AddLines(
            string filePath,
            string functionName,
            IEnumerable<string> lines)
        {
            string currentLines = string.Empty;

            if (File.Exists(filePath))
            {
                currentLines = File.ReadAllText(filePath);
            }

            //// first write the new lines

            StreamWriter sw = new StreamWriter(filePath, false);

            sw.WriteLine(string.Empty);
            sw.WriteLine(this.GetSeperatorLine());
            sw.WriteLine(DateTime.Now.ToString("dd MMM yy HH:mm")  + " " + functionName);
            sw.WriteLine("MvvmCross v" + this.mmvmCrossVersion);
            sw.WriteLine(this.GetSeperatorLine());
            sw.WriteLine(string.Empty);

            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }

            //// now write the old lines or add footer

            if (string.IsNullOrEmpty(currentLines) == false)
            {
                sw.Write(currentLines);
            }
            else
            {
                foreach (string footerLine in this.GetFooterLines())
                {
                    sw.WriteLine(footerLine);
                }
            }

            sw.Close();
        }

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <returns>The footer text.</returns>
        internal List<string> GetFooterLines()
        {
            return new List<string>
            {
                string.Empty,
                this.GetSeperatorLine(),
                "Ninja Coder for MvvmCross v" + this.applicationVersion,
                "Built with MvvmCross v" + this.mmvmCrossVersion,
                this.GetSeperatorLine(),
                string.Empty,
                "All feedback welcome - get in touch with the email or twitter address below.",
                string.Empty,
                "Have a good day!",
                string.Empty,
                "Thanks",
                string.Empty,
                "asudbury@scorchio.org",
                "@asudbury"
            };
        }

        /// <summary>
        /// Gets the seperator line.
        /// </summary>
        /// <returns>the seperator line.</returns>
        internal string GetSeperatorLine()
        {
            return "----------------------------------------------------------------------------------------------------";
        }
    }
}
