'***********************************************************************
' Assembly         : DistributedService.Billing
' Author           : Jhossept k. Garay
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.Crystal
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class CrystalService
    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code">Código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProfessionalByCode(Code As String) As ActionResult(Of INPROFSAL) Implements ICrystalServiceHealthCareProfessional.GetProfessionalByCode
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.GetProfessionalByCode(Code)
        End Using
        'Return Me._healthCareProfessionalAdminService.GetProfessionalByCode(Code)
    End Function

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSpecialityByCode(Code As String) As ActionResult(Of INESPECIA) Implements ICrystalServiceHealthCareProfessional.GetSpecialityByCode
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.GetSpecialityByCode(Code)
        End Using
        'Return Me._healthCareProfessionalAdminService.GetSpecialityByCode(Code)
    End Function

    ''' <summary>
    ''' Guarda Profesionales
    ''' </summary>
    ''' <param name="professional">entidad profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveProfessional(professional As INPROFSAL, ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                              ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract), audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.INPROFSAL) Implements ICrystalServiceHealthCareProfessional.SaveProfessional
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.SaveProfessional(professional, ListHealthProfessionalContract, ListDeleteHealthProfessionalContract, audit)
        End Using
        'Return Me._healthCareProfessionalAdminService.SaveProfessional(professional, ListHealthProfessionalContract, ListDeleteHealthProfessionalContract, audit)
    End Function

    ''' <summary>
    ''' Elimina profesionales
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteProfessional(Code As String) As ActionResult Implements ICrystalServiceHealthCareProfessional.DeleteProfessional
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.DeleteProfessional(Code)
        End Using
        'Return Me._healthCareProfessionalAdminService.DeleteProfessional(Code)
    End Function

    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateStatusProfessional(Code As String, Status As Boolean) As ActionResult(Of INPROFSAL) Implements ICrystalServiceHealthCareProfessional.UpdateStatusProfessional
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.UpdateStatusProfessional(Code, Status)
        End Using
        'Return Me._healthCareProfessionalAdminService.UpdateStatusProfessional(Code, Status)
    End Function

    ''' <summary>
    ''' Obtiene el listado de detalle de contrtato del medico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As ActionResult(Of List(Of Domain.Entities.HealthProfessionalContract)) Implements ICrystalServiceHealthCareProfessional.GetListHealthProfessionalContractByHealthProfessionalCode
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
        End Using
        'Return Me._healthCareProfessionalAdminService.GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode)
    End Function

    ''' <summary>
    ''' Obtiene el usuario del HIS
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserHIS(Code As String) As ActionResult(Of SEGusuaru) Implements ICrystalServiceHealthCareProfessional.GetUserHIS
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.GetUserHIS(Code)
        End Using
        'Return Me._healthCareProfessionalAdminService.GetUserHIS(Code)
    End Function

    Public Function GetHealthProfessionalByCode(Code As String) As ActionResult(Of HealthProfessionalModel) Implements ICrystalServiceHealthCareProfessional.GetHealthProfessionalByCode
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.GetHealthProfessionalByCode(Code)
        End Using
    End Function

    Public Function SaveHealthProfessional(professional As HealthProfessionalModel,
                                            ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            audit As AuditMessage) As ActionResult(Of HealthProfessionalModel) Implements ICrystalServiceHealthCareProfessional.SaveHealthProfessional

        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.SaveHealthProfessional(professional, ListHealthProfessionalContract, ListDeleteHealthProfessionalContract, audit)
        End Using

    End Function

    Public Function UpdateStatusProfessionalHealth(Code As String, Status As Boolean) As ActionResult(Of HealthProfessionalModel) Implements ICrystalServiceHealthCareProfessional.UpdateStatusProfessionalHealth
        Using service As IHealthCareProfessionalAdminService = Container.Current.Resolve(Of IHealthCareProfessionalAdminService)()
            Return service.UpdateStatusProfessionalHealth(Code, Status)
        End Using
    End Function

End Class
