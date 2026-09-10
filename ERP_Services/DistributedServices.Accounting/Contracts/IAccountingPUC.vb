#Region "Imports"
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.Text

#End Region

<ServiceContract()>
Public Interface IAccountingPUC

#Region "Methods"

    ''' <summary>
    ''' Funcion para guardar la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function Sp_InsertPUC(mainAccount As MainAccounts) As ActionResult(Of MainAccounts)

    ''' <summary>
    ''' Funcion para eliminar las cuentas contables
    ''' </summary>
    ''' <param name="accounting">The accounting.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteAccounting(ByVal accounting As MainAccounts) As ActionResult

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountByCode(ByVal code As String, ByVal tracking As Boolean, ByVal Session As SessionValues) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo y libro oficial
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountByCodeAndLegalBookId(ByVal code As String, LegalBookId As Integer) As MainAccounts

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAccountById(ByVal id As Integer, ByVal tracking As Boolean, ByVal Session As SessionValues) As MainAccounts

    ''' <summary>
    ''' funcion para listar todas las cuentas
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetAllAcounts() As List(Of MainAccounts)

    <OperationContract()>
    Function GetAccountParentbyCode(ByVal code As String, legalBookId As Integer) As MainAccounts

    ''' <summary>
    ''' Updates the puc.
    ''' </summary>
    ''' <param name="_puc">The _puc.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdatePuc(ByVal _puc As MainAccounts) As ActionResult(Of MainAccounts)

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStatePUC(ByVal code As String, LegalBookId As Integer, ByVal state As Boolean) As ActionResult(Of MainAccounts)

    ''' <summary>
    ''' Valida si la cuenta contable tiene movimientos
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
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
    <OperationContract()>
    Function GetListReportBulletinDefaultersState(DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, session As SessionValues) As DataSet

    ''' <summary>
    ''' Genera el archivo plano de boletin deudores morosos
    ''' </summary>
    ''' <param name="TypeReport"></param>
    ''' <param name="DateCourt"></param>
    ''' <param name="ReportValue"></param>
    ''' <param name="value"></param>
    ''' <param name="PeriodType"></param>
    ''' <param name="PeriodValue"></param>
    ''' <param name="ThirdPartyStart"></param>
    ''' <param name="ThirdPartyEnd"></param>
    ''' <param name="EntityCode"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
    Function GenerateFileCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, InThousands As Boolean, Session As SessionValues) As StringBuilder

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportGeneralBalance
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
    Function GetListReportCertificateRetSource(LegalBookId As Integer, Year As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, InitialDate As DateTime, FinalDate As DateTime, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetIVA
    ''' </summary>
    ''' <param name="DateInitial"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
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
    ''' <param name="CostCenterEnd"></param>>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
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
    <OperationContract()>
    Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues, container As String) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportListMainAccountsFixedAssetByStatusAndBookId realizado para cargar las cuentas contables de Activos fijos
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
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
    <OperationContract()>
    Function GetListReportSubAccount(AccountStart As String, AccountEnd As String, ItemStart As String, ItemEnd As String, EquipmentTypeStart As String, EquipmentTypeEnd As String, BookId As Boolean, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportIncomeAndWithholding realizado para cargar los datos del reporte Ingresos y Retenciones de Nómina 
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="EmployeeId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetListReportIncomeAndWithholding(Year As Integer, EmployeeId As Integer, Session As SessionValues) As DataSet

    ''' <summary>
    ''' Metodo que realiza el llamdo al storeProcedure [SP_ReportCircular015] realizado para reportar el xml de la circular 015
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    <OperationContract()>
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
    <OperationContract()>
    Function GetListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, Session As SessionValues) As DataSet


    ''' <summary>
    ''' Metodo que realiza el llamado a la consulta de maneja Centro de Costo de la Cuenta Contable
    ''' </summary>
    ''' <param name="idProfit"></param>
    ''' <param name="idLost"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetHadleCostCenterByMainAccountId(idProfit As Integer?, idLost As Integer?) As ActionResult
#End Region

End Interface