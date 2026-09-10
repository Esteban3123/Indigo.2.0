'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Juan F. Tamayo
' Created          : 2014-11-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Interface IAdmissionRepository
    Inherits IRepository(Of ADINGRESO)

    ''' <summary>
    ''' Obtiene un ingreso y sus agregados en una entidad plana y serializada en formato JSON
    ''' </summary>
    ''' <param name="code">Código del ingreso</param>
    ''' <returns>Entidad plana serializada</returns>
    Function GetAdmissionPOCOByCode(ByVal code As String) As ExpandoObject
    ''' <summary>
    ''' Obtiene un ingreso por codigo para utilizarlo en orden de servicio
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionByServiceOrder(admissionNumber As String) As ExpandoObject

    ''' <summary>
    ''' Obtiene un ingreso por código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionByCode(Code As String, Optional tracking As Boolean = True) As ADINGRESO

    ''' <summary>
    ''' Obtiene los ingresos de un paciente
    ''' </summary>
    ''' <param name="PatientIdentification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdmissionByPatient(PatientIdentification As String) As List(Of ADINGRESO)

    ''' <summary>
    ''' Obtiene los parametros por centro de atención
    ''' </summary>
    ''' <param name="_codcenate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCentersParameters(_codcenate As String) As ADPARAMET

    ''' <summary>
    ''' Obtiene fecha de triage para validar creación de ingreo
    ''' </summary>
    ''' <param name="paciente"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTriageDate(paciente As String) As DateTime

    ''' <summary>
    ''' para saber si la cama esta habilitada para asignarse
    ''' </summary>
    ''' <param name="cama"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetBedStatus(cama As String) As Boolean

    ''' <summary>
    ''' Funcion que valida si se puede anular un ingreso
    ''' </summary>
    ''' <param name="NUMINGRES"></param>
    ''' <param name="IPCODPACI"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function AdmissionAnulateValidation(NUMINGRES As String, IPCODPACI As String) As SP_AD_ValidaAnulacionIngreso_Result

    Function GetAdmissionStatusByNumIngres(admissionNumber As String) As String

    Function GetINDIAGNOPByAdmissionNumber(adminssionNumber As String) As INDIAGNOP


End Interface
