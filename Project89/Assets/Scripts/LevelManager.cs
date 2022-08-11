using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelManager
{
    // Start is called before the first frame update
    public static int currentRoomsCount = 0;

    public static int currentRoomIndex = -1;
    public static int previousRoomIndex = -1;
    public static int currentDepth = -1;
    public static bool isGenerated = false;

    public static string currentWing;

    public static int[] leftWing;
    public static int[] rightWing;
    public static int[] frontWing;

    public static bool[] needEnemyLeftWing;
    public static bool[] needEnemyRightWing;
    public static bool[] needEnemyFrontWing;

    public static void ResetLevel()
    {
        currentRoomsCount = 0;
        currentRoomIndex = -1;
        previousRoomIndex = -1;
        currentDepth = -1;
        isGenerated = false;

        currentWing = null;

        leftWing = null;
        rightWing = null;
        frontWing = null;

        needEnemyLeftWing = null;
        needEnemyRightWing = null;
        needEnemyFrontWing = null;

    }

}
