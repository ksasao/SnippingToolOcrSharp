using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SnippingToolOcr
{
    [StructLayout(LayoutKind.Sequential)]
    struct Img
    {
        public int t;
        public int col;
        public int row;
        public int _unk;
        public long step;
        public IntPtr data_ptr;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct BoundingBox
    {
        public float x1;
        public float y1;
        public float x2;
        public float y2;
        public float x3;
        public float y3;
        public float x4;
        public float y4;
    }
    internal class NativeMethods
    {
        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long CreateOcrInitOptions(out long ctx);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrLineCount(long instance, out long count);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrLine(long instance, long index, out long line);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrLineContent(long line, out IntPtr content);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrLineBoundingBox(long line, out IntPtr boundingBoxPtr);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrLineWordCount(long instance, out long count);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrWord(long instance, long index, out long line);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrWordContent(long line, out IntPtr content);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long GetOcrWordBoundingBox(long line, out IntPtr boundingBoxPtr);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long OcrProcessOptionsSetMaxRecognitionLineCount(long opt, long count);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long RunOcrPipeline(long pipeline, ref Img img, long opt, out long instance);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long CreateOcrProcessOptions(out long opt);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long OcrInitOptionsSetUseModelDelayLoad(long ctx, byte flag);

        [DllImport("oneocr.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern long CreateOcrPipeline(string modelPath, string key, long ctx, out long pipeline);

    }
}
