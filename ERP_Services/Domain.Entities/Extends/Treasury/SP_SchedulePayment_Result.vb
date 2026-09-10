#Region "Imports"

Imports System.Runtime.Serialization

#End Region

Partial Public Class SP_SchedulePayment_Result

#Region "Properties"

    ''' <summary>
    ''' Agrega detalles que se interfazaran con presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property SchedulePaymentDetailBudget As New List(Of SchedulePaymentDetailBudget)()

    ''' <summary>
    ''' descuento aplicado
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property ApplyDiscountValue As Decimal

    ''' <summary>
    ''' Guarda la posicion de los detalles para luego organizarlos de forma correcta
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property PositionDetailsAdd As Integer

#End Region

End Class
