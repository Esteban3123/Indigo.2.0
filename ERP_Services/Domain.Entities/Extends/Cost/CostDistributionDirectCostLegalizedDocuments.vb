Imports System.Runtime.Serialization

Partial Public Class CostDistributionDirectCostLegalizedDocuments

#Region "Properties"

    ''' <summary>
    ''' Codigo del documento de provision
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Property Code As String

    ''' <summary>
    ''' Valor del documento de provision
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property Value As Decimal

    ''' <summary>
    ''' Estado
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property StatusName As String

    ''' <summary>
    ''' Fecha de confirmacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Property ConfirmDate As String

#End Region

End Class
