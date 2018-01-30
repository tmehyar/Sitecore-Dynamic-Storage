# Sitecore-Dynamic-Storage
Library that allows storing KeyValue data to Core DB with caching.
For usage examples see 'SitecoreDynamicStorage.Examples' Project

# Features:
- Dynamically add, update and delete Key-Value records that get saved in core DB
- Data gets cached on first retrieval so you don't have to worry about excessive DB hits
- Can subscribe to Added, Updated or Retrieved events on certain keys
- DynamicModel that allows usage of this module in a statically typed and named manner

# Setup:
- Please executed the 'CreateTableScript.sql' on Sitecore's core DB
- Make sure the core DB connection string's name is 'core' otherwise edit the connection string's name in 'SitecoreDynamicStorage.DataAccess.DynamicStorage.designer.cs' from "core" to your connection string's name.
- Update lib/Sitecore.Kernel.dll to the version you're using
- You will only have to add a reference to 'SitecoreDynamicStorage.Core' in your project

# Notes:
- Working with dynamically typed and named variables is a bit risky due to its runtime evaluation nature. You could have bugs that do not appear during compiling, so you'll have to be carefully when using it. You can also overcome this drawback by using the DynamicModel but it requires some extra coding (few lines of code)
