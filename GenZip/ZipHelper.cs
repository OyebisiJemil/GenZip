using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenZip
{
    public class ZipHelper
    {
        /// <summary>
        /// Creates a ZIP archive from a collection of files.
        /// </summary>
        /// <param name="files">The collection of files to include in the archive.</param>
        /// <returns>A byte array that represents the ZIP archive.</returns>
        public static byte[] CreateZipArchive(IEnumerable<ZipFileEntry> files)
        {
            if (files == null)
                throw new ArgumentNullException(nameof(files));

            using (var zipMemoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(zipMemoryStream, ZipArchiveMode.Create, leaveOpen: true))
                {
                    foreach (var file in files)
                    {
                        if (file == null || file.Content == null || string.IsNullOrWhiteSpace(file.FileName))
                            continue;

                        var entry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);

                        using (var entryStream = entry.Open())
                        {
                            entryStream.Write(file.Content, 0, file.Content.Length);
                        }
                    }
                }
                return zipMemoryStream.ToArray();
            }
        }
    }
}
