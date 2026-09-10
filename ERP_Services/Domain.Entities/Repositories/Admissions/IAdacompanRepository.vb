'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Cesar Collazos
' Created          : 01/12/2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base

Public Interface IAdacompanRepository
    Inherits IRepository(Of ADACOMPAN)

    ''' <summary>
    ''' Obtiene el responsable de un ingreso específico
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Entidad ADACOMPAN del responsable si existe, Nothing en caso contrario</returns>
    ''' <remarks>
    ''' Busca en la tabla ADACOMPAN donde RESPONSAB = '2' (responsable) para el ingreso dado.
    ''' Si hay múltiples responsables, retorna el más reciente por FECHAREGI.
    ''' </remarks>
    Function GetResponsibleByAdmissionNumber(admissionNumber As String) As ADACOMPAN

    ''' <summary>
    ''' Obtiene todos los acompañantes de un ingreso específico
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>Lista de acompañantes del ingreso</returns>
    Function GetByAdmissionNumber(admissionNumber As String) As List(Of ADACOMPAN)

    ''' <summary>
    ''' Obtiene todos los acompañantes de un paciente específico
    ''' </summary>
    ''' <param name="patientCode">Código del paciente (IPCODPACI)</param>
    ''' <returns>Lista de acompañantes del paciente</returns>
    Function GetByPatientCode(patientCode As String) As List(Of ADACOMPAN)

    ''' <summary>
    ''' Busca un acompañante por su identificación
    ''' </summary>
    ''' <param name="identification">Identificación del acompañante (IDACOMPAN)</param>
    ''' <returns>Entidad ADACOMPAN si existe, Nothing en caso contrario</returns>
    Function GetByIdentification(identification As String) As ADACOMPAN

    ''' <summary>
    ''' Verifica si existe un responsable para un ingreso específico
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <returns>True si existe un responsable, False en caso contrario</returns>
    Function HasResponsible(admissionNumber As String) As Boolean

    ''' <summary>
    ''' Obtiene todos los acompañantes por tipo de parentesco
    ''' </summary>
    ''' <param name="admissionNumber">Número de ingreso</param>
    ''' <param name="relationshipType">Tipo de parentesco (PARACOMPA)</param>
    ''' <returns>Lista de acompañantes con el tipo de parentesco especificado</returns>
    Function GetByRelationshipType(admissionNumber As String, relationshipType As String) As List(Of ADACOMPAN)

End Interface

