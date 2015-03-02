//Author: Rob Steiner
//Date 3/1/15

//This is the persistent time system designed to keep a very accurate in-game date for the server.
//Below is a collection of scripts that must be pushed into their respective areas.

//---------------------------------------------------------------

// Author: Robert Steiner
// Date: 3/1/15

////////PURPOSE
// This is all the code to execute when the module first loads. Loading of
// the NWNX2 DB, setting the in-game date, etc takes place here.
////////

////////HOW-TO
// Make sure NWNX2 with ODBC is installed properly and working!
// Edit -> Module Properties -> Events -> OnModuleLoad
////////

#include "aps_include"

void setInGameDate();

void main()
{
    // Init placeholders for ODBC gateway
    SQLInit();
    setInGameDate();
}

//This function is used to set the in-game date correctly.
//Apart of the persistent time system
void setInGameDate()
{
    //Launching a script to essentially determine if the table exist
    SQLExecDirect("SELECT id FROM currentDate WHERE id = 1");

    if (SQLFetch() == SQL_SUCCESS) //Table does exist, update in-game date
    {
        //Local variables for ease of access
        int nYear, nMonth, nDay;
        object oModule = GetModule();

        //Attempting to pull the required dates from currentDate table
        SQLExecDirect("SELECT year FROM currentDate WHERE id = 1");
        SetLocalString(oModule, "NWNX!ODBC!FETCH", "-2147483647");
        nYear = StringToInt(GetLocalString(oModule, "NWNX!ODBC!FETCH"));
        SQLExecDirect("SELECT month FROM currentDate WHERE id = 1");
        SetLocalString(oModule, "NWNX!ODBC!FETCH", "-2147483647");
        nMonth = StringToInt(GetLocalString(oModule, "NWNX!ODBC!FETCH"));
        SQLExecDirect("SELECT day FROM currentDate WHERE id = 1");
        SetLocalString(oModule, "NWNX!ODBC!FETCH", "-2147483647");
        nDay = StringToInt(GetLocalString(oModule, "NWNX!ODBC!FETCH"));

        //Set the calendar date to the pulled currentDate values
        SetCalendar(nYear, nMonth, nDay);

    }
    else  //Table does not exist, so create it and populate it
    {
        SQLExecDirect("CREATE TABLE currentDate(id int,year int,month int,day int);");
        //Default date is 1/1/1370
        SQLExecDirect("INSERT INTO currentDate(id, year, month, day) VALUES (1, 1370, 1, 1)");
    }
}

//---------------------------------------------------------------

// Author: Robert Steiner
// Date: 3/1/15

////////PURPOSE
// This is all the code to execute when a player rests. Keeping persistent time,
// etc is found here.
////////

////////HOW-TO
// Make sure NWNX2 with ODBC is installed properly and working!
// Edit -> Module Properties -> Events -> OnPlayerRest
////////

#include "aps_include"

void saveInGameDate();

void main()
{
    saveInGameDate();
}

//This function will save the current in-game date to the database.
//Apart of the persistent time system
void saveInGameDate()
{
    SQLExecDirect("UPDATE currentDate SET year = " + IntToString(GetCalendarYear()) + ", month = " + IntToString(GetCalendarMonth()) + ", day = " + IntToString(GetCalendarDay()) + "WHERE id = 1");
}

//---------------------------------------------------------------

// Author: Robert Steiner
// Date: 3/1/15

////////PURPOSE
// This is all the code to execute when a player logins. Keeping persistent time,
// etc is found here.
////////

////////HOW-TO
// Make sure NWNX2 with ODBC is installed properly and working!
// Edit -> Module Properties -> Events -> OnClientEnter
////////

#include "aps_include"

void saveInGameDate();

void main()
{
    saveInGameDate();
}

//This function will save the current in-game date to the database.
//Apart of the persistent time system
void saveInGameDate()
{
    SQLExecDirect("UPDATE currentDate SET year = " + IntToString(GetCalendarYear()) + ", month = " + IntToString(GetCalendarMonth()) + ", day = " + IntToString(GetCalendarDay()) + "WHERE id = 1");
}

//---------------------------------------------------------------

// Author: Robert Steiner
// Date: 3/1/15

////////PURPOSE
// This is all the code to execute when a player logouts. Keeping persistent time,
// etc is found here.
////////

////////HOW-TO
// Make sure NWNX2 with ODBC is installed properly and working!
// Edit -> Module Properties -> Events -> OnClientLeave
////////

#include "aps_include"

void saveInGameDate();

void main()
{
    saveInGameDate();
}

//This function will save the current in-game date to the database.
//Apart of the persistent time system
void saveInGameDate()
{
    SQLExecDirect("UPDATE currentDate SET year = " + IntToString(GetCalendarYear()) + ", month = " + IntToString(GetCalendarMonth()) + ", day = " + IntToString(GetCalendarDay()) + "WHERE id = 1");
}