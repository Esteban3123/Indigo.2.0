'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ConsumeServiceRepository
    Inherits GenericRepository(Of PharmaceuticalDispensing)
    Implements IConsumeServiceRepository

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
    ''' Guarda en las tablas de control para la integración con HEON
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_SaveIntegration(XmlObject As String, UserCode As String) As SP_SaveIntegration_Result Implements IConsumeServiceRepository.SP_SaveIntegration
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveIntegration(XmlObject, UserCode).SingleOrDefault
    End Function

End Class
