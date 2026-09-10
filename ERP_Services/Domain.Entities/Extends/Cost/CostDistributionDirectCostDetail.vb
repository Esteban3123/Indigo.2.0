Imports System.Runtime.Serialization

Partial Public Class CostDistributionDirectCostDetail

#Region "Properties"


    <DataMember()>
    Public Property ProductionCenterCodeName As String

    <DataMember()>
    Public Property MainAccountCodeName As String

    <DataMember()>
    Public Property HandlesThirdParty As Boolean?

    <DataMember()>
    Public Property CostCenterCodeName As String

    <DataMember()>
    Public Property MeasurementUnitCodeName As String

    <DataMember()>
    Public Property ThirdPartyNitName As String

    <DataMember()>
    Public Property ProductCodeName As String

    <DataMember()>
    Public Property PositionCodeName As String

    ''' <summary>
    ''' Extendido para la Naturaleza del movimiento
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property NatureText As String

#End Region

End Class
