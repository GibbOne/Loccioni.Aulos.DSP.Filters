# Loccioni.Aulos.Dsp.Filters

It contains a set of utilities to implement some digital signal processing.
This is a wrapper around [Intel Performance Primitives](https://software.intel.com/en-us/ipp).

In order to make it works, please add path of Intel library to `PATH` Windows system variable.
For instance an IPP version it's located in:

```
C:\Program Files (x86)\IntelSWTools\compilers_and_libraries_2019.5.281\windows\redist\intel64_win\ipp
```

## Low pass signal filtering

An example of the simplest usage:

``` c#
Butterworth filter = new Butterworth(
    order: 8,                // 8th order it could be enough :-)
    samplingFrequency: 1000, //Hz 
    cutoff: 300);            //Hz

var filteredData = f.Filter(sourceData);
```

In order to improve performance, you could create the filter one time and apply it at each new data arrival.

``` c#
Butterworth filter = new Butterworth(
    order: 8,                // 8th order it could be enough :-)
    samplingFrequency: 1000, //Hz 
    cutoff: 300);            //Hz

// later on a data arrival event...

var filteredData = f.Filter(sourceData);
```

If each new data array is not time connected, you should reset internal filter state by calling `Reset()` method
before every `Filter` call. 

## Benchmark

It's a simple benchmark to verify performance compared with the (already used and payed :-)) *NationalInstruments.Analysis.Enterprise* library
included in *NI Measurement Studio*.

**Input signal**: **20000** samples.

| order: **8th**,  sampl. feq: **10KHz**,  cutoff **3KHz** |   NI [ms] |   this [ms] |
|----------------------------------------------------------|-----------|-------------|
| Filter creation										   |     0.9   |      0.04   |
| Filter creation and 10 time application                  |    68.0   |   **62.0**  |

**Boundary conditions**: 
- BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17763.805 (1809/October2018Update/Redstone5)
- Intel Core i7-4770S CPU 3.10GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
  - [Host]     : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3468.0  [AttachedDebugger]
  - DefaultJob : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3468.0

## References

- [Intel IPP dev reference](https://software.intel.com/en-us/ipp-dev-reference).
