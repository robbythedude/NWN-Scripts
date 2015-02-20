//Toggle Light System
//Author: Rob Steiner (robbythedude@hotmail.com)
//Date: 2/20/15

/******PURPOSE******
//This scrip will toggle all lights on/off with the ID 'aToggleLight' based on
//the time of day.
*/

/******HOW-TO******
This script is to be executed on an placeables heartbeat function.
Right-click placeable -> Properties -> Scripts -> OnHeartbeat
*/

void main()
{
    //Grab first light to toggle on/off
    object oAToggleLight = GetObjectByTag("aToggleLight", 0);
    //Incrementor used for looping through lights
    int nLightInc = 0;

    //This will only run once to synchronize any toggle light
    if(GetLocalInt(OBJECT_SELF, "hasRanOnce") != 1)
    {
        SetLocalInt(OBJECT_SELF, "hasRanOnce", 1); //Recording this code has at least ran once

        if(GetIsDusk() || GetIsNight()) //If night, set to day so that Night time script runs.
            SetLocalInt(OBJECT_SELF, "IsNight", 0);
        else //If day, set to night so that Day time script runs.
            SetLocalInt(OBJECT_SELF, "IsNight", 1);
    }

    //This will set every lamp on once night
    if(GetLocalInt(OBJECT_SELF, "IsNight") == 0 && (GetIsDusk() || GetIsNight()))
    {
        SetLocalInt(OBJECT_SELF, "IsNight", 1); //Make sure calling object knows it is night time
        while(GetIsObjectValid(oAToggleLight))  //This will kick out once it increments to a null value (no more toggle lights)
        {
            PlayAnimation(ANIMATION_PLACEABLE_ACTIVATE);  //Set the animation of the toggled light
            SetPlaceableIllumination(oAToggleLight, TRUE);  //Will prepare for illuminating
            RecomputeStaticLighting(GetArea(oAToggleLight));  //Refreshes area to display enabled illumination
            nLightInc++;
            oAToggleLight = GetObjectByTag("aToggleLight", nLightInc); //Move to the next ToggleLight
        }//End of TurnOn While loop
    }//End of LampOnAtNight if
    //This will set every lamp off once day
    else if(GetLocalInt(OBJECT_SELF, "IsNight") == 1 && (GetIsDawn() || GetIsDay()))
    {
        SetLocalInt(OBJECT_SELF, "IsNight", 0); //Make sure calling object knows it is day time
        while(GetIsObjectValid(oAToggleLight))  //This will kick out once it increments to a null value (no more toggle lights)
        {
            PlayAnimation(ANIMATION_PLACEABLE_DEACTIVATE);  //Set the animation of the toggled light
            SetPlaceableIllumination(oAToggleLight, FALSE);  //Will prepare for illuminating
            RecomputeStaticLighting(GetArea(oAToggleLight));  //Refreshes area to display enabled illumination
            nLightInc++;
            oAToggleLight = GetObjectByTag("aToggleLight", nLightInc); //Move to the next ToggleLight
        }//End of TurnOff While loop
    }//End of LampOnAtNight if
}