# IDSS-PortQualityAndRouteCalculatorV2
Project prototype for DV2573 - Decision Support Systems. 
This system exists to support shippers in decision making by supplying plenty of data from the World Port Index on around 3.7k different ports and terminals. While also providing the ability to calculate the shortest path and visualize it on a map. 

Written in ASP.NET MVC while using Javascript, and the Openlayers library, for map visualization. The data is extracted from Excel sheets and stored in SQL Server using Code-first approach. The Branch and Bound algorithm is written in C# while a Python script is used to automatically request multiple distances between ports from sea-distances.org.
