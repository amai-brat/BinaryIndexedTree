using System.Collections.ObjectModel;
using OxyPlot;
using BinaryIndexedTree;
namespace Benchmark.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Title = "Sum";
        Measurements = new Collection<Measurement>();
        var meter = new Meter();
        var measureOfSums = meter.MeasureSumsTo(50000);
        for (int i = 0; i < measureOfSums.Count; i++)
        {
            var currentMeasure = measureOfSums[i];
            Measurements.Add(new Measurement()
            {
                N = 100 * (i+1),
                SourceArray = currentMeasure.Item1,
                ArrayOfSums = currentMeasure.Item2,
                BinaryIndexedTree = currentMeasure.Item3
            });
        }
        
    }
    
    public Collection<Measurement> Measurements { get; private set; }
    public IInterpolationAlgorithm InterpolationAlgorithm => new CanonicalSpline(0);
    public string Title { get; set; }
}