using CmdExecuter.Core.Models;

using OneOf;

using System;
using System.IO;

namespace CmdExecuter.Core.Components {
    internal class DirectoryCreator {
        public readonly string ResourcesDirectoryPath;

        public DirectoryCreator() {
            ResourcesDirectoryPath = $"{Directory.GetCurrentDirectory()}\\Resources";
        }

        /// <summary>
        /// Checks whether the resources directory exists
        /// </summary>
        public bool DoesResourcesDirectoryExist() {
            return Directory.Exists(ResourcesDirectoryPath);
        }

        /// <summary>
        /// Checks if there any files in the resources directory
        /// </summary>
        public bool IsResourcesEmpty() {
            return Directory.GetFiles(ResourcesDirectoryPath).Length == 0;
        }

        /// <summary>
        /// Creates the resources directory if doesn't exist
        /// </summary>
        public OneOf<Success, Error> CreateResourcesDirectory() {
            try {
                Directory.CreateDirectory(ResourcesDirectoryPath);
            } catch (Exception ex) {
                return new Error(ex.Message);
            }
            return new Success("Directory created successfully.");
        }
    }
}
