'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryControlServiceRepository
' Author           : Miguel Angel Fonseca Castro
' Created          : 2018-04-14
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class InventoryControlServiceRepository
    Inherits GenericRepository(Of InventoryControlService)
    Implements IInventoryControlServiceRepository

    Private _context As IGlobalModelUnitOfWork

    Sub New(ByVal Context As IGlobalModelUnitOfWork)
        MyBase.New(Context)
        _context = Context
    End Sub

    ''' <summary>
    ''' Método para obtener el registro del servicio de control de inventario por EntityId y EntityName
    ''' </summary>
    ''' <param name="entityId"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlServiceByEntityIdAndEntityName(entityId As Integer, entityName As String) As InventoryControlService Implements IInventoryControlServiceRepository.GetInventoryControlServiceByEntityIdAndEntityName
        Dim res = (From c In Me._context.InventoryControlService Where c.EntityId = entityId AndAlso c.EntityName = entityName Select c).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New InventoryControlService
        End If
    End Function

    ''' <summary>
    ''' Método para obtener el registro del servicio de control de inventario por EntityId y EntityName
    ''' </summary>
    ''' <param name="entityCode"></param>
    ''' <param name="entityName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlServiceByEntityIdAndEntityName(entityCode As String, entityName As String) As InventoryControlService Implements IInventoryControlServiceRepository.GetInventoryControlServiceByEntityCodeAndEntityName
        Dim res = (From c In Me._context.InventoryControlService Where c.EntityCode = entityCode AndAlso c.EntityName = entityName Select c).FirstOrDefault
        If res IsNot Nothing Then
            Return res
        Else
            Return New InventoryControlService
        End If
    End Function

    ''' <summary>
    ''' Método una lista de control de servicios de inventario por orden medica
    ''' </summary>
    ''' <param name="medicalOrderRecipe"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetInventoryControlServiceByMedicalOrderRecipe(medicalOrderRecipe As String) As List(Of InventoryControlService) Implements IInventoryControlServiceRepository.GetInventoryControlServiceByMedicalOrderRecipe
        Return (From c In Me._context.InventoryControlService Where c.MedicalOrderRecipe = medicalOrderRecipe Select c).ToList()
    End Function

End Class