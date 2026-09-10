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

Public Class TraceabilityPaperworkRepository
    Inherits GenericRepository(Of TraceabilityPaperwork)
    Implements ITraceabilityPaperworkRepository

#Region "Builder"

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

#End Region

#Region "Methods"

    Public Function GetTraceabilityPaperworkById(id As Integer) As TraceabilityPaperwork Implements ITraceabilityPaperworkRepository.GetTraceabilityPaperworkById
        Return (From x In _context.TraceabilityPaperwork Where x.Id = id).FirstOrDefault()
    End Function

    Public Function SP_AssignTraceabilityPaperwork(xml As String) As SP_AssignTraceabilityPaperwork_Result Implements ITraceabilityPaperworkRepository.SP_AssignTraceabilityPaperwork
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_AssignTraceabilityPaperwork(xml).SingleOrDefault()
    End Function

    Public Function SP_SaveTraceabilityPaperwork(xml As String) As SP_SaveTraceabilityPaperwork_Result Implements ITraceabilityPaperworkRepository.SP_SaveTraceabilityPaperwork
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveTraceabilityPaperwork(xml).SingleOrDefault()
    End Function

    Public Function SP_SaveAcceptanceAuthorization(xml As String) As SP_SaveAcceptanceAuthorization_Result Implements ITraceabilityPaperworkRepository.SP_SaveAcceptanceAuthorization
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveAcceptanceAuthorization(xml).SingleOrDefault()
    End Function

#End Region

End Class
