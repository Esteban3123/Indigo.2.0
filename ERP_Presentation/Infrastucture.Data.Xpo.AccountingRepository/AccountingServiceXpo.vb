'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.AccountingRepository
' Author           : Juan F. Tamayo
' Created          : 2014-01-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports DevExpress.Data.PLinq
Imports System.Threading
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class AccountingServiceXpo
    Inherits XpoBaseService

#Region "Fields"

    ''' <summary>
    ''' uri donde estan localizado los servicios xpo
    ''' </summary>
    Dim uriServiceEntitiesXpo As String

    ''' <summary>
    ''' protocolo utilizado para los servicios xpo
    ''' </summary>
    Dim protocolServicesXpo As Protocol

    ''' <summary>
    ''' Variable de Tipo Consultas asincronas de xpo
    ''' </summary>
    Dim serverMode As XPInstantFeedbackSource

    ''' <summary>
    ''' variable que contiene el mapeo especifo por entidad para realizar la consulta mediante xpo
    ''' </summary>
    Dim classEntity As XPClassInfo

    Dim serverCollection As XPServerCollectionSource

#End Region

#Region "Builders"

    ''' <summary>
    ''' Incializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New(Company As String)
        'verifico que exista el archivo
        ReadConfiguration()
        'establezclo la capa de datos para XPO
        XpoDefault.DataLayer = New SimpleDataLayer(New WCFServiceDataStore(GetEndPoint, GetRemoteAddress, Company))
    End Sub

#End Region

#Region "Private Methods"

    ''' <summary>
    ''' metodo necesario para leer la configuracion xml de la aplicacion
    ''' </summary>
    Private Sub ReadConfiguration()
        uriServiceEntitiesXpo = ConfigurationFile.Instance.UrlXpoWebServer
        'cargo el protocolo
        protocolServicesXpo = ConfigurationFile.Instance.ProtocolUrlXpoWebServer
    End Sub

    ''' <summary>
    ''' funcion para contatenar el nombre del endpoint por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre de la configuracion del Endpoint Correspondiente</returns>
    Private Function GetEndPoint() As String
        Return System.String.Format("{0}_Endpoint", [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

    ''' <summary>
    ''' funcion para contatenar el remoteaddress por cada protocolo
    ''' </summary>
    ''' <returns>El Nombre del remoteaddress del Endpoint Correspondiente</returns>
    Private Function GetRemoteAddress() As String
        Return System.String.Format("{0}XpoGate.svc/{1}", uriServiceEntitiesXpo, [Enum].GetName(GetType(Protocol), protocolServicesXpo))
    End Function

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListBook() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(BookXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(BookXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatusAndOfficialBook(status As Boolean, OfficialBook As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " And OfficialBook=" & OfficialBook)
        classEntity = sessionNew.GetClassInfo(GetType(BookXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Description;TypeBook;Status;StatusName;TypeBookName;CodeName;OfficialBook", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookByStatusXpCollection(status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BookXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListBookJournalVoucherHomologations(BookId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 and Id <>" & BookId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(BookXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento por estado
    ''' </summary>
    Public Function ListVieBotByForm(Form As String) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Form = '" & Form & "'")
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(VieBotXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los comprobantes por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherReportFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(JournalVouchersXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Consecutive;VoucherDate;Detail;IdJournalVoucher.Name", criteria)
        serverMode.DefaultSorting = "Consecutive"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function GetLastClosingDate() As Date
        Dim LastClosingDate As Date
        Dim collect As XPCollection(Of GeneralLedgerCompanySettingsXpo) = New XPCollection(Of GeneralLedgerCompanySettingsXpo)
        If collect.ToList().Count() > 0 Then
            LastClosingDate = collect.Max(Function(x) x.LastClosingDate)
        End If

        Return LastClosingDate
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListDocumentTypes(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(DocumentTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Consecutive;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListAccountClass() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AccountClassXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListAccountLevel() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AccountLevelXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Level;Length", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListRetentionConcept() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(RetentionConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los conceptos de retencion por tipo de retencion
    ''' </summary>
    Public Function ListRetentionConceptByTypeRetention(retention As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " AND Retention=" & retention)
        classEntity = sessionNew.GetClassInfo(GetType(RetentionConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListStatementFolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(StatementFolioXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPatrimonialPart() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PatrimonialPartXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Part", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos centros de costos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCostcenterReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CostCenterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;State", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonThirdPartyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;ContributionType;RetentionType;PersonType;State", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsReport() As XPCollection(Of MainAccountsXpo)
        Dim collect As XPCollection(Of MainAccountsXpo) = New XPCollection(Of MainAccountsXpo)
        Return collect
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListMainAccountsByStatusAndBookId(status As Boolean, bookId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & " AND LegalBookId.Id = " & bookId)
        classEntity = sessionNew.GetClassInfo(GetType(MainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable
    ''' </summary>
    ''' <param name="journalVourcherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherDetailsByJournalVoucherId(journalVourcherId As Integer) As XPCollection(Of JournalVoucherDetailsXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdAccounting.Id= " & journalVourcherId)
        Dim collect As XPCollection(Of JournalVoucherDetailsXpo) = New XPCollection(Of JournalVoucherDetailsXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista los detalles del comprobante contable cuando se hizo por medio de importacion de archivo de excel
    ''' </summary>
    ''' <param name="journalVourcherId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourcherDetailsImportedByJournalVoucherId(journalVourcherId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdAccounting.Id= " & journalVourcherId)
        classEntity = sessionNew.GetClassInfo(GetType(JournalVoucherDetailsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdAccounting.Id;IdMainAccount;IdMainAccount.NumberName;IdThirdParty;IdThirdParty.NitName;IdCostCenter;IdCostCenter.CodeName;DebitValue;CreditValue", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista todas las cuentas por level
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevelReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AllowsMovement = 1")
        classEntity = sessionNew.GetClassInfo(GetType(MainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por level y que manejen terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevelHandlesThirdReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AllowsMovement = 1 And HandlesThirdParty = 1")
        classEntity = sessionNew.GetClassInfo(GetType(MainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas por level y que sean de patrimonio
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevelPatrimonial() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AllowsMovement = 1 And IdAccountClass.Patrimony = 1")
        classEntity = sessionNew.GetClassInfo(GetType(MainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status;RetencionType;AllowsMovement", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsForSearch(LegalBookId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId=" & LegalBookId)
        classEntity = sessionNew.GetClassInfo(GetType(MainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;LegalBookId", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los tipos de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTypeVoucherRepor() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(DocumentTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(JournalVoucherXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Consecutive;VoucherDate;Detail;IdJournalVoucher.Name", Nothing)
        serverMode.DefaultSorting = "Consecutive"
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista los comprobantes con homologaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ListJournalVourcherHomologations(AccountingMovementId As Integer, journalVoucherId As Integer) As XPCollection(Of JournalVouchersXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AccountingMovementId=" & AccountingMovementId & "and Id <> " & journalVoucherId)
        Dim collect As XPCollection(Of JournalVouchersXpo) = New XPCollection(Of JournalVouchersXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los comprobantes con homologaciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ListJournalVourcherMassiveConfirm() As XPCollection(Of JournalVouchersMassiveConfirmXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim collect As XPCollection(Of JournalVouchersMassiveConfirmXpo) = New XPCollection(Of JournalVouchersMassiveConfirmXpo)(sessionNew, criteria)
        Return collect
    End Function



    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccounts(LegalBookId As Integer) As XPServerCollectionSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId.Id=" & LegalBookId)
        Dim unidad As New UnitOfWork
        unidad.AutoCreateOption = DB.AutoCreateOption.SchemaAlreadyExists
        unidad.ConnectionString = sessionNew.ConnectionString
        Dim XpServerCollectionSource1 = New XPServerCollectionSource(unidad, GetType(PUCServiceXpo), criteria)
        Return XpServerCollectionSource1
    End Function

    ''' <summary>
    ''' lista las cuentas contables que tengan asociado el libro oficial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewHomologationAccountByLegalBookId(LegalBookId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId=" & LegalBookId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewHomologationAccountXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista las cuentas contables que tenga el libro oficial y el libro de homologacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewHomologationAccountByLegalBookIdAndHomologationLegalBookId(LegalBookId As Integer, HomologationLegalBookId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId=" & LegalBookId & " And HomologationLegalBookId=" & HomologationLegalBookId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewHomologationAccountXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountsByLegalBookAndAllowMovementAndStatus(LegalBookId As Integer, AllowMovement As Boolean, Status As Boolean) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId.Id=" & LegalBookId & " And Status=" & Status & " And AllowsMovement=" & AllowMovement)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(PUCServiceXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountsByLegalBookAndAllowMovementAndStatusForSearch(LegalBookId As Integer, AllowMovement As Boolean, Status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId.Id=" & LegalBookId & " And Status=" & Status & " And AllowsMovement=" & AllowMovement)
        classEntity = sessionNew.GetClassInfo(GetType(PUCServiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountsByLegalBookId(LegalBookId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId.Id=" & LegalBookId)
        classEntity = sessionNew.GetClassInfo(GetType(PUCServiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las definiciones de tarifa
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAccountById(Id As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & Id)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(PUCServiceXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsCostCenter(handlesCostCenter As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("AllowsMovement=1 And Status=1 and HandlesCostCenter=" & handlesCostCenter)
        classEntity = sessionNew.GetClassInfo(GetType(PUCServiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel;AllowsMovement", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByLevel(ByVal level As Integer, ByVal Status As Boolean, Optional legalBookId As Integer = 0) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If legalBookId = 0 Then
            criteria = CriteriaOperator.Parse("AllowsMovement=1 And Status=" & Status & " and LegalBookId.OfficialBook=1")
        Else
            criteria = CriteriaOperator.Parse("AllowsMovement=1 And Status=" & Status & " and LegalBookId.Id=" & legalBookId)
        End If

        classEntity = sessionNew.GetClassInfo(GetType(PUCServiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las participaciones patrimoniales
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByRetentionAndFreelancerCategory(ByVal TypeRetention As Integer, ByVal FreelancerCategory As Boolean, ByVal Status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("RetencionType=" & TypeRetention & " And FreelancerCategory=" & FreelancerCategory & " And Status=" & Status & "")
        classEntity = sessionNew.GetClassInfo(GetType(PUCServiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista toda las cuentas contable si se necesitan solo con retencion o todas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountsByRetention(ByVal retention As Boolean, ByVal Status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If retention = True Then
            criteria = CriteriaOperator.Parse("AllowsMovement= 1  And  Status=" & Status & " and LegalBookId.OfficialBook = true")
        Else
            criteria = CriteriaOperator.Parse("AllowsMovement= 1 And HandlesCostCenter= false AND RetencionType = 0 And Status=" & Status & " and LegalBookId.OfficialBook = true")
        End If
        classEntity = sessionNew.GetClassInfo(GetType(PUCServiceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;IdParent;HandlesThirdParty;HandlesCostCenter;RetencionType;NumberName;IdAccountLevel;AllowsMovement", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListRetentionConceptByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(RetentionConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;MinBase;Rate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas los tipos de documento
    ''' </summary>
    Public Function ListJournalVoucherByState(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state)
        classEntity = sessionNew.GetClassInfo(GetType(DocumentTypeXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Consecutive;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' funcion para consultar todos los comprabantes contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListJournalVourchers(legalBookId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("LegalBookId.Id=" & legalBookId)

        classEntity = sessionNew.GetClassInfo(GetType(JournalVouchersXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Consecutive;IdJournalVoucher.Name;VoucherDate;Detail;Status;EntityName;EntityCode", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listad todos los meses 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMonth(ByVal Year As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Year=" & Year & "")
        classEntity = sessionNew.GetClassInfo(GetType(ClosedMonthXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Year;Month;Status", criteria)
        Return serverMode
    End Function

#End Region

#Region "MainAccountsHandlesCostCenterAndRetention"

    Private WithEvents vlinqMainAccountsHandlesCostCenterAndRetention As New LinqInstantFeedbackSource
    Private _retentionType As Integer
    Private _status As Boolean
    Private _handlesCostCenter As Boolean
    Private _level As Integer

    Public Function ListMainAccountsHandlesCostCenterAndRetention(level As Integer, retentionType As Integer, handlesCostCenter As Boolean, status As Boolean)
        _level = level
        _retentionType = retentionType
        _status = status
        _handlesCostCenter = handlesCostCenter
        Return vlinqMainAccountsHandlesCostCenterAndRetention
    End Function

    Private Sub OnGetQueryableCashReceiptsByRetentionType(sender As Object, e As GetQueryableEventArgs) Handles vlinqMainAccountsHandlesCostCenterAndRetention.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing

            TmpQueryableSource = From ma In tableMainAccount Where ma.AllowsMovement = 1 And ma.RetencionType = _retentionType And ma.Status = _status And ma.HandlesCostCenter = _handlesCostCenter Select ma

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableMainAccount
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceiptsByRetentionType(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqMainAccountsHandlesCostCenterAndRetention.DismissQueryable
        Try
            'Dispose of the DataContext 
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
    Public Function GetCollectionAccountThird(ByVal INDStatus As Integer, ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, INDBookId As Integer) As List(Of JournalVoucherDetailsXpo)
        Dim resulXpo As IList(Of JournalVoucherDetailsXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(IdAccounting.VoucherDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(IdAccounting.VoucherDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#  AND IdAccounting.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountIni <> 0 And INDAccountEnd <> 0 Then
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

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralBalanceAccountThirdMonth13(13, INDAno, INDBookId)
        Else
            resulPlinq = Me.ListGetGeneralBalanceAccountThirdMonth13(13, INDAno - 1, INDBookId)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceAccountThird(INDMes, INDAno, INDBookId)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq3.Source
            Else
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdTercero = itemPlinq3.IdTercero And e.IdCuenta = itemPlinq3.IdCuenta Select e).ToList
                    For Each itemPlinq In listFilter3
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                    Next
                Next
            End If
        End If
        Dim INDDia As Integer
        INDDia = Day(INDFechaIni)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorAccountThird(INDFechaInicio, INDFechaFin, INDBookId)

            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq2.Source
            Else
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdTercero = itemPlinq2.IdTercero And e.IdCuenta = itemPlinq2.IdCuenta Select e).ToList
                    For Each itemPlinq In listFilter2
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                    Next
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id).ThenBy(Function(x) If(IsNothing(x.IdThirdParty) = True, 0, x.IdThirdParty.Id)).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultBalanceAccount = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id Group e By e.IdCuenta Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta}).SingleOrDefault
            Dim resultAccumulate = (From e In resulPlinq.Source Where e.IdTercero = If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) And e.IdCuenta = itemXpo.IdMainAccount.Id Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
                If resultBalanceAccount IsNot Nothing Then
                    itemXpo.SaldoAnteriorPrimerFiltro = (resultBalanceAccount.sumaDebito - resultBalanceAccount.SumaCredito)
                Else
                    itemXpo.SaldoAnteriorPrimerFiltro = 0
                End If
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                If resultBalanceAccount IsNot Nothing Then
                    itemXpo.SaldoAnteriorPrimerFiltro = (resultBalanceAccount.SumaCredito - resultBalanceAccount.sumaDebito)
                Else
                    itemXpo.SaldoAnteriorPrimerFiltro = 0
                End If
            End If
            'añadimos el nuevo saldo del 1 filtro en este caso la cuenta
            If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                dictionaryBalance(itemXpo.IdMainAccount.Id) += OperacionXpo
                itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + dictionaryBalance(itemXpo.IdMainAccount.Id)
            Else
                dictionaryBalance.Add(itemXpo.IdMainAccount.Id, OperacionXpo)
                itemXpo.NuevoSaldoPrimerFiltro = itemXpo.SaldoAnteriorPrimerFiltro + dictionaryBalance(itemXpo.IdMainAccount.Id)
            End If
            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.sumaDebito - item.SumaCredito)
                Else
                    OperacionLinq = (item.SumaCredito - item.sumaDebito)
                End If
                ' añadimos el saldo anterior y nuevo saldo del segundo filtro en este caso el tercero
                If dictionaryBalance.ContainsKey(item.IdCuenta & "-" & item.IdTercero) Then
                    dictionaryBalance(item.IdCuenta & "-" & item.IdTercero) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdCuenta & "-" & item.IdTercero)
                Else
                    dictionaryBalance.Add(item.IdCuenta & "-" & item.IdTercero, OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdCuenta & "-" & item.IdTercero)
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id)) Then
                    dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id)) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id))
                Else
                    dictionaryBalance.Add(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id), OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
                itemXpo.SaldoCuenta = 0
            End If

        Next
        Dim result = resulXpo
        Return result
    End Function

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountThird(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                          Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountThirdMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                          Where T1.Month = INDMes And T1.Year = INDAno And MA.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetSaldoAnteriorAccountThird(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(sessionNew)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                    Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                    Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId
                                    Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                    Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
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
        If INDAccountIni <> 0 And INDAccountEnd <> 0 Then
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

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralBalanceAccountCostCenterMonth13(13, INDAno, INDBookId)
        Else
            resulPlinq = Me.ListGetGeneralBalanceAccountCostCenterMonth13(13, INDAno - 1, INDBookId)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceAccountCostCenter(INDMes, INDAno, INDBookId)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq3.Source
            Else
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCostCenter = itemPlinq3.IdCostCenter And e.IdCuenta = itemPlinq3.IdCuenta Select e).ToList
                    For Each itemPlinq In listFilter3
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                    Next
                Next
            End If
        End If
        Dim INDDia As Integer
        INDDia = Day(INDFechaIni)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorAccountCostCenter(INDFechaInicio, INDFechaFin, INDBookId)

            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq2.Source
            Else
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdCostCenter = itemPlinq2.IdCostCenter And e.IdCuenta = itemPlinq2.IdCuenta Select e).ToList
                    For Each itemPlinq In listFilter2
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                    Next
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id).ThenBy(Function(x) If(IsNothing(x.IdCostCenter) = True, 0, x.IdCostCenter.Id)).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulPlinq.Source Where e.IdCostCenter = If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id) And e.IdCuenta = itemXpo.IdMainAccount.Id Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
            End If
            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.sumaDebito - item.SumaCredito)
                Else
                    OperacionLinq = (item.SumaCredito - item.sumaDebito)
                End If
                If dictionaryBalance.ContainsKey(item.IdCuenta & "-" & item.IdCostCenter) Then
                    dictionaryBalance(item.IdCuenta & "-" & item.IdCostCenter) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdCuenta & "-" & item.IdCostCenter)
                Else
                    dictionaryBalance.Add(item.IdCuenta & "-" & item.IdCostCenter, OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdCuenta & "-" & item.IdCostCenter)
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) Then
                    dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id))
                Else
                    dictionaryBalance.Add(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id), OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
                itemXpo.SaldoCuenta = 0
            End If

        Next

        'Dim lock As New ReaderWriterLockSlim()
        'Parallel.ForEach(resulXpo, Sub(d As Infrastructure.Data.Xpo.AccountingRepository.JournalVoucherDetailsXpo)
        '                               lock.EnterWriteLock()
        '                               Try
        '                                   If d.IdAccounting.EntityCode Is Nothing Then
        '                                       d.IdAccounting.ACEntityCodeName = d.IdAccounting.EntityCode & " - " & ResourceManager.GetString("AC_JournalVouchers", "Reports")
        '                                   Else
        '                                       d.IdAccounting.ACEntityCodeName = d.IdAccounting.EntityCode & " - " & ResourceManager.GetString("AC_" & d.IdAccounting.EntityName, "Reports")
        '                                   End If

        '                               Finally
        '                                   lock.ExitWriteLock()
        '                               End Try
        '                           End Sub)
        'lock.Dispose()

        'Dim result = resulXpo
        Return resulXpo
    End Function

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountCostCenter(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                          Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta del mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountCostCenterMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                          Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetSaldoAnteriorAccountCostCenter(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(sessionNew)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                    Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                    Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId
                                    Group T1 By T1.IdCostCenter, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                    Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
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
        If INDAccountIni <> 0 And INDAccountEnd <> 0 Then
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

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralBalanceThirdPartyCostCenterMonth13(13, INDAno, INDBookId)
        Else
            resulPlinq = Me.ListGetGeneralBalanceThirdPartyCostCenterMonth13(13, INDAno - 1, INDBookId)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceThirdPartyCostCenter(INDMes, INDAno, INDBookId)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq3.Source
            Else
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCostCenter = itemPlinq3.IdCostCenter And e.IdThirdParty = itemPlinq3.IdThirdParty Select e).ToList
                    For Each itemPlinq In listFilter3
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                    Next
                Next
            End If
        End If
        Dim INDDia As Integer
        INDDia = Day(INDFechaIni)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorThirdPartyCostCenter(INDFechaInicio, INDFechaFin, INDBookId)

            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq2.Source
            Else
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdCostCenter = itemPlinq2.IdCostCenter And e.IdThirdParty = itemPlinq2.IdThirdParty Select e).ToList
                    For Each itemPlinq In listFilter2
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                    Next
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) If(IsNothing(x.IdThirdParty) = True, 0, x.IdThirdParty.Id)).ThenBy(Function(x) If(IsNothing(x.IdCostCenter) = True, 0, x.IdCostCenter.Id)).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulPlinq.Source Where e.IdCostCenter = If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id) And e.IdThirdParty = If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If item.Nature = 1 Then
                    OperacionLinq = (item.sumaDebito - item.SumaCredito)
                Else
                    OperacionLinq = (item.SumaCredito - item.sumaDebito)
                End If
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
                Else
                    OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
                End If
                If dictionaryBalance.ContainsKey(item.IdThirdParty & "-" & item.IdCostCenter) Then
                    dictionaryBalance(item.IdThirdParty & "-" & item.IdCostCenter) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdThirdParty & "-" & item.IdCostCenter)
                Else
                    dictionaryBalance.Add(item.IdThirdParty & "-" & item.IdCostCenter, OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdThirdParty & "-" & item.IdCostCenter)
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                If dictionaryBalance.ContainsKey(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) Then
                    dictionaryBalance(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id))
                Else
                    dictionaryBalance.Add(If(IsNothing(itemXpo.IdThirdParty) = True, 0, itemXpo.IdThirdParty.Id) & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id), OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
                itemXpo.SaldoCuenta = 0
            End If

        Next
        Dim result = resulXpo
        Return result
    End Function

#Region "Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero"

    ''' <summary>
    ''' Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero
    ''' </summary>
    Public Function ListGetGeneralBalanceThirdPartyCostCenter(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                 Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                          Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe tercero - Centro de Costo y Centro de Costo - tercero
    ''' </summary>
    Public Function ListGetGeneralBalanceThirdPartyCostCenterMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join T2 In tableMainAccount On T2.Id Equals T1.IdMainAccount.Id
                                 Join T3 In tableMainAccountClass On T3.Id Equals T2.IdAccountClass.Id
                          Where T1.Month = INDMes And T1.Year = INDAno And T2.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdCostCenter, T1.IdThirdParty, T3.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe tercero - Centro de Costo y Centro de Costo - tercero"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe tercero - Centro de Costo y Centro de Costo - tercero
    ''' </summary>
    Public Function ListGetSaldoAnteriorThirdPartyCostCenter(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(sessionNew)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                    Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                    Join T3 In tableMainAccount On T3.Id Equals T1.IdMainAccount.Id
                                    Join T4 In tableMainAccountClass On T4.Id Equals T3.IdAccountClass.Id
                                    Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId
                                    Group T1 By T1.IdCostCenter, T1.IdThirdParty, T4.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                    Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCostCenter = IdCostCenter.Id, .IdThirdParty = IdThirdParty.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
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
        If INDAccountIni <> 0 And INDAccountEnd <> 0 Then
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

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralBalanceAccountDateMonth13(13, INDAno, INDBookId)
        Else
            resulPlinq = Me.ListGetGeneralBalanceAccountDateMonth13(13, INDAno - 1, INDBookId)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceAccountDate(INDMes, INDAno, INDBookId)
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
        Dim INDDia As Integer
        INDDia = Day(INDFechaIni)

        'consultar el saldo anterior cuando seleccionan una fecha Mayor al 1 dia del mes
        If INDDia > 1 Then
            Dim resulPlinq2 As PLinqServerModeSource
            Dim INDFechaInicio As Date = New Date(INDFechaIni.Year, INDFechaIni.Month, 1)
            Dim INDFechaFin As Date = DateAdd(DateInterval.Day, -1, INDFechaIni)
            resulPlinq2 = Me.ListGetSaldoAnteriorAccountDate(INDFechaInicio, INDFechaFin, INDBookId)

            'sumar el saldo anterior cuando la fecha inicial seleccionada es mayor al 1 dia del mes
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq2.Source
            Else
                For Each itemPlinq2 In resulPlinq2.Source
                    Dim listFilter2 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq2.IdCuenta Select e).ToList
                    For Each itemPlinq In listFilter2
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq2.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq2.SumaCredito)
                    Next
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de movimientos
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id).ThenBy(Function(x) x.IdAccounting.VoucherDate)
            Dim resultAccumulate = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id Select e).ToList
            'añadir el acronimo de la entidad de donde se genero el documento
            If itemXpo.IdAccounting.EntityCode IsNot Nothing AndAlso itemXpo.IdAccounting.EntityCode.Count > 0 Then
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString(itemXpo.IdAccounting.EntityName), itemXpo.IdAccounting.EntityCode)
            Else
                itemXpo.IdAccounting.EntityCodeNameText = String.Format(ResourceManager.GetString("JournalVoucher"), itemXpo.IdAccounting.Consecutive)
            End If

            Dim OperacionXpo As Long
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
            End If
            If resultAccumulate.Count > 0 Then
                Dim item = resultAccumulate.Item(0)
                Dim OperacionLinq As Long
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionLinq = (item.sumaDebito - item.SumaCredito)
                Else
                    OperacionLinq = (item.SumaCredito - item.sumaDebito)
                End If
                If dictionaryBalance.ContainsKey(item.IdCuenta) Then
                    dictionaryBalance(item.IdCuenta) += OperacionXpo
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdCuenta)
                Else
                    dictionaryBalance.Add(item.IdCuenta, OperacionXpo)
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.SaldoAnterior = itemXpo.SaldoCuenta + dictionaryBalance(item.IdCuenta)
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                If dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                    dictionaryBalance(itemXpo.IdMainAccount.Id) += OperacionXpo
                    itemXpo.SaldoAnterior = dictionaryBalance(itemXpo.IdMainAccount.Id)
                Else
                    dictionaryBalance.Add(itemXpo.IdMainAccount.Id, OperacionXpo)
                    itemXpo.SaldoAnterior = OperacionXpo
                End If
                itemXpo.SaldoCuenta = 0
            End If

        Next
        Dim result = resulXpo
        Return result
    End Function

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountDate(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                          Where T1.Month >= 1 And T1.Month <= INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceAccountDateMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                          Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta"

    ''' <summary>
    ''' Listar Sumatoria Movimientos De Dinero Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetSaldoAnteriorAccountDate(ByVal INDFechaInicio As Date, ByVal INDFechaFin As Date, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableJournalVoucherDetail As XPQuery(Of JournalVoucherDetailsXpo) = New XPQuery(Of JournalVoucherDetailsXpo)(sessionNew)
        Dim tableJournalVoucher As XPQuery(Of JournalVouchersXpo) = New XPQuery(Of JournalVouchersXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableJournalVoucherDetail
                                    Join T2 In tableJournalVoucher On T2.Id Equals T1.IdAccounting.Id
                                    Where T2.VoucherDate.Date.ToString("yyyy-MM-dd") >= INDFechaInicio And T2.VoucherDate.Date.ToString("yyyy-MM-dd") <= INDFechaFin And T2.LegalBookId.Id = INDBookId
                                    Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
                                    Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#End Region

#Region "Informe Trial Balance"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionTrialBalance(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDAccountIni As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDAccountingZero As Integer, ByVal INDDetailingThirdParty As Boolean, ByVal INDDetailingCostCenter As Boolean, ByVal INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim resulXpo As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND Month <> 13 AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountIni <> 0 And INDAccountEnd <> 0 Then
            criteria &= "AND IdMainAccount.Number >= '" & INDAccountIni & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "'"
        End If
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If

        Dim session = New Session(XpoDefault.DataLayer)

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

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralBalanceTrialBalanceMonth13(13, INDAno, INDBookId)
        Else
            resulPlinq = Me.ListGetGeneralBalanceTrialBalanceMonth13(13, INDAno - 1, INDBookId)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceTrialBalance(INDFechaIni, INDBookId)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq3.Source
            Else
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta And e.IdCostCenter = itemPlinq3.IdCostCenter And e.IdTercero = itemPlinq3.IdTercero Select e).ToList
                    For Each itemPlinq In listFilter3
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                    Next
                Next
            End If
        End If

        Dim dictionaryBalance As New Dictionary(Of String, MainAccountsXpo)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.IdMainAccount.Id)
            Dim OperacionXpo As Long
            'operacion para sacar el nuevo saldo segun la naturaleza de la cuenta
            'If INDDetailingCostCenter = False AndAlso INDDetailingThirdParty = False Then
            'cuando las cuentas no tienen terceros o centros de costos
            If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id) Then
                Dim itemBalanceAccount = (From e In resulXpo Where e.IdMainAccount.Id = itemXpo.IdMainAccount.Id Group e By e.IdMainAccount.Id Into SumaDebito = Sum(CType(e.DebitValue, Decimal)), SumaCredito = Sum(CType(e.CreditValue, Decimal)) Select New With {.sumaDebito = SumaDebito, .SumaCredito = SumaCredito}).SingleOrDefault
                If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                    OperacionXpo = (itemBalanceAccount.sumaDebito - itemBalanceAccount.SumaCredito)
                Else
                    OperacionXpo = (itemBalanceAccount.SumaCredito - itemBalanceAccount.sumaDebito)
                End If
                dictionaryBalance.Add(itemXpo.IdMainAccount.Id, itemXpo.IdMainAccount)
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
                    itemXpo.SaldoCuenta = OperacionLinq
                    itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo

                Else ' Si no hay acumulados de la cuenta y el tercero
                    itemXpo.SaldoCuenta = 0
                    itemXpo.NuevoSaldo = itemXpo.SaldoCuenta + OperacionXpo
                End If
            End If
            'Else
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
            End If

            'si selecciona detallar centros de costos
            If INDDetailingCostCenter = True Then
                If Not dictionaryBalance.ContainsKey(itemXpo.IdMainAccount.Id & "-" & If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id)) Then
                    Dim resultAccumulate = (From e In resulPlinq.Source Where e.IdCuenta = itemXpo.IdMainAccount.Id And e.IdCostCenter = If(IsNothing(itemXpo.IdCostCenter) = True, 0, itemXpo.IdCostCenter.Id) Select e).ToList
                    If resultAccumulate.Count > 0 Then
                        Dim itemResult = (From e In resultAccumulate Group e By e.IdCuenta, e.IdCostCenter Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta, .IdCostCenter = IdCostCenter}).SingleOrDefault
                        'operacion para sacar el saldo anterior segun la naturaleza de la cuenta
                        Dim OperacionLinq As Long
                        If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                            OperacionLinq = (itemResult.sumaDebito - itemResult.SumaCredito)
                        Else
                            OperacionLinq = (itemResult.SumaCredito - itemResult.sumaDebito)
                        End If

                        dictionaryBalance.Add(itemResult.IdCuenta & "-" & itemResult.IdCostCenter, itemXpo.IdMainAccount)
                        itemXpo.SaldoAnterior = OperacionLinq
                        itemXpo.NuevoSaldoCentroCosto = itemXpo.SaldoAnterior + OperacionXpo

                    Else ' Si no hay acumulados de la cuenta y el tercero
                        itemXpo.SaldoAnterior = 0
                        itemXpo.NuevoSaldoCentroCosto = itemXpo.SaldoAnterior + OperacionXpo
                    End If
                End If
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
            'End If
        Next

        If INDDetailingThirdParty = True OrElse INDDetailingCostCenter = True Then
            For Each itemNotMovement In resulPlinq.Source
                If dictionaryBalance.ContainsKey(itemNotMovement.IdCuenta) Then

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
                                ResultGeneralBalance.NuevoSaldoTercero = OperacionLinq
                                resulXpo.Add(ResultGeneralBalance)
                                dictionaryBalance.Add(itemNotMovement.IdCuenta & "-" & itemNotMovement.IdTercero, mainAccount)
                            End If
                        End If
                    End If

                    'agregamos al listado las cuentas y centros que no tienen movimientos
                    If INDDetailingCostCenter = True Then
                        If Not dictionaryBalance.ContainsKey(itemNotMovement.IdCuenta & "-" & itemNotMovement.IdCostCenter) Then
                            Dim resultAccumulateCostCenter = (From e In resulPlinq.Source Where e.IdCuenta = itemNotMovement.IdCuenta And e.IdCostCenter = itemNotMovement.IdCostCenter Select e).ToList
                            If resultAccumulateCostCenter.Count > 0 Then
                                Dim item = (From e In resultAccumulateCostCenter Group e By e.IdCuenta, e.IdCostCenter, e.Nature, e.Code, e.CostCenterName Into sumaDebito = Sum(CType(e.sumaDebito, Decimal)), SumaCredito = Sum(CType(e.SumaCredito, Decimal)) Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdCuenta, .IdCostCenter = IdCostCenter, .Nature = Nature, .Code = Code, .CostCenterName = CostCenterName}).SingleOrDefault
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
                                ResultGeneralBalance.NuevoSaldoCentroCosto = OperacionLinq
                                resulXpo.Add(ResultGeneralBalance)
                                dictionaryBalance.Add(itemNotMovement.IdCuenta & "-" & itemNotMovement.IdCostCenter, mainAccount)
                            End If
                        End If
                    End If
                End If
            Next
        End If

        Return resulXpo
    End Function

#Region "Listar Saldo Anterior Trial Balance"

    ''' <summary>
    ''' Listar Saldo Anterior Trial Balance
    ''' </summary>
    Public Function ListGetGeneralBalanceTrialBalance(ByVal INDFechaIni As Date, ByVal INDBookId As Integer) As PLinqServerModeSource


        Dim INDDateEndBalance = DateAdd(DateInterval.Month, -1, INDFechaIni)
        Dim INDMontEnd As Integer = Month(INDDateEndBalance)
        Dim INDYear As Integer = Year(INDDateEndBalance)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(sessionNew)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                                 Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                 Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
        Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdThirdParty, T1.IdMainAccount, T1.IdCostCenter, MAC.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior trial balance Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Cuenta - Tercero y Tercero - Cuenta
    ''' </summary>
    Public Function ListGetGeneralBalanceTrialBalanceMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)
        Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(sessionNew)
        Dim tableCostCenter As XPQuery(Of CostCenterXpo) = New XPQuery(Of CostCenterXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals Ma.IdAccountClass.Id
                                 Join TP In tableThirdParty On TP.Id Equals T1.IdThirdParty.Id
                                 Join CC In tableCostCenter On CC.Id Equals T1.IdCostCenter.Id
        Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount, T1.IdThirdParty, T1.IdCostCenter, MAC.Nature, TP.Nit, TP.Name, CC.Code, CostCenterName = CC.Name Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id, .IdCostCenter = IdCostCenter.Id, .Nature = Nature, .Nit = Nit, .Name = Name, .Code = Code, .CostCenterName = CostCenterName}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#End Region

#Region "Informe Balance General"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionGeneralBalance(ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDCostCenterStart As String, ByVal INDCostCenterEnd As String, ByVal INDNatureAccount As String, ByVal INDMemorandumAccounts As String, ByVal INDAccountingZero As Integer, ByVal INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim resulXpo As IList(Of GeneralLedgerBalanceXpo)
        Dim resulXpoAccount As IList(Of MainAccountsXpo)

        'Definir Criteria 
        Dim criteria As String = Nothing
        criteria &= "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "# AND Month <> 13 AND IdMainAccount.IdAccountClass.Type in (" & INDMemorandumAccounts & ") AND IdMainAccount.IdAccountClass.Nature in (" & INDNatureAccount & ") AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If

        Dim session = New Session(XpoDefault.DataLayer)
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
        Return resulXpo

    End Function

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
        criteria &= "GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate('01/' + ToStr(Month) + '/' + ToStr(Year)) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "# AND Month <> 13 AND IdMainAccount.IdAccountClass.Type in (2) AND IdMainAccount.IdAccountClass.Nature in (" & INDNatureAccount & ") AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por centro de costo
        If INDCostCenterStart IsNot Nothing And INDCostCenterEnd IsNot Nothing Then
            criteria &= "AND IdCostCenter.Code >= '" & INDCostCenterStart & "' AND IdCostCenter.Code <= '" & INDCostCenterEnd & "'"
        End If
        'filtro cuentas con centro de costo
        If INDAccountsCostCenter = True Then
            criteria &= "AND IdMainAccount.HandlesCostCenter = 1"
        End If
        Dim session = New Session(XpoDefault.DataLayer)
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
                resulPlinq = Me.ListGetGeneralResultStatusMonth13(13, INDAno, INDBookId)
            Else
                resulPlinq = Me.ListGetGeneralResultStatusMonth13(13, INDAno - 1, INDBookId)
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

    End Function

#Region "Listar Saldo Anterior Estado Resultado"

    ''' <summary>
    ''' Listar Saldo Anterior Estado Resultado
    ''' </summary>
    Public Function ListGetGeneralBalanceStatusResult(ByVal INDDateStart As Date, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim INDDateEndBalance = DateAdd(DateInterval.Month, -1, INDDateStart)
        Dim INDMontEnd As Integer = Month(INDDateEndBalance)
        Dim INDYear As Integer = Year(INDDateEndBalance)

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccounts As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccounts On MA.Id Equals T1.IdMainAccount.Id
                          Where T1.Month >= 1 And T1.Month <= INDMontEnd And T1.Year = INDYear And MA.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior Estado Resultado Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Estado Resultado Mes 13
    ''' </summary>
    Public Function ListGetGeneralResultStatusMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join Ma In tableMainAccount On Ma.Id Equals T1.IdMainAccount.Id
                          Where T1.Month = INDMes And T1.Year = INDAno And Ma.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#End Region

#Region "ThirdBalance"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionThirdBalance(ByVal INDperiod As Date, ByVal INDAccountStart As String, ByVal INDAccountEnd As String, ByVal INDThirdPartyStart As String, INDThirdPartyEnd As String, INDBookId As Integer) As List(Of GeneralLedgerBalanceXpo)
        Dim result As IList(Of GeneralLedgerBalanceXpo)

        Dim INDMonth As Integer = Month(INDperiod)
        Dim INDYear As Integer = Year(INDperiod)
        'Definir Criteria
        Dim criteria As String = Nothing
        criteria = "Month =" & INDMonth & "And Year =" & INDYear & "AND IdThirdParty is not null AND IdMainAccount.HandlesThirdParty = 1 AND IdMainAccount.LegalBookId.Id = " & INDBookId
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND IdThirdParty.Nit >= '" & INDThirdPartyStart & "' AND IdThirdParty.Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuentas
        If INDAccountStart <> 0 And INDAccountEnd <> 0 Then
            criteria &= "AND IdMainAccount.Number >= '" & INDAccountStart & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "'"
        End If

        'cargar datasource
        result = Me.LoadCollection(Of GeneralLedgerBalanceXpo)(XpoDefault.DataLayer, Nothing, criteria)


        Dim INDMes As Integer
        Dim INDFechaMesAnte = DateAdd(DateInterval.Month, -1, INDperiod)
        INDMes = Month(INDFechaMesAnte)
        Dim INDAno As Integer
        INDAno = Year(INDFechaMesAnte)

        'consultar el saldo anterior cuando seleccionan una fecha igual al 1 dia del mes
        Dim resulPlinq As PLinqServerModeSource
        If INDMes = 12 Then
            resulPlinq = Me.ListGetGeneralThirdBalanceMonth13(13, INDAno)
        Else
            resulPlinq = Me.ListGetGeneralThirdBalanceMonth13(13, INDAno - 1)
            Dim resulPlinq3 As PLinqServerModeSource
            resulPlinq3 = Me.ListGetGeneralBalanceThirdBalance(INDMes, INDAno)
            'sumar el saldo anterior del mes 13 del anterior año con el rango de meses seleccionado del actual año
            If DirectCast(resulPlinq.Source, ICollection).Count = 0 Then
                resulPlinq.Source = resulPlinq3.Source
            Else
                For Each itemPlinq3 In resulPlinq3.Source
                    Dim listFilter3 = (From e In resulPlinq.Source Where e.IdCuenta = itemPlinq3.IdCuenta And e.IdTercero = itemPlinq3.IdTercero Select e).ToList
                    For Each itemPlinq In listFilter3
                        itemPlinq.sumaDebito = (itemPlinq.sumaDebito + itemPlinq3.sumaDebito)
                        itemPlinq.SumaCredito = (itemPlinq.SumaCredito + itemPlinq3.SumaCredito)
                    Next
                Next
            End If
        End If

        'adicionar el saldo anterior a la lista de saldos actuales
        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemXpo In result.OrderBy(Function(x) x.IdMainAccount.Id)
            Dim OperacionXpo As Long
            'operacion para sacar el nuevo saldo segun la naturaleza de la cuenta
            If itemXpo.IdMainAccount.IdAccountClass.Nature = 1 Then
                OperacionXpo = (itemXpo.DebitValue - itemXpo.CreditValue)
            Else
                OperacionXpo = (itemXpo.CreditValue - itemXpo.DebitValue)
            End If
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
                If Not dictionaryBalance.ContainsKey(item.IdCuenta & "-" & If(IsNothing(item.IdTercero) = True, 0, item.IdTercero)) Then
                    dictionaryBalance.Add(item.IdCuenta & "-" & item.IdTercero, OperacionLinq)
                    itemXpo.SaldoAnterior = OperacionLinq
                    itemXpo.SaldoAnteriorDebito = item.sumaDebito
                    itemXpo.SaldoAnteriorCredito = item.SumaCredito
                    itemXpo.NuevoSaldo = itemXpo.SaldoAnterior + OperacionXpo
                End If
            Else ' Si no hay acumulados de la cuenta y el tercero
                itemXpo.SaldoAnterior = 0
                itemXpo.SaldoAnteriorDebito = 0
                itemXpo.SaldoAnteriorCredito = 0
                itemXpo.NuevoSaldo = itemXpo.SaldoAnterior + OperacionXpo
            End If
        Next

        Return result
    End Function

#Region "Listar Saldo Anterior Informe Saldo de terceros"

    ''' <summary>
    ''' Listar Saldo Anterior Informe Saldo de terceros
    ''' </summary>
    Public Function ListGetGeneralBalanceThirdBalance(ByVal MontPreviousBalance As Integer, ByVal YearPreviousBalance As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                          Where T1.Month >= 1 And T1.Month <= MontPreviousBalance And T1.Year = YearPreviousBalance And T1.IdThirdParty IsNot Nothing
                          Group T1 By T1.IdThirdParty, T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

#End Region

#Region "Listar Saldo Anterior Saldo de terceros Mes 13"

    ''' <summary>
    ''' Listar Saldo Anterior Saldo de terceros Mes 13
    ''' </summary>
    Public Function ListGetGeneralThirdBalanceMonth13(ByVal INDMes As Integer, ByVal INDAno As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                          Where T1.Month = INDMes And T1.Year = INDAno And T1.IdThirdParty IsNot Nothing
                          Group T1 By T1.IdMainAccount, T1.IdThirdParty Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdTercero = IdThirdParty.Id, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
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
        criteria = "IdMainAccount.Number >= '" & INDAccountStart & "' AND IdMainAccount.Number <= '" & INDAccountEnd & "' AND Month = 13 AND Year =" & INDLastClosingYear

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

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                          Where T1.Month >= 1 And T1.Month <= 12 And T1.Year = YearPreviousBalance
                          Group T1 By T1.IdMainAccount Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
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
        Dim session = New Session(XpoDefault.DataLayer)

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
        Dim criteriaBalancePreviousClosed As CriteriaOperator = CriteriaOperator.Parse("MonthBalance = 13 AND YearBalance = " & INDYearPreviousBalance & " AND LegalBookId = " & INDBookId)
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
                    ResultGeneralBalance.SaldoAnteriorDebito = 0
                    ResultGeneralBalance.SaldoAnteriorCredito = 0
                    ResultGeneralBalance.NuevoSaldoDebito = 0
                    ResultGeneralBalance.NuevoSaldoCredito = 0
                    result.Add(ResultGeneralBalance)
                End If
            Next
        End If

        Return result
    End Function

#Region "Listar Saldo Anterior Informe libro mayor y balance"

    ''' <summary>
    ''' Listar Saldo Anterior Informe libro mayor y balance
    ''' </summary>
    Public Function ListGetGeneralBalanceLedgerAndBalance(ByVal MonthPreviousBalance As Integer, ByVal YearPreviousBalance As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                          Where T1.Month >= 1 And T1.Month <= MonthPreviousBalance And T1.Year = YearPreviousBalance And MA.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount, MAC.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
    End Function

    ''' <summary>
    ''' Listar Saldo Anterior Informe libro mayor y balance Mes de Cierre
    ''' </summary>
    Public Function ListGetGeneralBalanceLedgerAndBalanceMonthClosure(ByVal YearPreviousBalance As Integer, ByVal INDBookId As Integer) As PLinqServerModeSource

        Dim sessionNew = New Session(XpoDefault.DataLayer)

        Dim tableGeneralBalance As XPQuery(Of GeneralLedgerBalanceXpo) = New XPQuery(Of GeneralLedgerBalanceXpo)(sessionNew)
        Dim tableMainAccount As XPQuery(Of MainAccountsXpo) = New XPQuery(Of MainAccountsXpo)(sessionNew)
        Dim tableMainAccountClass As XPQuery(Of AccountClassXpo) = New XPQuery(Of AccountClassXpo)(sessionNew)

        Dim TmpQueryableSource = From T1 In tableGeneralBalance
                                 Join MA In tableMainAccount On MA.Id Equals T1.IdMainAccount.Id
                                 Join MAC In tableMainAccountClass On MAC.Id Equals MA.IdAccountClass.Id
                          Where T1.Month = 13 And T1.Year = YearPreviousBalance And MA.LegalBookId.Id = INDBookId
                          Group T1 By T1.IdMainAccount, MAC.Nature Into sumaDebito = Sum(T1.DebitValue), SumaCredito = Sum(T1.CreditValue)
          Select New With {.sumaDebito = sumaDebito, .SumaCredito = SumaCredito, .IdCuenta = IdMainAccount.Id, .Nature = Nature}
        Dim b As New PLinqServerModeSource
        b.Source = TmpQueryableSource.ToList()
        Return b
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
        Dim session = New Session(XpoDefault.DataLayer)

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
    End Function

#End Region

#End Region

End Class