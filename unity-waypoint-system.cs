using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    [MenuItem("Tools/Waypoint Editor")]
     public static void open()
     {
        GetWindow<WaypointManager>();
     }

    public Transform waypointRoot;


    private void OnGui()
    {
        SerializedObjeckt obj = new SerializedObjeckt(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if(waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform mus be selected. please assing a root transform.", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.EndVertical();
            DrawButtons();
        }
        obj.ApplyModifiedProperties();

    }

    void DrawButtons()
    {
        if(GUILayout.("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }

    void CreateWaypoint();
    {
        GameObject waypointObjekt = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObjekt.transform.SetPArent(waypointRoot, false);

        Waypoint waypoint = waypointObjekt.getComponent<Waypoint>();
        if(waypointRoot.childCount > 1)
        {
            waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
            waypoint.previousWaypoint.nextWaypoint = waypoint
                //Place the waypoint at the last position
             waypoint.transform.position = waypoint.previousWaypoint.transform.position;
            waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
        }

        Selection.activeGameObject = waypoint.gameObjekt
    }
}