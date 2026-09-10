'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Jorge Leonardo Vernaza
' Created          : 10-12-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Windows.Forms
Imports System.Windows.Interop
#End Region



''' <summary>
''' This class helps in flashing a window to inform the user that the window 
''' requires attention when it doesn't have the focus or is not active.
''' </summary>
Public NotInheritable Class WindowFlash
#Region "Fields"

    '<DllImport("user32.dll")> _
    'Private Shared Function FlashWindow(hwnd As IntPtr, invert As Boolean) As Boolean
    'End Function

    'Private Shared ReadOnly _clock As Timer = New Timer()
    'Public Shared ReadOnly _flashing As [Boolean]
    'Public Shared _Handle As IntPtr
    'Private Const Interval As Integer = 500

#End Region



#Region "Public Methods"

    ' ''' <summary>
    ' ''' This method starts the clock. The value of Inverval can be altered to change the speed of flashing.
    ' ''' (default 500 mseconds)
    ' ''' </summary>
    'Public Shared Sub StartFlashing()
    '    _clock.Interval = Interval
    '    _clock.Start()
    '    AddHandler _clock.Tick, AddressOf ClockTick
    'End Sub

    ' ''' <summary>
    ' ''' This method stops the clock
    ' ''' </summary>
    'Public Shared Sub StopFlashing()
    '    _clock.[Stop]()
    'End Sub

#End Region

#Region "Private Methods"

    ' ''' <summary>
    ' ''' This method gets fired when the clock ticks
    ' ''' </summary>
    'Private Shared Sub ClockTick(sender As Object, e As EventArgs)
    '    Dim hwnd As IntPtr = _Handle
    '    FlashWindow(hwnd, Not _flashing)
    'End Sub

#End Region
End Class