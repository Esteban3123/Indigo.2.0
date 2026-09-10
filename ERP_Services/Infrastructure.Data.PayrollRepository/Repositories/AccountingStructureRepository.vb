'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 22-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class AccountingStructureRepository

    Inherits GenericRepository(Of AccountingStructure)
    Implements IAccountingStructureRepository

    'Contexto de payroll
    Private _context As IPayrollUnitOfWork

    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Código
    ''' </summary>
    ''' <param name="code">Código Accounting Structure</param>
    ''' <param name="desatach"></param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructure(code As String, Optional desatach As Boolean = True) As AccountingStructure Implements IAccountingStructureRepository.GetAccountingStructure
        Dim accountingStructure = From e In _context.AccountingStructure
                       Where e.Code = code
                       Select e
        If accountingStructure.Count > 0 Then
            Dim objAccountingStructure = Nothing
            If desatach = False Then
                objAccountingStructure = (From e In _context.AccountingStructure.AsNoTracking
                               Where e.Code = code
                               Select e).SingleOrDefault
            Else
                objAccountingStructure = accountingStructure.SingleOrDefault
            End If
            Return objAccountingStructure
        Else
            Return New AccountingStructure()
        End If
    End Function

    ''' <summary>
    ''' Obtiene la Estructura Contable de Nómina x Id
    ''' </summary>
    ''' <param name="accountingStructureId">Id Accounting Structure</param>
    ''' <returns>AccountingStructure</returns>
    ''' <remarks></remarks>
    Public Function GetAccountingStructureById(accountingStructureId As Integer, Optional desatach As Boolean = True) As AccountingStructure Implements IAccountingStructureRepository.GetAccountingStructureById
        Dim accountingStructure = From e In _context.AccountingStructure
                      Where e.Id = accountingStructureId
                      Select e
        If accountingStructure.Count > 0 Then
            Dim objAccountingStructure = Nothing
            If desatach = False Then
                objAccountingStructure = (From e In _context.AccountingStructure.AsNoTracking
                               Where e.Id = accountingStructureId
                               Select e).SingleOrDefault
            Else
                objAccountingStructure = accountingStructure.SingleOrDefault
            End If
            Return objAccountingStructure
        Else
            Return New AccountingStructure()
        End If
    End Function

    ''' <summary>
    ''' Lista Toda las Estructuras Contables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllAccountingStructure() As List(Of AccountingStructure) Implements IAccountingStructureRepository.ListAllAccountingStructure
        Dim AccountingStructure = From e In _context.AccountingStructure
                       Select e
        Return AccountingStructure.ToList()
    End Function

  
    
End Class
