Imports Infrastructure.CrossCutting
Imports Domain.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Payroll
Imports System.Linq

Public Class PaymentServices

#Region "Fields"

    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>
    Private Const MODULE_NAME = "Payments"

    ''' <summary>
    ''' Variable tipo repositorio para parametros de pago
    ''' </summary>
    ''' <remarks></remarks>
    Private _settingsPaymentsRepository As ISettingPaymentsRepository
    ''' <summary>
    ''' Variable tipo repositorio para cxp
    ''' </summary>
    ''' <remarks></remarks>
    Private _accountPayableRepository As IAccountPayableRepository
    ''' <summary>
    ''' Variable tipo repositorio para anticipos
    ''' </summary>
    ''' <remarks></remarks>
    Private _advancePaymentsRepository As IMoneyAdvanceRepository
    ''' <summary>
    ''' Variable tipo repositorio para proveedor
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierRepository As Domain.Maintenance.ISupplierRepository
    ''' <summary>
    ''' Variable tipo repositorio para lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _distributionLinesRepository As IDistributionLinesRepository
    ''' <summary>
    ''' Variable tipo repositorio para la asociacion de proveedor con lineas de distribucion
    ''' </summary>
    ''' <remarks></remarks>
    Private _supplierDistributionLines As ISuppliersDistributionLinesRepository
    ''' <summary>
    ''' Variable tipo repositorio para la cuenta contable
    ''' </summary>
    ''' <remarks></remarks>
    Private _pucRepository As IPUCRepository
    ''' <summary>
    ''' Repositorio de centro de costo
    ''' </summary>
    ''' <remarks></remarks>
    Private _costCenterRepository As ICostCenterRepository

    ''' <summary>
    ''' Repositorio de documento soporte
    ''' </summary>
    Private _electronicSupportDocumentRepository As IElectronicSupportDocumentRepository

    Private _currencyRepository As ICurrencyRepository

#End Region

#Region "Builder"

    Public Sub New(
                  Optional settingsPaymentsRepository As ISettingPaymentsRepository = Nothing,
                  Optional accountPayableRepository As IAccountPayableRepository = Nothing,
                  Optional advancePaymentsRepository As IMoneyAdvanceRepository = Nothing,
                  Optional electronicSupportDocumentRepository As IElectronicSupportDocumentRepository = Nothing,
                  Optional currencyRepository As ICurrencyRepository = Nothing)
        _settingsPaymentsRepository = settingsPaymentsRepository
        _accountPayableRepository = accountPayableRepository
        _advancePaymentsRepository = advancePaymentsRepository
        _electronicSupportDocumentRepository = electronicSupportDocumentRepository
        _currencyRepository = currencyRepository
    End Sub

    Public Sub New(Optional supplierRepository As Domain.Maintenance.ISupplierRepository = Nothing, Optional distributionLinesRepository As IDistributionLinesRepository = Nothing,
                   Optional supplierDistributionLines As ISuppliersDistributionLinesRepository = Nothing, Optional pucRepository As IPUCRepository = Nothing,
                   Optional costCenterRepository As ICostCenterRepository = Nothing, Optional accountPayableRepository As IAccountPayableRepository = Nothing,
                   Optional currencyRepository As ICurrencyRepository = Nothing)
        _supplierRepository = supplierRepository
        _distributionLinesRepository = distributionLinesRepository
        _supplierDistributionLines = supplierDistributionLines
        _pucRepository = pucRepository
        _costCenterRepository = costCenterRepository
        _accountPayableRepository = accountPayableRepository
        _currencyRepository = currencyRepository
    End Sub

#End Region

#Region "Shared"

    ''' <summary>
    ''' Metodo que agrega los dias a la fecha
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function AddDaysDate(ByVal days As Integer, ByVal dateDoc As DateTime) As DateTime
        Dim dateServ As Date = DateAdd(DateInterval.Day, days, dateDoc)
        Return dateServ
    End Function

    ''' <summary>
    ''' Suma débitos y créditos
    ''' </summary>
    ''' <param name="listAccountPayableConcept"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function DebitCredit(ByVal listAccountPayableConcept As List(Of AccountPayableDetailConcept)) As Decimal
        If listAccountPayableConcept IsNot Nothing Then
            Dim credit As Decimal = 0
            Dim debit As Decimal = 0
            Dim val As Decimal
            For Each item As AccountPayableDetailConcept In listAccountPayableConcept
                If item.Nature = 2 Then
                    credit = credit + item.Value
                Else
                    debit = debit + item.Value
                End If
            Next
            val = debit - credit
            Return val
        Else
            Return 0
        End If
    End Function

    ''' <summary>
    ''' Crea las cuotas para los conceptos de las facturas
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateShares(ByVal shares As Integer, ByVal value As Decimal, ByVal dateServer As DateTime) As List(Of AccountPayableShares)
        Dim listAccountPayableShares = New List(Of AccountPayableShares)
        Dim valShare As Decimal
        'Dim dateServ As Date = DateAdd(DateInterval.Day, 1, dateServer)
        Dim listShares As List(Of Decimal)
        If value = 0 Then
            valShare = 0
        Else
            valShare = value / shares
            listShares = CommonService.GetValueShare(value, shares)
        End If
        For i As Int16 = 1 To shares
            Dim accountPayableShares = New AccountPayableShares
            accountPayableShares.Share = i
            If i <> 1 Then
                dateServer = DateAdd(DateInterval.Month, 1, dateServer)
            End If
            accountPayableShares.DateExpires = dateServer
            If valShare = 0 Then
                accountPayableShares.InitialValue = valShare
                accountPayableShares.Balance = valShare
            Else
                accountPayableShares.InitialValue = listShares.Item(i - 1) 'valShare
                accountPayableShares.Balance = listShares.Item(i - 1) 'valShare
            End If
            listAccountPayableShares.Add(accountPayableShares)
        Next
        Return listAccountPayableShares
    End Function

    ''' <summary>
    ''' Agrupa las facturas por cuentas contables y centros de costo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function GroupMainAccountsAndCostCenter(ByVal listAccountPayable As List(Of AccountPayable), ByVal ListDeferredCausation As List(Of DeferredCausation)) As List(Of DeferredCausation)
        If ListDeferredCausation Is Nothing Then
            ListDeferredCausation = New List(Of DeferredCausation)
        End If
        Dim deferredCausation As DeferredCausation

        For Each itemCabecera As AccountPayable In listAccountPayable
            Dim banBillNumber As Boolean = False
            If ListDeferredCausation.Count > 0 Then
                For Each itemDC As DeferredCausation In ListDeferredCausation
                    If itemCabecera.BillNumber = itemDC.BillNumber Then
                        banBillNumber = True
                    End If
                Next
            End If

            If banBillNumber = False Then
                For Each itemDetalle As AccountPayableDetailConcept In itemCabecera.AccountPayableDetailConcept

                    If itemDetalle.Nature = 1 Then
                        If itemDetalle.DeferredCausation = True Then
                            If ListDeferredCausation.Count = 0 Then
                                deferredCausation = CreateDeferredCausation(itemCabecera, itemDetalle)
                                ListDeferredCausation.Add(deferredCausation)
                            Else
                                Dim ban As Boolean = False
                                For Each itemDeferredCausation As DeferredCausation In ListDeferredCausation
                                    If itemDeferredCausation.BillNumber = itemCabecera.BillNumber Then
                                        If itemDetalle.IdCostCenter IsNot Nothing Then
                                            If itemDeferredCausation.IdMainAccount = itemDetalle.IdAccount AndAlso itemDeferredCausation.IdCostCenter = itemDetalle.IdCostCenter Then
                                                itemDeferredCausation.ValueCreditPeriod = itemDeferredCausation.ValueCreditPeriod + itemDetalle.Value
                                                ban = True
                                                Exit For
                                            End If
                                        ElseIf itemDeferredCausation.IdMainAccount = itemDetalle.IdAccount AndAlso itemDeferredCausation.IdCostCenter Is Nothing Then
                                            itemDeferredCausation.ValueCreditPeriod = itemDeferredCausation.ValueCreditPeriod + itemDetalle.Value
                                            ban = True
                                            Exit For
                                        End If
                                    End If
                                Next
                                If ban = False Then
                                    deferredCausation = CreateDeferredCausation(itemCabecera, itemDetalle)
                                    ListDeferredCausation.Add(deferredCausation)
                                End If
                            End If
                        End If
                    End If

                Next
            End If

        Next

        Return ListDeferredCausation
    End Function

    ''' <summary>
    ''' Crea un nuevo objeto complejo de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateDeferredCausation(ByVal itemCabecera As AccountPayable, ByVal itemDetalle As AccountPayableDetailConcept) As DeferredCausation
        Dim deferredCausation As New DeferredCausation
        With deferredCausation
            .BillNumber = itemCabecera.BillNumber
            .IdMainAccount = itemDetalle.IdAccount
            .IdCostCenter = itemDetalle.IdCostCenter
            .ValueCreditPeriod = If(itemCabecera.DeductibleIva, itemDetalle.BaseValue, itemDetalle.Value)
            .Status = 1
            .CurrencyAbbreviation = itemCabecera.CurrencyAbbreviation
            .CurrencyId = itemCabecera.CurrencyId
        End With
        Return deferredCausation
    End Function

    ''' <summary>
    ''' Metodo para crear el valor proporcional para la causacion diferida
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateValueProportional(ByVal value As Decimal, ByVal dividir As Integer, ByVal mode As Boolean) As Decimal
        If mode = True Then
            dividir = dividir + 1
        End If
        value = value / dividir
        Return value
    End Function

    ''' <summary>
    ''' Metodo para sumar los valores
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SumValues(ByVal list As List(Of DeferredCausationDetails), ByVal val As Decimal, ByVal mode As Boolean) As Decimal
        Dim sum As Decimal = 0
        Dim sumList As Decimal = 0
        For Each item As DeferredCausationDetails In list
            sumList = sumList + item.Value
        Next
        If mode = True Then
            sum = sumList + val
        Else
            sum = sumList
        End If
        Return sum
    End Function

    ''' <summary>
    ''' Metodo para crear la cuota que se va a diferir entre las diferentes cuentas contables y centros de costo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function MonthlyShare(ByVal value As Decimal, ByVal share As Integer) As Decimal
        value = value / share
        Return value
    End Function

    ''' <summary>
    ''' Valido que los valores credito y debito sean iguales y no este desbalanceado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function ValidateCreditDebit(ByVal accounting As JournalVouchers) As Boolean
        Dim valCredit As Decimal = 0
        Dim valDebit As Decimal = 0
        For Each itemDetail As JournalVoucherDetails In accounting.JournalVoucherDetails
            If itemDetail.CreditValue > 0 Then
                valCredit = valCredit + itemDetail.CreditValue
            ElseIf itemDetail.DebitValue > 0 Then
                valDebit = valDebit + itemDetail.DebitValue
            End If
        Next
        If valCredit <> valDebit Then
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Crea el objeto de documento contable
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function CreateJournalVoucherEntity(ByVal filter() As Object) As JournalVoucherDetails
        Dim accountDetail As New JournalVoucherDetails
        With accountDetail
            .IdMainAccount = filter(0)
            .IdThirdParty = filter(1)
            .IdCostCenter = filter(2)
            If filter(3) = 1 Then
                .DebitValue = filter(6)
            Else
                .CreditValue = filter(6)
            End If
            .Detail = filter(4)
            .IdRetention = filter(5)
            .RetentionRate = filter(7)
            If .IdRetention IsNot Nothing Then
                .BaseValue = filter(8)
                .BillingValue = IIf(filter(9) = Nothing OrElse filter(9) = 0, filter(8), filter(9))
            End If
        End With
        Return accountDetail
    End Function

    ''' <summary>
    ''' Modifica el estado del listado de cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ModifyAccountPayable(accountPayable As AccountPayable, accountNature As Integer, nature As Integer, acp As AccountPayable, Optional ListPaymentNotesAccountPayableAdvance As List(Of PaymentNotesAccountPayableAdvance) = Nothing) As AccountPayable
        If accountPayable.ChangeTracker.State <> ObjectState.Deleted Then
            For Each itemShare As AccountPayableShares In accountPayable.AccountPayableShares

                itemShare.ValueNoteShare = (From p In ListPaymentNotesAccountPayableAdvance Where p.AccountPayableShareId IsNot Nothing AndAlso p.AccountPayableShareId = itemShare.Id Select p.AdjustmentValueShare).FirstOrDefault

                If nature = 1 Then 'debito

                    If accountNature = 1 Then 'naturaleza debito aumentamos
                        itemShare.Balance = itemShare.Balance + itemShare.ValueNoteShare
                        itemShare.DebitValue = itemShare.DebitValue + itemShare.ValueNoteShare
                    Else 'se disminuye
                        itemShare.Balance = itemShare.Balance - itemShare.ValueNoteShare
                        itemShare.DebitValue = itemShare.DebitValue + itemShare.ValueNoteShare
                    End If

                Else 'credito
                    If accountNature = 1 Then ' naturalza credit de la cuenta se disminuye el saldo
                        itemShare.Balance = itemShare.Balance - itemShare.ValueNoteShare
                        itemShare.CreditValue = itemShare.CreditValue + itemShare.ValueNoteShare
                    Else 'naturaleza credito de la cuenta se aumenta el saldo 
                        itemShare.Balance = itemShare.Balance + itemShare.ValueNoteShare
                        itemShare.CreditValue = itemShare.CreditValue + itemShare.ValueNoteShare
                    End If

                End If
                itemShare.MarkAsModified()
            Next

            'accountPayable.Adjustment = (From p In ListPaymentNotesAccountPayableAdvance Where p.AccountPayableId IsNot Nothing AndAlso p.AccountPayableId = accountPayable.Id Select p.AdjusmentValue).FirstOrDefault
            'If nature = 1 Then
            '    accountPayable.Balance = accountPayable.Balance - accountPayable.Adjustment
            'Else
            '    accountPayable.Balance = accountPayable.Balance + accountPayable.Adjustment
            'End If

            accountPayable.Balance = accountPayable.AccountPayableShares.Sum(Function(item) item.Balance)

            accountPayable.MarkAsModified()
        End If
        Return accountPayable
    End Function

    ''' <summary>
    ''' Modifica el estado del listado de anticipos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ModifyAdvancePayments(advancePayments As AdvancePayments, nature As Integer, ap As AdvancePayments) As AdvancePayments
        If advancePayments.ChangeTracker.State <> ObjectState.Deleted Then
            advancePayments.Adjustment = ap.Adjustment
            If nature = 1 Then 'Debito
                If advancePayments.DebitValue Is Nothing Then
                    advancePayments.DebitValue = 0
                End If
                'advancePayments.Balance = advancePayments.Balance - advancePayments.Adjustment
                'Este cambio se hace porque jhon dijo que el saldo aumentaba con debito y disminuye con credito
                advancePayments.Balance = advancePayments.Balance + advancePayments.Adjustment
                advancePayments.DebitValue = advancePayments.DebitValue + advancePayments.Adjustment
            Else 'Credito
                If advancePayments.CreditValue Is Nothing Then
                    advancePayments.CreditValue = 0
                End If
                'advancePayments.Balance = advancePayments.Balance + advancePayments.Adjustment
                'Este cambio se hace porque jhon dijo que el saldo aumentaba con debito y disminuye con credito
                advancePayments.Balance = advancePayments.Balance - advancePayments.Adjustment
                advancePayments.CreditValue = advancePayments.CreditValue + advancePayments.Adjustment
            End If
            advancePayments.MarkAsModified()
        End If
        Return advancePayments
    End Function

    ''' <summary>
    ''' Crea el listado de facturas que trae el objeto de traslados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateListAccountPayableWithPaymentTransfer(ByVal paymentTransfer As PaymentTransfer) As List(Of Tuple(Of String, Integer))
        Dim listBillNumbers As List(Of Tuple(Of String, Integer)) = Nothing
        For Each itemDetail As PaymentTransferDetail In paymentTransfer.PaymentTransferDetail
            Dim ban As Integer = 0
            If listBillNumbers Is Nothing Then
                listBillNumbers = New List(Of Tuple(Of String, Integer))
            Else
                For i As Integer = 0 To listBillNumbers.Count - 1
                    If itemDetail.NumberBill = listBillNumbers(i).Item1 Then
                        ban = 1
                        Exit For
                    End If
                Next
            End If
            If ban = 0 Then
                listBillNumbers.Add(New Tuple(Of String, Integer)(itemDetail.NumberBill, itemDetail.SupplierId))
            End If
        Next
        Return listBillNumbers
    End Function

    ''' <summary>
    ''' Crea el comprobante contable para la cxp
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateAccountingAccountPayable(ByVal ListAccountPayable As List(Of AccountPayable), ByVal settingPayment As SettingPayments, Optional ByVal listSuppliers As List(Of Supplier) = Nothing) As JournalVouchers
        Dim accounting As New Domain.Entities.JournalVouchers
        With accounting
            'Se crea la cabecera
            'Se pregunta por la propiedad extendida de JournalVoucher
            If ListAccountPayable(0).JournalVoucherId IsNot Nothing Then 'Si esta llena es porque viene desde el form de comprobante de entrada
                .IdJournalVoucher = ListAccountPayable(0).JournalVoucherId
            Else 'Sino viene llena el tipo de comprobante se saca de los parametros de pago
                .IdJournalVoucher = settingPayment.IdJournalVoucherAccountPayable
            End If
            'Armamos el detalle
            If ListAccountPayable IsNot Nothing AndAlso ListAccountPayable.Count > 0 Then
                Dim sb As New StringBuilder()
                ListAccountPayable.ForEach(Sub(s)
                                               Dim sup = listSuppliers.Where(Function(ss) ss.Id = s.IdSupplier).FirstOrDefault()
                                               sb.Append(String.Format("Factura No. {0} - Proveedor: ({1}) - Descripción: {2} " + vbLf, s.BillNumber, sup.Code & " - " & sup.Name.Trim(), s.Coments))
                                           End Sub)
                'Se valida que si la cxp viene desde ingresos de activos o viene desde el form de cxp u otro
                If ListAccountPayable(0).ChangeProperties Then
                    .Detail = String.Format("Ingreso de Activo No. {0} - {1}", ListAccountPayable(0).AuxEntityCode, sb.ToString().Substring(0, sb.ToString().Length - 3))
                Else
                    .Detail = sb.ToString()
                End If
            End If

            .VoucherDate = ListAccountPayable(0).DocumentDate
            .Status = 2
            .EntityCode = ListAccountPayable(0).Code
            .EntityId = ListAccountPayable(0).Id
            .EntityName = GetType(AccountPayable).Name
            If ListAccountPayable(0).EntityName <> .EntityName Then
                .OriginEntityName = ListAccountPayable(0).EntityName
            End If
            .IsClosedYear = False

            'Se crean los detalles
            For Each itemCabecera As AccountPayable In ListAccountPayable
                For Each itemDetail As AccountPayableDetailConcept In itemCabecera.AccountPayableDetailConcept
                    'Si se agrega un concepto de retencion (Calculadora) pero no se practica retencion, se almacena, pero no se contabiliza
                    If itemDetail.Value = 0 AndAlso itemDetail.AccountPayableDetailConceptLiquidation IsNot Nothing AndAlso itemDetail.AccountPayableDetailConceptLiquidation.Count > 0 Then
                        Continue For
                    End If

                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = itemDetail.IdAccount
                        .IdThirdParty = itemDetail.IdThirdParty
                        .IdCostCenter = itemDetail.IdCostCenter
                        If itemDetail.Nature = 1 Then
                            .DebitValue = itemDetail.Value
                        Else
                            .CreditValue = itemDetail.Value
                        End If
                        .Detail = itemDetail.Detail
                        .IdRetention = itemDetail.IdRetentionConcept
                        .RetentionRate = itemDetail.Percentage

                        .BaseValue = itemDetail.BaseValue
                        If (itemDetail.BillingValue = 0) Then
                            .BillingValue = itemCabecera.InvoiceValue
                        Else
                            .BillingValue = itemDetail.BillingValue
                        End If
                    End With
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next

                If itemCabecera.Value <> 0 Then
                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = itemCabecera.IdAccount
                        .IdThirdParty = itemCabecera.IdThirdParty
                        .IdCostCenter = itemCabecera.IdCostCenter
                        .DebitValue = 0
                        .CreditValue = itemCabecera.Value
                        .Detail = String.Empty
                        .IdRetention = Nothing
                    End With
                    accounting.JournalVoucherDetails.Add(accountDetail)
                End If
            Next

        End With
        Return accounting
    End Function

    ''' <summary>
    ''' Crea el comprobante contable para otros libros que no sean homologables sobre la misma cuenta por pagar
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateAccountingAccountPayableOthersNotHomologatedBooks(ByVal ListAccountPayable As List(Of AccountPayable), ByVal accounting As Domain.Entities.JournalVouchers) As List(Of JournalVouchers)
        Dim accountings As New List(Of JournalVouchers)
        For Each itemCabecera As AccountPayable In ListAccountPayable.Where(Function(i) i.AccountPayableDetailConceptOthersNotHomologatedBooks IsNot Nothing AndAlso i.AccountPayableDetailConceptOthersNotHomologatedBooks.Count() > 0)
            For Each otherNotHomologatedBook In itemCabecera.AccountPayableDetailConceptOthersNotHomologatedBooks
                Dim accountingLocal = accountings.Where(Function(a) a.LegalBookId = otherNotHomologatedBook.Key).FirstOrDefault
                If accountingLocal Is Nothing Then
                    accountingLocal = New JournalVouchers
                    accountingLocal.IdJournalVoucher = accounting.IdJournalVoucher
                    accountingLocal.Detail = accounting.Detail
                    accountingLocal.VoucherDate = accounting.VoucherDate
                    accountingLocal.Status = accounting.Status
                    accountingLocal.EntityCode = accounting.EntityCode
                    accountingLocal.EntityId = accounting.EntityId
                    accountingLocal.EntityName = accounting.EntityName
                    accountingLocal.OriginEntityName = accounting.OriginEntityName
                    accountingLocal.IsClosedYear = accounting.IsClosedYear
                    accountingLocal.LegalBookId = otherNotHomologatedBook.Key
                    accountings.Add(accountingLocal)
                End If

                For Each itemDetail As AccountPayableDetailConcept In otherNotHomologatedBook.Value
                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = itemDetail.IdAccount
                        .IdThirdParty = itemDetail.IdThirdParty
                        .IdCostCenter = itemDetail.IdCostCenter
                        If itemDetail.Nature = 1 Then
                            .DebitValue = itemDetail.Value
                        Else
                            .CreditValue = itemDetail.Value
                        End If
                        .Detail = itemDetail.Detail
                        .IdRetention = itemDetail.IdRetentionConcept
                        .RetentionRate = itemDetail.Percentage

                        .BaseValue = itemDetail.BaseValue
                        If (itemDetail.BillingValue = 0) Then
                            .BillingValue = itemCabecera.InvoiceValue
                        Else
                            .BillingValue = itemDetail.BillingValue
                        End If
                    End With
                    accountingLocal.JournalVoucherDetails.Add(accountDetail)
                Next

                If itemCabecera.Value <> 0 Then
                    Dim accountDetail As New JournalVoucherDetails
                    With accountDetail
                        .IdMainAccount = itemCabecera.IdAccount
                        .IdThirdParty = itemCabecera.IdThirdParty
                        .IdCostCenter = itemCabecera.IdCostCenter
                        .DebitValue = 0
                        .CreditValue = itemCabecera.Value
                        .Detail = String.Empty
                        .IdRetention = Nothing
                    End With
                    accountingLocal.JournalVoucherDetails.Add(accountDetail)
                End If
            Next
        Next
        Return accountings
    End Function

    ''' <summary>
    ''' Crea el comprobante contable para la amortizacion mensual
    ''' </summary>
    ''' <param name="deferredCausation"></param>
    ''' <param name="deferredCausationShare"></param>
    ''' <param name="settingPayments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateAccountingByMonthlyAmortization(ByVal deferredCausation As DeferredCausation, ByVal deferredCausationShare As DeferredCausationShare, ByVal settingPayments As SettingPayments) As ActionResult(Of JournalVouchers)
        Try
            Dim accounting As New JournalVouchers
            With accounting
                Dim actualDate = DateTime.Now
                .IdJournalVoucher = settingPayments.IdJournalVocuherAmortization

                If DateSerial(deferredCausationShare.PaymentYear, deferredCausationShare.PaymentMonth, 1) > DateSerial(DatePart(DateInterval.Year, actualDate), DatePart(DateInterval.Month, actualDate), 1) Then
                    Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = "No se pueden amortizar cuotas de fechas superiores a la actual"}
                End If

                .VoucherDate = actualDate
                If DatePart(DateInterval.Year, actualDate) <> deferredCausationShare.PaymentYear _
                     OrElse DatePart(DateInterval.Month, actualDate) <> deferredCausationShare.PaymentMonth Then
                    .VoucherDate = DateAdd(DateInterval.Minute, -1, DateAdd(DateInterval.Month, 1, DateSerial(deferredCausationShare.PaymentYear, deferredCausationShare.PaymentMonth, 1))).Date
                End If
                .Status = 2
                .Detail = "Proceso automático de amortización mensual de los diferidos del mes de " + MonthName(deferredCausationShare.PaymentMonth) + " de " + deferredCausationShare.PaymentYear.ToString + ", afectados por la cuenta por pagar con código " + deferredCausation.AccountPayable.Code
                .EntityCode = String.Empty
                .EntityId = deferredCausation.Id
                .EntityName = GetType(DeferredCausation).Name
                .IsClosedYear = False
                .BookCurrencyId = deferredCausation?.AccountPayable?.CurrencyId

                Dim accountDetail As JournalVoucherDetails
                Dim filter() As Object

                filter = {deferredCausation.IdMainAccount, deferredCausation.IdThirdParty, deferredCausation.IdCostCenter, 2, String.Empty, Nothing, deferredCausationShare.Value, Nothing}
                accountDetail = CreateJournalVoucherEntity(filter)
                accounting.JournalVoucherDetails.Add(accountDetail)

                Dim count As Integer = 1
                Dim totalShare As Decimal = 0
                Dim valSum = deferredCausation.DeferredCausationDetails.Sum(Function(x) x.Value)
                For Each itemDeferredCausationDetail As DeferredCausationDetails In deferredCausation.DeferredCausationDetails
                    Dim valShare As Decimal = Math.Round((deferredCausationShare.Value * ((itemDeferredCausationDetail.Value * 100) / valSum)) / 100, 1)
                    totalShare = totalShare + valShare

                    'Ajustamos el ultimo detalle con la diferencia por ponderación
                    If count = deferredCausation.DeferredCausationDetails.Count Then
                        If deferredCausationShare.Value <> totalShare Then
                            valShare = valShare + (deferredCausationShare.Value - totalShare)
                        End If
                    End If

                    filter = {itemDeferredCausationDetail.IdMainAccount, deferredCausation.IdThirdParty, itemDeferredCausationDetail.IdCostCenter, 1, String.Empty, Nothing, valShare, Nothing}
                    accountDetail = CreateJournalVoucherEntity(filter)
                    accounting.JournalVoucherDetails.Add(accountDetail)

                    count = count + 1
                Next
            End With

            'Valido que los valores credito y debito sean iguales y no este desbalanceado el documento
            Dim ban As Boolean = ValidateCreditDebit(accounting)
            If ban = False Then
                Dim DebitValue = accounting.JournalVoucherDetails.Sum(Function(d) d.DebitValue)
                Dim CreditValue = accounting.JournalVoucherDetails.Sum(Function(d) d.CreditValue)
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = String.Format("El Documento contable de amortización para la factura '{0}' esta desbalanceado (Debito: {1} - Credito: {2}).", deferredCausation.BillNumber, DebitValue, CreditValue)}
            End If

            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = accounting}
        Catch ex As Exception
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .Message = ex.Message}
        End Try
    End Function

#End Region

#Region "Function"

    ''' <summary>
    ''' Valida los campos del copyPaste de anticipos
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetAdvanceInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAdvance))
        Dim listErrors As New List(Of String)
        Dim listInitialBalanceAdvance As New List(Of InitialBalanceAdvance)

        For i As Integer = 0 To data.Count - 1
            If data(i).Count <> 6 Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Pregunto si existe el proveedor, ya sea con el nit del tercero o el codigo del proveedor
            If data(i).Item(0).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontChoose", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultSupplier As Domain.Maintenance.Entities.Supplier
            resultSupplier = _supplierRepository.GetSupplierByNitThirdParty(data(i).Item(0).ToUpper)
            If resultSupplier.Id = 0 Then
                resultSupplier = _supplierRepository.GetSupplier(data(i).Item(0).ToUpper, False)
                If resultSupplier.Id = 0 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontExist", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                End If
            End If

            'Pregunto si existe la linea de distribucion
            If data(i).Item(1).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DistributionLineDontChoose", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultDistributionLine As DistributionLines
            resultDistributionLine = _distributionLinesRepository.GetDistributionLines(data(i).Item(1).ToUpper)
            If resultDistributionLine.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("DistributionLineDontExist", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            'Verifico que la linea la tenga asociada el proveedor
            Dim supplierDistributionLine As SuppliersDistributionLines
            Dim resultSupplierDistributionLines As List(Of SuppliersDistributionLines)
            resultSupplierDistributionLines = _supplierDistributionLines.GetSuppliersDistributionLinesByIdSupplier(resultSupplier.Id)
            If resultSupplierDistributionLines.Count > 0 Then
                Dim ban As Integer = resultSupplierDistributionLines.FindAll(Function(item) item.IdDistributionLine = resultDistributionLine.Id).Cast(Of SuppliersDistributionLines).ToList().Count
                If ban = 0 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontAssociatedDistributionLine", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                Else
                    supplierDistributionLine = resultSupplierDistributionLines.Find(Function(item) item.IdDistributionLine = resultDistributionLine.Id)
                End If
            End If

            'Verifico que la cuenta contable de la linea maneja centro costo
            Dim resultPUC As MainAccounts
            resultPUC = _pucRepository.GetAccountById(resultDistributionLine.IdMainAccount, False)
            Dim resultCostCenter As Domain.Payroll.Entities.CostCenter = Nothing
            If resultPUC.Id > 0 Then
                If resultPUC.HandlesCostCenter Then
                    If data(i).Item(2) = String.Empty Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterEmpty", MODULE_NAME), (i + 1).ToString()))
                        Continue For
                    End If
                    resultCostCenter = _costCenterRepository.GetCostCenter(data(i).Item(2), True)
                    If resultCostCenter.Id = 0 Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterDontExist", MODULE_NAME), (i + 1).ToString()))
                        Continue For
                    End If
                Else
                    If data(i).Item(2) <> String.Empty Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterNotRequired", MODULE_NAME), (i + 1).ToString()))
                        Continue For
                    End If
                End If
            End If

            'Valido la fecha
            If data(i).Item(3) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DateEmpty", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim dateAdvance As DateTime
            If IsDate(data(i).Item(3)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("DateFormatIncorrect", MODULE_NAME), (i + 1).ToString()))
                Continue For
            Else
                dateAdvance = DateTime.Parse(data(i).Item(3))
            End If
            '-------------------

            'Verifico que el valor sea numerico y que no sea menor a cero
            If data(i).Item(5) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DontValue", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If IsNumeric(data(i).Item(5)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("PayValueNotNumeric", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If CDec(data(i).Item(5)) <= 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("PaymentValueZero", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Creo el Objeto que va en el listado
            Dim initialBalanceAdvancePayments As New InitialBalanceAdvance
            With initialBalanceAdvancePayments
                .SupplierId = resultSupplier.Id
                .ThirdPartyId = resultSupplier.IdThirdParty
                .SupplierDistributionLinesId = supplierDistributionLine.Id
                .MainAccountId = resultDistributionLine.IdMainAccount

                If resultCostCenter IsNot Nothing Then
                    .CostCenterId = resultCostCenter.Id
                    .CostCenterDescription = resultCostCenter.Code + " - " + resultCostCenter.Name
                Else
                    .CostCenterId = Nothing
                    .CostCenterDescription = String.Empty
                End If


                .AdvancePaymentsId = Nothing
                .AdvancePaymentsDate = dateAdvance
                .AdvancePaymentsDescription = data(i).Item(4)
                .Value = data(i).Item(5)

                .SupplierDescription = resultSupplier.Code + " - " + resultSupplier.Name
                .MainAccountDescription = resultPUC.Number + " - " + resultPUC.Name
            End With
            listInitialBalanceAdvance.Add(initialBalanceAdvancePayments)


        Next

        Return New ActionResult(Of List(Of InitialBalanceAdvance)) With {.ObjectEmbbeded = listInitialBalanceAdvance, .MessageResult = listErrors}
    End Function

    ''' <summary>
    ''' Valida los campos del copyPaste de facturas
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetBillsInitialBalance(data As List(Of List(Of String))) As ActionResult(Of List(Of InitialBalanceAccountPayable))
        Dim listErrors As New List(Of String)
        Dim listInitialBalanceAccountPayable As New List(Of InitialBalanceAccountPayable)
        Dim listCompareBillNumber As List(Of Tuple(Of String, Integer)) = Nothing
        Dim dictSupplier As New Dictionary(Of String, Domain.Maintenance.Entities.Supplier)()
        Dim dictDistributionLines As New Dictionary(Of String, DistributionLines)()
        Dim dictMainAccounts As New Dictionary(Of String, MainAccounts)()
        Dim dictSupplierType As New Dictionary(Of String, SupplierType)()
        Dim dictFilingUnit As New Dictionary(Of String, FilingUnit)()
        Dim dictCurrency As New Dictionary(Of String, Currency)()

        For i As Integer = 0 To data.Count - 1
            'Se valida la cantidad de campos que debe tener la estructura
            If data(i).Count <> 13 Then
                listErrors.Add(String.Format(ResourceManager.GetString("IncorrectStructure", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Pregunto si existe el proveedor, ya sea con el nit del tercero o el codigo del proveedor
            If data(i).Item(0).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontChoose", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultSupplier As Domain.Maintenance.Entities.Supplier
            If dictSupplier.ContainsKey(data(i).Item(0).ToUpper) Then
                resultSupplier = dictSupplier(data(i).Item(0).ToUpper)
            Else
                resultSupplier = _supplierRepository.GetSupplierByNitThirdParty(data(i).Item(0).ToUpper)
                dictSupplier.Add(data(i).Item(0).ToUpper(), resultSupplier)
            End If
            If resultSupplier.Id = 0 Then
                resultSupplier = _supplierRepository.GetSupplier(data(i).Item(0).ToUpper, False)
                If resultSupplier.Id = 0 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontExist", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                End If
            End If

            'Pregunto si el proveedor ya tiene la factura
            If data(i).Item(1).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("BillNumberEmpty", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultAccountPayable As AccountPayable
            resultAccountPayable = _accountPayableRepository.GetAccountPayableByBillNumber(data(i).Item(1), resultSupplier.Id)
            If resultAccountPayable.Id > 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("BillNumberExist", MODULE_NAME), resultSupplier.Name, data(i).Item(1), (i + 1).ToString()))
                Continue For
            End If
            'Pregunto si el numero de factura ya existe en la lista
            If listCompareBillNumber Is Nothing Then
                listCompareBillNumber = New List(Of Tuple(Of String, Integer))
                listCompareBillNumber.Add(New Tuple(Of String, Integer)(data(i).Item(1), resultSupplier.Id))
            Else
                Dim ban As Integer = listCompareBillNumber.FindAll(Function(item) item.Item1 = data(i).Item(1) And item.Item2 = resultSupplier.Id).Count
                If ban > 0 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("BillNumberDuplicated", MODULE_NAME), data(i).Item(1), (i + 1).ToString()))
                    Continue For
                Else
                    listCompareBillNumber.Add(New Tuple(Of String, Integer)(data(i).Item(1), resultSupplier.Id))
                End If
            End If

            'Pregunto si existe la linea de distribucion
            If data(i).Item(2).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DistributionLineDontChoose", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultDistributionLine As DistributionLines
            If dictDistributionLines.ContainsKey(data(i).Item(2).ToUpper) Then
                resultDistributionLine = dictDistributionLines(data(i).Item(2).ToUpper)
            Else
                resultDistributionLine = _distributionLinesRepository.GetDistributionLines(data(i).Item(2).ToUpper)
                dictDistributionLines.Add(data(i).Item(2).ToUpper, resultDistributionLine)
            End If

            If resultDistributionLine.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("DistributionLineDontExist", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            'Verifico que la linea la tenga asociada el proveedor
            Dim supplierDistributionLine As SuppliersDistributionLines
            Dim resultSupplierDistributionLines As List(Of SuppliersDistributionLines)
            resultSupplierDistributionLines = _supplierDistributionLines.GetSuppliersDistributionLinesByIdSupplier(resultSupplier.Id)
            If resultSupplierDistributionLines.Count > 0 Then
                Dim ban As Integer = resultSupplierDistributionLines.FindAll(Function(item) item.IdDistributionLine = resultDistributionLine.Id).Cast(Of SuppliersDistributionLines).ToList().Count
                If ban = 0 Then
                    listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontAssociatedDistributionLine", MODULE_NAME), (i + 1).ToString()))
                    Continue For
                Else
                    supplierDistributionLine = resultSupplierDistributionLines.Find(Function(item) item.IdDistributionLine = resultDistributionLine.Id)
                End If
            Else 'Sino esta asociada devuelve que el proveedor no tiene asociadas la linea de distribucion
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierDontAssociatedDistributionLine", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Verifico que la cuenta contable de la linea maneja centro costo
            Dim resultPUC As MainAccounts
            If dictMainAccounts.ContainsKey(resultDistributionLine.IdMainAccount) Then
                resultPUC = dictMainAccounts(resultDistributionLine.IdMainAccount)
            Else
                resultPUC = _pucRepository.GetAccountById(resultDistributionLine.IdMainAccount, False)
                dictMainAccounts.Add(resultDistributionLine.IdMainAccount, resultPUC)
            End If

            Dim resultCostCenter As Domain.Payroll.Entities.CostCenter = Nothing
            If resultPUC.Id > 0 Then
                If resultPUC.HandlesCostCenter Then
                    If data(i).Item(3) = String.Empty Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterEmpty", MODULE_NAME), (i + 1).ToString()))
                        Continue For
                    End If
                    resultCostCenter = _costCenterRepository.GetCostCenter(data(i).Item(3), True)
                    If resultCostCenter.Id = 0 Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterDontExist", MODULE_NAME), (i + 1).ToString()))
                        Continue For
                    End If
                Else
                    If data(i).Item(3) <> String.Empty Then
                        listErrors.Add(String.Format(ResourceManager.GetString("CostCenterNotRequired", MODULE_NAME), (i + 1).ToString()))
                        Continue For
                    End If
                End If
            End If

            'Valido la fecha
            If data(i).Item(4) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DateEmpty", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim dateAccountPayable As DateTime
            If IsDate(data(i).Item(4)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("DateFormatIncorrect", MODULE_NAME), (i + 1).ToString()))
                Continue For
            Else
                dateAccountPayable = DateTime.Parse(data(i).Item(4))
            End If

            Dim radicatedDate As Date
            If Not IsDate(data(i).Item(11)) Then
                listErrors.Add(String.Format(ResourceManager.GetString("DateFormatIncorrect", MODULE_NAME), (i + 1).ToString()))
                Continue For
            Else
                radicatedDate = Date.Parse(data(i).Item(11))
            End If

            If radicatedDate > DateTime.Now Then
                listErrors.Add($"La fecha de radicación del registro {(i + 1)} no debe ser mayor a la fecha actual")
                Continue For
            End If

            'Verifico que el plazo sea numerico y que no sea menor a cero
            If data(i).Item(5) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DontTerm", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If IsNumeric(data(i).Item(5)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("PayTermNotNumeric", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If CDec(data(i).Item(5)) <= 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("PaymentTermZero", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Verifico que el valor sea numerico y que no sea menor a cero
            If data(i).Item(6) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DontValue", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If IsNumeric(data(i).Item(6)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("PayValueNotNumeric", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If CDec(data(i).Item(6)) <= 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("PaymentValueZero", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Obtengo la fecha de vencimiento con la fecha de factura y el plazo
            Dim dateExpires As DateTime
            dateExpires = AddDaysDate(data(i).Item(5), data(i).Item(4))

            'Verifico que el tipo de proveedor exista
            If data(i).Item(7).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierTypeDontChoose", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultSupplierType As SupplierType
            If dictSupplierType.ContainsKey(data(i).Item(7).ToUpper) Then
                resultSupplierType = dictSupplierType(data(i).Item(7).ToUpper)
            Else
                resultSupplierType = _distributionLinesRepository.GetSupplierTypeByCode(data(i).Item(7).ToUpper)
                dictSupplierType.Add(data(i).Item(7).ToUpper, resultSupplierType)
            End If

            If resultSupplierType.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierTypeDontExist", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            'Verifico que el tipo de proveedor (El campo ParentId) no sea el padre
            If resultSupplierType.ParentId Is Nothing Then
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierTypeIsFather", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            'y que ademas sea el ultimo hijo
            If resultSupplierType.SupplierType1.Count > 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("SupplierTypeDontLastSon", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Verifico que la unidad de radicación exista
            If data(i).Item(8).ToUpper = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("FilingUnitDontChoose", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim resultFilingUnit As FilingUnit
            If dictFilingUnit.ContainsKey(data(i).Item(8).ToUpper) Then
                resultFilingUnit = dictFilingUnit(data(i).Item(8).ToUpper)
            Else
                resultFilingUnit = _distributionLinesRepository.GetFilingUnitByCode(data(i).Item(8).ToUpper)
                dictFilingUnit.Add(data(i).Item(8).ToUpper, resultFilingUnit)
            End If

            If resultFilingUnit.Id = 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("FilingUnitDontExist", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Valido la fecha de servicio
            If data(i).Item(9) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("ServiceDateEmpty", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            Dim dateService As DateTime
            If IsDate(data(i).Item(9)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("DateServiceFormatIncorrect", MODULE_NAME), (i + 1).ToString()))
                Continue For
            Else
                dateService = DateTime.Parse(data(i).Item(9))
            End If

            'Verifico que el saldo sea numerico y que no sea menor a cero
            If data(i).Item(10) = String.Empty Then
                listErrors.Add(String.Format(ResourceManager.GetString("DontBalance", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If IsNumeric(data(i).Item(10)) = False Then
                listErrors.Add(String.Format(ResourceManager.GetString("PayBalanceNotNumeric", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If
            If CDec(data(i).Item(10)) <= 0 Then
                listErrors.Add(String.Format(ResourceManager.GetString("PaymentBalanceZero", MODULE_NAME), (i + 1).ToString()))
                Continue For
            End If

            'Valido que la moneda exista
            Dim currency As Currency
            If dictCurrency.ContainsKey(data(i).Item(12)) Then
                currency = dictCurrency(data(i).Item(12))
            Else
                currency = _currencyRepository.GetCurrencyByAbbreviation(data(i).Item(12))
                dictCurrency.Add(data(i).Item(12), currency)
            End If

            If data(i).Item(10) = String.Empty Then
                listErrors.Add(String.Format("Moneda no puede ser vacia {0}", data(i).Item(12)))
                Continue For
            End If
            If currency Is Nothing Then
                listErrors.Add(String.Format("La moneda {0} no se encontro en la DB", data(i).Item(12)))
                Continue For
            End If

            'Se quita esta validación a petición de JhonRojas
            ''Se valida que el saldo no sea mayor al valor
            'If CDec(data(i).Item(10)) > CDec(data(i).Item(6)) Then
            '    listErrors.Add(String.Format(ResourceManager.GetString("BalanceIsOlderToValue", MODULE_NAME), (i + 1).ToString()))
            '    Continue For
            'End If



            'Creo el objeto que va en el listado
            Dim initialBalanceAccountPayable As New InitialBalanceAccountPayable
            With initialBalanceAccountPayable
                .SupplierId = resultSupplier.Id
                .ThirdPartyId = resultSupplier.IdThirdParty
                .SupplierDistributionLinesID = supplierDistributionLine.Id
                .MainAccountId = resultDistributionLine.IdMainAccount

                If resultCostCenter IsNot Nothing Then
                    .CostCenterId = resultCostCenter.Id
                    .CostCenterDescription = resultCostCenter.Code + " - " + resultCostCenter.Name
                Else
                    .CostCenterId = Nothing
                    .CostCenterDescription = String.Empty
                End If


                .AccountPayableId = Nothing
                .BillNumber = data(i).Item(1)
                .BillDate = dateAccountPayable
                .Term = data(i).Item(5)
                .ExpiredDate = dateExpires
                .Value = data(i).Item(6)
                .Balance = data(i).Item(10)
                .SupplierTypeId = resultSupplierType.Id
                .FilingUnitId = resultFilingUnit.Id
                .ServicePeriodDate = dateService
                .RadicatedDate = radicatedDate

                .SupplierDescription = resultSupplier.Code + " - " + resultSupplier.Name
                .MainAccountDescription = resultPUC.Number + " - " + resultPUC.Name
                .SupplierTypeDescription = resultSupplierType.Code + " - " + resultSupplierType.Name
                .FilingUnitDescription = resultFilingUnit.Code + " - " + resultFilingUnit.Name
                .Currency = currency
                .CurrencyId = currency.Id
            End With
            listInitialBalanceAccountPayable.Add(initialBalanceAccountPayable)

        Next

        Return New ActionResult(Of List(Of InitialBalanceAccountPayable)) With {.ObjectEmbbeded = listInitialBalanceAccountPayable, .MessageResult = listErrors}
    End Function

    ''' <summary>
    ''' Metodo para crear un nuevo objeto de paymentNotesAccountPayableAdvance con la cxp o con el anticipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreatePaymentNotesAccountPayableAdvance(accountPayable As AccountPayable, advancePayments As AdvancePayments, listPaymentNotesAccountPayableAdvance As List(Of PaymentNotesAccountPayableAdvance), Optional isNative As Boolean = False) As ActionResult(Of List(Of PaymentNotesAccountPayableAdvance))
        If listPaymentNotesAccountPayableAdvance Is Nothing Then
            listPaymentNotesAccountPayableAdvance = New List(Of PaymentNotesAccountPayableAdvance)
        End If
        If accountPayable IsNot Nothing Then
            For Each itemShare As AccountPayableShares In accountPayable.AccountPayableShares
                Dim paymentNotesAccountPayableAdvance As New PaymentNotesAccountPayableAdvance
                With paymentNotesAccountPayableAdvance
                    '.PaymentNoteId = idPaymentNote
                    .AccountPayableId = accountPayable.Id
                    .AccountPayableShareId = itemShare.Id
                    .AdvancePaymentId = Nothing
                    .AdjusmentValue = accountPayable.Adjustment
                    .ConceptAdjustmentId = GetConceptAdjusment(accountPayable, isNative)
                    .PercentageValue = accountPayable.Percentage
                    .AdjustmentValueShare = itemShare.ValueNoteShare
                End With
                If accountPayable.AccountPayableCommitments IsNot Nothing Then
                    For Each detail In accountPayable.AccountPayableCommitments.Where(Function(d) d.Value > 0)
                        paymentNotesAccountPayableAdvance.PaymentNoteAccountPayableBudget.Add(New PaymentNoteAccountPayableBudget With {
                            .ObligationDetailId = detail.Id,
                            .Value = detail.Value
                        })
                    Next
                End If
                listPaymentNotesAccountPayableAdvance.Add(paymentNotesAccountPayableAdvance)
            Next
        ElseIf advancePayments IsNot Nothing Then
            Dim paymentNotesAccountPayableAdvance As New PaymentNotesAccountPayableAdvance
            With paymentNotesAccountPayableAdvance
                '.PaymentNoteId = idPaymentNote
                .AccountPayableId = Nothing
                .AccountPayableShareId = Nothing
                .AdvancePaymentId = advancePayments.Id
                .AdjusmentValue = advancePayments.Adjustment
                .PercentageValue = advancePayments.Percentage
            End With
            listPaymentNotesAccountPayableAdvance.Add(paymentNotesAccountPayableAdvance)
        End If
        Return New ActionResult(Of List(Of PaymentNotesAccountPayableAdvance)) With {.StateResult = True, .ObjectEmbbeded = listPaymentNotesAccountPayableAdvance}
    End Function

    ''' <summary>
    ''' Función que asigna un concepto de ajuste a la cuenta por pagar
    ''' Si la nota viene del form de Notas Debito Crédito de CXP deja el valor que venga (puede ser Nothing)
    ''' </summary>
    ''' <param name="accountPayable"></param>
    ''' <param name="isNative"></param>
    ''' <returns></returns>
    Public Function GetConceptAdjusment(accountPayable As AccountPayable, Optional isNative As Boolean = False) As Integer?
        If accountPayable.ConceptAdjustmentId IsNot Nothing Or isNative Then
            Return accountPayable.ConceptAdjustmentId
        Else
            Dim ElectroniDocument = Me._electronicSupportDocumentRepository.GetElectronicSupportDocumentByDocumentOrigin(accountPayable.Id, accountPayable.GetType.Name())
            If ElectroniDocument?.Id > 0 Then
                Return 2
            Else
                Return Nothing
            End If
        End If
    End Function

    ''' <summary>
    ''' Se crea el comprobante contable para las notas
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateJournalVoucher(ByVal paymentNote As PaymentNotes, ByVal listAccountPayable As List(Of AccountPayable), ByVal listAdvancePayments As List(Of AdvancePayments)) As ActionResult(Of JournalVouchers)
        Dim settingPayment As SettingPayments = _settingsPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(paymentNote.IdOperatingUnit)
        If settingPayment.Id > 0 Then
            Dim accounting As New JournalVouchers
            With accounting
                'Se pregunta si el JournalVoucher viene lleno o vacio
                If paymentNote.JournalVoucherId Is Nothing Then 'Si viene vacio es porque se esta realizando desde el form de notas de pago
                    If paymentNote.Nature = 1 Then
                        .IdJournalVoucher = settingPayment.IdJournalVoucherDebitNotes
                    ElseIf paymentNote.Nature = 2 Then
                        .IdJournalVoucher = settingPayment.IdJournalVoucherCreditNotes
                    End If
                Else 'Si viene lleno es porque se esta realizando desde inventarios
                    .IdJournalVoucher = paymentNote.JournalVoucherId
                End If
                .VoucherDate = paymentNote.NoteDate
                .Status = 2
                .Detail = paymentNote.Comment
                .EntityCode = paymentNote.Code
                .EntityId = paymentNote.Id
                .EntityName = GetType(PaymentNotes).Name
                If paymentNote.EntityName <> .EntityName Then
                    .OriginEntityName = paymentNote.EntityName
                End If
                .IsClosedYear = False

                'Creo los conceptos del documento contable con los datos de las facturas o anticipos que se afectaron segun corresponda
                Dim accountDetail As JournalVoucherDetails
                If listAccountPayable IsNot Nothing Then
                    For Each itemAccountPayable As AccountPayable In listAccountPayable
                        Dim filterCabecera() As Object = {itemAccountPayable.IdAccount, itemAccountPayable.IdThirdParty, itemAccountPayable.IdCostCenter, paymentNote.Nature, itemAccountPayable.Coments, Nothing, itemAccountPayable.Adjustment, Nothing}
                        accountDetail = CreateJournalVoucherEntity(filterCabecera)
                        accounting.JournalVoucherDetails.Add(accountDetail)
                    Next
                ElseIf listAdvancePayments IsNot Nothing Then
                    For Each itemAdvancePayments As AdvancePayments In listAdvancePayments
                        Dim filterCabecera() As Object = {itemAdvancePayments.IdAccount, itemAdvancePayments.IdThirdParty, itemAdvancePayments.IdCostCenter, paymentNote.Nature, itemAdvancePayments.Comments, Nothing, itemAdvancePayments.Adjustment, Nothing}
                        accountDetail = CreateJournalVoucherEntity(filterCabecera)
                        accounting.JournalVoucherDetails.Add(accountDetail)
                    Next
                End If

                'Creo los conceptos del documento contable con los datos del detalle de la nota
                For Each itemDetail As PaymentsNoteDetails In paymentNote.PaymentsNoteDetails
                    Dim filterDetail() As Object = {itemDetail.IdAccount, itemDetail.IdThirdParty, itemDetail.IdCostCenter, itemDetail.Nature, itemDetail.Comments, itemDetail.IdRetentionConcept, itemDetail.Value, itemDetail.Percentage, itemDetail.BaseValue, itemDetail.BillingValue}
                    accountDetail = CreateJournalVoucherEntity(filterDetail)
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next
            End With

            'Valido que los valores credito y debito sean iguales y no este desbalanceado el documento
            Dim ban As Boolean = ValidateCreditDebit(accounting)
            If ban = False Then
                Dim debitValue = accounting.JournalVoucherDetails.Sum(Function(d) d.DebitValue)
                Dim creditValue = accounting.JournalVoucherDetails.Sum(Function(d) d.CreditValue)
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .MessageResult = {"El Documento contable se encuentra desbalanceado, Debitos: " & debitValue.ToString("N") & " Creditos: " & creditValue.ToString("N")}.ToList()}
            End If

            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = accounting}
        Else
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .MessageResult = {"No hay parametros de pago para la unidad operativa seleccionada."}.ToList()}
        End If
    End Function

    ''' <summary>
    ''' Crea el comprobante contable para otros libros que no sean homologables sobre la misma nota
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateJournalVoucherPaymentNoteOthersNotHomologatedBooks(ByVal paymentNote As PaymentNotes, ByVal listAccountPayable As List(Of AccountPayable), ByVal listAdvancePayments As List(Of AdvancePayments), ByVal accounting As Domain.Entities.JournalVouchers) As List(Of JournalVouchers)
        Dim accountings As New List(Of JournalVouchers)
        For Each otherNotHomologatedBook In paymentNote.PaymentsNoteDetailOthersNotHomologatedBooks
            Dim accountingLocal = accountings.Where(Function(a) a.LegalBookId = otherNotHomologatedBook.Key).FirstOrDefault
            If accountingLocal Is Nothing Then
                accountingLocal = New JournalVouchers
                accountingLocal.IdJournalVoucher = accounting.IdJournalVoucher
                accountingLocal.Detail = accounting.Detail
                accountingLocal.VoucherDate = accounting.VoucherDate
                accountingLocal.Status = accounting.Status
                accountingLocal.EntityCode = accounting.EntityCode
                accountingLocal.EntityId = accounting.EntityId
                accountingLocal.EntityName = accounting.EntityName
                accountingLocal.OriginEntityName = accounting.OriginEntityName
                accountingLocal.IsClosedYear = accounting.IsClosedYear
                accountingLocal.LegalBookId = otherNotHomologatedBook.Key
                accountings.Add(accountingLocal)
            End If

            Dim accountDetail As JournalVoucherDetails

            If listAccountPayable IsNot Nothing Then
                For Each itemAccountPayable As AccountPayable In listAccountPayable
                    Dim filterCabecera() As Object = {itemAccountPayable.IdAccount, itemAccountPayable.IdThirdParty, itemAccountPayable.IdCostCenter, paymentNote.Nature, itemAccountPayable.Coments, Nothing, itemAccountPayable.Adjustment, Nothing}
                    accountDetail = CreateJournalVoucherEntity(filterCabecera)
                    accountingLocal.JournalVoucherDetails.Add(accountDetail)
                Next
            ElseIf listAdvancePayments IsNot Nothing Then
                For Each itemAdvancePayments As AdvancePayments In listAdvancePayments
                    Dim filterCabecera() As Object = {itemAdvancePayments.IdAccount, itemAdvancePayments.IdThirdParty, itemAdvancePayments.IdCostCenter, paymentNote.Nature, itemAdvancePayments.Comments, Nothing, itemAdvancePayments.Adjustment, Nothing}
                    accountDetail = CreateJournalVoucherEntity(filterCabecera)
                    accountingLocal.JournalVoucherDetails.Add(accountDetail)
                Next
            End If

            For Each itemDetail As PaymentsNoteDetails In otherNotHomologatedBook.Value
                Dim filterDetail() As Object = {itemDetail.IdAccount, itemDetail.IdThirdParty, itemDetail.IdCostCenter, itemDetail.Nature, itemDetail.Comments, itemDetail.IdRetentionConcept, itemDetail.Value, itemDetail.Percentage, itemDetail.BaseValue, itemDetail.BillingValue}
                accountDetail = CreateJournalVoucherEntity(filterDetail)
                accountingLocal.JournalVoucherDetails.Add(accountDetail)
            Next
        Next
        Return accountings
    End Function

    ''' <summary>
    ''' Valida que haya saldo en las cuotas que se modificaran
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBillSharesOrAdvance(ByVal listAccountPayable As List(Of AccountPayable), ByVal listAdvancePayments As List(Of AdvancePayments), ByVal nature As Integer) As ActionResult(Of String)
        Dim result As New StringBuilder
        If listAccountPayable IsNot Nothing Then
            Dim accountPayable As AccountPayable
            For Each item As AccountPayable In listAccountPayable
                accountPayable = _accountPayableRepository.GetAccountPayableById(item.Id)
                If accountPayable.Balance = 0 AndAlso nature = 1 Then
                    result.AppendLine("- " + "El saldo de la fatura " + item.BillNumber.ToString + ", es igual a cero.")
                End If

                For Each itemShareOriginal As AccountPayableShares In item.AccountPayableShares
                    For Each itemShareConsult As AccountPayableShares In accountPayable.AccountPayableShares
                        If itemShareOriginal.Id = itemShareConsult.Id Then
                            If nature = 1 Then
                                If itemShareOriginal.ValueNoteShare > itemShareConsult.Balance AndAlso itemShareConsult.Balance > 0 Then
                                    result.AppendLine("- " + "El saldo de la cuota No. " + itemShareConsult.Share.ToString + " de la factura " + item.BillNumber.ToString + ", es inferior al ajuste.")
                                    'ElseIf itemShareConsult.Balance = 0 Then
                                    '    result.AppendLine("- " + "El saldo de la cuota No. " + itemShareConsult.Share.ToString + " de la factura " + item.BillNumber.ToString + ", es igual a cero.")
                                End If
                            End If
                            Exit For
                        End If
                    Next
                Next


            Next
        ElseIf listAdvancePayments IsNot Nothing Then
            Dim advancePayments As AdvancePayments
            For Each item As AdvancePayments In listAdvancePayments
                advancePayments = _advancePaymentsRepository.GetMoneyAdvanceById(item.Id)
                If nature = 1 Then
                    If item.Adjustment > advancePayments.Balance AndAlso advancePayments.Balance > 0 Then
                        result.AppendLine("- " + "El saldo del anticipo con codigo " + advancePayments.Code.ToString + ", es inferior al ajuste.")
                    ElseIf advancePayments.Balance = 0 Then
                        result.AppendLine("- " + "El saldo del anticipo con codigo " + advancePayments.Code.ToString + ", es igual a cero.")
                    End If
                End If
            Next
        End If

        If result.Length = 0 Then
            Return New ActionResult(Of String) With {.StateResult = True}
        Else
            Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = result.ToString()}
        End If
    End Function

    ''' <summary>
    ''' Crea el documento contable con el objeto de traslados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateDocumentWithPaymentTransfer(ByVal transfer As PaymentTransfer) As ActionResult(Of JournalVouchers)
        Dim settingPayment As SettingPayments = _settingsPaymentsRepository.GetSettingPaymentsByIdOperatingUnit(transfer.OperatingUnitId)
        If settingPayment.Id > 0 Then
            Dim accounting As New JournalVouchers
            With accounting
                .IdJournalVoucher = settingPayment.IdJournalVoucherTranslation
                .VoucherDate = transfer.DocumentDate
                .Status = 2
                .Detail = transfer.Observations
                .EntityCode = transfer.Code
                .EntityId = transfer.Id
                .EntityName = GetType(PaymentTransfer).Name
                .IsClosedYear = False

                Dim accountDetail As JournalVoucherDetails
                'Se crea el detalle del documento contable con los datos de la cabecera del traslado(PaymentTransfer)
                Dim valSum As Decimal
                valSum = transfer.PaymentTransferDetail.Sum(Function(x) x.Value)
                If transfer.PaymentTransferOtherConcept IsNot Nothing AndAlso transfer.PaymentTransferOtherConcept.Count > 0 Then 'Si diligenciaron otros conceptos
                    valSum += transfer.PaymentTransferOtherConcept.Where(Function(z) z.Nature = 1).Sum(Function(x) x.Value) 'Sumo debito
                    valSum -= transfer.PaymentTransferOtherConcept.Where(Function(z) z.Nature = 2).Sum(Function(x) x.Value) 'Resto credito
                End If
                Dim filterCabecera() As Object = {transfer.MainAccountId, transfer.ThirdPartyId, transfer.CostCenterId, 2, transfer.Observations, Nothing, valSum, Nothing}
                accountDetail = CreateJournalVoucherEntity(filterCabecera)
                accounting.JournalVoucherDetails.Add(accountDetail)

                'Se crea los detalles del documento contable con los datos de los detalles del traslado(PaymentTrasnferDetail)
                For Each itemDetail As PaymentTransferDetail In transfer.PaymentTransferDetail
                    Dim filterDetail() As Object = {itemDetail.MainAccountId, itemDetail.ThirdPartyId, itemDetail.CostCenterId, 1, String.Empty, Nothing, itemDetail.Value, Nothing}
                    accountDetail = CreateJournalVoucherEntity(filterDetail)
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next
                'se crea un detalle por otros conceptos
                For Each itemOther In transfer.PaymentTransferOtherConcept
                    Dim filterDetail() As Object = {itemOther.MainAccountId, itemOther.ThirdPartyId, itemOther.CostCenterId, itemOther.Nature, String.Empty, Nothing, itemOther.Value, Nothing}
                    accountDetail = CreateJournalVoucherEntity(filterDetail)
                    accounting.JournalVoucherDetails.Add(accountDetail)
                Next
            End With

            'Valido que los valores credito y debito sean iguales y no este desbalanceado el documento
            Dim ban As Boolean = ValidateCreditDebit(accounting)
            If ban = False Then
                Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .MessageResult = {"El Documento contable esta desbalanceado."}.ToList()}
            End If

            Return New ActionResult(Of JournalVouchers) With {.StateResult = True, .ObjectEmbbeded = accounting}
        Else
            Return New ActionResult(Of JournalVouchers) With {.StateResult = False, .MessageResult = {"No hay parametros de pago para la unidad operativa seleccionada."}.ToList()}
        End If
    End Function

    ''' <summary>
    ''' Valida que haya saldo en el anticipo y en la cuota
    ''' </summary>
    ''' <param name="paymentTransfer"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateBalance(ByVal paymentTransfer As PaymentTransfer) As ActionResult(Of String)
        Dim result As New StringBuilder

        Dim valSum As Decimal
        valSum = paymentTransfer.PaymentTransferDetail.Sum(Function(x) x.Value)
        If paymentTransfer.PaymentTransferOtherConcept IsNot Nothing AndAlso paymentTransfer.PaymentTransferOtherConcept.Count > 0 Then
            valSum += paymentTransfer.PaymentTransferOtherConcept.Where(Function(z) z.Nature = 1).Sum(Function(x) x.Value) 'Sumo debito
            valSum -= paymentTransfer.PaymentTransferOtherConcept.Where(Function(z) z.Nature = 2).Sum(Function(x) x.Value) 'Resto credito
        End If

        'Valida que haya saldo en el anticipo
        Dim advancePayments As AdvancePayments = _advancePaymentsRepository.GetMoneyAdvanceById(paymentTransfer.AdvancePaymentId)
        If valSum > advancePayments.Balance Then
            'result.AppendLine("- " + "El saldo del anticipo con codigo " + advancePayments.Code.ToString + " es menor al total del valor a cruzar.")
        ElseIf advancePayments.Balance = 0 OrElse advancePayments.Balance = Nothing Then
            result.AppendLine("- " + "El saldo del anticipo con codigo " + advancePayments.Code.ToString + " es igual a 0.")
        End If

        'Valida que haya saldo en las cuotas
        For Each itemDetail As PaymentTransferDetail In paymentTransfer.PaymentTransferDetail
            Dim accountPayableShare As AccountPayableShares = _accountPayableRepository.GetAccountPayableShareById(itemDetail.AccountPayableShareId)
            If itemDetail.Value > accountPayableShare.Balance Then
                result.AppendLine("- " + "El saldo de la cuota No. " + accountPayableShare.Share.ToString + " de la factura " + accountPayableShare.InvoiceBillNumber.ToString + " es menor al valor a cruzar.")
            ElseIf accountPayableShare.Balance = 0 OrElse accountPayableShare.Balance = Nothing Then
                result.AppendLine("- " + "El saldo de la cuota No. " + accountPayableShare.Share.ToString + " de la factura " + accountPayableShare.InvoiceBillNumber.ToString + " es igual a 0.")
            End If
        Next

        If result.Length = 0 Then
            Return New ActionResult(Of String) With {.StateResult = True}
        Else
            Return New ActionResult(Of String) With {.StateResult = False, .ObjectEmbbeded = result.ToString()}
        End If
    End Function

    ''' <summary>
    ''' Modifica el saldo de anticipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyBalanceAdvancePayments(ByVal paymentTransfer As PaymentTransfer) As AdvancePayments
        Dim advancePayments As AdvancePayments = _advancePaymentsRepository.GetMoneyAdvanceById(paymentTransfer.AdvancePaymentId)
        Dim valSum As Decimal

        valSum = paymentTransfer.PaymentTransferDetail.Sum(Function(x) x.Value)
        If paymentTransfer.PaymentTransferOtherConcept IsNot Nothing AndAlso paymentTransfer.PaymentTransferOtherConcept.Count > 0 Then
            valSum += paymentTransfer.PaymentTransferOtherConcept.Where(Function(z) z.Nature = 1).Sum(Function(x) x.Value) 'Sumo debito
            valSum -= paymentTransfer.PaymentTransferOtherConcept.Where(Function(z) z.Nature = 2).Sum(Function(x) x.Value) 'Resto credito
        End If

        If valSum > advancePayments.Balance Then
            advancePayments.Balance = valSum - advancePayments.Balance
        Else
            advancePayments.Balance = advancePayments.Balance - valSum
        End If

        advancePayments.CreditValue = advancePayments.CreditValue + valSum
        advancePayments.MarkAsModified()
        Return advancePayments
    End Function

    ''' <summary>
    ''' Se modifica el saldo de las facturas y de las cuotas 
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ModifyBalanceShare(ByVal paymentTransfer As PaymentTransfer, ByVal listBillNumber As List(Of Tuple(Of String, Integer))) As List(Of AccountPayable)
        Dim listPaymentTransferDetail As List(Of PaymentTransferDetail) = paymentTransfer.PaymentTransferDetail.ToList
        Dim listAccountPayable As List(Of AccountPayable) = New List(Of AccountPayable)
        Dim valSum As Decimal
        'valSum = paymentTransfer.PaymentTransferDetail.Sum(Function(x) x.Value)
        If listBillNumber IsNot Nothing AndAlso listBillNumber.Count > 0 Then


            For i As Integer = 0 To listBillNumber.Count - 1
                Dim accountPayable As AccountPayable = _accountPayableRepository.GetAccountPayableByBillNumber(listBillNumber(i).Item1, listBillNumber(i).Item2)

                Dim res = paymentTransfer.PaymentTransferDetail.Where(Function(x) x.NumberBill = listBillNumber(i).Item1).FirstOrDefault
                valSum = res.Value

                accountPayable.Balance = accountPayable.Balance - valSum

                For Each itemAP As AccountPayableShares In accountPayable.AccountPayableShares
                    For Each itemDetail As PaymentTransferDetail In paymentTransfer.PaymentTransferDetail
                        If itemAP.Id = itemDetail.AccountPayableShareId Then
                            itemAP.Balance = itemAP.Balance - itemDetail.Value
                            itemAP.ValueTransfers = itemAP.ValueTransfers + itemDetail.Value
                            itemAP.MarkAsModified()
                        End If
                    Next
                Next

                accountPayable.MarkAsModified()
                listAccountPayable.Add(accountPayable)
            Next
        End If
        Return listAccountPayable
    End Function

#End Region

End Class