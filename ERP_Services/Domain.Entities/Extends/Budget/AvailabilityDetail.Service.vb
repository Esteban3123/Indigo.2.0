#Region "imports"
Imports System.Runtime.Serialization
#End Region

Partial Public Class AvailabilityDetail

    ''' <summary>
    ''' Obtiene o establece el id del rubro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CategoryId As Integer

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
    ''' Obtiene o establece el codigo y nombre de la categoria
    ''' </summary>
    <DataMember()>
    Public Property CodeNameCategory As String

    ''' <summary>
    ''' Obtiene o establece el id del tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property RevenueTypeId As Integer

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
    ''' Obtiene o establece el codigo de la disponibilidad
    ''' </summary>
    <DataMember()>
    Public Property CodeAvailability As String

    ''' <summary>
    ''' Obtiene o establece la fecha del documento de disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DateAvailability As Date

    ''' <summary>
    ''' Obtiene o establece la fecha de expiración de la disponibilidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property DateExpirationAvailability As DateTime

    ''' <summary>
    ''' Obtiene o establece el saldo del presupuesto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property ValueBalance As Decimal

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    <DataMember()>
    Public Property CategoryCPCCodeId As Integer?

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
End Class
