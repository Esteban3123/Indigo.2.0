#Region "Imports"
Imports Domain.Entities
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports System.Text
Imports Microsoft.Practices.Unity
#End Region

Partial Public Class AccountingService

#Region "functions"
    ''' <summary>
    ''' Funcion para eliminar las cuentas contables
    ''' </summary>
    ''' <param name="accounting">The accounting.</param>
    ''' <returns></returns>
    Public Function DeleteAccounting(accounting As Domain.Entities.MainAccounts) As ActionResult Implements IAccountingPUC.DeleteAccounting
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteAccounting(accounting, audit)
        End Using
        'Return Me._pucAdminService.DeleteAccounting(accounting, audit)
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por codigo
    ''' </summary>
    ''' <param name="code">code.</param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetAccountByCode(code As String, tracking As Boolean, Session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.MainAccounts Implements IAccountingPUC.GetAccountByCode
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetAccountByCode(code, tracking)
        End Using
        'Return Me._pucAdminService.GetAccountByCode(code, tracking)
    End Function

    ''' <summary>
    ''' Obtiene una cuenta contable por codigo y libro oficial
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="LegalBookId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountByCodeAndLegalBookId(code As String, LegalBookId As Integer) As Domain.Entities.MainAccounts Implements IAccountingPUC.GetAccountByCodeAndLegalBookId
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetAccountByCodeAndLegalBookId(code, LegalBookId)
        End Using
        'Return Me._pucAdminService.GetAccountByCodeAndLegalBookId(code, LegalBookId)
    End Function

    ''' <summary>
    ''' Funcion para obtener la cuenta por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking">if set to <c>true</c> [tracking].</param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetAccountById(id As Integer, tracking As Boolean, Session As Infrastructure.CrossCutting.Base.SessionValues) As Domain.Entities.MainAccounts Implements IAccountingPUC.GetAccountById
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetAccountById(id, tracking)
        End Using
        'Return Me._pucAdminService.GetAccountById(id, tracking)
    End Function

    ''' <summary>
    ''' Funcion para guardar la cuenta contable
    ''' </summary>
    ''' <returns></returns>
    Public Function Sp_InsertPUC(mainAccount As MainAccounts) As Domain.Base.Entities.ActionResult(Of MainAccounts) Implements IAccountingPUC.Sp_InsertPUC
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.Sp_InsertPUC(mainAccount, audit)
        End Using
        'Return _pucAdminService.Sp_InsertPUC(mainAccount, audit)
    End Function

    ''' <summary>
    ''' funcion para listar todas las cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAcounts() As List(Of Domain.Entities.MainAccounts) Implements IAccountingPUC.GetAllAcounts
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetAllAcounts()
        End Using
        'Return _pucAdminService.GetAllAcounts()
    End Function

    ''' <summary>
    ''' Gets the account parentby code.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Public Function GetAccountParentbyCode(code As String, legalBookId As Integer) As Domain.Entities.MainAccounts Implements IAccountingPUC.GetAccountParentbyCode
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.ValidateAccountParent(code, legalBookId)
        End Using
        'Return _pucAdminService.ValidateAccountParent(code)
    End Function

    ''' <summary>
    ''' Updates the puc.
    ''' </summary>
    ''' <param name="_puc">The _puc.</param>
    ''' <returns></returns>
    Public Function UpdatePuc(_puc As Domain.Entities.MainAccounts) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MainAccounts) Implements IAccountingPUC.UpdatePuc
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.UpdatePuc(_puc, audit)
        End Using
        'Return Me._pucAdminService.UpdatePuc(_puc, audit)
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Function UpdateStatePUC(code As String, LegalBookId As Integer, state As Boolean) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MainAccounts) Implements IAccountingPUC.UpdateStatePUC
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.UpdateStatePUC(code, LegalBookId, state, audit)
        End Using
        'Return Me._pucAdminService.UpdateStatePUC(code, LegalBookId, state, audit)
    End Function

    ''' <summary>
    ''' Valida si la cuenta contable tiene movimientos
    ''' </summary>
    ''' <param name="MainAccountId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateMovementsOfMainAccount(MainAccountId As Integer) As Boolean Implements IAccountingPUC.ValidateMovementsOfMainAccount
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.ValidateMovementsOfMainAccount(MainAccountId)
        End Using
        'Return Me._pucAdminService.ValidateMovementsOfMainAccount(MainAccountId)
    End Function

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
    Public Function GetListReportBulletinDefaultersState(DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportBulletinDefaultersState
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportBulletinDefaultersState(DateCourt, ReportValue, value, PeriodType, PeriodValue, ThirdPartyStart, ThirdPartyEnd, session)
        End Using
        'Return Me._pucAdminService.GetListReportBulletinDefaultersState(DateCourt, ReportValue, value, PeriodType, PeriodValue, ThirdPartyStart, ThirdPartyEnd, session)
    End Function

    ''' <summary>
    ''' Genera el archivo plano de boletin deudores morosos
    ''' </summary>
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
    Public Function GenerateArchiveBulletinDefaultersState(TypeReport As Integer, DateCourt As String, ReportValue As Boolean, value As Decimal, PeriodType As Byte, PeriodValue As Integer, ThirdPartyStart As String, ThirdPartyEnd As String, EntityCode As String, session As SessionValues) As StringBuilder Implements IAccountingPUC.GenerateArchiveBulletinDefaultersState
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GenerateArchiveBulletinDefaultersState(TypeReport, DateCourt, ReportValue, value, PeriodType, PeriodValue, ThirdPartyStart, ThirdPartyEnd, EntityCode, session)
        End Using
        'Return Me._pucAdminService.GenerateArchiveBulletinDefaultersState(DateCourt, ReportValue, value, PeriodType, PeriodValue, ThirdPartyStart, ThirdPartyEnd, EntityCode, session)
    End Function

    ''' <summary>
    ''' Genera el archivo plano del CGN002
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="LevelSubAccount"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GenerateFileCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer, Session As SessionValues) As Text.StringBuilder Implements IAccountingPUC.GenerateFileCGN002
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GenerateFileCGN002(DateStart, DateEnd, AccountStart, AccountEnd, LevelSubAccount, BookId, Session)
        End Using
        'Return Me._pucAdminService.GenerateFileCGN002(DateStart, DateEnd, AccountStart, AccountEnd, LevelSubAccount, BookId, Session)
    End Function

    ''' <summary>
    ''' Obtiene el dataSet del CGN002
    ''' </summary>
    ''' <param name="DateStart"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="AccountStart"></param>
    ''' <param name="AccountEnd"></param>
    ''' <param name="LevelSubAccount"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCGN002(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, LevelSubAccount As Boolean, BookId As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportCGN002
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCGN002(DateStart, DateEnd, AccountStart, AccountEnd, LevelSubAccount, BookId, Session)
        End Using
        'Return Me._pucAdminService.GetListReportCGN002(DateStart, DateEnd, AccountStart, AccountEnd, LevelSubAccount, BookId, Session)
    End Function

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
    Public Function GenerateFileCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, InThousands As Boolean, Session As SessionValues) As Text.StringBuilder Implements IAccountingPUC.GenerateFileCGN001
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GenerateFileCGN001(DateStart, DateEnd, AccountStart, AccountEnd, AccountingZero, BookId, InThousands, Session)
        End Using
        'Return Me._pucAdminService.GenerateFileCGN001(DateStart, DateEnd, AccountStart, AccountEnd, AccountingZero, BookId, InThousands, Session)
    End Function

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
    Public Function GetListReportCGN001(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, AccountingZero As Boolean, BookId As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportCGN001
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCGN001(DateStart, DateEnd, AccountStart, AccountEnd, AccountingZero, BookId, Session)
        End Using
        'Return Me._pucAdminService.GetListReportCGN001(DateStart, DateEnd, AccountStart, AccountEnd, AccountingZero, BookId, Session)
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportGeneralBalance
    ''' </summary>
    ''' <param name="criterias"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportGeneralBalance(criterias As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportGeneralBalance
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportGeneralBalance(criterias, Session)
        End Using
    End Function

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
    Public Function GetListReportLedgerAndBalance(month As Integer, ano As Integer, AccountingZero As Boolean, BookId As Integer, accountLevel As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportLedgerAndBalance
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportLedgerAndBalance(month, ano, AccountingZero, BookId, accountLevel, Session)
        End Using
        'Return Me._pucAdminService.GetListReportLedgerAndBalance(month, ano, AccountingZero, BookId, accountLevel, Session)
    End Function

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
    Public Function GetListReportRetentions(criterias As Dictionary(Of String, String), filters As Dictionary(Of String, String), Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportRetentions
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportRetentions(criterias, filters, Session)
        End Using
    End Function

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
    Public Function GetListReportCertificateRetSource(LegalBookId As Integer, Year As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, InitialDate As DateTime, FinalDate As DateTime, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportCertificateRetSource
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCertificateRetSource(LegalBookId, Year, AccountStart, AccountEnd, NitStart, NitEnd, InitialDate, FinalDate, Session)
        End Using
        'Return Me._pucAdminService.GetListReportCertificateRetSource(LegalBookId, Year, AccountStart, AccountEnd, NitStart, NitEnd, Session)
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_CertificateRetIVA
    ''' </summary>
    ''' <param name="DateInitial"></param>
    ''' <param name="DateEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportCertificateRetIVA(DateInitial As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, RetencionType As Integer, LegalBookId As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportCertificateRetIVA
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCertificateRetIVA(DateInitial, DateEnd, AccountStart, AccountEnd, NitStart, NitEnd, RetencionType, LegalBookId, Session)
        End Using
        'Return Me._pucAdminService.GetListReportCertificateRetIVA(DateInitial, DateEnd, AccountStart, AccountEnd, NitStart, NitEnd, RetencionType, LegalBookId, Session)
    End Function

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
    Public Function GetListReportCertificateRetICA(DateStart As Date, DateEnd As Date, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, RetencionType As Integer, LegalBookId As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportCertificateRetICA
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCertificateRetICA(DateStart, DateEnd, AccountStart, AccountEnd, NitStart, NitEnd, RetencionType, LegalBookId, Session)
        End Using
    End Function

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
    Public Function GetListReportInventoryAndBalance(monthStart As Integer, monthEnd As Integer, ano As Integer, AccountingZero As Boolean, BookId As Integer, accountLevel As Integer, AllowThirdParty As Boolean, InitialAccount As Integer, FinalAccount As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportInventoryAndBalance
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportInventoryAndBalance(monthStart, monthEnd, ano, AccountingZero, BookId, accountLevel, AllowThirdParty, InitialAccount, FinalAccount, Session)
        End Using
        'Return Me._pucAdminService.GetListReportInventoryAndBalance(monthStart, monthEnd, ano, AccountingZero, BookId, accountLevel, AllowThirdParty, InitialAccount, FinalAccount, Session)
    End Function

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
    Public Function GetListReportAuxiliar(InitialDate As Date, EndDate As Date, Summarized As Boolean, Criteria As Integer, SubCriteria As Integer, BookId As Integer, Status As Integer, OrderBy As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, CostCenterStart As String, CostCenterEnd As String, AccumulatedBalance As Boolean, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportAuxiliar
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportAuxiliar(InitialDate, EndDate, Summarized, Criteria, SubCriteria, BookId, Status, OrderBy, AccountStart, AccountEnd, NitStart, NitEnd, CostCenterStart, CostCenterEnd, AccumulatedBalance, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportAuxiliar con soporte para paginación
    ''' </summary>
    Public Function GetListReportAuxiliarPaged(InitialDate As Date, EndDate As Date, Summarized As Boolean, Criteria As Integer, SubCriteria As Integer, BookId As Integer, Status As Integer, OrderBy As Integer, AccountStart As String, AccountEnd As String, NitStart As String, NitEnd As String, CostCenterStart As String, CostCenterEnd As String, AccumulatedBalance As Boolean, PageNumber As Integer, PageSize As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportAuxiliarPaged
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportAuxiliarPaged(InitialDate, EndDate, Summarized, Criteria, SubCriteria, BookId, Status, OrderBy, AccountStart, AccountEnd, NitStart, NitEnd, CostCenterStart, CostCenterEnd, AccumulatedBalance, PageNumber, PageSize, Session)
        End Using
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure [Budget].[SP_ReportExpenditureBudgetSituation]
    ''' </summary>
    ''' <param name="ValidityId"></param>
    ''' <param name="CodeCategoryStart"></param>
    ''' <param name="CodeCategoryEnd"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReportExpenditureBudgetSituation(ValidityId As Integer, CodeCategoryStart As String, CodeCategoryEnd As String, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportExpenditureBudgetSituation
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportExpenditureBudgetSituation(ValidityId, CodeCategoryStart, CodeCategoryEnd, Session)
        End Using
        'Return Me._pucAdminService.GetListReportExpenditureBudgetSituation(ValidityId, CodeCategoryStart, CodeCategoryEnd, Session)
    End Function

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
    Public Function GetListReportCostMeasurementUnit(initialMonth As Integer, initialYear As Integer, endMonth As Integer, endYear As Integer, initialMeasurementUnitCode As String, endMeasurementUnitCode As String, ProductionCenterId As Integer, Session As SessionValues, container As String) As DataSet Implements IAccountingPUC.GetListReportCostMeasurementUnit
        Using service As IPUCAdminService = DistributedServices.Accounting.Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId, Session, container)
        End Using
        'Return Me._pucAdminService.GetListReportCostMeasurementUnit(initialMonth, initialYear, endMonth, endYear, initialMeasurementUnitCode, endMeasurementUnitCode, ProductionCenterId, Session, container)
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportListMainAccountsFixedAssetByStatusAndBookId realizado para cargar las cuentas contables de Activos fijos
    ''' </summary>
    ''' <param name="Status"></param>
    ''' <param name="BookId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListReporMainAccountsFixedAssetByStatusAndBookId(Status As Boolean, BookId As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReporMainAccountsFixedAssetByStatusAndBookId
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReporMainAccountsFixedAssetByStatusAndBookId(Status, BookId, Session)
        End Using
        'Return Me._pucAdminService.GetListReporMainAccountsFixedAssetByStatusAndBookId(Status, BookId, Session)
    End Function

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
    Public Function GetListReportSubAccount(AccountStart As String, AccountEnd As String, ItemStart As String, ItemEnd As String, EquipmentTypeStart As String, EquipmentTypeEnd As String, BookId As Boolean, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportSubAccount
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportSubAccount(AccountStart, AccountEnd, ItemStart, ItemEnd, EquipmentTypeStart, EquipmentTypeEnd, BookId, Session)
        End Using
        'Return Me._pucAdminService.GetListReportSubAccount(AccountStart, AccountEnd, ItemStart, ItemEnd, EquipmentTypeStart, EquipmentTypeEnd, BookId, Session)
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamado al storeProcedure SP_ReportIncomeAndWithholding realizado para cargar los datos del reporte Ingresos y Retenciones de Nómina 
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="EmployeeId"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportIncomeAndWithholding(Year As Integer, EmployeeId As Integer, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportIncomeAndWithholding
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportIncomeAndWithholding(Year, EmployeeId, Session)
        End Using
        'Return Me._pucAdminService.GetListReportIncomeAndWithholding(Year, EmployeeId, Session)
    End Function

    ''' <summary>
    ''' Metodo que realiza el llamdo al storeProcedure [SP_ReportCircular015] realizado para reportar el xml de la circular 015
    ''' </summary>
    ''' <param name="InitialDate"></param>
    ''' <param name="EndDate"></param>
    ''' <param name="Session"></param>
    ''' <returns></returns>
    Public Function GetListReportCircular015(InitialDate As Date, EndDate As Date, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportCircular015
        Using service As IPUCAdminService = Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportCircular015(InitialDate, EndDate, Session)
        End Using
        'Return Me._pucAdminService.GetListReportCircular015(InitialDate, EndDate, Session)
    End Function

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
    Public Function GetListReportOperatingProductionCenter(InitialMonth As Integer, EndMonth As Integer, Year As Integer, Container As String, CodePCenterIni As String, CodePCenterFin As String, Session As SessionValues) As DataSet Implements IAccountingPUC.GetListReportOperatingProductionCenter
        Using service As IPUCAdminService = DistributedServices.Accounting.Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetListReportOperatingProductionCenter(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, Session)
        End Using
        'Return Me._pucAdminService.GetListReportOperatingProductionCenter(InitialMonth, EndMonth, Year, Container, CodePCenterIni, CodePCenterFin, Session)
    End Function

    ''' <summary>
    ''' Obtiene si la cuenta contable maneja centro de costo
    ''' </summary>
    ''' <param name="idProfit"></param>
    ''' <param name="idLost"></param>
    ''' <returns></returns>
    Public Function GetHadleCostCenterByMainAccountId(idProfit As Integer?, idLost As Integer?) As ActionResult Implements IAccountingPUC.GetHadleCostCenterByMainAccountId
        Using service As IPUCAdminService = DistributedServices.Accounting.Container.Current.Resolve(Of IPUCAdminService)()
            Return service.GetHadleCostCenterByMainAccountId(idProfit, idLost)
        End Using
    End Function
#End Region

End Class
