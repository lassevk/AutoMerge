# Read me

This project aims to create a small tool that can handle some merge conflicts in some file types automagically.

## Example

As an example of the type of merge conflict I am planning on looking at adding is when you add files to a .csproj
project file in two branches and then merge.

Since the additions are in the same place, but the file is .XML, there is enough common between the lines (the xml
syntax) that the git merge gets confused and matches up lines that then look modified in both branches.

Instead the automatic merge would treat both as additions, provided it does enough checks to ensure this
is safe.
