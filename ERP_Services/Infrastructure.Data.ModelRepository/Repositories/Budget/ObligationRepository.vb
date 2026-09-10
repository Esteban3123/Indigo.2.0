'***********************************************************************
' Assembly         : Infrastructure.Data.ModelRepository
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base.Entities
Imports System.Data.Entity.Infrastructure

#End Region

Public Class ObligationRepository
    Inherits GenericRepository(Of Obligation)
    Implements IObligationRepository



    'Contexto de Budget
    Private _context As IGlobalModelUnitOfWork

#Region "Buider"
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub
#End Region

    ''' <summary>
    ''' obtiene una obligacion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Public Function GetObligationByCode(code As String, BudgetaryValidityId As Integer) As Obligation Implements IObligationRepository.GetObligationByCode
        Dim res = (From c In _context.Obligation Where c.Code = code AndAlso c.BudgetaryValidityId = BudgetaryValidityId Select c).FirstOrDefault()
        If res IsNot Nothing Then
            Dim validaty = (From v In _context.BudgetaryValidity.AsNoTracking() Where v.Id = res.BudgetaryValidityId Select v).FirstOrDefault()
            res.BudgetEntityId = validaty.BudgetaryEntityId
            res.NitNameThirdParty = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = res.ThirdPartyId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
            res.OriginalValue = (From c In _context.Obligation.AsNoTracking() Where c.Code = code AndAlso c.BudgetaryValidityId = BudgetaryValidityId Select c).FirstOrDefault()
            Return res
        Else
            Return New Obligation
        End If
    End Function

    ''' <summary>
    ''' obtiene una obligacion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetObligationById(id As Integer, Optional flagTracking As Boolean = True) As Obligation Implements IObligationRepository.GetObligationById
        If flagTracking Then
            Dim res = (From c In _context.Obligation.Include("ObligationDetail") Where c.Id = id Select c).FirstOrDefault()
            If res IsNot Nothing Then
                Dim validaty = (From v In _context.BudgetaryValidity.AsNoTracking() Where v.Id = res.BudgetaryValidityId Select v).FirstOrDefault()
                res.BudgetEntityId = validaty.BudgetaryEntityId
                res.NitNameThirdParty = (From t In _context.ThirdParty.AsNoTracking() Where t.Id = res.ThirdPartyId Select String.Concat(t.Nit, " - ", t.Name)).FirstOrDefault()
                res.OriginalValue = (From c In _context.Obligation.AsNoTracking() Where c.Id = id Select c).FirstOrDefault()
                Return res
            End If
        Else
            Return (From o In _context.Obligation.AsNoTracking() Where o.Id = id Select o).FirstOrDefault()
        End If
        Return New Obligation
    End Function

    Public Function SP_SaveObligation(ObligationXml As String, ObligationDetailForDeleteXml As String, CodeUser As String) As SP_SaveObligation_Result Implements IObligationRepository.SP_SaveObligation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveObligation(ObligationXml, ObligationDetailForDeleteXml, CodeUser).SingleOrDefault()
    End Function

End Class
