#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class ReimbursementResource

#Region "Properties"

    ''' <summary>
    ''' obtiene o establece el id de la unidad operativa
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property OperatingUnitId As Integer

    ''' <summary>
    ''' Código de la orden de pago
    ''' </summary>
    <DataMember()>
    Public Property CodePaymentOrder As String

#End Region

End Class
