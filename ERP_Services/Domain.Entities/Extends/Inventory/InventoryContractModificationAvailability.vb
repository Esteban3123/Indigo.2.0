Imports System.Runtime.Serialization

Partial Public Class InventoryContractModificationAvailability

#Region "Properties"

    ''' <summary>
    ''' Código de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AvailabilityCode As String

    ''' <summary>
    ''' Rubro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CategoryCodeName As String

    ''' <summary>
    ''' Recurso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FinancialSourceCodeName As String

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RevenueTypeCodeName As String

    ''' <summary>
    ''' Saldo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property Balance As Decimal

#End Region

End Class
