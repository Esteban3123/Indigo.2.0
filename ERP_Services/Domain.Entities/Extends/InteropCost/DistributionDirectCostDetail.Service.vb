Imports System.Runtime.Serialization

Partial Public Class DistributionDirectCostDetail

#Region "Properties"

    <DataMember()>
    Public Property MeasurementUnitCodeName As String

    <DataMember()>
    Public Property ProductionCenterCodeName As String

    ''' <summary>
    ''' Obtiene o asigna el valor que se uso para allár el porcentaje a distribuir
    ''' </summary>
    ''' <value>Valor usado</value>
    ''' <returns>El valor usado</returns>
    <DataMember()>
    Public Property ByArea As Decimal

    <DataMember()>
    Public Property ByOfficialHours As Decimal

    <DataMember()>
    Public Property BySupplyValue As Decimal

    <DataMember()>
    Public Property ByWorkmanship As Decimal

    <DataMember()>
    Public Property ByAssetValue As Decimal

    <DataMember()>
    Public Property BySales As Decimal

#End Region

End Class
