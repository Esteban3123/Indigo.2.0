'***********************************************************************
' Assembly         : Domain.Crystal
' Author           : Jhossept K. Garay
' Created          : 26-01-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Crystal.Entities
Imports System.Dynamic

Public Interface IPatientRepository
    Inherits IRepository(Of INPACIENT)

    Function ListarHemocomponentesPorIngreso(ByVal ingreso As String) As List(Of SP_ListarHemocomponentesPorIngreso_Result)

    ''' <summary>
    ''' Obtiene un Paciente por identificación
    ''' </summary>
    ''' <param name="Identification">Identificación del paciente</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPatientByIdentification(Identification As String) As INPACIENT

    ''' <summary>
    ''' Obtener Nivel Por Código
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLevelByCode(Code As String) As ADNIVELES

    ''' <summary>
    ''' Obtiene el paciente
    ''' </summary>
    ''' <param name="CodePatient"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPatient(CodePatient As String) As INPACIENT

    ''' <summary>
    ''' Obtiene únicamente el paciente por identificación
    ''' </summary>
    ''' <param name="Identification">Identificación del paciente</param>
    ''' <param name="tracking">Indica si se realiza seguimiento de la entidad</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetOnlyPatientByIdentification(Identification As String, tracking As Boolean) As INPACIENT

    ''' <summary>
    ''' Obtiene el paciente por número de ingreso
    ''' </summary>
    ''' <param name="admissionNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPatientByAdmissionNumber(admissionNumber As String) As INPACIENT
End Interface
