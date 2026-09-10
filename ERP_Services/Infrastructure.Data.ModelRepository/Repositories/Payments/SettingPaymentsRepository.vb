'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SettingPaymentsRepository
    Inherits GenericRepository(Of SettingPayments)
    Implements ISettingPaymentsRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene los parametros de pago por id de la unidad operativa
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSettingPaymentsByIdOperatingUnit(id As Integer, Optional tracking As Boolean = False) As SettingPayments Implements ISettingPaymentsRepository.GetSettingPaymentsByIdOperatingUnit
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If

        Dim res As SettingPayments

        If tracking Then
            res = (From d In Me._context.SettingPayments.Include("AgesPayments") Where d.IdOperatingUnit = id Select d).FirstOrDefault
        Else
            res = (From d In Me._context.SettingPayments.AsNoTracking.Include("AgesPayments").AsNoTracking Where d.IdOperatingUnit = id Select d).FirstOrDefault
        End If

        If res Is Nothing Then
            Return New SettingPayments()
        End If

        Dim journalVoucher = (From vc In _context.JournalVoucherTypes.AsNoTracking Where vc.Id = res.IdJournalVoucherAccountPayable Select vc).FirstOrDefault
        res.VoucherCxpDescription = journalVoucher.Code + " - " + journalVoucher.Name

        journalVoucher = (From vt In _context.JournalVoucherTypes.AsNoTracking Where vt.Id = res.IdJournalVoucherTranslation Select vt).FirstOrDefault
        res.VoucherTransferDescription = journalVoucher.Code + " - " + journalVoucher.Name

        journalVoucher = (From vnc In _context.JournalVoucherTypes.AsNoTracking Where vnc.Id = res.IdJournalVoucherCreditNotes Select vnc).FirstOrDefault
        res.VoucherCreditNotesDescription = journalVoucher.Code + " - " + journalVoucher.Name

        journalVoucher = (From vnd In _context.JournalVoucherTypes.AsNoTracking Where vnd.Id = res.IdJournalVoucherDebitNotes Select vnd).FirstOrDefault
        res.VoucherDebitNotesDescription = journalVoucher.Code + " - " + journalVoucher.Name

        journalVoucher = (From va In _context.JournalVoucherTypes.AsNoTracking Where va.Id = res.IdJournalVocuherAmortization Select va).FirstOrDefault
        res.VoucherAmortizationDescription = journalVoucher.Code + " - " + journalVoucher.Name

        res.OriginalValue = (From d As SettingPayments In Me._context.SettingPayments.AsNoTracking Where d.IdOperatingUnit = id Select d).FirstOrDefault

        Return res
    End Function
End Class
