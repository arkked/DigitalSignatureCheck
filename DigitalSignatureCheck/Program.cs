using iText.Kernel.Pdf;
using iText.Signatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DigitalSignatureCheck
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Type a program path: ");
            var path = Console.ReadLine();  // C:\Users\arekk\Desktop\samplecertifiedpdf.pdf

            if (!File.Exists(path))
            {
                Console.WriteLine("File doesn't exist.");
               
            }

            PdfReader pdfReader = new PdfReader(path);
            PdfDocument pdfDocument = new PdfDocument(pdfReader);

            Console.WriteLine("Is document " + pdfDocument.GetDocumentInfo().GetTitle() + " signed? " + HasSignatures(pdfDocument));


            bool HasSignatures(PdfDocument pdfDocument)
            {
                List<String> signatureNames = (List<string>)new SignatureUtil(pdfDocument).GetSignatureNames();
                return signatureNames.Any();

            }
        }
    
    }
}
