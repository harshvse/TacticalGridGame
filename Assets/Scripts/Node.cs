using UnityEngine;
//A node is any point in the map where the agent can check to see if he can move to it
public class Node: MonoBehaviour, IHeapItem<Node> 
{
    public bool walkable;
    public Vector3 worldPostion;
    public int gCost;
    public int hCost;
    public int gridX, gridY;
    public Node parent;
    int heapIndex;
    
    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
    
    public int HeapIndex {
        get {
            return heapIndex;
        }
        set {
            heapIndex = value;
        }
    }

    public int CompareTo(Node nodeToCompare) {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0) {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
