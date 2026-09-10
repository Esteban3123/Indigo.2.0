#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class MaintenanceFailureRequestDetail

    ''' <summary>
    ''' Activo o Parte
    ''' </summary>
    <DataMember()>
    Public Property NameClassPart As String

    ''' <summary>
    ''' Nombre del articulo
    ''' </summary>
    <DataMember()>
    Public Property NameArticle As String

    ''' <summary>
    ''' Placa del articulo
    ''' </summary>
    <DataMember()>
    Public Property Plate As String

    ''' <summary>
    ''' Modelo del articulo
    ''' </summary>
    <DataMember()>
    Public Property Model As String

    ''' <summary>
    ''' Serie del articulo
    ''' </summary>
    <DataMember()>
    Public Property Serie As String

    ''' <summary>
    ''' Nombre de la parte
    ''' </summary>
    <DataMember()>
    Public Property PartsName As String

    ''' <summary>
    ''' Ubicacion
    ''' </summary>
    <DataMember()>
    Public Property Location As String

    ''' <summary>
    ''' Responsable
    ''' </summary>
    <DataMember()>
    Public Property Responsible As String

    ''' <summary>
    ''' Telefono del responsable
    ''' </summary>
    <DataMember()>
    Public Property ResponsiblePhone As String

End Class
