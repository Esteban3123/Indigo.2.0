'***********************************************************************
' Assembly         : DistributedServices.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 29-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IInteropCostServiceInteropCostSetting

    ''' <summary>
    ''' Guarda un parámetro de costos
    ''' </summary>
    <OperationContract()>
    Function SaveInteropCostSetting(interopCostSetting As InteropCostSetting, audit As AuditMessage) As ActionResult(Of InteropCostSetting)

    ''' <summary>
    ''' Elimina un parámetro de costos
    ''' </summary>
    <OperationContract()>
    Function DeleteInteropCostSetting(interopCostSetting As InteropCostSetting, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene el parámetro actual de costos
    ''' </summary>
    <OperationContract()>
    Function GetInteropCostSetting() As InteropCostSetting

    ''' <summary>
    ''' Obtiene el parámetro actual de costos por id
    ''' </summary>
    <OperationContract()>
    Function GetInteropCostSettingById(id As Integer) As InteropCostSetting


End Interface