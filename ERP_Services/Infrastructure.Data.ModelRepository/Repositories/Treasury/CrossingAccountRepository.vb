'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 25-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class CrossingAccountRepository
    Inherits GenericRepository(Of CrossingAccount)
    Implements ICrossingAccountRepository

    'Contexto global
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por codigo
    ''' </summary>
    Public Function GetCrossingAccount(code As String, Optional tracking As Boolean = False) As CrossingAccount Implements ICrossingAccountRepository.GetCrossingAccount
        If String.IsNullOrEmpty(code) Then
            Throw New ArgumentNullException("code")
        End If
        Dim query As CrossingAccount = Nothing
        If tracking Then
            query = (From ca In _context.CrossingAccount.Include("CrossingAccountDetailOtherConcept").Include("Currency").AsNoTracking()
                     Where ca.Code.Equals(code) Select ca).FirstOrDefault()
            '.Include("CrossingAccountDetailCxP").Include("CrossingAccountDetailCxC")
        Else
            query = (From ca In _context.CrossingAccount.AsNoTracking().Include("CrossingAccountDetailOtherConcept").AsNoTracking().Include("Currency").AsNoTracking()
                     Where ca.Code.Equals(code) Select ca).FirstOrDefault()
            '.Include("CrossingAccountDetailCxP").AsNoTracking().Include("CrossingAccountDetailCxC").AsNoTracking()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ca In _context.CrossingAccount.AsNoTracking() Where ca.Code.Equals(code) Select ca).FirstOrDefault()
            Dim _cashFlowConcept As CashFlowConcept = Nothing

            Dim listCrossingConcept As List(Of CrossingAccountDetailOtherConcept) = query.CrossingAccountDetailOtherConcept.ToList()
            For Each crossingConcept As CrossingAccountDetailOtherConcept In listCrossingConcept
                Dim _mainAccountId As Integer = crossingConcept.MainAccountId
                crossingConcept.MainAccountCodeName = (From ma In _context.MainAccounts
                                                      Where ma.Id = _mainAccountId
                                                      Select String.Concat(ma.Number, " - ", ma.Name)).FirstOrDefault()
                crossingConcept.ConceptCodeName = (From c In _context.NoteConcepts.AsNoTracking() Where c.Id = crossingConcept.TreasuryNoteConceptId Select String.Concat(c.Code, " - ", c.Description)).FirstOrDefault()
                If crossingConcept.ThirdPartyId IsNot Nothing Then
                    crossingConcept.ThirdPartyNitName = (From c In _context.ThirdParty.AsNoTracking() Where c.Id = crossingConcept.ThirdPartyId Select String.Concat(c.Nit, " - ", c.Name)).FirstOrDefault()
                End If
                If crossingConcept.CostCenterId IsNot Nothing Then
                    crossingConcept.CostCenterCodeName = (From c In _context.CostCenter.AsNoTracking() Where c.Id = crossingConcept.CostCenterId Select String.Concat(c.Code, " - ", c.Name)).FirstOrDefault()
                End If
                If crossingConcept.Nature = 1 Then
                    crossingConcept.NatureName = "Debito"
                Else
                    crossingConcept.NatureName = "Credito"
                End If
                _cashFlowConcept = (From cfc In _context.CashFlowConcept.AsNoTracking Where cfc.Id = crossingConcept.IdCashFlowConcept).FirstOrDefault
                If _cashFlowConcept IsNot Nothing Then
                    crossingConcept.CodeNameCashFlowConcept = String.Format("{0} - {1}", _cashFlowConcept.Code, _cashFlowConcept.NameConcept)
                End If
            Next

            Return query
        Else
            Return New CrossingAccount()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un registro de cruce de cuentas por ir
    ''' </summary>
    Public Function GetCrossingAccountById(Id As Integer, Optional tracking As Boolean = False) As CrossingAccount Implements ICrossingAccountRepository.GetCrossingAccountById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim query As CrossingAccount = Nothing
        If tracking Then
            query = (From ca In _context.CrossingAccount Where ca.Id = Id Select ca).FirstOrDefault()
        Else
            query = (From ca In _context.CrossingAccount.AsNoTracking() Where ca.Id = Id Select ca).FirstOrDefault()
        End If
        If query IsNot Nothing AndAlso query.Id > 0 Then
            query.OriginalValue = (From ca In _context.CrossingAccount.AsNoTracking() Where ca.Id = Id Select ca).FirstOrDefault()
            Return query
        Else
            Return New CrossingAccount()
        End If
    End Function

    Public Function GetCrossingAccountByAccountPayableListId(listAccountPayableId As List(Of Integer)) As List(Of String) Implements ICrossingAccountRepository.GetCrossingAccountByAccountPayableListId
        Dim Uno As Byte = 1
        Return (From ca In _context.CrossingAccount.AsNoTracking()
                     Join cap In _context.CrossingAccountDetailCxP.AsNoTracking() On cap.CrossingAccountId Equals ca.Id
                     Join ap In _context.AccountPayable.AsNoTracking() On cap.AccountPayableId Equals ap.Id
                     Where listAccountPayableId.Contains(cap.AccountPayableId) AndAlso ca.Status = Uno
                     Select String.Concat(ap.BillNumber, ";", ca.Code)).ToList()
    End Function

    Public Function SP_SaveMasiveCxCCrossingAccount(XmlObject As String, CrossingType As Integer, ThirdPartyId As Integer) As List(Of SP_SaveMasiveCxCCrossingAccount_Result) Implements ICrossingAccountRepository.SP_SaveMasiveCxCCrossingAccount
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveMasiveCxCCrossingAccount(XmlObject, CrossingType, ThirdPartyId).ToList
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="TreasuryNoteId"></param>
    ''' <param name="UserCode"></param>
    ''' <returns></returns>
    Public Function SP_ReverseCrossingAccount(TreasuryNoteId As Integer, UserCode As String) As List(Of SP_ReverseCrossingAccount_Result) Implements ICrossingAccountRepository.SP_ReverseCrossingAccount
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ReverseCrossingAccount(TreasuryNoteId, UserCode).ToList()
    End Function

#End Region

End Class