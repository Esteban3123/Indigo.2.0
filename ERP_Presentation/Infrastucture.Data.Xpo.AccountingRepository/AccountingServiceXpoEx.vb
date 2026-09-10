'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Xpo.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports DevExpress.Data.PLinq
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class AccountingServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"
    Public Function GetCountVouchers(id As Integer) As Integer
        Dim session As New IndigoXPOSession(Of JournalVoucherDetailXpo)() 'XpoDefault.DataLayer
        Dim count As Integer = session.Evaluate(GetType(JournalVoucherDetailXpo),
                                                    CriteriaOperator.Parse("Count()"),
                                                    CriteriaOperator.Parse($"IdAccounting.Id = {id}"))
        Return count
    End Function

    Public Function GetDebitCreditVouchers(id As Integer, Optional debit As Boolean = True) As Decimal
        Dim session As New IndigoXPOSession(Of JournalVoucherDetailXpo)()
        Dim value As Decimal
        If debit Then
            value = CDec(session.Evaluate(GetType(JournalVoucherDetailXpo),
                                              CriteriaOperator.Parse("Sum(DebitValue)"),
                                              CriteriaOperator.Parse($"IdAccounting.Id = {id}")))
        Else
            value = CDec(session.Evaluate(GetType(JournalVoucherDetailXpo),
                                              CriteriaOperator.Parse("Sum(CreditValue)"),
                                              CriteriaOperator.Parse($"IdAccounting.Id = {id}")))
        End If
        Return value
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListAllBook() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                               "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook;CommonCurrency.CurrencyName",
                                               Nothing)
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListBook() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                               "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook",
                                               CriteriaOperator.Parse($"TypeBook IN (1, 2)"))
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListAllBookByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                               "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook;LastYearClose",
                                               CriteriaOperator.Parse($"Status={status}"))
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                               "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook;LastYearClose",
                                               CriteriaOperator.Parse($"Status={status} AND TypeBook IN (1, 2)"))
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatusAndNotInBookId(status As Boolean, bookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                               "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName",
                                               CriteriaOperator.Parse($"Status={status} And not Id in ({bookId}) AND TypeBook IN (1, 2)"))
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookAllByStatusAndOfficialBook(status As Boolean, OfficialBook As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                                         "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook",
                                                         CriteriaOperator.Parse($"Status={status} And OfficialBook={OfficialBook}"))
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatusAndOfficialBook(status As Boolean, OfficialBook As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(BookXpo)),
                                                         "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook",
                                                         CriteriaOperator.Parse($"Status={status} And OfficialBook={OfficialBook} AND TypeBook IN (1, 2)"))
        Return xp
    End Function

    Public Function GetLegalBook() As XPCollection
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Return New XPCollection(session, GetType(BookXpo), CriteriaOperator.Parse($"Status={True} And OfficialBook={True} AND TypeBook IN (1, 2)"))
        'End Using
    End Function

    ''' <summary>
    ''' Metodo que devuelve el libro contable según el Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetBookById(Id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Return New XPCollection(session, GetType(BookXpo),
                                    CriteriaOperator.Parse($"Id = {Id}"))
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListAllBookByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Return New XPCollection(session, GetType(BookXpo),
                                    CriteriaOperator.Parse($"Status = {status}"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatusXpCollection(status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Return New XPCollection(session, GetType(BookXpo),
                                    CriteriaOperator.Parse($"Status = {status} AND TypeBook IN (1, 2, 3)"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookJournalVoucherHomologations(BookId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of BookXpo)()
        Return New XPCollection(session, GetType(BookXpo),
                                    CriteriaOperator.Parse($"Status = 1 And Id <> {BookId} AND TypeBook IN (1, 2)"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListVieBotByForm(Form As String) As XPCollection
        Dim session As New IndigoXPOSession(Of VieBotXpo)()
        Return New XPCollection(session, GetType(VieBotXpo),
                                    CriteriaOperator.Parse($"Form = '{Form}'"))
        'End Using
    End Function

    ''' <summary>
    ''' lista todos los comprobantes por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherReportFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of JournalVouchersXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(JournalVouchersXpo)),
                                                         "Consecutive;VoucherDate;Detail;IdJournalVoucher.Name",
                                                         CriteriaOperator.Parse(filtro))
        serverMode.DefaultSorting = "Consecutive"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastClosingDate() As Date
        Dim session As New IndigoXPOSession(Of GeneralLedgerCompanySettingsXpo)()
        Dim LastClosingDate As Date
        Dim collect As XPCollection(Of GeneralLedgerCompanySettingsXpo) = New XPCollection(Of GeneralLedgerCompanySettingsXpo)(session)
        If collect.ToList().Count() > 0 Then
            LastClosingDate = collect.Max(Function(x) x.LastClosingDate)
        End If
        Return LastClosingDate
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListDocumentTypes(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentTypeXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentTypeXpo)),
                                                         "Id;Code;Name;Consecutive;CodeName",
                                                         CriteriaOperator.Parse($"Status={status}"))
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListAccountClass() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountClassXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(AccountClassXpo)),
                                                         "Code;Name", Nothing)
        Return xp
    End Function


    Public Function ListMainAccountLevels() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralLedgerMainAccountsLevelsXpo)()
        Dim _classEntity = session.GetClassInfo(GetType(GeneralLedgerMainAccountsLevelsXpo))
        Return New XPInstantFeedbackSource(_classEntity, "Id;Code;Name;Level;Length;digits", Nothing)
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListAccountLevel() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountLevelXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(AccountLevelXpo)),
                                                         "Level;Length", Nothing)
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListRetentionConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RetentionConceptXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(RetentionConceptXpo)),
                                                         "Id;Code;Name;CodeName", Nothing)
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los conceptos de retencion por tipo de retencion
    ''' </summary>
    Public Function ListRetentionConceptByTypeRetention(retention As String, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RetentionConceptXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(RetentionConceptXpo)),
                                                         "Id;Code;Name;CodeName;Rate",
                                                         CriteriaOperator.Parse($"Status={status} And Retention In ({retention})"))
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListStatementFolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of StatementFolioXpo)()
        Dim classEntity = session.GetClassInfo(GetType(StatementFolioXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPatrimonialPart() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PatrimonialPartXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PatrimonialPartXpo)),
                                                     "Id;Code;Name;Part", Nothing)
    End Function

    ''' <summary>
    ''' Lista todos centros de costos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostcenterReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CostCenterXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(CostCenterXpo)),
                                                         "Id;Code;Name;State;CodeName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(CommonThirdPartyXpo)),
                                                         "Id;Nit;Name;ContributionType;RetentionType;PersonType;State;PersonTypeName;ContributionTypeName;RetentionTypeName;StateName;PersonId.IdentificationTypeName;NitName", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyByState(state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(CommonThirdPartyXpo)),
                                                     "Id;Nit;Name;ContributionType;RetentionType;PersonType;State;PersonTypeName;ContributionTypeName;RetentionTypeName;StateName;PersonId.IdentificationTypeName;NitName",
                                                     CriteriaOperator.Parse($"State={state}"))
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsReport() As XPCollection(Of MainAccountsXpo)
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim collect As XPCollection(Of MainAccountsXpo) = New XPCollection(Of MainAccountsXpo)(session)
        Return collect
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMainAccountsByStatusAndBookIdHandledThirdParty(status As Boolean, bookId As Integer, HandlesThirdParty As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountsXpo)),
                                                         "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement",
                                                         CriteriaOperator.Parse($"Status = {status} And LegalBookId.Id = {bookId} And HandlesThirdParty = {HandlesThirdParty}"))
            serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function


    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMainAccountsByStatusAndBookId(status As Boolean, bookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountsXpo)),
                                                         "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement",
                                                         CriteriaOperator.Parse($"Status = {status} And LegalBookId.Id = {bookId}"))
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

#Region "ListMainAccountsLastLevel"

    ''' <summary>
    ''' Lista de parametros, (0) estado, (1) id de legalbook
    ''' </summary>
    ''' <remarks></remarks>
    Private _parameters As Object()
    ' ''' <summary>
    ' ''' Objeto de consulta
    ' ''' </summary>
    'Private WithEvents linqListCategory As LinqInstantFeedbackSource
    ''' <summary>
    ''' Lista de cuentas activas de un libro y de ultimo nivel
    ''' </summary>
    Public Function ListMainAccountsLastLevel(parameters As Object()) As LinqInstantFeedbackSource
        Dim linqListMainAccountsLastLevel As New LinqInstantFeedbackSource
        AddHandler linqListMainAccountsLastLevel.GetQueryable, AddressOf linqListMainAccountsLastLevel_GetQueryable
        linqListMainAccountsLastLevel.KeyExpression = "Id"
        _parameters = parameters
        Return linqListMainAccountsLastLevel
    End Function
    ''' <summary>
    ''' Ejecuta y obtiene un conjunto de datos
    ''' </summary>
    Private Sub linqListMainAccountsLastLevel_GetQueryable(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableMainAccount As New XPQuery(Of MainAccountsXpo)(session)
            Dim TmpQueryableSource = From c In tableMainAccount.Where(Function(m) m.Status = CBool(_parameters(0)) And m.LegalBookId.Id = CInt(_parameters(1)))
                                     Group Join c1 In tableMainAccount.Where(Function(m) m.Status = CBool(_parameters(0)) And m.LegalBookId.Id = CInt(_parameters(1)))
                                     On c1.IdParent.Id Equals c.Id Into temp = Group
                                     From t In temp.DefaultIfEmpty()
                                     Where t Is Nothing
                                     Select c

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableMainAccount
            'End Using
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Sub

#End Region


    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <param name="journalVourcherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherDetailsByJournalVoucherId(journalVourcherId As Integer) As XPCollection(Of JournalVoucherDetailsXpo)
        Dim session As New IndigoXPOSession(Of JournalVoucherDetailsXpo)()
        Return New XPCollection(Of JournalVoucherDetailsXpo)(session, CriteriaOperator.Parse($"IdAccounting.Id= {journalVourcherId}"))
        'End Using
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable cuando se hizo por medio de importacion de archivo de excel
    ''' </summary>
    ''' <param name="journalVourcherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherDetailsImportedByJournalVoucherId(journalVourcherId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of JournalVoucherDetailsXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(JournalVoucherDetailsXpo)),
                                                         "Id;IdAccounting.Id;IdMainAccount;IdMainAccount.NumberName;IdThirdParty;IdThirdParty.NitName;IdCostCenter;IdCostCenter.CodeName;DebitValue;CreditValue",
                                                         CriteriaOperator.Parse($"IdAccounting.Id= {journalVourcherId}"))
    End Function
    ''' <summary>
    ''' Lista todas las cuentas por level
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevelReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountsXpo)),
                                                         "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement",
                                                         CriteriaOperator.Parse("AllowsMovement = 1"))
            serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por level y que manejen terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevelHandlesThirdReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountsXpo)),
                                                         "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement",
                                                         CriteriaOperator.Parse("AllowsMovement = 1 And HandlesThirdParty = 1"))
            serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por level y que sean de patrimonio
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevelPatrimonial() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountsXpo)),
                                                         "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement",
                                                         CriteriaOperator.Parse("AllowsMovement = 1 And IdAccountClass.Patrimony = 1"))
            serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsForSearch(LegalBookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of MainAccountsXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountsXpo)),
                                                         "Id;Number;Name;LegalBookId",
                                                         CriteriaOperator.Parse($"LegalBookId={LegalBookId}"))
            serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTypeVoucherRepor() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentTypeXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentTypeXpo)),
                                                         "Id;Code;Name;CodeName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of JournalVoucherXpo)()
        Dim serverMode = New XPInstantFeedbackSource(session.GetClassInfo(GetType(JournalVoucherXpo)),
                                                         "Consecutive;VoucherDate;Detail;IdJournalVoucher.Name", Nothing)
            serverMode.DefaultSorting = "Consecutive"
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista los comprobantes con homologaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ListJournalVourcherHomologations(AccountingMovementId As Integer, journalVoucherId As Integer) As XPCollection(Of JournalVouchersXpo)
        Dim session As New IndigoXPOSession(Of JournalVouchersXpo)()
        Return New XPCollection(Of JournalVouchersXpo)(session, CriteriaOperator.Parse($"AccountingMovementId={AccountingMovementId} And Id <> {journalVoucherId}"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista los comprobantes con homologaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ListJournalVourcherMassiveConfirm() As XPCollection(Of JournalVouchersMassiveConfirmXpo)
        Dim session As New IndigoXPOSession(Of JournalVouchersMassiveConfirmXpo)()
        Return New XPCollection(Of JournalVouchersMassiveConfirmXpo)(session, CriteriaOperator.Parse("Status=1"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccounts(LegalBookId As Integer) As XPServerCollectionSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Dim unidad As New UnitOfWork()
        unidad.AutoCreateOption = DB.AutoCreateOption.SchemaAlreadyExists
        unidad.ConnectionString = session.ConnectionString
        Return New XPServerCollectionSource(unidad, GetType(PUCServiceXpo),
                                                                     CriteriaOperator.Parse($"LegalBookId.Id={LegalBookId}"))
        '    End Using
        'End Using
    End Function

    ''' <summary>
    ''' lista las cuentas contables que tengan asociado el libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewHomologationAccountByLegalBookId(LegalBookId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewHomologationAccountXpo)()
        Return New XPCollection(session, GetType(ViewHomologationAccountXpo),
                                                           CriteriaOperator.Parse($"LegalBookId={LegalBookId}"))
        'End Using
    End Function

	''' <summary>
	''' lista las cuentas contables que tenga el libro oficial y el libro de homologacion
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId As Integer, HomologationLegalBookId As Integer) As XPCollection
		Dim session As New IndigoXPOSession(Of ViewHomologationAccountXpo)()
		Return New XPCollection(session, GetType(ViewHomologationAccountXpo),
														   CriteriaOperator.Parse($"LegalBookId={LegalBookId}"))
		'End Using
	End Function

	''' <summary>
	''' lista todas las definiciones de tarifa
	''' </summary>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function ListAccountsByLegalBookAndAllowMovementAndStatus(LegalBookId As Integer, AllowMovement As Boolean, Status As Boolean) As XPCollection
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPCollection(session, GetType(PUCServiceXpo),
                                    CriteriaOperator.Parse($"LegalBookId.Id={LegalBookId} And Status={Status} And AllowsMovement={AllowMovement}"))
        'End Using
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountsByLegalBookAndAllowMovementAndStatusForSearch(LegalBookId As Integer, AllowMovement As Boolean, Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)), Nothing,
                                                         CriteriaOperator.Parse($"LegalBookId.Id={LegalBookId} And Status={Status} And AllowsMovement={AllowMovement}"))
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountsByLegalBookId(LegalBookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)), Nothing,
                                                         CriteriaOperator.Parse($"LegalBookId.Id={LegalBookId}"))
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountById(Id As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPCollection(session, GetType(PUCServiceXpo), CriteriaOperator.Parse($"Id={Id}"))
        'End Using
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsCostCenter(handlesCostCenter As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)), "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel;AllowsMovement",
                                                         CriteriaOperator.Parse($"AllowsMovement=1 And Status=1 And HandlesCostCenter={handlesCostCenter}"))
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevel(ByVal level As Integer, ByVal Status As Boolean,
                                        Optional legalBookId As Integer = 0, Optional Ids As String = Nothing,
                                        Optional ClassType As Byte? = Nothing) As XPInstantFeedbackSource
        Dim filter = $"AllowsMovement=1 AND Status={Status}"

        If legalBookId = 0 Then
            filter &= " AND LegalBookId.OfficialBook=1"
        Else
            filter &= $" AND LegalBookId.Id={legalBookId}"
        End If

        If Not String.IsNullOrEmpty(Ids) Then
            filter &= $" AND Id IN ({Ids})"
        End If

        If ClassType IsNot Nothing Then
            filter &= $"AND IdAccountClass.Type={ClassType}"
        End If

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)), "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;RetencionTypeName;NumberName;IdAccountLevel", criteria)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByClass(Optional _class As Integer = 0, Optional legalBookId As Integer = 0) As XPInstantFeedbackSource
        Dim filter = $"AllowsMovement=1 AND Status=1"

        If legalBookId = 0 Then
            filter &= " AND LegalBookId.OfficialBook=1"
        Else
            filter &= $" AND LegalBookId.Id={legalBookId}"
        End If

        If _class > 0 Then
            filter &= $" AND IdAccountClass.Code = {_class}"
        End If

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)), "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;RetencionTypeName;NumberName;IdAccountLevel", criteria)
    End Function

    ''' <summary>
    ''' Lista las cuentas contables dependiendo del control presupuestal
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountByBudgetControl(Status As Boolean, BudgetControl As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Dim criteria As CriteriaOperator
            Select Case BudgetControl
                Case 1 'Ninguno
                    criteria = CriteriaOperator.Parse($"AllowsMovement=1 And Status={Status} And LegalBookId.OfficialBook=1")
                Case 2 'Tercero
                    criteria = CriteriaOperator.Parse($"AllowsMovement=1 And Status={Status} And LegalBookId.OfficialBook=1 And HandlesThirdParty=1")
                Case 3 'Centro costo
                    criteria = CriteriaOperator.Parse($"AllowsMovement=1 And Status={Status} And LegalBookId.OfficialBook=1 And HandlesCostCenter=1")
                Case 4, 5 '4.Centro costo/Tercero ó 5.Tercero/Centro costo
                    criteria = CriteriaOperator.Parse($"AllowsMovement=1 And Status={Status} And LegalBookId.OfficialBook=1 And HandlesThirdParty=1 And HandlesCostCenter=1")
                Case Else
                    criteria = CriteriaOperator.Parse($"AllowsMovement=1 And Status={Status} And LegalBookId.OfficialBook=1")
            End Select
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)),
                                               "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel",
                                               criteria)
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByRetentionAndFreelancerCategory(ByVal TypeRetention As Integer, ByVal FreelancerCategory As Boolean, ByVal Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)),
                                                         "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel",
                                                         CriteriaOperator.Parse($"RetencionType={TypeRetention} And FreelancerCategory={FreelancerCategory} And Status={Status} And LegalBookId.OfficialBook = 1"))
    End Function

    ''' <summary>
    ''' Lista toda las cuentas contable si se necesitan solo con retencion o todas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByRetention(ByVal retention As Boolean, ByVal Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Dim criteria As CriteriaOperator
            If retention = True Then
            criteria = CriteriaOperator.Parse($"AllowsMovement= 1  And  Status={Status} And LegalBookId.OfficialBook = True")
        Else
            criteria = CriteriaOperator.Parse($"AllowsMovement= 1 And HandlesCostCenter= False And RetencionType = 0 And Status={Status} And LegalBookId.OfficialBook = True")
        End If
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)),
                                               "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel;AllowsMovement",
                                               criteria)
    End Function

    ''' <summary>
    ''' Lista las cuentas contables de tipo resultado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByClassResult() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PUCServiceXpo)()
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse($"AllowsMovement = 1 And IdAccountClass.Type <=2 And Status= 1 And LegalBookId.OfficialBook = True And IdAccountLevel.Level >= 5")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(PUCServiceXpo)),
                                               "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel;AllowsMovement",
                                               criteria)
    End Function


    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListRetentionConceptByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RetentionConceptXpo)()
        Dim strCriteria = String.Format("Status = {0}", state)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(RetentionConceptXpo)), "Id;Code;Name;CodeName;MinBase;Rate", criteria)
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListJournalVoucherByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentTypeXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentTypeXpo)), "Id;Code;Name;Consecutive;CodeName",
                                                         CriteriaOperator.Parse($"Status={state}"))
    End Function

    ''' <summary>
    ''' funcion para consultar todos los comprabantes contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourchers(legalBookId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewJournalVouchersXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewJournalVouchersXpo)),
                                                         "Id;Consecutive;JournalVoucherTypeName;VoucherDate;Detail;StatusName;EntityName;EntityCode;Value",
                                                         CriteriaOperator.Parse($"LegalBookId={legalBookId}"))
    End Function

    ''' <summary>
    ''' funcion para consultar todos los comprabantes contables Asociado a una cuenta contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function JournalVouchersByMainAccount(IdMainAccount As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewJournalVouchersByMainAccountXpo)()
        Dim Criteria As CriteriaOperator = CriteriaOperator.Parse($"LegalBookId=1 AND IdMainAccount ={IdMainAccount}")
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewJournalVouchersByMainAccountXpo)),
                                                              "Id;JournalVouchersId;Consecutive;JournalVoucherTypeName;VoucherDate;Detail;StatusName;EntityName;EntityCode;Value;IdMainAccount", Criteria)
    End Function

    ''' <summary>
    ''' funcion para consultar todos los comprabantes contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourchersByLegalBookAndJournalVoucherType(legalBookId As Integer, journalVoucherTypeId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of JournalVouchersXpo)()
        Dim strCriteria = String.Format("LegalBookId.Id = {0} And IdJournalVoucher.Id = {1}", legalBookId, journalVoucherTypeId)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(strCriteria)
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(JournalVouchersXpo)), Nothing, criteria)
    End Function

    ''' <summary>
    ''' Listad todos los meses 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMonth(ByVal Year As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ClosedMonthXpo)()
        Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(ClosedMonthXpo)),
                                                         "Id;Year;Month;Status",
                                                         CriteriaOperator.Parse($"Year={Year}"))
    End Function

    ''' <summary>
    ''' Lista todas las unidades operativas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListOperatingUnitByIdd(ByVal operatingUnitId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of CommonOperatingUnitReportXpo)()
        Return New XPCollection(session, GetType(CommonOperatingUnitReportXpo), CriteriaOperator.Parse($"Id = {operatingUnitId}"))
    End Function

    ''' <summary>
    ''' Lista todas los formatos de exógena
    ''' </summary>
    Public Function ListExogenousFormat() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExogenousFormatXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(ExogenousFormatXpo)),
                                               "Id;Code;Format;Version;Status;StatusName",
                                               Nothing)
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los formatos de SuperSalud
    ''' </summary>
    Public Function ListHealthSuperParameters() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HealthSuperParametersXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(HealthSuperParametersXpo)), "Id;Code;Format;Status;StatusName;FormatName", Nothing)
        Return xp
    End Function

    Public Function ListCollectionExogenousFormat(ByVal filtro As String) As XPCollection(Of ExogenousFormatXpo)
        Dim session As New IndigoXPOSession(Of ExogenousFormatXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of ExogenousFormatXpo)(session, criteria)
    End Function

    Public Function ListMainAccountRestriction(ByVal MainAccountId As Integer) As XPCollection(Of MainAccountRestrictionsXpo)
		Dim session As New IndigoXPOSession(Of MainAccountRestrictionsXpo)()
		Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"MainAccountId = {MainAccountId}")
        Return New XPCollection(Of MainAccountRestrictionsXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista todas los centros de costo asociado a la cuenta contable
    ''' </summary>
    Public Function ListMainAccountRestrictionBank(ByVal MainAccountId As Integer) As XPInstantFeedbackSource
		Dim session As New IndigoXPOSession(Of MainAccountRestrictionsXpo)()
		Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"MainAccountId = {MainAccountId} And CostCenterId Is Not Null")
		Return New XPInstantFeedbackSource(session.GetClassInfo(GetType(MainAccountRestrictionsXpo)), Nothing, criteria)
	End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListEntityNames() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewEntityNameDescriptionsXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(ViewEntityNameDescriptionsXpo)),
                                               "EntityName;Description",
                                               Nothing)
        Return xp
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListJournalVoucherTypes() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of DocumentTypeXpo)()
        Dim xp = New XPInstantFeedbackSource(session.GetClassInfo(GetType(DocumentTypeXpo)),
                                               "Id;Code;Name",
                                               Nothing)
        Return xp
    End Function

#End Region

#Region "MainAccountsHandlesCostCenterAndRetention"

    Private _retentionType As Integer
    Private _status As Boolean
    Private _handlesCostCenter As Boolean
    Private _level As Integer

    Public Function ListMainAccountsHandlesCostCenterAndRetention(level As Integer, retentionType As Integer, handlesCostCenter As Boolean, status As Boolean)
        Dim vlinqMainAccountsHandlesCostCenterAndRetention As New LinqInstantFeedbackSource
        AddHandler vlinqMainAccountsHandlesCostCenterAndRetention.GetQueryable, AddressOf OnGetQueryableCashReceiptsByRetentionType
        AddHandler vlinqMainAccountsHandlesCostCenterAndRetention.DismissQueryable, AddressOf DismissQueryableCashReceiptsByRetentionType
        _level = level
        _retentionType = retentionType
        _status = status
        _handlesCostCenter = handlesCostCenter
        Return vlinqMainAccountsHandlesCostCenterAndRetention
    End Function

    Private Sub OnGetQueryableCashReceiptsByRetentionType(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(session)
            e.QueryableSource = From ma In tableMainAccount
                                Where ma.AllowsMovement = 1 And ma.RetencionType = _retentionType _
                                    And ma.Status = _status And ma.HandlesCostCenter = _handlesCostCenter
                                Select ma
            e.Tag = tableMainAccount
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceiptsByRetentionType(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "Informes"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        Return result
    End Function

#Region "Informe Por Cuenta - Tercero y Tercero - Cuenta"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionAccountThird(ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, INDBookId As Integer) As XPCollection(Of JournalVoucherDetailsXpo)
        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= $"GetDate(IdAccounting.VoucherDate) >= #{Format(INDFechaIni, "yyyy-MM-dd")}# And GetDate(IdAccounting.VoucherDate) <= #{Format(INDFechaEnd, "yyyy-MM-dd")}#  And IdAccounting.LegalBookId.Id = {INDBookId} And IdAccounting.IsClosedYear In (0, 1)"
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= $"And IdThirdParty.Nit >= '{INDThirdPartyStart}' AND IdThirdParty.Nit <= '{INDThirdPartyEnd}'"
        End If
        'filtro por cuentas
        If INDAccountIni IsNot Nothing And INDAccountEnd IsNot Nothing Then
            criteria &= $"AND IdMainAccount.Number >= '{INDAccountIni}' AND IdMainAccount.Number <= '{INDAccountEnd}'"
        End If
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= $"AND IdCostCenter.Code >= '{INDCostCenterStart}' AND IdCostCenter.Code <= '{INDCostCenterEnd}'"
        End If
        'filtra por estado
        If INDStatus <> 4 Then
            criteria &= $"AND IdAccounting.Status = {INDStatus}"
        End If
        Dim INDMes As Integer
        Dim INDAno As Integer
        Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDFechaIni)
        Dim resulXpoCollection As XPCollection(Of JournalVoucherDetailsXpo) = Nothing
        Dim session0 As New Session(XpoDefault.DataLayer)
        resulXpoCollection = New XPCollection(Of JournalVoucherDetailsXpo)(session0, CriteriaOperator.Parse(criteria))
        Dim sorting As New SortingCollection()
        sorting.Add(New SortProperty("IdMainAccount.Id", DevExpress.Xpo.DB.SortingDirection.Ascending))
        sorting.Add(New SortProperty("IdThirdParty.Id", DevExpress.Xpo.DB.SortingDirection.Ascending))
        sorting.Add(New SortProperty("IdAccounting.Consecutive", DevExpress.Xpo.DB.SortingDirection.Ascending))
        resulXpoCollection.Sorting = sorting
        INDMes = Month(INDFechaMesAnte)
        INDAno = Year(INDFechaMesAnte)
        'End Using

        Dim resulPlinq As PLinqServerModeSource
        Dim resulxpoPreviousBalance As New List(Of GeneralLedgerBalanceXpo)
        Dim dictionaryPreviousBalance As New Dictionary(Of String, String)
        Dim session As New Session(XpoDefault.DataLayer)
        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralBalanceAccountThirdMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            For Each itemPlinq In resulPlinq.Source
                Dim ValueId As Integer
                If resulxpoPreviousBalance.Count < 1 Then
                    ValueId = 1
                Else
                    ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                End If
                Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                Dim thirdParty As New CommonThirdPartyXpo(session)
                thirdParty.Id = itemPlinq.IdTercero
                Dim mainAccount As New MainAccountsXpo(session)
                mainAccount.Id = itemPlinq.IdCuenta
                ResultGeneralBalance.Id = ValueId
                ResultGeneralBalance.IdMainAccount = mainAccount
                ResultGeneralBalance.IdThirdParty = thirdParty
                ResultGeneralBalance.SaldoTercero = 0
                ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                ResultGeneralBalance.SaldoAnterior = 0
                ResultGeneralBalance.NuevoSaldo = 0
                ResultGeneralBalance.NuevoSaldoTercero = 0
                resulxpoPreviousBalance.Add(ResultGeneralBalance)

                dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero, itemPlinq.IdCuenta)
            Next
        Else
            resulPlinq = Me.ListGetGeneralBalanceAccountThirdMonth13(14, INDAno - 1, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceAccountThird(INDMes, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim thirdParty As New CommonThirdPartyXpo(session)
                    thirdParty.Id = itemPlinq3.IdTercero
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq3.IdCuenta
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdTercero, itemPlinq3.IdCuenta)
                Next
            Else
                'recorremos el primer listado
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta And e.IdTercero = itemPlinq3.IdTercero Select e).ToList
                    If listFilter3.Count > 0 Then
                        For Each itemPlinq In listFilter3
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = itemPlinq.IdTercero
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero, itemPlinq.IdCuenta)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq3.IdTercero
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq3.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdTercero, itemPlinq3.IdCuenta)
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalance.ContainsKey(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq.IdTercero
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero, itemPlinq.IdCuenta)
                    End If
                Next
            End If
        End If

        Dim INDDia As Integer

        INDDia = Day(INDFechaIni)
        Dim dictionaryPreviousBalanceMovement As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorAccountThird(INDFechaInicio, INDFechaFin, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim thirdParty As New CommonThirdPartyXpo(session)
                    thirdParty.Id = itemPlinq2.IdTercero
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq2.IdCuenta
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdCuenta & " - " & itemPlinq2.IdTercero, itemPlinq2.IdCuenta)
                Next
            Else

                'recorremos el primer listado
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq2.IdCuenta And e.IdTercero = itemPlinq2.IdTercero Select e).ToList
                    If listFilter2.Count > 0 Then
                        For Each itemPlinq In listFilter2
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = itemPlinq.IdTercero
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalanceMovement.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero, itemPlinq.IdCuenta)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq2.IdTercero
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq2.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdCuenta & " - " & itemPlinq2.IdTercero, itemPlinq2.IdCuenta)
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalanceMovement.ContainsKey(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq.IdTercero
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalanceMovement.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero, itemPlinq.IdCuenta)
                    End If
                Next
            End If
        End If
        'End Using
        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpoCollection 'resulXpoCollection.OrderBy(Function(x) x.IdMainAccount.Id).ThenBy(Function(x) If(IsNothing(x.IdThirdParty) = True, 0, x.IdThirdParty.Id)).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulxpoPreviousBalance Where If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Id) = If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) And e.IdMainAccount.Id = itemXpo.IdMainAccount.Id Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If itemXpo.IdAccounting.Status = "Confirmado" Then
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
                Else
                    OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                End If
            Else
                OperacionXpo = 0
            End If

            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.DebitValue - item.CreditValue)
                Else
                    OperacionLinq = (item.CreditValue - item.DebitValue)
                End If
                ' añadimos el saldo anterior y nuevo saldo del segundo filtro en este caso el tercero
                If dictionaryBalance.ContainsKey(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id)) Then
                    dictionaryBalance(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id)) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id))
                    itemXpo.SaldoAnteriorPrimerFiltro = 0
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                Else
                    dictionaryBalance.Add(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id), OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id))
                    itemXpo.SaldoAnteriorPrimerFiltro = OperacionLinq
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                itemXpo.SaldoCuenta = 0
                itemXpo.SaldoAnteriorPrimerFiltro = 0
                itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id)) Then
                    dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id)) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id))
                Else
                    dictionaryBalance.Add(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id), OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
            End If
        Next
        Return resulXpoCollection
    End Function

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountThird(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource
        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource
        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            b.Source = (From T1 In tableGeneralBalance
                        Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                        Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                        Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                        Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                        Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                        Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}).ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using
    End Function

#End Region

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountThirdMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource
        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using
    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetSaldoAnteriorAccountThird(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(session)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Informe Por Cuenta - Centro de Costo y Centro de Costo - Cuenta"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionAccountCostCenter(ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As List(Of JournalVoucherDetailsXpo)
        Dim resulXpo As IList(Of JournalVoucherDetailsXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(IdAccounting.VoucherDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(IdAccounting.VoucherDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND IdAccounting.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountIni IsNot Nothing And INDAccountEnd IsNot Nothing Then
            criteria &= "AND IdMainAccount.Number >= '" & INDAccountIni & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "'"
        End If
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If
        'filtra por estado
        If INDStatus <> 4 Then
            criteria &= "AND IdAccounting.Status = " & INDStatus
        End If

        resulXpo = Me.LoadCollection(Of JournalVoucherDetailsXpo)(XpoDefault.DataLayer, Nothing, criteria)
        Dim INDMes As Integer
        Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDFechaIni)
        INDMes = Month(INDFechaMesAnte)
        Dim INDAno As Integer
        INDAno = Year(INDFechaMesAnte)

        Dim session As New Session(XpoDefault.DataLayer)
        Dim resulxpoPreviousBalance As New List(Of GeneralLedgerBalanceXpo)
        Dim dictionaryPreviousBalance As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then

            resulPlinq = Me.ListGetGeneralBalanceAccountCostCenterMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            For Each itemPlinq In resulPlinq.Source
                Dim ValueId As Integer
                If resulxpoPreviousBalance.Count < 1 Then
                    ValueId = 1
                Else
                    ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                End If
                Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                Dim costCenter As New CostCenterXpo(session)
                costCenter.Id = itemPlinq.IdCostCenter
                Dim mainAccount As New MainAccountsXpo(session)
                mainAccount.Id = itemPlinq.IdCuenta
                ResultGeneralBalance.Id = ValueId
                ResultGeneralBalance.IdMainAccount = mainAccount
                ResultGeneralBalance.IdCostCenter = costCenter
                ResultGeneralBalance.SaldoTercero = 0
                ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                ResultGeneralBalance.SaldoAnterior = 0
                ResultGeneralBalance.NuevoSaldo = 0
                ResultGeneralBalance.NuevoSaldoTercero = 0
                resulxpoPreviousBalance.Add(ResultGeneralBalance)

                dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
            Next
        Else
            resulPlinq = Me.ListGetGeneralBalanceAccountCostCenterMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceAccountCostCenter(INDMes, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim costCenter As New CostCenterXpo(session)
                    costCenter.Id = itemPlinq3.IdCostCenter
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq3.IdCuenta
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.IdCostCenter = costCenter
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdCostCenter, itemPlinq3.IdCuenta)
                Next
            Else
                'recorremos el primer listado
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta And e.IdCostCenter = itemPlinq3.IdCostCenter Select e).ToList
                    If listFilter3.Count > 0 Then
                        For Each itemPlinq In listFilter3
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim costCenter As New CostCenterXpo(session)
                            costCenter.Id = itemPlinq.IdCostCenter
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdCostCenter = costCenter
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq3.IdCostCenter
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq3.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdCostCenter, itemPlinq3.IdCuenta)
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalance.ContainsKey(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq.IdCostCenter
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
                    End If
                Next
            End If
        End If
        Dim INDDia As Integer

        INDDia = Day(INDFechaIni)
        Dim dictionaryPreviousBalanceMovement As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorAccountCostCenter(INDFechaInicio, INDFechaFin, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim costCenter As New CostCenterXpo(session)
                    costCenter.Id = itemPlinq2.IdCostCenter
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq2.IdCuenta
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.IdCostCenter = costCenter
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdCuenta & " - " & itemPlinq2.IdCostCenter, itemPlinq2.IdCuenta)
                Next
            Else

                'recorremos el primer listado
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq2.IdCuenta And e.IdCostCenter = itemPlinq2.IdCostCenter Select e).ToList
                    If listFilter2.Count > 0 Then
                        For Each itemPlinq In listFilter2
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim costCenter As New CostCenterXpo(session)
                            costCenter.Id = itemPlinq.IdCostCenter
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdCostCenter = costCenter
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalanceMovement.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq2.IdCostCenter
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq2.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdCuenta & " - " & itemPlinq2.IdCostCenter, itemPlinq2.IdCuenta)
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalanceMovement.ContainsKey(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq.IdCostCenter
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalanceMovement.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
                    End If
                Next
            End If
        End If
        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id).ThenBy(Function(x) If(IsNothing(x.IdCostCenter) = True, 0, x.IdCostCenter.Id)).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulxpoPreviousBalance Where If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Id) = If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id) And e.IdMainAccount.Id = itemXpo.IdMainAccount.Id Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If itemXpo.IdAccounting.Status = "Confirmado" Then
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
                Else
                    OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                End If
            Else
                OperacionXpo = 0
            End If
            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.DebitValue - item.CreditValue)
                Else
                    OperacionLinq = (item.CreditValue - item.DebitValue)
                End If
                ' añadimos el saldo anterior y nuevo saldo del segundo filtro
                If dictionaryBalance.ContainsKey(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id)) Then
                    dictionaryBalance(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id)) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id))
                    itemXpo.SaldoAnteriorPrimerFiltro = 0
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                Else
                    dictionaryBalance.Add(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id), OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdMainAccount.Id & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id))
                    itemXpo.SaldoAnteriorPrimerFiltro = OperacionLinq
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                itemXpo.SaldoCuenta = 0
                itemXpo.SaldoAnteriorPrimerFiltro = 0
                itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) Then
                    dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id))
                Else
                    dictionaryBalance.Add(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id), OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
            End If
        Next
        Return resulXpo
        'End Using
    End Function

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountCostCenter(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource
        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using
    End Function

#End Region

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta del mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountCostCenterMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetSaldoAnteriorAccountCostCenter(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(session)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Informe Por tercero - Centro de Costo y Centro de Costo - tercero"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionThirdPartyCostCenter(ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As List(Of JournalVoucherDetailsXpo)
        Dim resulXpo As IList(Of JournalVoucherDetailsXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(IdAccounting.VoucherDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(IdAccounting.VoucherDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND IdAccounting.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountIni IsNot Nothing And INDAccountEnd IsNot Nothing Then
            criteria &= "AND IdMainAccount.Number >= '" & INDAccountIni & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "'"
        End If
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If
        'filtra por estado
        If INDStatus <> 4 Then
            criteria &= "AND IdAccounting.Status = " & INDStatus
        End If

        resulXpo = Me.LoadCollection(Of JournalVoucherDetailsXpo)(XpoDefault.DataLayer, Nothing, criteria)
        Dim INDMes As Integer
        Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDFechaIni)
        INDMes = Month(INDFechaMesAnte)
        Dim INDAno As Integer
        INDAno = Year(INDFechaMesAnte)

        Dim session As New Session(XpoDefault.DataLayer)
        Dim resulxpoPreviousBalance As New List(Of GeneralLedgerBalanceXpo)
        Dim dictionaryPreviousBalance As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then

            resulPlinq = Me.ListGetGeneralBalanceThirdPartyCostCenterMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            For Each itemPlinq In resulPlinq.Source
                Dim ValueId As Integer
                If resulxpoPreviousBalance.Count < 1 Then
                    ValueId = 1
                Else
                    ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                End If
                Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                Dim costCenter As New CostCenterXpo(session)
                costCenter.Id = itemPlinq.IdCostCenter
                Dim thirdParty As New CommonThirdPartyXpo(session)
                thirdParty.Id = itemPlinq.IdThirdParty
                ResultGeneralBalance.Id = ValueId
                ResultGeneralBalance.IdThirdParty = thirdParty
                ResultGeneralBalance.IdCostCenter = costCenter
                ResultGeneralBalance.SaldoTercero = 0
                ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                ResultGeneralBalance.SaldoAnterior = 0
                ResultGeneralBalance.NuevoSaldo = 0
                ResultGeneralBalance.NuevoSaldoTercero = 0
                resulxpoPreviousBalance.Add(ResultGeneralBalance)

                dictionaryPreviousBalance.Add(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter, itemPlinq.IdThirdParty)
            Next
        Else
            resulPlinq = Me.ListGetGeneralBalanceThirdPartyCostCenterMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceThirdPartyCostCenter(INDMes, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim costCenter As New CostCenterXpo(session)
                    costCenter.Id = itemPlinq3.IdCostCenter
                    Dim thirdParty As New CommonThirdPartyXpo(session)
                    thirdParty.Id = itemPlinq3.IdThirdParty
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.IdCostCenter = costCenter
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalance.Add(itemPlinq3.IdThirdParty & " - " & itemPlinq3.IdCostCenter, itemPlinq3.IdThirdParty)
                Next
            Else
                'recorremos el primer listado
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdThirdParty = itemPlinq3.IdThirdParty And e.IdCostCenter = itemPlinq3.IdCostCenter Select e).ToList
                    If listFilter3.Count > 0 Then
                        For Each itemPlinq In listFilter3
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim costCenter As New CostCenterXpo(session)
                            costCenter.Id = itemPlinq.IdCostCenter
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = itemPlinq.IdThirdParty
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.IdCostCenter = costCenter
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalance.Add(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter, itemPlinq.IdThirdParty)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq3.IdCostCenter
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq3.IdThirdParty
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalance.Add(itemPlinq3.IdThirdParty & " - " & itemPlinq3.IdCostCenter, itemPlinq3.IdThirdParty)
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalance.ContainsKey(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq.IdCostCenter
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq.IdThirdParty
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalance.Add(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter, itemPlinq.IdThirdParty)
                    End If
                Next
            End If
        End If
        Dim INDDia As Integer

        INDDia = Day(INDFechaIni)
        Dim dictionaryPreviousBalanceMovement As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorThirdPartyCostCenter(INDFechaInicio, INDFechaFin, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim costCenter As New CostCenterXpo(session)
                    costCenter.Id = itemPlinq2.IdCostCenter
                    Dim thirdParty As New CommonThirdPartyXpo(session)
                    thirdParty.Id = itemPlinq2.IdThirdParty
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.IdCostCenter = costCenter
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdThirdParty & " - " & itemPlinq2.IdCostCenter, itemPlinq2.IdThirdParty)
                Next
            Else

                'recorremos el primer listado
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdThirdParty = itemPlinq2.IdThirdParty And e.IdCostCenter = itemPlinq2.IdCostCenter Select e).ToList
                    If listFilter2.Count > 0 Then
                        For Each itemPlinq In listFilter2
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim costCenter As New CostCenterXpo(session)
                            costCenter.Id = itemPlinq.IdCostCenter
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = itemPlinq.IdThirdParty
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.IdCostCenter = costCenter
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalanceMovement.Add(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter, itemPlinq.IdThirdParty)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq2.IdCostCenter
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq2.IdThirdParty
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdThirdParty & " - " & itemPlinq2.IdCostCenter, itemPlinq2.IdThirdParty)
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalanceMovement.ContainsKey(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq.IdCostCenter
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq.IdThirdParty
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.IdCostCenter = costCenter
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalanceMovement.Add(itemPlinq.IdThirdParty & " - " & itemPlinq.IdCostCenter, itemPlinq.IdThirdParty)
                    End If
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) If(IsNothing(x.IdThirdParty) = True, 0, x.IdThirdParty.Id)).ThenBy(Function(x) If(IsNothing(x.IdCostCenter) = True, 0, x.IdCostCenter.Id)).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulxpoPreviousBalance Where If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Id) = If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id) And If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Id) = If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If
            Dim OperacionXpo As Long
            If itemXpo.IdAccounting.Status = "Confirmado" Then
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
                Else
                    OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                End If
            Else
                OperacionXpo = 0
            End If

            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.DebitValue - item.CreditValue)
                Else
                    OperacionLinq = (item.CreditValue - item.DebitValue)
                End If
                If dictionaryBalance.ContainsKey(If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id) & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id)) Then
                    dictionaryBalance(If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id) & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id)) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id) & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id))
                    itemXpo.SaldoAnteriorPrimerFiltro = 0
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                Else
                    dictionaryBalance.Add(If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id) & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id), OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(If(IsNothing(item.IdThirdParty) = True, 0, item.IdThirdParty.Id) & "-" & If(IsNothing(item.IdCostCenter) = True, 0, item.IdCostCenter.Id))
                    itemXpo.SaldoAnteriorPrimerFiltro = OperacionLinq
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                itemXpo.SaldoCuenta = 0
                itemXpo.SaldoAnteriorPrimerFiltro = 0
                itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                If dictionaryBalance.ContainsKey(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) Then
                    dictionaryBalance(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id))
                Else
                    dictionaryBalance.Add(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id), OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
            End If
        Next
        Return resulXpo
        'End Using

    End Function

#Region "Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero"

    ''' <summary>
    ''' Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero
    ''' </summary>
    Public Function ListGetGeneralBalanceThirdPartyCostCenter(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero
    ''' </summary>
    Public Function ListGetGeneralBalanceThirdPartyCostCenterMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                     Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId And T2.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T2.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe tercero - Centro de Costo y Centro de Costo - tercero"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe tercero - Centro de Costo y Centro de Costo - tercero
    ''' </summary>
    Public Function ListGetSaldoAnteriorThirdPartyCostCenter(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(session)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join T3 In tableMainAccount On T3.Id Equals T1.IdMainAccount.Id
                                     Join T4 In tableMainAccountClass On T4.Id Equals T3.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And T3.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T3.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T4.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join T3 In tableMainAccount On T3.Id Equals T1.IdMainAccount.Id
                                     Join T4 In tableMainAccountClass On T4.Id Equals T3.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And T3.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T3.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T4.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join T3 In tableMainAccount On T3.Id Equals T1.IdMainAccount.Id
                                     Join T4 In tableMainAccountClass On T4.Id Equals T3.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And T3.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T3.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T4.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join T3 In tableMainAccount On T3.Id Equals T1.IdMainAccount.Id
                                     Join T4 In tableMainAccountClass On T4.Id Equals T3.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And T3.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And T3.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdCostCenter, T1.IdThirdParty, T4.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Informe Por Cuenta - Fecha"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionAccountingDate(ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDBookId As Integer) As List(Of JournalVoucherDetailsXpo)
        Dim resulXpo As IList(Of JournalVoucherDetailsXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(IdAccounting.VoucherDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(IdAccounting.VoucherDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND IdAccounting.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountIni IsNot Nothing And INDAccountEnd IsNot Nothing Then
            criteria &= "AND IdMainAccount.Number >= '" & INDAccountIni & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "'"
        End If
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If
        'filtra por estado
        If INDStatus <> 4 Then
            criteria &= "AND IdAccounting.Status = " & INDStatus
        End If

        resulXpo = Me.LoadCollection(Of JournalVoucherDetailsXpo)(XpoDefault.DataLayer, Nothing, criteria)
        Dim INDMes As Integer
        Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDFechaIni)
        INDMes = Month(INDFechaMesAnte)
        Dim INDAno As Integer
        INDAno = Year(INDFechaMesAnte)

        Dim session As New Session(XpoDefault.DataLayer)
        Dim resulxpoPreviousBalance As New List(Of GeneralLedgerBalanceXpo)
        Dim dictionaryPreviousBalance As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then

            resulPlinq = Me.ListGetGeneralBalanceAccountDateMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            For Each itemPlinq In resulPlinq.Source
                Dim ValueId As Integer
                If resulxpoPreviousBalance.Count < 1 Then
                    ValueId = 1
                Else
                    ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                End If
                Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                Dim mainAccount As New MainAccountsXpo(session)
                mainAccount.Id = itemPlinq.IdCuenta
                ResultGeneralBalance.Id = ValueId
                ResultGeneralBalance.IdMainAccount = mainAccount
                ResultGeneralBalance.SaldoTercero = 0
                ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                ResultGeneralBalance.SaldoAnterior = 0
                ResultGeneralBalance.NuevoSaldo = 0
                ResultGeneralBalance.NuevoSaldoTercero = 0
                resulxpoPreviousBalance.Add(ResultGeneralBalance)

                dictionaryPreviousBalance.Add(itemPlinq.IdCuenta, itemPlinq.IdCuenta)
            Next

        Else

            resulPlinq = Me.ListGetGeneralBalanceAccountDateMonth13(14, INDAno - 1, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceAccountDate(INDMes, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq3.IdCuenta
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta, itemPlinq3.IdCuenta)
                Next
            Else
                'recorremos el primer listado
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta Select e).ToList
                    If listFilter3.Count > 0 Then
                        For Each itemPlinq In listFilter3
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalance.Add(itemPlinq.IdCuenta, itemPlinq.IdCuenta)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq3.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta, itemPlinq3.IdCuenta)

                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalance.ContainsKey(itemPlinq.IdCuenta) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalance.Add(itemPlinq.IdCuenta, itemPlinq.IdCuenta)
                    End If
                Next
            End If
        End If

        Dim INDDia As Integer
        INDDia = Day(INDFechaIni)
        Dim dictionaryPreviousBalanceMovement As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorAccountDate(INDFechaInicio, INDFechaFin, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq2.IdCuenta
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdCuenta, itemPlinq2.IdCuenta)
                Next
            Else

                'recorremos el primer listado
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq2.IdCuenta Select e).ToList
                    If listFilter2.Count > 0 Then
                        For Each itemPlinq In listFilter2
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalanceMovement.Add(itemPlinq.IdCuenta, itemPlinq.IdCuenta)
                        Next
                    Else

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq2.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq2.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq2.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalanceMovement.Add(itemPlinq2.IdCuenta, itemPlinq2.IdCuenta)
                        'End If
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalanceMovement.ContainsKey(itemPlinq.IdCuenta) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalanceMovement.Add(itemPlinq.IdCuenta, itemPlinq.IdCuenta)
                    End If
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemXpo.IdMainAccount.Id Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If itemXpo.IdAccounting.Status = "Confirmado" Then
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
                Else
                    OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                End If
            Else
                OperacionXpo = 0
            End If
            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.DebitValue - item.CreditValue)
                Else
                    OperacionLinq = (item.CreditValue - item.DebitValue)
                End If
                If dictionaryBalance.ContainsKey(item.IdMainAccount.Id) Then
                    dictionaryBalance(item.IdMainAccount.Id) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdMainAccount.Id)
                    itemXpo.SaldoAnteriorPrimerFiltro = 0
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                Else
                    dictionaryBalance.Add(item.IdMainAccount.Id, OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdMainAccount.Id)
                    itemXpo.SaldoAnteriorPrimerFiltro = OperacionLinq
                    itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                itemXpo.SaldoCuenta = 0
                itemXpo.SaldoAnteriorPrimerFiltro = 0
                itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + OperacionXpo
                If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                    dictionaryBalance(itemXpo.IdMainAccount.Id) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(itemXpo.IdMainAccount.Id)
                Else
                    dictionaryBalance.Add(itemXpo.IdMainAccount.Id, OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
            End If
        Next
        Return resulXpo
        'End Using
    End Function

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountDate(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}

            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountDateMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetSaldoAnteriorAccountDate(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(session)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then

            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                     Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using


    End Function

#End Region

#End Region

#Region "Informe Trial Balance"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionTrialBalance(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDAccountingZero As Integer, ByVal INDDetailingThirdParty As Integer, ByVal INDDetailingCostCenter As Integer, ByVal INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim resulXpo As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND Month < 13 AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountIni IsNot Nothing And INDAccountEnd IsNot Nothing Then
            criteria &= "AND IdMainAccount.Number >= '" & INDAccountIni & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "'"
        End If
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If

        Dim session As New Session(XpoDefault.DataLayer)
        'cargar lista de movimientos de las cuentas
        resulXpo = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria, session)

        'si selecciona listar cuentas en 0
        If INDAccountingZero = True Then
            'definir criteria lista cuentas en 0
            Dim criteriaAccount As String = Nothing
            criteriaAccount = "AllowsMovement = 1 And LegalBookId.Id = " & INDBookId
            'filtro por cuentas
            If INDAccountIni <> 0 And INDAccountEnd <> 0 Then
                criteriaAccount &= "AND Number >= '" & INDAccountIni & "' AND Number <= '" & INDAccountEnd & "'"
            End If
            resulXpoAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount, session)

            For Each itemXpoAccount In resulXpoAccount
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.IdMainAccount.Id = itemXpoAccount.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim ValueId As Integer
                    If resulXpo.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulXpo.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(itemXpoAccount.Session)
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = itemXpoAccount
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    ResultGeneralBalance.NuevoSaldoCentroCosto = 0
                    resulXpo.Add(ResultGeneralBalance)
                End If
            Next
        End If

        'cargar los saldos anteriores de las cuentas
        'Dim resulPlinq As PLinqServerModeSource
        'resulPlinq = Me.ListGetGeneralBalanceTrialBalance(INDFechaIni, INDBookId)

        Dim INDMes As Integer
        Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDFechaIni)
        INDMes = Month(INDFechaMesAnte)
        Dim INDAno As Integer
        INDAno = Year(INDFechaMesAnte)

        Dim resulxpoPreviousBalance As New List(Of GeneralLedgerBalanceXpo)
        Dim dictionaryPreviousBalance As New Dictionary(Of String, String)

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then

            resulPlinq = Me.ListGetGeneralBalanceTrialBalanceMonth13(14, INDAno, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            For Each itemPlinq In resulPlinq.Source
                Dim ValueId As Integer
                If resulxpoPreviousBalance.Count < 1 Then
                    ValueId = 1
                Else
                    ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                End If
                Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                Dim thirdParty As New CommonThirdPartyXpo(session)
                thirdParty.Id = itemPlinq.IdTercero
                thirdParty.Nit = itemPlinq.Nit
                thirdParty.Name = itemPlinq.Name
                Dim costCenter As New CostCenterXpo(session)
                costCenter.Id = itemPlinq.IdCostCenter
                costCenter.Code = itemPlinq.Code
                costCenter.Name = itemPlinq.CostCenterName
                Dim mainAccount As New MainAccountsXpo(session)
                mainAccount.Id = itemPlinq.IdCuenta
                mainAccount.Nature = itemPlinq.Nature
                ResultGeneralBalance.Id = ValueId
                ResultGeneralBalance.IdMainAccount = mainAccount
                ResultGeneralBalance.IdThirdParty = thirdParty
                ResultGeneralBalance.SaldoTercero = 0
                ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                ResultGeneralBalance.SaldoAnterior = 0
                ResultGeneralBalance.NuevoSaldo = 0
                ResultGeneralBalance.NuevoSaldoTercero = 0
                resulxpoPreviousBalance.Add(ResultGeneralBalance)

                dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
            Next
        Else
            resulPlinq = Me.ListGetGeneralBalanceTrialBalanceMonth13(14, INDAno - 1, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceTrialBalance(INDFechaIni, INDBookId, INDAccountIni, INDAccountEnd, INDThirdPartyStart, INDThirdPartyEnd, INDCostCenterStart, INDCostCenterEnd)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim ValueId As Integer
                    If resulxpoPreviousBalance.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim thirdParty As New CommonThirdPartyXpo(session)
                    thirdParty.Id = itemPlinq3.IdTercero
                    thirdParty.Nit = itemPlinq3.Nit
                    thirdParty.Name = itemPlinq3.Name
                    Dim costCenter As New CostCenterXpo(session)
                    costCenter.Id = itemPlinq3.IdCostCenter
                    costCenter.Code = itemPlinq3.Code
                    costCenter.Name = itemPlinq3.CostCenterName
                    Dim mainAccount As New MainAccountsXpo(session)
                    mainAccount.Id = itemPlinq3.IdCuenta
                    mainAccount.Nature = itemPlinq3.Nature
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount
                    ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.SaldoTercero = 0
                    ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                    ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = 0
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulxpoPreviousBalance.Add(ResultGeneralBalance)
                    dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdTercero & " - " & itemPlinq3.IdCostCenter, itemPlinq3.IdCuenta)
                Next
            Else
                'recorremos el primer listado
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta And e.IdCostCenter = itemPlinq3.IdCostCenter And e.IdTercero = itemPlinq3.IdTercero Select e).ToList
                    If listFilter3.Count > 0 Then
                        For Each itemPlinq In listFilter3
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                            Dim ValueId As Integer
                            If resulxpoPreviousBalance.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = itemPlinq.IdTercero
                            thirdParty.Nit = itemPlinq.Nit
                            thirdParty.Name = itemPlinq.Name
                            Dim costCenter As New CostCenterXpo(session)
                            costCenter.Id = itemPlinq.IdCostCenter
                            costCenter.Code = itemPlinq.Code
                            costCenter.Name = itemPlinq.CostCenterName
                            Dim mainAccount As New MainAccountsXpo(session)
                            mainAccount.Id = itemPlinq.IdCuenta
                            mainAccount.Nature = itemPlinq.Nature
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.SaldoTercero = 0
                            ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                            ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = 0
                            resulxpoPreviousBalance.Add(ResultGeneralBalance)

                            dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
                        Next
                    Else
                        'If Not dictionaryPreviousBalance.ContainsKey(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdTercero & " - " & itemPlinq3.IdCostCenter) Then

                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq3.IdTercero
                        thirdParty.Nit = itemPlinq3.Nit
                        thirdParty.Name = itemPlinq3.Name
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq3.IdCostCenter
                        costCenter.Code = itemPlinq3.Code
                        costCenter.Name = itemPlinq3.CostCenterName
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq3.IdCuenta
                        mainAccount.Nature = itemPlinq3.Nature
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq3.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq3.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)
                        dictionaryPreviousBalance.Add(itemPlinq3.IdCuenta & " - " & itemPlinq3.IdTercero & " - " & itemPlinq3.IdCostCenter, itemPlinq3.IdCuenta)
                        'End If
                    End If
                Next
                For Each itemPlinq In resulPlinq.Source
                    If Not dictionaryPreviousBalance.ContainsKey(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero & " - " & itemPlinq.IdCostCenter) Then
                        Dim ValueId As Integer
                        If resulxpoPreviousBalance.Count < 1 Then
                            ValueId = 1
                        Else
                            ValueId = resulxpoPreviousBalance.Max(Function(x) x.Id)
                        End If
                        Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                        Dim thirdParty As New CommonThirdPartyXpo(session)
                        thirdParty.Id = itemPlinq.IdTercero
                        thirdParty.Nit = itemPlinq.Nit
                        thirdParty.Name = itemPlinq.Name
                        Dim costCenter As New CostCenterXpo(session)
                        costCenter.Id = itemPlinq.IdCostCenter
                        costCenter.Code = itemPlinq.Code
                        costCenter.Name = itemPlinq.CostCenterName
                        Dim mainAccount As New MainAccountsXpo(session)
                        mainAccount.Id = itemPlinq.IdCuenta
                        mainAccount.Nature = itemPlinq.Nature
                        ResultGeneralBalance.Id = ValueId
                        ResultGeneralBalance.IdMainAccount = mainAccount
                        ResultGeneralBalance.IdThirdParty = thirdParty
                        ResultGeneralBalance.SaldoTercero = 0
                        ResultGeneralBalance.CreditValue = itemPlinq.SumaCredito
                        ResultGeneralBalance.DebitValue = itemPlinq.sumaDebito
                        ResultGeneralBalance.SaldoAnterior = 0
                        ResultGeneralBalance.NuevoSaldo = 0
                        ResultGeneralBalance.NuevoSaldoTercero = 0
                        resulxpoPreviousBalance.Add(ResultGeneralBalance)

                        dictionaryPreviousBalance.Add(itemPlinq.IdCuenta & " - " & itemPlinq.IdTercero & " - " & itemPlinq.IdCostCenter, itemPlinq.IdCuenta)
                    End If
                Next

            End If
        End If

        Dim dictionaryBalance As New Dictionary(Of String, MainAccountsXpo)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id)
            Dim OperacionXpo As Long
            'operacion para sacar el nuevo saldo segun la naturaleza de la cuenta
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
            End If
            'cuando las cuentas no tienen terceros o centros de costos
            If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                Dim resultAccumulateAccount = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemXpo.IdMainAccount.Id Select e).ToList
                If resultAccumulateAccount.Count > 0 Then
                    Dim item = (From e In resultAccumulateAccount Group e By e.IdMainAccount.Id Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito}).SingleOrDefault
                    'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                    Dim OperacionLinq As Long
                    If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                        OperacionLinq = (item.sumaDebito - item.SumaCredito)
                    Else
                        OperacionLinq = (item.SumaCredito - item.sumaDebito)
                    End If
                    If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                        dictionaryBalance.Add(itemXpo.IdMainAccount.Id, itemXpo.IdMainAccount)
                        itemXpo.SaldoCuenta += OperacionLinq
                        itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                    End If
                Else ' Si no hay acumulados de la cuenta y el tercero
                    itemXpo.SaldoCuenta = 0
                    itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                End If
            Else
                itemXpo.NuevoSaldo = OperacionXpo
            End If
            'si selecciona detallar centros de costos
            If INDDetailingCostCenter = True Then
                If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) Then
                    Dim resultAccumulate = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemXpo.IdMainAccount.Id And If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Id) = If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id) Select e).ToList
                    If resultAccumulate.Count > 0 Then
                        Dim itemResult = (From e In resultAccumulate Group e By e.IdMainAccount.Id, idCostCenter = If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Id) Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito}).SingleOrDefault
                        'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                        Dim OperacionLinq As Long
                        If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                            OperacionLinq = (itemResult.sumaDebito - itemResult.SumaCredito)
                        Else
                            OperacionLinq = (itemResult.SumaCredito - itemResult.sumaDebito)
                        End If

                        dictionaryBalance.Add(itemXpo.IdMainAccount.Id & "-" & itemXpo.IdCostCenter.Id, itemXpo.IdMainAccount)
                        itemXpo.SaldoAnterior = OperacionLinq
                        itemXpo.NuevoSaldoCentroCosto = itemXpo.SaldoAnterior + OperacionXpo

                    Else ' Si no hay acumulados de la cuenta y el tercero
                        itemXpo.SaldoAnterior = 0
                        itemXpo.NuevoSaldoCentroCosto = itemXpo.SaldoAnterior + OperacionXpo
                    End If
                Else
                    itemXpo.NuevoSaldoCentroCosto = OperacionXpo
                End If
            End If
            'si selecciona detallar terceros
            If INDDetailingThirdParty = True Then
                If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id)) Then
                    Dim resultAccumulateThirParty = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemXpo.IdMainAccount.Id And If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Id) = If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) Select e).ToList
                    If resultAccumulateThirParty.Count > 0 Then
                        Dim item = (From e In resultAccumulateThirParty Group e By e.IdMainAccount.Id, ThirpartyId = If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Id) Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito}).SingleOrDefault
                        'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                        Dim OperacionLinq As Long
                        If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                            OperacionLinq = (item.sumaDebito - item.SumaCredito)
                        Else
                            OperacionLinq = (item.SumaCredito - item.sumaDebito)
                        End If
                        dictionaryBalance.Add(itemXpo.IdMainAccount.Id & "-" & itemXpo.IdThirdParty.Id, itemXpo.IdMainAccount)
                        itemXpo.SaldoTercero = OperacionLinq
                        itemXpo.NuevoSaldoTercero = itemXpo.SaldoTercero + OperacionXpo
                    Else ' Si no hay acumulados de la cuenta y el tercero
                        itemXpo.SaldoTercero = 0
                        itemXpo.NuevoSaldoTercero = itemXpo.SaldoTercero + OperacionXpo
                    End If
                Else
                    itemXpo.NuevoSaldoTercero = OperacionXpo
                End If
            End If
        Next


        For Each itemNotMovement In resulxpoPreviousBalance
            If Not dictionaryBalance.ContainsKey(itemNotMovement.IdMainAccount.Id) Then

                Dim resultAccumulate = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemNotMovement.IdMainAccount.Id Select e).ToList
                If resultAccumulate.Count > 0 Then
                    Dim item = (From e In resultAccumulate Group e By e.IdMainAccount.Id, Nature = e.IdMainAccount.Nature Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .Nature = Nature}).SingleOrDefault
                    'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                    Dim OperacionLinq As Long
                    If item.Nature = 1 Then
                        OperacionLinq = (item.sumaDebito - item.SumaCredito)
                    Else
                        OperacionLinq = (item.SumaCredito - item.sumaDebito)
                    End If
                    Dim ValueId As Integer
                    If resulXpo.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulXpo.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim criteriaAccount = "Id = " & itemNotMovement.IdMainAccount.Id
                    Dim mainAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount, session)
                    'Dim mainAccount = dictionaryBalance(itemNotMovement.IdCuenta)
                    'Dim thirdParty As New CommonThirdPartyXpo(session)
                    'thirdParty.Id = item.IdTercero
                    'thirdParty.Nit = item.Nit
                    'thirdParty.Name = item.Name
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount(0)
                    'ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.SaldoCuenta = OperacionLinq
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = OperacionLinq
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulXpo.Add(ResultGeneralBalance)
                    dictionaryBalance.Add(itemNotMovement.IdMainAccount.Id, mainAccount(0))
                End If

            Else

                'agregamos al listado las cuentas y terceros que no tienen movimientos
                If INDDetailingThirdParty = True Then
                    If Not dictionaryBalance.ContainsKey(itemNotMovement.IdMainAccount.Id & "-" & If(IsNothing(itemNotMovement.IdThirdParty) = True, 0, itemNotMovement.IdThirdParty.Id)) Then
                        Dim resultAccumulateThirParty = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemNotMovement.IdMainAccount.Id And If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Id) = If(IsNothing(itemNotMovement.IdThirdParty) = True, 0, itemNotMovement.IdThirdParty.Id) Select e).ToList
                        If resultAccumulateThirParty.Count > 0 Then
                            Dim item = (From e In resultAccumulateThirParty Group e By e.IdMainAccount.Id, IdTercero = If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Id), Nature = e.IdMainAccount.Nature, Nit = If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Nit), Name = If(IsNothing(e.IdThirdParty) = True, 0, e.IdThirdParty.Name) Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdTercero, .Nature = Nature, .Nit = Nit, .Name = Name}).SingleOrDefault
                            'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                            Dim OperacionLinq As Long
                            If item.Nature = 1 Then
                                OperacionLinq = (item.sumaDebito - item.SumaCredito)
                            Else
                                OperacionLinq = (item.SumaCredito - item.sumaDebito)
                            End If
                            Dim ValueId As Integer
                            If resulXpo.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulXpo.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim mainAccount = dictionaryBalance(itemNotMovement.IdMainAccount.Id)
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = item.IdTercero
                            thirdParty.Nit = item.Nit
                            thirdParty.Name = item.Name
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.SaldoTercero = OperacionLinq
                            ResultGeneralBalance.CreditValue = 0
                            ResultGeneralBalance.DebitValue = 0
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = OperacionLinq
                            resulXpo.Add(ResultGeneralBalance)
                            dictionaryBalance.Add(itemNotMovement.IdMainAccount.Id & "-" & If(IsNothing(itemNotMovement.IdThirdParty) = True, 0, itemNotMovement.IdThirdParty.Id), mainAccount)
                        End If
                    End If
                End If

                'agregamos al listado las cuentas y centros que no tienen movimientos
                If INDDetailingCostCenter = True Then
                    If Not dictionaryBalance.ContainsKey(itemNotMovement.IdMainAccount.Id & "-" & If(IsNothing(itemNotMovement.IdCostCenter) = True, 0, itemNotMovement.IdCostCenter.Id)) Then
                        Dim resultAccumulateCostCenter = (From e In resulxpoPreviousBalance Where e.IdMainAccount.Id = itemNotMovement.IdMainAccount.Id And If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Id) = If(IsNothing(itemNotMovement.IdCostCenter) = True, 0, itemNotMovement.IdCostCenter.Id) Select e).ToList
                        If resultAccumulateCostCenter.Count > 0 Then
                            Dim item = (From e In resultAccumulateCostCenter Group e By e.IdMainAccount.Id, IdCostCenter = If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Id), Nature = e.IdMainAccount.Nature, Code = If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Code), CostCenterName = If(IsNothing(e.IdCostCenter) = True, 0, e.IdCostCenter.Name) Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter, .Nature = Nature, .Code = Code, .CostCenterName = CostCenterName}).SingleOrDefault
                            'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                            Dim OperacionLinq As Long
                            If item.Nature = 1 Then
                                OperacionLinq = (item.sumaDebito - item.SumaCredito)
                            Else
                                OperacionLinq = (item.SumaCredito - item.sumaDebito)
                            End If
                            Dim ValueId As Integer
                            If resulXpo.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulXpo.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim mainAccount = dictionaryBalance(itemNotMovement.IdMainAccount.Id)
                            Dim costCenter As New CostCenterXpo(session)
                            costCenter.Id = item.IdCostCenter
                            costCenter.Code = item.Code
                            costCenter.Name = item.CostCenterName
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdCostCenter = costCenter
                            ResultGeneralBalance.SaldoTercero = OperacionLinq
                            ResultGeneralBalance.CreditValue = 0
                            ResultGeneralBalance.DebitValue = 0
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoCentroCosto = OperacionLinq
                            resulXpo.Add(ResultGeneralBalance)
                            dictionaryBalance.Add(itemNotMovement.IdMainAccount.Id & "-" & If(IsNothing(itemNotMovement.IdCostCenter) = True, 0, itemNotMovement.IdCostCenter.Id), mainAccount)
                        End If
                    End If
                End If
            End If
        Next

        Return resulXpo
        'End Using


    End Function

#Region "Listar Saldo Anterior Trial Balance"

    ''' <summary>
    ''' Listar Saldo Anterior Trial Balance
    ''' </summary>
    Public Function ListGetGeneralBalanceTrialBalance(ByVal INDFechaIni As Date, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource


        Dim INDDateEndBalance = DateAdd(DateInterval.Month, -1, INDFechaIni)
        Dim INDMontEnd As Integer = Month(INDDateEndBalance)
        Dim INDYear As Integer = Year(INDDateEndBalance)
        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource

        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount, T1.IdCostCenter, MA.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount, T1.IdCostCenter, MA.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount, T1.IdCostCenter, MA.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId And MA.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And MA.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdThirdParty, T1.IdMainAccount, T1.IdCostCenter, MA.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Saldo Anterior trial balance Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceTrialBalanceMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim b As New PLinqServerModeSource
        If INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals Ma.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId And Ma.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And Ma.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd)
                                     Group T1 By T1.IdMainAccount, T1.IdThirdParty, T1.IdCostCenter, Ma.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart Is Nothing And INDCostCenterEnd Is Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals Ma.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId And Ma.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And Ma.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd)
                                     Group T1 By T1.IdMainAccount, T1.IdThirdParty, T1.IdCostCenter, Ma.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart Is Nothing And INDThirdPartyEnd Is Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals Ma.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId And Ma.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And Ma.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount, T1.IdThirdParty, T1.IdCostCenter, Ma.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()

        ElseIf INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing And INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                     Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                     Join MAC In tableMainAccountClass On MAC.Id Equals Ma.IdAccountClass.Id
                                     Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                     Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                     Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId And Ma.Number >= If(IsNothing(INDAccountIni) = True, "0", INDAccountIni) And Ma.Number <= If(IsNothing(INDAccountEnd) = True, "99999999999999999999", INDAccountEnd) And TP.Nit >= If(IsNothing(INDThirdPartyStart) = True, "0", INDThirdPartyStart) And TP.Nit <= If(IsNothing(INDThirdPartyEnd) = True, "999999999999999", INDThirdPartyEnd) And CC.Code >= If(IsNothing(INDCostCenterStart) = True, "0", INDCostCenterStart) And CC.Code <= If(IsNothing(INDCostCenterEnd) = True, "99999999999999999999", INDCostCenterEnd)
                                     Group T1 By T1.IdMainAccount, T1.IdThirdParty, T1.IdCostCenter, Ma.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                     Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
            b.Source = TmpQueryableSource.ToList()
        End If
        Return b
        'End Using


    End Function

#End Region

#End Region

#Region "Informe Balance General"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionGeneralBalance(ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDNatureAccount As String, ByVal INDMemorandumAccounts As String, ByVal INDAccountingZero As Integer, ByVal INDBookId As Integer, ByVal INDDetailingThirdParty As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim resulXpo As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)

        'Definir Criteria 
        Dim criteria As String = Nothing
        criteria &= "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "# AND Month < 13 AND IdMainAccount.IdAccountClass.Type in (" & INDMemorandumAccounts & ") AND IdMainAccount.IdAccountClass.Nature in (" & INDNatureAccount & ") AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If

        Dim session As New Session(XpoDefault.DataLayer)
        resulXpo = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria, session)

        'si selecciona listar cuentas en 0
        If INDAccountingZero = True Then
            'definir criteria lista cuentas en 0
            Dim criteriaAccount As String = Nothing
            criteriaAccount = "AllowsMovement = 1 AND IdAccountClass.Type in (" & INDMemorandumAccounts & ") AND IdAccountClass.Nature in (" & INDNatureAccount & ") AND LegalBookId.Id = " & INDBookId
            resulXpoAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount, session)

            For Each itemXpoAccount In resulXpoAccount
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.IdMainAccount.Id = itemXpoAccount.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim ValueId As Integer
                    If resulXpo.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulXpo.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(itemXpoAccount.Session)
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = itemXpoAccount
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    resulXpo.Add(ResultGeneralBalance)
                End If
            Next
        End If

        Dim INDAno = Year(INDDateStart)

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        resulPlinq = Me.ListGetGeneralBalanceBalanceMonth13(14, INDAno, INDBookId, INDMemorandumAccounts)

        Dim dictionaryBalance As New Dictionary(Of String, MainAccountsXpo)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id)
            Dim OperacionXpo As Long
            'operacion para sacar el nuevo saldo segun la naturaleza de la cuenta
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
            End If
            'cuando las cuentas no tienen terceros o centros de costos
            If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                Dim resultAccumulateAccount = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id Select e).ToList
                If resultAccumulateAccount.Count > 0 Then
                    Dim item = (From e In resultAccumulateAccount Group e By e.IdCuenta Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta}).SingleOrDefault
                    'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                    Dim OperacionLinq As Long
                    If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                        OperacionLinq = (item.sumaDebito - item.SumaCredito)
                    Else
                        OperacionLinq = (item.SumaCredito - item.sumaDebito)
                    End If
                    If Not dictionaryBalance.ContainsKey(item.IdCuenta) Then
                        dictionaryBalance.Add(item.IdCuenta, itemXpo.IdMainAccount)
                        itemXpo.SaldoCuenta += OperacionLinq
                        itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                    End If
                Else ' Si no hay acumulados de la cuenta y el tercero
                    itemXpo.SaldoCuenta = 0
                    itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                End If
            Else
                itemXpo.NuevoSaldo = OperacionXpo
            End If
            'si selecciona detallar terceros
            If INDDetailingThirdParty = True Then
                If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id)) Then
                    Dim resultAccumulateThirParty = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id And e.IdTercero = If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) Select e).ToList
                    If resultAccumulateThirParty.Count > 0 Then
                        Dim item = (From e In resultAccumulateThirParty Group e By e.IdCuenta, e.IdTercero Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta, .IdTercero = IdTercero}).SingleOrDefault
                        'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                        Dim OperacionLinq As Long
                        If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                            OperacionLinq = (item.sumaDebito - item.SumaCredito)
                        Else
                            OperacionLinq = (item.SumaCredito - item.sumaDebito)
                        End If
                        dictionaryBalance.Add(item.IdCuenta & "-" & item.IdTercero, itemXpo.IdMainAccount)
                        itemXpo.SaldoTercero = OperacionLinq
                        itemXpo.NuevoSaldoTercero = itemXpo.SaldoTercero + OperacionXpo
                    Else ' Si no hay acumulados de la cuenta y el tercero
                        itemXpo.SaldoTercero = 0
                        itemXpo.NuevoSaldoTercero = itemXpo.SaldoTercero + OperacionXpo
                    End If
                End If
            End If
        Next

        For Each itemNotMovement In resulPlinq.Source
            If Not dictionaryBalance.ContainsKey(itemNotMovement.IdCuenta) Then

                Dim resultAccumulate = (From e In resulPlinq.Source Where e.IdCuenta = itemNotMovement.IdCuenta Select e).ToList
                If resultAccumulate.Count > 0 Then
                    Dim item = (From e In resultAccumulate Group e By e.IdCuenta, e.Nature Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta, .Nature = Nature}).SingleOrDefault
                    'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                    Dim OperacionLinq As Long
                    If item.Nature = 1 Then
                        OperacionLinq = (item.sumaDebito - item.SumaCredito)
                    Else
                        OperacionLinq = (item.SumaCredito - item.sumaDebito)
                    End If
                    Dim ValueId As Integer
                    If resulXpo.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulXpo.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                    Dim criteriaAccount = "Id = " & itemNotMovement.IdCuenta
                    Dim mainAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount, session)
                    'Dim mainAccount = dictionaryBalance(itemNotMovement.IdCuenta)
                    'Dim thirdParty As New CommonThirdPartyXpo(session)
                    'thirdParty.Id = item.IdTercero
                    'thirdParty.Nit = item.Nit
                    'thirdParty.Name = item.Name
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = mainAccount(0)
                    'ResultGeneralBalance.IdThirdParty = thirdParty
                    ResultGeneralBalance.SaldoCuenta = OperacionLinq
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    ResultGeneralBalance.SaldoAnterior = 0
                    ResultGeneralBalance.NuevoSaldo = OperacionLinq
                    ResultGeneralBalance.NuevoSaldoTercero = 0
                    resulXpo.Add(ResultGeneralBalance)
                    dictionaryBalance.Add(itemNotMovement.IdCuenta, mainAccount(0))
                End If
            Else
                'agregamos al listado las cuentas y terceros que no tienen movimientos
                If INDDetailingThirdParty = True Then
                    If Not dictionaryBalance.ContainsKey(itemNotMovement.IdCuenta & "-" & itemNotMovement.IdTercero) Then
                        Dim resultAccumulateThirParty = (From e In resulPlinq.Source Where e.IdCuenta = itemNotMovement.IdCuenta And e.IdTercero = itemNotMovement.IdTercero Select e).ToList
                        If resultAccumulateThirParty.Count > 0 Then
                            Dim item = (From e In resultAccumulateThirParty Group e By e.IdCuenta, e.IdTercero, e.Nature, e.Nit, e.Name Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta, .IdTercero = IdTercero, .Nature = Nature, .Nit = Nit, .Name = Name}).SingleOrDefault
                            'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                            Dim OperacionLinq As Long
                            If item.Nature = 1 Then
                                OperacionLinq = (item.sumaDebito - item.SumaCredito)
                            Else
                                OperacionLinq = (item.SumaCredito - item.sumaDebito)
                            End If
                            Dim ValueId As Integer
                            If resulXpo.Count < 1 Then
                                ValueId = 1
                            Else
                                ValueId = resulXpo.Max(Function(x) x.Id)
                            End If
                            Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(session)
                            Dim mainAccount = dictionaryBalance(itemNotMovement.IdCuenta)
                            Dim thirdParty As New CommonThirdPartyXpo(session)
                            thirdParty.Id = item.IdTercero
                            thirdParty.Nit = item.Nit
                            thirdParty.Name = item.Name
                            ResultGeneralBalance.Id = ValueId
                            ResultGeneralBalance.IdMainAccount = mainAccount
                            ResultGeneralBalance.IdThirdParty = thirdParty
                            ResultGeneralBalance.SaldoTercero = OperacionLinq
                            ResultGeneralBalance.CreditValue = 0
                            ResultGeneralBalance.DebitValue = 0
                            ResultGeneralBalance.SaldoAnterior = 0
                            ResultGeneralBalance.NuevoSaldo = 0
                            ResultGeneralBalance.NuevoSaldoTercero = OperacionLinq
                            resulXpo.Add(ResultGeneralBalance)
                            dictionaryBalance.Add(itemNotMovement.IdCuenta & "-" & itemNotMovement.IdTercero, mainAccount)
                        End If
                    End If
                End If
            End If
        Next

        Return resulXpo
        'End Using

    End Function

#Region "Listar Saldo Anterior General balance Mes 13"

    ''' <summary>
    ''' Listar Saldo del mes 13 del balance general
    ''' </summary>
    Public Function ListGetGeneralBalanceBalanceMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer, ByVal INDMemorandumAccounts As String) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim ArrayType = Split(INDMemorandumAccounts, ",")

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(session)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(session)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals Ma.IdAccountClass.Id
                                 Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                 Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
                                 Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId And ArrayType.Contains(MAC.Type)
                                 Group T1 By T1.IdMainAccount, T1.IdThirdParty, T1.IdCostCenter, MAC.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                 Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Informe Estado de Resultado"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionResultStatus(ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDNatureAccount As String, ByVal INDAccountsCostCenter As Integer, ByVal INDAccountingZero As Integer, ByVal INDVisualization As Integer, ByVal INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)

        Dim resulXpo As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)

        'Definir Criteria 
        Dim criteria As String = Nothing
        criteria &= "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "# AND Month < 13 AND IdMainAccount.IdAccountClass.Type in (2) AND IdMainAccount.IdAccountClass.Nature in (" & INDNatureAccount & ") AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If
        'filtro cuentas con centro de costo
        If INDAccountsCostCenter = True Then
            criteria &= "AND IdMainAccount.HandlesCostCenter = 1"
        End If
        Dim session As New Session(XpoDefault.DataLayer)
        resulXpo = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria, session)

        'si selecciona listar cuentas en 0
        If INDAccountingZero = True Then
            'definir criteria lista cuentas en 0
            Dim criteriaAccount As String = Nothing
            criteriaAccount = "AllowsMovement = 1 AND IdAccountClass.Type in (2) AND IdAccountClass.Nature in (" & INDNatureAccount & ") AND LegalBookId.Id = " & INDBookId
            If INDAccountsCostCenter = True Then
                criteriaAccount &= "AND HandlesCostCenter = 1"
            End If
            resulXpoAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount, session)

            For Each itemXpoAccount In resulXpoAccount
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.IdMainAccount.Id = itemXpoAccount.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim ValueId As Integer
                    If resulXpo.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = resulXpo.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(itemXpoAccount.Session)
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = itemXpoAccount
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    resulXpo.Add(ResultGeneralBalance)
                End If
            Next
        End If

        If INDVisualization = 2 Then

            Dim INDMes As Integer
            Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDDateStart)
            INDMes = Month(INDFechaMesAnte)
            Dim INDAno As Integer
            INDAno = Year(INDFechaMesAnte)

            'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
            Dim resulPlinq As PLinqServerModeSource
            If INDMes = 12 Then
                resulPlinq = Me.ListGetGeneralResultStatusMonth13(14, INDAno, INDBookId)
            Else
                resulPlinq = Me.ListGetGeneralResultStatusMonth13(14, INDAno - 1, INDBookId)
                Dim resulPlinq3 As PLinqServerModeSource
                resulPlinq3 = Me.ListGetGeneralBalanceStatusResult(INDDateStart, INDBookId)
                'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
                If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                    resulPlinq.Source = resulPlinq3.Source
                Else
                    For Each itemPlinq3 In resulPlinq3.Source
                        Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta Select e).ToList
                        For Each itemPlinq In listFilter3
                            itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                            itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                        Next
                    Next
                End If
            End If

            Dim dictionaryBalance As New Dictionary(Of String, Long)
            For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id)
                If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                    Dim resultAccumulateAccount = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id Select e).ToList
                    If resultAccumulateAccount.Count > 0 Then
                        Dim item = (From e In resultAccumulateAccount Group e By e.IdCuenta Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta}).SingleOrDefault
                        'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                        Dim OperacionLinq As Long
                        If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                            OperacionLinq = (item.sumaDebito - item.SumaCredito)
                        Else
                            OperacionLinq = (item.SumaCredito - item.sumaDebito)
                        End If
                        If Not dictionaryBalance.ContainsKey(item.IdCuenta) Then
                            dictionaryBalance.Add(item.IdCuenta, OperacionLinq)
                            itemXpo.SaldoAnterior += OperacionLinq
                            'itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                        End If
                    Else ' Si no hay acumulados de la cuenta y el tercero
                        itemXpo.SaldoAnterior = 0
                        'itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                    End If
                End If
            Next
        End If
        Return resulXpo
        'End Using

    End Function

#Region "Listar Saldo Anterior Estado Resultado"

    ''' <summary>
    ''' Listar Saldo Anterior Estado Resultado
    ''' </summary>
    Public Function ListGetGeneralBalanceStatusResult(ByVal INDDateStart As Date, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim INDDateEndBalance = DateAdd(DateInterval.Month, -1, INDDateStart)
        Dim INDMontEnd As Integer = Month(INDDateEndBalance)
        Dim INDYear As Integer = Year(INDDateEndBalance)

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccounts As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccounts On MA.Id Equals T1.IdMainAccount.Id
                                 Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId
                                 Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                 Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using

    End Function

#End Region

#Region "Listar Saldo Anterior Estado Resultado Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Estado Resultado Mes 13
    ''' </summary>
    Public Function ListGetGeneralResultStatusMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(session)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                 Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                                 Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                 Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Patrimonial Changes"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionPatrimonialChanges(ByVal INDAccountStart As String, ByVal INDAccountEnd As String, ByVal INDLastClosingDate As Date) As List(Of GeneralLedgerBalanceXpo)
        Dim result As IList(Of GeneralLedgerBalanceXpo)

        'Dim INDLastClosingMonth As Integer = Month(INDDatePreviousMonth)
        Dim INDLastClosingYear As Integer = Year(INDLastClosingDate)
        'Definir Criteria
        Dim criteria As String = Nothing
        criteria = "IdMainAccount.Number >= '" & INDAccountStart & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "' AND Month = 14 AND Year =" & INDLastClosingYear

        'cargar datasource
        result = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria)

        'consultar el saldo anterior 
        Dim INDDatePreviousYear = DateAdd(DateInterval.Year, +1, INDLastClosingDate)
        Dim YearPreviousBalance As Integer = Year(INDDatePreviousYear)
        Dim resulPlinq As PLinqServerModeSource
        resulPlinq = Me.ListGetGeneralBalancePatrimonialChanges(YearPreviousBalance)

        'adicionar el nuevo saldo al xpo
        Dim dictionaryBalance As New Dictionary(Of String, Integer)
        For Each itemXpo In result.OrderBy(Function(x) x.IdMainAccount.Id)
            Dim OperacionXpo As Long
            'operacion para sacar el nuevo saldo segun la naturaleza de la cuenta
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                If itemXpo.DebitValue <> 0 Then
                    OperacionXpo = itemXpo.DebitValue
                Else
                    OperacionXpo = itemXpo.CreditValue * -1
                End If
            Else
                If itemXpo.DebitValue <> 0 Then
                    OperacionXpo = itemXpo.DebitValue * -1
                Else
                    OperacionXpo = itemXpo.CreditValue
                End If
            End If

            Dim resultAccumulateAccount = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id Select e).ToList
            If resultAccumulateAccount.Count > 0 Then
                Dim item = (From e In resultAccumulateAccount Group e By e.IdCuenta Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta}).SingleOrDefault
                'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.sumaDebito - item.SumaCredito)
                Else
                    OperacionLinq = (item.SumaCredito - item.sumaDebito)
                End If
                If Not dictionaryBalance.ContainsKey(item.IdCuenta) Then
                    dictionaryBalance.Add(item.IdCuenta, OperacionLinq)
                    itemXpo.SaldoCuenta += OperacionLinq
                    itemXpo.SaldoAnterior = OperacionXpo
                    itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                    If itemXpo.SaldoCuenta > 0 Then
                        itemXpo.TypeValue = 1
                    ElseIf itemXpo.SaldoCuenta < 0 Then
                        itemXpo.TypeValue = 2
                    ElseIf itemXpo.SaldoCuenta = 0 Then
                        itemXpo.TypeValue = 3
                    End If
                End If
            Else ' Si no hay acumulados de la cuenta 
                itemXpo.SaldoCuenta = 0
                itemXpo.SaldoAnterior = OperacionXpo
                itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                itemXpo.TypeValue = 3
            End If
        Next

        Return result
    End Function

#Region "Listar los movimientos Informe Saldo de terceros"

    ''' <summary>
    ''' Listar los movimientos Informe Saldo de terceros
    ''' </summary>
    Public Function ListGetGeneralBalancePatrimonialChanges(ByVal YearPreviousBalance As Integer) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As New XPQuery(Of GeneralLedgerBalanceXpo)(session)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Where T1.Month >= 1 And T1.Month <= 12 And T1.Year = YearPreviousBalance
                                 Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                 Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Ledger And Balance"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionLedgerAndBalance(ByVal INDPeriod As Date, ByVal INDAccountZero As Boolean, ByVal INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim result As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)
        Dim resultBalancePreviosClosed As XPCollection(Of GeneralLedgerVBalanceMainAccountXpo)
        Dim resulBalancePreviosOpen As XPCollection(Of GeneralLedgerVBalanceMainAccountXpo)
        Dim resultBalance As IEnumerable(Of GeneralLedgerVBalanceMainAccountXpo)
        Dim session As New Session(XpoDefault.DataLayer)
        Dim INDMonthMovement As Integer = Month(INDPeriod)
        Dim INDYearMovement As Integer = Year(INDPeriod)
        'Definir Criteria
        Dim criteria As String = Nothing
        criteria = "Month = " & INDMonthMovement & "AND Year =" & INDYearMovement & " AND IdMainAccount.LegalBookId.Id = " & INDBookId

        'cargar datasource
        result = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria, session)

        'consultar el saldo anterior al cierre anual
        Dim INDYearPreviousDate = DateAdd(DateInterval.Year, -1, INDPeriod)
        Dim INDYearPreviousBalance = Year(INDYearPreviousDate)
        Dim criteriaBalancePreviousClosed As CriteriaOperator = CriteriaOperator.Parse("MonthBalance = 14 AND YearBalance = " & INDYearPreviousBalance & " AND LegalBookId = " & INDBookId)
        resultBalancePreviosClosed = New XPCollection(Of GeneralLedgerVBalanceMainAccountXpo)(session, criteriaBalancePreviousClosed)
        Dim INDMonthPreviousDate = DateAdd(DateInterval.Month, -1, INDPeriod)
        Dim INDMontPreviousBalance As Integer = Month(INDMonthPreviousDate)

        'si el mes es diferente de 12 unir el saldo anterior del año cerrado con los movimientos
        If INDMontPreviousBalance <> 12 Then
            'consultamos el saldo anterior de las cuentas 
            Dim criteriaPreviosBalanceOpen As CriteriaOperator = CriteriaOperator.Parse("MonthBalance >= 1 AND MonthBalance <= " & INDMontPreviousBalance & " AND YearBalance = " & INDYearMovement & " AND LegalBookId = " & INDBookId)
            resulBalancePreviosOpen = New XPCollection(Of GeneralLedgerVBalanceMainAccountXpo)(session, criteriaPreviosBalanceOpen)
            ' unimos el saldo que hubo en el ultimo año cerrado y los de los meses del año abierto
            resultBalance = resultBalancePreviosClosed.Union(resulBalancePreviosOpen)

        Else
            resultBalance = resultBalancePreviosClosed
        End If

        'adicionar el nuevo saldo al xpo
        Dim dictionaryBalance As New Dictionary(Of Integer, Long)
        For Each itemXpo In result.OrderBy(Function(x) x.IdMainAccount.Id)
            If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                'agrupamos los movimientos consultados por id de la cuenta
                Dim objData = (From da In result Where da.IdMainAccount.Id = itemXpo.IdMainAccount.Id Group da By MainAccountId = da.IdMainAccount.Id Into MovementDebit = Sum(CType(da.DebitValue, Decimal)), MovementCredit = Sum(CType(da.CreditValue, Decimal)) Select New With {.MovementDebit = MovementDebit, .MovementCredit = MovementCredit, .MainAccountId = MainAccountId}).SingleOrDefault
                Dim OperacionXpo As Long
                'operacion para sacar el nuevo saldo segun la naturaleza de la cuenta
                'If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (objData.MovementDebit - objData.MovementCredit)
                'Else
                '    OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                'End If
                Dim resultAccumulateAccount = resultBalance.Where(Function(x) x.IdAccount = itemXpo.IdMainAccount.Id).ToList
                If resultAccumulateAccount.Count > 0 Then
                    Dim item = (From e In resultAccumulateAccount Group e By e.IdAccount Into sumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdAccount}).SingleOrDefault

                    Dim ValueBalance As Long
                    ValueBalance = item.sumaDebito - item.SumaCredito
                    If ValueBalance > 0 Then
                        itemXpo.SaldoAnteriorDebito = ValueBalance
                        If OperacionXpo > 0 Then
                            itemXpo.NuevoSaldoDebito = itemXpo.SaldoAnteriorDebito + OperacionXpo
                        Else
                            itemXpo.NuevoSaldoCredito = itemXpo.SaldoAnteriorCredito + (Math.Abs(OperacionXpo))
                        End If
                    Else
                        itemXpo.SaldoAnteriorCredito = Math.Abs(ValueBalance)
                        If OperacionXpo > 0 Then
                            itemXpo.NuevoSaldoDebito = itemXpo.SaldoAnteriorDebito + OperacionXpo
                        Else
                            itemXpo.NuevoSaldoCredito = itemXpo.SaldoAnteriorCredito + (Math.Abs(OperacionXpo))
                        End If
                    End If
                Else ' Si no hay acumulados de la cuenta 
                    itemXpo.SaldoAnteriorDebito = 0
                    itemXpo.SaldoAnteriorCredito = 0
                    If OperacionXpo > 0 Then
                        itemXpo.NuevoSaldoDebito = itemXpo.SaldoAnteriorDebito + OperacionXpo
                    Else
                        itemXpo.NuevoSaldoCredito = itemXpo.SaldoAnteriorCredito + (Math.Abs(OperacionXpo))
                    End If
                End If
                dictionaryBalance.Add(itemXpo.IdMainAccount.Id, OperacionXpo)
            End If

        Next

        'si selecciona listar cuentas en 0
        If INDAccountZero = True Then
            'definir criteria lista cuentas en 0
            Dim criteriaAccount As String = Nothing
            criteriaAccount = "AllowsMovement = 1"
            resulXpoAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount)

            For Each itemXpoAccount In resulXpoAccount
                Dim ResultAccumulate = (result.Where(Function(x) x.IdMainAccount.Id = itemXpoAccount.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim ValueId As Integer
                    If result.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = result.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(itemXpoAccount.Session)
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = itemXpoAccount
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    ResultGeneralBalance.SaldoAnteriorDebito = 0
                    ResultGeneralBalance.SaldoAnteriorCredito = 0
                    ResultGeneralBalance.NuevoSaldoDebito = 0
                    ResultGeneralBalance.NuevoSaldoCredito = 0
                    result.Add(ResultGeneralBalance)
                End If
            Next
        End If

        Return result
        'End Using


    End Function

#Region "Listar Saldo Anterior Informe libro mayor y balance"

    ''' <summary>
    ''' Listar Saldo Anterior Informe libro mayor y balance
    ''' </summary>
    Public Function ListGetGeneralBalanceLedgerAndBalance(ByVal MonthPreviousBalance As Integer, ByVal YearPreviousBalance As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As New XPQuery(Of AccountClassXpo)(session)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                 Where T1.Month >= 1 And T1.Month <= MonthPreviousBalance And T1.Year = YearPreviousBalance And MA.LegalBookId.Id = INDBookId
                                 Group T1 By T1.IdMainAccount, MAC.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                 Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using

    End Function

    ''' <summary>
    ''' Listar Saldo Anterior Informe libro mayor y balance Mes de Cierre
    ''' </summary>
    Public Function ListGetGeneralBalanceLedgerAndBalanceMonthClosure(ByVal YearPreviousBalance As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim session As New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As New XPQuery(Of GeneralLedgerBalanceXpo)(session)
        Dim tableMainAccount As New XPQuery(Of MainAccountsXpo)(session)
        Dim tableMainAccountClass As New XPQuery(Of AccountClassXpo)(session)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                 Where T1.Month = 14 And T1.Year = YearPreviousBalance And MA.LegalBookId.Id = INDBookId
                                 Group T1 By T1.IdMainAccount, MAC.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                 Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
        'End Using

    End Function

#End Region

#End Region

#Region "Inventory And Balance"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionInventoryAndBalance(ByVal INDPeriod As Date, ByVal INDAccountZero As Boolean, ByVal INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim result As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)
        Dim session As New Session(XpoDefault.DataLayer)
        Dim INDMonthMovement As Integer = Month(INDPeriod)
        Dim INDYearMovement As Integer = Year(INDPeriod)
        'Definir Criteria
        Dim criteria As String = Nothing
        criteria = "Month = " & INDMonthMovement & "AND Year =" & INDYearMovement & " AND IdMainAccount.LegalBookId.Id = " & INDBookId

        'cargar datasource
        result = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria, session)


        'si selecciona listar cuentas en 0
        If INDAccountZero = True Then
            'definir criteria lista cuentas en 0
            Dim criteriaAccount As String = Nothing
            criteriaAccount = "AllowsMovement = 1 AND LegalBookId.Id = " & INDBookId
            resulXpoAccount = Me.LoadCollection(Of MainAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaAccount, session)

            For Each itemXpoAccount In resulXpoAccount
                Dim ResultAccumulate = (result.Where(Function(x) x.IdMainAccount.Id = itemXpoAccount.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim ValueId As Integer
                    If result.Count < 1 Then
                        ValueId = 1
                    Else
                        ValueId = result.Max(Function(x) x.Id)
                    End If
                    Dim ResultGeneralBalance As New GeneralLedgerBalanceXpo(itemXpoAccount.Session)
                    ResultGeneralBalance.Id = ValueId
                    ResultGeneralBalance.IdMainAccount = itemXpoAccount
                    ResultGeneralBalance.CreditValue = 0
                    ResultGeneralBalance.DebitValue = 0
                    result.Add(ResultGeneralBalance)
                End If
            Next
        End If

        Return result
        'End Using

    End Function

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class