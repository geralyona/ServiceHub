Application monitors the file with periodic refresh
FilePath and Period can be set with app config or command line like:

ServiceHub.exe -f testrss.json

In the main project there are test files:
test.json - just some json file, specified in configuration by default
testrss.json - RSS json sample

Current implementation handles different json types, which may be extended:
* RSS
* JSON (tree view)
* text (not valid JSON)

More tests should be added, for example for builders

It may be usefull to add UI for the source file selection

It is possible to add new JSON format displaying by adding another builder, vm view, and registering the builder in DI

It is expected the json source may be not only a file

It is possible to add multiple files monitoing in a single app

To improve new VM displaying perfomance, we can adjust existing vm with file changes in source instead of rebuiulding from scratch

For tree view it is possible to apply json attributes, like menu, with templates/styles to display a menu, not changing the VM