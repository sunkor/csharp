<Query Kind="Program" />

void Main()
{
	var path = string.Empty;
//	var path = "/home"; //=> "/home"
//	SimplifyPath(path).Dump();
//	
//	path = "/"; //=> "/."
//	SimplifyPath(path).Dump();
//	
//	path = "/..";
//	SimplifyPath(path).Dump();
//	
//	path = "/...";
//	SimplifyPath(path).Dump();
	
	//path = "/abc/...";
	//SimplifyPath(path).Dump();
	
	//path = "/."; //=> "/."
	//SimplifyPath(path).Dump();
	
//	path = "/home/"; //=> "/home"
//	SimplifyPath(path).Dump();
//	
	//path = "home"; //=> "/home"
	//SimplifyPath(path).Dump();
	
	//path = "/a/./b/../../c/"; //=> "/c"
	//SimplifyPath(path).Dump();
	//path = "/";
	//SimplifyPath(path).Dump();
	
	path = "/..hidden";
	SimplifyPath(path).Dump();
	
	path = "/.aa/....hidden";
	SimplifyPath(path).Dump();
}

public string SimplifyPath(string path) 
{
	if(path == null || path.Length == 0) return path;
		
	var slashToConsiderIndex = -1;
	
	var considerDots = true;
	var numOfDots = 0;
	
	var length = path.Length;
	length = length > 1 && path[length - 1] == '/' ? length - 1 : length;
	
	for(var i = length - 1; i >= 0; i--)
	{
		var ch = path[i];
		
		if(ch == '/')
		{
			considerDots = false;
			slashToConsiderIndex = i;
			if(numOfDots >= 3)
			{
				numOfDots = -1;
				continue;
			}
			else
				break;
		}
		else if(considerDots)
		{
			if(ch == '.')
				numOfDots++;
			else
			{
				considerDots = false;
			}
		}
	}
	
	if(slashToConsiderIndex > -1 && (numOfDots == 1 || numOfDots == 2)) return "/";
	slashToConsiderIndex = slashToConsiderIndex == -1 ? 0 : slashToConsiderIndex;
	
	return path.Substring(slashToConsiderIndex, length - slashToConsiderIndex);
}
