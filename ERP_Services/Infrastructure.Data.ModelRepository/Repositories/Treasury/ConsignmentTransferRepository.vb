'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 07-10-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Data.Entity.Infrastructure

Public Class ConsignmentTransferRepository
    Inherits GenericRepository(Of Consignment)
    Implements IConsignmentTransferRepository

    'Contexto global
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por codigo
    ''' </summary>
    Public Function GetConsignmentTransfer(code As String, Optional tracking As Boolean = False) As Consignment Implements IConsignmentTransferRepository.GetConsignmentTransfer
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query As Consignment = Nothing
        If tracking Then
            query = (From ct In _context.Consignment.Include("ConsignmentDetail")
                     Where ct.Code.Equals(code) Select ct).FirstOrDefault()
        Else
            query = (From ct In _context.Consignment.AsNoTracking() Where ct.Code.Equals(code) Select ct).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ct In _context.Consignment.AsNoTracking() Where ct.Code.Equals(code) Select ct).FirstOrDefault()
            If tracking Then
                Dim entityBank As EntityBankAccounts = (From ea In _context.EntityBankAccounts.Include("Currency").AsNoTracking() Where ea.Id = query.EntityBankAccountId Select ea).FirstOrDefault()
                Dim bank As Bank = (From b In _context.Bank Where b.Id = entityBank.IdBank Select b).FirstOrDefault()
                Dim fType As String
                If entityBank.Type = 1 Then
                    fType = ResourceManager.GetString("AccountTypeCurrent")
                Else
                    fType = ResourceManager.GetString("AccountTypeSaving")
                End If
                query.FullNameEntityBankAccount = String.Concat(entityBank.Code, " - ", bank.Name, " CTA. ", fType, " ", entityBank.Number)
                query.FullNameCostCenter = IIf(query.CostCenterId IsNot Nothing, (From cc In _context.CostCenter Where cc.Id = query.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault(), String.Empty)
                If entityBank?.Currency Is Nothing Then
                    Dim _companySettings = (From x In _context.CompanySettings.Include("Currency").AsNoTracking() Select x)?.FirstOrDefault
                    query.CurrencyId = _companySettings?.Currency?.Id
                    query.CurrencyAbbreviation = _companySettings?.Currency?.Abbreviation
                Else
                    query.CurrencyId = entityBank.Currency?.Id
                    query.CurrencyAbbreviation = entityBank.Currency?.Abbreviation
                End If
                If query?.ConsignmentDetail?.Any() Then
                    Dim _listCashRegisterIds = query.ConsignmentDetail.Select(Function(s) s.CashRegisterId).ToList()
                    Dim _listMainAccountIds = query.ConsignmentDetail.Select(Function(s) s.MainAccountId).ToList()
                    Dim _listCashRegister = (From cr In _context.CashRegisters.Include("Currency").AsNoTracking() Where _listCashRegisterIds.Contains(cr.Id) Select cr).ToList()
                    Dim _listMainAccount = (From ma In _context.MainAccounts.AsNoTracking() Where _listMainAccountIds.Contains(ma.Id) Select ma).ToList()

                    For Each detail As ConsignmentDetail In query.ConsignmentDetail
                        Dim _cashRegister = _listCashRegister.Find(Function(s) s.Id = detail.CashRegisterId)
                        detail.FullNameCashRegister = String.Concat(_cashRegister.Code, " - ", _cashRegister.Name)
                        detail.CurrentBalance = _cashRegister.CurrentBalance
                        detail.FullNameCostCenter = IIf(detail.CostCenterId IsNot Nothing, (From cc In _context.CostCenter Where cc.Id = detail.CostCenterId Select String.Concat(cc.Code, " - ", cc.Name)).FirstOrDefault(), String.Empty)
                        detail.FullNameMainAccount = (From ma In _listMainAccount Where ma.Id = detail.MainAccountId Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()

                        If _cashRegister?.Currency Is Nothing Then
                            Dim CompanyCurrency = (From x In _context.CompanySettings.Include("Currency").AsNoTracking() Select x)?.FirstOrDefault
                            detail.CashCurrencyId = CompanyCurrency?.Currency?.Id
                            detail.CashCurrencyAbbreviation = CompanyCurrency?.Currency?.Abbreviation
                        Else
                            detail.CashCurrencyId = _cashRegister?.Currency?.Id
                            detail.CashCurrencyAbbreviation = _cashRegister?.Currency?.Abbreviation
                        End If
                    Next

                End If

            End If
            Return query
        Else
            Return New Consignment()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de consignacion o traslado por id
    ''' </summary>
    Public Function GetConsignmentTransferById(Id As Integer, Optional tracking As Boolean = False) As Consignment Implements IConsignmentTransferRepository.GetConsignmentTransferById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As Consignment = Nothing
        If tracking Then
            query = (From ct In _context.Consignment.Include("ConsignmentDetail") Where ct.Id = Id Select ct).FirstOrDefault()
        Else
            query = (From ct In _context.Consignment.AsNoTracking() Where ct.Id = Id Select ct).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ct In _context.Consignment.AsNoTracking() Where ct.Id = Id Select ct).FirstOrDefault()
            Return query
        Else
            Return New Consignment()
        End If
    End Function

    Public Function SP_ReverseConsignment(TreasuryNoteId As Integer, UserCode As String) As List(Of SP_ReverseConsignment_Result) Implements IConsignmentTransferRepository.SP_ReverseConsignment
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseConsignment(TreasuryNoteId, UserCode).ToList()
    End Function

#End Region

    Public Function ListConsignmentMassiveConfirm(listDocuments As List(Of String)) As List(Of Consignment) Implements IConsignmentTransferRepository.ListConsignmentMassiveConfirm
        Return (From tn In _context.Consignment.Include("ConsignmentDetail") Where listDocuments.Contains(tn.Code) Select tn).ToList()
    End Function

End Class