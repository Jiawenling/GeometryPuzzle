# GeometryPuzzle

> Learn more about shapes!

## Prerequisites 

.NET Core Version
This project is built using .NET Core 7. To run this project, you will need to have .NET Core 7 installed on your machine. You can download the latest version of .NET Core from the official Microsoft website.

System Requirements
This project will run on any OS.


In order to test the console application run the following commands from the command line:

```
dotnet build
```

You can run the application using the .dll found in /bin

```
dotnet GeometryPuzzleApp.dll
```

## Important

1. Points on the vertices of the polygon are not considered WITHIN the polygon
2. Only integer inputs are accepted
3. You can configure the range of acceptable integers for coordinates when you create a random shape. This will apply for both X and Y coordinates. Please see below:

In GeometryPuzzleApp/app.config:
```csharp
<configuration>
    <appSettings>
        <add key="InclusiveMinValue" value="-100" />
        <add key="ExclusiveMaxValue" value="101" />
    </appSettings>
</configuration>
```

## Program flow

```csharp
// Users will be first be asked to choose a run mode

 if (int.TryParse(input.KeyChar.ToString(), out int option))
        {
            if (option == 1)
            {
                var shapeGenerator = new CustomShapeGenerator();
                runMode = new CustomShapeRunMode(shapeGenerator, messageUtil, inputUtil, checkPointUtil);
                runMode.Start();
                break;
            }
            else if (option == 2)
            {
                var shapeGenerator = new RandomShapeGenerator();
                runMode = new RandomShapeRunMode(shapeGenerator, messageUtil, inputUtil, checkPointUtil);
                runMode.Start();
                break;
            }

// Each runmode extends IRunMode, which corresponds to the application flow of first getting to a final shape, then testing if point is within shape

	public interface IRunMode
	{
		void Start();
		bool CheckPointWithin(Point point);
	}
```

A corresponding shape generator will be initialised and passed to the runmode. This shape generator will take care of the first portion of the workflow to get a final shape.

```csharp
//CustomShapeGenerator extends ICustomShapeGenerator
//It will use utility classes from namespace PolygonUtility.PolygonIntersectionCheckUtility to ensure that no intersecting lines will be added

	public interface ICustomShapeGenerator: IShapeGenerator
	{
		bool AddPoints(int x, int y);
		bool NewLineIsValid(LineSegment newLine);
        bool LastLineIsValid();
        bool IsValidAndCompleteShape();
    }

//RandomShapeGenerator extends ICustomShapeGenerator

    public interface IRandomShapeGenerator: IShapeGenerator
	{
        List<Point> GeneratePoints();
    }

//Both shape generators extends IShapeGenerator
    public interface IShapeGenerator
	{
		List<Point> GetPointsOfPolygon();
		bool IsPointOfPolygon(Point point); 
	}
```
### Considerations for the custom shape generator:

- Is new line intersecting existing lines or final polygon is self-intersecting?
- Are these valid intersections? i.e edges of a polygon

This is solved using a modified version of the Bentley-Ottman algorithm, which is a more efficient algorithm to check if numerous lines are intersecting (as opposed to checking every pair of lines)

The process will check if the line under consideration is a vertice and if vertices self-intersect.
Otherwise, it will proceed to check if line intersects with lines directly above or below

### Considerations for the custom shape generator:
- How to ensure randomly generated coordinates will not form a self intersecting polygon?

This is solved by ordering. 
1. Find points with max and min x-coordinates, minX, maxX
2. Points that are above and below the line formed by minX and maxX will be stored in two different lists List1 and List2 respectively.
3. Sort List2 by descending order of x-coodinates
4. The resulting ordering of points is:
minX ->> List1 ->> MaxX ->> List2

### Considerations for checking if point is within shape

This is done by ray testing method - whereby a horizontal ray is extended from the right side of the beam and monitoring how many times the ray intersects with the polygon. 


