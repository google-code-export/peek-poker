Last Update - 14/04/12 by 8Ball
==============================================================
NB: Make sure the ISOLib DLL is in the Important Files Folder.
NB: Update source file before you commit.
==============================================================
Revision 5
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
Peek Length box should be able to take hex string? [Awaiting Review] -8Ball
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