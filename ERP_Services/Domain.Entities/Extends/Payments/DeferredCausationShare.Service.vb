Imports System.Runtime.Serialization

Partial Public Class DeferredCausationShare

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece la descripcion de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property MainAccountDescription As String

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta contable
    ''' </summary>
    <DataMember()>
    Public Property IdMainAccount As Integer

    ''' <summary>
    ''' Obtiene o establece la descripcion del tercero
    ''' </summary>
    <DataMember()>
    Public Property ThirdPartyDescription As String

    ''' <summary>
    ''' Obtiene o establece el id del tercero
    ''' </summary>
    <DataMember()>
    Public Property IdThirdParty As Integer

    ''' <summary>
    ''' Obtiene o establece el id del centro de costo
    ''' </summary>
    <DataMember()>
    Public Property IdCostCenter As Integer

    ''' <summary>
    ''' Obtiene o establece la descripcion del centro de costo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CostCenterDescription As String

    ''' <summary>
    ''' Obtiene o establece los periodos
    ''' </summary>
    <DataMember()>
    Public Property Periods As Integer

    ''' <summary>
    ''' Obtiene o establece el numero de factura
    ''' </summary>
    <DataMember ()>
    Public Property BillNumber As String

    ''' <summary>
    ''' Obtiene o establece la fecha del periodo
    ''' </summary>
    <DataMember()>
    Public Property DatePeriod As DateTime

    ''' <summary>
    ''' Obtiene o establece el nombre del mes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property MonthName As String

    ''' <summary>
    ''' Obtiene o establece la abreviación de la moneda
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <DataMember()>
    Public Property CurrencyAbbreviation As String

#End Region

End Class
