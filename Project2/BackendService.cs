using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using QuickGraph;
using QuickGraph.Algorithms.Observers;

namespace DataStructures_Algorithms
{
	public class BackendService
	{
		//TODO: Declare LocationServiceQueue (public property & field)
		//This is where device messages will be enqueued. 
		//The type of this data structure need to be ConcurrentQueue<DeviceMessage>
        public ConcurrentQueue<DeviceMessage> LocationServiceQueue { get; set; }


        //TODO: Declare POIsTable (public property & field), this table is used to maintain POI information
        //Key is string (POI Name), and value is POI object
        //The type of this data structure need to be Dictionary<string, POI>
        public Dictionary <string, POI> POIsTable { get; set; }


        //TODO: Declare POIsGraph (public property & field), this is a Bidirectional Graph between all POIs in the shopping center
        //Nodes of type POI, and Edges of type Edge<POI>
        //The type of this data structure need to be BidirectionalGraph<POI, Edge<POI>>

        public BidirectionalGraph<POI, Edge<POI>> POIsGraph { get; set; }

        //TODO: Declare ActiveDevicesTable (public property & field), this is a dictionary of active devices and their navigation details
        //The type of this data structure needs to be ConcurrentDictionary<string, NavigationDetails>
        public ConcurrentDictionary<string, NavigationDetails> ActiveDevicesTable { get; set; }



        public int NumberOfPOIs 
		{ 
			get { return POIsTable.Count; }

		}

		public bool ShoppingCenterIsOpen { get; set; }


		public bool FindPOI(string DeviceId, POI Source, POI Target)
		{
			Stack<Edge<POI>> path = GetShortestPath(Source, Target);
			if (path == null) return false;
            //TODO: Please complete the FindPOI method - add navigation details to the ActiveDevicesTable.
            //This might be the first time the user asks for a navigation path or
            //He might have asked before, and changed his mind
            //So we need to use AddOrUpdate method which exists in the ConccurentDictionary class.
            //Please read here for details & example: https://msdn.microsoft.com/en-us/library/ee378665(v=vs.110).aspx
            //You may want to do it manually? then check if the key does not exist using ContainsKey, then call TryAdd
            //and if it does exist, then do ActiveDevicesTable[deviceId] = your new NavigationDetails object 
            ActiveDevicesTable.AddOrUpdate(DeviceId, new NavigationDetails() { CurrentPOI = Source, DestinationPOI = Target, PathToDestination = path }, (k, v) => new NavigationDetails() { CurrentPOI = Source, DestinationPOI = Target, PathToDestination = path });



            return true;

		}
		public BackendService()
		{
			
		}
		public void Init()
		{
            LocationServiceQueue = new ConcurrentQueue<DeviceMessage>();
            //TODO: Initialise your POIsTable
            POIsTable = new Dictionary<string, POI>();

            //TODO: Intialise your POIsGraph
            POIsGraph = new BidirectionalGraph<POI, Edge<POI>>();
            //TODO: Add a list of POI locations (at least 10 POIs) to the POIsTable & to the POIsGraph
            //Many lines here for POIs
            POI POI1 = new POI() { POIName = "Myer", POIDescription = "Clothing store", Services = new List<string>() { "clothing", "stuff" } };
            POIsTable.Add(POI1.POIName, POI1);
            POI POI2 = new POI() { POIName = "DJ", POIDescription = "Clothing store", Services = new List<string>() { "Clothing", "Fashion", } };
            POIsTable.Add(POI2.POIName, POI2);
            POI POI3 = new POI() { POIName= "KFC", POIDescription = "Junk food", Services = new List<string>() { "Fried chicken", "chips", } };
            POIsTable.Add(POI3.POIName, POI3);
            POI POI4 = new POI() { POIName = "MD", POIDescription = "Doctor", Services = new List<string>() { "surgery", "Medical check-up" } };
            POIsTable.Add(POI4.POIName, POI4);
            POI POI5 = new POI() { POIName = "Harvey Norman", POIDescription = "Household goods", Services = new List<string>() { "Electical", "White goods" } };
            POIsTable.Add(POI5.POIName, POI5);
            POI POI6 = new POI() { POIName = "McDonalds", POIDescription = "Junk food", Services = new List<string>() { "Burgers", "Fries" } };
            POIsTable.Add(POI6.POIName, POI6);
            POI POI7 = new POI() { POIName = "Subway", POIDescription = "Fresh rolls", Services = new List<string>() { "Fresh rolls", "healthy" } };
            POIsTable.Add(POI7.POIName, POI7);
            POI POI8 = new POI() { POIName = "Red Rooster", POIDescription = "Roast chicken", Services = new List<string>() { "Roast chicken", "chips" } };
            POIsTable.Add(POI8.POIName, POI8);
            POI POI9 = new POI() { POIName = "JB HI FI", POIDescription = "Electical goods", Services = new List<string>() { "Electical goods", "DVDs" } };
            POIsTable.Add(POI9.POIName, POI9);
            POI POI10 = new POI(){ POIName = "Pets R Us", POIDescription = "Pets for sale", Services = new List<string>() { "Cats", "Dogs" } };
            POIsTable.Add(POI10.POIName, POI10);



            //TODO: Add edges to your POIsGraph (20 edges? more? up to you)
            //Many lines here for edge
            POIsGraph.AddVertex(POI1);
            POIsGraph.AddVertex(POI2);
            POIsGraph.AddVertex(POI3);
            POIsGraph.AddVertex(POI4);
            POIsGraph.AddVertex(POI5);
            POIsGraph.AddVertex(POI6);
            POIsGraph.AddVertex(POI7);
            POIsGraph.AddVertex(POI8);
            POIsGraph.AddVertex(POI9);
            POIsGraph.AddVertex(POI10);

            POIsGraph.AddEdge(new Edge<POI>() { Source = POI2, Target = POI1, Description = "route 22", Distance=55 });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI3, Target = POI2, Description = "route 22", Distance= 55 });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI4, Target = POI3, Description = "route 1", Distance = 150 });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI5, Target = POI4, Description = "route 77", Distance = 200 });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI6, Target = POI5, Description = "route 55", Distance = 300 });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI7, Target = POI6, Description = "route 18", Distance = 400 });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI8, Target = POI7, Description = "route 36", Distance = 700, });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI9, Target = POI8, Description = "route 88", Distance = 800, });
            POIsGraph.AddEdge(new Edge<POI>() { Source = POI10, Target = POI9, Description = "route 99", Distance = 900, });

            //TODO: Initialise your ActiveDevicesTable, this is an empty table - to be filled automatically
            ActiveDevicesTable = new ConcurrentDictionary<string, NavigationDetails>();
            //Now let's open the shopping center and create a queue handling thread.
            //You do not need to modify this code
            ShoppingCenterIsOpen = true;
			Thread queueHandler = new Thread(() => this.WatchLocationServiceQueue());
            
			queueHandler.Start();
		}
		void WatchLocationServiceQueue()
		{
            DeviceMessage message = null;
            NavigationDetails tempDestination = new NavigationDetails();

            while (ShoppingCenterIsOpen == true && LocationServiceQueue.TryDequeue(out message) == false) { }
      
			if (ShoppingCenterIsOpen == false) return;

			//TODO: Complete the WatchLocationServiceQueue method
			//At this point (line), you have a message you retrieved from the queue - this is the message object
            if(ActiveDevicesTable.ContainsKey(message.DeviceId))
            {
                if (ActiveDevicesTable[message.DeviceId].PathToDestination.Count > 0)
                {
                    bool flag = ActiveDevicesTable.TryGetValue(message.DeviceId, out tempDestination);
                    Edge<POI> tempEdge = tempDestination.PathToDestination.Pop();
                    ActiveDevicesTable[message.DeviceId].CurrentPOI = tempEdge.Source;
                }
            }


            //If the message is sent by a known device (exists in the ActiveDevicesTable, then we need to update route & give directions
            //Otherwise -else skip the message, in a future version, you may track people movement in the shoppping center (not now)


            //If the device known - exists in the ActiveDevicesTable, then we need to check if there is any step left in 
            //the PathToDestination stack - you can use count > 0
            //If yes then we need to Pop an edge from the stack
            // and then set the CurrentPOI of this device entry in the ActiveDevicesTable 
            // to either Source or Target (based on route direction)
            //This step is important, because I wait for the value of the CurrentPOI to change 
            //in the Walk method (see the Runner class)
            //To tell the user that there is a new direction 



            //This will make sure that we process the next item in the LocationServiceQueue
            WatchLocationServiceQueue();
		}


		public Stack<Edge<POI>> GetShortestPath(POI Source, POI Target)
		{
            QuickGraph.Algorithms.ShortestPath.DijkstraShortestPathAlgorithm<POI, Edge<POI>> algo =
				new QuickGraph.Algorithms.ShortestPath.DijkstraShortestPathAlgorithm<POI, Edge<POI>>(POIsGraph, (Edge<POI> arg) => arg.Distance);


			// creating the observer & attach it
			var vis = new VertexPredecessorRecorderObserver<POI, Edge<POI>>();
			vis.Attach(algo);


			// compute and record shortest paths
			algo.Compute(Target);

			// vis can create all the shortest path in the graph
			IEnumerable<Edge<POI>> path = null;
			vis.TryGetPath(Source, out path);
			Stack<Edge<POI>> pathStack = new Stack<Edge<POI>>();

            if (path == null) return null;

            foreach (Edge<POI> e in path)
                pathStack.Push(e);


            return pathStack;
		}

			   
		
	}
}
