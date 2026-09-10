'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class DistributionLinesRepository
    Inherits GenericRepository(Of DistributionLines)
    Implements IDistributionLinesRepository

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
    ''' Obtiene una linea de distribucion por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLines(code As String, Optional tracking As Boolean = True) As DistributionLines Implements IDistributionLinesRepository.GetDistributionLines
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As DistributionLines In Me._context.DistributionLines.AsNoTracking.Include("DistributionLinesDetail").AsNoTracking.Include("DistributionLinesICARetention").AsNoTracking.Include("AccountPayableConceptNotes.MainAccounts").AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            Dim account = (From ma In _context.MainAccounts.AsNoTracking Where ma.Id = res.IdMainAccount Select ma).FirstOrDefault
            res.NumberNameMainAccount = account.Number + " - " + account.Name
            If res.AccountPayableConceptNotes IsNot Nothing Then
                res.AccountPayableConceptNoteName = $"{res.AccountPayableConceptNotes.Code} - {res.AccountPayableConceptNotes.Name}"

                If res.AccountPayableConceptNotes.MainAccounts IsNot Nothing Then
                    res.IdAccountName = $"{res.AccountPayableConceptNotes.MainAccounts.Number} - {res.AccountPayableConceptNotes.MainAccounts.Name}"
                End If

            End If

            Dim expenseConcept = (From e In _context.ExpenseConcepts.AsNoTracking Where e.Id = res.ExpensesConceptId).FirstOrDefault
            res.ExpensiveConceptDescription = expenseConcept.Code + " - " + expenseConcept.Description

            If res.DistributionLinesDetail IsNot Nothing AndAlso res.DistributionLinesDetail.Count > 0 Then
                For Each itemDetail As DistributionLinesDetail In res.DistributionLinesDetail
                    Dim accountPayableConcept = (From r In _context.AccountPayableConcepts.AsNoTracking Where r.Id = itemDetail.AccountPayableConceptId Select r).FirstOrDefault
                    itemDetail.DescriptionAccountPayableConcept = accountPayableConcept.Code + " - " + accountPayableConcept.Name
                Next
            End If

            If res.DistributionLinesICARetention IsNot Nothing AndAlso res.DistributionLinesICARetention.Count > 0 Then
                For Each itemDetail As DistributionLinesICARetention In res.DistributionLinesICARetention
                    Dim accountPayableConcept = (From r In _context.AccountPayableConcepts.AsNoTracking Where r.Id = itemDetail.AccountPayableConceptId Select r).FirstOrDefault
                    itemDetail.DescriptionConcept = accountPayableConcept.Code + " - " + accountPayableConcept.Name

                    Dim operatingUnit = (From o In _context.OperatingUnit.AsNoTracking Where o.Id = itemDetail.OperatingUnitId Select o).FirstOrDefault
                    itemDetail.DescriptionOperatingUnit = operatingUnit.UnitCode + " - " + operatingUnit.UnitName
                Next
            End If

            Return res
        Else
            Return New DistributionLines()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una linea de distribucion por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDistributionLinesById(id As Integer, Optional tracking As Boolean = True) As DistributionLines Implements IDistributionLinesRepository.GetDistributionLinesById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.DistributionLines.Include("DistributionLinesDetail") Where d.Id = id Select d).ToList
        If res.Count > 0 Then
            res(0).OriginalValue = (From d In Me._context.DistributionLines.AsNoTracking.Include("DistributionLinesDetail").AsNoTracking Where d.Id = id Select d).SingleOrDefault()
            Return res.SingleOrDefault
        Else
            Return New DistributionLines()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una unidad de mercadeo por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetFilingUnitByCode(code As String) As FilingUnit Implements IDistributionLinesRepository.GetFilingUnitByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As FilingUnit In Me._context.FilingUnit.AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New FilingUnit()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un tipo de proveedor por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSupplierTypeByCode(code As String) As SupplierType Implements IDistributionLinesRepository.GetSupplierTypeByCode
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As SupplierType In Me._context.SupplierType.AsNoTracking.Include("SupplierType1").AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New SupplierType()
        End If
    End Function

End Class
