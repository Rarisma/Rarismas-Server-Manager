#Use this to build RSM if you arent on windows
#Remember to check the PUBLISH directory inside of the bin
 dotnet publish -c Release --self-contained true -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true --version-suffix 2.1 --runtime Windows-x64
