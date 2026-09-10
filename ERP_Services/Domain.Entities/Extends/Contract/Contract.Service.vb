Imports System.Runtime.Serialization

Partial Public Class Contract

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la unidad operativa
    ''' </summary>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Obtiene o establece el Id del tercero de la entidad administradora de salud
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyId As Integer

    ''' <summary>
    ''' Obtiene o establece la descripcion de la entidad administradora de salud
    ''' </summary>
    <DataMember()>
    Public Property HealthAdministratorDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la entidad de contrato
    ''' </summary>
    <DataMember()>
    Public Property ContractEntityDescription As String

#End Region

End Class
