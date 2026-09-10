'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.EconomicIndicator
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
Imports Infrastructure.Data.Xpo.PortfolioRepository

#End Region

Public Class PortfolioServicesXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    Public Function GetPortfolioAccountReceivableShareById(id As Integer) As PortfolioAccountReceivableShareXpo

        Dim sessionNew = New IndigoXPOSession(Of PortfolioAccountReceivableShareXpo)()
        Return sessionNew.GetObjectByKey(Of PortfolioAccountReceivableShareXpo)(id)
    End Function

    Public Function GetPortfolioAccountReceivableAccountingById(id As Integer) As PortfolioAccountReceivableAccountingXpo

        Dim sessionNew = New IndigoXPOSession(Of PortfolioAccountReceivableAccountingXpo)()
        Return sessionNew.GetObjectByKey(Of PortfolioAccountReceivableAccountingXpo)(id)
    End Function

    Public Function GetPortfolioAdvanceById(id As Integer) As Portfolio_PortfolioAdvance
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Return session.GetObjectByKey(Of Portfolio_PortfolioAdvance)(id)
    End Function


    Public Function GetAccountReceivableById(id As Integer) As PortfolioAccountReceivableXpo
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
        Return session.GetObjectByKey(Of PortfolioAccountReceivableXpo)(id)
    End Function

    ''' <summary>
    ''' lista los documentos de control por tipo de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioControlByDocumentType(documentType As Integer) As XPCollection(Of PortfolioControlXpo)
        Dim session As New IndigoXPOSession(Of PortfolioControlXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentType=" & documentType & "")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioControlXpo))
        Return New XPCollection(Of PortfolioControlXpo)(session, criteria)

    End Function

    Public Function ListVReportListRadicatedInvoice(filtro As String) As XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo)
        Dim session As New IndigoXPOSession(Of PortfolioVReportListRadicatedInvoiceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo) = New XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo)(session, criteria)
        Return collect

    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListHardCollection() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of HardCollectionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(HardCollectionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function
    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioReclassification() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioViewPortfolioReclasificationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioViewPortfolioReclasificationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;DocumentType;StatusName;Value;InvoiceNumber;CurrencyAbbreviation", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListLawyer() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LawyerXpo)()
        Dim classEntity = session.GetClassInfo(GetType(LawyerXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ThirdPartyId.NitName;Type;Percentage;Status;TypeName;StatusName;CodeName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListLawyerActive() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of LawyerXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim classEntity = session.GetClassInfo(GetType(LawyerXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ThirdPartyId.NitName;Type;Percentage;Status;TypeName;StatusName;CodeName", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDemandStatusActive() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioDemandStatusXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=1")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioDemandStatusXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status;StatusName;CodeDescription", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioProvision() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioProvisionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioProvisionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;CourtDate;DocumentType;Status;StatusName;DocumentTypeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioReclassificationDetail(code As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioViewPortfolioReclasificationDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code='" & code & "'")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioViewPortfolioReclasificationDetailXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;AccountReceivableCode;AccoutSource;TargetAccount;Value", criteria)
        serverMode.DefaultSorting = "AccountReceivableCode"
        Return serverMode
    End Function


    ''' <summary>
    ''' lista los radicados de portfolio
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRadicateInvoice(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioRadicateInvoiceCReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioRadicateInvoiceCReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;RadicatedConsecutive;CustomerId.Nit;CustomerId.Name;DocumentDate;State", criteria)
        serverMode.DefaultSorting = "RadicatedConsecutive"
        Return serverMode
    End Function

    Public Function ListCollectionRadicateInvoice(ByVal filtro As String) As XPCollection(Of PortfolioRadicateInvoiceCReportXpo)
        Dim session As New IndigoXPOSession(Of PortfolioRadicateInvoiceCReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Return New XPCollection(Of PortfolioRadicateInvoiceCReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista radicados de portfolio sin filtro Reporte
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRadicateInvoiceReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioRadicateInvoiceCReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State != 4")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioRadicateInvoiceCReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;RadicatedConsecutive;CustomerId.Nit;CustomerId.Name;RadicatedDate;DocumentDate;State;StateName", criteria)
        serverMode.DefaultSorting = "RadicatedConsecutive"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportPortfolioByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioNoteReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioNoteReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioNoteReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioNoteReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los grupos de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCareGroupReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ContractCareGroupXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ContractCareGroupXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ContractId.ContractEntityId.Name;ContractId.ContractNumber;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountsReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralLedgerMainAccountsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(GeneralLedgerMainAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status", Nothing)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas contables por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountsReportByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of GeneralLedgerMainAccountsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(GeneralLedgerMainAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CommonThirdPartyXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;ContributionType;RetentionType;PersonType;State;PersonId.IdentificacionCityId.Name;", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las Cuentas Bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountBankReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryEntityBankAccountsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryEntityBankAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank.Name;Number;IdMainAccount.NumberName", Nothing)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los Clientes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCustomerReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonCustomerReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CommonCustomerReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;ThirdPartyId.Name;Name;ThirdPartyId.PersonId.IdentificacionCityId.Name;State;StatusName", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    Public Function ListCollectionCustomerReportPortfolio() As XPCollection(Of CommonCustomerReportXpo)
        Dim session As New IndigoXPOSession(Of CommonCustomerReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Nothing)
        Return New XPCollection(Of CommonCustomerReportXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' lista todos los Vendedores
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSellerReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonSellerReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CommonSellerReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;ThirdPartyId.Nit;ThirdPartyId.Name;ThirdPartyId.PersonId.IdentificacionCityId.Name;State", Nothing)
        serverMode.DefaultSorting = "ThirdPartyId.Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las Facturas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceReportPortfolio() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;AccountReceivableDate;ThirdPartyId.Nit;ThirdPartyId.Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del saldo inicial
    ''' </summary>
    ''' <param name="PortfolioInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListViewBudgetAllocationInitialBalances(PortfolioInitialBalanceId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewBudgetAllocationInitialBalancesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PortfolioInitialBalanceId=" & PortfolioInitialBalanceId)
        Dim collect As XPCollection = New XPCollection(session, GetType(ViewBudgetAllocationInitialBalancesXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Lista los detalles del traslado por el id de la cabecera del traslado
    ''' </summary>
    ''' <param name="PortfolioTransferId">Id de la cabecera del traslado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioTransferDetailByPortfolioTransferId(PortfolioTransferId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of ViewListPortfolioTransferDetailXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PortfolioTransferId=" & PortfolioTransferId)
        Dim collect As XPCollection = New XPCollection(session, GetType(ViewListPortfolioTransferDetailXpo), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista las cuentas por cobrar que tengan glosa
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceCxCProvisionAndDeteriorationGlosa(courtDate As Date) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAccountReceivableByPortfolioProvisionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentDate < #" & Format(courtDate, "yyyy-MM-dd") & "# AND GlosaPortfolioGlosadaId IS NOT NULL")
        Dim classEntity = session.GetClassInfo(GetType(ViewAccountReceivableByPortfolioProvisionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las cuentas por cobrar que no tengan glosa
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceCxCProvisionAndDeteriorationNotGlosa(courtDate As Date) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAccountReceivableByPortfolioProvisionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentDate < #" & Format(courtDate, "yyyy-MM-dd") & "# AND GlosaPortfolioGlosadaId IS NULL")
        Dim classEntity = session.GetClassInfo(GetType(ViewAccountReceivableByPortfolioProvisionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las cuentas por cobrar para el reporte de cartera por edades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceCxCProvisionAndDeterioration() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewAccountReceivableByPortfolioProvisionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewAccountReceivableByPortfolioProvisionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las cuentas por cobrar para el reporte de cartera por edades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceCxCProvisionAndDeteriorationByInitialRangeAndEndRange(CourtDate As String, InitialRange As Integer, EndRange As Integer) As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)
        Dim session As New IndigoXPOSession(Of ViewAccountReceivableByPortfolioProvisionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DateDiffDay(DocumentDate,#" & CourtDate & "#) >= " & InitialRange & " AND DateDiffDay(DocumentDate,#" & CourtDate & "#) <= " & EndRange)
        Dim collect As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo) = New XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)(session, criteria)
        Return collect
    End Function

    Public Function ListInvoiceCxCProvisionAndDeteriorationByInitialRangeAndEndRangeGlosa(CourtDate As String, initialRange As Integer, endRange As Integer) As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)
        Dim session As New IndigoXPOSession(Of ViewAccountReceivableByPortfolioProvisionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DateDiffDay(DocumentDate,#" & CourtDate & "#) >= " & initialRange & " AND DateDiffDay(DocumentDate,#" & CourtDate & "#) <= " & endRange & " AND GlosaPortfolioGlosadaId IS NOT NULL")
        Dim collect As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo) = New XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)(session, criteria)
        Return collect
    End Function

    Public Function ListInvoiceCxCProvisionAndDeteriorationByInitialRangeAndEndRangeNotGlosa(CourtDate As String, initialRange As Integer, endRange As Integer) As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)
        Dim session As New IndigoXPOSession(Of ViewAccountReceivableByPortfolioProvisionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DateDiffDay(DocumentDate,#" & CourtDate & "#) >= " & initialRange & " AND DateDiffDay(DocumentDate,#" & CourtDate & "#) <= " & endRange & " AND GlosaPortfolioGlosadaId IS NULL")
        Dim collect As XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo) = New XPCollection(Of ViewAccountReceivableByPortfolioProvisionXpo)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todas las Facturas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgesPortfolioByOperatingUnitId(OperatingUnitId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAgesPortfolioXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("SettingPortfolioId.OperatingUnitId = " & OperatingUnitId)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAgesPortfolioXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;InitialRange;EndRange;DeteriorationPercentage;ProvisionPercentage;SettingPortfolioId.OperatingUnitId", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioTransferReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioTransferReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioTransferReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los indicadores economicos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllInitialBalance() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioInitialBalanceXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioInitialBalanceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los saldos iniciales por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInitialBalanceByStatus(status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioInitialBalanceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioInitialBalanceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;Status;AllBudgetAssigned", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los indicadores economicos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllEconomicIndicator() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioEconomicIndicatorXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioEconomicIndicatorXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Year;Moth;FinancialInterests;LatePaymentInterest", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Busca anticipos donde:
    ''' - Condición 1: ThirdPartyId = paciente Y AdmissionNumber = ingreso actual (o nulo)
    ''' - Condición 2: ThirdPartyId = tercero responsable del folio Y AdmissionNumber 
    ''' </summary>
    ''' <param name="thirdPartyPatientId">The third party Patient identifier.</param>
    ''' <param name="ThirdPartyId">The third party identifier of the folio responsible.</param>
    ''' <param name="admission">The admission number.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByThirdPartyIdAndAdmission(thirdPartyPatientId As Integer, admission As String, Optional ThirdPartyId As Integer? = Nothing) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()

        ' Primera condición: por paciente y admisión (anticipos directamente a nombre del paciente)
        ' Incluye anticipos sin AdmissionNumber (nulo) o con el número de admisión actual
        Dim condition1 As CriteriaOperator =
        CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > 0 AND Status = 2 AND (IsNull(AdmissionNumber) OR Trim(AdmissionNumber) = ?)", thirdPartyPatientId, admission?.Trim())

        Dim finalCriteria As CriteriaOperator = condition1
        
        ' Si el parámetro opcional tiene valor y es diferente al paciente, añade la segunda condición con OR
        ' Segunda condición: anticipos del tercero responsable del folio asociados al mismo número de ingreso
        ' NOTA: Para el tercero responsable, SOLO se incluyen anticipos que tengan el AdmissionNumber del ingreso actual
        ' (no se incluyen anticipos sin AdmissionNumber para evitar postular anticipos de otros contextos)
        If ThirdPartyId.HasValue AndAlso ThirdPartyId.Value <> thirdPartyPatientId AndAlso Not String.IsNullOrWhiteSpace(admission) Then
            Dim condition2 As CriteriaOperator =
            CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > 0 AND Status = 2 AND Trim(AdmissionNumber) = ?", ThirdPartyId.Value, admission?.Trim())
            finalCriteria = New GroupOperator(GroupOperatorType.Or, condition1, condition2)
        End If

        Dim classEntity = session.GetClassInfo(GetType(Portfolio_PortfolioAdvance))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, finalCriteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets the portfolio advance by third party identifier, thirdParty Patient identifier and admission.
    ''' Busca anticipos donde ThirdPartyId = tercero responsable Y ThirdPartyBeneficiaryId = paciente Y AdmissionNumber = ingreso (o nulo)
    ''' </summary>
    ''' <param name="ThirdPartyId">The third party identifier (responsible).</param>
    ''' <param name="thirdPartyPatientId">The third party patient identifier (beneficiary).</param>
    ''' <param name="admission">The admission number.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByThirdPartyIdAndAdmissionAndThirdPartyBeneficiary(ThirdPartyId As Integer, thirdPartyPatientId As Integer, admission As String) As XPCollection
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > 0 AND Status = 2 And ThirdPartyBeneficiaryId = ? And (IsNull(AdmissionNumber) Or Trim(AdmissionNumber) = ?)", ThirdPartyId, thirdPartyPatientId, admission?.Trim())
        Dim serverMode = New XPCollection(session, GetType(Portfolio_PortfolioAdvance), criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets the portfolio advance by third party identifier, thirdParty Patient identifier and admission.
    ''' </summary>
    ''' <param name="thirdPartyId">The third party identifier.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByThirdPartyIdForFixedAmountInvoice(ThirdPartyId As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > ? AND Status = ? AND CashReceiptDetailId.IdCashReceiptConcept.IsFixedAmountInvoiceAdvance = TRUE", ThirdPartyId, 0, 2)
        Dim serverMode = New XPCollection(session, GetType(Portfolio_PortfolioAdvance), criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets the portfolio advance by third party identifier
    ''' </summary>
    ''' <param name="thirdPartyId">The third party identifier.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByThirdPartyIdAndCashReceiptStatus(thirdPartyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > 0 AND Status = 2 And IsNull(AdmissionNumber) And CashReceiptId.Status = 2", thirdPartyId)
        Dim classEntity = session.GetClassInfo(GetType(Portfolio_PortfolioAdvance))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets colletction the portfolio advance by third party identifier and admission.
    ''' </summary>
    ''' <param name="thirdPartyId">The third party identifier.</param>
    ''' <param name="admission">The admission.</param>
    ''' <returns></returns>
    Public Function GetCollectionPortfolioAdvanceByThirdPartyIdAndAdmission(thirdPartyId As Integer, admission As String) As XPCollection
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > 0 AND Status = 2 And (IsNull(AdmissionNumber) Or Trim(AdmissionNumber) = ?)", thirdPartyId, admission?.Trim())
        Dim collect As XPCollection = New XPCollection(session, GetType(Portfolio_PortfolioAdvance), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Gets collection of portfolio advances by third party patient identifier and admission,
    ''' also includes advances from the folio's third party if provided.
    ''' Busca anticipos donde:
    ''' - Condición 1: ThirdPartyId = paciente Y AdmissionNumber = ingreso actual (o nulo)
    ''' - Condición 2: ThirdPartyId = tercero responsable del folio Y AdmissionNumber = ingreso actual (requiere AdmissionNumber exacto)
    ''' </summary>
    ''' <param name="thirdPartyPatientId">The third party patient identifier.</param>
    ''' <param name="admission">The admission number.</param>
    ''' <param name="thirdPartyFolioId">Optional. The third party identifier of the folio (responsible entity).</param>
    ''' <returns>XPCollection of portfolio advances matching the criteria</returns>
    Public Function GetCollectionPortfolioAdvanceByThirdPartyIdAndAdmission(thirdPartyPatientId As Integer, admission As String, thirdPartyFolioId As Integer?) As XPCollection
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()

        ' Condición 1: Anticipos del paciente asociados al número de admisión
        ' Para el paciente: incluye anticipos sin AdmissionNumber (nulo) o con el número de admisión actual
        Dim condition1 As CriteriaOperator = CriteriaOperator.Parse(
            "ThirdPartyId = ? AND Balance > 0 AND Status = 2 AND (IsNull(AdmissionNumber) Or Trim(AdmissionNumber) = ?)",
            thirdPartyPatientId, admission?.Trim())

        Dim finalCriteria As CriteriaOperator = condition1

        ' Si se proporciona el tercero del folio (entidad responsable) y es diferente al paciente
        ' Condición 2: Anticipos del tercero responsable asociados al mismo número de ingreso
        ' NOTA: Para el tercero responsable, SOLO se incluyen anticipos que tengan el AdmissionNumber del ingreso actual
        ' (no se incluyen anticipos sin AdmissionNumber para evitar postular anticipos de otros contextos)
        If thirdPartyFolioId.HasValue AndAlso thirdPartyFolioId.Value <> thirdPartyPatientId AndAlso Not String.IsNullOrWhiteSpace(admission) Then
            Dim condition2 As CriteriaOperator = CriteriaOperator.Parse(
                "ThirdPartyId = ? AND Balance > 0 AND Status = 2 AND Trim(AdmissionNumber) = ?",
                thirdPartyFolioId.Value, admission?.Trim())
            finalCriteria = New GroupOperator(GroupOperatorType.Or, condition1, condition2)
        End If

        Dim collect As XPCollection = New XPCollection(session, GetType(Portfolio_PortfolioAdvance), finalCriteria)
        Return collect
    End Function

    Public Function GetPortfolioAdvanceWithBalance() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Balance > ? AND Status = ?", 0, 2)
        Dim classEntity = session.GetClassInfo(GetType(Portfolio_PortfolioAdvance))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets all portfolio advance by third identifier.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Public Function GetAllPortfolioAdvanceByThirdId(ByVal ThirdId As Integer, Optional CurrencyId As Integer? = Nothing, Optional idMainAccount As Integer? = Nothing) As XPCollection
        Dim session As New IndigoXPOSession(Of Portfolio_PortfolioAdvance)()
        Dim zero As Integer = 0
        Dim Dos As Integer = 2
        Dim filter As String = "ThirdPartyId=" & ThirdId & " AND Balance > " & zero & " AND Status=" & Dos
        If CurrencyId IsNot Nothing Then
            filter &= $" AND CurrencyId = {CurrencyId}"
        End If

        If idMainAccount IsNot Nothing Then
            filter &= $" AND MainAccountId.Id = {idMainAccount}"
        End If

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim collect As XPCollection = New XPCollection(session, GetType(Portfolio_PortfolioAdvance), criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Gets all portfolio Extract.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllVReportExtractAccountReceivableXpo(filtro As String) As XPCollection(Of VReportExtractAccountReceivableXpo)
        Dim session As New IndigoXPOSession(Of VReportExtractAccountReceivableXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportExtractAccountReceivableXpo) = New XPCollection(Of VReportExtractAccountReceivableXpo)(session, criteria)
        collect.Sorting.Add(New SortProperty("DocumentNumber", SortingDirection.Ascending))
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' lista los movimientos que se le hicieron a la factura
    ''' </summary>
    Public Function GetAllExtractAccountReceivableByDocumentNumber(documentNumber As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of VReportExtractAccountReceivableXpo)()
        Dim criteria = CriteriaOperator.Parse("DocumentNumber='" & documentNumber & "'")
        Dim classEntity = session.GetClassInfo(GetType(VReportExtractAccountReceivableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las retenciones que se le hicieron a la factura
    ''' </summary>
    Public Function GetAllInvoiceCustomerRetention(documentNumber As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewInvoiceCustomerRetentionXpo)()
        Dim criteria = CriteriaOperator.Parse("InvoiceNumber='" & documentNumber & "'")
        Dim classEntity = session.GetClassInfo(GetType(ViewInvoiceCustomerRetentionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllVReportSuretyFileXpo(filtro As String) As XPCollection(Of VReportSuretyFileXpo)
        Dim session As New IndigoXPOSession(Of VReportSuretyFileXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportSuretyFileXpo) = New XPCollection(Of VReportSuretyFileXpo)(session, criteria)
        Return collect
        'End Using
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de cuenta por cobrar
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de notas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioNoteConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioPortfolioNoteConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Metodo para obtener todos los conceptos de notas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioNoteConceptGeneral(NoteType As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioPortfolioNoteConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NoteType = " & NoteType)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName;NoteType", criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' Metodo para obtener todos los rangos de provicion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllProvisionRanges() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioProvisionRangesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioProvisionRangesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Initial;Final", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableDocument() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AccountReceivableDocumentXpo)()
        Dim classEntity = session.GetClassInfo(GetType(AccountReceivableDocumentXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;CustomerId;CustomerId.NitName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioTransfers() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioTransferXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioTransferXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;CustomerId;ThirdPartyId;CustomerId.NitName;ThirdPartyId.NitName;PortfolioAdvanceId.Code;ValueTotal;AdvancePaymentId.CurrencyAbbreviation;PortfolioAdvanceId.Abbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener los traslados confirmados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioTransfersConfirmed() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioTransferXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioTransferXpo))
        Dim criteria = CriteriaOperator.Parse("Status=2")
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;StatusName;CustomerId;ThirdPartyId;CustomerId.NitName;ThirdPartyId.NitName;NameCustomer;CodeNameCustomer", criteria)
        Return serverMode
    End Function

    ' ''' <summary>
    ' ''' Metodo para obtener todos los rangos de provicion
    ' ''' </summary>
    ' ''' <returns></returns>
    'Public Function ListPortfolioAccountReceivableShare(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
    '    Dim session = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator
    '    If idCostCenter = 0 Then
    '        criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2 And Balance > 0 And AccountReceivableId.ThirdPartyId=" & idThirdParty & " AND AccountReceivableId.MainAccountId=" & idMainAccount)
    '    Else
    '        criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2 And Balance > 0 And AccountReceivableId.ThirdPartyId=" & idThirdParty & " AND AccountReceivableId.MainAccountId=" & idMainAccount & " AND AccountReceivableId.CostCenterId =" & idCostCenter)
    '    End If
    '    Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
    '    Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Metodo para obtener todos los rangos de provicion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioAccountReceivableByCashReceipt(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
        Dim criteria As CriteriaOperator
        If idCostCenter = 0 Then
            criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And ThirdPartyId.Id=" & idThirdParty & " AND PortfolioAccountReceivableAccounting[Balance>0 And MainAccountId=" & idMainAccount & "]")
        Else
            criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And ThirdPartyId.Id=" & idThirdParty & " AND PortfolioAccountReceivableAccounting[Balance>0 And MainAccountId=" & idMainAccount & " AND CostCenterId =" & idCostCenter & "]")
        End If
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista facturas para dificil recaudo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillsHardCollection() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 and PortfolioStatus<>15")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista facturas con estado 2
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PortfolioAccountReceivableXpo(thirdpartyId) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
        Dim criteria As CriteriaOperator
        criteria = CriteriaOperator.Parse("ThirdPartyId.Id = " & thirdpartyId & " AND Status = 2 ")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' metodo para listar las facturas que se utilizan en notas de cuentas por cobrar
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <param name="nature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillsPortfolioNote(customerId As Integer, noteType As Integer, nature As Integer) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)
            Dim criteria As CriteriaOperator
            Dim filter As String = String.Empty
            Dim classEntity As XPClassInfo = Nothing
            If nature = 1 Then
                If noteType = 1 Then
                    filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares = 1 AND Balance > 0"
                    classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
                Else
                    filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares >1 AND Balance > 0"
                    classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
                End If
            Else
                If noteType = 1 Then
                    filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares = 1 AND Balance > 0"
                    classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
                Else
                    filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares > 1 AND Balance > 0"
                    classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
                End If
            End If
            filter &= " AND AccountReceivableId.Status = 2"
            criteria = CriteriaOperator.Parse(filter)
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
            Return serverMode
        End Using
    End Function
    ''' <summary>
    ''' metodo para listar las facturas que se utilizan en notas de cuentas por cobrar por tercero
    ''' </summary>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="nature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillsPortfolioNoteByThridParty(thirdPartyId As Integer, noteType As Integer, nature As Integer, StatusSettingPortfolioNote As Boolean) As XPInstantFeedbackSource
        Using session As New Session(XpoDefault.DataLayer)
            Dim criteria As CriteriaOperator
            Dim filter As String = If(noteType = 6, "InvoiceId IS NOT NULL AND ", String.Empty)
            Dim classEntity As XPClassInfo = Nothing
            If nature = 1 Then 'Si es debido
                If noteType = 1 OrElse noteType = 6 Then
                    filter &= "ThirdPartyId = " & thirdPartyId & IIf(StatusSettingPortfolioNote, "", "AND SpecificPortfolioStatus <> 4")
                    classEntity = session.GetClassInfo(GetType(ViewAccountReceivableAccountingByPortfolioNoteXpo))
                Else
                    filter &= "AccountReceivableId.ThirdPartyId =" & thirdPartyId & " AND AccountReceivableId.NumberShares > 1"
                    classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
                    filter &= " AND AccountReceivableId.Status = 2"
                End If
            Else
                If noteType = 1 OrElse noteType = 6 Then
                    filter &= "ThirdPartyId = " & thirdPartyId & " and Balance > 0" & IIf(StatusSettingPortfolioNote, "", "AND SpecificPortfolioStatus <> 4")
                    classEntity = session.GetClassInfo(GetType(ViewAccountReceivableAccountingByPortfolioNoteXpo))
                Else
                    filter &= "AccountReceivableId.ThirdPartyId =" & thirdPartyId & " AND AccountReceivableId.NumberShares > 1 AND Balance > 0"
                    classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
                    filter &= " AND AccountReceivableId.Status = 2"
                End If
            End If
            criteria = CriteriaOperator.Parse(filter)
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
            Return serverMode
        End Using
    End Function
    ''' <summary>
    ''' lista las facturas del tercero para traslados
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillsTransfers(ThirdPartyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableAccountingXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
        Dim criteria As CriteriaOperator
        If ThirdPartyId > 0 Then
            criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2 AND AccountReceivableId.ThirdPartyId=" & ThirdPartyId & " AND Balance > 0 and (((AccountReceivableId.AccountReceivableType = 2 And AccountReceivableId.PortfolioStatus <>  1) AND  (AccountReceivableId.AccountReceivableType = 2 AND AccountReceivableId.PortfolioStatus <> 2)) or AccountReceivableId.AccountReceivableType <> 2) ")
        Else
            'criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2  AND Balance > 0 And AccountReceivableId.PortfolioStatus <> 1 And AccountReceivableId.AccountReceivableType = 2")
            criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2  AND Balance > 0 and (((AccountReceivableId.AccountReceivableType = 2 And AccountReceivableId.PortfolioStatus <>  1) AND  (AccountReceivableId.AccountReceivableType = 2 AND AccountReceivableId.PortfolioStatus <> 2)) or AccountReceivableId.AccountReceivableType <> 2) ")
        End If
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las facturas del tercero para traslados
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ViewListBillsTransfers(ThirdPartyId As Integer, StatusSettingTranfers As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ViewPortfolioListBillsTransfersXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ViewPortfolioListBillsTransfersXpo))
        Dim criteria As CriteriaOperator
        If StatusSettingTranfers Then
            If ThirdPartyId > 0 Then
                criteria = CriteriaOperator.Parse("Status = 2 AND ThirdPartyId=" & ThirdPartyId & " AND (((AccountReceivableType = 2 And PortfolioStatus <>  1) AND  (AccountReceivableType = 2 AND PortfolioStatus <> 2)) or AccountReceivableType <> 2) ")
            Else
                criteria = CriteriaOperator.Parse("Status = 2  AND (((AccountReceivableType = 2 And PortfolioStatus <>  1) AND  (AccountReceivableType = 2 AND PortfolioStatus <> 2)) or AccountReceivableType <> 2) ")
            End If
        Else
            If ThirdPartyId > 0 Then
                criteria = CriteriaOperator.Parse("Status = 2 AND ThirdPartyId=" & ThirdPartyId & " AND (((AccountReceivableType = 2 And PortfolioStatus <>  1) AND  (AccountReceivableType = 2 AND PortfolioStatus <> 2)) or AccountReceivableType <> 2) AND  SpecificPortfolioStatus <> 4 ")
            Else
                criteria = CriteriaOperator.Parse("Status = 2  AND (((AccountReceivableType = 2 And PortfolioStatus <>  1) AND  (AccountReceivableType = 2 AND PortfolioStatus <> 2)) or AccountReceivableType <> 2) AND  SpecificPortfolioStatus <> 4 ")
            End If
        End If
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las facturas del tercero para convenios
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivableAccountingByThirdParty(thirdPartyId As Integer?) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableAccountingXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
        Dim filter = "Balance > 0 AND AccountReceivableId.Status = 2 AND AccountReceivableId.PortfolioStatus <> 1"
        If thirdPartyId > 0 Then
            filter = String.Format("{0} AND AccountReceivableId.ThirdPartyId.Id = {1}", filter, thirdPartyId)
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function


    ''' <summary>
    ''' lista las facturas por filtro
    ''' </summary>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceReportPortfolioFilter(Filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableReportXpo)()
        Dim criteria = CriteriaOperator.Parse(Filtro)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;AccountReceivableDate;ThirdPartyId.Nit;ThirdPartyId.Name", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function
    ''' <summary>
    ''' lista los anticipos por filtro
    ''' </summary>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdvancesReportPortfolioFilter(Filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAdvanceReportXpo)()
        Dim criteria = CriteriaOperator.Parse(Filtro)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAdvanceReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId.Nit;ThirdPartyId.Name;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function
    ''' <summary>
    ''' lista los traslados por filtro
    ''' </summary>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioTransferReportFilter(Filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioTransferReportXpo)()
        Dim criteria = CriteriaOperator.Parse(Filtro)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioTransferReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function
    ''' <summary>
    ''' lista los anticipos del tercero para traslados
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAdvanceTransfers(thirdPartyId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAdvanceXpo)()
        Dim criteria = CriteriaOperator.Parse("ThirdPartyId=" & thirdPartyId & " And Balance > 0 and Status = 2")
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAdvanceXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del saldo incial
    ''' </summary>
    ''' <param name="portfolioInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioInitialBalanceAccountReceivable(portfolioInitialBalanceId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioInitialBalanceAccountReceivableXpo)()
        Dim criteria = CriteriaOperator.Parse("PortfolioInitialBalanceId=" & portfolioInitialBalanceId)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioInitialBalanceAccountReceivableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles de las cuentas donde esta la factura
    ''' </summary>
    Public Function ListAccountReceivableAccounting(AccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableAccountingXpo)()
        Dim criteria = CriteriaOperator.Parse("AccountReceivableId.Id=" & AccountReceivableId)
            Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioAccountReceivableXpo(id As Integer) As PortfolioAccountReceivableXpo
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
        Return session.GetObjectByKey(Of PortfolioAccountReceivableXpo)(id)
    End Function

    ''' <summary>
    ''' lista los detalles del saldo incial de las cuentas
    ''' </summary>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioInitialBalanceAccountReceivableAccounting(portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioInitialBalanceAccountReceivableAccountingXpo)()
        Dim criteria = CriteriaOperator.Parse("PortfolioInitialBalanceAccountReceivableId.Id=" & portfolioInitialBalanceAccountReceivableId)
            Dim classEntity = session.GetClassInfo(GetType(PortfolioInitialBalanceAccountReceivableAccountingXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del saldo incial de las cuotas
    ''' </summary>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioInitialBalanceAccountReceivableShare(portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioInitialBalanceAccountReceivableShareXpo)()
        Dim criteria = CriteriaOperator.Parse("PortfolioInitialBalanceAccountReceivableId.Id=" & portfolioInitialBalanceAccountReceivableId)
            Dim classEntity = session.GetClassInfo(GetType(PortfolioInitialBalanceAccountReceivableShareXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los anticipos para utilizarlos en notas
    ''' </summary>
    ''' <param name="customerId"></param>
    ''' <param name="nature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioAdvancePortfolioNote(customerId As Integer, nature As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAdvanceXpo)()
        Dim criteria As CriteriaOperator
        If nature = 1 Then
            criteria = CriteriaOperator.Parse("ThirdPartyId=" & customerId & " and Status = 2 and Balance > 0")
        Else
            criteria = CriteriaOperator.Parse("ThirdPartyId=" & customerId & " and Status = 2")
        End If
            Dim classEntity = session.GetClassInfo(GetType(PortfolioAdvanceXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos las facturas, esto sirve para validar en recibos de cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioAccountReceivableValidation(invoiceNumbers As List(Of String)) As XPCollection
        If invoiceNumbers.Count > 0 Then
            Dim invoiceParameter As String = ""
            For x As Integer = 0 To invoiceNumbers.Count - 1 Step 1
                If invoiceNumbers.Count - 1 = x Then
                    invoiceParameter &= "'" & invoiceNumbers(x) & "'"
                Else
                    invoiceParameter &= "'" & invoiceNumbers(x) & "'" & ","
                End If
            Next
            Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
            Dim criteria As CriteriaOperator
            criteria = CriteriaOperator.Parse("InvoiceNumber in (" & invoiceParameter & ")")
            Dim collection As New XPCollection(GetType(PortfolioAccountReceivableXpo), criteria)
            Return collection
            'End Using
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' metodo para traer las notas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioNote() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioNoteXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioNoteXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Code;NoteDate;Nature;NatureName;NoteType;NoteTypeName;CustomerId.NitName;CustomerName;Status;StatusName;PortfolioTransferId.Id;PortfolioTransferId.CodeNameCustomer;Value;CurrencyId;Abbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de notas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioNoteConceptByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse("Status =" & status)
        Dim session As New IndigoXPOSession(Of PortfolioPortfolioNoteConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de notas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioNoteConceptByFilter(Optional filter As String = Nothing) As XPInstantFeedbackSource
        Dim criteria = CriteriaOperator.Parse(filter)
        Dim session As New IndigoXPOSession(Of PortfolioPortfolioNoteConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de cuentas x cobrar por el estado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableConceptByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableConceptXpo)()
        Dim criteria = CriteriaOperator.Parse("Status =" & status)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los conceptos de nota por tipo de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioNoteConceptByNoteType(noteType As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioPortfolioNoteConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NoteType=" & noteType & " And Status=" & status)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName;NoteType;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los conceptos de conciliacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListConciliationConcepts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioConciliationConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioConciliationConceptsXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ThirdPartyId.Name;StatusName", Nothing)
    End Function

    ''' <summary>
    ''' lista los  conciliacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioConciliation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioConciliationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioConciliationXpo))
        Return New XPInstantFeedbackSource(classEntity, "Id;ConciliationConsecutive;DocumentNumber;StatusName;NitName", Nothing)
    End Function

    ''' <summary>
    ''' lista las facturas en cartera
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivableByStatus(status As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
            Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;ExpiredDate;CustomerId.NitName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las facturas en cartera
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivable() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista estados de demanda
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDemandStatus() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioDemandStatusXpo)()

        Dim classEntity = session.GetClassInfo(GetType(PortfolioDemandStatusXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista estados de demanda
    ''' </summary>
    ''' <param name="filtro"></param>
    ''' <returns></returns>
    Public Function ListDemandStatusFilter(filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioDemandStatusXpo)()
        Dim criterio As CriteriaOperator = CriteriaOperator.Parse(filtro)

        Dim classEntity = session.GetClassInfo(GetType(PortfolioDemandStatusXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Status", criterio)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas por cobrar por estado en carteras
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountReceivableByPortfolioStatus(PortfolioStatus As String) As XPInstantFeedbackSource
        Dim filter As String = Nothing
        If Not String.IsNullOrEmpty(PortfolioStatus) Then
            filter = String.Format("PortfolioStatus In ({0})", PortfolioStatus)
        End If

        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableReportXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)

        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos las facturas, esto sirve para validar en recibos de cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function LoadDatasourceInvoiceTraceability(cutoffDate As DateTime, opertatingUnitIds As String, customerIds As String, invoiceIds As String, typeReport As Byte) As XPCollection(Of PortfolioViewReportInvoiceTraceability)
        Dim filter As String = "InvoiceDate <= # " & Format(cutoffDate, "yyyy-MM-dd HH:mm:ss ") & "#"

        If Not String.IsNullOrEmpty(opertatingUnitIds) Then
            filter &= String.Format(" AND OperatingUnitId IN ({0})", opertatingUnitIds)
        End If

        If Not String.IsNullOrEmpty(opertatingUnitIds) Then
            filter &= String.Format(" AND OperatingUnitId IN ({0})", opertatingUnitIds)
        End If

        If typeReport = 1 Then
            filter &= " AND PortfolioGlosadaId IS NULL"
        ElseIf typeReport = 2 Then
            filter &= " AND PortfolioGlosadaId IS NOT NULL"
        End If

        If Not String.IsNullOrEmpty(invoiceIds) Then
            filter &= String.Format(" AND AccountReceivableId IN ({0})", invoiceIds)
        End If

        If Not String.IsNullOrEmpty(customerIds) Then
            filter &= String.Format(" AND CustomerId IN ({0})", customerIds)
        End If

        Dim session As New IndigoXPOSession(Of PortfolioViewReportInvoiceTraceability)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Return New XPCollection(Of PortfolioViewReportInvoiceTraceability)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista los detalles de una factura por el id de la cuenta de cobro
    ''' </summary>
    ''' <param name="AccountReceivableId"></param>
    ''' <param name="Nature"></param>
    ''' <returns></returns>
    Public Function ListInvoiceDetailsByAccountReceivable(AccountReceivableId As Integer, Nature As Integer, Optional noteId As Integer? = Nothing) As XPCollection(Of ViewInvoiceDetailsXpo)
        Dim balanceFilter As String = If(Nature = 2, " AND Balance > 0", "")

        Dim noteFilter As String
        If noteId Is Nothing Then
            noteFilter = " AND noteId IS NULL"
        Else
            noteFilter = $" AND (noteId IS NULL OR noteId = {noteId.Value})"
        End If

        Dim filters As String =
        $"AccountReceivableId = {AccountReceivableId}{balanceFilter}{noteFilter}"

        Dim session As New IndigoXPOSession(Of ViewInvoiceDetailsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filters)
        Return New XPCollection(Of ViewInvoiceDetailsXpo)(session, criteria)
    End Function

    ''' <summary>
    ''' Lista los detalles de los radicados de factura
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioViewRadicateInvoiceDetailXpo() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioViewRadicateInvoiceDetailXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioViewRadicateInvoiceDetailXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listo los registros de clasificacion deterioro de cartera
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioDeteriorationClassification() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PortfolioDeteriorationClassificationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioDeteriorationClassificationXpo))
        Return New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
    End Function

    ''' <summary>
    ''' Listo los registros de clasificacion deterioro de cartera activos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListActivePortfolioDeteriorationClassification() As XPInstantFeedbackSource
        Dim filter As String = "Status = 1"
        Dim session As New IndigoXPOSession(Of PortfolioDeteriorationClassificationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioDeteriorationClassificationXpo))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filter)
        Return New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    End Function
#End Region

#Region "LinqInstantFeedbackSource"

#Region "ListAccountRecivableAccountByState"
    'Function ListAccountRecivableAccountByState(state As Integer) As LinqInstantFeedbackSource
    '    Throw New NotImplementedException
    'End Function
    Private _parameterStatus As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountRecivableAccountByState(status As Byte) As LinqInstantFeedbackSource
        Dim vlinqAccountRecivableByState As New LinqInstantFeedbackSource
        AddHandler vlinqAccountRecivableByState.GetQueryable, AddressOf OnGetQueryableAccountRecivableByState
        AddHandler vlinqAccountRecivableByState.DismissQueryable, AddressOf DismissQueryableAccountRecivableByState
        _parameterStatus = status
        vlinqAccountRecivableByState.KeyExpression = "Id"
        Return vlinqAccountRecivableByState
    End Function

    Private Sub OnGetQueryableAccountRecivableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim _accountRecivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(session)
            Dim _accountRecivableAccounting As XPQuery(Of PortfolioAccountReceivableAccountingXpo) = New XPQuery(Of PortfolioAccountReceivableAccountingXpo)(session)
            Dim zero As Integer = 0
            Dim tmpQueryableSource = From ara In _accountRecivableAccounting
                                     Join ar In _accountRecivable On ara.AccountReceivableId.Id Equals ar.Id
                                     Where ar.Status = _parameterStatus AndAlso ar.Balance > zero AndAlso ar.PortfolioStatus <> 1
                                     Select ara
            e.QueryableSource = tmpQueryableSource
            e.Tag = _accountRecivable
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountRecivableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "AccountRecivable"
    Private _parameterThirdId As Integer
    Private _parameterState As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountRecivableAccountByThirdIdState(thirdId As Integer, status As Byte) As LinqInstantFeedbackSource
        Dim vlinqAccountRecivable As New LinqInstantFeedbackSource
        AddHandler vlinqAccountRecivable.GetQueryable, AddressOf OnGetQueryableAccountRecivable
        AddHandler vlinqAccountRecivable.DismissQueryable, AddressOf DismissQueryableAccountRecivable
        _parameterThirdId = thirdId
        _parameterState = status
        vlinqAccountRecivable.KeyExpression = "Id"
        Return vlinqAccountRecivable
    End Function

    ''' <summary>
    ''' lista las monedas en accountreceivable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function PortfolioAccountReceivableGroupByCurrencyXpo() As Array
        Dim session As New IndigoXPOSession(Of PortfolioAccountReceivableXpo)()
        Dim classEntity = session.GetClassInfo(GetType(PortfolioAccountReceivableXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Dim _accountRecivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(session)
        Dim tmpQueryableSource = (From ar In _accountRecivable
                                  Select ar.CurrencyId).Distinct().ToArray()
        Return tmpQueryableSource
    End Function

    Private Sub OnGetQueryableAccountRecivable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim _accountRecivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(session)
            Dim _accountRecivableAccounting As XPQuery(Of PortfolioAccountReceivableAccountingXpo) = New XPQuery(Of PortfolioAccountReceivableAccountingXpo)(session)
            'Dim _accountRecivableShares As XPQuery(Of PortfolioAccountReceivableShareXpo) = New XPQuery(Of PortfolioAccountReceivableShareXpo)(session)
            Dim zero As Integer = 0

            Dim tmpQueryableSource = From ara In _accountRecivableAccounting
                                     Join ar In _accountRecivable On ara.AccountReceivableId.Id Equals ar.Id
                                     Where ar.ThirdPartyId.Id = _parameterThirdId AndAlso ar.Status = _parameterState AndAlso ar.Balance > zero AndAlso ar.PortfolioStatus <> 1
                                     Select ara

            'Dim TmpQueryableSource = From S In _supplier
            '                         Join SDL In _supplierDistributionLines On S.Id Equals SDL.IdSupplier
            '                         Join DL In _distributionLines On SDL.IdDistributionLine Equals DL.Id
            '                         Join AP In _accountPayable On AP.IdSupplier Equals S.Id
            '                         Where AP.Balance > zero And AP.Status = Dos
            '                         Select Supplier = S.Name, DistributionLine = DL.Description, Invoice = AP.BillNumber, ExpirationDate = AP.ExpirationDate, Balance = AP.Balance
            'Select New With {Key .Nombre = S.Name, Key .Linea = DL.Description, Key .Factura = AP.BillNumber, Key .Fecha = AP.ExpirationDate, Key .Edad = CalculateAgePayments(AP.ExpirationDate), Key .Saldo = AP.Balance}
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = tmpQueryableSource
            e.Tag = _accountRecivable
            'End Using

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountRecivable(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

#End Region

#Region "ListPortfolioAccountReceivableShare"
    'Private WithEvents vlinqPortfolioAccountReceivableShare As New LinqInstantFeedbackSource
    'Dim _idThirdParty As Integer
    'Dim _idMainAccount As Integer
    'Dim _idCostCenter As Integer

    'Public Function ListPortfolioAccountReceivableShare(idThirdParty As Integer, idMainAccount As Integer, Optional idCostCenter As Integer = 0) As LinqInstantFeedbackSource
    '    _idThirdParty = idThirdParty
    '    _idMainAccount = idMainAccount
    '    _idCostCenter = idCostCenter
    '    vlinqPortfolioAccountReceivableShare.KeyExpression = "Id"
    '    Return vlinqPortfolioAccountReceivableShare
    'End Function

    'Private Sub OnGetQueryablePortfolioAccountReceivableShare(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqPortfolioAccountReceivableShare.GetQueryable
    '    Try
    '        Dim session = New Session(XpoDefault.DataLayer)
    '        Dim tableAccountReceivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(session)
    '        Dim tableAccountReceivableShare As XPQuery(Of PortfolioAccountReceivableShareXpo) = New XPQuery(Of PortfolioAccountReceivableShareXpo)(session)

    '        If _idCostCenter = 0 Then
    '            Dim TmpQueryableSource = From ars In tableAccountReceivableShare
    '                                     Join ar In tableAccountReceivable On ar.Id Equals ars.AccountReceivableId
    '                                     Where ar.ThirdPartyId = _idThirdParty And ar.MainAccountId = _idMainAccount
    '                                     Select Id = ars.Id, AccountReceivableId = ars.AccountReceivableId, InvoiceNumber = ar.InvoiceNumber, Number = ars.Number, ExpiredDate = ars.ExpiredDate, Value = ars.Value, Balance = ars.Balance,
    '                                     DebitValue = ars.DebitValue, CreditValue = ars.CreditValue, TransferValue = ars.TransferValue, PaymentValue = ars.PaymentValue, InterestPaymentDate = ars.InterestPaymentDate,
    '                                     InterestValue = ars.InterestValue, SurchargesValue = ars.SurchargesValue, CapitalRepaymentAgreement = ars.CapitalRepaymentAgreement, FinancialInterest = ars.FinancialInterest,
    '                                     RepaymentAgreementInterest = ars.RepaymentAgreementInterest
    '            e.QueryableSource = TmpQueryableSource
    '        Else
    '            Dim TmpQueryableSource = From ars In tableAccountReceivableShare
    '                                     Join ar In tableAccountReceivable On ar.Id Equals ars.AccountReceivableId
    '                                     Where ar.ThirdPartyId = _idThirdParty And ar.MainAccountId = _idMainAccount And ar.CostCenterId = _idCostCenter
    '                                     Select Id = ars.Id, AccountReceivableId = ars.AccountReceivableId, InvoiceNumber = ar.InvoiceNumber, Number = ars.Number, ExpiredDate = ars.ExpiredDate, Value = ars.Value, Balance = ars.Balance,
    '                                     DebitValue = ars.DebitValue, CreditValue = ars.CreditValue, TransferValue = ars.TransferValue, PaymentValue = ars.PaymentValue, InterestPaymentDate = ars.InterestPaymentDate,
    '                                     InterestValue = ars.InterestValue, SurchargesValue = ars.SurchargesValue, CapitalRepaymentAgreement = ars.CapitalRepaymentAgreement, FinancialInterest = ars.FinancialInterest,
    '                                     RepaymentAgreementInterest = ars.RepaymentAgreementInterest

    '            e.QueryableSource = TmpQueryableSource
    '        End If
    '        e.Tag = tableAccountReceivableShare
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub DismissQueryablePaymentConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqPortfolioAccountReceivableShare.DismissQueryable
    '    Try
    '        CType(e.Tag, Object).Dispose()
    '    Catch ex As Exception
    '        ex.Message.ToString()
    '    End Try
    'End Sub
#End Region

#End Region

#Region "Reports"

    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <typeparam name="T">Objeto XPO</typeparam>
    ''' <param name="Fun">Objeto tipo Función opcional para filtrar los datos necesarios</param>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollection(Of T)(Optional Fun As Func(Of T, Boolean) = Nothing, Optional criteria As String = Nothing) As List(Of T)
        Dim result = Me.LoadCollection(Of T)(XpoDefault.DataLayer, Fun, criteria)
        result.Sort()
        Return result
    End Function

#End Region

#Region "Informe Por Cuenta - Tercero y Tercero - Cuenta"
    ''' <summary>
    ''' Función para obtener una lista de datos de una entidad XPO
    ''' </summary>
    ''' <returns>Lista tipo T</returns>
    Public Function GetCollectionCashReceipts(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDThirdPartyStart As String, ByVal INDThirdPartyEnd As String, ByVal INDBankAccountStart As String, ByVal INDBankAccountEnd As String) As List(Of VReportSuretyFileXpo)
        Dim resulXpo As IList(Of VReportSuretyFileXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        'filtro por terceros
        If INDThirdPartyStart IsNot Nothing And INDThirdPartyEnd IsNot Nothing Then
            criteria &= "AND Nit >= '" & INDThirdPartyStart & "' AND Nit <= '" & INDThirdPartyEnd & "'"
        End If
        'filtro por cuenta bancaria
        If INDBankAccountStart IsNot Nothing And INDBankAccountEnd IsNot Nothing Then
            criteria &= "AND BankAccount >= '" & INDBankAccountStart & "' AND BankAccount <= '" & INDBankAccountEnd & "'"
        End If

        resulXpo = Me.LoadCollection(Of VReportSuretyFileXpo)(XpoDefault.DataLayer, Nothing, criteria)
        Dim result = resulXpo
        Return result
    End Function

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
