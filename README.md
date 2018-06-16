# WatchMovement

This project serves to emulate watch movements depicting various astronomical phenomena, such as extended precision moon phase and in the future, other similar events occurring in the universe. The goal is to emulate a mechanical watch movement, with all the inherent inaccuracies and limitations.

The code emulates 135 and 90 notched moon phase disks on a mechanical watch using realistic gear logic. The supporting gear ratios for each disk results in exactly 122 years of non stop accuracy before the disk is 1 day behind of the real moon phase. It is possible to model other gear trains with different ratios to emulate other astronomical events.

Right now, the project contains only logic as a C# console application, there is no interface or output display. Use the debugger in Visual Studio to see what is happening. The logic involves defining simple and compound gears with teeth which drive other downstream gears. If the number of teeth are wrong, the moon phase disk will move incorrectly!

I want to introduce graphics similar to a real moon phase disk. The idea is to depict what an extended precision moon phase disk might look like after 100+ years, right down to the number of revolutions of individual gears within the gear train since no mechanical watch ever will stay running for that long due to required service and "forgetting" to wind the movement.

This is one of my many hobby projects. I am looking for collaborators who might be interested to contribute, especially in the area of graphics.

