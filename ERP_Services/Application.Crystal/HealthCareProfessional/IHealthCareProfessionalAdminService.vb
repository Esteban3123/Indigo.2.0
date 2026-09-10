'***********************************************************************
' Assembly         : Application.Crystal
' Author           : J. Kevin Garay
' Created          : 26-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.Dynamic
Imports Domain.Crystal.Entities
Imports Domain.Entities

Public Interface IHealthCareProfessionalAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtener Profesional Por Código
    ''' </summary>
    ''' <param name="Code">Código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProfessionalByCode(Code As String) As ActionResult(Of INPROFSAL)

    ''' <summary>
    ''' Obtener Especialidad Por Código
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSpecialityByCode(Code As String) As ActionResult(Of INESPECIA)

    ''' <summary>
    ''' Guarda Profesionales
    ''' </summary>
    ''' <param name="professional">entidad profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveProfessional(professional As INPROFSAL, ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                              ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract), audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.INPROFSAL)

    ''' <summary>
    ''' Elimina profesionales
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteProfessional(Code As String) As ActionResult

    ''' <summary>
    ''' Cambia el estado del paciente
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateStatusProfessional(Code As String, Status As Boolean) As ActionResult(Of INPROFSAL)

    ''' <summary>
    ''' Obtiene el listado de detalles de contratos que tiene asociado el médico
    ''' </summary>
    ''' <param name="healthProfessionalCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetListHealthProfessionalContractByHealthProfessionalCode(healthProfessionalCode As String) As ActionResult(Of List(Of Domain.Entities.HealthProfessionalContract))

    ''' <summary>
    ''' Obtener Usuario del HIS
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetUserHIS(Code As String) As ActionResult(Of SEGusuaru)

    ''' <summary>
    ''' Funcion para obtener un profesional de la Salud INT o Ext
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    Function GetHealthProfessionalByCode(Code As String) As ActionResult(Of HealthProfessionalModel)

    Function SaveHealthProfessional(professional As HealthProfessionalModel,
                                            ListHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            ListDeleteHealthProfessionalContract As List(Of Domain.Entities.HealthProfessionalContract),
                                            audit As AuditMessage) As ActionResult(Of HealthProfessionalModel)
    Function UpdateStatusProfessionalHealth(Code As String, Status As Boolean) As ActionResult(Of HealthProfessionalModel)

End Interface
