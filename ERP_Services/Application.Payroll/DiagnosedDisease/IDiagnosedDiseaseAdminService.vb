'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Juan Diego Díaz
' Created          : 05-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IDiagnosedDiseaseAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Elimina una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteDiagnosedDisease(ByVal diagnosedDisease As DiagnosedDisease, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Almacena o Actualiza una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="diagnosedDisease">Enfermedad Diagnosticada</param>
    ''' <param name="audit">Objeto Auditoría</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveDiagnosedDisease(ByVal diagnosedDisease As DiagnosedDisease, ByVal audit As AuditMessage, ByVal Optional idSequense As Long = 0) As ActionResult(Of DiagnosedDisease)

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada
    ''' </summary>
    ''' <param name="code">Código de la Enfermedad Diagnosticada</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Function GetDiagnosedDisease(ByVal code As String, ByVal tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DiagnosedDisease)

    ''' <summary>
    ''' Obtiene una Enfermedad Diagnosticada por ID
    ''' </summary>
    ''' <param name="ID">Id de la Enfermedad Diagnosticada</param>
    ''' <returns>Actividad en Tiempo Libre</returns>
    ''' <remarks></remarks>
    Function GetDiagnosedDiseaseById(ByVal id As Integer, ByVal tracking As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DiagnosedDisease)

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of DiagnosedDisease)

End Interface
