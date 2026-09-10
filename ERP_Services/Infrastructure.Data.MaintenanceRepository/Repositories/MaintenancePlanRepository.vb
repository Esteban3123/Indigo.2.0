'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 21-06-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Maintenance.Entities
Imports Domain.Maintenance

Public Class MaintenancePlanRepository

    Inherits GenericRepository(Of MaintenancePlan)
    Implements IMaintenancePlanRepository


    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IMaintenanceModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de Mantenimiento
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IMaintenanceModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' consulta para retornar un Plan de Mantenimiento por Código
    ''' </summary>
    ''' <param name="codeMaintenancePlan">el codigo del Plan de Mantenimiento</param>
    ''' <returns>Objeto Compañia</returns>
    Public Function GetMaintenancePlan(codeMaintenancePlan As String, Optional Tracking As Boolean = False) As MaintenancePlan Implements IMaintenancePlanRepository.GetMaintenancePlan
        Dim MaintenancePlan = From e In _context.MaintenancePlan.Include("MaintenancePlanDetail").Include("MaintenancePlanDetail.MaintenanceActivity").Include("MaintenancePlanDetail.EquipmentTypePartsAccesoriesConsumibles").Include("MaintenancePlanDetail.EquipmentTypePartsAccesoriesConsumibles.PartsAccesoriesConsumables")
                  Where e.Code = codeMaintenancePlan
                  Select e
        If MaintenancePlan.Count > 0 Then
            Dim ObjMaintenancePlan = Nothing
            If Tracking = False Then
                ObjMaintenancePlan = (From e In _context.MaintenancePlan.AsNoTracking()
                          Where e.Code = codeMaintenancePlan
                          Select e).SingleOrDefault
            Else
                ObjMaintenancePlan = MaintenancePlan.SingleOrDefault
            End If
            Return ObjMaintenancePlan
        Else
            Return New MaintenancePlan()
        End If
    End Function
End Class
