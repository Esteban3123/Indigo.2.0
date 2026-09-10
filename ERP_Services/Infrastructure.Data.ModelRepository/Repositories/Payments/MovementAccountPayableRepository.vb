'***********************************************************************
' Assembly         : Infrastructure.Data.PaymentsRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class MovementAccountPayableRepository
    Inherits GenericRepository(Of MovementAccountPayables)
    Implements IMovementAccountPayableRepository

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
    ''' Obtiene un movimiento de cuentas por pagar por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetMovementAccountPayablesById(Id As Integer) As MovementAccountPayables Implements IMovementAccountPayableRepository.GetMovementAccountPayablesById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As MovementAccountPayables In Me._context.MovementAccountPayables Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As MovementAccountPayables In Me._context.MovementAccountPayables Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New MovementAccountPayables()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayable">The identifier account payable.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdAccountPayable</exception>
    Public Function GetMovementAccountPayablesByIdAccountPayable(IdAccountPayable As Integer) As List(Of MovementAccountPayables) Implements IMovementAccountPayableRepository.GetMovementAccountPayablesByIdAccountPayable
        If IdAccountPayable = 0 Then
            Throw New ArgumentNullException("IdAccountPayable")
        End If
        Return (From d As MovementAccountPayables In Me._context.MovementAccountPayables Where d.IdAccountPayable = IdAccountPayable Select d).ToList()
    End Function

    ''' <summary>
    ''' Obtiene un movimiento de cuentas por pagar por id de las cuotas de cuentas por pagar
    ''' </summary>
    ''' <param name="IdAccountPayableShare">The identifier account payable share.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">IdAccountPayableShare</exception>
    Public Function GetMovementAccountPayablesByIdAccountPayableShare(IdAccountPayableShare As Integer) As List(Of MovementAccountPayables) Implements IMovementAccountPayableRepository.GetMovementAccountPayablesByIdAccountPayableShare
        If IdAccountPayableShare = 0 Then
            Throw New ArgumentNullException("IdAccountPayableShare")
        End If
        Return (From d As MovementAccountPayables In Me._context.MovementAccountPayables Where d.IdAccountPayableShare = IdAccountPayableShare Select d).ToList()
    End Function

End Class
