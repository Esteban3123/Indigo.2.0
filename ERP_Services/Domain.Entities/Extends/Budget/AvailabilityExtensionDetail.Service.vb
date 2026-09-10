#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class AvailabilityExtensionDetail

#Region "Properties"

    ''' <summary>
    ''' Codigo de la disponibilidad
    ''' </summary>
    <DataMember()>
    Public Property CodeAvailability As String

    ''' <summary>
    ''' saldo de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property BalanceAvailability As Decimal

    ''' <summary>
    ''' Estado de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property StatusAvailability As String

#End Region

End Class
