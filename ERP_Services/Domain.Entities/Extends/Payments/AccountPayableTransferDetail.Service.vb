Imports System.Runtime.Serialization

Partial Public Class AccountPayableTransferDetail

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion del proveedor
    ''' </summary>
    <DataMember()>
    Public Property AccountPayableSupplierDescription As String

    ''' <summary>
    ''' Obtiene o establece el codigo de la cuenta por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AccountPayableConsecutive As String

    ''' <summary>
    ''' Obtiene o establece el numero de factura de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AccountPayableBillNumber As String

    ''' <summary>
    ''' Obtiene o establece la fecha de la cxp
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property AccountPayableDocumentDate As DateTime

    ''' <summary>
    ''' Obtiene o establece el nombre del estado de cada detalle
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property StatusName As String

    ''' <summary>
    ''' Obtiene o establece la descripcion de la razon de rechazo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RejectionReasonDescription As String

#End Region

End Class
