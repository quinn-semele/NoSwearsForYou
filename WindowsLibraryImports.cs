using System.Runtime.InteropServices;

namespace NoSwearsForYou;

public partial class WindowsLibraryImports
{
    [LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExW")]
    public static partial IntPtr SetWindowsHookEx(
        int hookId, 
        LowLevelKeyboardCallbackDelegate keyboardEventCallback, 
        IntPtr processHandle, 
        uint processIdToHook
    );
    
    [LibraryImport("kernel32.dll", StringMarshalling = StringMarshalling.Utf16, EntryPoint = "GetModuleHandleA")]
    public static partial IntPtr GetModuleHandle(string moduleName);
    
    [LibraryImport("user32.dll")]
    public static partial IntPtr CallNextHookEx(
        int dumbWindowsFlowControlNumber,
        IntPtr eventId,
        IntPtr key
    );
    
    // Windows hook Keyboard Low Level - Allows monitoring of all keyboard inputs.
    public const int LowLevelKeyboardHookId = 13;

    // Windows manager, Key down
    public const int KeyDownEvent = 0x100;
    
    // See: https://learn.microsoft.com/en-us/windows/win32/winmsg/lowlevelkeyboardproc
    public delegate IntPtr LowLevelKeyboardCallbackDelegate(int idkWhatWindowsWasCooking, IntPtr eventId, IntPtr key);
}