Last Update - 17/04/12 by 8Ball
==============================================================
NB: Make sure the ISOLib DLL is in the Important Files Folder.
NB: Update source file before you commit.
==============================================================
Revision 6
Release date: Unreleased


-Changes - 12/04/12
IP address loading - 8Ball
code clean up - pureIso

-Changes - 13/04/12
Search Range coded - Not tested - pureIso
Automatic addressbox formatting - 8Ball

-Changes - 14/04/12
Search Range coded searching forever bugfix - pureIso
Clearing SearchListView box bugfix - pureIso
search range result bugfix
added RealTimeMemory class - trying to slowly decrease ISOLib dependancy
Addressbox formatting improvements and peek checks -8Ball

To Do: 
Peek Length box should be able to take hex string? [Done] -8Ball
Message to tell user pointer not found or a notification
Add a loading bar ?


- Changes - 16/04/12
Combined the peek&poke textbox together in to one hexbox using (Be.hexbox with some modifications)
changed the functions and events to work with the new hexbox
added teh Be.Windows.Forms.HexBox.dll to the Important Files directory
added 2 new functions
+ ByteArrayToString(byte[] bytes)
+ StringToByteArray(string text)

Remove IsoLib Dependancy
Added the Function class where all the functions goes into
Added the IO class for reading - feel free to edit it for better algorithm
Cleaned up the realtimememory class for somethings we don't need
Transfered some of the functions in the MainForm.cs to the Function class
Added random future stuff to the UI

- Changes - 17/04/12
Added support for "h" in peeklength box, now supports 0x or h to indicate hex.
Changed calculator to automatically calculate when text is entered, added checks.
Added context menu to Listview to enable copying addresses. [Incomplete, uncoded]

- Changes 18/04/12
added a few conversion functions in the Functions.cs
	((u)int16,32, float & double to bytes array and vice versa)
added target address (Selected Address label) in the peek&poke tab 
added values numerics for (U)Int8, (U)Int16 & (U)Int32 
	(still need to add events so one can edit inside those numerics)
added a checkbox to change between signed and unsigned values

added search range list view code - tested - pureIso
added group box to make the UI cleaner
added option to select search range value base: hex or dec - [Incomplete, uncoded]
added option to select Search range end type: by offset or by length - [Incomplete, uncoded]
added marquee progress bar for search range: [completed but not tested]
code clean up with a little more documentation
*******Added context menu to Listview to enable copying addresses. [Removed]

changed how the progressbar works... (it didn't really work in the first place... no delegates)
	no longer marquee but it behaves like a normal progressbar
added UpdateProgressbar(int min, int max, int value, string text) delegate to the MainForm.cs
added Eventhandler to the RWStream.cs and RealTimeMemory.cs tp handle the progressbar updates
added events to _rtm(in the MainForm.cs) and readWriter(in RealTimeMemory.cs) variables
added KeyUp event to the searchRangeValueTextBox to handle enter/return keys 
set the combobox selectedindex for the 2 comboboxes (search tab)
also corrected the range canlculation for the Range/length textbox (search tab)

- changes 20/04/12
added experimental search function (its faster... but the performance depends on the search pattern)
expanded the result listview with the values
	(the values are only shown if the experimental search is used didn't have the time to add it to the defualt search funtion)
added refresh button to the search tab it will refresh the values in the result list
changed the lick event for the listview it now add "0x" to the string
added new file in the Functions Folder... this file will contain List types atm it contains only one type though
