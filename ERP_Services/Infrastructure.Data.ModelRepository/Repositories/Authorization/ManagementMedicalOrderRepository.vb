Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class ManagementMedicalOrderRepository
    Inherits GenericRepository(Of ManagementMedicalOrder)
    Implements IManagementMedicalOrderRepository

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

    Public Function GetManagementMedicalOrderById(ByVal id As Integer) As ManagementMedicalOrder Implements IManagementMedicalOrderRepository.GetManagementMedicalOrderById
        Return (From x In _context.ManagementMedicalOrder Where x.Id = id).FirstOrDefault()
    End Function

    Public Function GetManagementMedicalOrderByEntity(ByVal entityName As String, ByVal entityId As Integer) As ManagementMedicalOrder Implements IManagementMedicalOrderRepository.GetManagementMedicalOrderByEntity
        Return (From x In _context.ManagementMedicalOrder Where x.EntityName = entityName AndAlso x.EntityId = entityId).FirstOrDefault()
    End Function

    Public Function SP_SaveManagementMedicalOrder(ByVal listManagementMedicalOrderXml As String, ByVal codeUser As String) As SP_SaveManagementMedicalOrder_Result Implements IManagementMedicalOrderRepository.SP_SaveManagementMedicalOrder
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveManagementMedicalOrder(listManagementMedicalOrderXml, codeUser).SingleOrDefault()
    End Function

#End Region

End Class
