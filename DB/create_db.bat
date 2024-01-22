ECHO off

sqlcmd -S E67\SQLEXPRESS -E -i C:\Users\67Eas\source\repos\CaughtOrNot\DB\caughtornotdb.sql

rem server is localhost

ECHO . 
ECHO if no errors appear DB was created
PAUSE 