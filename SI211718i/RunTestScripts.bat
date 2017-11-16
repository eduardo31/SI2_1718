echo off
echo Running sql test scripts from group 

sqlcmd -i %cd%\SI211718i\alinea_A_CreateTable.sql
sqlcmd -i %cd%\SI211718i\alinea_C.sql
sqlcmd -i %cd%\SI211718i\alinea_D.sql
sqlcmd -i %cd%\SI211718i\alinea_E.sql
sqlcmd -i %cd%\SI211718i\alinea_F.sql
sqlcmd -i %cd%\SI211718i\alinea_G.sql
sqlcmd -i %cd%\SI211718i\alinea_O_tests.sql
sqlcmd -i %cd%\SI211718i\alinea_B_Drop.sql

set /p delExit=Press the ENTER key to exit...: