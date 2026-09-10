Imports System.Runtime.Serialization
Public Class LoanMerchandiseDevolutionDetailBatchSerial

    ''' <summary>
    ''' Codigo del producto ala que esta esta relacionada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property Code As String

    ''' <summary>
    ''' codigo y nombre del producto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember>
    Property CodeNameProduct As String

    ''' <summary>
    ''' Lote
    ''' </summary>
    ''' <returns></returns>
    <DataMember>
    Property CodeBatchSerial As String

End Class
