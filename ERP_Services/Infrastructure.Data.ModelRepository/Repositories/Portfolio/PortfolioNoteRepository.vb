'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Carlos Ernesto Cordoba
' Created          : 03-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure
Imports Domain.Payroll

#End Region

Public Class PortfolioNoteRepository
    Inherits GenericRepository(Of PortfolioNote)
    Implements IPortfolioNoteRepository

#Region "Builder"

    'contexto de cartera
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
    ''' contiene o establece la concatenacion del codigo y el nombre del nombre de costo
    ''' </summary>
    ''' <remarks></remarks>
    Private CostCenterDataCodeName As String

    Public Sub New(ByVal contex As IGlobalModelUnitOfWork, ByVal generalLedgerIVA As IGeneralLedgerIVARepository, ByVal costCenter As Domain.Payroll.ICostCenterRepository, ByVal mainAccount As ICostDistributionsRepository)
        MyBase.New(contex)
        _context = contex

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' obtiene una nota por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function getPortfolioNoteById(id As Integer, Optional tracking As Boolean = True) As PortfolioNote Implements IPortfolioNoteRepository.getPortfolioNoteById
        Dim res As PortfolioNote
        If tracking Then
            res = (From pn In _context.PortfolioNote Where pn.Id = id Select pn).FirstOrDefault()
        Else
            res = (From pn In _context.PortfolioNote.AsNoTracking() Where pn.Id = id Select pn).FirstOrDefault()
        End If
        If res IsNot Nothing Then
            If tracking Then
                res.OriginalValue = (From pn In _context.PortfolioNote.AsNoTracking() Where pn.Id = id Select pn).FirstOrDefault()
                res.ThirdPartyId = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c.ThirdPartyId).FirstOrDefault()
            End If
            Return res
        Else
            Return New PortfolioNote
        End If
    End Function

    ''' <summary>
    ''' obtiene una nota por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteByCode(code As String) As PortfolioNote Implements IPortfolioNoteRepository.GetPortfolioNoteByCode
        Dim res = (From pn In _context.PortfolioNote.Include("Currency") Where pn.Code = code Select pn).FirstOrDefault()
        If res IsNot Nothing Then
            If res.NoteType <> 5 AndAlso res.CustomerId IsNot Nothing Then
                Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = res.CustomerId Select c).FirstOrDefault()
                res.CodeNameCustomer = customer.Nit + " - " + customer.Name
                Dim third = (From tp In _context.ThirdParty.AsNoTracking() Where customer.ThirdPartyId = tp.Id Select tp).FirstOrDefault()
                res.ThirdPartyId = third.Id
                res.NitNameThirParty = third.Nit + " - " + third.Name
            ElseIf res.NoteType = 5 Then
                Dim portfolioTransfer = (From pt In _context.PortfolioTransfer.AsNoTracking() Where pt.Id = res.PortfolioTransferId Select pt).FirstOrDefault()
                If portfolioTransfer IsNot Nothing Then
                    res.PortfolioTransferCode = portfolioTransfer.Code
                    If portfolioTransfer.CustomerId IsNot Nothing Then
                        Dim customer = (From c In _context.Customer.AsNoTracking() Where c.Id = portfolioTransfer.CustomerId Select c).FirstOrDefault()
                        res.CustomerId = customer.Id
                        res.CodeNameCustomer = customer.Nit + " - " + customer.Name
                        Dim third = (From tp In _context.ThirdParty.AsNoTracking() Where customer.ThirdPartyId = tp.Id Select tp).FirstOrDefault()
                        res.ThirdPartyId = third.Id
                        res.NitNameThirParty = third.Nit + " - " + third.Name
                        res.PortfolioTransferCodeNameCustomer = res.PortfolioTransferCode + " - " + res.NitNameThirParty
                    Else
                        Dim third = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = portfolioTransfer.ThirdPartyId Select t).FirstOrDefault()
                        res.ThirdPartyId = third.Id
                        res.NitNameThirParty = third.Nit + " - " + third.Name
                        res.PortfolioTransferCodeNameCustomer = res.PortfolioTransferCode + " - " + res.NitNameThirParty
                        Dim customer = (From c In _context.Customer.AsNoTracking() Where c.ThirdPartyId = res.ThirdPartyId Select c).FirstOrDefault()
                        If customer IsNot Nothing Then
                            res.CustomerId = customer.Id
                            res.CodeNameCustomer = customer.Nit + " - " + customer.Name
                            res.PortfolioTransferCodeNameCustomer = res.PortfolioTransferCode + " - " + res.CodeNameCustomer
                        End If
                    End If
                End If
            End If

            If res.PortfolioAdvanceId IsNot Nothing Then
                res.CodeNameAdvanceDistribution = (From pa In _context.PortfolioAdvance.AsNoTracking() Where pa.Id = res.PortfolioAdvanceId Select pa).FirstOrDefault().Code
            End If
            res.OriginalValue = (From pn In _context.PortfolioNote.AsNoTracking() Where pn.Code = code Select pn).FirstOrDefault()
            Return res
        Else
            Return New PortfolioNote
        End If
    End Function

    ''' <summary>
    ''' lista todos los documnetos para confirmarlos masivamente
    ''' </summary>
    ''' <param name="listDocuments"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListPortfolioNoteMassiveConfirm(listDocuments As List(Of String)) As List(Of PortfolioNote) Implements IPortfolioNoteRepository.ListPortfolioNoteMassiveConfirm
        Return (From cr In _context.PortfolioNote.Include("PortfolioNoteDetail").Include("PortfolioNoteAccountReceivableAdvance").Include("PortfolioNoteDistribution") Where listDocuments.Contains(cr.Code) Select cr).ToList()
    End Function

    ''' <summary>
    ''' metodo para obtener los detalles de la nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteDetailByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteDetail) Implements IPortfolioNoteRepository.GetPortfolioNoteDetailByIdPortfolioNote
        Dim res = (From pnd In _context.PortfolioNoteDetail Where pnd.PortfolioNoteId = idPortfolioNote Select pnd).ToList()
        If res.Count > 0 Then
            For Each itemDetalle In res
                Dim portfolioNoteConcept = (From pnc In _context.PortfolioNoteConcept.AsNoTracking() Where pnc.Id = itemDetalle.PortfolioNoteConceptId Select pnc).FirstOrDefault()
                itemDetalle.CodeNameNoteConcept = portfolioNoteConcept.Code + " - " + portfolioNoteConcept.Name
                Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = itemDetalle.MainAccountId Select ma).FirstOrDefault()
                itemDetalle.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
                Dim thirdParty = (From tp In _context.ThirdParty.AsNoTracking() Where tp.Id = itemDetalle.ThirdPartyId Select tp).FirstOrDefault()
                itemDetalle.CodeNameThirdParty = thirdParty.Nit + " - " + thirdParty.Name
                If itemDetalle.CostCenterId IsNot Nothing Then
                    Dim costCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = itemDetalle.CostCenterId Select cc).FirstOrDefault()
                    itemDetalle.CodeNameCostCenter = costCenter.Code + " - " + costCenter.Name
                End If
                If itemDetalle.RetentionConceptId IsNot Nothing Then
                    Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking() Where rc.Id = itemDetalle.RetentionConceptId Select rc).FirstOrDefault()
                    itemDetalle.CodeNameRetentionConcept = retentionConcept.Code + " - " + retentionConcept.Name
                End If

                'validacion para llenar la informacion para la childlist
                If itemDetalle.IdGeneralLedgerIVA.HasValue Then
                    Dim GeneralLedgerIVA As GeneralLedgerIVA = _generalLedgerIVA.GetGeneralLedgerIVAById(itemDetalle.IdGeneralLedgerIVA)
                    itemDetalle.GeneralLedgerIVA = GeneralLedgerIVA
                    If GeneralLedgerIVA?.IdAccountPurchaseService IsNot Nothing And GeneralLedgerIVA?.IdAccountPurchaseService > 0 Then
						Dim MainAccountIVA As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountPurchaseService)
						Dim AccountConcept As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(itemDetalle.MainAccountId)
						Dim GeneralLedgerCodeName = MainAccountIVA.Number + " - " + GeneralLedgerIVA.Name
						Dim ConceptCodeName = AccountConcept.Number + " - " + AccountConcept.Name
						'se llena el obejto para la childlist
						itemDetalle.PortfolioNoteDetailChild.Add(New PortfolioNoteDetailChild With {
							.MainAccountCodeName = ConceptCodeName,
							.CostCenterCodeName = CostCenterDataCodeName,
							.NatureName = IIf(itemDetalle.Nature = 1, "Débito", "Crédito"),
							.ValueDetailConcept = itemDetalle.Value
							 })

						'Si maneja impuesto se agregan los campos al detalle
						If portfolioNoteConcept.HandleTax = True Then
							'Dim taxMainAccount As Domain.Payroll.Entities.MainAccounts = _mainAccounts.GetMainAccountById(GeneralLedgerIVA.IdAccountSale)

							itemDetalle.PortfolioNoteDetailChild.Add(New PortfolioNoteDetailChild With {
										.MainAccountCodeName = GeneralLedgerCodeName,
										.CostCenterCodeName = "",
										.NatureName = IIf(itemDetalle.Nature = 1, "Débito", "Crédito"),
										.ValueDetailConcept = itemDetalle.IvaRate
										})
						End If

					End If

					CostCenterDataCodeName = ""

					If itemDetalle.CostCenterId IsNot Nothing Then
						Dim CostCenterData As Domain.Payroll.Entities.CostCenter = _CostCenter.GetCostCenterById(itemDetalle.CostCenterId, False)
						CostCenterDataCodeName = CostCenterData.Code + " - " + CostCenterData.Name
					End If

				End If
            Next
            Return res
        Else
            Return New List(Of PortfolioNoteDetail)
        End If
    End Function

    ''' <summary>
    ''' obtiene las distribuciones 
    ''' </summary>
    ''' <param name="portfolioNoteId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPortfolioNoteDistributionByPortfolioNoteId(portfolioNoteId As Integer) As List(Of PortfolioNoteDistribution) Implements IPortfolioNoteRepository.GetPortfolioNoteDistributionByPortfolioNoteId
        Dim res = (From pnd In _context.PortfolioNoteDistribution Where pnd.PortfolioNoteId = portfolioNoteId Select pnd).ToList()
        If res.Count > 0 Then
            For Each item In res
                item.NitNameCustomer = (From c In _context.Customer.AsNoTracking() Where c.Id = item.CustomerId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
                item.NumberNameMainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                If item.CostCenterId IsNot Nothing Then
                    item.CodeNameCostCenter = (From cc In _context.CostCenter.AsNoTracking() Where cc.Id = item.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault()
                End If
            Next
        End If
        Return res
    End Function

    ''' <summary>
    ''' metodo para obtener la factura asociada al detalle de la nota
    ''' </summary>
    ''' <param name="PortfolioNoteAccountReceivableAdvanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceByPortfolioNoteAccountReceivableAdvanceId(PortfolioNoteAccountReceivableAdvanceId As Integer) As Invoice Implements IPortfolioNoteRepository.GetInvoiceByPortfolioNoteAccountReceivableAdvanceId
        Dim res = (From pnara In _context.PortfolioNoteAccountReceivableAdvance.AsNoTracking()
                   Join ar In _context.AccountReceivable.AsNoTracking()
                        On pnara.AccountReceivableId Equals ar.Id
                   Join i In _context.Invoice.AsNoTracking()
                        On ar.InvoiceId Equals i.Id
                   Where pnara.Id = PortfolioNoteAccountReceivableAdvanceId Select i).FirstOrDefault()
        If res Is Nothing Then
            Return New Invoice()
        Else
            Return res
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener la factura asociada al detalle de la nota
    ''' </summary>
    ''' <param name="listPortfolioNoteAccountReceivableAdvanceId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInvoiceByPortfolioNoteAccountReceivableAdvanceId(listPortfolioNoteAccountReceivableAdvanceId As List(Of Integer)) As List(Of Invoice) Implements IPortfolioNoteRepository.GetInvoiceByPortfolioNoteAccountReceivableAdvanceId
        Dim res = (From pnara In _context.PortfolioNoteAccountReceivableAdvance.AsNoTracking()
                   Join ar In _context.AccountReceivable.AsNoTracking()
                        On pnara.AccountReceivableId Equals ar.Id
                   Join i In _context.Invoice.AsNoTracking()
                        On ar.InvoiceId Equals i.Id
                   Where listPortfolioNoteAccountReceivableAdvanceId.Contains(pnara.Id) Select New With {.pnaraId = pnara.Id, .invoice = i})

        Dim result As New List(Of Invoice)

        If res.Any() Then
            Dim query = res.Select(Function(x) x.invoice).ToList()

            For Each item In res
                Dim invoice = query.FirstOrDefault(Function(f) f.Id = item.invoice.Id)
                invoice.PortfolioNoteAccountReceivableAdvanceId = item.pnaraId
            Next

            result = query
        End If

        Return result
    End Function

    ''' <summary>
    ''' metodo para obtener las facturas o los anticipos asociados a esa nota
    ''' </summary>
    ''' <param name="idPortfolioNote"></param>
    ''' <returns></returns>
    Public Function GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote(idPortfolioNote As Integer) As List(Of PortfolioNoteAccountReceivableAdvance) Implements IPortfolioNoteRepository.GetPortfolioNoteAccountReceivableAdvanceByIdPortfolioNote
        Dim portfolioNote = (From pn In _context.PortfolioNote.AsNoTracking() Where pn.Id = idPortfolioNote Select pn).FirstOrDefault()
        Dim res = (From pnara In _context.PortfolioNoteAccountReceivableAdvance.Include("PortfolioNoteAccountReceivableDetail") Where pnara.PortfolioNoteId = idPortfolioNote Select pnara).ToList()
        If res.Count > 0 Then
            For Each item In res
                Select Case portfolioNote.NoteType
                    Case 1
                        Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select ma).FirstOrDefault()
                        item.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
                        Dim accountRecivableAccounting = (From aca In _context.AccountReceivableAccounting.AsNoTracking() Where aca.Id = item.AccountReceivableAccountingId Select aca).FirstOrDefault()
                        item.Balance = accountRecivableAccounting.Balance
                        item.Value = accountRecivableAccounting.Value
                        Dim accountReceivable = (From ar In _context.AccountReceivable.AsNoTracking() Where ar.Id = item.AccountReceivableId Select ar).FirstOrDefault()
                        item.InvoiceNumber = accountReceivable.InvoiceNumber
                        Select Case accountRecivableAccounting.MainAccountId
                            Case accountReceivable.AccountWithoutRadicateId
                                item.PortfolioStatusName = "1-Sin Radicar"
                            Case accountReceivable.AccountRadicateId
                                item.PortfolioStatusName = "3-Radicada Entidad"
                            Case accountReceivable.AccountObjectionRemediedId
                                item.PortfolioStatusName = "4-Glosada sin Conciliar"
                            Case accountReceivable.AccountConciliationId
                                item.PortfolioStatusName = "12-Glosada Conciliada"
                            Case accountReceivable.AccountHardCollectionId
                                item.PortfolioStatusName = "15-Cuenta de Dificil Recaudo"
                            Case accountReceivable.AccountLegalCollectionId
                                item.PortfolioStatusName = "16-Cobro Juridico"
                            Case Else
                                item.PortfolioStatusName = "N/A"
                        End Select
                    Case 2
                        Dim accountReceivable = (From ar In _context.AccountReceivable.AsNoTracking() Where ar.Id = item.AccountReceivableId Select ar).FirstOrDefault()
                        item.InvoiceNumber = accountReceivable.InvoiceNumber
                        Dim accountReceivableShare = (From ars In _context.AccountReceivableShare.AsNoTracking() Where ars.Id = item.AccountReceivableShareId Select ars).FirstOrDefault()
                        item.NumberShare = accountReceivableShare.Number
                        item.Balance = accountReceivableShare.Balance
                        item.Value = accountReceivableShare.Value
                    Case 3
                        Dim advance = (From pa In _context.PortfolioAdvance.AsNoTracking() Where pa.Id = item.PortfolioAdvanceId Select pa).FirstOrDefault()
                        item.Balance = advance.Balance
                        item.Value = advance.Value
                        item.CodeAdvance = advance.Code
                    Case 6
                        Dim mainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where ma.Id = item.MainAccountId Select ma).FirstOrDefault()
                        item.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
                        Dim accountRecivableAccounting = (From aca In _context.AccountReceivableAccounting.AsNoTracking() Where aca.Id = item.AccountReceivableAccountingId Select aca).FirstOrDefault()
                        item.Balance = accountRecivableAccounting.Balance
                        item.Value = accountRecivableAccounting.Value
                        Dim accountReceivable = (From ar In _context.AccountReceivable.AsNoTracking() Where ar.Id = item.AccountReceivableId Select ar).FirstOrDefault()
                        item.InvoiceNumber = accountReceivable.InvoiceNumber
                End Select
            Next
            Return res
        Else
            Return New List(Of PortfolioNoteAccountReceivableAdvance)
        End If
    End Function

    ''' <summary>
    ''' Guarda y confirma una nota
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_SavePortfolioNote(xml As String, UserCode As String, CompanyType As Byte) As ObjectResult(Of SP_SavePortfolioNote_Result) Implements IPortfolioNoteRepository.SP_SavePortfolioNote
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SavePortfolioNote(xml, UserCode, CompanyType)
    End Function

    Public Function SP_CopyAndPastePortfolioNoteAccountReceivableAdvance(XmlParameter As String, XmlObject As String) As List(Of SP_CopyAndPastePortfolioNoteAccountReceivableAdvance_Result) Implements IPortfolioNoteRepository.SP_CopyAndPastePortfolioNoteAccountReceivableAdvance
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_CopyAndPastePortfolioNoteAccountReceivableAdvance(XmlParameter, XmlObject).ToList
    End Function

#End Region

End Class
