'***********************************************************************
' Assembly         : Infrastructure.Data.Xpo.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 03-04-2014
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
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Data.PLinq

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class TreasuryServiceXpo
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
    ''' lista todos los proveedores
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonSuplierReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdThirdParty.Nit;IdThirdParty.Name;Name;IdCity.Name;Status", Nothing)
        serverMode.DefaultSorting = "IdThirdParty.Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the voucher transaction by payment method.
    ''' </summary>
    ''' <param name="paymentMethod">The payment method.</param>
    ''' <returns></returns>
    Function ListVoucherTransactionByPaymentMethod(paymentMethod As Byte) As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ExpenseType = 1 And PaymentMethod = " & paymentMethod)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(VoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CodeClass;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName;NoteNumber", criteria)
        'serverMode.DefaultSorting = "IdThirdParty.Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de egreso que se pagaron con cheque por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTranscationCheckReportTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        If filtro Is Nothing Then
            filtro = "IdChecks is not null"
        Else
            filtro &= "And IdChecks is not null"
        End If
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CheckNumber;TransactionDate;Value;Status;IdThirdParty.NitName", criteria)
        serverMode.DefaultSorting = "CheckNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los reembolsos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRefundsReportTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryRefundsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InitialDate;FinalDate;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notas debito y credito de tesoreria por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryNotesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las consignaciones y transferencia de tesoreria por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVReportConsignmentTransferTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryVReportConsignmentTransfer))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Type;DocumentDate;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los anticipos de pago
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPaymentsAdvancesReportTreasury(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(PaymentsAdvancePaymentsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdSupplier.IdThirdParty.Nit;IdSupplier.IdThirdParty.Name;DocumentDate", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas de cruce
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCrossingAccountReportTreasuryFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryCrossingAccountXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;ThirdPartyId.NitName", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene el concepto por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExpenseConceptById(ExpenseConceptId As Integer) As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & ExpenseConceptId)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(ExpenseConceptXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los comprobantes de transacción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTranscationReportTreasuryFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;IdThirdParty.NitName;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryEntityBankAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank.Name;Number;IdMainAccount.Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de egreso que se pagaron con cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTranscationCheckReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdChecks is not null")
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CheckNumber;TransactionDate;Value;Status;IdThirdParty.NitName", criteria)
        serverMode.DefaultSorting = "CheckNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' data source para listar todos los conceptos de los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReceiptConceptsReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryCashReceiptConceptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Nature;IdMainAccount.Number", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' data source para listar todos los conceptos de los comprobantes de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpensesConceptsReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryExpenseConceptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;IdMainAccount.Number;Behavior", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' data source para listar todos los cheques cancelados de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCancellationChecksReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryCancellationChecksXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;CheckNumber;IdEntityAccount.IdBank.Name;IdEntityAccount.Number;IdEntityAccount.IdMainAccount.NumberName", Nothing)
        serverMode.DefaultSorting = "Id"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los reembolsos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRefundsReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryRefundsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InitialDate;FinalDate;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las consignaciones y transferencia de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVReportConsignmentTransferTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryVReportConsignmentTransfer))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Type;DocumentDate;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notas debito y credito de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryNotesXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyReportTreasury() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CommonThirdPartyReportXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;PersonType;ContributionType;RetentionType;State", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de transacción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTransactionReportTreasury() As XPInstantFeedbackSource
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)
        'Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim collect As XPCollection(Of TreasuryVoucherTransactionXpo) = New XPCollection(Of TreasuryVoucherTransactionXpo)(sessionNew)
        'Return collect
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;IdThirdParty.NitName;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    '''' <summary>
    '''' SE RECOMIENDA USAR XPO SEGURIDAD
    '''' Lista todos los usuarios de creación
    '''' </summary>
    '''' <returns></returns>
    'Public Function ListCreationUsersReportTreasury() As XPInstantFeedbackSource
    '    'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)
    '    'Dim sessionNew = New Session(XpoDefault.DataLayer)
    '    'Dim collect As XPCollection(Of SecurityUserXpo) = New XPCollection(Of SecurityUserXpo)(sessionNew)
    '    'Return collect
    '    Dim sessionNew = New Session(XpoDefault.DataLayer)
    '    classEntity = sessionNew.GetClassInfo(GetType(SecurityUserXpo))
    '    serverMode = New XPInstantFeedbackSource(classEntity, "UserCode;IdPerson.Fullname", Nothing)
    '    serverMode.DefaultSorting = "UserCode"
    '    Return serverMode
    'End Function

    Public Function ListCrossingAccountReportTreasury() As XPInstantFeedbackSource
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)

        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryCrossingAccountXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;ThirdPartyId.NitName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashReceiptReport(ByVal Filtro As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filtro)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryCashReceiptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;IdThirdParty.Nit", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccountsReport() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryEntityBankAccountsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank.Name;Number;IdMainAccount.Name;IdMainAccount.NumberName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las Cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegistersEntity() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryCashRegistersXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the voucher transaction advance by voucher transaction detail identifier.
    ''' </summary>
    ''' <param name="voucherTransactionDetailId">The voucher transaction detail identifier.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionAdvanceByVoucherTransactionDetailId(voucherTransactionDetailId As Integer) As XPCollection(Of VoucherTransactionAdvanceXpo)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[IdVoucherTransactionD] == ?", voucherTransactionDetailId)
        Dim collect As XPCollection(Of VoucherTransactionAdvanceXpo) = New XPCollection(Of VoucherTransactionAdvanceXpo)(New Session(XpoDefault.DataLayer), criteria)
        Return collect
    End Function

    Public Function GetFirstCash() As XPCollection(Of CashRegisterXpo)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[IdVoucherTransactionD] == ?")
        'Dim collect As XPCollection(Of CashRegisterXpo) = New XPCollection(Of CashRegisterXpo)(New Session(XpoDefault.DataLayer))
        'Return collect
    End Function

    ''' <summary>
    ''' Lista todos los anticipos de cartera pagados por comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTransactionAdvance() As XPCollection
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim collect As XPCollection = New XPCollection(sessionNew, GetType(VoucherTransactionAdvanceXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptsByBehavior(behavior As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior=" & behavior & "")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;AffectBudget;Behavior;BehaviorName;IdMainAccount.NumberName;CodeName;NatureName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCheckCashing() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CheckCashingXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CurrentCheckNumber;NextCheckNumber;CancellationDate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las notas de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTreasuryNote() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryNoteXpo))
        serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas los cruces de cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCrossingAccount() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" &  & "")
        classEntity = sessionNew.GetClassInfo(GetType(CrossingAccountXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;ThirdPartyId;ThirdPartyId.NitName;DocumentDate;Status;StatusName", Nothing)
        'serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptsByBehaviorAndIdMainAccount(behavior As Integer, IdMainAccount As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior=" & behavior & " and IdMainAccount.Id=" & IdMainAccount & "")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;AffectBudget;Behavior;BehaviorName;IdMainAccount.NumberName;CodeName;NatureName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todos los reembolsos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRefund() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(RefundXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdCashRegister;InitialDate;FinalDate;Value;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todos los reembolsos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConsignment() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(ConsignmentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;EntityBankAccountId;MainAccountId;Value;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the schedule payment.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSchedulePayment() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(SchedulePaymentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ScheduledDate;EntityBankAccountId;PaymentMethod;TaxByMil;Status;StatusName;StatusNameDispersionFund", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the schedule payment.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSchedulePaymentConfirm() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & 2 & " Or Status=" & 4 & " Or Status=" & 5)
        classEntity = sessionNew.GetClassInfo(GetType(SchedulePaymentXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ScheduledDate;EntityBankAccountId;PaymentMethod;TaxByMil;Status;StatusName;StatusNameDispersionFund", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los detalles de egresos por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDischargeBillByIdVoucherTransactionDXpo(ByVal IdVoucherTransactionD As Integer) As XPCollection
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)
        Dim collect As XPCollection = New XPCollection(GetType(DischargeBillXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCard(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CardXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;IdRetentionConceptCommision;IdRetentionConceptRTF;IdRetentionConceptICA;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashReceiptConcept(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CashReceiptConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Nature;Affectation;AffectationName;CodeName;IdMainAccount;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashReceiptConceptCollection(status As Boolean) As XPCollection(Of CashReceiptConceptXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CashReceiptConceptXpo))
        Return New XPCollection(Of CashReceiptConceptXpo)(sessionNew, criteria)
    End Function


    ''' <summary>
    ''' lista los documentos de control por tipo de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTreasuryControlByDocumentType(documentType As Integer) As XPCollection(Of TreasuryControlXpo)
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentType=" & documentType & "")
        classEntity = sessionNew.GetClassInfo(GetType(TreasuryControlXpo))
        Return New XPCollection(Of TreasuryControlXpo)(sessionNew, criteria)
    End Function

    

    ''' <summary>
    ''' Lists the cash receipt concept by affectation.
    ''' </summary>
    Public Function ListCashReceiptConceptByAffectation(status As Boolean, affectation As Byte) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " AND Affectation=" & affectation)
        classEntity = sessionNew.GetClassInfo(GetType(CashReceiptConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Nature;Affectation;CodeName;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegister(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(CashRegisterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdCostCenter;CodeName;IdMainAccount;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the type of the cash register by.
    ''' </summary>
    ''' <param name="Type">The type.</param>
    ''' <returns></returns>
    Public Function ListCashRegisterByType(Type As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=" & Type & "")
        classEntity = sessionNew.GetClassInfo(GetType(CashRegisterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdMainAccount;IdCostCenter;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egresos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConcept(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;IdMainAccount;AffectBudget;IdMainAccount.NumberName;Behavior;IdMainAccount.Number;NatureName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los conceptos de egreso por id de caja
    ''' </summary>
    ''' <param name="IdCash">The identifier cash.</param>
    ''' <returns></returns>
    Public Function ListExpenseConceptCashRegisterByCash(IdCash As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdCashRegister=" & IdCash & "")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptCashRegisterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdExpenseConcept;IdExpenseConcept.CodeName;IdExpenseConcept.Id;IdExpenseConcept.Code;IdExpenseConcept.Description;IdCashRegister;IdCashRegister.CodeName", criteria)
        Return serverMode
    End Function

    'Public Function ListExpenseConceptByCash(IdCash As Integer) As XPInstantFeedbackSource
    '    Dim sessionNew = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdCashRegister=" & IdCash & "")
    '    classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
    '    serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;IdAccount;AffectBudget;IdAccount.NumberName;Behavior", criteria)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Lista todas las cajas distintas a la elegida
    ''' </summary>
    ''' <param name="IdCash">The identifier cash.</param>
    ''' <returns></returns>
    Public Function ListExpenseConceptCashRegisterByNotCash(IdCash As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdCashRegister<>" & IdCash & "")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptCashRegisterXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdExpenseConcept;IdExpenseConcept.CodeName;IdExpenseConcept.Id;IdExpenseConcept.Code;IdExpenseConcept.Description;IdCashRegister;IdCashRegister.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteConcept(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(NoteConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;AffectBudget;AutoCollect;Nature;NatureValue;IdMainAccount;IdMainAccount.Id;IdMainAccount.NumberName;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThridPartyBankAccount(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(ThridPartyBankAccountXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Type;Number;IdThirParty;IdBank;IdRadicationCity;IdBankCity;IdThirParty.Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de terceros por id del banco
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccountByBank(status As Boolean, bankId As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator
        If bankId <> 0 Then
            criteria = CriteriaOperator.Parse("Status = " & status & " AND IdBank.Id = " & bankId)
        Else
            criteria = CriteriaOperator.Parse("Status = " & status)
        End If
        classEntity = sessionNew.GetClassInfo(GetType(EntityBankAccountXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccount(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(EntityBankAccountXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists all bank.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllBank(status As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
        classEntity = sessionNew.GetClassInfo(GetType(BankXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId;Name;AccountNumber;AccountType;AchCode;BankFileCode;State;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllPaymentConcept(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(PaymentConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las chequeras canceladas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllCancellationCheck() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        classEntity = sessionNew.GetClassInfo(GetType(CancellationCheckXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdEntityAccount;IdEntityAccount.Id;CancellationDate;CheckNumber;Description;IdEntityAccount.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllVoucherTransaction() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(VoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CodeClass;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName;NoteNumber", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los comprobantes de egreso por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllVoucherTransactionByStatus(state As Byte) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(VoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists all cash receipt by status.
    ''' </summary>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Function ListAllCashReceiptByStatus(state As Integer) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        classEntity = sessionNew.GetClassInfo(GetType(CashReceiptsXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, 
            "Id;Code;IdThirdParty.Name;IdThirdParty.NitName;CollectType;DocumentDate;Value;Status;CurrencyAbbreviation", 
            criteria)
        Return serverMode
    End Function

    

    ''' <summary>
    ''' Lists all voucher transaction with voucher type check.
    ''' </summary>
    Public Function ListAllVoucherTransactionWithVoucherTypeCheck() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim one As Integer = 1
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ExpenseType=" & one & " and PaymentMethod=" & one & " and Status!=3")
        classEntity = sessionNew.GetClassInfo(GetType(VoucherTransactionXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the expense concept by cash1.
    ''' </summary>
    ''' <param name="cashRegisterId">The cash register identifier.</param>
    ''' <returns></returns>
    Public Function ListExpenseConceptByCash1(ByVal cashRegisterId As String) As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ExpenseConceptCashRegisterXpo[IdCashRegister.Id = ?] or Behavior = 6", cashRegisterId)
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;NatureName;IdMainAccount;IdMainAccount.Id;AffectBudget;Behavior;BehaviorName;Status;CodeName;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the expense concept by major cash.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptByMajorCash() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior = 4 or Behavior = 6")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;NatureName;IdMainAccount;IdMainAccount.Id;AffectBudget;Behavior;BehaviorName;Status;CodeName;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the expense concept by not cash1.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptByNotCash1() As XPInstantFeedbackSource
        Dim sessionNew = New Session(XpoDefault.DataLayer)
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior != 2 AND Behavior != 7")
        classEntity = sessionNew.GetClassInfo(GetType(ExpenseConceptXpo))
        serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;NatureName;IdMainAccount;IdMainAccount.Id;AffectBudget;Behavior;BehaviorName;Status;CodeName;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function


#End Region

#Region "LinqFeedBackSource"

#Region "SchedulePayment"
    Private WithEvents vlinqSchedulePayment As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetSchedulePayment() As LinqInstantFeedbackSource
        vlinqSchedulePayment.KeyExpression = "Name"
        Return vlinqSchedulePayment
    End Function

    Private Sub OnGetQueryableSchedulePayment(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqSchedulePayment.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim _supplier As XPQuery(Of Supplier) = New XPQuery(Of Supplier)(sessionNew)
            Dim _supplierDistributionLines As XPQuery(Of SuppliersDistributionLinesXpo) = New XPQuery(Of SuppliersDistributionLinesXpo)(sessionNew)
            Dim _distributionLines As XPQuery(Of DistributionLinesXpo) = New XPQuery(Of DistributionLinesXpo)(sessionNew)
            Dim _accountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(sessionNew)

            Dim zero As Integer = 0
            Dim Dos As Integer = 2

            Dim TmpQueryableSource = From S In _supplier
                                     Join SDL In _supplierDistributionLines On S.Id Equals SDL.IdSupplier
                                     Join DL In _distributionLines On SDL.IdDistributionLine Equals DL.Id
                                     Join AP In _accountPayable On AP.IdSupplier Equals S.Id
                                     Where AP.Balance > zero And AP.Status = Dos
                                     Select Supplier = S.Name, DistributionLine = DL.Description, Invoice = AP.BillNumber, ExpirationDate = AP.ExpirationDate, Balance = AP.Balance
            'Select New With {Key .Nombre = S.Name, Key .Linea = DL.Description, Key .Factura = AP.BillNumber, Key .Fecha = AP.ExpirationDate, Key .Edad = CalculateAgePayments(AP.ExpirationDate), Key .Saldo = AP.Balance}
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = _accountPayable
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableSchedulePayment(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqSchedulePayment.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Public Function CalculateAgePayments(ByVal ExpirationDate As Date, session As Session) As String
        Try
            Dim _agesPayments As XPQuery(Of AgesPaymentsXpo) = New XPQuery(Of AgesPaymentsXpo)(session)
            Dim dias = Date.Now.Day - ExpirationDate.Day + 1
            Dim edad = From AP In _agesPayments
                                     Where AP.InitialRange <= dias And AP.EndRange >= dias
                                     Select AP.Name
            Return edad.FirstOrDefault()
        Catch ex As Exception
            Return String.Empty
        End Try
    End Function

#End Region

#Region "CashRegisterByUser"

    Private WithEvents vlinqCashRegister As New LinqInstantFeedbackSource
    Private _idUser As Integer
    Private _status As Boolean
    Private _type As Integer

    Public Function ListCashRegisterByUser(idUser As Integer, type As Integer, status As Boolean)
        _idUser = idUser
        _status = status
        _type = type
        Return vlinqCashRegister
    End Function

    Private Sub OnGetQueryableCashRegister(sender As Object, e As GetQueryableEventArgs) Handles vlinqCashRegister.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableCashRegister As XPQuery(Of CashRegisterXpo) = New XPQuery(Of CashRegisterXpo)(sessionNew)
            Dim tableCashRegisterUser As XPQuery(Of CashRegisterUserXpo) = New XPQuery(Of CashRegisterUserXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing
            'El tipo 0 me trae todas las cajas
            If _type <> 0 Then
                TmpQueryableSource = From cr In tableCashRegister
                                     Join cru In tableCashRegisterUser On cr.Id Equals cru.IdCashRegister
                                     Where cru.IdUser = _idUser And cr.Status = _status And cr.Type = _type
                                     Select cr
            Else
                TmpQueryableSource = From cr In tableCashRegister
                                     Join cru In tableCashRegisterUser On cr.Id Equals cru.IdCashRegister
                                     Where cru.IdUser = _idUser And cr.Status = _status
                                     Select cr
            End If


            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCashRegister
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashRegister(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConcept.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "EntityBankAccount LinqInstantFeedBackSource"
    Private WithEvents vlinqEntityBankAccount As New LinqInstantFeedbackSource
    Private _filterCodUser As String
    Private _filterStatus As Boolean
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListEntityBankAccountByUser(ByVal codUser As String, ByVal status As Boolean) As LinqInstantFeedbackSource
        _filterCodUser = codUser
        _filterStatus = status
        vlinqEntityBankAccount.KeyExpression = "Id"
        Return vlinqEntityBankAccount
    End Function

    Private Sub OnGetQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEntityBankAccount.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableEntityBankAccount As XPQuery(Of EntityBankAccountXpo) = New XPQuery(Of EntityBankAccountXpo)(sessionNew)
            Dim tableEntityBankAccountUser As XPQuery(Of EntityBankAccountUserXpo) = New XPQuery(Of EntityBankAccountUserXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableEntityBankAccount
                                     Join T2 In tableEntityBankAccountUser On T2.IdEntityBankAccount Equals T1.Id
                                     Where T2.CodUser.Equals(_filterCodUser) And T1.Status = _filterStatus
                                     Select T1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEntityBankAccountUser
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub DismissQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqEntityBankAccount.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ExpenseConcept LinqInstantFeedBackSource"
    Private WithEvents vlinqExpenseConcept As New LinqInstantFeedbackSource
    Private _filterExpenseConcept As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListExpenseConceptByCash(ByVal filter As String) As LinqInstantFeedbackSource
        _filterExpenseConcept = CInt(filter)
        vlinqExpenseConcept.KeyExpression = "IdCashRegister"
        Return vlinqExpenseConcept
    End Function

    Private Sub OnGetQueryableExpenseConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConcept.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableExpenseConceptCashRegister As XPQuery(Of ExpenseConceptCashRegisterXpo) = New XPQuery(Of ExpenseConceptCashRegisterXpo)(sessionNew)
            Dim tableExpenseConcept As XPQuery(Of ExpenseConceptXpo) = New XPQuery(Of ExpenseConceptXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableExpenseConcept
                                     Join T2 In tableExpenseConceptCashRegister On T2.IdExpenseConcept.Id Equals T1.Id
                                     Where T2.IdCashRegister.Id = _filterExpenseConcept
                                     Select T1 'T1.Id, T2.RadicatedConsecutive, T1.InvoiceNumber, NitName = T2.CustomerId.NitName.Trim, T2.DocumentDate
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableExpenseConceptCashRegister
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableExpenseConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConcept.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ExpenseConcept LinqInstantFeedBackSource"
    Private WithEvents vlinqExpenseConceptByNotCash As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListExpenseConceptByNotCash() As LinqInstantFeedbackSource
        vlinqExpenseConceptByNotCash.KeyExpression = "Id"
        Return vlinqExpenseConceptByNotCash
    End Function

    Private Sub OnGetQueryableExpConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConceptByNotCash.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableExpenseConcept As XPQuery(Of ExpenseConceptXpo) = New XPQuery(Of ExpenseConceptXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableExpenseConcept
                                     Where T1.Behavior <> 2 AndAlso T1.Behavior <> 7
                                     Select T1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableExpenseConcept
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableExpConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConceptByNotCash.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListExpenseConceptEndorsement"
    Private WithEvents vlinqExpenseConceptEndorsement As New LinqInstantFeedbackSource
    Private _behavior As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListExpenseConceptEndorsement(ByVal Behavior As Integer) As LinqInstantFeedbackSource
        _behavior = Behavior
        vlinqExpenseConceptEndorsement.KeyExpression = "Id"
        Return vlinqExpenseConceptEndorsement
    End Function

    Private Sub OnGetQueryableExpConceptEndorsement(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConceptEndorsement.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableExpenseConcept As XPQuery(Of ExpenseConceptXpo) = New XPQuery(Of ExpenseConceptXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tableExpenseConcept
                                     Where T1.Behavior = _behavior
                                     Select T1
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableExpenseConcept
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableExpConceptEndorsement(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqExpenseConceptEndorsement.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "PaymentConcept LinqInstantFeedBackSource"
    Private WithEvents vlinqPaymentConcept As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListPaymentConcept() As LinqInstantFeedbackSource
        vlinqPaymentConcept.KeyExpression = "Id"
        Return vlinqPaymentConcept
    End Function

    Private Sub OnGetQueryablePaymentConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqPaymentConcept.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tablePaymentConcept As XPQuery(Of PaymentConceptXpo) = New XPQuery(Of PaymentConceptXpo)(sessionNew)
            Dim TmpQueryableSource = From T1 In tablePaymentConcept
                                     Where T1.Status = True
                                     Select T1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tablePaymentConcept
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryablePaymentConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqPaymentConcept.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "CashReceipts LinqInstantFeedBackSource"
    Private WithEvents vlinqCashReceipts As New LinqInstantFeedbackSource
    ''' <summary>
    ''' Obtiene todos los recibos de caja
    ''' </summary>
    Public Function ListCashReceipts() As LinqInstantFeedbackSource
        vlinqCashReceipts.KeyExpression = "Id"
        Return vlinqCashReceipts
    End Function

    Private Sub OnGetQueryableCashReceipts(sender As Object, e As GetQueryableEventArgs) Handles vlinqCashReceipts.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableCashReceipts As XPQuery(Of CashReceiptsXpo) = New XPQuery(Of CashReceiptsXpo)(sessionNew)
            Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(sessionNew)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(sessionNew)
            Dim tmpQueryableSource = From cr In tableCashReceipts
                      Join tp In tableThirdParty On cr.IdThirdParty.Id Equals tp.Id
                      Join ma In tableMainAccount On cr.IdMainAccount Equals ma.Id
                      Select Id = cr.Id, Code = cr.Code, CollectType = If(cr.CollectType = 1, ResourceManager.GetString("ExpenseTypeCash", "Treasury"), ResourceManager.GetString("ExpenseTypeBankAccount", "Treasury")), DocumentDate = cr.DocumentDate, Value = cr.Value, NumberName = ma.NumberName, NitName = tp.NitName, StatusName = If(cr.Status = 1, ResourceManager.GetString("StateUnconfirmed"), If(cr.Status = 2, ResourceManager.GetString("StateConfirmed"), ResourceManager.GetString("StatusCanceled")))

            e.QueryableSource = tmpQueryableSource
            e.Tag = tableCashReceipts
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceipts(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqCashReceipts.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "CashReceiptConceptsByRetentionType"

    Private WithEvents vlinqCashReceiptConceptsByRetentionType As New LinqInstantFeedbackSource
    Private _retentionType As Integer

    Public Function ListCashReceiptConceptsByRetentionType(retentionType As Integer, status As Boolean)
        _retentionType = retentionType
        _status = status
        Return vlinqCashReceiptConceptsByRetentionType
    End Function

    Private Sub OnGetQueryableCashReceiptsByRetentionType(sender As Object, e As GetQueryableEventArgs) Handles vlinqCashReceiptConceptsByRetentionType.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableCashReceiptConcetp As XPQuery(Of CashReceiptConceptXpo) = New XPQuery(Of CashReceiptConceptXpo)(sessionNew)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing

            TmpQueryableSource = From crc In tableCashReceiptConcetp
                                 Join ma In tableMainAccount On ma.Id Equals crc.IdMainAccount.Id
                                 Where ma.RetencionType = _retentionType And crc.Status = _status
                                 Select crc

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCashReceiptConcetp
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceiptsByRetentionType(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqCashReceiptConceptsByRetentionType.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "CashReceiptConceptWithOutCostCenter"
    'lista los conceptos de recibo de caja que la cuenta contable no maneje centro de costo
    Private WithEvents vlinqCashReceiptConceptWithOutCostCenter As New LinqInstantFeedbackSource
    Private _handlesCosteCenter As Boolean

    Public Function ListCashReceiptConceptWithOutCostCenter(handlesCosteCenter As Boolean, status As Boolean)
        _handlesCosteCenter = handlesCosteCenter
        _status = status
        Return vlinqCashReceiptConceptWithOutCostCenter
    End Function

    Private Sub OnGetQueryableCashReceiptConceptWithOutCostCenter(sender As Object, e As GetQueryableEventArgs) Handles vlinqCashReceiptConceptWithOutCostCenter.GetQueryable
        Try
            Dim sessionNew = New Session(XpoDefault.DataLayer)
            Dim tableCashReceiptConcetp As XPQuery(Of CashReceiptConceptXpo) = New XPQuery(Of CashReceiptConceptXpo)(sessionNew)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(sessionNew)
            Dim TmpQueryableSource = Nothing

            TmpQueryableSource = From crc In tableCashReceiptConcetp
                                 Join ma In tableMainAccount On ma.Id Equals crc.IdMainAccount.Id
                                 Where ma.HandlesCostCenter = _handlesCosteCenter And crc.Status = _status
                                 Select crc

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCashReceiptConcetp
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceiptConceptWithOutCostCenter(ByVal sender As Object, ByVal e As GetQueryableEventArgs) Handles vlinqCashReceiptConceptWithOutCostCenter.DismissQueryable
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
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

#Region "Reporte Libro de Caja"
    ''' <summary>
    ''' Lista todos lo movimientos registrados agrupados por cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCollectionReportCashBook(ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDStatus As String, ByVal INDPaymentType As Integer, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDUserStart As String, ByVal INDUserEnd As String) As List(Of TreasuryVReportCashBookXpo)

        Dim resulXpo As IList(Of TreasuryVReportCashBookXpo)
        Dim resulXpoTreasuryBalance As IList(Of TreasuryTreasuryBalance)

        'Definir Criteria
        Dim criteria As String = Nothing
        Dim criteriaPreviousBalance
        criteria = "DocumentDate >= #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDateEnd, "yyyy-MM-dd HH:mm:ss") & "#"
        criteriaPreviousBalance = "CreationDate >= #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "# AND CreationDate <= #" & Format(INDDateEnd, "yyyy-MM-dd HH:mm:ss") & "#"
        'filtro por cajas
        If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
            criteria &= "AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
            criteriaPreviousBalance &= "AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
        End If
        If INDUserStart IsNot Nothing And INDUserEnd IsNot Nothing Then
            criteria &= " AND UserCode >= '" & INDUserStart & "' AND UserCode <= '" & INDUserEnd & "'"
        End If
        If INDPaymentType <> 6 Then
            criteria &= " AND PaymentMethod = " & INDPaymentType
        End If
        If INDStatus IsNot Nothing Then
            criteria &= " AND Status in(" & INDStatus.ToString & ")"
        End If

        resulXpo = Me.LoadCollection(Of TreasuryVReportCashBookXpo)(XpoDefault.DataLayer, Nothing, criteria)
        'cargar el saldo anterior a los cajas
        resulXpoTreasuryBalance = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaPreviousBalance)
        Dim dictionaryBalance As New Dictionary(Of String, Double)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.CashRegisterId).ThenBy(Function(x) x.DocumentDate)
            Dim OperacionXpo As Long
            OperacionXpo = (itemXpo.ValueDebit - itemXpo.ValueCredit)
            Dim ListTreasury = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId)).ToList()
            Dim DateMin As Date
            If ListTreasury.Count > 0 Then
                DateMin = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId)).ToList.Min(Function(x) x.CreationDate)
            Else
                Continue For
            End If
            For Each itemBalanceMin In resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId And x.CreationDate = DateMin)
                If dictionaryBalance.ContainsKey("C" & itemXpo.CashRegisterId) Then
                    dictionaryBalance("C" & itemXpo.CashRegisterId) += OperacionXpo
                    'itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
                    itemXpo.NuevoSaldo = itemBalanceMin.PreviousBalance + dictionaryBalance("C" & itemXpo.CashRegisterId)
                Else
                    dictionaryBalance.Add("C" & itemXpo.CashRegisterId, OperacionXpo)
                    itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
                    itemXpo.NuevoSaldo = itemXpo.SaldoAnterior + dictionaryBalance("C" & itemXpo.CashRegisterId)
                End If
            Next
        Next

        Return resulXpo
    End Function

#End Region

#Region "Reporte Diario de Caja"
    ''' <summary>
    ''' Lista todos lo movimientos registrados agrupados por cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCollectionReportCashJournal(ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDStatus As Integer, ByVal INDPaymentType As Integer, ByVal INDCashStart As String, ByVal INDCashEnd As String) As List(Of TreasuryVReportCashBookXpo)

        Dim resulXpo As IList(Of TreasuryVReportCashBookXpo)
        Dim resulXpoTreasuryBalance As IList(Of TreasuryTreasuryBalance)

        'Definir Criteria
        Dim criteria As String = Nothing
        Dim criteriaPreviousBalance
        criteria = "GetDate(DocumentDate) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "#"
        criteriaPreviousBalance = "GetDate(DocumentDate) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "#"
        'filtro por cajas
        If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
            criteria &= "AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
            criteriaPreviousBalance &= "AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
        End If
        If INDPaymentType <> 5 Then
            criteria &= "AND PaymentMethod = " & INDPaymentType
        End If
        If INDStatus = 3 Then
            criteria &= "AND Status in (1,2)"
        Else
            criteria &= "AND Status = " & INDStatus
        End If

        resulXpo = Me.LoadCollection(Of TreasuryVReportCashBookXpo)(XpoDefault.DataLayer, Nothing, criteria)
        'cargar el saldo anterior a los cajas
        resulXpoTreasuryBalance = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaPreviousBalance)
        Dim dictionaryBalance As New Dictionary(Of String, Integer)
        For Each itemXpo In resulXpo.OrderBy(Function(x) x.CashRegisterId).ThenBy(Function(x) x.DocumentDate)
            Dim OperacionXpo As Long
            OperacionXpo = (itemXpo.ValueDebit - itemXpo.ValueCredit)
            Dim DateMin = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId)).ToList.Min(Function(x) x.DocumentDate)
            For Each itemBalanceMin In resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId And x.DocumentDate = DateMin)
                If dictionaryBalance.ContainsKey("C" & itemXpo.CashRegisterId) Then
                    dictionaryBalance("C" & itemXpo.CashRegisterId) += OperacionXpo
                    'itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
                    itemXpo.NuevoSaldo = itemBalanceMin.PreviousBalance + dictionaryBalance("C" & itemXpo.CashRegisterId)
                Else
                    dictionaryBalance.Add("C" & itemXpo.CashRegisterId, OperacionXpo)
                    itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
                    itemXpo.NuevoSaldo = itemXpo.SaldoAnterior + dictionaryBalance("C" & itemXpo.CashRegisterId)
                End If
            Next
        Next

        Return resulXpo
    End Function

#End Region

#Region "Report TreasuryNewsletterEntityBank"
    ''' <summary>
    ''' Lista todos lo movimientos registrados en las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCollectionTreasuryNewsletterEntityBank(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDStatus As String, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDTypeVoucher As Integer) As List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)

        Dim resulXpo As IList(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND VoucherType =" & INDTypeVoucher
        'filtro por cuentas bancarias
        If INDCurrentAccountSavingsStart IsNot Nothing And INDCurrentAccountSavingsEnd IsNot Nothing Then
            criteria &= "AND EntityBankAccountCode >= '" & INDCurrentAccountSavingsStart & "' AND EntityBankAccountCode <= '" & INDCurrentAccountSavingsEnd & "'"
        End If
        If INDStatus IsNot Nothing Then
            criteria &= " AND Status in(" & INDStatus.ToString & ")"
        End If

        resulXpo = Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)(XpoDefault.DataLayer, Nothing, criteria)
        Return resulXpo
    End Function

#End Region

#Region "Report TreasuryNewsletterEntityCash"
    ''' <summary>
    ''' Lista todos los movimientos registados en las cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCollectionTreasuryNewsletterEntityCash(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDStatus As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDTypeVoucher As Integer) As List(Of TreasuryVReportTreasuryNewsletterCash)

        Dim resulXpo As IList(Of TreasuryVReportTreasuryNewsletterCash)

        'Definir Criteria
        Dim criteria As String = Nothing
        criteria &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND VoucherType =" & INDTypeVoucher
        'filtro por Cajas
        If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
            criteria &= "AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
        End If
        If INDStatus IsNot Nothing Then
            criteria &= " AND Status in(" & INDStatus.ToString & ")"
        End If

        resulXpo = Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterCash)(XpoDefault.DataLayer, Nothing, criteria)
        Return resulXpo

    End Function
#End Region

#Region "Report TreasuryNewsletterSummaryNewsletter"
    ''' <summary>
    ''' Lista todas los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionSummaryNewsletter(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDStatus As String, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer) As List(Of TreasuryVReportTreasuryNewsletterSummary)

        Dim resulXpo As IList(Of TreasuryVReportTreasuryNewsletterSummary)
        Dim resulXpoTreasuryBalance As IList(Of TreasuryTreasuryBalance)
        Dim resulXpoTreasuryBalanceNoMovement As IList(Of TreasuryTreasuryBalance)
        Dim resultXpoEntityBanksAccount As IList(Of TreasuryEntityBankAccountsXpo)
        Dim resultXpoCashRegister As IList(Of TreasuryCashRegistersXpo)

        'Definir Criteria
        Dim criteria As String = Nothing
        Dim criteriaNoMovement As String = Nothing
        Dim criteriaBalanceMovement As String = Nothing
        Dim criteriaEntityBanksAccount As String = Nothing
        Dim criteriaCashRegister As String = Nothing
        criteria &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        criteriaBalanceMovement &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        criteriaNoMovement = "GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "#"
        'filtro por cuentas bancarias y cajas
        If INDCurrentAccountSavingsStart IsNot Nothing And INDCurrentAccountSavingsEnd IsNot Nothing And INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then

            criteria &= "AND EntityBankAccountCode >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountCode <= '" & INDCurrentAccountSavingsEnd & "' or GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
            criteriaBalanceMovement &= "AND EntityBankAccountId.Code >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountId.Code <= '" & INDCurrentAccountSavingsEnd & "' or GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
            criteriaNoMovement &= "AND EntityBankAccountId.Code >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountId.Code <= '" & INDCurrentAccountSavingsEnd & "' or GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
            criteriaEntityBanksAccount = "Code >= '" & INDCurrentAccountSavingsStart & "' And  Code <= '" & INDCurrentAccountSavingsEnd & "'"
            criteriaCashRegister = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "'"
        ElseIf INDCurrentAccountSavingsStart IsNot Nothing And INDCurrentAccountSavingsEnd IsNot Nothing Then

            criteria &= "AND EntityBankAccountCode >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountCode <= '" & INDCurrentAccountSavingsEnd & "'"
            criteriaBalanceMovement &= "AND EntityBankAccountId.Code >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountId.Code <= '" & INDCurrentAccountSavingsEnd & "'"
            criteriaNoMovement &= "AND EntityBankAccountId.Code >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountId.Code <= '" & INDCurrentAccountSavingsEnd & "'"
            criteriaEntityBanksAccount = "Code >= '" & INDCurrentAccountSavingsStart & "' And  Code <= '" & INDCurrentAccountSavingsEnd & "'"
        ElseIf INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then

            criteria &= "AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
            criteriaBalanceMovement &= "AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
            criteriaNoMovement &= "AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
            criteriaCashRegister = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "'"
        End If

        'cargar cuentas y cajas que tuvieron movimiento en el rango de fecha
        resulXpo = Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterSummary)(XpoDefault.DataLayer, Nothing, criteria)

        'Cargar Los saldos Anteriores de las cuentas y cajas que tuvieron movimientos en el rango de fecha
        resulXpoTreasuryBalance = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaBalanceMovement)
        Dim dictionaryBalance As New Dictionary(Of String, Decimal)
        For Each itemXpo In resulXpo
            If itemXpo.EntityBankAccountId = 0 Then
                Dim DateMin = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId)).ToList.Min(Function(x) x.DocumentDate)
                For Each itemBalanceMin In resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId And x.DocumentDate = DateMin)
                    If Not dictionaryBalance.ContainsKey("C" & itemXpo.CashRegisterId) Then
                        itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
                        dictionaryBalance.Add("C" & itemXpo.CashRegisterId, itemXpo.SaldoAnterior)
                    End If
                Next
            ElseIf itemXpo.CashRegisterId = 0 Then
                Dim DateMin = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemXpo.EntityBankAccountId)).ToList.Min(Function(x) x.DocumentDate)
                For Each itemBalanceMin In resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemXpo.EntityBankAccountId And x.DocumentDate = DateMin)
                    If Not dictionaryBalance.ContainsKey("B" & itemXpo.EntityBankAccountId) Then
                        itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
                        dictionaryBalance.Add("B" & itemXpo.EntityBankAccountId, itemXpo.SaldoAnterior)
                    End If
                Next
            End If
        Next

        Dim ValueRow As Integer = 1
        'Cargar los Saldo Anteriores De las Cuentas y cajas que no tienen movimentos en el rango de fechas
        resulXpoTreasuryBalanceNoMovement = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaNoMovement)
        For Each itemXpo In resulXpoTreasuryBalanceNoMovement
            If itemXpo.EntityBankAccountId Is Nothing Then
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.CashRegisterId = If(IsNothing(itemXpo.CashRegisterId) = True, 0, itemXpo.CashRegisterId.Id))).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim resultList = (resulXpoTreasuryBalanceNoMovement.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = If(IsNothing(itemXpo.CashRegisterId) = True, 0, itemXpo.CashRegisterId.Id))).ToList
                    If resultList.Count > 0 Then
                        Dim DateMax = resultList.Max(Function(x) x.DocumentDate)
                        Dim itemXpoTreasuryBalanceNoMovement = resulXpoTreasuryBalanceNoMovement.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = If(IsNothing(itemXpo.CashRegisterId) = True, 0, itemXpo.CashRegisterId.Id) And x.DocumentDate = DateMax).SingleOrDefault
                        If Not dictionaryBalance.ContainsKey("C" & itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id) Then
                            Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                            If resulXpo.Count < 1 Then
                                ValueRow = 1
                            Else
                                ValueRow = resulXpo.Max(Function(x) x.Row)
                            End If
                            resulTresuryBalance.Row = ValueRow
                            resulTresuryBalance.DocumentDate = itemXpoTreasuryBalanceNoMovement.DocumentDate
                            resulTresuryBalance.CashRegisterId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id)
                            resulTresuryBalance.EntityBankAccountId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id)
                            resulTresuryBalance.SaldoAnterior = itemXpoTreasuryBalanceNoMovement.PreviousBalance
                            resulTresuryBalance.SumReceipts = 0
                            resulTresuryBalance.SumExpenditures = 0
                            resulTresuryBalance.Code = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.IdBank.Code)
                            resulTresuryBalance.Name = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.IdBank.Name)
                            resulTresuryBalance.Type = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Type)
                            resulTresuryBalance.Number = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Number)
                            resulTresuryBalance.CashRegisterCode = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Code)
                            resulTresuryBalance.NameCash = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Name)
                            resulXpo.Add(resulTresuryBalance)
                            dictionaryBalance.Add("C" & itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id, itemXpoTreasuryBalanceNoMovement.PreviousBalance)
                        End If
                    End If
                End If
            ElseIf itemXpo.CashRegisterId Is Nothing Then
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.EntityBankAccountId = If(IsNothing(itemXpo.EntityBankAccountId) = True, 0, itemXpo.EntityBankAccountId.Id))).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim resultList = (resulXpoTreasuryBalanceNoMovement.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = If(IsNothing(itemXpo.EntityBankAccountId) = True, 0, itemXpo.EntityBankAccountId.Id))).ToList
                    If resultList.Count > 0 Then
                        Dim DateMax = resultList.Max(Function(x) x.DocumentDate)
                        Dim itemXpoTreasuryBalanceNoMovement = resulXpoTreasuryBalanceNoMovement.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = If(IsNothing(itemXpo.EntityBankAccountId) = True, 0, itemXpo.EntityBankAccountId.Id) And x.DocumentDate = DateMax).SingleOrDefault
                        If Not dictionaryBalance.ContainsKey("B" & itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id) Then
                            Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                            If resulXpo.Count < 1 Then
                                ValueRow = 1
                            Else
                                ValueRow = resulXpo.Max(Function(x) x.Row)
                            End If
                            resulTresuryBalance.Row = ValueRow
                            resulTresuryBalance.DocumentDate = itemXpoTreasuryBalanceNoMovement.DocumentDate
                            resulTresuryBalance.CashRegisterId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id)
                            resulTresuryBalance.EntityBankAccountId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id)
                            resulTresuryBalance.SaldoAnterior = itemXpoTreasuryBalanceNoMovement.PreviousBalance
                            resulTresuryBalance.SumReceipts = 0
                            resulTresuryBalance.SumExpenditures = 0
                            resulTresuryBalance.Code = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.IdBank.Code)
                            resulTresuryBalance.Name = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.IdBank.Name)
                            resulTresuryBalance.Type = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Type)
                            resulTresuryBalance.Number = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Number)
                            resulTresuryBalance.CashRegisterCode = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Code)
                            resulTresuryBalance.NameCash = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Name)
                            resulXpo.Add(resulTresuryBalance)
                            dictionaryBalance.Add("B" & itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id, itemXpoTreasuryBalanceNoMovement.PreviousBalance)
                        End If
                    End If

                End If
            End If
        Next


        'Cargar las entidades bancarias que no tengan ningun movimiento en treasuryBalance
        If INDCurrentAccountSavingsStart IsNot Nothing And INDCurrentAccountSavingsEnd IsNot Nothing Then
            criteriaEntityBanksAccount = "Code >= '" & INDCurrentAccountSavingsStart & "' And  Code <= '" & INDCurrentAccountSavingsEnd & "'"
            resultXpoEntityBanksAccount = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaEntityBanksAccount)
            For Each itemXpo In resultXpoEntityBanksAccount
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.EntityBankAccountId = itemXpo.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim resultList = (resultXpoEntityBanksAccount.Where(Function(x) x.Id = itemXpo.Id)).FirstOrDefault()
                    If resultList IsNot Nothing Then
                        If Not dictionaryBalance.ContainsKey("B" & itemXpo.Id) Then
                            Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                            If resulXpo.Count < 1 Then
                                ValueRow = 1
                            Else
                                ValueRow = resulXpo.Max(Function(x) x.Row)
                            End If
                            resulTresuryBalance.Row = ValueRow
                            resulTresuryBalance.DocumentDate = resultList.InitialDate
                            resulTresuryBalance.CashRegisterId = Nothing
                            resulTresuryBalance.EntityBankAccountId = resultList.Id
                            resulTresuryBalance.EntityBankAccountCode = resultList.Code
                            resulTresuryBalance.SaldoAnterior = resultList.InitialBalance
                            resulTresuryBalance.SumReceipts = 0
                            resulTresuryBalance.SumExpenditures = 0
                            resulTresuryBalance.Code = resultList.IdBank.Code
                            resulTresuryBalance.Name = resultList.IdBank.Name
                            resulTresuryBalance.Type = resultList.Type
                            resulTresuryBalance.Number = resultList.Number
                            resulTresuryBalance.CashRegisterCode = Nothing
                            resulTresuryBalance.NameCash = Nothing
                            resulTresuryBalance.MainAccount = resultList.IdMainAccount.Number
                            resulXpo.Add(resulTresuryBalance)
                            dictionaryBalance.Add("B" & resultList.Id, resultList.InitialBalance)
                        End If
                    End If
                End If
            Next

        End If

        'Cargar las cajas que no tengan ningun movimiento en treasuryBalance
        If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
            criteriaCashRegister = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "'"
            resultXpoCashRegister = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, criteriaCashRegister)
            For Each itemXpo In resultXpoCashRegister
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.CashRegisterId = itemXpo.Id)).ToList
                If ResultAccumulate.Count < 1 Then
                    Dim resultList = (resultXpoCashRegister.Where(Function(x) x.Id = itemXpo.Id)).FirstOrDefault()
                    If resultList IsNot Nothing Then
                        If Not dictionaryBalance.ContainsKey("C" & resultList.Id) Then
                            Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                            If resulXpo.Count < 1 Then
                                ValueRow = 1
                            Else
                                ValueRow = resulXpo.Max(Function(x) x.Row)
                            End If
                            resulTresuryBalance.Row = ValueRow
                            resulTresuryBalance.DocumentDate = resultList.InitialDate
                            resulTresuryBalance.CashRegisterId = resultList.Id
                            resulTresuryBalance.EntityBankAccountId = Nothing
                            resulTresuryBalance.SaldoAnterior = resultList.InitialBalance
                            resulTresuryBalance.SumReceipts = 0
                            resulTresuryBalance.SumExpenditures = 0
                            resulTresuryBalance.Code = Nothing
                            resulTresuryBalance.Name = Nothing
                            resulTresuryBalance.Type = Nothing
                            resulTresuryBalance.Number = Nothing
                            resulTresuryBalance.CashRegisterCode = resultList.Code
                            resulTresuryBalance.NameCash = resultList.Name
                            resulTresuryBalance.MainAccount = resultList.IdMainAccount.Number
                            resulXpo.Add(resulTresuryBalance)
                            dictionaryBalance.Add("C" & resultList.Id, resultList.InitialBalance)
                        End If
                    End If
                End If
            Next
        End If

        'retornar la lista
        Return resulXpo
    End Function

#End Region

#Region "ValuePreviousBalance Reporte Boletin Tesoreria"
    ''' <summary>
    ''' Lista todas los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ValuePreviousBalance(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDPreviousBalanceType As Integer) As Long

        Dim resulXpo As IList(Of TreasuryTreasuryBalance)

        'Definir Criteria
        Dim criteriaNoMovement As String = Nothing
        criteriaNoMovement = "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        
        If INDPreviousBalanceType = 1 Then
            If INDCurrentAccountSavingsStart IsNot Nothing And INDCurrentAccountSavingsEnd IsNot Nothing Then
                criteriaNoMovement &= " AND EntityBankAccountId.Code >= '" & INDCurrentAccountSavingsStart & "' And  EntityBankAccountId.Code <= '" & INDCurrentAccountSavingsEnd & "'"
            End If
        ElseIf INDPreviousBalanceType = 2 Then
            If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
                criteriaNoMovement &= " AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
            End If
        End If

        'cargar cuentas y cajas que tuvieron movimiento en el rango de fecha
        resulXpo = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaNoMovement)

        Dim ValuePrevioBalance As Decimal = 0
        'Cargar los saldos anteriores de las cuentas bancarias y las cajas

        If resulXpo.Count > 0 Then
            If INDPreviousBalanceType = 1 Then
                Dim dictionaryBalance As New Dictionary(Of String, Decimal)
                For Each itemxpo In resulXpo
                    If itemxpo.EntityBankAccountId IsNot Nothing Then
                        If Not dictionaryBalance.ContainsKey("B" & If(IsNothing(itemxpo.EntityBankAccountId) = True, 0, itemxpo.EntityBankAccountId.Id)) Then
                            Dim resultList = (resulXpo.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = If(IsNothing(itemxpo.EntityBankAccountId) = True, 0, itemxpo.EntityBankAccountId.Id))).ToList
                            If resultList.Count > 0 Then
                                Dim DateMin = resultList.Min(Function(x) x.DocumentDate)
                                Dim itemXpoTreasuryBalanceNoMovement = resulXpo.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = If(IsNothing(itemxpo.EntityBankAccountId) = True, 0, itemxpo.EntityBankAccountId.Id) And x.DocumentDate = DateMin).SingleOrDefault

                                ValuePrevioBalance += itemXpoTreasuryBalanceNoMovement.PreviousBalance
                                dictionaryBalance.Add("B" & itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id, itemXpoTreasuryBalanceNoMovement.PreviousBalance)
                            End If
                        End If
                    End If
                Next
            ElseIf INDPreviousBalanceType = 2 Then
                Dim dictionaryBalance As New Dictionary(Of String, Decimal)
                For Each itemxpo In resulXpo
                    If itemxpo.CashRegisterId IsNot Nothing Then
                        If Not dictionaryBalance.ContainsKey("C" & If(IsNothing(itemxpo.CashRegisterId) = True, 0, itemxpo.CashRegisterId.Id)) Then
                            Dim resultList = (resulXpo.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = If(IsNothing(itemxpo.CashRegisterId) = True, 0, itemxpo.CashRegisterId.Id))).ToList
                            If resultList.Count > 0 Then
                                Dim DateMin = resultList.Min(Function(x) x.DocumentDate)
                                Dim itemXpoTreasuryBalanceNoMovement = resulXpo.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = If(IsNothing(itemxpo.CashRegisterId) = True, 0, itemxpo.CashRegisterId.Id) And x.DocumentDate = DateMin).SingleOrDefault

                                ValuePrevioBalance += itemXpoTreasuryBalanceNoMovement.PreviousBalance
                                dictionaryBalance.Add("C" & itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id, itemXpoTreasuryBalanceNoMovement.PreviousBalance)
                            End If
                        End If
                    End If
                Next
            End If
        Else
            If INDPreviousBalanceType = 1 Then
                Dim criteriaBanks As String = "Code >= '" & INDCurrentAccountSavingsStart & "' And  Code <= '" & INDCurrentAccountSavingsEnd & "'"
                Dim resultBanks As IList(Of TreasuryEntityBankAccountsXpo) = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaBanks)
                ValuePrevioBalance = resultBanks.Select(Function(x) x.InitialBalance).Sum()

            ElseIf INDPreviousBalanceType = 2 Then
                Dim criteriaCash As String = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "'"
                Dim resultCash As IList(Of TreasuryCashRegistersXpo) = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, criteriaCash)
                ValuePrevioBalance = resultCash.Select(Function(x) x.InitialBalance).Sum()
            End If
        End If
        'retornar la lista
        Return ValuePrevioBalance
    End Function

#End Region

#Region "Reporte Estado de efectivo"
    ''' <summary>
    ''' Lista todas los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionReportStatusEfecty(ByVal INDDateStart As Date, ByVal INDDateEnd As Date) As List(Of TreasuryTreasuryBalance)

        Dim resulXpo As IList(Of TreasuryTreasuryBalance)

        'definir criteria
        Dim Criteria As String = Nothing
        Criteria = "GetDate(DocumentDate) >= #" & Format(INDDateStart, "yyyy-MM-dd") & "# And GetDate(DocumentDate) <= #" & Format(INDDateEnd, "yyyy-MM-dd") & "#"

        'cargar cuentas y cajas que tuvieron movimiento en el rango de fecha
        resulXpo = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, Criteria)

        Dim dictionaryBalance As New Dictionary(Of String, Long)
        For Each itemResulXpo In resulXpo
            If itemResulXpo.CashRegisterId Is Nothing Then
                If Not dictionaryBalance.ContainsKey("B" & itemResulXpo.EntityBankAccountId.Id) Then
                    Dim resultList = (resulXpo.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemResulXpo.EntityBankAccountId.Id)).ToList
                    If resultList.Count > 0 Then
                        'fecha maxima
                        Dim DateMax = resultList.Max(Function(x) x.DocumentDate)
                        Dim itemXpoTreasuryBalanceDateMax = resulXpo.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemResulXpo.EntityBankAccountId.Id And x.DocumentDate = DateMax).SingleOrDefault
                        'fecha minima
                        Dim DateMin = resultList.Min(Function(x) x.DocumentDate)
                        Dim itemXpoTreasuryBalanceDateMin = resulXpo.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemResulXpo.EntityBankAccountId.Id And x.DocumentDate = DateMin).SingleOrDefault
                        itemResulXpo.BalanceMonthStart = itemXpoTreasuryBalanceDateMin.PreviousBalance
                        If itemXpoTreasuryBalanceDateMax.Nature = 1 Then
                            itemResulXpo.BalanceMonthEnd = itemXpoTreasuryBalanceDateMax.PreviousBalance + itemXpoTreasuryBalanceDateMax.ValueMovement

                        Else
                            itemResulXpo.BalanceMonthEnd = itemXpoTreasuryBalanceDateMax.PreviousBalance - itemXpoTreasuryBalanceDateMax.ValueMovement

                        End If
                        dictionaryBalance.Add("B" & itemResulXpo.EntityBankAccountId.Id, itemResulXpo.PreviousBalance)
                    End If
                End If
            ElseIf itemResulXpo.EntityBankAccountId Is Nothing Then
                If Not dictionaryBalance.ContainsKey("C" & itemResulXpo.CashRegisterId.Id) Then
                    Dim resultList = (resulXpo.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemResulXpo.CashRegisterId.Id)).ToList
                    If resultList.Count > 0 Then
                        'fecha maximo
                        Dim DateMax = resultList.Max(Function(x) x.DocumentDate)
                        Dim itemXpoTreasuryBalanceDateMax = resulXpo.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemResulXpo.CashRegisterId.Id And x.DocumentDate = DateMax).SingleOrDefault
                        'fecha minima
                        Dim DateMin = resultList.Min(Function(x) x.DocumentDate)
                        Dim itemXpoTreasuryBalanceDateMin = resulXpo.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemResulXpo.CashRegisterId.Id And x.DocumentDate = DateMin).SingleOrDefault
                        itemResulXpo.BalanceMonthStart = itemXpoTreasuryBalanceDateMin.PreviousBalance
                        If itemXpoTreasuryBalanceDateMax.Nature = 1 Then
                            itemResulXpo.BalanceMonthEnd = itemXpoTreasuryBalanceDateMax.PreviousBalance + itemXpoTreasuryBalanceDateMax.ValueMovement

                        Else
                            itemResulXpo.BalanceMonthEnd = itemXpoTreasuryBalanceDateMax.PreviousBalance - itemXpoTreasuryBalanceDateMax.ValueMovement

                        End If
                        dictionaryBalance.Add("C" & itemResulXpo.CashRegisterId.Id, itemResulXpo.PreviousBalance)
                    End If
                End If
            End If
        Next
        'retornar la lista
        Return resulXpo
    End Function

#End Region

#End Region

End Class
