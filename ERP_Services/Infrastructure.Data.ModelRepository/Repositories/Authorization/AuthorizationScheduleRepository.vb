'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 22/05/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AuthorizationScheduleRepository
    Inherits GenericRepository(Of AuthorizationSchedule)
    Implements IAuthorizationScheduleRepository

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

    Public Function GetAuthorizationScheduleById(id As Integer) As AuthorizationSchedule Implements IAuthorizationScheduleRepository.GetAuthorizationScheduleById
        Return (From x In _context.AuthorizationSchedule Where x.Id = id).FirstOrDefault()
    End Function

    Public Function SP_SaveAuthorizationSchedule(xml As String) As SP_SaveAuthorizationSchedule_Result Implements IAuthorizationScheduleRepository.SP_SaveAuthorizationSchedule
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAuthorizationSchedule(xml).SingleOrDefault()
    End Function

End Class
