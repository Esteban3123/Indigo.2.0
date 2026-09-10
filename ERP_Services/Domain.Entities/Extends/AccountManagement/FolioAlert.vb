Imports System.Runtime.Serialization

Partial Public Class FolioAlert
#Region "Properties"

    ''' <summary>
    ''' Establece el nombre del area de gestión asociada a la alerta
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ManagementAreaName As String

    ''' <summary>
    ''' Establece el nombre del usuario que creó la alerta
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property UserName As String
#End Region
End Class
