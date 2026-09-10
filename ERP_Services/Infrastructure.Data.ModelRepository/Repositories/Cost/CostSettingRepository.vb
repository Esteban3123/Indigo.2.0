'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources

Public Class CostSettingRepository
    Inherits GenericRepository(Of CostSetting)
    Implements ICostSettingRepository

    ''' <summary>
    ''' The _context
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetCostSetting() As CostSetting Implements ICostSettingRepository.GetCostSetting
        Dim query = (From s In _context.CostSetting Select s).FirstOrDefault()
        If query IsNot Nothing Then
            If query.JournalVoucherTypeId IsNot Nothing Then
                query.FullNameJournalVoucherType = (From o In _context.JournalVoucherTypes.AsNoTracking() Where o.Id = query.JournalVoucherTypeId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()
            End If
            If query.ProvisionJournalVoucherTypeId IsNot Nothing Then
                query.ProvisionJournalVoucherTypeDescription = (From o In _context.JournalVoucherTypes.AsNoTracking() Where o.Id = query.ProvisionJournalVoucherTypeId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()
            End If
            If query.ProvisionReversalJournalVoucherTypeId IsNot Nothing Then
                query.ProvisionReversalJournalVoucherTypeDescription = (From o In _context.JournalVoucherTypes.AsNoTracking() Where o.Id = query.ProvisionReversalJournalVoucherTypeId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()
            End If
            query.AccountPayableConceptDescription = (From o In _context.AccountPayableConcepts.AsNoTracking() Where o.Id = query.AccountPayableConceptsId Select String.Concat(o.Code, " - ", o.Name)).FirstOrDefault()
            query.OriginalValue = (From s In _context.CostSetting.AsNoTracking() Select s).FirstOrDefault()
            Return query
        Else
            Return New CostSetting()
        End If
    End Function

    Public Function GetCostSettingById(id As Integer) As CostSetting Implements ICostSettingRepository.GetCostSettingById
        If id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query = (From s In _context.CostSetting Where s.Id = id Select s).FirstOrDefault()
        If query IsNot Nothing Then
            query.OriginalValue = (From s In _context.CostSetting.AsNoTracking() Where s.Id = id Select s).FirstOrDefault()
            Return query
        Else
            Return New CostSetting()
        End If
    End Function

End Class
