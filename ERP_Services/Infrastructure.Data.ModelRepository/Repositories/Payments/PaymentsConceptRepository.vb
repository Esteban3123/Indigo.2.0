'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class PaymentsConceptRepository
    Inherits GenericRepository(Of AccountPayableConcepts)
    Implements IPaymentsConceptRepository

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
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConcept(code As String, Optional tracking As Boolean = True) As AccountPayableConcepts Implements IPaymentsConceptRepository.GetPaymentConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As AccountPayableConcepts In Me._context.AccountPayableConcepts Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then

            If res.IdAccount IsNot Nothing Then
                Dim account = (From a In _context.MainAccounts.AsNoTracking Where res.IdAccount = a.Id Select a).FirstOrDefault
                res.NumberNameAccount = account.Number + " - " + account.Name
            End If

            If res.ThreeEightThreeAccountId IsNot Nothing Then
                Dim account383 = (From a In _context.MainAccounts.AsNoTracking Where res.ThreeEightThreeAccountId = a.Id Select a).FirstOrDefault
                res.NumberNameAccountThreeEightThree = account383.Number + " - " + account383.Name
            End If

            If res.ThreeEightFourAccountId IsNot Nothing Then
                Dim account384 = (From a In _context.MainAccounts.AsNoTracking Where res.ThreeEightFourAccountId = a.Id Select a).FirstOrDefault
                res.NumberNameAccounthreeEightFour = account384.Number + " - " + account384.Name
            End If

            If res.RetentionConceptId IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where res.RetentionConceptId = rc.Id Select rc).FirstOrDefault
                res.RetentionConceptDescription = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            If res.ThreeEightThreeRetentionConceptId IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where res.ThreeEightThreeRetentionConceptId = rc.Id Select rc).FirstOrDefault
                res.RetentionConceptThreeDescription = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            If res.ThreeEightFourRetentionConceptId IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where res.ThreeEightFourRetentionConceptId = rc.Id Select rc).FirstOrDefault
                res.RetentionConceptFourDescription = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            res.OriginalValue = (From d As AccountPayableConcepts In Me._context.AccountPayableConcepts.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Return res
        Else
            Return New AccountPayableConcepts()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los conceptos de pagos
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllPaymentsConcepts() As List(Of AccountPayableConcepts) Implements IPaymentsConceptRepository.ListAllPaymentsConcepts
        Dim paymentsConcept = From e In _context.AccountPayableConcepts
                              Select e
        Return paymentsConcept.ToList()
    End Function

    ''' <summary>
    ''' Concepto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConceptById(id As String, Optional tracking As Boolean = True) As AccountPayableConcepts Implements IPaymentsConceptRepository.GetPaymentConceptById
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim res = (From d In Me._context.AccountPayableConcepts.Include("MainAccounts").Include("MainAccounts1").Include("MainAccounts2").Include("MainAccounts.MainAccountClasses").Include("RetentionConcepts") Where d.Id = id Select d)
        If res.Count > 0 Then
            res.SingleOrDefault.OriginalValue = (From d As AccountPayableConcepts In Me._context.AccountPayableConcepts.Include("MainAccounts").AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Dim objtmp = res.SingleOrDefault

            'espcecifica
            If objtmp.MainAccounts IsNot Nothing Then
                objtmp.NumberNameAccount = objtmp.MainAccounts.Number + " - " + objtmp.MainAccounts.Name
            End If
            '383
            If objtmp.MainAccounts1 IsNot Nothing Then
                objtmp.NumberNameAccountThreeEightThree = objtmp.MainAccounts1.Number + " - " + objtmp.MainAccounts1.Name
            End If
            'e384
            If objtmp.MainAccounts2 IsNot Nothing Then
                objtmp.NumberNameAccounthreeEightFour = objtmp.MainAccounts2.Number + " - " + objtmp.MainAccounts2.Name
            End If
            'CONCEPTOS
            If objtmp.ThreeEightThreeRetentionConceptId IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where objtmp.ThreeEightThreeRetentionConceptId = rc.Id Select rc).FirstOrDefault
                objtmp.RetentionConceptThreeDescription = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            If objtmp.ThreeEightFourRetentionConceptId IsNot Nothing Then
                Dim retentionConcept = (From rc In _context.RetentionConcepts.AsNoTracking Where objtmp.ThreeEightFourRetentionConceptId = rc.Id Select rc).FirstOrDefault
                objtmp.RetentionConceptFourDescription = retentionConcept.Code + " - " + retentionConcept.Name
            End If

            Return res.SingleOrDefault
        Else
            Return New AccountPayableConcepts()
        End If
    End Function

    Public Function GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay(idSupplierDistributionLine As Integer, OperatingUnitId As Integer) As AccountPayableConcepts Implements IPaymentsConceptRepository.GetAccountPayableICARetentionByDistributionLineIdAndOperatingUnitIdPay
        If idSupplierDistributionLine = 0 OrElse OperatingUnitId = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Dim idDistributtionLines = (From sdl In Me._context.SuppliersDistributionLines Where sdl.Id = idSupplierDistributionLine Select sdl.IdDistributionLine).FirstOrDefault
        If idDistributtionLines <> Nothing Then

            Dim result = (From d In Me._context.DistributionLinesICARetention.Include("AccountPayableConcepts").Include("AccountPayableConcepts.MainAccounts").Include("AccountPayableConcepts.RetentionConcepts")
                Where d.DistributionLineId = idDistributtionLines And d.OperatingUnitId = OperatingUnitId Select d).FirstOrDefault()

            If result IsNot Nothing Then
                Return result.AccountPayableConcepts
            Else
                Return New AccountPayableConcepts
            End If
            'Return (From d In Me._context.DistributionLinesICARetention.Include("AccountPayableConcepts")
            '    Join apc In _context.AccountPayableConcepts.Include("MainAccounts").Include("MainAccounts1").Include("MainAccounts2").Include("RetentionConcepts").Include("RetentionConcepts1").Include("RetentionConcepts2") On apc.Id Equals d.AccountPayableConceptId
            '    Where d.DistributionLineId = idDistributtionLines And d.OperatingUnitId = OperatingUnitId Select apc).FirstOrDefault()
        Else
            Return New AccountPayableConcepts
        End If
    End Function

    ''' <summary>
    ''' Obtiene un concepto de pago por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPaymentConceptByIdSimple(id As Integer) As AccountPayableConcepts Implements IPaymentsConceptRepository.GetPaymentConceptByIdSimple
        If id = 0 Then
            Throw New ArgumentNullException("id")
        End If
        Return (From apc In _context.AccountPayableConcepts.AsNoTracking.Include("RetentionConcepts").AsNoTracking.Include("MainAccounts").AsNoTracking()
                                Where apc.Id = id
                               Select apc).FirstOrDefault()
    End Function

End Class
