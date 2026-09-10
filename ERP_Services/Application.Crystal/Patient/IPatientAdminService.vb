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
Public Interface IPatientAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un Paciente por identificación
    ''' </summary>
    ''' <param name="Identification">Identificación de paciente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPatientByIdentification(Identification As String) As ActionResult(Of INPACIENT)

    ''' <summary>
    ''' Guarda Pacientes
    ''' </summary>
    ''' <param name="patient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SavePatient(patient As INPACIENT, audit As AuditMessage) As ActionResult(Of Domain.Crystal.Entities.INPACIENT)

    ''' <summary>
    ''' Elimina pacientes
    ''' </summary>
    ''' <param name="Identification"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeletePatient(Identification As String, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtener Nivel Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLevelByCode(Code As String) As ADNIVELES

    ''' <summary>
    ''' Guarda el peso del paciente
    ''' </summary>
    ''' <param name="weight"></param>
    ''' <param name="codePatient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveWeightPatient(weight As Integer, codePatient As String) As Boolean

End Interface
