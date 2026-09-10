'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CashReceiptConceptRepository
    Inherits GenericRepository(Of CashReceiptConcepts)
    Implements ICashReceiptConceptRepository


    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un concepto de recibo de caja
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">code</exception>
    Public Function GetCashReceiptConcept(code As String) As CashReceiptConcepts Implements ICashReceiptConceptRepository.GetCashReceiptConcept
        If code Is Nothing OrElse code.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("code")
        End If
        Dim res = (From d As CashReceiptConcepts In Me._context.CashReceiptConcepts.Include("CashReceiptConceptUser").Include("CashFlowConcept").AsNoTracking Where d.Code.Equals(code.Trim()) Select d).FirstOrDefault
        If res IsNot Nothing Then
            res.OriginalValue = (From d As CashReceiptConcepts In Me._context.CashReceiptConcepts.AsNoTracking() Where d.Code.Equals(code.Trim()) Select d).SingleOrDefault()
            Dim mainAccount = (From ma In _context.MainAccounts Where ma.Id = res.IdMainAccount Select ma).FirstOrDefault()
            res.CodeNameMainAccount = mainAccount.Number + " - " + mainAccount.Name
            Return res
        Else
            Return New CashReceiptConcepts()
        End If
    End Function

    ''' <summary>
    ''' metodo para obtener un concepto de recibo de  caja por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetCashReceiptConceptById(id As Integer) As CashReceiptConcepts Implements ICashReceiptConceptRepository.GetCashReceiptConceptById
        Dim res = (From d As CashReceiptConcepts In Me._context.CashReceiptConcepts.AsNoTracking.Include("MainAccounts").AsNoTracking.Include("CashFlowConcept").AsNoTracking Where d.Id = id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As CashReceiptConcepts In Me._context.CashReceiptConcepts.AsNoTracking() Where d.Id = id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New CashReceiptConcepts()
        End If
    End Function

    ''' <summary>
    ''' Obtiene los conceptos de recibo de caja que estan relacionados a un concepto de flujo de caja
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Public Function GetCashReceiptConceptByFlowConcept(id As Integer) As List(Of CashReceiptConcepts) Implements ICashReceiptConceptRepository.GetCashReceiptConceptByFlowConcept
        Return (From d As CashReceiptConcepts In Me._context.CashReceiptConcepts.AsNoTracking Where d.IdCashFlowConcept = id Select d).ToList()
    End Function
End Class
