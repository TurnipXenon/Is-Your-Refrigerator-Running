// from https://hub.packtpub.com/building-your-own-basic-behavior-tree-tutorial/
// by Natasha Mathur
public class Inverter : Decorator
{

    /* Reports a success if the child fails and 
     * a failure if the child succeeds. Running will report 
     * as running */
    public override NodeState Evaluate(Context context)
    {
        switch (node.Evaluate(context))
        {
            case NodeState.Failure:
                return NodeState.Success;
            case NodeState.Success:
                return NodeState.Failure;
            case NodeState.Running:
                return NodeState.Running;
        }
        return NodeState.Success;
    }
}