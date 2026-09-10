'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán lozano
' Created          : 09-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.InteropCost
Imports Microsoft.Practices.Unity

Partial Class InteropCostService
    Implements IInteropCostServiceBlockRecord

    ''' <summary>
    ''' Elimina un registro bloqueado
    ''' </summary>
    ''' <param name="blockRecordInteropCost"></param>
    ''' <returns></returns>
    Public Function DeleteBlockRecordInteropCost(blockRecordInteropCost As BlockRecordInteropCost) As ActionResult Implements IInteropCostServiceBlockRecord.DeleteBlockRecordInteropCost
        Using service As IInteropCostBlockRecordAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostBlockRecordAdminService)()
            Return service.DeleteBlockRecordInteropCost(blockRecordInteropCost)
        End Using
        'Return _blockRecordAdminService.DeleteBlockRecordInteropCost(blockRecordInteropCost)
    End Function

    ''' <summary>
    ''' Obtiene un registro bloqueado por id del registro y formulario
    ''' </summary>
    ''' <param name="IdForm"></param>
    ''' <param name="IdRecord"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    Public Function GetBlockRecordInteropCostByIdformAndIdRecord(IdForm As String, IdRecord As String, Optional tracking As Boolean = True) As BlockRecordInteropCost Implements IInteropCostServiceBlockRecord.GetBlockRecordInteropCostByIdformAndIdRecord
        Using service As IInteropCostBlockRecordAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostBlockRecordAdminService)()
            Return service.GetBlockRecordInteropCostByIdformAndIdRecord(IdForm, IdRecord, tracking)
        End Using
        'Return _blockRecordAdminService.GetBlockRecordInteropCostByIdformAndIdRecord(IdForm, IdRecord, tracking)
    End Function

    ''' <summary>
    ''' Bloquea un registro
    ''' </summary>
    ''' <param name="blockRecordInteropCost"></param>
    ''' <returns></returns>
    Public Function SaveBlockRecordInteropCost(blockRecordInteropCost As BlockRecordInteropCost) As ActionResult(Of BlockRecordInteropCost) Implements IInteropCostServiceBlockRecord.SaveBlockRecordInteropCost
        Using service As IInteropCostBlockRecordAdminService = DistributedServices.InteropCost.Container.Current.Resolve(Of IInteropCostBlockRecordAdminService)()
            Return service.SaveBlockRecordInteropCost(blockRecordInteropCost)
        End Using
        'Return _blockRecordAdminService.SaveBlockRecordInteropCost(blockRecordInteropCost)
    End Function

End Class