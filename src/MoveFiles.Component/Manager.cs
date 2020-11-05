using MoveFiles.Component.Helper;
using MoveFiles.Component.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MoveFiles.Component
{
    public class Manager
    {
        private static Criteria _criteria;
        private static IDictionary<string, string[]> _filePathCollection;

        static Manager()
        {
            _filePathCollection = new Dictionary<string, string[]>();
        }

        public static async ValueTask<bool> Validate(string[] arguments)
        {            
            if (arguments.Length == 0)
            {
                await ArgumentParser.GeneralUsage();
                return false;
            }

            await Logger.MessageLog("Arguments validation started");
            _criteria = await ArgumentParser.Parse(arguments);
            await Logger.MessageLog("Arguments validation completed");
            return (_criteria != null) ? true : false;
        }

        public static async ValueTask CopyFiles()
        {
            await GetSearchedFiles();
            await MoveTheFiles();            
        }

        private static async ValueTask GetSearchElements()
        {
            try
            {
                await Logger.MessageLog("Search criteria read started");

                using (var streamReader = new StreamReader(_criteria.FilterFor))
                {
                    string currentLine;

                    while ((currentLine = streamReader.ReadLine()) != null)
                    {
                        if (currentLine.ToLower().Equals("filename"))
                            continue;
                        
                        // Add the key item to dictionary
                        _filePathCollection.Add(currentLine, null);
                    }
                }

                await Logger.MessageLog("Search criteria read completed");
            }
            catch (Exception ex)
            {
                await Logger.ErrorLog(ex.ToString());
            }

            return;
        }

        private static async ValueTask GetSearchedFiles()
        {
            IDictionary<string, string[]> sourceFileDictionary = null;

            try
            {
                await Logger.MessageLog("Get searched files operation started");

                // Invoke to get manager level dictionary ready with list of Keys
                await GetSearchElements();

                // Get all the subfolder file paths
                var sourceFileMap = Directory.GetFiles(_criteria.Source, "*", SearchOption.AllDirectories);

                // Convert sourceFileMap to a dictionary of (key as filname, value as filepath)
                WrapFileMapToCollection(sourceFileMap, out sourceFileDictionary);

                // Loop through the items of file path collection and compare with file collection
                // Update key value pair for required filename and filepath
                foreach (var filePathPair in _filePathCollection.ToList())
                {
                    string[] sourceValues = new string[] { };
                    foreach (var sourceKey in sourceFileDictionary.Keys)
                    {
                        // if (sourceKey.StartsWith(filePathPair.Key))
                        if (FilterCheckFlag(sourceKey, filePathPair.Key))
                        {
                            var oldSourceValuesLength = sourceValues.Length;
                            var newSourceValue = sourceFileDictionary[sourceKey];                            
                            Array.Resize(ref sourceValues, oldSourceValuesLength + newSourceValue.Length);
                            Array.Copy(newSourceValue, 0, sourceValues, oldSourceValuesLength, newSourceValue.Length);
                        }
                    }

                    if (sourceValues.Length > 0)
                    {
                        _filePathCollection[filePathPair.Key] = sourceValues;
                    }
                }

                await Logger.MessageLog("Get searched files operation completed");
            }
            catch (Exception ex)
            {
                await Logger.ErrorLog(ex.ToString());
            }
            finally
            {
                if (sourceFileDictionary != null)
                {
                    sourceFileDictionary.Clear();
                }
            }

            return;
        }

        private static async ValueTask MoveTheFiles()
        {
            try
            {
                await Logger.MessageLog("Move files operation started");


                Parallel.ForEach(_filePathCollection, fileMapped =>
                {
                    // return if no value available for key as part of the collection  
                    // and continue operate with rest of the items in collection
                    if (fileMapped.Value == null)
                        return;

                    foreach (var fileValue in fileMapped.Value)
                    {
                        // Making the operations thread-safe when there are too many File level operations
                        lock (fileValue)
                        {
                            string fileName = fileValue.Substring(fileValue.LastIndexOf('\\') + 1);
                            string destinationPath = $"{_criteria.Destination}\\{fileName}";

                            // Move deletes the source. So, Copy is best option here.
                            //Directory.Move(fileMapped, destinationPath);
                            File.Copy(fileValue, destinationPath, true);
                        }
                    }
                });

                await Logger.MessageLog("Move files operation completed");
            }
            catch (Exception ex)
            {
                await Logger.ErrorLog(ex.ToString());
            }

            return;
        }

        private static string GetFileName(string filePath)
        {
            int startIndex = filePath.LastIndexOf('\\') + 1;
            int extractedLength = filePath.LastIndexOf('.') - startIndex;
            return filePath.Substring(startIndex, extractedLength);
        }

        private static void WrapFileMapToCollection(string[] sourceFileMap, out IDictionary<string, string[]> sourceCollection)
        {
            sourceCollection = new Dictionary<string, string[]>();
            
            foreach (var sourceFile in sourceFileMap)
            {
                var extractedName = GetFileName(sourceFile);
                string[] sourceValue = new string[] { sourceFile };
                string[] extractedValue = null;
                if (sourceCollection.TryGetValue(extractedName, out extractedValue))
                {
                    Array.Resize(ref extractedValue, extractedValue.Length + 1);
                    extractedValue[extractedValue.Length - 1] = sourceFile;
                    sourceCollection[extractedName] = extractedValue;
                }
                else
                {
                    sourceCollection.Add(extractedName, sourceValue);
                }                
            }

            return;
        }

        private static bool FilterCheckFlag(string sourceKey, string targetKey)
        {
            bool filterFlag = false;

            if(_criteria.FilterKind.Equals(FilterCriteria.Equals))
            {
                filterFlag = sourceKey.Equals(targetKey);
            }
            else if (_criteria.FilterKind.Equals(FilterCriteria.Contains))
            {
                filterFlag = sourceKey.Contains(targetKey);
            }
            else if (_criteria.FilterKind.Equals(FilterCriteria.NotEquals))
            {
                filterFlag = !sourceKey.Equals(targetKey);
            }
            else if (_criteria.FilterKind.Equals(FilterCriteria.NotContains))
            {
                filterFlag = !sourceKey.Contains(targetKey);
            }

            return filterFlag;
        }
    }
}
