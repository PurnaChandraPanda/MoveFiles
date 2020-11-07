# MoveFiles
This project outlines easy special case of filtering file from source folder (could have many sub-folders within) and moving them to a destination folder.

>>PS C:\> .\MoveFiles.Main.exe
```
MoveFiles.Main.exe
      -toFilterFile YourCSVFilePath
      -filterKind YourSearchCriteria [equals | contains | !equals | !contains]
      -source YourSourceFolderPath
      -destination YourDestinationFolderPath
```

# Download utility
- Get the utility downloaded from [latest-release](https://github.com/PurnaChandraPanda/MoveFiles/releases) section.
- Download [MoveFiles.zip] from latest section.
- Go to downloaded folder. Right click the zip -> Extract All -> go with default path (or update if some other path required) -> Extract.
- Launch PS or CMD -> cd to the earlier extracted directory.

# How to execute?
1. Define filter
    - Create a CSV file
    - Include contents
        | FileName  | 
        | :--:| 
        | foo  | 
        | bar  |
        | pixel  |
2. Define filter condition
    - equals: if like to pull files that equal the file names in soure folder
    - contains: if like to pull files that contains the file names (partial names) in soure folder
    - !equals: if like to pull files that does not equal the file names in soure folder
    - !contains: if like to pull files that does not contain the file names (partial names) in soure folder
3. Define source folder
    - Folder path of root where all the required files kept directly under or subfolders within
4. Define destination folder
    - Folder path where you like the files be copied over to 

Note: There is a structure added in [sample-images](https://github.com/PurnaChandraPanda/MoveFiles/tree/master/sample-images) folder. You could refer to review the samples.

# Run
- Use PS or cmd prompt to run utility
- if PS:
    ```
    >>.\MoveFiles.Main.exe -toFilterFile "C:\temp\images\filter\ToFilterFiles1.csv" -filterKind contains -source "C:\temp\images\source" -destination "C:\temp\images\target"
    ```
- if cmd:
    ```
    >>MoveFiles.Main.exe -toFilterFile "C:\temp\images\filter\ToFilterFiles1.csv" -filterKind contains -source "C:\temp\images\source" -destination "C:\temp\images\target"
    ```
- Review the output in destination folder



