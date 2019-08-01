// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
#endif
using UnityEngine;

public abstract class Node : ScriptableObject
{
#if UNITY_EDITOR
    #region Editor GUI
    public const float DEFAULT_WIDTH = 200f;
    public const float DEFAULT_HEIGHT = 72f;

    [Header("Debug details")]
    public float lastStartTime;
    public NodeState nodeState;

    [Header("Unity GUI Editor")]
    public Rect rect = new Rect(0f, 0f, 200f, 72f);
    public GUIStyle defaultNodeStyle = new GUIStyle();
    public GUIStyle selectedNodeStyle = new GUIStyle();
    public GUIStyle inPointStyle;
    public GUIStyle outPointStyle;

    private NodeUI nodeUI;

    public void Setup()
    {
        Setup(Vector2.zero);
    }


    public void Setup(Vector2 offset)
    {
        defaultNodeStyle = new GUIStyle();
        defaultNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        defaultNodeStyle.border = new RectOffset(12, 12, 12, 12);

        selectedNodeStyle = new GUIStyle();
        selectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        selectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPointStyle = new GUIStyle();
        inPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        inPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        inPointStyle.border = new RectOffset(4, 4, 12, 12);

        outPointStyle = new GUIStyle();
        outPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        outPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        outPointStyle.border = new RectOffset(4, 4, 12, 12);

        nodeUI = new NodeUI(this);

        rect.position = offset;
        Composite composite = this as Composite;
        Decorator decorator = this as Decorator;

        if (composite != null)
        {
            List<Node> nodeList = composite.nodeList;
            float HALFHEIGHT = DEFAULT_HEIGHT / 2f;
            float inc = DEFAULT_HEIGHT * 1.5f;
            Vector2 newOffset = offset + new Vector2(
                DEFAULT_WIDTH * 1.5f, 
                rect.y - ((nodeList.Count - 1) * HALFHEIGHT));
            foreach (Node item in nodeList)
            {
                item.Setup(newOffset);
                newOffset.y += inc;
            }
        }
        else if (decorator != null)
        {
            Vector2 newOffset = offset + new Vector2(
                DEFAULT_WIDTH * 1.5f,
                rect.y);
            decorator.node.Setup(newOffset);
        }
    }

    public void DrawGUI(Context context)
    {
        nodeUI.Draw(context);

        Composite composite = this as Composite;
        Decorator decorator = this as Decorator;

        if (composite != null)
        {
            List<Node> nodeList = composite.nodeList;
            foreach (Node item in nodeList)
            {
                item.DrawGUI(context);
            }
        }
        else if (decorator != null)
        {
            decorator.node.DrawGUI(context);
        }
    }

    public void Drag(Vector2 delta)
    {
        nodeUI.Drag(delta);

        Composite composite = this as Composite;
        Decorator decorator = this as Decorator;

        if (composite != null)
        {
            foreach (Node item in composite.nodeList)
            {
                item.Drag(delta);
            }
        }
        else if (decorator != null)
        {
            decorator.node.Drag(delta);
        }
    }
    #endregion
#endif

    /* Delegate that returns the state of the node.*/
    public delegate NodeState NodeReturn();

    /* The constructor for the node */
    public Node() { }

    /* Implementing classes use this method to evaluate the desired set of conditions */
    public abstract NodeState Evaluate(Context context);

    protected void SetNodeState(Context context, NodeState nodeState)
    {
#if UNITY_EDITOR
        if (this == context.rootNode)
        {
            context.nodeStateDict.Clear();
        }

        context.nodeStateDict[this.GetInstanceID()] = nodeState;
#endif
    }
}

//#if UNITY_EDITOR
//#region Editor GUI
//// from http://gram.gs/gramlog/creating-node-based-editor-unity/
public class NodeUI
{
    private Node node;
    public const float MARGIN = 12.0f;
    private Vector2 MARGIN_VECTOR = new Vector2(MARGIN, MARGIN);

    public string title;
    public bool isDragged;
    public bool isSelected;

    //public ConnectionPoint inPoint;
    //public ConnectionPoint outPoint;

    public GUIStyle style;

    public Action<NodeUI> OnRemoveNode;

    public NodeUI(Node node)
    {
        this.node = node;
        this.style = node.defaultNodeStyle;
    }

    public void Drag(Vector2 delta)
    {
        node.rect.position += delta;
    }

    public void Draw(Context context)
    {
        Rect usableSpace = new Rect(node.rect);
        Rect nodeTypeRect = new Rect(usableSpace);
        nodeTypeRect.position += MARGIN_VECTOR;
        nodeTypeRect.width -= (2 * MARGIN);
        nodeTypeRect.height = EditorGUIUtility.singleLineHeight;

        //inPoint.Draw();
        //outPoint.Draw();

        GUI.Box(usableSpace, title, style);
        node = EditorGUI.ObjectField(nodeTypeRect, node, typeof(Node), false) as Node;
        if (context != null)
        {
            Rect labelRect = new Rect(nodeTypeRect);
            labelRect.y += EditorGUIUtility.singleLineHeight * 1.5f;

            string status = "None";
            if (context.nodeStateDict.ContainsKey(node.GetInstanceID()))
            {
                status = context.nodeStateDict[node.GetInstanceID()].ToString();
            }
            EditorGUI.LabelField(labelRect, "Status: " + status);
        }
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    if (node.rect.Contains(e.mousePosition))
                    {
                        isDragged = true;
                        GUI.changed = true;
                        isSelected = true;
                        style = node.selectedNodeStyle;
                    }
                    else
                    {
                        GUI.changed = true;
                        isSelected = false;
                        style = node.defaultNodeStyle;
                    }
                }

                if (e.button == 1 && isSelected && node.rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                isDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && isDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (OnRemoveNode != null)
        {
            OnRemoveNode(this);
        }
    }
}

//// from http://gram.gs/gramlog/creating-node-based-editor-unity/
//public enum ConnectionPointType { In, Out }

//public class ConnectionPoint
//{
//    public Rect rect;

//    public ConnectionPointType type;

//    public Node node;

//    public GUIStyle style;

//    public Action<ConnectionPoint> OnClickConnectionPoint;

//    public ConnectionPoint(Node node, ConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint)
//    {
//        this.node = node;
//        this.type = type;
//        this.style = style;
//        this.OnClickConnectionPoint = OnClickConnectionPoint;
//        rect = new Rect(0, 0, 10f, 20f);
//    }

//    public void Draw()
//    {
//        rect.y = node.rect.y + (node.rect.height * 0.5f) - rect.height * 0.5f;

//        switch (type)
//        {
//            case ConnectionPointType.In:
//                rect.x = node.rect.x - rect.width + 8f;
//                break;

//            case ConnectionPointType.Out:
//                rect.x = node.rect.x + node.rect.width - 8f;
//                break;
//        }

//        if (GUI.Button(rect, "", style))
//        {
//            OnClickConnectionPoint?.Invoke(this);
//        }
//    }
//}

//#endregion
//#endif