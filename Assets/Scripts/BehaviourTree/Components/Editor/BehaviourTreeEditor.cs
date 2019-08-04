using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public class BehaviourTreeEditor : EditorWindow
{
    private const string PROP_ROOT_NODE = "rootNode";

    public Node rootNode;
    private Context context;
    private Vector2 scrollPosition;


    [MenuItem("Window/Behaviour Tree Editor")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(BehaviourTreeEditor)).Show();
    }

    private void OnGUI()
    {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        context = Selection.activeGameObject?.GetComponent<Brain>()?.context;
        rootNode = EditorGUILayout.ObjectField(rootNode, typeof(Node), false) as Node;

        if (rootNode != null)
        {

            rootNode.developerDescription =
                EditorGUILayout.TextField("Description", rootNode.developerDescription);
            DisplayNode(rootNode);
        }

        EditorGUILayout.EndScrollView();
    }

    private void DisplayNode(Node node)
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.Space();

        if (context != null)
        {
            string status = "None";
            if (context.nodeStateDict.ContainsKey(node.GetInstanceID()))
            {
                status = context.nodeStateDict[node.GetInstanceID()].ToString();
            }
            EditorGUILayout.LabelField("Status: " + status.ToString());
        }
        else
        {
            if (node != null)
            {
                SerializedObject serializedObject = new SerializedObject(node);
                System.Type type = node.GetType();
                foreach (FieldInfo item in type.GetFields())
                {
                    if (item.FieldType == typeof(ContextName))
                    {
                        SerializedProperty property = serializedObject.FindProperty(item.Name);
                        EditorGUILayout.PropertyField(property);
                    }
                }
                serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUILayout.Space();

        if (node is Composite)
        {
            EditorGUI.indentLevel++;
            Composite composite = (Composite)node;
            int possibleSize = EditorGUILayout.DelayedIntField("Count", composite.nodeList.Count);
            if (0 <= possibleSize)
            {
                int difference = composite.nodeList.Count - possibleSize;
                if (composite.nodeList.Count > possibleSize)
                {
                    composite.nodeList.RemoveRange(possibleSize, difference);
                }
                else
                {
                    difference *= -1;
                    composite.nodeList.AddRange(new Node[difference]);
                }
            }

            for (int index = 0; index < composite.nodeList.Count; index++)
            {
                composite.nodeList[index] = EditorGUILayout.ObjectField(
                    composite.nodeList[index],
                    typeof(Node),
                    false) as Node;
                DisplayNode(composite.nodeList[index]);
            }
            EditorGUI.indentLevel--;
        }
        else if (node is Decorator)
        {
            EditorGUI.indentLevel++;
            Decorator decorator = (Decorator)node;
            decorator.node = EditorGUILayout.ObjectField(
                decorator.node,
                typeof(Node),
                false) as Node;
            DisplayNode(decorator.node);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.EndVertical();
    }
}
