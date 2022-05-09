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

            var path = @"C:\users\Arekk\Desktop\DigiSigns\";

            string[] fileEntries = Directory.GetFiles(path);
            Dictionary<string, PdfDocument> directoryPdfs = new Dictionary<string, PdfDocument>();
            int i = 0;

            foreach (var item in fileEntries)
            {
                Console.WriteLine(++i + ": Loaded: " + item);
                PdfDocument pdfDocument = new PdfDocument(new PdfReader(item));
                directoryPdfs.Add(item, pdfDocument);
            }

            Console.WriteLine();
            i = 0;
            foreach (KeyValuePair<string, PdfDocument> item in directoryPdfs)
            {
                List<string> signatureAuthors = new List<string>();

                if (HasSignatures(item.Value))
                {
                    List<string> signatureNames = (List<string>)new SignatureUtil(item.Value).GetSignatureNames();

                    foreach (var sign in signatureNames)
                    {
                        var signature = new SignatureUtil(item.Value).GetSignature(sign);
                        signatureAuthors.Add(signature.GetName());
                    }
                 
                }

                

                Console.Write(++i + ": Is document " + item.Key + " signed? " + HasSignatures(item.Value));
                Console.Write(" Authors: ");
                foreach (var author in signatureAuthors)
                {
                    Console.Write(author + " ");
                }

                Console.WriteLine();
            }


            bool HasSignatures(PdfDocument pdfDocument)
            {
                List<string> signatureNames = (List<string>)new SignatureUtil(pdfDocument).GetSignatureNames();
                return signatureNames.Any();
            }
        }
    
    }
}
