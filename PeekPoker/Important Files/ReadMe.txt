Last Update - 30/06/2012 by PureIso
==============================================================
NB: Update source file before you commit.
NB: Update source file before you commit.
NB: Update source file before you commit.
NB: Update source file before you commit.
==============================================================
Revision 7
Release date: 30/06/2012


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

- changes 24/04/12
added feature requested by fairchild - reading the ip address from the registry if it does exist
	- though it will look up the value only if the sdk is installed and it will save the ip to the text file
	- and it will prefer the ip from the text file
added new experimental search function "Ex2SearchHexString" which is set as a default when you press the experimental button
added color change for the values that were changes after pressing the refresh button in the search tab

-Changes 25/04/12
added Resident evil - select game
added stop search function
fixed - threading problems. I tested most of teh functions
Bug - The refresh button seems to return null value for Item value
Repaired hex2dec, now actually works.

-Change 29/04/12
Added log to the application
Removed Dumping tab

-Changes 1/05/12
[8Ball]
Created Trainer menuitem
Added some Skyrim trainer codes for testing - [complete success]
Changed Writememory to public void to accomodate trainers.

NB: Nice log.
[/8Ball]

- changes 05/05/12
added Resonance of Fate trainer editing the Buy Menu (thanks to mariokart64n's research in for this)
	- just open the buy menu ingame and then select the option you want to add
	- wait till it's done and refresh the screen by scrolling down (ingame) to see what items are in the buy menu now (amount is set to 999 and price to 1)
	- buy or leave the menu to select another option
added scrollability to the logbox.. so now it will always scroll to the newest entry on update

- changes 06/05/12
fixed - Resoncance of fate trainer whne pressing on the colored hex option it didn't choose the right code

- changes 23/05/12
Added trainer utility for managing trainer files.

- changes 25/05/12
Fix - Select Game - How to use, now displays messagebox.

- changes 27/05/12
Added:
-Scan trainer files and add their codes to the list.
-Improve utility 'idiot proofing' and general features.

-changes 15/06/12
[PureIso]
Added: 
-Conversion code

-changes 20/06/12
[PurIso]
Added: 
-Added Dump Tab with controls

-changes 21/06/12
[PurIso]
Finished the dump coding and added some log text
UI clean up, Games removed(Full trainer left)

-changes 24/06/12
[cornnatron]
Added: 
-Added dumpLength to dump tab controls
-Added freeze/unfreeze buttons to dump tab
[PureIso]
Added & Finished Plugin,Code clean up

-changes 25/06/12
[PureIso]
More code cleanup
-Added quick calcultor to dump section
-Added plugin tutorial

-changes 28/06/12
[PureIso]
-Added more classes to the PeekPoker.interface to aid with plugin application coding
-Fixed the svn (release to trunk)
-Fixed the folder so peekpoker contains peekpoker.interface

-changes 30/06/12
[PureIso]
-Fixed peek exception handler
-Version 7 Release

-changes 07/07/12
[PureIso]
-Fixed freeze and Unfreeze exceptions
-Added extra constructor to RealTimeMemory Class - Interface
-Added get plugin Icon

-changes 12/07/12
[PureIso]
Cleanup
-Version 7.1.6 Release

-changes 13/09/12
[PureIso]
-Fixed RealTimeMemory poke bug

TODO:
-Add game id recognition to help user name trainer file correctly