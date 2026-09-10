'***********************************************************************
' Assembly         : Application.Accounting.PUCAdminService
' Author           : Sergio Abraham Fernandez Cruz
' Created          : 02-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Text

#End Region

Public Interface IPUCAdminService
    Inherits IDisposable

#Region "Imports"

    ''' <summary>
    ''' Funcion para guardar la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Function Sp_InsertPUC(mainAccount As MainAccounts, audit As AuditMessage) As ActionResult(Of MainAccounts)

    ''' <summary>
    ''' Funcion para eliminar las cuentas contables
    ''' </summary>
    ''' <param name="accounting">The accounting.</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAccounting(ByVal accounting As MainAccounts, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Funcion para validar si la cuenta maneja centro de costo
    ''' </summary>
    ''' <param name="idAccount"></param>
    ''' <returns></returns>
    Function MainAccountHandlesCostCenter(idAccount As Integer) As Boolean

    ''' <summary>
    ''' Funcion para validar si la cuenta maneja tercero
    ''' </summary>
    ''' <param name="idAccount"></param>
    ''' <returns></returns>
    Function MainAccountHandlesThirdParty(idAccount As Integer) As Boolean

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountByCode(ByVal code As String, ByVal tracking As Boolean) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo y libro oficial
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <returns></returns>
    Function GetAccountByCodeAndLegalBookId(ByVal code As String, LegalBookId As Integer) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    Function GetAccountById(ByVal id As Integer, ByVal tracking As Boolean) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener todas las cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Function GetAllAcounts() As List(Of MainAccounts)

    ''' <summary>
    ''' Validates the account parent.
    ''' </summary>
    ''' <param name="codeAccount">The code account.</param>
    ''' <returns></returns>
    Function ValidateAccountParent(ByVal codeAccount As String, legalBookId As Integer) As MainAccounts

    ''' <summary>
    ''' Updates the puc.
    ''' </summary>
    ''' <param name="puc">The puc.</param>
    ''' <returns></returns>
    Function UpdatePuc(ByVal puc As MainAccounts, audit As AuditMessage) As ActionResult(Of MainAccounts)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function UpdateStatePUC(ByVal code As String, LegalBookId As Integer, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of MainAccounts)

    ''' <summary>
    ''' Valida si la cuenta contable tiene movimientos
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateMovementsOfMainAccount(ByVal MainAccountId As Integer) As Boolean

    ''' <summary>
    ''' Obtiene el listado de disponibilidades para cargar el datasource del reporte Boletín de deudores morosos del estado
    ''' </summary>
    ''' <param name="DateCourt"></param>
    ''' <param name="ReportValue"></param>
    ''' <param name="value"></param>
    ''' <param name="PeriodType"></param>
    ''' <param name="PeriodValue"></param>
    ''' <param name="ThirdPartyStart"></param>
    ''' <param name="ThirdPartyEnd"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportBulletinDefaultersState(DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Genera el archivo plano de boletin de deudores morosos
    ''' </summary>
    ''' <param name="DateCourt"></param>
    ''' <param name="ReportValue"></param>
    ''' <param name="value"></param>
    ''' <param name="PeriodType"></param>
    ''' <param name="PeriodValue"></param>
    ''' <param name="ThirdPartyStart"></param>
    ''' <param name="ThirdPartyEnd"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateArchiveBulletinDefaultersState(TypeReport As Integer, DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, EntityCode As String, session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al StoreProcedure de SP_ReportCGN002
    ''' </summary>
    ''' <param name="DateStart">Fecha inicial</param>
    ''' <param name="DateEnd">Fecha final</param>
    ''' <param name="AccountStart">Cuenta contable inicial</param>
    ''' <param name="AccountEnd">Cuenta contable final</param>
    ''' <param name="LevelSubAccount">Nivel de la SubCuenta</param>
    ''' <param name="BookId">Libro oficial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Genera el archivo plano de CGN002
    ''' </summary>
    ''' <param name="DateStart">Fecha inicial</param>
    ''' <param name="DateEnd">Fecha final</param>
    ''' <param name="AccountStart">Cuenta contable inicial</param>
    ''' <param name="AccountEnd">Cuenta contable final</param>
    ''' <param name="LevelSubAccount">Nivel de la SubCuenta</param>
    ''' <param name="BookId">Libro oficial</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateFileCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer, Session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al StoreProcedure de SP_ReportCGN001
    ''' </summary>
    ''' <param name="DateStart">Fecha inicial</param>
    ''' <param name="DateEnd">Fecha final</param>
    ''' <param name="AccountStart">Cuenta contable inicial</param>
    ''' <param name="AccountEnd">Cuenta contable final</param>
    ''' <param name="AccountingZero">Cuenta cero</param>
    ''' <param name="BookId">Libro oficial</param>
    ''' <param name="Session">Variable de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Genera el archivo plano de CGN001
    ''' </summary>
    ''' <param name="DateStart">Fecha inicial</param>
    ''' <param name="DateEnd">Fecha final</param>
    ''' <param name="AccountStart">Cuenta contable inicial</param>
    ''' <param name="AccountEnd">Cuenta contable final</param>
    ''' <param name="AccountingZero">Cuenta cero</param>
    ''' <param name="BookId">Libro oficial</param>
    ''' <param name="Session">Variable de sesion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GenerateFileCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, InThousands As Boolean, Session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportGeneralBalance
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportGeneralBalance(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportLedgerAndBalance
    ''' </summary>
    ''' <param name="month"></param>
    ''' <param name="ano"></param>
    ''' <param name="AccountingZero"></param>
    ''' <param name="BookId"></param>
    ''' <param name="accountLevel"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportLedgerAndBalance(month As Integer, ano As Integer, AccountingZero As Boolean, BookId As Integer, accountLevel As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportRetentions
    ''' </summary>
    ''' <param name="DateInitial"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="LegalBookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportRetentions(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetSource
    ''' </summary>
    ''' <param name="LegalBookId"></param>
    ''' <param name="Year"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportCertificateRetSource(LegalBookId As Integer, Year As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, InitialDate As DateTime, FinalDate As DateTime, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetIVA
    ''' </summary>
    ''' <param name="DateInitial"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportCertificateRetIVA(DateInitial As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, RetencionType As Integer, LegalBookId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetICA
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="RetencionType"></param>
    ''' <param name="LegalBookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportCertificateRetICA(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, RetencionType As Integer, LegalBookId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportInventoryAndBalance
    ''' </summary>
    ''' <param name="monthStart"></param>
    ''' <param name="monthEnd"></param>
    ''' <param name="ano"></param>
    ''' <param name="AccountingZero"></param>
    ''' <param name="BookId"></param>
    ''' <param name="accountLevel"></param>
    ''' <param name="allowThirdParty"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportInventoryAndBalance(monthStart As Integer, monthEnd As Integer, ano As Integer, AccountingZero As Boolean, BookId As Integer, accountLevel As Integer, AllowThirdParty As Boolean, InitialAccount As Integer, FinalAccount As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportAuxiliar
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="Summarized"></param>
    ''' <param name="Criteria"></param>
    ''' <param name="SubCriteria"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Status"></param>
    ''' <param name="OrderBy"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="NitStart"></param>
    ''' <param name="NitEnd"></param>
    ''' <param name="CostCenterStart"></param>
    ''' <param name="CostCenterEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportAuxiliar(InitialDate As Date, EndDate As Date, Summarized As Boolean, Criteria As Integer, SubCriteria As Integer, BookId As Integer, Status As Integer, OrderBy As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, CostCenterStart As String, CostCenterEnd As String, AccumulatedBalance As Boolean, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportAuxiliar con soporte para paginación
    ''' </summary>
    ''' <param name="InitialDate">Fecha inicial</param>
    ''' <param name="EndDate">Fecha final</param>
    ''' <param name="Summarized">Resumido</param>
    ''' <param name="Criteria">Criterio principal</param>
    ''' <param name="SubCriteria">Sub criterio</param>
    ''' <param name="BookId">Id del libro</param>
    ''' <param name="Status">Estado</param>
    ''' <param name="OrderBy">Ordenamiento</param>
    ''' <param name="AccountStart">Cuenta inicial</param>
    ''' <param name="AccountEnd">Cuenta final</param>
    ''' <param name="NitStart">Nit inicial</param>
    ''' <param name="NitEnd">Nit final</param>
    ''' <param name="CostCenterStart">Centro de costo inicial</param>
    ''' <param name="CostCenterEnd">Centro de costo final</param>
    ''' <param name="AccumulatedBalance">Saldo acumulado</param>
    ''' <param name="PageNumber">Número de página (1-based)</param>
    ''' <param name="PageSize">Tamaño de página</param>
    ''' <param name="Session">Valores de sesión</param>
    ''' <returns>DataSet con los datos paginados y el total de registros</returns>
    Function GetListReportAuxiliarPaged(InitialDate As Date, EndDate As Date, Summarized As Boolean, Criteria As Integer, SubCriteria As Integer, BookId As Integer, Status As Integer, OrderBy As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, CostCenterStart As String, CostCenterEnd As String, AccumulatedBalance As Boolean, PageNumber As Integer, PageSize As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure [Budget].[SP_ReportExpenditureBudgetSituation]
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategoryStart"></param>
    ''' <param name="CodeCategoryEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportExpenditureBudgetSituation(ValidityId As Integer, CodeCategoryStart As String, CodeCategoryEnd As String, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure spReportCostMeasurementUnit
    ''' </summary>
    ''' <param name="initialMonth"></param>
    ''' <param name="initialYear"></param>
    ''' <param name="endMonth"></param>
    ''' <param name="endYear"></param>
    ''' <param name="initialMeasurementUnitCode"></param>
    ''' <param name="endMeasurementUnitCode"></param>
    ''' <param name="ProductionCenterId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues, container As String) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportListMainAccountsFixedAssetByStatusAndBookId realizado para cargar las cuentas contables de Activos fijos
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReporMainAccountsFixedAssetByStatusAndBookId(Status As Boolean, BookId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportListReportSubAccount realizado para cargar los datos del reporte de Subcuentas de Activos fijos
    ''' </summary>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="ItemStart"></param>
    ''' <param name="ItemEnd"></param>
    ''' <param name="EquipmentTypeStart"></param>
    ''' <param name="EquipmentTypeEnd"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListReportSubAccount(AccountStart As String, AccountEnd As String, ItemStart As String, ItemEnd As String, EquipmentTypeStart As String, EquipmentTypeEnd As String, BookId As Boolean, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportIncomeAndWithholding realizado para cargar los datos del reporte Ingresos y Retenciones de Nómina 
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="EmployeeId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportIncomeAndWithholding(Year As Integer, EmployeeId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamdo al storeProcedure [SP_ReportCircular015] realizado para reportar el xml de la circular 015
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportCircular015(InitialDate As Date, EndDate As Date, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al stored Procedure SP_ReportOperatingResultProductionCenter realizado para cargar los datos del reporte de Estructura Organizacional ProductionCenter
    ''' </summary>
    ''' <param name="InitialMonth"></param>
    ''' <param name="EndMonth"></param>
    ''' <param name="Year"></param>
    ''' <param name="Container"></param>
    ''' <param name="CodePCenterIni"></param>
    ''' <param name="CodePCenterFin"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Function GetListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado a la consulta de maneja Centro de Costo de la Cuenta Contable
    ''' </summary>
    ''' <param name="idProfit"></param>
    ''' <param name="idLost"></param>
    ''' <returns></returns>
    Function GetHadleCostCenterByMainAccountId(idProfit As Integer?, idLost As Integer?) As ActionResult
#End Region

End Interface