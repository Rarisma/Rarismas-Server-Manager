@echo off
timeout /t 1 /nobreak
del "RSM.exe"
rename "RSMNew.exe" "RSM.exe"
RSM.exe