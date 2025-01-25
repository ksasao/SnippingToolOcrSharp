# Windows 11 SnippingTool OCR Wrapper
This code is a wrapper for easily using oneocr.dll, which is included in the Windows 11 Snipping Tool, in C#. It will not work on Windows 10 because oneocr.dll is not included.
Please refer to https://github.com/b1tg/win11-oneocr by b1tg for the original C++ code.

The following conditions have been confirmed to work.
- Windows 11 23H2
- SnippingTool 11.2409.25.0
- Visual Studio 2022
- .NET Framework 4.7.2 (x64)

## Sample
SnippingToolOcrTest is an explanation of how to use it.