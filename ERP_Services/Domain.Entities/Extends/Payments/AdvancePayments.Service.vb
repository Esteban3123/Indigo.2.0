Imports System.Runtime.Serialization

Partial Public Class AdvancePayments

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el porcentaje
    ''' </summary>
    <DataMember()>
    Public Property Percentage As Decimal

    ''' <summary>
    ''' Obtiene o establece el ajuste
    ''' </summary>
    <DataMember()>
    Public Property Adjustment As Decimal

    <DataMember()>
    Public Property FullNameMainAccount As String

    ''' <summary>
    ''' Obtiene o establece la bandera para saber si el anticipo fue modificada
    ''' </summary>
    <DataMember()>
    Public Property HandlesAddModifyDelete As Integer

    ''' <summary>
    ''' Obtiene o establece el id de la relacion de paymentNotesAccountPayableAdvance
    ''' </summary>
    <DataMember()>
    Public Property IdPaymentNotesAccountPayableAdvance As Integer

#End Region

End Class
