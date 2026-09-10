'***********************************************************************
' Assembly         : DistributedServices.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 26-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface ICostServiceCostSetting
    ''' <summary>
    ''' Guarda un parámetro de costos
    ''' </summary>
    <OperationContract()>
    Function SaveCostSetting(costSetting As Domain.Entities.CostSetting, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.CostSetting)
    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    <OperationContract()>
    Function DeleteCostSetting(costSetting As Domain.Entities.CostSetting, audit As AuditMessage) As Domain.Base.Entities.ActionResult
    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    <OperationContract()> _
    Function GetCostSetting() As CostSetting
    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    <OperationContract()> _
    Function GetCostSettingById(id As Integer) As CostSetting
End Interface
