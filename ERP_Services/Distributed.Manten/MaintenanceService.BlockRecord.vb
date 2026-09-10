'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/04/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.Maintenance
Imports Infrastructure.CrossCutting.IOC
Imports Microsoft.Practices.Unity

Partial Class MaintanceService

    ''' <summary>
    ''' Elimina una registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function DeleteBlockRecordMaintenance(blockRecord As Domain.Entities.BlockRecordMaintenance, session As SessionValues) As Domain.Base.Entities.ActionResult Implements IBlockRecordMaintenanceService.DeleteBlockRecordMaintenance
        Using service As IBlockRecordMaintenanceAdminService = Container.Current.Resolve(Of IBlockRecordMaintenanceAdminService)()
            Return service.DeleteBlockRecordMaintenance(blockRecord)
        End Using
    End Function

    ''' <summary>
    ''' Gets the block record treasury by idform and identifier record.
    ''' </summary>
    ''' <param name="IdForm">The identifier form.</param>
    ''' <param name="IdRecord">The identifier record.</param>
    ''' <returns></returns>
    Public Function GetBlockRecordMaintenanceByIdformAndIdRecord(IdForm As String, IdRecord As String, session As SessionValues) As Domain.Entities.BlockRecordMaintenance Implements IBlockRecordMaintenanceService.GetBlockRecordMaintenanceByIdformAndIdRecord
        Using service As IBlockRecordMaintenanceAdminService = Container.Current.Resolve(Of IBlockRecordMaintenanceAdminService)()
            Return service.GetBlockRecordMaintenanceByIdformAndIdRecord(IdForm, IdRecord)
        End Using
    End Function

    ''' <summary>
    ''' Almacena o Actualiza registro bloqueado
    ''' </summary>
    ''' <returns>
    ''' ActionResult
    ''' </returns>
    Public Function SaveBlockRecordMaintenance(blockRecord As Domain.Entities.BlockRecordMaintenance, session As SessionValues) As Domain.Base.Entities.ActionResult(Of Domain.Entities.BlockRecordMaintenance) Implements IBlockRecordMaintenanceService.SaveBlockRecordMaintenance
        Using service As IBlockRecordMaintenanceAdminService = Container.Current.Resolve(Of IBlockRecordMaintenanceAdminService)()
            Return service.SaveBlockRecordMaintenance(blockRecord)
        End Using
    End Function

End Class
