'***********************************************************************
' Assembly         : Domain.Billing
' Author           : Diego Andrés Roldán Lozano
' Created          : 17-02-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities

Public Interface ISettingsBillingRepository
    Inherits IRepository(Of SettingsBilling)

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    Function GetSettingsBillingById(id As Integer, tracking As Boolean) As SettingsBilling

    ''' <summary>
    ''' Obtiene un registro de parámetros por unidad operativa
    ''' </summary>
    Function GetCustomTRMByOperatingUnitId(operatingUnitId As Integer, tracking As Boolean) As List(Of CustomTRM)

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    Function GetSettingsBillingByIdUnitOperative(IdUnitOperative As Integer, tracking As Boolean) As SettingsBilling

    ''' <summary>
    ''' Obtiene un registro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative">id de la unidad operativa</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingBillingByUnitOperativeForm(IdUnitOperative As Integer) As SettingsBilling

    ''' <summary>
    ''' Obtiene la cantidad de registros de facturas de tipo de documento 5
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CountBillingInvoice() As Integer

    ''' <summary>
    ''' Funcion que retorna los datos para el reporte de estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials"></param>
    ''' <param name="XmlFilters"></param>
    ''' <returns></returns>
    Function GetReportBillingStadistics(XmlCriterials As String, XmlFilters As String) As List(Of SP_ReportBillingStadistics_Result)

    ''' <summary>
    ''' Funcion que retorna la cantidades de datos que retorna el reporte de estadistico de facturacion
    ''' </summary>
    ''' <param name="XmlCriterials"></param>
    ''' <param name="XmlFilters"></param>
    ''' <returns></returns>
    Function ReportBillingStadisticsCount(XmlCriterials As String, XmlFilters As String) As SP_ReportBillingStadistics_Count_Result

End Interface