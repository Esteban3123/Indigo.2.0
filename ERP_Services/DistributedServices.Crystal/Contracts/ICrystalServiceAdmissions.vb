'***********************************************************************
' Assembly         : DistributedService.Crystal
' Author           : Jhossept K. Garay
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Crystal.Entities

#End Region
<ServiceContract()> _
Public Interface ICrystalServiceAdmissions
    ''' <summary>
    ''' Obtener Ingreso Por Código
    ''' </summary>
    ''' <param name="Code">Código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAdmissionByCode(Code As String) As ActionResult(Of ADINGRESO)

    <OperationContract()>
    Function ListarHemocomponentesPorIngreso(ingreso As String) As List(Of SP_ListarHemocomponentesPorIngreso_Result)

    ''' <summary>
    ''' Obtener Ingresos Por Paciente
    ''' </summary>
    ''' <param name="Identification">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetAdmissionsByPatient(Identification As String) As ActionResult(Of List(Of ADINGRESO))

    ''' <summary>
    ''' Guarda Ingreso
    ''' </summary>
    ''' <param name="admission">entidad profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveAdmission(admission As ADINGRESO, audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.ADINGRESO)

    ''' <summary>
    ''' Elimina Ingreso
    ''' </summary>
    ''' <param name="Code">código Profesional</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteAdmission(Code As String) As ActionResult

    ''' <summary>
    ''' Cambia el estado del ingreso
    ''' </summary>
    ''' <param name="Code">Código del paciente</param>
    ''' <param name="Status">nuevo estado </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function UpdateStatusAdmission(Code As String, Status As String, Justification As String, audit As AuditMessage) As ActionResult(Of ADINGRESO)

    ''' <summary>
    ''' Obtiene los parametros de un centro de atención
    ''' </summary>
    ''' <param name="_codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetCentersParameters(_codcenate As String) As ActionResult(Of ADPARAMET)

    ''' <summary>
    ''' Obtiene la fecha de triage para validacion de ingreso
    ''' </summary>
    ''' <param name="paciente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetTriageDate(paciente As String) As DateTime

    ''' <summary>
    ''' Obtiene un usuardio de crystal
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function GetusuarioCrystal(code As String) As SEGusuaru

    <OperationContract()> _
    Function GetAdmissionStatusByNumIngres(admissionNumber As String) As String

    <OperationContract()> _
    Function ModifyAuthorizationAdmission(admissionNumber As String, authorizationNumber As String) As ActionResult
    <OperationContract()>
    Function GetINDIAGNOPByAdmissionNumber(adminssionNumber As String) As INDIAGNOP

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="AdmisionCode"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function AdmisionValidations(AdmisionCode As String) As ActionResult

End Interface

