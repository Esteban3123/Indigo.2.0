'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 28/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ConfigurationServicesAmbulatoryRepository
    Inherits GenericRepository(Of ConfigurationServicesAmbulatory)
    Implements IConfigurationServicesAmbulatoryRepository

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

    Public Function SP_SaveConfigurationServicesAmbulatory(xml As String, userCode As String) As SP_SaveConfigurationServicesAmbulatory_Result Implements IConfigurationServicesAmbulatoryRepository.SP_SaveConfigurationServicesAmbulatory
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveConfigurationServicesAmbulatory(xml, userCode).SingleOrDefault()
    End Function

    Public Function GetConfigurationServicesAmbulatoryById(id As Integer) As ConfigurationServicesAmbulatory Implements IConfigurationServicesAmbulatoryRepository.GetConfigurationServicesAmbulatoryById
        Return (From x In _context.ConfigurationServicesAmbulatory Where x.Id = id Select x).FirstOrDefault()
    End Function

End Class
