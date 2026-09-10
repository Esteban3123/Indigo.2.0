'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure
Imports Domain.Payroll
Imports Infrastructure.CrossCutting.Resources

Public Class NotesDebitCreditRepository
    Inherits GenericRepository(Of PaymentNotes)
    Implements INotesDebitCreditRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Contiene o establece la tarifa de iva para el concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _generalLedgerIVA As IGeneralLedgerIVARepository

    ''' <summary>
    ''' Contiene o establece la cuenta de los conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Private _mainAccounts As ICostDistributionsRepository

    ''' <summary>
    ''' Contiene o establece el centro de costo del concepto
    ''' </summary>
    ''' <remarks></remarks>
    Private _CostCenter As Domain.Payroll.ICostCenterRepository



    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork, ByVal generalLedgerIVA As IGeneralLedgerIVARepository, ByVal costCenter As Domain.Payroll.ICostCenterRepository, ByVal mainAccount As ICostDistributionsRepository)
        MyBase.New(context)
        _context = context

        If generalLedgerIVA Is Nothing Then
            Throw New ArgumentNullException("generalLedgerIVA")
        End If
        If costCenter Is Nothing Then
            Throw New ArgumentNullException("costCenter")
        End If
        _generalLedgerIVA = generalLedgerIVA

        _CostCenter = costCenter

        _mainAccounts = mainAccount
    End Sub

    Public Function SP_ImportBillsToPortfolioNote(XmlObject As String, XmlParameters As String) As List(Of SP_ImportBillsToPortfolioNote_Result) Implements INotesDebitCreditRepository.SP_ImportBillsToPortfolioNote
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportBillsToPortfolioNote(XmlObject, XmlParameters).ToList
    End Function

    Public Function SP_ImportAdvancesToPortfolioNote(XmlObject As String, XmlParameters As String) As List(Of SP_ImportAdvancesToPortfolioNote_Result) Implements INotesDebitCreditRepository.SP_ImportAdvancesToPortfolioNote
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ImportAdvancesToPortfolioNote(XmlObject, XmlParameters).ToList
    End Function

    Function SP_ConfirmPaymentNotes(AccountsPayableXml As String, codeUser As String, isMassiveConfirm As Boolean) As List(Of SP_ConfirmPayableNote_Result) Implements INotesDebitCreditRepository.SP_ConfirmPaymentNotes
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmPayableNote(AccountsPayableXml, codeUser, isMassiveConfirm).ToList()
    End Function

    ''' <summary>
    ''' Obtiene una nota debito/credito por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsNote(code As String, Optional tracking As Boolean = True) As PaymentNotes Implements INotesDebitCreditRepository.GetPaymentsNote
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As PaymentNotes In Me._context.PaymentNotes.Include("PaymentsNoteDetails").Include("PaymentNotesAccountPayableAdvance").Include("PaymentNotesAccountPayableAdvance.PaymentNoteAccountPayableBudget").Include("Currency").AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            'Se consulta el nombre del proveedor y de la linea de distribucion
            Dim supplierDistributionLine = (From sdl In _context.SuppliersDistributionLines.AsNoTracking() Where res.IdSupplierDistributionLines = sdl.Id Select sdl).FirstOrDefault
            Dim supplier = (From s In _context.Supplier.AsNoTracking Where supplierDistributionLine.IdSupplier = s.Id Select s).FirstOrDefault
            Dim distributionLine = (From dl In _context.DistributionLines.AsNoTracking Where supplierDistributionLine.IdDistributionLine = dl.Id Select dl).FirstOrDefault
            Dim AccountPayable = (From dl In _context.AccountPayable.AsNoTracking Where res.IdAccountPayable = dl.Id Select dl).FirstOrDefault

            res.DescriptionSupplier = supplier.Code + " - " + supplier.Name + " - " + distributionLine.Code + " - " + distributionLine.Name

            If AccountPayable IsNot Nothing Then
                res.DescriptionAccountPayable = AccountPayable.Code + " - " + AccountPayable.BillNumber
                res.AccountPayable = AccountPayable
            End If

            If res.IdCostCenter IsNot Nothing Then
                Dim costCenter = (From cc In _context.CostCenter.AsNoTracking Where res.IdCostCenter = cc.Id Select cc).FirstOrDefault
                res.DescriptionCostCenter = costCenter.Code + " - " + costCenter.Name
            End If

            For Each itemDetalle As PaymentsNoteDetails In res.PaymentsNoteDetails
                Dim concept = (From nc In _context.AccountPayableConceptNotes.AsNoTracking Where itemDetalle.IdAccountPayableConceptNotes = nc.Id Select nc).FirstOrDefault
                itemDetalle.DescriptionNoteConcept = concept.Code + " - " + concept.Name

                Dim account = (From a In _context.MainAccounts.AsNoTracking Where itemDetalle.IdAccount = a.Id Select a).FirstOrDefault
                itemDetalle.NumberNameMainAccount = account.Number + " - " + account.Name

                'Nit y nombre del tercero
                itemDetalle.DescriptionThirdParty = (From x In _context.ThirdParty.AsNoTracking Where itemDetalle.IdThirdParty = x.Id Select String.Concat(x.Nit, " - ", x.Name)).FirstOrDefault

                If itemDetalle.IdCostCenter IsNot Nothing Then
                    Dim cost = (From c In _context.CostCenter.AsNoTracking Where itemDetalle.IdCostCenter = c.Id Select c).FirstOrDefault
                    itemDetalle.DescriptionCostCenter = cost.Code + " - " + cost.Name
                End If

                'validacion para llenar la informacion para la childlist
                If itemDetalle.IdGeneralLedgerIVA.HasValue Then
                    Dim GeneralLedgerIVA As GeneralLedgerIVA = _generalLedgerIVA.GetGeneralLedgerIVAById(itemDetalle.IdGeneralLedgerIVA)
                    Dim AccountConcept As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(itemDetalle.IdAccount)

                    Dim CostCenterDataCodeName = ""

                    If itemDetalle.CostCenter IsNot Nothing Then
                        Dim CostCenterData As Domain.Payroll.Entities.CostCenter = _CostCenter.GetCostCenterById(itemDetalle.IdCostCenter, False)
                        CostCenterDataCodeName = CostCenterData.Code + " - " + CostCenterData.Name
                    End If

                    Dim ConceptCodeName = AccountConcept.Number + " - " + AccountConcept.Name
                    Dim NatureName = IIf(itemDetalle.Nature = 1, ResourceManager.GetString("AccountNatureDebit"), ResourceManager.GetString("AccountNatureCredit"))

                    'se llena el obejto para la childlist
                    itemDetalle.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                        .MainAccountCodeName = ConceptCodeName,
                        .CostCenterCodeName = CostCenterDataCodeName,
                        .NatureName = NatureName,
                        .ValueDetailConcept = If(itemDetalle.TaxRegistration = 1, itemDetalle.BaseValue + itemDetalle.IVAValue, itemDetalle.Value)
                        })

                    Dim Observations = String.Concat("Tarifa IVA", CInt(GeneralLedgerIVA.Percentage), " % artículo")

                    'validacion para llenar la tarifa iva segun el registro de IVA
                    If itemDetalle.TaxRegistration = 2 Then
                        Dim acountDescountable As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountPurchaseService)

                        itemDetalle.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = acountDescountable.Number + " - " + acountDescountable.Name,
                                .CostCenterCodeName = "",
                                .Observations = Observations,
                                .NatureName = NatureName,
                                .ValueDetailConcept = itemDetalle.IVAValue
                                })

                    ElseIf itemDetalle.TaxRegistration = 1 Then
                        Dim accountDebit As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountDebitControlFiscal)
                        Dim accountCredit As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountCreditControlFiscal)

                        itemDetalle.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = accountDebit.Number + " - " + accountDebit.Name,
                                .CostCenterCodeName = "",
                                .Observations = Observations,
                                .NatureName = NatureName,
                                .ValueDetailConcept = itemDetalle.IVAValue
                                })

                        itemDetalle.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = accountCredit.Number + " - " + accountCredit.Name,
                                .CostCenterCodeName = "",
                                .Observations = Observations,
                                .NatureName = IIf(itemDetalle.Nature = 1, "Credito", "Debito"),
                                .ValueDetailConcept = itemDetalle.IVAValue
                                })

                    ElseIf itemDetalle.TaxRegistration = 4 Then

                        itemDetalle.PaymentsNoteDetailsAccountInfo.Add(New PaymentsNoteDetailsAccountInfo With {
                                .MainAccountCodeName = ConceptCodeName,
                                .CostCenterCodeName = CostCenterDataCodeName,
                                .Observations = Observations,
                                .NatureName = NatureName,
                                .ValueDetailConcept = itemDetalle.IVAValue
                                })

                    End If
                End If
            Next

            res.OriginalValue = (From d As PaymentNotes In Me._context.PaymentNotes.Include("PaymentsNoteDetails").Include("PaymentNotesAccountPayableAdvance").AsNoTracking().Include("PaymentNotesAccountPayableAdvance.PaymentNoteAccountPayableBudget").AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New PaymentNotes()
        End If
    End Function

    ''' <summary>
    ''' Consulta una nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentsNoteById(id As Integer, Optional tracking As Boolean = True) As PaymentNotes Implements INotesDebitCreditRepository.GetPaymentsNoteById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As PaymentNotes In Me._context.PaymentNotes.AsNoTracking.Include("PaymentsNoteDetails").AsNoTracking Where d.Id = id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As PaymentNotes In Me._context.PaymentNotes.AsNoTracking().Include("PaymentsNoteDetails").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New PaymentNotes()
        End If
    End Function

    ''' <summary>
    ''' Valida que las cuentas por pagar agregadas a la rejilla no existan en una programacion de pagos confirmada
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateSchedulePaymentDetailContainsAccountPayable(ListAccountPayable As List(Of AccountPayable)) As List(Of AccountPayable) Implements INotesDebitCreditRepository.ValidateSchedulePaymentDetailContainsAccountPayable
        If ListAccountPayable Is Nothing OrElse ListAccountPayable.Count = 0 Then
            Throw New ArgumentNullException("ListAccountPayable")
        End If
        Dim operatorIn As New List(Of Integer)
        ListAccountPayable.ForEach(Sub(item)
                                       operatorIn.Add(item.Id)
                                   End Sub)
        Dim ListCompare = (From spd In _context.SchedulePaymentDetail.AsNoTracking
                           Join sp In _context.SchedulePayment.AsNoTracking On sp.Id Equals spd.SchedulePaymentId
                           Join ap In _context.AccountPayable.AsNoTracking On ap.Id Equals spd.AccountPayableId
                           Where operatorIn.Contains(spd.AccountPayableId) AndAlso spd.GeneratedVoucher = False AndAlso (sp.Status = 2 Or sp.Status = 4 Or sp.Status = 1)
                           Select ap).ToList
        If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
            Return ListCompare
        End If
        Return Nothing
    End Function

    ''' <summary>
    ''' Valida que las cuentas por pagar agregadas a la rejilla no existan en un comprobante de egreso confirmado
    ''' </summary>
    ''' <param name="ListAccountPayable"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidateDischargeBillsContainsAccountPayable(ListAccountPayable As List(Of AccountPayable)) As List(Of AccountPayable) Implements INotesDebitCreditRepository.ValidateDischargeBillsContainsAccountPayable
        If ListAccountPayable Is Nothing OrElse ListAccountPayable.Count = 0 Then
            Throw New ArgumentNullException("ListAccountPayable")
        End If
        Dim operatorIn As New List(Of Integer)
        ListAccountPayable.ForEach(Sub(item)
                                       operatorIn.Add(item.Id)
                                   End Sub)
        Dim ListCompare = (From db In _context.DischargeBill.AsNoTracking
                           Join ap In _context.AccountPayable.AsNoTracking On ap.Id Equals db.IdAccountPayable
                           Join vtd In _context.VoucherTransactionDetails.AsNoTracking On vtd.Id Equals db.IdVoucherTransactionD
                           Join vt In _context.VoucherTransaction.AsNoTracking On vt.Id Equals vtd.IdVoucherTransaction
                           Where operatorIn.Contains(db.IdAccountPayable) AndAlso vt.Status = 2
                           Select ap).ToList
        If ListCompare IsNot Nothing AndAlso ListCompare.Count > 0 Then
            Return ListCompare
        End If
        Return Nothing
    End Function

    Public Function ListPaymentNotesMassiveConfirm(listDocuments As List(Of String)) As List(Of PaymentNotes) Implements INotesDebitCreditRepository.ListPaymentNotesMassiveConfirm
        Return (From cr In _context.PaymentNotes.Include("PaymentsNoteDetails").Include("PaymentNotesAccountPayableAdvance") Where listDocuments.Contains(cr.Code) Select cr).ToList()
    End Function

End Class
