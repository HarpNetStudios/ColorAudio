# ColorAudio v1 Living Specs
''These will eventually be formalized.''

Files can have a maximum of 255 sections, applications can define a max or specific ones they look for.

Label name format should be structured like this:

`~0;255,255,255;t:1;f:500;c:Free comment;`

`~section;R,G,B;tag:value[;]`

### Light type [t]
0: On until region end, off immediately
1: On until region end, fade out
2: On at start, fade during region

### Fade duration [f]
Default is 100ms

### Comment [c]
Free comment, UTF-8

Label file should look like this:
```
0.000000	0.000000	ColorAudio v1
120.000000	140.000000	~0;255,0,255;t:0
123.345346	234.273565	~0;0,0,255;t:2
```

## License
The documentation and specifications for ColorAudio are licensed under the [Creative Commons Attribution 3.0 Unported license](https://creativecommons.org/licenses/by/3.0/), and the source code examples are licensed under the [MIT license](LICENSE.md) unless stated otherwise.