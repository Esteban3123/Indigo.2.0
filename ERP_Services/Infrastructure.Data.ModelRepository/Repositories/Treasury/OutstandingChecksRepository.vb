'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 20-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class OutstandingChecksRepository
    Inherits GenericRepository(Of OutstandingChecks)
    Implements IOutstandingChecksRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtiene el primer cheque que esta en espera
    ''' </summary>
    ''' <returns></returns>
    Public Function GetFirstOutstandingChecks(IdCheckBook As Integer) As OutstandingChecks Implements IOutstandingChecksRepository.GetFirstOutstandingChecks
        Dim res = (From oc As OutstandingChecks In Me._context.OutstandingChecks.AsNoTracking()
                   Join c As Checkbooks In Me._context.Checkbooks.AsNoTracking() On oc.IdCheckBook Equals c.Id
                   Where oc.IdCheckBook = IdCheckBook AndAlso
                         Not Me._context.VoucherTransaction.Any(Function(vt) _
                             vt.IdEntityBankAccount.HasValue AndAlso
                             vt.IdEntityBankAccount.Value = c.IdEntityBanckAccount AndAlso
                             vt.CheckNumber.HasValue AndAlso
                             vt.CheckNumber.Value = oc.CheckNumber AndAlso
                             (vt.Status = 1 OrElse vt.Status = 2))
                   Order By oc.CheckNumber Ascending
                   Select oc).FirstOrDefault()

        If res IsNot Nothing Then
            Return res
        Else
            Return New OutstandingChecks()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cheque pendiente por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Public Function GetOutstandingChecksById(Id As Integer) As OutstandingChecks Implements IOutstandingChecksRepository.GetOutstandingChecksById
        Dim res = (From d As OutstandingChecks In Me._context.OutstandingChecks Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As OutstandingChecks In Me._context.OutstandingChecks.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New OutstandingChecks()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los cheques pendientes por id de la chequera
    ''' </summary>
    ''' <param name="IdCheckBook">The identifier check book.</param>
    ''' <returns></returns>
    Public Function ListOutstandingChecksByIdCheckBook(IdCheckBook As Integer) As List(Of OutstandingChecks) Implements IOutstandingChecksRepository.ListOutstandingChecksByIdCheckBook
        Dim res = (From d As OutstandingChecks In Me._context.OutstandingChecks Where d.IdCheckBook = IdCheckBook Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            Return res
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cheque pendiente por id de la chequera y numero del cheque
    ''' </summary>
    ''' <returns></returns>
    Public Function GetOutstandingCheckByCheckBookIdAndCheckNumber(checkBookId As Integer, checkNumber As Long) As OutstandingChecks Implements IOutstandingChecksRepository.GetOutstandingCheckByCheckBookIdAndCheckNumber
        Dim res = (From d As OutstandingChecks In Me._context.OutstandingChecks Where d.IdCheckBook = checkBookId AndAlso d.CheckNumber = checkNumber Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As OutstandingChecks In Me._context.OutstandingChecks.AsNoTracking() Where d.IdCheckBook = checkBookId AndAlso d.CheckNumber = checkNumber Select d).FirstOrDefault()
            Return res(0)
        Else
            Return New OutstandingChecks()
        End If
    End Function

End Class
