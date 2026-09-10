'***********************************************************************
' Assembly         : DistributedServices.MedicalFees
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel

<ServiceContract()> _
Public Interface IMedicalFeesHealthProfessionalContract

    ''' <summary>
    ''' Obtiene el listado de detalles de contratos que tiene asociado el médico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As ActionResult(Of List(Of HealthProfessionalContract))

    ''' <summary>
    ''' Obtiene un contrato asociado al medico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetHealthProfessionalContractById(id As Integer) As ActionResult(Of HealthProfessionalContract)

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el médico y son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode As String) As ActionResult(Of List(Of HealthProfessionalContract))

End Interface
