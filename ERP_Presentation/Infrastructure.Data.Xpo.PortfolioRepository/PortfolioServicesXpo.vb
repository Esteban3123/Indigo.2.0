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
'Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Xpo.Base
Imports System.IO
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Linq
Imports System.Configuration
#End Region

Public Class PortfolioServicesXpo
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
    Private Sub ReadConfiguration()
        'verifico que exista el archivo
        'If File.Exists(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", FileName)) = False Then
        '    Throw New FileNotFoundException("El Archivo de Configuracion no existe!", FileName)
        'End If
        ''leo el archivo
        'Dim xmlConfiguration As New DataTable("ConfiguracionGenesis")
        'With xmlConfiguration
        '    .Columns.Add("UrlServidorWeb")
        '    .Columns.Add("UrlServidorEntidades")
        '    .Columns.Add("ProtocoloURLServidorWeb")
        '    .Columns.Add("ProtocoloURlServidorEntidades")
        '    .Columns.Add("RutaActualizacion")
        '    .Columns.Add("BuscarActulizaciones")
        '    .ReadXml(String.Concat(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "\", "Indigo Technologies", "\", "Indigo Crystal", "\", FileName))
        'End With
        'cargo la uri de servicios
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
    'serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    Public Function ListPortfolioCircularZeroThirty() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioCircular030Xpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioReclassification() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioViewPortfolioReclasificationXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;DocumentType;StatusName;Value", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioReclassificationDetail(code As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code='" & code & "'")
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioViewPortfolioReclasificationDetailXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;AccountReceivableCode;AccoutSource;TargetAccount;Value", criteria)
        serverMode.DefaultSorting = "AccountReceivableCode"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los radicados de portfolio
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRadicateInvoice(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioRadicateInvoiceCReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;RadicatedConsecutive;CustomerId.Nit;CustomerId.Name;DocumentDate;State", criteria)
        serverMode.DefaultSorting = "RadicatedConsecutive"
        Return serverMode
    End Function


    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportPortfolioByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioNoteReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las notas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioNoteReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los grupos de atención
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCareGroupReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ContractCareGroupXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;ContractId.ContractEntityId.Name;ContractId.ContractNumber;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas contables
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountsReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(GeneralLedgerMainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status", Nothing)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas contables por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListMainAccountsReportByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(GeneralLedgerMainAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Number;Name;HandlesThirdParty;HandlesCostCenter;Status", criteria)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonThirdPartyXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;ContributionType;RetentionType;PersonType;State", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos las Cuentas Bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAccountBankReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryEntityBankAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank.Name;Number;IdMainAccount.NumberName", Nothing)
        serverMode.DefaultSorting = "Number"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los Clientes
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCustomerReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonCustomerReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;ThirdPartyId.Name;Name;ThirdPartyId.PersonId.IdentificacionCityId.Name;State", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los Vendedores
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSellerReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonSellerReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;ThirdPartyId.Nit;ThirdPartyId.Name;ThirdPartyId.PersonId.IdentificacionCityId.Name;State", Nothing)
        serverMode.DefaultSorting = "ThirdPartyId.Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las Facturas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListInvoiceReportPortfolio() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;AccountReceivableDate;ThirdPartyId.Nit;ThirdPartyId.Name", Nothing)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PortfolioInitialBalanceId=" & PortfolioInitialBalanceId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewBudgetAllocationInitialBalancesXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista los detalles del traslado por el id de la cabecera del traslado
    ''' </summary>
    ''' <param name="PortfolioTransferId">Id de la cabecera del traslado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioTransferDetailByPortfolioTransferId(PortfolioTransferId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("PortfolioTransferId=" & PortfolioTransferId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ViewListPortfolioTransferDetailXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista las cuentas por cobrar para el reporte de cartera por edades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVReportListRadicatedInvoice(filtro As String) As XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo) = New XPCollection(Of PortfolioVReportListRadicatedInvoiceXpo)(sessionNew, criteria)
        'collect.Sorting.Add(New SortProperty("ServiceDate", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' lista las cuentas por cobrar para el reporte de cartera por edades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListVReportPortfolioByAge(filtro As String) As XPCollection(Of VReportPortfolioByAge)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportPortfolioByAge) = New XPCollection(Of VReportPortfolioByAge)(sessionNew, criteria)
        'collect.Sorting.Add(New SortProperty("ServiceDate", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioTransferReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioTransferReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los indicadores economicos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllInitialBalance() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioInitialBalanceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;Status", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los saldos iniciales por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetInitialBalanceByStatus(status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioInitialBalanceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Observations;Status;AllBudgetAssigned", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los indicadores economicos
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllEconomicIndicator() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioEconomicIndicatorXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Year;Moth;FinancialInterests;LatePaymentInterest", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets the portfolio advance by third party identifier and admission.
    ''' </summary>
    ''' <param name="thirdPartyId">The third party identifier.</param>
    ''' <param name="admission">The admission.</param>
    ''' <returns></returns>
    Public Function GetPortfolioAdvanceByThirdPartyIdAndAdmission(thirdPartyId As Integer, admission As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId = ? AND Balance > ? AND Status = ? And (IsNull(AdmissionNumber) Or AdmissionNumber = '" & admission & "')", thirdPartyId, 0, 2) ', admission) 'Falta la admision
        classEntity = sessionNew.GetClassInfo(GetType(Portfolio_PortfolioAdvance))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Gets all portfolio advance by third identifier.
    ''' </summary>
    ''' <param name="ThirdId">The third identifier.</param>
    ''' <returns></returns>
    Public Function GetAllPortfolioAdvanceByThirdId(ByVal ThirdId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim zero As Integer = 0
        Dim Dos As Integer = 2
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ThirdPartyId=" & ThirdId & " AND Balance > " & zero & " AND Status=" & Dos)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(Portfolio_PortfolioAdvance), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Gets all portfolio Extract.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllVReportExtractAccountReceivableXpo(filtro As String) As XPCollection(Of VReportExtractAccountReceivableXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportExtractAccountReceivableXpo) = New XPCollection(Of VReportExtractAccountReceivableXpo)(sessionNew, criteria)
        'collect.Sorting.Add(New SortProperty("DocumentNumber", SortingDirection.Ascending))
        Return collect
    End Function

    ''' <summary>
    ''' Gets all portfolio SuretyFile.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllVReportSuretyFileXpo(filtro As String) As XPCollection(Of VReportSuretyFileXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim collect As XPCollection(Of VReportSuretyFileXpo) = New XPCollection(Of VReportSuretyFileXpo)(sessionNew, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de cuenta por cobrar
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioConcept() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de notas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioNoteConcept() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' Metodo para obtener todos los rangos de provicion
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllProvisionRanges() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioProvisionRangesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Initial;Final", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableDocument() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(AccountReceivableDocumentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;CustomerId;CustomerId.NitName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los traslados
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioTransfers() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioTransferXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;CustomerId;CustomerId.NitName;ThirdPartyId.NitName", Nothing)
        Return serverMode
    End Function

    ' ''' <summary>
    ' ''' Metodo para obtener todos los rangos de provicion
    ' ''' </summary>
    ' ''' <returns></returns>
    'Public Function ListPortfolioAccountReceivableShare(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
    '    Dim sessionNew = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator
    '    If idCostCenter = 0 Then
    '        criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2 And Balance > 0 And AccountReceivableId.ThirdPartyId=" & idThirdParty & " AND AccountReceivableId.MainAccountId=" & idMainAccount)
    '    Else
    '        criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2 And Balance > 0 And AccountReceivableId.ThirdPartyId=" & idThirdParty & " AND AccountReceivableId.MainAccountId=" & idMainAccount & " AND AccountReceivableId.CostCenterId =" & idCostCenter)
    '    End If
    '    classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
    '    serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Metodo para obtener todos los rangos de provicion
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPortfolioAccountReceivableByCashReceipt(idThirdParty As Integer, idMainAccount As Integer, idCostCenter As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If idCostCenter = 0 Then
            criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And ThirdPartyId=" & idThirdParty & " AND PortfolioAccountReceivableAccounting[Balance>0 And MainAccountId=" & idMainAccount & "]")
        Else
            criteria = CriteriaOperator.Parse("Status = 2 And Balance > 0 And ThirdPartyId=" & idThirdParty & " AND PortfolioAccountReceivableAccounting[Balance>0 And MainAccountId=" & idMainAccount & " AND CostCenterId =" & idCostCenter & "]")
        End If
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        Dim filter As String = String.Empty
        If nature = 1 Then
            If noteType = 1 Then
                filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares = 1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
            Else
                filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares >1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
            End If
        Else
            If noteType = 1 Then
                filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares = 1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
            Else
                filter &= "AccountReceivableId.CustomerId=" & customerId & " AND AccountReceivableId.NumberShares > 1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
            End If
        End If
        filter &= " AND AccountReceivableId.Status = 2"
        criteria = CriteriaOperator.Parse(filter)
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' metodo para listar las facturas que se utilizan en notas de cuentas por cobrar por tercero
    ''' </summary>
    ''' <param name="thirdPartyId"></param>
    ''' <param name="nature"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillsPortfolioNoteByThridParty(thirdPartyId As Integer, noteType As Integer, nature As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        Dim filter As String = String.Empty
        If nature = 1 Then
            If noteType = 1 Then
                filter &= "AccountReceivableId.ThirdPartyId =" & thirdPartyId & " AND AccountReceivableId.NumberShares = 1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
            Else
                filter &= "AccountReceivableId.ThirdPartyId =" & thirdPartyId & " AND AccountReceivableId.NumberShares >1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
            End If
        Else
            If noteType = 1 Then
                filter &= "AccountReceivableId.ThirdPartyId =" & thirdPartyId & " AND AccountReceivableId.NumberShares = 1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
            Else
                filter &= "AccountReceivableId.ThirdPartyId =" & thirdPartyId & " AND AccountReceivableId.NumberShares > 1 AND Balance > 0"
                classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableShareXpo))
            End If
        End If
        filter &= " AND AccountReceivableId.Status = 2"
        criteria = CriteriaOperator.Parse(filter)
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista las facturas del tercero para traslados
    ''' </summary>
    ''' <param name="ThirdPartyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListBillsTransfers(ThirdPartyId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
        Dim criteria As CriteriaOperator
        If ThirdPartyId > 0 Then
            criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2 AND AccountReceivableId.ThirdPartyId=" & ThirdPartyId & " AND Balance > 0")
        Else
            criteria = CriteriaOperator.Parse("AccountReceivableId.Status = 2  AND Balance > 0")
        End If
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista las facturas por filtro
    ''' </summary>
    ''' <param name="Filtro"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListInvoiceReportPortfolioFilter(Filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse(Filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;AccountReceivableDate;ThirdPartyId.Nit;ThirdPartyId.Name", criteria)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse(Filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAdvanceReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId.Nit;ThirdPartyId.Name;Status", criteria)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse(Filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioTransferReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;ThirdPartyId.NitName;Status", criteria)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("ThirdPartyId=" & thirdPartyId & " And Balance > 0 and Status = 2")
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAdvanceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del saldo incial
    ''' </summary>
    ''' <param name="portfolioInitialBalanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioInitialBalanceAccountReceivable(portfolioInitialBalanceId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("PortfolioInitialBalanceId=" & portfolioInitialBalanceId)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioInitialBalanceAccountReceivableXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles de las cuentas donde esta la factura
    ''' </summary>
    Public Function ListAccountReceivableAccounting(AccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("AccountReceivableId.Id=" & AccountReceivableId)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableAccountingXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' obtiene una factura por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioAccountReceivableXpo(id As Integer) As PortfolioAccountReceivableXpo
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Return sessionNew.GetObjectByKey(Of PortfolioAccountReceivableXpo)(id)
    End Function

    ''' <summary>
    ''' lista los detalles del saldo incial de las cuentas
    ''' </summary>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioInitialBalanceAccountReceivableAccounting(portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("PortfolioInitialBalanceAccountReceivableId.Id=" & portfolioInitialBalanceAccountReceivableId)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioInitialBalanceAccountReceivableAccountingXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los detalles del saldo incial de las cuotas
    ''' </summary>
    ''' <param name="portfolioInitialBalanceAccountReceivableId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioInitialBalanceAccountReceivableShare(portfolioInitialBalanceAccountReceivableId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("PortfolioInitialBalanceAccountReceivableId.Id=" & portfolioInitialBalanceAccountReceivableId)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioInitialBalanceAccountReceivableShareXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If nature = 1 Then
            criteria = CriteriaOperator.Parse("ThirdPartyId=" & customerId & " And Balance >= 0 and Status = 2")
        Else
            criteria = CriteriaOperator.Parse("ThirdPartyId=" & customerId & " And Balance > 0 and Status = 2")
        End If
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAdvanceXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
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
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim criteria As CriteriaOperator
            criteria = CriteriaOperator.Parse("InvoiceNumber in (" & invoiceParameter & ")")
            Dim collection As New XPCollection(GetType(PortfolioAccountReceivableXpo), criteria)
            Return collection
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
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioNoteXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Code;CustomerId.NitName;NoteDate;NoteTypeName;NatureName;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de notas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllPortfolioNoteConceptByStatus(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("Status =" & status)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Metodo para obtener todos los conceptos de cuentas x cobrar por el estado
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAccountReceivableConceptByStatus(status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria = CriteriaOperator.Parse("Status =" & status)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista los conceptos de nota por tipo de nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioNoteConceptByNoteType(noteType As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("NoteType=" & noteType & " And Status=" & status)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioPortfolioNoteConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;CodeName;IdAccount.NumberName;NoteType;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las facturas en cartera
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivableByStatus(status As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;ExpiredDate;CustomerId.NitName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista las facturas en cartera
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAccountReceivable() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(PortfolioAccountReceivableReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InvoiceNumber;ExpiredDate;CustomerId.NitName", Nothing)
        Return serverMode
    End Function

#End Region

#Region "LinqInstantFeedbackSource"

#Region "ListAccountRecivableAccountByState"
    'Function ListAccountRecivableAccountByState(state As Integer) As LinqInstantFeedbackSource
    '    Throw New NotImplementedException
    'End Function
    Private _parameterStatus As Integer
    Private WithEvents vlinqAccountRecivableByState As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountRecivableAccountByState(status As Byte) As LinqInstantFeedbackSource
        _parameterStatus = status
        vlinqAccountRecivableByState.KeyExpression = "Id"
        Return vlinqAccountRecivableByState
    End Function

    Private Sub OnGetQueryableAccountRecivableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountRecivableByState.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim _accountRecivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(sessionNew)
            Dim _accountRecivableAccounting As XPQuery(Of PortfolioAccountReceivableAccountingXpo) = New XPQuery(Of PortfolioAccountReceivableAccountingXpo)(sessionNew)
            Dim zero As Integer = 0
            Dim tmpQueryableSource = From ara In _accountRecivableAccounting
                                     Join ar In _accountRecivable On ara.AccountReceivableId.Id Equals ar.Id
                                     Where ar.Status = _parameterStatus AndAlso ar.Balance > zero
                                     Select ara
            e.QueryableSource = tmpQueryableSource
            e.Tag = _accountRecivable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountRecivableByState(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountRecivableByState.DismissQueryable
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
    Private WithEvents vlinqAccountRecivable As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListAccountRecivableAccountByThirdIdState(thirdId As Integer, status As Byte) As LinqInstantFeedbackSource
        _parameterThirdId = thirdId
        _parameterState = status
        vlinqAccountRecivable.KeyExpression = "Id"
        Return vlinqAccountRecivable
    End Function

    Private Sub OnGetQueryableAccountRecivable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountRecivable.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim _accountRecivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(sessionNew)
            Dim _accountRecivableAccounting As XPQuery(Of PortfolioAccountReceivableAccountingXpo) = New XPQuery(Of PortfolioAccountReceivableAccountingXpo)(sessionNew)
            'Dim _accountRecivableShares As XPQuery(Of PortfolioAccountReceivableShareXpo) = New XPQuery(Of PortfolioAccountReceivableShareXpo)(sessionNew)
            Dim zero As Integer = 0

            Dim tmpQueryableSource = From ara In _accountRecivableAccounting
                                     Join ar In _accountRecivable On ara.AccountReceivableId.Id Equals ar.Id
                                     Where ar.ThirdPartyId = _parameterThirdId AndAlso ar.Status = _parameterState AndAlso ar.Balance > zero
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
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableAccountRecivable(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqAccountRecivable.DismissQueryable
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
    '        Dim sessionNew = New Session(XpoDefault.DataLayer)
    '        Dim tableAccountReceivable As XPQuery(Of PortfolioAccountReceivableXpo) = New XPQuery(Of PortfolioAccountReceivableXpo)(sessionNew)
    '        Dim tableAccountReceivableShare As XPQuery(Of PortfolioAccountReceivableShareXpo) = New XPQuery(Of PortfolioAccountReceivableShareXpo)(sessionNew)

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

End Class
