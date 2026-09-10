'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity
Imports Domain.Base.Entities

Public Class BankRepository
    Inherits GenericRepository(Of Bank)
    Implements IBankRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un banco especifivo
    ''' </summary>
    ''' <param name="code">Codigo del banco</param>
    ''' <returns>Banco</returns>
    ''' <remarks></remarks>
    Public Function GetBank(code As String, Optional tracking As Boolean = True) As Bank Implements IBankRepository.GetBank

        Dim ObjBank As Bank = Nothing
        If tracking = False Then
            ObjBank = (From e In _context.Bank.Include("BankDetail").Include("BankAutomaticRecognitionRules").AsNoTracking()
                       Where e.Code = code
                       Select e).FirstOrDefault()
        Else

            ObjBank = (From e In _context.Bank.Include("BankDetail").Include("BankAutomaticRecognitionRules")
                       Where e.Code = code
                       Select e).FirstOrDefault()

        End If

        If ObjBank Is Nothing Then
            Return New Bank()
        Else
            ObjBank.StartTracking()
            If ObjBank.BankAutomaticRecognitionRules.Any() Then
                For Each item In ObjBank.BankAutomaticRecognitionRules
                    item.NoteConceptCodeName = (From e In _context.NoteConcepts Where e.Id = item.NoteConceptsId Select String.Concat(e.Code, " - ", e.Description)).FirstOrDefault()
                    item.AccountingAccountNumberName = (From e In _context.MainAccounts Where e.Id = item.MainAccountsId Select String.Concat(e.Number, " - ", e.Name)).FirstOrDefault()
                    item.CostCenterCodeName = (From e In _context.CostCenter Where e.Id = item.CostCenterId Select String.Concat(e.Code, " - ", e.Name)).FirstOrDefault
                Next
            End If
            Return ObjBank
        End If
    End Function

    ''' <summary>
    ''' Lista todos los bancos
    ''' </summary>
    ''' <returns>Lista de bancos</returns>
    ''' <remarks></remarks>
    Public Function ListAllBank() As List(Of Bank) Implements IBankRepository.ListAllBank
        Dim bank = From e In _context.Bank
                   Select e
        Return bank.ToList()
    End Function

    ''' <summary>
    ''' Obtiene un banco por el identificador
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetBankById(Id As Integer, Optional tracking As Boolean = True) As Bank Implements IBankRepository.GetBankById
        Dim bank = From e In _context.Bank
                   Where e.Id = Id
                   Select e
        If bank.Count > 0 Then
            Dim ObjBank = Nothing
            If tracking = False Then
                ObjBank = (From e In _context.Bank.AsNoTracking
                           Where e.Id = Id
                           Select e).SingleOrDefault
            Else
                ObjBank = bank.SingleOrDefault
            End If
            Return ObjBank
        Else
            Return New Bank()
        End If
    End Function

    ''' <summary>
    ''' Sp para la importación de archivo
    ''' </summary>
    ''' <param name="xml"></param>
    ''' <returns></returns>
    Public Function SetBankDetail(xml As String) As ObjectResult(Of SP_SetBankDetail_Result) Implements IBankRepository.SetBankDetail
        Return _context.SP_SetBankDetail(xml)
    End Function


    '' <summary>
    '' Copiar y pegar para el formulario
    '' </summary>
    '' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetBankAutomaticRecognitionRules(xmlObject As String) As List(Of SP_SetBankAutomaticRecognitionRules_Result) Implements IBankRepository.SetBankAutomaticRecognitionRules
        'DirectCast(Of _context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SetBankAutomaticRecognitionRules(xmlObject).ToList()
    End Function


End Class
