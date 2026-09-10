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
    Implements ICrystalServiceAdmissions

    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code">Código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionByCode(Code As String) As ActionResult(Of ADINGRESO) Implements ICrystalServiceAdmissions.GetAdmissionByCode
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetAdmissionByCode(Code)
        End Using
        'Return Me._admissionsAdminService.GetAdmissionByCode(Code)
    End Function

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Identification">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionsByPatient(Identification As String) As ActionResult(Of List(Of ADINGRESO)) Implements ICrystalServiceAdmissions.GetAdmissionsByPatient
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetAdmissionsByPatient(Identification)
        End Using
        'Return Me._admissionsAdminService.GetAdmissionsByPatient(Identification)
    End Function

    ''' <summary>
    ''' Guarda Profesionales
    ''' </summary>
    ''' <param name="admission">entidad profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAdmission(admission As ADINGRESO, audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.ADINGRESO) Implements ICrystalServiceAdmissions.SaveAdmission
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.SaveAdmission(admission, audit)
        End Using
        'Return Me._admissionsAdminService.SaveAdmission(admission, audit)
    End Function

    ''' <summary>
    ''' Elimina profesionales
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAdmission(Code As String) As ActionResult Implements ICrystalServiceAdmissions.DeleteAdmission
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.DeleteAdmission(Code)
        End Using
        'Return Me._admissionsAdminService.DeleteAdmission(Code)
    End Function

    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateStatusAdmission(Code As String, Status As String, Justification As String, audit As AuditMessage) As ActionResult(Of ADINGRESO) Implements ICrystalServiceAdmissions.UpdateStatusAdmission
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.UpdateStatusAdmission(Code, Status, Justification, audit)
        End Using
        'Return Me._admissionsAdminService.UpdateStatusAdmission(Code, Status, Justification, audit)
    End Function

    ''' <summary>
    ''' Obtiene los parametros de un centro de atención
    ''' </summary>
    ''' <param name="_codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCentersParameters(_codcenate As String) As ActionResult(Of ADPARAMET) Implements ICrystalServiceAdmissions.GetCentersParameters
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetCentersParameters(_codcenate)
        End Using
        'Return Me._admissionsAdminService.GetCentersParameters(_codcenate)
    End Function

    ''' <summary>
    ''' Obtiene la fecha de triage para validacion de ingreso
    ''' </summary>
    ''' <param name="paciente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTriageDate(paciente As String) As DateTime Implements ICrystalServiceAdmissions.GetTriageDate
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetTriageDate(paciente)
        End Using
        'Return Me._admissionsAdminService.GetTriageDate(paciente)
    End Function

    ''' <summary>
    ''' Obtiene un usuario de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetusuarioCrystal(code As String) As SEGusuaru Implements ICrystalServiceAdmissions.GetusuarioCrystal
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetusuarioCrystal(code)
        End Using
        'Return Me._admissionsAdminService.GetusuarioCrystal(code)
    End Function

    Public Function GetAdmissionStatusByNumIngres(admissionNumber As String) As String Implements ICrystalServiceAdmissions.GetAdmissionStatusByNumIngres
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetAdmissionStatusByNumIngres(admissionNumber)
        End Using
        'Return _admissionsAdminService.GetAdmissionStatusByNumIngres(admissionNumber)
    End Function

    Public Function ModifyAuthorizationAdmission(admissionNumber As String, authorizationNumber As String) As ActionResult Implements ICrystalServiceAdmissions.ModifyAuthorizationAdmission
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.ModifyAuthorizationAdmission(admissionNumber, authorizationNumber)
        End Using
        'Return _admissionsAdminService.ModifyAuthorizationAdmission(admissionNumber, authorizationNumber)
    End Function

    Public Function GetINDIAGNOPByAdmissionNumber(adminssionNumber As String) As INDIAGNOP Implements ICrystalServiceAdmissions.GetINDIAGNOPByAdmissionNumber
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.GetINDIAGNOPByAdmissionNumber(adminssionNumber)
        End Using
        'Return _admissionsAdminService.GetINDIAGNOPByAdmissionNumber(adminssionNumber)
    End Function

    Public Function ListarHemocomponentesPorIngreso(ingreso As String) As List(Of SP_ListarHemocomponentesPorIngreso_Result) Implements ICrystalServiceAdmissions.ListarHemocomponentesPorIngreso
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.ListarHemocomponentesPorIngreso(ingreso)
        End Using
        'Return _admissionsAdminService.ListarHemocomponentesPorIngreso(ingreso)
    End Function

    ''' <summary>
    '''valida el ingreso que no este en estado fadturado,anulado, cerrado, si tiene egreso de cama y si tiene alta medica
    ''' </summary>
    Function AdmisionValidations(AdmissionCode As String) As ActionResult Implements ICrystalServiceAdmissions.AdmisionValidations
        Using service As IAdmissionsAdminService = Container.Current.Resolve(Of IAdmissionsAdminService)()
            Return service.AdmisionValidations(AdmissionCode)
        End Using
    End Function
End Class
