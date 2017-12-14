echo off
echo Running sql test scripts from group 

sqlcmd -i %cd%\SI211718i\alinea_A_CreateTable.sql
sqlcmd -i %cd%\SI211718i\alinea_C.sql
sqlcmd -i %cd%\SI211718i\alinea_D.sql
sqlcmd -i %cd%\SI211718i\alinea_E.sql
sqlcmd -i %cd%\SI211718i\alinea_F.sql
sqlcmd -i %cd%\SI211718i\alinea_G.sql
sqlcmd -i %cd%\SI211718i\alinea_H_tests.sql
sqlcmd -i %cd%\SI211718i\alinea_I.sql
sqlcmd -i %cd%\SI211718i\alinea_J.sql
sqlcmd -i %cd%\SI211718i\alinea_k.sql
sqlcmd -i %cd%\SI211718i\alinea_L.sql
sqlcmd -i %cd%\SI211718i\alinea_M.sql
sqlcmd -i %cd%\SI211718i\alinea_N.sql
sqlcmd -i %cd%\SI211718i\alinea_O_tests.sql
sqlcmd -i %cd%\SI211718i\alinea_B_Drop.sql

set /p delExit=Press the ENTER key to exit...: