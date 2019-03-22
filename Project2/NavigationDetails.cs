using System.Collections.Generic;

namespace DataStructures_Algorithms
{
	
	public class NavigationDetails
	{
		public POI CurrentPOI { get; set; }
		public POI DestinationPOI { get; set; }
		public Stack<Edge<POI>> PathToDestination;
	}
}