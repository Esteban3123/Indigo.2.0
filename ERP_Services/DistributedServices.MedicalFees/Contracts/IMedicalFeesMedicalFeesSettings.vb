'***********************************************************************
' Assembly         : DistributedServices.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/01/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesMedicalFeesSettings

    ''' <summary>
    ''' Guarda o Actualiza la entidad
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveMedicalFeesSettings(MedicalFeesSettings As Domain.Entities.SettingMedicalFees, audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingMedicalFees)

    ''' <summary>
    ''' Obtiene la entidad por codigo
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function GetMedicalFeesSettings(audit As AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.SettingMedicalFees)

End Interface
