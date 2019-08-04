// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public class Inverter : Decorator
{

    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */
    public override NodeState Evaluate(Context context)
    {
        NodeState result = NodeState.Success;

        switch (node.Evaluate(context))
        {
            case NodeState.Failure:
            default:
                break;
            case NodeState.Success:
                result = NodeState.Failure;
                break;
            case NodeState.Running:
                result = NodeState.Running;
                break;
        }

        SetNodeState(context, result);
        return result;
    }
}