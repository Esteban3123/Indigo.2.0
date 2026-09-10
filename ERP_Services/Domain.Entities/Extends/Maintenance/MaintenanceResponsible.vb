#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class MaintenanceResponsible

    ''' <summary>
    ''' Obtiene o establece el Nombre del Tercero de la Aseguradora
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyName As String

    ''' <summary>
    ''' Obtiene o establece el Nombre del Tipo de Vinculacion
    ''' </summary>
    <DataMember()>
    Public Property VinculationName As String

    ''' <summary>
    ''' Obtiene o establece el Nombre del typo de responsable
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleTypeName As String

    ''' <summary>
    ''' Obtiene o establece el Nombre del Rol del responsable
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleRoleName As String

End Class
