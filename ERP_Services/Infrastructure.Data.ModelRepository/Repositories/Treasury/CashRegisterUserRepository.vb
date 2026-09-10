'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CashRegisterUserRepository
    Inherits GenericRepository(Of CashRegisterUser)
    Implements ICashRegisterUserRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene un usuario de registro de caja
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetCashRegisterUserById(Id As Integer) As CashRegisterUser Implements ICashRegisterUserRepository.GetCashRegisterUserById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From c As CashRegisterUser In Me._context.CashRegisterUser Where c.Id.Equals(Id) Select c).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From c As CashRegisterUser In Me._context.CashRegisterUser.AsNoTracking() Where c.Id.Equals(Id) Select c).SingleOrDefault()
            Return res(0)
        Else
            Return New CashRegisterUser()
        End If
    End Function

    ''' <summary>
    ''' Lista los usuarios relacionados a un registro de caja
    ''' </summary>
    ''' <param name="IdCashRegister"></param>
    ''' <returns></returns>
    Public Function ListCashRegisterUserByIdCashRegister(IdCashRegister As Integer) As List(Of CashRegisterUser) Implements ICashRegisterUserRepository.ListCashRegisterUserByIdCashRegister
        Dim res = (From c As CashRegisterUser In Me._context.CashRegisterUser Where c.IdCashRegister.Equals(IdCashRegister) Select c).ToList()
        Return res
    End Function

End Class
