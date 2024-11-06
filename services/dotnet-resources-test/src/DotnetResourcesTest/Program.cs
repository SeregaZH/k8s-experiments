using System.Diagnostics;

Console.WriteLine("Starting memory and CPU spike test...");

// Adjust these variables for different load patterns
int memorySpikeDuration = 5000; // Memory spike duration in milliseconds
int cpuSpikeDuration = 2000; // CPU spike duration in milliseconds
int memorySizeMb = 100; // Size of memory spike in MB
int cpuSpikeInterval = 1000; // Interval between CPU spikes in milliseconds

var sharedList = new List<byte[]>();

while (true) 
{
    Console.WriteLine("Generating memory spike...");
    await GenerateMemorySpike(memorySizeMb, memorySpikeDuration);

    Console.WriteLine("Generating CPU spike...");
    GenerateCpuSpike(cpuSpikeDuration);

    // Sleep between spikes
    await Task.Delay(cpuSpikeInterval);
}

async Task GenerateMemorySpike(int sizeMb, int durationMs)
{
    List<byte[]> memorySpike = new List<byte[]>();
    for (int i = 0; i < sizeMb; i++)
    {
        memorySpike.Add(new byte[1024 * 1024 * 3]); // Allocate 10 MB
        sharedList.Add(new byte[1024 * 20]); // Allocate 10 KB
    }
    await Task.Delay(durationMs);
    memorySpike.Clear(); // Release memory
}

static void GenerateCpuSpike(int durationMs)
{
    Stopwatch stopwatch = Stopwatch.StartNew();
    while (stopwatch.ElapsedMilliseconds < durationMs)
    {
        // Run CPU-intensive task
        Math.Sqrt(new Random().NextDouble());
    }
}
