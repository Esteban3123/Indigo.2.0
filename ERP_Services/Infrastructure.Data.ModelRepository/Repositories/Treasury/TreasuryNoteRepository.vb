'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 10-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class TreasuryNoteRepository
    Inherits GenericRepository(Of TreasuryNote)
    Implements ITreasuryNoteRepository


    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' obtiene una nota de tesoreria por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetTreasuryNote(code As String) As TreasuryNote Implements ITreasuryNoteRepository.GetTreasuryNote
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As TreasuryNote In Me._context.TreasuryNote.Include("TreasuryNoteDetail").Include("Currency").Include("TreasuryNoteCashReceiptsDetail").Include("TreasuryNoteCashReceiptsDetail.CashReceipts") Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As TreasuryNote In Me._context.TreasuryNote.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault()
            res.FullNameVoucherTransaction = If(res.VoucherTransactionId IsNot Nothing, (From vt In _context.VoucherTransaction Where vt.Id = res.VoucherTransactionId Select vt).FirstOrDefault().Code, String.Empty)
            res.FullNameCashReceipt = If(res.CashReceiptId IsNot Nothing, (From vt In _context.CashReceipts Where vt.Id = res.CashReceiptId Select vt).FirstOrDefault().Code, String.Empty)
            res.ConsignmentCode = If(res.ConsignmentId IsNot Nothing, (From c In _context.Consignment Where c.Id = res.ConsignmentId Select New With { .Code = c.Code }).FirstOrDefault().Code, String.Empty)
            res.CrossAccountCode = If(res.CrossingAccountId IsNot Nothing, (From c In _context.CrossingAccount Where c.Id = res.CrossingAccountId Select New With { .Code = c.Code }).FirstOrDefault().Code, String.Empty)

            res.VoucherTransactionValue = If(res.VoucherTransactionId IsNot Nothing, (From vt In _context.VoucherTransaction Where vt.Id = res.VoucherTransactionId Select vt).FirstOrDefault().Value, 0)
            res.FullNameCashRegister = If(res.CashRegisterId IsNot Nothing, (From c In _context.CashRegisters Where c.Id = res.CashRegisterId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault(), String.Empty)
            Dim varEntityBankAccount = (From EA In _context.EntityBankAccounts
                                        Join B In _context.Bank On EA.IdBank Equals B.Id
                                        Where EA.Id = res.EntityBankAccountId
                                        Select New With {Key .Code = EA.Code, Key .Name = B.Name}).FirstOrDefault()
            res.FullNameEntityAccount = If(varEntityBankAccount IsNot Nothing, String.Concat(varEntityBankAccount.Code, " - ", varEntityBankAccount.Name), String.Empty)
            res.FullNameCostCenter = If(res.CostCenterId IsNot Nothing, (From cc In _context.CostCenter Where cc.Id = res.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault(), String.Empty)
            Dim _cashFlowConcept As CashFlowConcept = Nothing
            For Each tnd As TreasuryNoteDetail In res.TreasuryNoteDetail
                tnd.FullNameCostCenter = If(tnd.CostCenterId IsNot Nothing, (From cc In _context.CostCenter Where cc.Id = tnd.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault(), String.Empty)
                tnd.FullNameMainAccount = (From cc In _context.MainAccounts Where cc.Id = tnd.CostCenterId Select String.Concat(cc.Number, " - ", cc.Name)).FirstOrDefault()
                tnd.FullNameNature = If(tnd.Nature = 1, ResourceManager.GetString("AccountNatureDebit"), ResourceManager.GetString("AccountNatureCredit"))
                tnd.FullNameThird = If(tnd.ThirdPartyId IsNot Nothing, (From cc In _context.ThirdParty Where cc.Id = tnd.ThirdPartyId Select String.Concat(cc.Nit, " - ", cc.Name)).FirstOrDefault(), String.Empty)
                Dim concept As NoteConcepts = (From nc In _context.NoteConcepts Where nc.Id = tnd.NoteConceptId Select nc).FirstOrDefault()
                tnd.NoteConceptCode = concept.Code
                tnd.NoteConceptName = concept.Description
                _cashFlowConcept = (From cfc In _context.CashFlowConcept.AsNoTracking Where cfc.Id = tnd.IdCashFlowConcept).FirstOrDefault
                If _cashFlowConcept IsNot Nothing Then
                    tnd.CodeNameCashFlowConcept = String.Format("{0} - {1}", _cashFlowConcept.Code, _cashFlowConcept.NameConcept)
                End If
            Next
            Return res
        Else
            Return New TreasuryNote()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una nota de tesoreria por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">id</exception>
    Public Function GetTreasuryNoteById(Id As Integer) As TreasuryNote Implements ITreasuryNoteRepository.GetTreasuryNoteById
        If Id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d As TreasuryNote In Me._context.TreasuryNote.Include("TreasuryNoteDetail") Where d.Id = Id Select d).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id > 0 Then
            res.OriginalValue = (From d As TreasuryNote In Me._context.TreasuryNote.AsNoTracking() Where d.Id = Id Select d).FirstOrDefault()
            Return res
        Else
            Return New TreasuryNote()
        End If
    End Function

    

    Public Function ListTreasuryNoteMassiveConfirm(listDocuments As List(Of String)) As List(Of TreasuryNote) Implements ITreasuryNoteRepository.ListTreasuryNoteMassiveConfirm
        Return (From tn In _context.TreasuryNote.Include("TreasuryNoteDetail") Where listDocuments.Contains(tn.Code) Select tn).ToList()
    End Function
End Class