using ScanShipper.Models;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanShipper.App
{
    public class Compressor
    {

        private string mainDir;

        public Compressor() {

             mainDir = Path.Combine(FileSystem.Current.AppDataDirectory,"ScanShipper");

        }

        private const int ChunkSize = 2048; // 2KB in bytes

        public List<ScanShipperPayload> CompressFile(string sourceFilePath, string fileName)
        {
            var res = new List<ScanShipperPayload>();

            Byte[] bytes = File.ReadAllBytes(sourceFilePath);
            String b64 = Convert.ToBase64String(bytes);

            var size= ASCIIEncoding.Unicode.GetByteCount(b64);

            if (size > ChunkSize) {

                // Needs Compression
                decimal aa = size / ChunkSize;
                var x = (int)Math.Ceiling(aa);


                var chunks = DivideStringIntoChunks(b64,x);
                int i = 1;
                foreach (var chunk in chunks) {

                    res.Add(new ScanShipperPayload() { 
                    
                        sizeInBytes=size,
                        chunck=i,
                        totalChuncks=chunks.Count(),
                        data=chunk,
                        fileName=fileName
                    
                    });
                
                
                }


            } else {
                // Small Enough
                res.Add(new ScanShipperPayload()
                {

                    sizeInBytes = size,
                    chunck = 1,
                    totalChuncks = 1,
                    data = b64,
                    fileName = fileName

                });
            }
            return res;
        }

        public static List<string> DivideStringIntoChunks(string input, int n)
        {
            // Create a list to hold the chunks
            List<string> chunks = new List<string>();

            // Check for invalid input
            if (string.IsNullOrEmpty(input) || n <= 0)
            {
                throw new ArgumentException("Input string must not be null or empty, and n must be a positive integer.");
            }

            // Calculate the chunk size
            int totalLength = input.Length;
            int chunkSize = totalLength / n;
            int remainingLength = totalLength % n;

            // Start dividing the string into chunks
            int startIndex = 0;
            for (int i = 0; i < n; i++)
            {
                int currentChunkSize = chunkSize + (i < remainingLength ? 1 : 0);
                if (startIndex + currentChunkSize > totalLength) currentChunkSize = totalLength - startIndex;

                // Extract a chunk and add it to the list
                chunks.Add(input.Substring(startIndex, currentChunkSize));

                // Update the start index for the next chunk
                startIndex += currentChunkSize;
            }

            return chunks;
        }
    }

}

