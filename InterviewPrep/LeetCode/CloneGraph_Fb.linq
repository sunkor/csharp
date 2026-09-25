<Query Kind="Program" />

void Main()
{
	
}


  //Definition for undirected graph.
  public class UndirectedGraphNode {
      public int label;
      public IList<UndirectedGraphNode> neighbors;
      public UndirectedGraphNode(int x) { label = x; neighbors = new List<UndirectedGraphNode>(); }
  };
 
 	//apply BFS
    public UndirectedGraphNode CloneGraph(UndirectedGraphNode node) {
        
		//Edge case
		if(node == null) return null;
		
		var createdNodes = new Dictionary<int, UndirectedGraphNode>();
		
		//Start with root
		var root = new UndirectedGraphNode(node.label);
		createdNodes.Add(root.label, root);
		
		//Enqueue root with its corresponding graph node
		var queue = new Queue<Tuple<UndirectedGraphNode,UndirectedGraphNode>>();
		queue.Enqueue(new Tuple<UndirectedGraphNode,UndirectedGraphNode>(root,node));
				
		//Maintain list of visited nodes to avoid cyclic moves.
		var visitedNodes = new HashSet<int>();
		do
		{
			var tuple = queue.Dequeue();
		
			//If we have not visited, then visit.
			if(!visitedNodes.Contains(tuple.Item1.label))
			{
				var currLabel = tuple.Item1.label;
				
				//Record as visited.
				visitedNodes.Add(currLabel);
				
				//Get the neighbors from the corresponding node neighbors.
				var neighbors = tuple.Item2.neighbors;
				
				//Check one or more neighbors exists
				if(neighbors?.Count > 0)
				{
					foreach(var childNode in neighbors)
					{					
						var label = childNode.label;
						
						//For each neighbor, add to the current visiting node.
						UndirectedGraphNode newChildNode = null;
						
						if(label == currLabel)
							newChildNode = tuple.Item1;
						else 
						{
							//If we have not visited the child node, add.
							if(!createdNodes.TryGetValue(label, out newChildNode))
							{
								newChildNode = new UndirectedGraphNode(label);
								queue.Enqueue(new Tuple<UndirectedGraphNode,UndirectedGraphNode>(newChildNode, childNode));
								createdNodes.Add(newChildNode.label, newChildNode);
							}
						}
						
						tuple.Item1.neighbors.Add(newChildNode);
					}
				}
			}
		}while(queue.Count > 0);
		
		return root;
    }

