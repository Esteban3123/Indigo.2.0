'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class BankRepository
    Inherits GenericRepository(Of Bank)
    Implements IBankRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
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
        Dim bank = From e In _context.Bank
                   Where e.Code = code
                   Select e
        If bank.Count > 0 Then
            Dim ObjBank = Nothing
            If tracking = False Then
                ObjBank = (From e In _context.Bank.AsNoTracking
                          Where e.Code = code
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

End Class
