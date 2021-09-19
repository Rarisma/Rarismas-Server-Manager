# RSM
IMAGE HERE
RSM is an open source server hosting tool written in C# which aims to make server hosting easy and accessible to everyone. To do this RSM attempts to automate as much as it can, and handles tasks such as EULAs, Port Forwarding and installing tools like Java. This means that ideally anyone with a sufficently powerful computer should be able to get a server up and running in under 10 minutes. 

###FAQ

##### How do I Install RSM?
You need to go to the releases tab and download the latest release

WARNING:
Due to RSM using a bleeding edge UI it cannot yet be distributed as a standard exe, for now you will need to run install.ps1 to install RSM. It will probably complain about it being an untrusted certificate this is because I cannot afford a code signing certificate so if you do not feel safe trusting the certificate I suggest you build RSM yourself or wait until the .exe version releases.  

#### How do I build RSM?
Open it in Visual Studio 2019 / 2022 and install Project Reunion / WinUI 3 and build the project called RSMUltra, not RSMUltra (packaged) unless you want to distribute your own build of RSM.

#### Who made RSM
RSM is made by Rarisma, however RSM would not be possible without the following forge team, the paper team, the TSHOCK team, Mojang, Microsoft, the Factorio team, Relogic and other Terraria developers,the Bukkit team, Anuken, Mono.NAT developer, the people who tested RSM and many many more.

#### What was RSM made in
RSM was originally made in WPF, it then changed to Avalonia for 2.0 in an attempt to run on Linux however this ultimately failed so I eventually switched to WinUI as I wanted to learn and thus RSM 3.0 was made made.
