# Loccioni.Aulos.Dsp.Filters

It contains a set of utilities to implement some digital signal processing.
This is a wrapper around [Intel Performance Primitives](https://software.intel.com/en-us/ipp).

## Low pass signal filtering

An example of the simplest usage:

``` c#
Butterworth filter = new Butterworth(
    samplingFrequency: 1000, //Hz 
    cutoff: 300,             //Hz
    order: 8);               // 8th order it could be enough :-)

var filteredData = f.Filter(sourceData);
```

In order to improve performance, you could create the filter one time and apply it at each new data arrival.

``` c#
Butterworth filter = new Butterworth(
    samplingFrequency: 1000, //Hz 
    cutoff: 300,             //Hz
    order: 8);               // 8th order it could be enough :-)

// later on a data arrival event...

var filteredData = f.Filter(sourceData);
```

If each new data array is not time connected, you should reset internal filter by calling `Reset()` method
before every `Filter` call. 

## Benchmark

It's a simple benchmark to verify performance compared with the (already used and payed :-)) *NationalInstruments.Analysis.Enterprise* library
included in *NI Measurement Studio*.

|                                                     |   NI  | this  |
|-----------------------------------------------------|-------|-------|
| **8th** order **100KHz** sampled cutoff **300KHz**  |       |       |

## References

- [Intel IPP dev reference](https://software.intel.com/en-us/ipp-dev-reference).
