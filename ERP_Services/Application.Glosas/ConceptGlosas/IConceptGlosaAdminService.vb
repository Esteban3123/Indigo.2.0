'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Diego A. Roldán
' Created          : 2022-04-04
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface IConceptGlosaAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un concepto de glosas
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    Function GetConceptGlosasById(id As Integer) As ConceptGlosas

    ''' <summary>
    ''' Consulta por código
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    Function GetConceptGlosasByCode(code As String) As ConceptGlosas

    ''' <summary>
    ''' Guarda o actualiza un concepto de glosas
    ''' </summary>
    ''' <param name="conceptGlosa"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveConceptGlosas(conceptGlosa As ConceptGlosas, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ConceptGlosas)

    ''' <summary>
    ''' Elimina un concepto de glosas
    ''' </summary>
    ''' <returns>ActionResult</returns>
    Function DeleteConceptGlosas(conceptGlosa As ConceptGlosas, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <remarks></remarks>
    Function ChangeConceptGlosas(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ConceptGlosas)
End Interface
