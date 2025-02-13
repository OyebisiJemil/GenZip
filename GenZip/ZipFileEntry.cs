namespace GenZip
{
    /// <summary>
    /// This represents a file to be included in the ZIP archive.
    /// </summary>
    public class ZipFileEntry
    {
        /// <summary>
        /// The name of the file (e.g., "file.txt" "Report.xlsx", "data.csv").
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The file content as a byte array.
        /// </summary>
        public byte[] Content { get; set; }
    }
}
