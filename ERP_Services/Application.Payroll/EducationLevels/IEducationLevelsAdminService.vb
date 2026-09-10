'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 12-04-2011
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IEducationLevelsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los niveles de educacion
    ''' </summary>
    ''' <returns>Lista de niveles de educacion</returns>
    ''' <remarks></remarks>
    Function ListAllEducationLevels() As List(Of EducationLevel)

    ''' <summary>
    ''' Obtiene un nivel de educacion especifico
    ''' </summary>
    ''' <param name="code">Codigo del nivel de educacion</param>
    ''' <returns>Nivel de educacion</returns>
    ''' <remarks></remarks>
    Function GetEducationLevels(ByVal code As String) As EducationLevel

    ''' <summary>
    ''' Graba o actualiza un nivel de educacion
    ''' </summary>
    ''' <param name="EducationLevel">Nivel de educacion a guardar</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function SaveEducationLevels(ByVal EducationLevel As EducationLevel, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of EducationLevel)

    ''' <summary>
    ''' Elimina un nivel de educacion
    ''' </summary>
    ''' <param name="EducationLevel">Nivel de educacion</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>True o False</returns>
    ''' <remarks></remarks>
    Function DeleteEducationLevels(ByVal EducationLevel As EducationLevel, ByVal audit As AuditMessage) As ActionResult

    Function ChangeStateEducationLevel(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of EducationLevel)

End Interface
