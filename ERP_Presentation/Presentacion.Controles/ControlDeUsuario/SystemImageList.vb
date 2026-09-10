'***********************************************************************
' Assembly         : Presentacion.Controls
' Author           : Jorge Leonardo Vernaza
' Created          : 10-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
#End Region

''' <summary>
''' Clase que obtiene un listado con los iconos de los procesos del sistema
''' </summary>
Public Class SystemImageList
    Private Declare Auto Function SHGetFileInfo Lib "shell32.dll" (ByVal pszPath As String, _
         ByVal dwFileAttributes As Integer, ByRef psfi As SHFileInfo, ByVal cbFileInfo As Integer, _
         ByVal uFlags As Integer) As IntPtr

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Private Structure SHFileInfo
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As Integer
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)> _
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)> _
        Public szTypeName As String
    End Structure

    Private _keys As Dictionary(Of String, Integer)

    Private _largeImages As ImageList
    Public ReadOnly Property LargeImages As ImageList
        Get
            Return _largeImages
        End Get
    End Property

    Public Sub New()
        _largeImages = New ImageList()
        _largeImages.ColorDepth = ColorDepth.Depth32Bit
        _largeImages.ImageSize = New Size(32, 32)
        _keys = New Dictionary(Of String, Integer)()
    End Sub

    Public Function GetImageIndex(fileName As String) As Integer
        Dim fi As SHFileInfo
        Dim ico As Icon
        Dim key As String
        Dim idx As Integer
        Dim flags As Integer
        Const SHGFI_USEFILEATTRIBUTES = &H10
        Const SHGFI_ICON = &H100
        Const SHGFI_LARGEICON = &H0

        flags = SHGFI_USEFILEATTRIBUTES + SHGFI_ICON + SHGFI_LARGEICON
        key = fileName.Substring(fileName.LastIndexOf("."c)).ToLower
        If String.IsNullOrEmpty(key) OrElse key = "." Then
            If IO.Directory.Exists(fileName) Then
                key = "04C6CC51695C4016963F11086BF65A0F"
                flags = SHGFI_ICON + SHGFI_LARGEICON
            Else
                key = "FB1F683391C0493AB6976227A9CE54B4"
            End If
        ElseIf key = ".exe" OrElse key = ".lnk" Then
            key = fileName.Substring(fileName.LastIndexOf("\"c) + 1).ToLower
        End If
        If _keys.TryGetValue(key, idx) Then
            Return idx
        Else
            fi.szDisplayName = New String(Chr(0), 260)
            fi.szTypeName = New String(Chr(0), 80)
            SHGetFileInfo(fileName, 0, fi, Marshal.SizeOf(fi), flags)
            ico = Icon.FromHandle(fi.hIcon)
            _largeImages.Images.Add(ico)
            idx = _largeImages.Images.Count - 1
            _keys.Add(key, idx)
            Return idx
        End If
    End Function
End Class
