Imports System.Runtime.Serialization
Partial Public Class RevenueType

#Region "Properties"

    <DataMember()>
    Public Property Value As Decimal

    <DataMember()>
    Public Property DebitValueModification As Decimal

    <DataMember()>
    Public Property CreditValueModification As Decimal

    <DataMember()>
    Public Property DebitValueTransfer As Decimal

    <DataMember()>
    Public Property CreditValueTransfer As Decimal

    <DataMember()>
    Public Property ExecutedValue As Decimal

    <DataMember()>
    Public Property SuspendedValue As Decimal

    <DataMember()>
    Public Property TotalBudget As Decimal

    <DataMember()>
    Public Property Balance As Decimal

    ''' <summary>
    ''' Permite saber si el registro esta guardado en la BD
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SaveInBD As Boolean

#End Region

End Class
