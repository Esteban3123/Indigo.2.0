'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/05/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Application.MedicalFees
Imports Microsoft.Practices.Unity

Partial Class MedicalFeesService

    ''' <summary>
    ''' Obtiene un detalle de contrato del medico por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetHealthProfessionalContractById(id As Integer) As Domain.Base.Entities.ActionResult(Of Domain.Entities.HealthProfessionalContract) Implements IMedicalFeesHealthProfessionalContract.GetHealthProfessionalContractById
        Using service As IHealthProfessionalContractAdminService = Container.Current.Resolve(Of IHealthProfessionalContractAdminService)()
            Return service.GetHealthProfessionalContractById(id)
        End Using
        'Return Me._healthProfessionalContractAdminService.GetHealthProfessionalContractById(id)
    End Function

    ''' <summary>
    ''' Obtiene un listado de detalles de contrato del medico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.HealthProfessionalContract)) Implements IMedicalFeesHealthProfessionalContract.GetListHealthProfessionalContractByHealthProfessionalCode
        Using service As IHealthProfessionalContractAdminService = Container.Current.Resolve(Of IHealthProfessionalContractAdminService)()
            Return service.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
        End Using
        'Return Me._healthProfessionalContractAdminService.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Obtiene el listado de contratos que tiene asociado el médico y son de tipo estandar
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode As String) As Domain.Base.Entities.ActionResult(Of List(Of Domain.Entities.HealthProfessionalContract)) Implements IMedicalFeesHealthProfessionalContract.ListHealthProfessionalContractWithTypeStandard
        Using service As IHealthProfessionalContractAdminService = Container.Current.Resolve(Of IHealthProfessionalContractAdminService)()
            Return service.ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode)
        End Using
        'Return Me._healthProfessionalContractAdminService.ListHealthProfessionalContractWithTypeStandard(healthProfessionalCode)
    End Function

End Class
