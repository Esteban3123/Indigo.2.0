'***********************************************************************
' Assembly         : Presentacion.Base
' Author           : Oscar stiven astudillo reyes
' Created          : 11-08-2024
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class ReportCache

#Region "Singleton"

    ''' <summary>
    ''' Instancia estatica
    ''' </summary>
    Private Shared _instance As ReportCache

    ''' <summary>
    ''' Constructor privado para evitar la creación de múltiples instancias
    ''' </summary>
    Private Sub New()
    End Sub

    ''' <summary>
    ''' Método para obtener la instancia única de ReportCache
    ''' </summary>
    ''' <returns>Instancia de ReportCache</returns>
    Public Shared Function GetInstance() As ReportCache
        If _instance Is Nothing Then
            _instance = New ReportCache()
        End If
        Return _instance
    End Function

#End Region

#Region "Private Fields"

    ' Diccionario para almacenar las rutas de los reportes
    Private _valuesCache As New Dictionary(Of String, String)()

#End Region

#Region "Methods"

    ''' <summary>
    ''' Registra o actualiza la ruta de reporte en el caché
    ''' </summary>
    ''' <param name="key">Clave única para el caché</param>
    ''' <param name="reportPath">Ruta del reporte</param>
    Public Sub SetValue(ByVal key As String, ByVal reportPath As String)
        _valuesCache(key) = reportPath
    End Sub

    ''' <summary>
    ''' Obtiene la ruta de reporte desde el caché
    ''' </summary>
    ''' <param name="key">Clave única para el caché</param>
    ''' <returns>Ruta del reporte o cadena vacía si no existe</returns>
    Public Function GetValue(ByVal key As String) As String
        If _valuesCache.ContainsKey(key) Then
            Return _valuesCache(key)
        End If
        Return String.Empty
    End Function

    ''' <summary>
    ''' Elimina un registro del caché
    ''' </summary>
    ''' <param name="key">Clave única del registro a eliminar</param>
    Public Sub RemoveValue(ByVal key As String)
        _valuesCache.Remove(key)
    End Sub

    ''' <summary>
    ''' Limpia todo el caché
    ''' </summary>
    Public Sub Clear()
        _valuesCache.Clear()
    End Sub

#End Region


End Class
