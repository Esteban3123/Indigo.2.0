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

Public Interface IAdmissionsAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtener Ingreso Por Código
    ''' </summary>
    ''' <param name="Code">Código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionByCode(Code As String) As ActionResult(Of ADINGRESO)

    Function ModifyAuthorizationAdmission(admissionNumber As String, authorizationNumber As String) As ActionResult

    Function ListarHemocomponentesPorIngreso(ByVal ingreso As String) As List(Of SP_ListarHemocomponentesPorIngreso_Result)

    ''' <summary>
    ''' Obtener Ingresos Por Paciente
    ''' </summary>
    ''' <param name="Identification">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionsByPatient(Identification As String) As ActionResult(Of List(Of ADINGRESO))

    ''' <summary>
    ''' Guarda Ingreso
    ''' </summary>
    ''' <param name="admission">entidad profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAdmission(admission As ADINGRESO, audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.ADINGRESO)

    ''' <summary>
    ''' Elimina Ingreso
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAdmission(Code As String) As ActionResult

    ''' <summary>
    ''' Cambia el estado del ingreso
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function UpdateStatusAdmission(Code As String, Status As String, Justification As String, audit As AuditMessage) As ActionResult(Of ADINGRESO)

    ''' <summary>
    ''' Obtiene los parametros de un centro de atención
    ''' </summary>
    ''' <param name="_codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCentersParameters(_codcenate As String) As ActionResult(Of ADPARAMET)

    ''' <summary>
    ''' Obtiene fecha de triage para validar creación de ingreo
    ''' </summary>
    ''' <param name="paciente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTriageDate(paciente As String) As DateTime

    ''' <summary>
    ''' Funcion que retorna un objeto tipo usaurio de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetusuarioCrystal(ByVal code As String) As SEGusuaru

    Function GetAdmissionStatusByNumIngres(admissionNumber As String) As String

    Function GetINDIAGNOPByAdmissionNumber(adminssionNumber As String) As INDIAGNOP

    ''' <summary>
    '''valida el ingreso que no este en estado fadturado,anulado, cerrado, si tiene egreso de cama y si tiene alta medica
    ''' </summary>
    ''' <returns></returns>
    Function AdmisionValidations(AdmissionCode As String) As ActionResult
End Interface
