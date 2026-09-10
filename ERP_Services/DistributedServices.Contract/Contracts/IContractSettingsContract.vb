'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/02/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()>
Public Interface IContractSettingsContract

    ''' <summary>
    ''' Guarda o Actualiza un parametro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveSettingsContract(ByVal SettingsContract As SettingsContract, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of SettingsContract)

    ''' <summary>
    ''' Elimina un parametro
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function DeleteSettingsContract(ByVal SettingsContract As SettingsContract, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un determinado parametro por el id de la unidad operativa
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetSettingsContractByOperatingUnitId(ByVal operatingUnitId As Integer, ByVal audit As AuditMessage) As ActionResult(Of SettingsContract)

End Interface
