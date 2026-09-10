'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 23-05-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CancellationCheckRepository
    Inherits GenericRepository(Of CancellationChecks)
    Implements ICancellationCheckRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtener un registro de cheque cancelado
    ''' </summary>
    ''' <param name="IdEntityAccount">The identifier entity account.</param>
    ''' <param name="CheckNumber">The check number.</param>
    Public Function GetCancellationCheckByEntityAccountAndCheckNumber(IdEntityAccount As Integer, CheckNumber As String) As CancellationChecks Implements ICancellationCheckRepository.GetCancellationCheckByEntityAccountAndCheckNumber
        If CheckNumber Is Nothing OrElse CheckNumber.Trim().Equals(String.Empty) Then
            Throw New ArgumentNullException("CheckNumber")
        End If
        If IdEntityAccount = 0 Then
            Throw New ArgumentNullException("IdEntityAccount")
        End If
        Dim res = (From d As CancellationChecks In Me._context.CancellationChecks Where d.IdEntityAccount.Equals(IdEntityAccount) And d.CheckNumber.Equals(CheckNumber.Trim()) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As CancellationChecks In Me._context.CancellationChecks.AsNoTracking() Where d.IdEntityAccount.Equals(IdEntityAccount) And d.CheckNumber.Equals(CheckNumber.Trim()) Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New CancellationChecks()
        End If
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id de chequera y numero de cheque
    ''' </summary>
    Public Function GetCancellationCheckByCheckBookIdAndCheckNumber(checkBookId As Integer, CheckNumber As Long) As CancellationChecks Implements ICancellationCheckRepository.GetCancellationCheckByCheckBookIdAndCheckNumber
        If checkBookId = 0 Then
            Throw New ArgumentNullException("checkBookId")
        End If
        If CheckNumber = 0 Then
            Throw New ArgumentNullException("CheckNumber")
        End If
        Dim number As String = CheckNumber.ToString()
        Dim res = (From d As CancellationChecks In Me._context.CancellationChecks Where d.IdCheckBook = checkBookId And d.CheckNumber.Equals(number) Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As CancellationChecks In Me._context.CancellationChecks.AsNoTracking() Where d.IdCheckBook = checkBookId And d.CheckNumber.Equals(number) Select d).FirstOrDefault()
            Return res(0)
        Else
            Return New CancellationChecks()
        End If
    End Function

    ''' <summary>
    ''' Obtener un registro de cheque cancelado por id
    ''' </summary>
    Public Function GetCancellationCheckById(id As Integer) As CancellationChecks Implements ICancellationCheckRepository.GetCancellationCheckById
        If id = 0 Then
            Throw New ArgumentNullException("id vacio")
        End If
        Dim res = (From d As CancellationChecks In Me._context.CancellationChecks.AsNoTracking() Where d.Id.Equals(id) Select d).FirstOrDefault()
        If res IsNot Nothing AndAlso res.Id = 0 Then
            Return res
        Else
            Return New CancellationChecks()
        End If
    End Function

End Class
