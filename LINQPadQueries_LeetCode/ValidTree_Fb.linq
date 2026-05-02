<Query Kind="Program" />

void Main()
{
	//var arr = new [,]{{0,1}, {0,2}, {2,3}, {2,4}};
	var arr = new [,] {{0,1},{0, 2},{2,1}};
	//var arr = new [,] {{1,2},{2,3},{3,4}};
	ValidTree(3, arr).Dump();
}

public bool ValidTree(int n, int[,] edges) {
        // initialize n isolated islands
        int[] nums = new int[n];
        for(int i = 0; i < n; i++) nums[i] = -1;
        
        // perform union find
        for (int i = 0; i < edges.GetLength(0); i++) {
            int x = Find(nums, edges[i,0]);
            int y = Find(nums, edges[i,1]);
            
            // if two vertices happen to be in the same set
            // then there's a cycle			
			$"{x},{y}".Dump();
            if (x == y) return false;
            			
            // union
            nums[x] = y;
        }
        return edges.GetLength(0) == n - 1;
    }
    
    int Find(int[] nums, int i) {
       if (nums[i] == -1) return i;
        return Find(nums, nums[i]);
    }