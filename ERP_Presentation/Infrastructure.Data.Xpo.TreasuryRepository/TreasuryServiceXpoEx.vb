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
Imports System.Dynamic
Imports System.Reflection

#End Region

''' <summary>
''' Clase que expone los servicios de los repositorios
''' </summary>
Public Class TreasuryServiceXpoEx
    Inherits XpoBaseService
    Implements IDisposable

#Region "Public Methods"

    ''' <summary>
    ''' Lista todos los fondos de caja menor
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConstitutionCashSmaller() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConstitutionCashSmallerXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ConstitutionCashSmallerXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;DocumentType;CashRegisterSmallerId.CodeName;SourceType;Status", Nothing)
        Return serverMode
    End Function

    Public Function GetCashReceiptAccountReceivableByCashReceiptDetailId(id As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of Treasury_CashReceiptAccountReceivable)()
        Dim classEntity = session.GetClassInfo(GetType(Treasury_CashReceiptAccountReceivable))
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CashReceiptDetailId=" & id)
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los proveedores
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSupplierReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonSuplierReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CommonSuplierReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdThirdParty.Nit;IdThirdParty.Name;Name;IdCity.Name;Status", Nothing)
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
        Dim session As New IndigoXPOSession(Of VoucherTransactionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(VoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CodeClass;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName;NoteNumber", criteria)
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
        Dim session As New IndigoXPOSession(Of TreasuryVoucherTransactionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CheckNumber;TransactionDate;Value;Status;IdThirdParty.NitName", criteria)
        serverMode.DefaultSorting = "CheckNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los reembolsos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRefundsReportTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryRefundsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryRefundsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InitialDate;FinalDate;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notas debito y credito de tesoreria por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryNotesXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryNotesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las consignaciones y transferencia de tesoreria por filtro
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVReportConsignmentTransferTreasuryByFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryVReportConsignmentTransfer)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryVReportConsignmentTransfer))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Type;DocumentDate;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los anticipos de pago
    ''' </summary>
    ''' <returns></returns>
    Public Function ListPaymentsAdvancesReportTreasury(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentsAdvancePaymentsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(PaymentsAdvancePaymentsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdSupplier.IdThirdParty.Nit;IdSupplier.IdThirdParty.Name;DocumentDate", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas de cruce
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCrossingAccountReportTreasuryFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryCrossingAccountXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryCrossingAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;ThirdPartyId.NitName", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene el concepto por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetExpenseConceptById(ExpenseConceptId As Integer) As XPCollection
        Dim session = New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Id=" & ExpenseConceptId)
        Dim collect As XPCollection = New XPCollection(session, GetType(ExpenseConceptXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los comprobantes de transacción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTranscationReportTreasuryFilter(ByVal filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryVoucherTransactionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;IdThirdParty.NitName;Status", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryEntityBankAccountsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryEntityBankAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank.Name;Number;IdMainAccount.Name", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de egreso que se pagaron con cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTranscationCheckReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryVoucherTransactionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdChecks is not null")
        Dim classEntity = session.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CheckNumber;TransactionDate;Value;Status;IdThirdParty.NitName", criteria)
        serverMode.DefaultSorting = "CheckNumber"
        Return serverMode
    End Function

    ''' <summary>
    ''' data source para listar todos los conceptos de los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListReceiptConceptsReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryCashReceiptConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryCashReceiptConceptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Nature;IdMainAccount.Number", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' data source para listar todos los conceptos de los comprobantes de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpensesConceptsReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryExpenseConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryExpenseConceptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;IdMainAccount.Number;Behavior", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' data source para listar todos los cheques cancelados de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCancellationChecksReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryCancellationChecksXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryCancellationChecksXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;CheckNumber;IdEntityAccount.IdBank.Name;IdEntityAccount.Number;IdEntityAccount.IdMainAccount.NumberName", Nothing)
        serverMode.DefaultSorting = "Id"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los reembolsos de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRefundsReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryRefundsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryRefundsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;InitialDate;FinalDate;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las consignaciones y transferencia de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVReportConsignmentTransferTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryVReportConsignmentTransfer)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryVReportConsignmentTransfer))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Type;DocumentDate;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las notas debito y credito de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryNotesXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryNotesXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NoteDate;Status;Nature", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThirdPartyReportTreasury() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CommonThirdPartyReportXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CommonThirdPartyReportXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Nit;Name;PersonType;ContributionType;RetentionType;State;PersonTypeName", Nothing)
        serverMode.DefaultSorting = "Nit"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los comprobantes de transacción
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTransactionReportTreasury() As XPInstantFeedbackSource
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)
        'Dim session = New Session(XpoDefault.DataLayer)
        'Dim collect As XPCollection(Of TreasuryVoucherTransactionXpo) = New XPCollection(Of TreasuryVoucherTransactionXpo)(session)
        'Return collect
        Dim session As New IndigoXPOSession(Of TreasuryVoucherTransactionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryVoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;IdThirdParty.NitName;Status", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    '''' <summary>
    ''''  Se recomienda usar XPO seguridad
    '''' Lista todos los usuarios de creación
    '''' </summary>
    '''' <returns></returns>
    'Public Function ListCreationUsersReportTreasury() As XPInstantFeedbackSource
    '    'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)
    '    'Dim session = New Session(XpoDefault.DataLayer)
    '    'Dim collect As XPCollection(Of SecurityUserXpo) = New XPCollection(Of SecurityUserXpo)(session)
    '    'Return collect
    '    Dim session As New IndigoXPOSession(Of SecurityUserXpo)()
    '    Dim classEntity = session.GetClassInfo(GetType(SecurityUserXpo))
    '    Dim serverMode = New XPInstantFeedbackSource(classEntity, "UserCode;IdPerson.Fullname", Nothing)
    '    serverMode.DefaultSorting = "UserCode"
    '    Return serverMode
    'End Function

    Public Function ListCrossingAccountReportTreasury() As XPInstantFeedbackSource
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)

        Dim session As New IndigoXPOSession(Of TreasuryCrossingAccountXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryCrossingAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;ThirdPartyId.NitName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashReceiptReport(ByVal Filtro As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryCashReceiptsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(Filtro)
        Dim classEntity = session.GetClassInfo(GetType(TreasuryCashReceiptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;Status;IdThirdParty.Nit;IdThirdParty.NitName;CurrencyAbbreviation", criteria)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccountsReport() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryEntityBankAccountsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryEntityBankAccountsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank.Name;Number;IdMainAccount.Name;IdMainAccount.NumberName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos las Cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegistersEntity() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryCashRegistersXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryCashRegistersXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;TypeName", Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the voucher transaction advance by voucher transaction detail identifier.
    ''' </summary>
    ''' <param name="voucherTransactionDetailId">The voucher transaction detail identifier.</param>
    ''' <returns></returns>
    Public Function ListVoucherTransactionAdvanceByVoucherTransactionDetailId(voucherTransactionDetailId As Integer) As XPCollection(Of VoucherTransactionAdvanceXpo)
        Dim session As New IndigoXPOSession(Of VoucherTransactionAdvanceXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("[IdVoucherTransactionD] == ?", voucherTransactionDetailId)
        Dim collect As XPCollection(Of VoucherTransactionAdvanceXpo) = New XPCollection(Of VoucherTransactionAdvanceXpo)(New Session(XpoDefault.DataLayer), session, criteria)
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
        Dim session = New IndigoXPOSession(Of VoucherTransactionAdvanceXpo)()
        Dim collect As XPCollection = New XPCollection(session, GetType(VoucherTransactionAdvanceXpo))
        Return collect
    End Function

    ''' <summary>
    ''' Listar todos los controles de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCheckCashingControl() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CheckCashingControlXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CheckCashingControlXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;DueDate;IdEntityAccount.IdBank.Name;IdEntityAccount.Number", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptsByBehavior(behavior As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior=" & behavior & "")
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;AffectBudget;Behavior;BehaviorName;IdMainAccount.NumberName;CodeName;NatureName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todos los cambios de cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCheckCashing() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CheckCashingXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CheckCashingXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CurrentCheckNumber;NextCheckNumber;CancellationDate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' lista todas las notas de tesoreria
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTreasuryNote() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of TreasuryNoteXpo)()
        Dim classEntity = session.GetClassInfo(GetType(TreasuryNoteXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas los cruces de cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCrossingAccount() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CrossingAccountXpo)()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" &  & "")
        Dim classEntity = session.GetClassInfo(GetType(CrossingAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;ThirdPartyId;ThirdPartyId.NitName;DocumentDate;Status;StatusName", Nothing)
        'Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas los cruces de cuentas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCrossingAccountConfirmed() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CrossingAccountXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=2")
        Dim classEntity = session.GetClassInfo(GetType(CrossingAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;ThirdPartyId;ThirdPartyId.NitName;DocumentDate;Status;StatusName", criteria)
        'Dim serverMode = New XPInstantFeedbackSource(classEntity)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptsByBehaviorAndIdMainAccount(behavior As Integer, IdMainAccount As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior=" & behavior & " and IdMainAccount.Id=" & IdMainAccount & "")
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;AffectBudget;Behavior;BehaviorName;IdMainAccount.NumberName;CodeName;NatureName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todos los reembolsos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListRefund() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RefundXpo)()
        Dim classEntity = session.GetClassInfo(GetType(RefundXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdCashRegister;InitialDate;FinalDate;Value;Status;StatusName;CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function
    ''' <summary>
    ''' lista los reembolsos para hacer traslados
    ''' </summary>
    ''' <param name="FilingUnitSourceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRefundByTransfer(FilingUnitSourceId As Integer) As XPCollection(Of RefundXpo)
        Dim session As New IndigoXPOSession(Of RefundXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Refunded = 0 and FilingUnitId=" & FilingUnitSourceId)
        Dim collect As XPCollection(Of RefundXpo) = New XPCollection(Of RefundXpo)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista los reembolsos para hacer traslados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListRefundByTrazability() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of RefundXpo)()
        Dim classEntity = session.GetClassInfo(GetType(RefundXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todos los reembolsos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConsignment() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConsignmentXpo)()
        Dim classEntity = session.GetClassInfo(GetType(ConsignmentXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;EntityBankAccountId;MainAccountId;Value;Status;StatusName;CodeName;EntityBankAccountId.IdBank.Name;EntityBankAccountId.Number;EntityBankAccountId.CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todos las consignaciones por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListConsignmentByStatus(state As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ConsignmentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        Dim classEntity = session.GetClassInfo(GetType(ConsignmentXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;DocumentDate;EntityBankAccountId;MainAccountId;Value;Status;StatusName;EntityBankAccountId.CodeBankName;EntityBankAccountId.CurrencyAbbreviation", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the schedule payment.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSchedulePayment() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SchedulePaymentXpo)()
        Dim classEntity = session.GetClassInfo(GetType(SchedulePaymentXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ScheduledDate;EntityBankAccountId;PaymentMethod;TaxByMil;Status;StatusName;StatusNameDispersionFund;AmountPaid;AmountPaidByCurrency", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the schedule payment.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListSchedulePaymentConfirm() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of SchedulePaymentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & 2 & " Or Status=" & 4 & " Or Status=" & 5)
        Dim classEntity = session.GetClassInfo(GetType(SchedulePaymentXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ScheduledDate;EntityBankAccountId;PaymentMethod;TaxByMil;Status;StatusName;StatusNameDispersionFund;AmountPaid;AmountPaidByCurrency", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los detalles de egresos por id del detalle del comprobante de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDischargeBillByIdVoucherTransactionDXpo(ByVal IdVoucherTransactionD As Integer) As XPCollection
        Dim session As New IndigoXPOSession(Of DischargeBillXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdVoucherTransactionD=" & IdVoucherTransactionD)
        Dim collect As XPCollection = New XPCollection(session, GetType(DischargeBillXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' lista todos los extractos bancarios
    ''' </summary>
    ''' <returns></returns>
    Public Function ListUploadBankStatements() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of UploadBankStatementsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(UploadBankStatementsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, Nothing)
        serverMode.DefaultSorting = "Code"
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCard(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CardXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(CardXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;IdRetentionConceptCommision;IdRetentionConceptRTF;IdRetentionConceptICA;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashReceiptConcept(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashReceiptConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse($"Status={status} AND CashReceiptConceptUsersXpo[UserId={SessionValues.Instance.UserIndigoId}]")
        Dim classEntity = session.GetClassInfo(GetType(CashReceiptConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Nature;Affectation;AffectationName;CodeName;IdMainAccount;IdMainAccount.NumberName;IdCashFlowConcept;IdCashFlowConcept.CodeName;", criteria)
        Return serverMode
    End Function

    Public Function ListAllCashReceiptConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashReceiptConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CashReceiptConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Nature;Affectation;AffectationName;CodeName;IdMainAccount;IdMainAccount.NumberName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Listar todas las tarjetas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashReceiptConceptCollection(status As Boolean) As XPCollection(Of CashReceiptConceptXpo)
        Dim session As New IndigoXPOSession(Of CashReceiptConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(CashReceiptConceptXpo))
        Return New XPCollection(Of CashReceiptConceptXpo)(session, criteria)
    End Function


    ''' <summary>
    ''' lista los documentos de control por tipo de documento
    ''' </summary>
    ''' <returns></returns>
    Public Function ListTreasuryControlByDocumentType(documentType As Integer) As XPCollection(Of TreasuryControlXpo)
        Dim session As New IndigoXPOSession(Of TreasuryControlXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("DocumentType=" & documentType & "")
        Dim classEntity = session.GetClassInfo(GetType(TreasuryControlXpo))
        Return New XPCollection(Of TreasuryControlXpo)(session, criteria)
    End Function



    ''' <summary>
    ''' Lists the cash receipt concept by affectation.
    ''' </summary>
    Public Function ListCashReceiptConceptByAffectation(status As Boolean, affectation As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashReceiptConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & " AND Affectation=" & affectation)
        Dim classEntity = session.GetClassInfo(GetType(CashReceiptConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Name;Code;Nature;Affectation;CodeName;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashRegister(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashRegisterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(CashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdCostCenter;CodeName;IdMainAccount;IdMainAccount.NumberName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los registros de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllCashRegister() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashRegisterXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdCostCenter;CodeName;IdMainAccount;IdMainAccount.NumberName;CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the type of the cash register by.
    ''' </summary>
    ''' <param name="Type">The type.</param>
    ''' <returns></returns>
    Public Function ListCashRegisterByType(Type As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashRegisterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=" & Type & "")
        Dim classEntity = session.GetClassInfo(GetType(CashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdMainAccount;IdCostCenter;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the type of the cash register by.
    ''' </summary>
    ''' <param name="Type">The type.</param>
    ''' <returns></returns>
    Public Function ListCashRegisterByTypeAndStatus(Type As Integer, Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashRegisterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Type=" & Type & " And Status=" & Status)
        Dim classEntity = session.GetClassInfo(GetType(CashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdMainAccount;IdCostCenter;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the type of the cash register by.
    ''' </summary>
    ''' <param name="Type">The type.</param>
    ''' <returns></returns>
    Public Function ListCashRegisterByCurrencyTypeAndStatus(currencyId As Integer, Type As Integer, Status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashRegisterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("Type={0} And Status={1} And CurrencyId={2}", Type, Status, currencyId))
        Dim classEntity = session.GetClassInfo(GetType(CashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Type;InitialBalance;InitialDate;RefundDate;AmountMax;AmountMin;CurrentBalance;IdMainAccount;IdCostCenter;CodeName;Status", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egresos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConcept(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;IdMainAccount;AffectBudget;IdMainAccount.NumberName;Behavior;IdMainAccount.Number;NatureName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los conceptos de egreso por id de caja
    ''' </summary>
    ''' <param name="IdCash">The identifier cash.</param>
    ''' <returns></returns>
    Public Function ListExpenseConceptCashRegisterByCash(IdCash As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptCashRegisterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdCashRegister=" & IdCash & "")
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptCashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdExpenseConcept;IdExpenseConcept.CodeName;IdExpenseConcept.Id;IdExpenseConcept.Code;IdExpenseConcept.Description;IdCashRegister;IdCashRegister.CodeName", criteria)
        Return serverMode
    End Function

    'Public Function ListExpenseConceptByCash(IdCash As Integer) As XPInstantFeedbackSource
    '    Dim session = New Session(XpoDefault.DataLayer)
    '    Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdCashRegister=" & IdCash & "")
    '    Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
    '    Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;IdAccount;AffectBudget;IdAccount.NumberName;Behavior", criteria)
    '    Return serverMode
    'End Function

    ''' <summary>
    ''' Lista todas las cajas distintas a la elegida
    ''' </summary>
    ''' <param name="IdCash">The identifier cash.</param>
    ''' <returns></returns>
    Public Function ListExpenseConceptCashRegisterByNotCash(IdCash As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptCashRegisterXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("IdCashRegister<>" & IdCash & "")
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptCashRegisterXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdExpenseConcept;IdExpenseConcept.CodeName;IdExpenseConcept.Id;IdExpenseConcept.Code;IdExpenseConcept.Description;IdCashRegister;IdCashRegister.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de nota
    ''' </summary>
    ''' <returns></returns>
    Public Function ListNoteConcept(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of NoteConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(NoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;AffectBudget;AutoCollect;Nature;NatureName;NatureValue;IdMainAccount;IdMainAccount.Id;IdMainAccount.NumberName;CodeName; _
            IdCashFlowConcept;IdCashFlowConcept.CodeName", criteria)
        Return serverMode
    End Function

    Public Function ListAllNoteConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of NoteConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(NoteConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;AffectBudget;AutoCollect;Nature;NatureName;NatureValue;IdMainAccount;IdMainAccount.Id;IdMainAccount.NumberName;CodeName; _
            IdCashFlowConcept;IdCashFlowConcept.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las reclasificaciones de conceptos de flujo de efectivo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashFlowReclassification() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashFlowReclassificationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CashFlowReclassificationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;DocumentDate;DocumentType;DocumentName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de flujo de efectivo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashFlowConcept() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashFlowConceptXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CashFlowConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NameConcept;StatusConcept;StatusName;CodeName;CreationDate", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los conceptos de flujo de efectivo, parameter(0) es el estado:1,0; parameter(1) es el tipo:1(ingreso),2(egreso)
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCashFlowConcept(ByVal parameters As String()) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashFlowConceptXpo)()
        Dim filter As String = String.Empty
        If parameters(0) IsNot Nothing Then
            filter = String.Format("StatusConcept = {0}", parameters(0))
        End If
        If parameters(1) IsNot Nothing Then
            filter = String.Format("{0}{1} TypeConcept in ({2})", filter, IIf(String.IsNullOrEmpty(filter), "", " AND "), parameters(1))
        End If
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(IIf(String.IsNullOrEmpty(filter), "1 = 1", filter))
        Dim classEntity = session.GetClassInfo(GetType(CashFlowConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;NameConcept;StatusConcept;StatusName;CodeName;TypeConcept;TypeConceptName;Activity;ActivityName;CreationDate", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashFlowConceptById(ByVal id As Integer) As CashFlowConceptXpo
        Dim session As New IndigoXPOSession(Of CashFlowConceptXpo)()
        Return session.GetObjectByKey(Of CashFlowConceptXpo)(id)
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de terceros
    ''' </summary>
    ''' <returns></returns>
    Public Function ListThridPartyBankAccount(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ThridPartyBankAccountXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(ThridPartyBankAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Type;Number;IdThirParty;IdBank;IdRadicationCity;IdBankCity;IdThirParty.Name", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de terceros por id del banco
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccountByBank(status As Boolean, bankId As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EntityBankAccountXpo)()
        Dim criteria As CriteriaOperator
        If bankId <> 0 Then
            criteria = CriteriaOperator.Parse("Status = " & status & " AND IdBank.Id = " & bankId)
        Else
            criteria = CriteriaOperator.Parse("Status = " & status)
        End If
        Dim classEntity = session.GetClassInfo(GetType(EntityBankAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllEntityBankAccount() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EntityBankAccountXpo)()
        Dim classEntity = session.GetClassInfo(GetType(EntityBankAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount;CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccount(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EntityBankAccountXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(EntityBankAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount", criteria)
        Return serverMode
    End Function

    Public Function ListEntityBankAccountByCurrency(currencyId As Integer, status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EntityBankAccountXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("Status={0} And CurrencyId = {1}", status, currencyId))
        Dim classEntity = session.GetClassInfo(GetType(EntityBankAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las cuentas bancarias de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEntityBankAccountByStatusAndConciliationAccount(status As Boolean, conciliationAccount As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of EntityBankAccountXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = " & status & " And IdMainAccount.ReconcileAccount = " & conciliationAccount)
        Dim classEntity = session.GetClassInfo(GetType(EntityBankAccountXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Status;IdBank;IdBank.Name;IdCity;Type;Number;IdCity.Name;InitialBalance;TypeName;Rate;Quota;InitialDate;IdMainAccount;CurrentBalance;IdCostCenter;CodeName;IdMainAccount.NumberName;CodeBankAccount;IdMainAccount.ReconcileAccount;FinancialSourceId.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Obtiene una cuenta bancaria por su id
    ''' </summary>
    ''' <param name="id">Id de la cuenta bancaria</param>
    ''' <returns>Cuenta bancaria</returns>
    Public Function GetEntityBankAccountById(ByVal id As Integer) As EntityBankAccountXpo
        Dim session As New IndigoXPOSession(Of EntityBankAccountXpo)()
        Return session.GetObjectByKey(Of EntityBankAccountXpo)(id)
    End Function

    ''' <summary>
    ''' Lists all bank.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllBank(status As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BankXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("State=" & status & "")
        Dim classEntity = session.GetClassInfo(GetType(BankXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;ThirdPartyId;Name;AccountNumber;AccountType;AchCode;BankFileCode;State;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pagos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllPaymentConcept(ByVal state As Boolean) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of PaymentConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        Dim classEntity = session.GetClassInfo(GetType(PaymentConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las chequeras canceladas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllCancellationCheck() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CancellationCheckXpo)()
        Dim classEntity = session.GetClassInfo(GetType(CancellationCheckXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;IdEntityAccount;IdEntityAccount.Id;CancellationDate;CheckNumber;Description;IdEntityAccount.CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de egreso
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllVoucherTransaction() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of VoucherTransactionXpo)()
        'Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        Dim classEntity = session.GetClassInfo(GetType(VoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CodeClass;VoucherClass;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName;NoteNumber;CurrencyAbbreviation", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista los comprobantes de egreso por estado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllVoucherTransactionByStatus(state As Byte) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of VoucherTransactionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        Dim classEntity = session.GetClassInfo(GetType(VoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName;CurrencyAbbreviation", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists all cash receipt by status.
    ''' </summary>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Function ListAllCashReceiptByStatus(state As Integer) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashReceiptsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status=" & state & "")
        Dim classEntity = session.GetClassInfo(GetType(CashReceiptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Detail;PaymentResponsibles;DocumentDate;Value;IdThirdParty.NitName;CollectType;CurrencyAbbreviation;CollectionTypeDescription", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Cash receipts to be submitted 
    ''' </summary>
    ''' <param name="state">The state.</param>
    ''' <returns></returns>
    Public Function ListCashReceiptsToPost(
    entityBankAccountId As Integer,
    startDate As Date,
    endDateInclusive As Date
) As XPCollection(Of TreasuryCashReceiptsXpo)

        Dim session As New IndigoXPOSession(Of TreasuryCashReceiptsXpo)()

        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(
        "Status = 2 " &
        "AND IdBankAccount.Id = ? " &
        "AND DocumentDate >= ? AND DocumentDate < ? " &
        "AND TreasuryPaymentMethodsXpo[PaymentMethodTypes = 3]",
        entityBankAccountId, startDate, endDateInclusive)

        Dim collection As New XPCollection(Of TreasuryCashReceiptsXpo)(session, criteria)
        collection.Sorting.Add(New SortProperty("Code", DevExpress.Xpo.DB.SortingDirection.Ascending))
        Return collection
    End Function

    ''' <summary>
    ''' Lista los recibos de caja tipo de recaudo cuenta bancaria
    ''' </summary>
    ''' <returns></returns>
    Function ListAllCashReceiptByBankAccount() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of CashReceiptsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("CollectType=2")
        Dim classEntity = session.GetClassInfo(GetType(CashReceiptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, Nothing, criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists all voucher transaction with voucher type check.
    ''' </summary>
    Public Function ListAllVoucherTransactionWithVoucherTypeCheck() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of VoucherTransactionXpo)()
        Dim one As Integer = 1
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("ExpenseType=" & one & " and PaymentMethod=" & one & " and Status!=3")
        Dim classEntity = session.GetClassInfo(GetType(VoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Detail;Value;IdThirdParty.Name;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists all voucher transaction with voucher type check.
    ''' </summary>
    Public Function ListAllVoucherTransactionWithVoucherTypeCheck(ByVal parametros As String()) As XPCollection(Of TreasuryVoucherTransactionXpo)

        Dim collect As XPCollection(Of TreasuryVoucherTransactionXpo) = Nothing
        Dim session As New IndigoXPOSession(Of TreasuryVoucherTransactionXpo)()
        'ExpenseType= 1: cuenta bancaria
        'PaymentMethod=1: cheque 
        'Status in (2,4): confirmada, reversado
        'IdCheckCashingStatus: estado de cheque
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("ExpenseType = 1 and PaymentMethod = 1 and Status in (2,4) and IdEntityBankAccount.Id = {0} and DocumentDate <= '{1}' and IdCheckCashingStatus in ({2})",
                                                                                parametros(0), parametros(1), parametros(2)))
        collect = New XPCollection(Of TreasuryVoucherTransactionXpo)(session, criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lists all voucher transaction by ids
    ''' </summary>
    Public Function GetVoucherTransactionByIds(ByVal parametros As String) As XPCollection(Of TreasuryVoucherTransactionXpo)

        Dim collect As XPCollection(Of TreasuryVoucherTransactionXpo) = Nothing
        Dim session As New IndigoXPOSession(Of TreasuryVoucherTransactionXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(String.Format("Id in ({0})", parametros))
        collect = New XPCollection(Of TreasuryVoucherTransactionXpo)(session, criteria)
        Return collect
    End Function


    ''' <summary>
    ''' Lists the expense concept by cash1.
    ''' </summary>
    ''' <param name="cashRegisterId">The cash register identifier.</param>
    ''' <returns></returns>
    Public Function ListExpenseConceptByCash1(ByVal cashRegisterId As String) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("(ExpenseConceptCashRegisterXpo[IdCashRegister.Id = ?] or Behavior = 6) AND Status = 1", cashRegisterId)
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;NatureName;IdMainAccount;IdMainAccount.Id;AffectBudget;Behavior;BehaviorName;Status;CodeName;IdMainAccount.NumberName; _
            IdCashFlowConcept;IdCashFlowConcept.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the expense concept by major cash.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptByMajorCash() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior = 4 or Behavior = 6 AND Status = 1")
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;NatureName;IdMainAccount;IdMainAccount.Id;AffectBudget;Behavior;BehaviorName;Status;CodeName;IdMainAccount.NumberName; _
            IdCashFlowConcept;IdCashFlowConcept.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lists the expense concept by not cash1.
    ''' </summary>
    ''' <returns></returns>
    Public Function ListExpenseConceptByNotCash1(HandlesDocumentSupport As Boolean?) As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of ExpenseConceptXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Behavior != 2 AND Behavior != 7 AND Status = 1")
        If HandlesDocumentSupport IsNot Nothing AndAlso HandlesDocumentSupport Then
            criteria = CriteriaOperator.Parse("Behavior IN(4,6) AND Status = 1")
        End If
        Dim classEntity = session.GetClassInfo(GetType(ExpenseConceptXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Description;Nature;NatureName;IdMainAccount;IdMainAccount.Id;AffectBudget;Behavior;BehaviorName;Status;CodeName;IdMainAccount.NumberName; _
            IdCashFlowConcept;IdCashFlowConcept.CodeName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista las detalles de la interfaz presupuestal de una programación de pagos
    ''' </summary>
    ''' <param name="SchedulePaymentDetailId"></param>
    ''' <returns></returns>
    Public Function ListSchedulePaymentDetailBudgetBySchedulePaymentDetailId(SchedulePaymentDetailId As Integer) As XPCollection
        Dim filters As String = "SchedulePaymentDetailId = " & SchedulePaymentDetailId
        Dim session As New IndigoXPOSession(Of SchedulePaymentDetailBudgetXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filters)
        Dim collect As XPCollection = New XPCollection(session, GetType(SchedulePaymentDetailBudgetXpo), criteria)
        Return collect
    End Function

    ''' <summary>
    ''' Lista todas las reclasificaciones de conceptos de flujo de efectivo
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBankReconciliation() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BankReconciliationXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BankReconciliationXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;EntityBankAccountId.CodeBankAccount;DocumentDate;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Lista todas las conciliaciones automaticas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListBankReconciliationAutomatic() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BankReconciliationAutomaticXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BankReconciliationAutomaticXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;EntityBankAccountId.CodeBankAccount;DocumentDate;Status;StatusName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta el listado de conceptos de conciliación bancaria
    ''' </summary>
    ''' <returns></returns> 
    Public Function ListBankConciliationConcepts() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of BankConciliationConceptsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(BankConciliationConceptsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los comprobantes de egreso con filtro a Bank account
    ''' </summary>
    ''' <returns></returns>
    Public Function ListVoucherTransaction() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("VoucherClass = 1 AND IdEntityBankAccount IS NOT NULL ")
        Dim session As New IndigoXPOSession(Of VoucherTransactionXpo)()
        Dim classEntity = session.GetClassInfo(GetType(VoucherTransactionXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;CodeClass;VoucherClass;Detail;IdThirdParty;IdMainAccount;IdCostCenter;DocumentDate;Value;IdThirdParty.NitName;Status;StatusName;TaxByMilValue;VoucherClassName;VoucherClassName", criteria)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los convenios de redención de puntos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgreementsRedemptionPoints() As XPInstantFeedbackSource
        Dim session As New IndigoXPOSession(Of AgreementsRedemptionPointsXpo)()
        Dim classEntity = session.GetClassInfo(GetType(AgreementsRedemptionPointsXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;Status;CodeName", Nothing)
        Return serverMode
    End Function

    ''' <summary>
    ''' Consulta los convenios de redención de puntos por id
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAgreementsRedemptionPointsById(AgreementsRedemptionPointsId As Integer) As XPCollection
        Dim filters As String = "Id = " & AgreementsRedemptionPointsId
        Dim session As New IndigoXPOSession(Of AgreementsRedemptionPointsXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse(filters)
        Dim collect = New XPCollection(session, GetType(AgreementsRedemptionPointsXpo), criteria)
        Return collect
    End Function
    ''' <summary>
    ''' Consulta las actividades economicas que generen ingresos
    ''' </summary>
    ''' <returns></returns>
    Public Function ListEconomicActivity() As XPInstantFeedbackSource
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Status = 1 AND IsIncomeGenerating = 1 ")
        Dim session As New IndigoXPOSession(Of EconomicActivityXpo)()
        Dim classEntity = session.GetClassInfo(GetType(EconomicActivityXpo))
        Dim serverMode = New XPInstantFeedbackSource(classEntity, "Id;Code;Name;IsIncomeGenerating;CodeName", criteria)
        Return serverMode
    End Function

#End Region

#Region "LinqFeedBackSource"

#Region "SchedulePayment"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function GetSchedulePayment() As LinqInstantFeedbackSource
        Dim vlinqSchedulePayment As New LinqInstantFeedbackSource
        AddHandler vlinqSchedulePayment.GetQueryable, AddressOf OnGetQueryableSchedulePayment
        AddHandler vlinqSchedulePayment.DismissQueryable, AddressOf DismissQueryableSchedulePayment
        vlinqSchedulePayment.KeyExpression = "Name"
        Return vlinqSchedulePayment
    End Function

    Private Sub OnGetQueryableSchedulePayment(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim _supplier As XPQuery(Of Supplier) = New XPQuery(Of Supplier)(session)
            Dim _supplierDistributionLines As XPQuery(Of SuppliersDistributionLinesXpo) = New XPQuery(Of SuppliersDistributionLinesXpo)(session)
            Dim _distributionLines As XPQuery(Of DistributionLinesXpo) = New XPQuery(Of DistributionLinesXpo)(session)
            Dim _accountPayable As XPQuery(Of AccountPayableXpo) = New XPQuery(Of AccountPayableXpo)(session)

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
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableSchedulePayment(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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

    Private _idUser As Integer
    Private _status As Boolean
    Private _type As Integer

    Public Function ListCashRegisterByUser(idUser As Integer, type As Integer, status As Boolean)
        Dim vlinqCashRegister As New LinqInstantFeedbackSource
        AddHandler vlinqCashRegister.GetQueryable, AddressOf OnGetQueryableCashRegister
        AddHandler vlinqCashRegister.DismissQueryable, AddressOf DismissQueryableCashRegister
        _idUser = idUser
        _status = status
        _type = type
        Return vlinqCashRegister
    End Function

    Private Sub OnGetQueryableCashRegister(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableCashRegister As XPQuery(Of CashRegisterXpo) = New XPQuery(Of CashRegisterXpo)(session)
            Dim tableCashRegisterUser As XPQuery(Of CashRegisterUserXpo) = New XPQuery(Of CashRegisterUserXpo)(session)
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
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashRegister(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "EntityBankAccount LinqInstantFeedBackSource"

    Private _filterCodUser As String
    Private _filterStatus As Boolean
    Private _filterCurrencyId As Integer

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListEntityBankAccountByUser(ByVal codUser As String, ByVal status As Boolean, Optional ByVal currencyId As Integer = 0) As LinqInstantFeedbackSource
        Dim vlinqEntityBankAccount As New LinqInstantFeedbackSource
        AddHandler vlinqEntityBankAccount.GetQueryable, AddressOf OnGetQueryableEntityBankAccount
        AddHandler vlinqEntityBankAccount.DismissQueryable, AddressOf DismissQueryableEntityBankAccount
        _filterCodUser = codUser
        _filterStatus = status
        _filterCurrencyId = currencyId
        vlinqEntityBankAccount.KeyExpression = "Id"
        Return vlinqEntityBankAccount
    End Function

    Private Sub OnGetQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableEntityBankAccount As XPQuery(Of EntityBankAccountXpo) = New XPQuery(Of EntityBankAccountXpo)(session)
            Dim tableEntityBankAccountUser As XPQuery(Of EntityBankAccountUserXpo) = New XPQuery(Of EntityBankAccountUserXpo)(session)
            Dim TmpQueryableSource = From T1 In tableEntityBankAccount
                                     Join T2 In tableEntityBankAccountUser On T2.IdEntityBankAccount Equals T1.Id
                                     Where T2.CodUser.Equals(_filterCodUser) And T1.Status = _filterStatus And (_filterCurrencyId = 0 Or T1.CurrencyId = _filterCurrencyId)
                                     Select T1

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableEntityBankAccountUser
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub

    Private Sub DismissQueryableEntityBankAccount(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ExpenseConcept LinqInstantFeedBackSource"

    Private _filterExpenseConcept As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListExpenseConceptByCash(ByVal filter As String) As LinqInstantFeedbackSource
        Dim vlinqExpenseConcept As New LinqInstantFeedbackSource
        AddHandler vlinqExpenseConcept.GetQueryable, AddressOf OnGetQueryableExpenseConcept
        AddHandler vlinqExpenseConcept.DismissQueryable, AddressOf DismissQueryableExpenseConcept
        _filterExpenseConcept = CInt(filter)
        vlinqExpenseConcept.KeyExpression = "IdCashRegister"
        Return vlinqExpenseConcept
    End Function

    Private Sub OnGetQueryableExpenseConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableExpenseConceptCashRegister As XPQuery(Of ExpenseConceptCashRegisterXpo) = New XPQuery(Of ExpenseConceptCashRegisterXpo)(session)
            Dim tableExpenseConcept As XPQuery(Of ExpenseConceptXpo) = New XPQuery(Of ExpenseConceptXpo)(session)
            Dim TmpQueryableSource = From T1 In tableExpenseConcept
                                     Join T2 In tableExpenseConceptCashRegister On T2.IdExpenseConcept.Id Equals T1.Id
                                     Where T2.IdCashRegister.Id = _filterExpenseConcept
                                     Select T1 'T1.Id, T2.RadicatedConsecutive, T1.InvoiceNumber, NitName = T2.CustomerId.NitName.Trim, T2.DocumentDate
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableExpenseConceptCashRegister
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableExpenseConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ExpenseConcept LinqInstantFeedBackSource"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListExpenseConceptByNotCash() As LinqInstantFeedbackSource
        Dim vlinqExpenseConceptByNotCash As New LinqInstantFeedbackSource
        AddHandler vlinqExpenseConceptByNotCash.GetQueryable, AddressOf OnGetQueryableExpConcept
        AddHandler vlinqExpenseConceptByNotCash.DismissQueryable, AddressOf DismissQueryableExpConcept
        vlinqExpenseConceptByNotCash.KeyExpression = "Id"
        Return vlinqExpenseConceptByNotCash
    End Function

    Private Sub OnGetQueryableExpConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableExpenseConcept As XPQuery(Of ExpenseConceptXpo) = New XPQuery(Of ExpenseConceptXpo)(session)
            Dim TmpQueryableSource = From T1 In tableExpenseConcept
                                     Where T1.Behavior <> 2 AndAlso T1.Behavior <> 7
                                     Select T1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableExpenseConcept
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableExpConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "ListExpenseConceptEndorsement"

    Private _behavior As Integer
    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListExpenseConceptEndorsement(ByVal Behavior As Integer) As LinqInstantFeedbackSource
        Dim vlinqExpenseConceptEndorsement As New LinqInstantFeedbackSource
        AddHandler vlinqExpenseConceptEndorsement.GetQueryable, AddressOf OnGetQueryableExpConceptEndorsement
        AddHandler vlinqExpenseConceptEndorsement.DismissQueryable, AddressOf DismissQueryableExpConceptEndorsement
        _behavior = Behavior
        vlinqExpenseConceptEndorsement.KeyExpression = "Id"
        Return vlinqExpenseConceptEndorsement
    End Function

    Private Sub OnGetQueryableExpConceptEndorsement(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableExpenseConcept As XPQuery(Of ExpenseConceptXpo) = New XPQuery(Of ExpenseConceptXpo)(session)
            Dim TmpQueryableSource = From T1 In tableExpenseConcept
                                     Where T1.Behavior = _behavior
                                     Select T1
            e.QueryableSource = TmpQueryableSource
            e.Tag = tableExpenseConcept
            'End Using

        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableExpConceptEndorsement(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "PaymentConcept LinqInstantFeedBackSource"

    ''' <summary>
    ''' Obtiene todos los empleados
    ''' </summary>
    Public Function ListPaymentConcept() As LinqInstantFeedbackSource
        Dim vlinqPaymentConcept As New LinqInstantFeedbackSource
        AddHandler vlinqPaymentConcept.GetQueryable, AddressOf OnGetQueryablePaymentConcept
        AddHandler vlinqPaymentConcept.DismissQueryable, AddressOf DismissQueryablePaymentConcept
        vlinqPaymentConcept.KeyExpression = "Id"
        Return vlinqPaymentConcept
    End Function

    Private Sub OnGetQueryablePaymentConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)

            Dim tablePaymentConcept As XPQuery(Of PaymentConceptXpo) = New XPQuery(Of PaymentConceptXpo)(session)
            Dim TmpQueryableSource = From T1 In tablePaymentConcept
                                     Where T1.Status = True
                                     Select T1
            'Dim prueba = TmpQueryableSource.ToList
            e.QueryableSource = TmpQueryableSource
            e.Tag = tablePaymentConcept
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryablePaymentConcept(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "CashReceipts LinqInstantFeedBackSource"

    ''' <summary>
    ''' Obtiene todos los recibos de caja
    ''' </summary>
    Public Function ListCashReceipts() As LinqInstantFeedbackSource
        Dim vlinqCashReceipts As New LinqInstantFeedbackSource
        AddHandler vlinqCashReceipts.GetQueryable, AddressOf OnGetQueryableCashReceipts
        AddHandler vlinqCashReceipts.DismissQueryable, AddressOf DismissQueryableCashReceipts
        vlinqCashReceipts.KeyExpression = "Id"
        Return vlinqCashReceipts
    End Function

    Private Sub OnGetQueryableCashReceipts(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableCashReceipts As XPQuery(Of CashReceiptsXpo) = New XPQuery(Of CashReceiptsXpo)(session)
            Dim tableThirdParty As XPQuery(Of CommonThirdPartyXpo) = New XPQuery(Of CommonThirdPartyXpo)(session)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(session)
            Dim tmpQueryableSource = From cr In tableCashReceipts
                                     Join tp In tableThirdParty On cr.IdThirdParty.Id Equals tp.Id
                                     Join ma In tableMainAccount On cr.IdMainAccount Equals ma.Id
                                     Select Id = cr.Id, Code = cr.Code, CollectType = If(cr.CollectType = 1, ResourceManager.GetString("ExpenseTypeCash", "Treasury"), ResourceManager.GetString("ExpenseTypeBankAccount", "Treasury")), DocumentDate = cr.DocumentDate, Value = cr.Value, NumberName = ma.NumberName, NitName = tp.NitName, CurrencyAbbreviation = cr.CurrencyAbbreviation, StatusName = If(cr.Status = 1, ResourceManager.GetString("StateUnconfirmed"), If(cr.Status = 2, ResourceManager.GetString("StateConfirmed"), ResourceManager.GetString("StatusCanceled")))

            e.QueryableSource = tmpQueryableSource
            e.Tag = tableCashReceipts
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceipts(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
        Try
            'Dispose of the DataContext 
            CType(e.Tag, Object).Dispose()
        Catch ex As Exception
            ex.Message.ToString()
        End Try
    End Sub
#End Region

#Region "CashReceiptConceptsByRetentionType"

    Private _retentionType As Integer

    Public Function ListCashReceiptConceptsByRetentionType(retentionType As Integer, status As Boolean)
        Dim vlinqCashReceiptConceptsByRetentionType As New LinqInstantFeedbackSource
        AddHandler vlinqCashReceiptConceptsByRetentionType.GetQueryable, AddressOf OnGetQueryableCashReceiptsByRetentionType
        AddHandler vlinqCashReceiptConceptsByRetentionType.DismissQueryable, AddressOf DismissQueryableCashReceiptsByRetentionType
        _retentionType = retentionType
        _status = status
        Return vlinqCashReceiptConceptsByRetentionType
    End Function

    Private Sub OnGetQueryableCashReceiptsByRetentionType(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableCashReceiptConcetp As XPQuery(Of CashReceiptConceptXpo) = New XPQuery(Of CashReceiptConceptXpo)(session)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(session)
            Dim TmpQueryableSource = Nothing

            TmpQueryableSource = From crc In tableCashReceiptConcetp
                                 Join ma In tableMainAccount On ma.Id Equals crc.IdMainAccount.Id
                                 Where ma.RetencionType = _retentionType And crc.Status = _status
                                 Select crc

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCashReceiptConcetp
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceiptsByRetentionType(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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
    Private _handlesCosteCenter As Boolean

    Public Function ListCashReceiptConceptWithOutCostCenter(handlesCosteCenter As Boolean, status As Boolean)
        Dim vlinqCashReceiptConceptWithOutCostCenter As New LinqInstantFeedbackSource
        AddHandler vlinqCashReceiptConceptWithOutCostCenter.GetQueryable, AddressOf OnGetQueryableCashReceiptConceptWithOutCostCenter
        AddHandler vlinqCashReceiptConceptWithOutCostCenter.DismissQueryable, AddressOf DismissQueryableCashReceiptConceptWithOutCostCenter
        _handlesCosteCenter = handlesCosteCenter
        _status = status
        Return vlinqCashReceiptConceptWithOutCostCenter
    End Function

    Private Sub OnGetQueryableCashReceiptConceptWithOutCostCenter(sender As Object, e As GetQueryableEventArgs)
        Try
            Dim session As New Session(XpoDefault.DataLayer)
            Dim tableCashReceiptConcetp As XPQuery(Of CashReceiptConceptXpo) = New XPQuery(Of CashReceiptConceptXpo)(session)
            Dim tableMainAccount As XPQuery(Of PUCServiceXpo) = New XPQuery(Of PUCServiceXpo)(session)
            Dim TmpQueryableSource = Nothing

            TmpQueryableSource = From crc In tableCashReceiptConcetp
                                 Join ma In tableMainAccount On ma.Id Equals crc.IdMainAccount.Id
                                 Where ma.HandlesCostCenter = _handlesCosteCenter And crc.Status = _status
                                 Select crc

            e.QueryableSource = TmpQueryableSource
            e.Tag = tableCashReceiptConcetp
            'End Using
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DismissQueryableCashReceiptConceptWithOutCostCenter(ByVal sender As Object, ByVal e As GetQueryableEventArgs)
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
        Dim resulXpoNotMovement As IList(Of TreasuryCashRegistersXpo)
        Dim session As New Session(XpoDefault.DataLayer)
        'Definir Criteria
        Dim criteria As String = Nothing
        Dim criteriaPreviousBalance
        Dim criteriaNotMovement As String = Nothing
        criteria = "DocumentDate >= #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDateEnd, "yyyy-MM-dd HH:mm:ss") & "#"
        criteriaPreviousBalance = "CreationDate >= #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "# AND CreationDate <= #" & Format(INDDateEnd, "yyyy-MM-dd HH:mm:ss") & "#"
        'filtro por cajas
        If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
            criteria &= "AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
            criteriaPreviousBalance &= "AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
            criteriaNotMovement = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "'"
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

        resulXpo = Me.LoadCollection(Of TreasuryVReportCashBookXpo)(XpoDefault.DataLayer, Nothing, criteria, session)
        'cargar el saldo anterior a los cajas
        resulXpoTreasuryBalance = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaPreviousBalance, session)
        Dim dictionaryBalance As New Dictionary(Of String, Double)
        For Each itemXpo In resulXpo.OrderBy(Function(x) IIf(x.CashRegisterId.GetValueOrDefault(0) = 0, x.IdBankAccount, x.CashRegisterId)).ThenBy(Function(x) x.DocumentDate)
            Dim OperacionXpo As Decimal
            Dim cash As New List(Of TreasuryCashRegistersXpo)
            Dim Bank As New List(Of EntityBankAccountXpo)
            Dim _balance As Decimal = 0
            OperacionXpo = (itemXpo.ValueDebit - itemXpo.ValueCredit)
            If Not dictionaryBalance.ContainsKey(IIf(itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0, "C" & itemXpo.CashRegisterId, "B" & itemXpo.IdBankAccount)) Then
                Dim criteriaBalance As String
                If itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0 Then
                    criteriaBalance = "CashRegisterId = " & itemXpo.CashRegisterId & " and DocumentDate < #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "#"
                    cash = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, "Id = " & itemXpo.CashRegisterId)
                    _balance = cash(0).InitialBalance
                Else
                    criteriaBalance = "EntityBankAccountId = " & itemXpo.IdBankAccount & " and DocumentDate < #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "#"
                    Bank = Me.LoadCollection(Of EntityBankAccountXpo)(XpoDefault.DataLayer, Nothing, "Id = " & itemXpo.IdBankAccount)
                    _balance = Bank(0).InitialBalance
                End If
                If INDStatus IsNot Nothing Then
                    criteriaBalance &= " AND Status in(" & INDStatus.ToString & ")"
                End If
                Dim viewBalance = ListLastBalance(criteriaBalance, IIf(itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0, True, False))

                Dim balanceCash = _balance

                balanceCash += viewBalance.DebitValue - viewBalance.CreditValue
                itemXpo.SaldoAnterior = balanceCash
                dictionaryBalance.Add(IIf(itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0, "C" & itemXpo.CashRegisterId, "B" & itemXpo.IdBankAccount), balanceCash + OperacionXpo)
                itemXpo.NuevoSaldo = dictionaryBalance(IIf(itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0, "C" & itemXpo.CashRegisterId, "B" & itemXpo.IdBankAccount))
            Else
                dictionaryBalance(IIf(itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0, "C" & itemXpo.CashRegisterId, "B" & itemXpo.IdBankAccount)) += OperacionXpo
                itemXpo.NuevoSaldo = dictionaryBalance(IIf(itemXpo.CashRegisterId.GetValueOrDefault(0) <> 0, "C" & itemXpo.CashRegisterId, "B" & itemXpo.IdBankAccount))
            End If
        Next

        ' agregamos las cajas que no tienen movimiento y mostramos el saldo inicial
        resulXpoNotMovement = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, criteriaNotMovement, session)
        For Each itemNotMovement In resulXpoNotMovement
            If Not dictionaryBalance.ContainsKey("C" & itemNotMovement.Id) Then
                Dim itemResultXpo As New TreasuryVReportCashBookXpo(session)
                itemResultXpo.CashRegisterId = itemNotMovement.Id
                itemResultXpo.DocumentDate = itemNotMovement.InitialDate
                itemResultXpo.CashRegisterCode = itemNotMovement.Code
                itemResultXpo.CashRegisterName = itemNotMovement.Name
                itemResultXpo.ValueDebit = 0
                itemResultXpo.ValueCredit = 0
                itemResultXpo.Code = Nothing
                itemResultXpo.SaldoAnterior = itemNotMovement.CurrentBalance
                itemResultXpo.CurrencyAbbreviation = itemNotMovement.CurrencyAbbreviation
                resulXpo.Add(itemResultXpo)
                dictionaryBalance.Add("C" & itemNotMovement.Id, itemNotMovement.CurrentBalance)
            End If
        Next

        Return resulXpo
    End Function

#End Region

#Region "Reporte Diario de Caja"
	''' <summary>
	''' Lista todos lo movimientos registrados agrupados por cajas
	''' </summary>
	''' <returns></returns>
	Public Function GetCollectionReportCashJournal(ByVal INDDateStart As Date, ByVal INDDateEnd As Date, ByVal INDStatus As Integer, ByVal INDPaymentType As Integer, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal CurrencyId As Integer) As List(Of TreasuryVReportCashBookXpo)

		'Definir Criteria
		INDDateEnd = INDDateEnd.Date.AddHours(23).AddMinutes(59).AddSeconds(59)
		Dim criteria = "DocumentDate >= #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDateEnd, "yyyy-MM-dd HH:mm:ss") & "#"
		Dim criteriaPreviousBalance = "DocumentDate >= #" & Format(INDDateStart, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDDateEnd, "yyyy-MM-dd HH:mm:ss") & "#"
		Dim criteriaDataWithDifferentDates = $"DocumentDate < #{Format(INDDateStart, "yyyy-MM-dd HH:mm:ss")}# AND CreationDate >= #{Format(INDDateStart, "yyyy-MM-dd HH:mm:ss")}#"

		'filtro por cajas
		If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
			criteria &= " AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
			criteriaPreviousBalance &= "AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
			criteriaDataWithDifferentDates &= " AND CashRegisterId.Code >= '" & INDCashStart & "' AND CashRegisterId.Code <= '" & INDCashEnd & "'"
		End If

		If INDPaymentType <> 5 Then
			criteria &= "AND PaymentMethod = " & INDPaymentType
		End If

		If INDStatus = 3 Then
			criteria &= "AND Status in (1, 2, 4)"
		ElseIf INDStatus = 2 Then
			criteria &= "AND Status in (2, 4) "
		Else
			criteria &= "AND Status = " & INDStatus
		End If

		If CurrencyId > 0 Then
			criteria &= "AND CurrencyId = " & CurrencyId
		End If

		Dim resulXpo = LoadCollection(Of TreasuryVReportCashBookXpo)(XpoDefault.DataLayer, Nothing, criteria)
		'cargar el saldo anterior a las cajas
		Dim resulXpoTreasuryBalance = LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaPreviousBalance)
		Dim dictionaryBalance As New Dictionary(Of String, Integer)
		Dim dataOrdered = resulXpo.OrderBy(Function(x) x.CashRegisterId).ThenBy(Function(x) x.DocumentDate)
		Dim itemsPasadosConFechaPosterior = LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaDataWithDifferentDates)
		Dim getPreviusBalance = Function(itemsBalance As IEnumerable(Of TreasuryTreasuryBalance), cashRegisterId As Integer)
									''se instancia la variable a retornar como un nuevo objeto
									Dim itemBalanceMin As New TreasuryTreasuryBalance

									''dado el caso que la caja no tenga saldos anteriores se retorna el objeto totalmente en blanco solo se agrega PreviousBalance = 0 para una validacion que hay adelante 
									''si la caja tiene saldo previo continua la logica que tenia
									If itemsBalance.Count = 0 Then
										itemBalanceMin.PreviousBalance = 0
									Else
										Dim itemsFiltered = itemsBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = cashRegisterId)
										If itemsFiltered.Any() Then
											Dim IdMin = itemsFiltered.Min(Function(x) x.Id)
											itemBalanceMin = itemsFiltered.FirstOrDefault(Function(x) x.Id = IdMin)
											If itemBalanceMin IsNot Nothing Then
												itemBalanceMin.PreviousBalance += itemsPasadosConFechaPosterior.Sum(Function(m) If(m.Nature = 1, m.ValueMovement, -m.ValueMovement))
											End If
										End If
									End If
									Return itemBalanceMin
								End Function
		For Each itemXpo In dataOrdered
			Dim operationValue = itemXpo.ValueDebit - itemXpo.ValueCredit
			If itemXpo?.CashRegisterId IsNot Nothing Then
				Dim itemBalanceMin = getPreviusBalance(resulXpoTreasuryBalance, itemXpo.CashRegisterId)
				If itemBalanceMin IsNot Nothing Then
					If dictionaryBalance.ContainsKey("C" & itemXpo.CashRegisterId) Then
						dictionaryBalance("C" & itemXpo.CashRegisterId) += operationValue
						itemXpo.NuevoSaldo = itemBalanceMin.PreviousBalance + dictionaryBalance("C" & itemXpo.CashRegisterId)
					Else
						dictionaryBalance.Add("C" & itemXpo.CashRegisterId, operationValue)
						itemXpo.SaldoAnterior = itemBalanceMin.PreviousBalance
						itemXpo.NuevoSaldo = itemXpo.SaldoAnterior + dictionaryBalance("C" & itemXpo.CashRegisterId)
					End If
				End If
			End If
		Next
		Return resulXpo
	End Function

#End Region

#Region "Report TreasuryNewsletterEntityBank"
	''' <summary>
	''' Lista todos lo movimientos registrados en las cuentas bancarias
	''' </summary>
	''' <returns></returns>
	Public Function GetCollectionTreasuryNewsletterEntityBank(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDDocumentStatus As String, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDTypeVoucher As Integer, ByVal INDStatus As String, ByVal INDCurrencyAbbreviation As String) As List(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)
        'Definir Criteria
        Dim criteria As String = "DocumentDate >= #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDFechaEnd, "yyyy-MM-dd HH:mm:ss") & "# AND VoucherType = " & INDTypeVoucher & " AND EntityBankStatus IN (" & INDStatus & ")"

        'filtro por cuentas bancarias
        If INDCurrentAccountSavingsStart <> "" And INDCurrentAccountSavingsEnd <> "" Then
            criteria &= " AND EntityBankAccountCode >= '" & INDCurrentAccountSavingsStart & "' AND EntityBankAccountCode <= '" & INDCurrentAccountSavingsEnd & "'"
        End If

        If INDDocumentStatus IsNot Nothing Then
            criteria &= " AND Status IN (" & INDDocumentStatus.ToString & ")"
        End If

        'filtro por Moneda
        If INDCurrencyAbbreviation IsNot Nothing Then
            criteria &= " AND CurrencyAbbreviation = '" & INDCurrencyAbbreviation & "'"
        End If

        Return Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterEntityBankAccount)(XpoDefault.DataLayer, Nothing, criteria)
    End Function

#End Region

#Region "Report TreasuryNewsletterEntityCash"
    ''' <summary>
    ''' Lista todos los movimientos registados en las cajas
    ''' </summary>
    ''' <returns></returns>
    Public Function GetCollectionTreasuryNewsletterEntityCash(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDDocumentStatus As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDTypeVoucher As Integer, ByVal INDStatus As String, ByVal INDCurrencyAbbreviation As String) As List(Of TreasuryVReportTreasuryNewsletterCash)
        'Definir Criteria
        Dim criteria As String = "DocumentDate >= #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDFechaEnd, "yyyy-MM-dd HH:mm:ss") & "# AND VoucherType = " & INDTypeVoucher & " AND CashRegisterStatus IN (" & INDStatus & ")"

        'filtro por Cajas
        If INDCashStart IsNot Nothing And INDCashEnd IsNot Nothing Then
            criteria &= "AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "'"
        End If

        If INDDocumentStatus IsNot Nothing Then
            criteria &= " AND Status IN (" & INDDocumentStatus.ToString & ")"
        End If

        'filtro por Moneda
        If INDCurrencyAbbreviation IsNot Nothing Then
            criteria &= " AND CurrencyAbbreviation = '" & INDCurrencyAbbreviation & "'"
        End If

        Return Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterCash)(XpoDefault.DataLayer, Nothing, criteria)
    End Function
#End Region

#Region "Report TreasuryEntityBank"

    Public Function GetReportTreasuryEntityBank(ByVal INDEntityBankId As Integer, ByVal INDFechaIni As Date, ByVal INDStatus As String) As Decimal
        Dim criteriaBalance As String = "EntityBankAccountId = " & INDEntityBankId & " and DocumentDate < #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "# AND Status in(" & INDStatus & ")"
        Dim viewBalance = ListLastBalance(criteriaBalance, False)
        Dim bank = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, "Id = " & INDEntityBankId)
        Dim balanceBank = bank(0).InitialBalance
        balanceBank += viewBalance.DebitValue
        balanceBank -= viewBalance.CreditValue
        Return balanceBank
    End Function
#End Region

#Region "Report TreasuryNewsletterSummaryNewsletter"

    Private Function ListLastBalance(criteria As String, isCash As Boolean) As Object
        criteria &= " AND Status <> 3"
        Dim session = New Session(XpoDefault.DataLayer)
        Dim obj As Object = New ExpandoObject()
        obj.DebitValue = 0
        obj.CreditValue = 0
        Dim xpv As XPView
        If (isCash = True) Then
            xpv = New XPView(session, GetType(TreasuryVReportCashBookXpo))
            xpv.AddProperty("ValueDebit", "sum(ValueDebit)")
            xpv.AddProperty("ValueCredit", "sum(ValueCredit)")
            xpv.Criteria = CriteriaOperator.Parse(criteria)
            For Each itemBalance As ViewRecord In xpv
                obj.DebitValue += itemBalance("ValueDebit")
                obj.CreditValue += itemBalance("ValueCredit")
            Next
        Else
            xpv = New XPView(session, GetType(TreasuryVReportEntityBankAccountBook))
            xpv.AddProperty("ValueDebit", "sum(ValueDebit)")
            xpv.AddProperty("ValueCredit", "sum(ValueCredit)")
            xpv.Criteria = CriteriaOperator.Parse(criteria)
            For Each itemBalance As ViewRecord In xpv
                obj.DebitValue += itemBalance("ValueDebit")
                obj.CreditValue += itemBalance("ValueCredit")
            Next
        End If
        Return obj
    End Function

    ''' <summary>
    ''' Lista todas los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionSummaryNewsletter(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDDocumentStatus As String,
                                                    ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String,
                                                    ByVal INDCashStart As String, ByVal INDCashEnd As String,
                                                    ByVal INDTypeReport As Integer, ByVal INDFormat As Integer, ByVal INDStatus As String) As List(Of TreasuryVReportTreasuryNewsletterSummary)
        Dim resulXpo As IList(Of TreasuryVReportTreasuryNewsletterSummary)

        Dim criteriaEntityBanksAccount As String = Nothing
        Dim criteriaCashRegister As String = Nothing
        Dim criteria As String = "DocumentDate >= #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "# AND DocumentDate <= #" & Format(INDFechaEnd, "yyyy-MM-dd HH:mm:ss") & "# AND Status IN (" & INDDocumentStatus & ")"

        If INDCurrentAccountSavingsStart <> "" AndAlso INDCurrentAccountSavingsEnd <> "" AndAlso INDCashStart <> "" AndAlso INDCashEnd <> "" Then
            criteriaEntityBanksAccount = "Code >= '" & INDCurrentAccountSavingsStart & "' AND Code <= '" & INDCurrentAccountSavingsEnd & "' AND Status IN (" & INDStatus & ")"
            criteriaCashRegister = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "' AND Status IN (" & INDStatus & ")"
            criteria &= " AND ((EntityBankAccountCode >= '" & INDCurrentAccountSavingsStart & "' AND EntityBankAccountCode <= '" & INDCurrentAccountSavingsEnd & "' AND EntityBankStatus IN (" & INDStatus & ")) OR (CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "' AND CashRegisterStatus IN (" & INDStatus & ")))"
        ElseIf INDCurrentAccountSavingsStart <> "" AndAlso INDCurrentAccountSavingsEnd <> "" Then
            criteriaEntityBanksAccount = "Code >= '" & INDCurrentAccountSavingsStart & "' AND Code <= '" & INDCurrentAccountSavingsEnd & "' AND Status IN (" & INDStatus & ")"
            criteria &= " AND EntityBankAccountCode >= '" & INDCurrentAccountSavingsStart & "' AND EntityBankAccountCode <= '" & INDCurrentAccountSavingsEnd & "' AND EntityBankStatus IN (" & INDStatus & ")"
        ElseIf INDCashStart <> "" AndAlso INDCashEnd <> "" Then
            criteriaCashRegister = "Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "' AND Status IN (" & INDStatus & ")"
            criteria &= " AND CashRegisterCode >= '" & INDCashStart & "' AND CashRegisterCode <= '" & INDCashEnd & "' AND CashRegisterStatus IN (" & INDStatus & ")"
        End If

        'cargar cuentas y cajas que tuvieron movimiento en el rango de fecha
        resulXpo = Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterSummary)(XpoDefault.DataLayer, Nothing, criteria)

        If Not String.IsNullOrEmpty(criteriaEntityBanksAccount) Then
            Dim resultXpoEntityBanksAccount = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, criteriaEntityBanksAccount)
            For Each itemXpo In resultXpoEntityBanksAccount
                Dim criteriaBalance As String = "EntityBankAccountId = " & itemXpo.Id & " AND DocumentDate < #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "# AND Status IN (" & INDDocumentStatus & ")"
                Dim viewBalance = ListLastBalance(criteriaBalance, False)
                Dim balanceBank = itemXpo.InitialBalance
                balanceBank += viewBalance.DebitValue
                balanceBank -= viewBalance.CreditValue

                resulXpo.Where(Function(r) r.EntityBankAccountId = itemXpo.Id).ToList().
                    ForEach(Function(r) r.SaldoAnterior = balanceBank)

                Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                resulTresuryBalance.Row = String.Format("EntityBankAccounts-{0}", itemXpo.Id)
                resulTresuryBalance.EntityBankAccountId = itemXpo.Id
                resulTresuryBalance.EntityBankAccountCode = itemXpo.Code
                resulTresuryBalance.Name = itemXpo.IdBank.Name
                resulTresuryBalance.Type = itemXpo.Type
                resulTresuryBalance.Number = itemXpo.Number
                resulTresuryBalance.EntityBankStatus = itemXpo.Status
                resulTresuryBalance.Code = itemXpo.IdBank.Code
                resulTresuryBalance.DocumentDate = Date.MinValue
                resulTresuryBalance.MainAccount = itemXpo.IdMainAccount.Number
                resulTresuryBalance.SaldoAnterior = balanceBank
                resulTresuryBalance.SumReceipts = 0
                resulTresuryBalance.SumExpenditures = 0
                resulTresuryBalance.CurrencyAbbreviation = If(String.IsNullOrEmpty(itemXpo.CurrencyAbbreviation), SessionValues.Instance.CurrencyISO4217, itemXpo.CurrencyAbbreviation)
                resulXpo.Add(resulTresuryBalance)
            Next
        End If

        If Not String.IsNullOrEmpty(criteriaCashRegister) Then
            Dim resultXpoCashRegister = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, criteriaCashRegister).OrderBy(Function(x) x.Code)
            For Each itemXpo In resultXpoCashRegister
                Dim criteriaBalance As String = "CashRegisterId = " & itemXpo.Id & " AND DocumentDate < #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "# AND Status IN (" & INDDocumentStatus & ")"
                Dim viewBalance = ListLastBalance(criteriaBalance, True)
                Dim ResultAccumulate = (resulXpo.Where(Function(x) x.CashRegisterId = itemXpo.Id)).ToList
                Dim balanceBank As Decimal

                If ResultAccumulate.Count > 0 Then
                    balanceBank = itemXpo.InitialBalance
                    balanceBank += viewBalance.DebitValue
                    balanceBank -= viewBalance.CreditValue
                    ResultAccumulate(0).SaldoAnterior = balanceBank
                    Continue For
                End If
                balanceBank = itemXpo.InitialBalance
                balanceBank += viewBalance.DebitValue
                balanceBank -= viewBalance.CreditValue

                If balanceBank > 0 Then
                    Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                    resulTresuryBalance.Row = String.Format("CashRegisters-{0}", itemXpo.Id)
                    resulTresuryBalance.CashRegisterId = itemXpo.Id
                    resulTresuryBalance.CashRegisterCode = itemXpo.Code
                    resulTresuryBalance.NameCash = itemXpo.Name
                    resulTresuryBalance.CashRegisterStatus = itemXpo.Status
                    resulTresuryBalance.Code = itemXpo.Code
                    resulTresuryBalance.DocumentDate = Date.MinValue
                    resulTresuryBalance.MainAccount = itemXpo.IdMainAccount.Number
                    resulTresuryBalance.SaldoAnterior = balanceBank
                    resulTresuryBalance.SumReceipts = 0
                    resulTresuryBalance.SumExpenditures = 0
                    resulTresuryBalance.CurrencyAbbreviation = If(String.IsNullOrEmpty(itemXpo.CurrencyAbbreviation), SessionValues.Instance.CurrencyISO4217, itemXpo.CurrencyAbbreviation)
                    resulXpo.Add(resulTresuryBalance)
                End If
            Next
        End If

        Return resulXpo
    End Function

    ''' <summary>
    ''' Lista de Flujo de Caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ListCollectionCashFlow(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date) As List(Of TreasuryVReportTreasuryNewsletterSummary)

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

        criteria &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND Status in(2)"
        criteriaBalanceMovement &= "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        criteriaNoMovement = "GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "#"
        'filtro por cuentas bancarias y cajas
        criteriaBalanceMovement &= "AND GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "#"
        criteriaNoMovement &= "AND GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "#"


        Dim bankAcco = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, Nothing)
        Dim aBank As String = bankAcco.First.Code.ToString
        Dim bBank As String = bankAcco.Last.Code.ToString

        Dim cashR = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, Nothing)
        Dim aCash As String = cashR.First.Code.ToString
        Dim bCash As String = cashR.Last.Code.ToString

        'cargar cuentas y cajas que tuvieron movimiento en el rango de fecha
        resulXpo = Me.LoadCollection(Of TreasuryVReportTreasuryNewsletterSummary)(XpoDefault.DataLayer, Nothing, "GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND Status in(2)AND EntityBankAccountCode >= '" & aBank & "' And  EntityBankAccountCode <= '" & bBank & "' or GetDate(DocumentDate) >= #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND GetDate(DocumentDate) <= #" & Format(INDFechaEnd, "yyyy-MM-dd") & "# AND CashRegisterCode >= '" & aCash & "' AND CashRegisterCode <= '" & bCash & "'")

        'Cargar Los saldos Anteriores de las cuentas y cajas que tuvieron movimientos en el rango de fecha
        ''resulXpoTreasuryBalance = Me.LoadCollection(Of TreasuryTreasuryBalance)(XpoDefault.DataLayer, Nothing, criteriaBalanceMovement)
        Dim dictionaryBalance As New Dictionary(Of String, Decimal)
        For Each itemXpo In resulXpo
            If itemXpo.EntityBankAccountId = 0 Then
                'Dim DateMin = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId)).ToList.Min(Function(x) x.DocumentDate)
                'For Each itemBalanceMin In resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = itemXpo.CashRegisterId And x.DocumentDate = DateMin)
                If Not dictionaryBalance.ContainsKey("C" & itemXpo.CashRegisterId) Then
                    Dim criteriaBalance As String = "CashRegisterId = " & itemXpo.CashRegisterId & " and GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND Status in(2)"
                    Dim viewBalance = ListLastBalance(criteriaBalance, True)
                    Dim cash = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, "Id = " & itemXpo.CashRegisterId)
                    Dim balanceCash = cash(0).InitialBalance
                    balanceCash += viewBalance.DebitValue
                    balanceCash -= viewBalance.CreditValue
                    itemXpo.SaldoAnterior = balanceCash
                    dictionaryBalance.Add("C" & itemXpo.CashRegisterId, itemXpo.SaldoAnterior)
                End If
                'Next
            ElseIf itemXpo.CashRegisterId = 0 Then
                'Dim DateMin = (resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemXpo.EntityBankAccountId)).ToList.Min(Function(x) x.DocumentDate)
                'For Each itemBalanceMin In resulXpoTreasuryBalance.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = itemXpo.EntityBankAccountId And x.DocumentDate = DateMin)
                If Not dictionaryBalance.ContainsKey("B" & itemXpo.EntityBankAccountId) Then
                    Dim criteriaBalance As String = "EntityBankAccountId = " & itemXpo.EntityBankAccountId & " and GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND Status in(2)"
                    Dim viewBalance = ListLastBalance(criteriaBalance, False)
                    Dim bank = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, "Id = " & itemXpo.EntityBankAccountId)
                    Dim balanceBank = bank(0).InitialBalance
                    balanceBank += viewBalance.DebitValue
                    balanceBank -= viewBalance.CreditValue
                    itemXpo.SaldoAnterior = balanceBank
                    dictionaryBalance.Add("B" & itemXpo.EntityBankAccountId, itemXpo.SaldoAnterior)
                End If
                'Next
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
                        Dim itemXpoTreasuryBalanceNoMovement = resulXpoTreasuryBalanceNoMovement.Where(Function(x) If(IsNothing(x.CashRegisterId) = True, 0, x.CashRegisterId.Id) = If(IsNothing(itemXpo.CashRegisterId) = True, 0, itemXpo.CashRegisterId.Id) And x.DocumentDate = DateMax).FirstOrDefault
                        If Not dictionaryBalance.ContainsKey("C" & itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id) Then
                            Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                            resulTresuryBalance.Row = String.Format("TreasuryBalance-{0}", itemXpoTreasuryBalanceNoMovement.Id)
                            resulTresuryBalance.DocumentDate = itemXpoTreasuryBalanceNoMovement.DocumentDate
                            resulTresuryBalance.CashRegisterId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id)
                            resulTresuryBalance.EntityBankAccountId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id)
                            resulTresuryBalance.EntityBankAccountCode = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Code)
                            Dim criteriaBalance As String = "CashRegisterId = " & resulTresuryBalance.CashRegisterId & " and GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "#"
                            Dim viewBalance = ListLastBalance(criteriaBalance, True)
                            Dim cash = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, "Id = " & resulTresuryBalance.CashRegisterId)
                            Dim balanceCash = cash(0).InitialBalance
                            balanceCash += viewBalance.DebitValue
                            balanceCash -= viewBalance.CreditValue
                            resulTresuryBalance.SaldoAnterior = balanceCash
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
                        Dim itemXpoTreasuryBalanceNoMovement = resulXpoTreasuryBalanceNoMovement.Where(Function(x) If(IsNothing(x.EntityBankAccountId) = True, 0, x.EntityBankAccountId.Id) = If(IsNothing(itemXpo.EntityBankAccountId) = True, 0, itemXpo.EntityBankAccountId.Id) And x.DocumentDate = DateMax).FirstOrDefault
                        If Not dictionaryBalance.ContainsKey("B" & itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id) Then
                            Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                            resulTresuryBalance.Row = String.Format("TreasuryBalance-{0}", itemXpoTreasuryBalanceNoMovement.Id)
                            resulTresuryBalance.DocumentDate = itemXpoTreasuryBalanceNoMovement.DocumentDate
                            resulTresuryBalance.CashRegisterId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.CashRegisterId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.CashRegisterId.Id)
                            resulTresuryBalance.EntityBankAccountId = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Id)
                            resulTresuryBalance.EntityBankAccountCode = If(IsNothing(itemXpoTreasuryBalanceNoMovement.EntityBankAccountId) = True, Nothing, itemXpoTreasuryBalanceNoMovement.EntityBankAccountId.Code)
                            Dim criteriaBalance As String = "EntityBankAccountId = " & resulTresuryBalance.EntityBankAccountId & " and GetDate(DocumentDate) < #" & Format(INDFechaIni, "yyyy-MM-dd") & "# AND Status in(2)"
                            Dim viewBalance = ListLastBalance(criteriaBalance, False)
                            Dim bank = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, "Id = " & resulTresuryBalance.EntityBankAccountId)
                            Dim balanceBank = bank(0).InitialBalance
                            balanceBank += viewBalance.DebitValue
                            balanceBank -= viewBalance.CreditValue
                            resulTresuryBalance.SaldoAnterior = balanceBank
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
        resultXpoEntityBanksAccount = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, "Code >= '" & aBank & "' And  Code <= '" & bBank & "'")
        Dim cunt = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, Nothing)

        For Each itemXpo In resultXpoEntityBanksAccount
            Dim ResultAccumulate = (resulXpo.Where(Function(x) x.EntityBankAccountId = itemXpo.Id)).ToList
            If ResultAccumulate.Count < 1 Then
                Dim resultList = (resultXpoEntityBanksAccount.Where(Function(x) x.Id = itemXpo.Id)).FirstOrDefault()
                If resultList IsNot Nothing Then
                    If Not dictionaryBalance.ContainsKey("B" & itemXpo.Id) Then
                        Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                        resulTresuryBalance.Row = String.Format("EntityBankAccounts-{0}", resultList.Id)
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

        'Cargar las cajas que no tengan ningun movimiento en treasuryBalance       
        resultXpoCashRegister = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, "Code >= '" & aCash & "' AND Code <= '" & bCash & "'")
        Dim gister = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, Nothing)
        For Each itemXpo In resultXpoCashRegister
            Dim ResultAccumulate = (resulXpo.Where(Function(x) x.CashRegisterId = itemXpo.Id)).ToList
            If ResultAccumulate.Count < 1 Then
                Dim resultList = (resultXpoCashRegister.Where(Function(x) x.Id = itemXpo.Id)).FirstOrDefault()
                If resultList IsNot Nothing Then
                    If Not dictionaryBalance.ContainsKey("C" & resultList.Id) Then
                        Dim resulTresuryBalance As New TreasuryVReportTreasuryNewsletterSummary
                        resulTresuryBalance.Row = String.Format("CashRegisters-{0}", resultList.Id)
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

        'retornar la lista
        Return resulXpo
    End Function

#End Region

#Region "ValuePreviousBalance Reporte Boletin Tesoreria"
    ''' <summary>
    ''' Lista todas los recibos de caja
    ''' </summary>
    ''' <returns></returns>
    Public Function ValuePreviousBalance(ByVal INDFechaIni As Date, ByVal INDFechaEnd As Date, ByVal INDCurrentAccountSavingsStart As String, ByVal INDCurrentAccountSavingsEnd As String, ByVal INDCashStart As String, ByVal INDCashEnd As String, ByVal INDPreviousBalanceType As Integer, ByVal INDStatus As String) As Decimal

        ''Dim resulXpo As IList(Of TreasuryTreasuryBalance)
        Dim ValuePrevioBalance As Decimal = 0
        If INDPreviousBalanceType = 1 Then
            Dim criteriaBank As String = ""
            If INDCurrentAccountSavingsStart <> "" And INDCurrentAccountSavingsEnd <> "" Then
                criteriaBank = " Code >= '" & INDCurrentAccountSavingsStart & "' And  Code <= '" & INDCurrentAccountSavingsEnd & "'"
            End If
            Dim listBank As IList(Of EntityBankAccountXpo) = Me.LoadCollection(Of EntityBankAccountXpo)(XpoDefault.DataLayer, Nothing, criteriaBank)
            For Each itemBank In listBank
                Dim criteriaBalance As String = "EntityBankAccountId = " & itemBank.Id & " and DocumentDate < #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "#  AND Status in(" & INDStatus & ")"
                Dim viewBalance = ListLastBalance(criteriaBalance, False)
                Dim bank = Me.LoadCollection(Of TreasuryEntityBankAccountsXpo)(XpoDefault.DataLayer, Nothing, "Id = " & itemBank.Id)
                Dim balanceCash = bank(0).InitialBalance
                balanceCash += viewBalance.DebitValue
                balanceCash -= viewBalance.CreditValue
                ValuePrevioBalance += balanceCash
            Next
        ElseIf INDPreviousBalanceType = 2 Then
            Dim criteriaCash As String = ""
            If INDCashStart <> "" And INDCashEnd <> "" Then
                criteriaCash = " Code >= '" & INDCashStart & "' AND Code <= '" & INDCashEnd & "'"
                Dim listCash As IList(Of CashRegisterXpo) = Me.LoadCollection(Of CashRegisterXpo)(XpoDefault.DataLayer, Nothing, criteriaCash)
                For Each itemCash In listCash
                    Dim criteriaBalance As String = "CashRegisterId = " & itemCash.Id & " and DocumentDate < #" & Format(INDFechaIni, "yyyy-MM-dd HH:mm:ss") & "#  AND Status in(" & INDStatus & ")"
                    Dim viewBalance = ListLastBalance(criteriaBalance, True)
                    Dim cash = Me.LoadCollection(Of TreasuryCashRegistersXpo)(XpoDefault.DataLayer, Nothing, "Id = " & itemCash.Id)
                    Dim balanceCash = cash(0).InitialBalance
                    balanceCash += viewBalance.DebitValue
                    balanceCash -= viewBalance.CreditValue
                    ValuePrevioBalance += balanceCash
                Next
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

#Region "Reporte de listado de comprobante de egreso"

    ''' <summary>
    ''' schedule payment  by code
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Public Function GetSchedulePaymentTreasuryByCode(Code) As XPCollection(Of SchedulePaymentXpo)
        Dim session As New IndigoXPOSession(Of SchedulePaymentXpo)()
        Dim criteria As CriteriaOperator = CriteriaOperator.Parse("Code= '" & Code & "' And (Status=" & 2 & " Or Status=" & 4 & " Or Status=" & 5 & ")")
        Dim collect = New XPCollection(Of SchedulePaymentXpo)(session, criteria)
        Return collect
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