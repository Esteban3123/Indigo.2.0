#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class FixedAssetDepreciationDetailCost

    ''' <summary>
    ''' Cuenta
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String

    ''' <summary>
    ''' Responsable
    ''' </summary>
    <DataMember()>
    Public Property ResponsibleDescription As String

    ''' <summary>
    ''' Localización
    ''' </summary>
    <DataMember()>
    Public Property LocationDescription As String

    ''' <summary>
    ''' Centro costo
    ''' </summary>
    <DataMember()>
    Public Property CostCenterDescription As String

End Class
