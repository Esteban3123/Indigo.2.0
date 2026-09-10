Imports System.Runtime.Serialization

Partial Public Class GlosaMedicalFeesDetail

#Region "Properties"

    ''' <summary>
    ''' obtiene el numero de la factura
    ''' </summary>
    <DataMember()>
    Public Property InvoiceNumber As String

    ''' <summary>
    ''' obtiene el valor de la factura
    ''' </summary>
    <DataMember()>
    Public Property InvoiceValue As Decimal

    ''' <summary>
    ''' descripcion del concepto de honorario medico glosado
    ''' </summary>
    <DataMember()>
    Public Property ConceptDescription As String

    ''' <summary>
    ''' descripcion cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property AccountPayableDescription As String

#End Region

End Class