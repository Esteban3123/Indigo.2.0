'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class SettingsTreasuryRepository
    Inherits GenericRepository(Of SettingsTreasury)
    Implements ISettingsTreasuryRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un registro de parámetros por Id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetSettingsTreasuryById(id As Integer) As SettingsTreasury Implements ISettingsTreasuryRepository.GetSettingsTreasuryById
        Dim res = (From d As SettingsTreasury In Me._context.SettingsTreasury Where d.Id = id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As SettingsTreasury In Me._context.SettingsTreasury.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New SettingsTreasury()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un resgistro de parámetros por el id de la unidad operativa
    ''' </summary>
    ''' <param name="IdUnitOperative">The identifier unit operative.</param>
    ''' <returns></returns>
    Public Function GetSettingsTreasuryByIdUnitOperative(IdUnitOperative As Integer) As SettingsTreasury Implements ISettingsTreasuryRepository.GetSettingsTreasuryByIdUnitOperative
        Dim res = (From d As SettingsTreasury In Me._context.SettingsTreasury Where d.IdOperatingUnit = IdUnitOperative Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As SettingsTreasury In Me._context.SettingsTreasury.AsNoTracking() Where d.IdOperatingUnit = IdUnitOperative Select d).SingleOrDefault()

            Dim IdJournalVoucherTypeCashReceipts As Integer = res(0).JournalVoucherTypeCashReceipts
            Dim IdJournalVoucherTypeVoucherTransaction As Integer = res(0).JournalVoucherTypeVoucherTransaction
            Dim IdJournalVoucherTypeVoucherTransactionCrossing As Integer = res(0).JournalVoucherTypeVoucherTransactionCrossing
            Dim IdJournalVoucherTypeBankAppropriations As Integer = res(0).JournalVoucherTypeBankAppropriations
            Dim IdJournalVoucherTypeTreasuryNotes As Integer = res(0).JournalVoucherTypeTreasuryNotes
            Dim ConstitutionCashId As Integer? = res(0).JournalVoucherTypeConstitutionCashId

            res(0).FullNameJournaVoucherTypeCashReceipts = (From JT In _context.JournalVoucherTypes Where JT.Id = IdJournalVoucherTypeCashReceipts Select String.Concat(JT.Code, " - ", JT.Name)).FirstOrDefault()
            res(0).FullNameJournaVoucherTypeVoucherTransaction = (From JT In _context.JournalVoucherTypes Where JT.Id = IdJournalVoucherTypeVoucherTransaction Select String.Concat(JT.Code, " - ", JT.Name)).FirstOrDefault()
            res(0).FullNameJournaVoucherTypeVoucherTransactionCrossing = (From JT In _context.JournalVoucherTypes Where JT.Id = IdJournalVoucherTypeVoucherTransactionCrossing Select String.Concat(JT.Code, " - ", JT.Name)).FirstOrDefault()
            res(0).FullNameJournaVoucherTypeBankAppropriations = (From JT In _context.JournalVoucherTypes Where JT.Id = IdJournalVoucherTypeBankAppropriations Select String.Concat(JT.Code, " - ", JT.Name)).FirstOrDefault()
            res(0).FullNameJournaVoucherTypeTreasuryNotes = (From JT In _context.JournalVoucherTypes Where JT.Id = IdJournalVoucherTypeTreasuryNotes Select String.Concat(JT.Code, " - ", JT.Name)).FirstOrDefault()

            If res(0).JournalVoucherTypeConstitutionCashId IsNot Nothing Then
                res(0).FullNameJournalVoucherTypeConstitutionCash = (From JT In _context.JournalVoucherTypes.AsNoTracking Where JT.Id = ConstitutionCashId Select String.Concat(JT.Code, " - ", JT.Name)).FirstOrDefault()
            End If

            Return res(0)
        Else
            Return New SettingsTreasury()
        End If
    End Function

End Class
