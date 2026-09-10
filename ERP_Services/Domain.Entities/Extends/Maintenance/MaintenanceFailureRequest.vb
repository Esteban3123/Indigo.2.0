#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class MaintenanceFailureRequest

    ''' <summary>
    ''' Compañia
    ''' </summary>
    <DataMember()>
    Public Property Company As String

    ''' <summary>
    ''' Sucursal
    ''' </summary>
    <DataMember()>
    Public Property BranchOfficeCodeName As String

    ''' <summary>
    ''' Tipo de solicitud
    ''' </summary>
    <DataMember()>
    Public Property NameRequest As String

    ''' <summary>
    ''' Quien Reporta
    ''' </summary>
    <DataMember()>
    Public Property NameReport As String


End Class
