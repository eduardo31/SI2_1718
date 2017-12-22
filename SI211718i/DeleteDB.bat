echo off
echo Running sql test scripts from group 

sqlcmd -i %cd%\SI211718i\alinea_B_Drop.sql

set /p delExit=Press the ENTER key to exit...: