Imports System.Runtime.Serialization
Partial Public Class Budget

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el codigo de la categoria
    ''' </summary>
    <DataMember()>
    Public Property CodeCategory As String

    ''' <summary>
    ''' Obtiene o establece el nombre de la categoria
    ''' </summary>
    <DataMember()>
    Public Property NameCategory As String

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre del tipo de ingreso
    ''' </summary>
    <DataMember()>
    Public Property CodeNameRevenueType As String

    ''' <summary>
    ''' Obtiene o establece el id de la fuente de financiacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property FinancialSourceId As Integer

    ''' <summary>
    ''' Obtiene o establece el codigo y nombre de la fuente de financiacion
    ''' </summary>
    <DataMember()>
    Public Property CodeNameFinancialSource As String

    ''' <summary>
    ''' Obtiene o establece la naturaleza
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property Nature As Byte

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CCPETCodeId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CPCCodeId As Integer?

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CodeNameCCPET As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CodeNameCPC As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CCPETLinkAccount As Boolean

#End Region

End Class
