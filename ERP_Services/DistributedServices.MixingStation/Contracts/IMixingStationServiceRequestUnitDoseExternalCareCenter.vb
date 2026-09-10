'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 09/02/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

<ServiceContract()>
Public Interface IMixingStationServiceRequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveRequestUnitDoseExternalCareCenter(ByVal RequestUnitDoseExternalCareCenter As RequestUnitDoseExternalCareCenter, ByVal audit As AuditMessage, operatingUnitId As Integer, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of RequestUnitDoseExternalCareCenter)

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRequestUnitDoseExternalCareCenter(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of RequestUnitDoseExternalCareCenter)

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetRequestUnitDoseExternalCareCenterById(ByVal id As Integer) As ActionResult(Of RequestUnitDoseExternalCareCenter)

End Interface
