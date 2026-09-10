'************************************************************
' Assembly         : Domain.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 2014-11-27
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports System.Dynamic
Imports System.Data.Entity.Core.Objects

#End Region

Public Interface IRevenueControlDetailRepository
    Inherits IRepository(Of RevenueControlDetail)

    Function UpdateRevenueControlDetailValues(id As Integer, Optional operativeUnitId As Integer? = Nothing) As SP_UpdateRevenueControlDetailValues_Result

    Function UpdateRevenueControlDetailValuesNew(id As Integer, Optional operativeUnitId As Integer? = Nothing) As SP_UpdateRevenueControlDetailValuesNew_Result
    Function GetRevenueControlDetailByIdWithIncludes(id As Integer, Optional includes() As String = Nothing) As RevenueControlDetail

    ''' <summary>
    ''' obtiene un detalle del folio por grupo de atencion y tercero
    ''' </summary>
    ''' <param name="CareGroupId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueControlDetailByCareGroupIdThirdPartyId(RevenueControlId As Integer, CareGroupId As Integer, ThirdpartyId As Integer) As RevenueControlDetail

    ''' <summary>
    ''' Obtiene el id del folio, si no existe devuelve 0
    ''' </summary>
    ''' <param name="RevenueControlId"></param>
    ''' <param name="CareGroupId"></param>
    ''' <param name="ThirdpartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetIdByCareGroupIdThirdPartyId(RevenueControlId As Integer, CareGroupId As Integer, ThirdpartyId As Integer) As Integer

    ''' <summary>
    ''' retorna el maximo numero de folio
    ''' </summary>
    ''' <param name="RevenueControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetMaxFolioOrder(RevenueControlId As Integer) As Integer
    ''' <summary>
    ''' obtiene el listado de los detalles
    ''' </summary>
    ''' <param name="RevenueControlId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueControlDetailByRevenueControlId(RevenueControlId As Integer) As List(Of RevenueControlDetail)
    ''' <summary>
    ''' odtiene un detalle del folio por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueControlDetailById(Id As Integer) As RevenueControlDetail
    ''' <summary>
    ''' odtiene un detalle del folio por id sin agregados
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueControlDetailByIdNoAdded(Id As Integer) As RevenueControlDetail
    ''' <summary>
    ''' odtiene un detalle del folio por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueControlDetailWithAggregatesById(Id As Integer) As RevenueControlDetail

    ''' <summary>
    ''' Lista los folios en los que se encuentra distribuida una estancias
    ''' </summary>
    ''' <param name="idStay">Id de la estancia</param>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Lista de folios</returns>
    Function ListStayFolios(ByVal idStay As Integer, ByVal admissionNumber As String) As List(Of Object)
    ''' <summary>
    ''' obtiene un folio por id del detalle de la orden de servicio
    ''' </summary>
    ''' <param name="serviceOrderDetailId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRevenueControlDetailByServiceOrderDetailId(serviceOrderDetailId As Integer) As RevenueControlDetail

    ''' <summary>
    ''' Generates the journal voucher details.
    ''' </summary>
    ''' <param name="folioId">The folio identifier.</param>
    ''' <param name="OperativeUnitId">The operative unit identifier.</param>
    ''' <returns></returns>
    Function GenerateJournalVoucherDetails(folioId As Integer, OperativeUnitId As Integer, reverse As Boolean, invoiceAnullateId As Integer?) As List(Of SP_GenerateJournalVoucherDetails_Result)
    Function IncludeInOtherService(xmlData As String) As SP_IncludeInOtherService_Result

    ''' <summary>
    ''' Gets the anullate invoice result.
    ''' </summary>
    ''' <param name="folioId">The folio identifier.</param>
    ''' <param name="ReversalReasonId">The reversal reason identifier.</param>
    ''' <param name="ReversalDescription">The reversal description.</param>
    ''' <param name="UserCode">The user code.</param>
    ''' <param name="containerHis">The container his.</param>
    ''' <param name="patientCode">The patient code.</param>
    ''' <returns></returns>
    Function SP_AnulateInvoice(OperativeUnitId As Integer, folioId As Integer, ReversalReasonId As Integer, ReversalDescription As String, UserCode As String, containerHis As String, patientCode As String, CompanyType As Byte) As SP_AnulateInvoice_Result

    ''' <summary>
    ''' obtiene el modo de impresión de contrato por el id del detalle del folio
    ''' </summary>
    ''' <param name="idRevenueControl"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPrintgModeByIdRevenueControl(idRevenueControl As Integer) As Byte

End Interface
