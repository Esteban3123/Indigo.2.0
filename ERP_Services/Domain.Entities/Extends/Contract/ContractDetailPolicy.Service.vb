Imports System.Runtime.Serialization

Partial Public Class ContractDetailPolicy

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del contrato
    ''' </summary>
    <DataMember()>
    Public Property ContractNumberName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la poliza
    ''' </summary>
    <DataMember()>
    Public Property FixedAssetPolicyDescription As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la aseguradora
    ''' </summary>
    <DataMember()>
    Public Property FixedAssetInsuranceDescription As String

#End Region

End Class
