using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Core.Helpers;

public static class PdfGeneratorService
{
    public static byte[] CreatePdf(List<List<string>> data, string title, List<string> headers)
    {
        using var memoryStream = new MemoryStream();
        using var writer = new PdfWriter(memoryStream);
        using var pdf = new PdfDocument(writer);
        using var document = new Document(pdf);

        document.Add(new Paragraph(title).SetTextAlignment(TextAlignment.CENTER));
        
        int numberOfColumns = headers.Count;
        Table table = new Table(numberOfColumns);
        
        foreach (var header in headers)
        {
            table.AddHeaderCell(new Cell().Add(new Paragraph(header)));
        }
        
        foreach (var row in data)
        {
            foreach (var value in row)
            {
                table.AddCell(new Cell().Add(new Paragraph(value)));
            }
        }

        document.Add(table);
        document.Close();

        return memoryStream.ToArray();
    }
}