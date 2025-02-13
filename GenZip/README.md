# GenZip

**GenZip** is a lightweight, generic library for creating ZIP archives from a collection of files in .NET. GenZip allows you to easily bundle various file types—such as Excel files, CSV files, text files, and more—into a single ZIP archive.

## Features

- **Generic File Support:** Bundle any file type by providing a filename and its content as a byte array.
- **Simple API:** A straightforward API that abstracts the complexities of ZIP archive creation.
- **Lightweight:** Minimal dependencies and overhead.
- **Flexible:** Easily integrate with your existing projects.

## Installation

Install **GenZip** via the .NET CLI:

```bash
dotnet add package GenZip
```

## Usage Examples

### 1. Creating a ZIP Archive with an Excel File


This example shows how to generate an Excel workbook using Aspose.Cells and create a ZIP archive containing that file.

```csharp
using System.IO;
using Aspose.Cells;
using GenZip;

public byte[] CreateZipWithExcelFile()
{
    // Generate an Excel workbook (using Aspose.Cells for example)
    Workbook workbook = new Workbook();
    // ... populate your workbook ...
    // Example: Adding a header and some data to the first worksheet
    var worksheet = workbook.Worksheets[0];
    worksheet.Cells["A1"].PutValue("Report Title");
    worksheet.Cells["A2"].PutValue("Date");
    worksheet.Cells["B2"].PutValue(DateTime.Now.ToShortDateString());

    // Add more cells or rows as needed to fill the workbook with your report data.
    // Save the workbook to a MemoryStream.
    byte[] excelBytes;
    using (var workbookStream = new MemoryStream())
    {
        workbook.Save(workbookStream, SaveFormat.Xlsx);
        excelBytes = workbookStream.ToArray();
    }

    // Create a file entry for the Excel file.
    var excelEntry = new ZipFileEntry
    {
        FileName = "Report.xlsx",
        Content = excelBytes
    };

    // Create the ZIP archive
    return ZipHelper.CreateZipArchive(new[] { excelEntry });
}
```


### 2. Creating a ZIP Archive with a CSV File

This example shows how to create a CSV file, convert it to a byte array, and bundle it into a ZIP archive

```csharp
using System.Text;
using GenZip;

public byte[] CreateZipWithCsvFile()
{
    // Create CSV content.
    string csvContent = "Name,Age,Location\nJemil,28,US\nAde,35,Nigeria";
    byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent);

    // Create a file entry for the CSV file.
    var csvEntry = new ZipFileEntry
    {
        FileName = "data.csv",
        Content = csvBytes
    };

    // Create the ZIP archive.
    return ZipHelper.CreateZipArchive(new[] { csvEntry });
}
```

### 3. Creating a ZIP Archive with Multiple Files

This example shows how to bundle multiple file types (Excel, CSV, and a text file) into a single ZIP archive.

```csharp
using System.Text;
using Aspose.Cells;
using System.IO;
using GenZip;
using System.Collections.Generic;

public byte[] CreateZipWithMultipleFiles()
{
    var fileEntries = new List<ZipFileEntry>();

    // Excel file
    Workbook workbook = new Workbook();
    // ... populate your workbook ...
    // Example: Adding a header and some data to the first worksheet
    var worksheet = workbook.Worksheets[0];
    worksheet.Cells["A1"].PutValue("Report Title");
    worksheet.Cells["A2"].PutValue("Date");
    worksheet.Cells["B2"].PutValue(DateTime.Now.ToShortDateString());

    // Add more cells or rows as needed to fill the workbook with your report data.

    byte[] excelBytes;
    using (var excelStream = new MemoryStream())
    {
        workbook.Save(excelStream, SaveFormat.Xlsx);
        excelBytes = excelStream.ToArray();
    }
    fileEntries.Add(new ZipFileEntry { FileName = "Report.xlsx", Content = excelBytes });

    // CSV file
    string csvContent = "Name,Age,Location\nJemil,20,US\nAde,22,Nigeria";
    byte[] csvBytes = Encoding.UTF8.GetBytes(csvContent);
    fileEntries.Add(new ZipFileEntry { FileName = "data.csv", Content = csvBytes });

    // Text file
    string textContent = "I break stuffs.";
    byte[] textBytes = Encoding.UTF8.GetBytes(textContent);
    fileEntries.Add(new ZipFileEntry { FileName = "breakStuffs.txt", Content = textBytes });

    // Create and return the ZIP archive.
    return ZipHelper.CreateZipArchive(fileEntries);
}
```
