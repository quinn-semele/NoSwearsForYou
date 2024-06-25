using System.Diagnostics;
using System.Runtime.InteropServices;
using static NoSwearsForYou.WindowsLibraryImports;

namespace NoSwearsForYou;

internal static class Program
{
    private static IntPtr KeyboardEventCallback(int idkWhatWindowsWasCooking, IntPtr eventId, IntPtr virtualKeyCode)
    {
        if (idkWhatWindowsWasCooking < 0)
        {
            return CallNextHookEx(idkWhatWindowsWasCooking, eventId, virtualKeyCode);
        }
        
        if (eventId == KeyDownEvent)
        {
            Keys key = (Keys) Marshal.ReadInt32(virtualKeyCode);
            Console.WriteLine($"[{key}]");

            // Return non-zero to disable keyboard input
            // if (key == Keys.F)
            // {
            //     return 1;
            // }
        }
        
        return CallNextHookEx(idkWhatWindowsWasCooking, eventId, virtualKeyCode);
    }
    
    [STAThread]
    private static void Main()
    {
        // ApplicationConfiguration.Initialize();
        // Application.Run(new Form1());
        
        LowLevelKeyboardCallbackDelegate keyboardEventCallbackDelegate = KeyboardEventCallback;

        string mainModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
        SetWindowsHookEx(LowLevelKeyboardHookId, keyboardEventCallbackDelegate, GetModuleHandle(mainModuleName), 0);

        Application.Run();
    }
}