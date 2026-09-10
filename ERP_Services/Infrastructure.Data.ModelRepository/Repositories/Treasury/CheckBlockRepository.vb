'***********************************************************************
' Assembly         : Infrastructure.Data.TreasuryRepositiry
' Author           : Diego Andrés Roldán Lozano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CheckBlockRepository
    Inherits GenericRepository(Of CheckBlock)
    Implements ICheckBlockRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Obtener un registro de cheque bloqueado
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">Id</exception>
    Public Function GetCheckBlockById(Id As Integer) As CheckBlock Implements ICheckBlockRepository.GetCheckBlockById
        If Id = 0 Then
            Throw New ArgumentNullException("Id")
        End If
        Dim res = (From d As CheckBlock In Me._context.CheckBlock Where d.Id = Id Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As CheckBlock In Me._context.CheckBlock.AsNoTracking() Where d.Id = Id Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New CheckBlock()
        End If
    End Function

    ''' <summary>
    ''' Obtiene un cheque bloqueado por el id de la chequera y numero del cheque
    ''' </summary>
    Public Function GetCheckBlockByIdCheckBookAndNumber(IdCheckBook As Integer, checkNumber As Long) As CheckBlock Implements ICheckBlockRepository.GetCheckBlockByIdCheckBookAndNumber
        If IdCheckBook = 0 Then
            Throw New ArgumentNullException("IdCheckBook")
        End If
        If checkNumber = 0 Then
            Throw New ArgumentNullException("checkNumber")
        End If
        Dim res = (From d As CheckBlock In Me._context.CheckBlock Where d.IdCheckbook = IdCheckBook And d.CheckNumber = checkNumber Select d).ToList()
        If res IsNot Nothing AndAlso res.Count > 0 Then
            res(0).OriginalValue = (From d As CheckBlock In Me._context.CheckBlock.AsNoTracking() Where d.IdCheckbook = IdCheckBook And d.CheckNumber = checkNumber Select d).SingleOrDefault()
            Return res(0)
        Else
            Return New CheckBlock()
        End If
    End Function

    Public Function ListCheckByCheckbookId(checkbookid As Integer) As List(Of CheckBlock) Implements ICheckBlockRepository.ListCheckByCheckbookId
        Return (From c In _context.CheckBlock.AsNoTracking() Where c.IdCheckbook = checkbookid Select c).ToList()
    End Function
End Class
