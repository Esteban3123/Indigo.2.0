'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Public Interface IStudyCenterAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista Todos los Centros de Estudio
    ''' </summary>
    ''' <returns>Centros de Estudio</returns>
    ''' <remarks></remarks>
    Function ListAllStudyCenter() As List(Of StudyCenter)

    ''' <summary>
    ''' Elimina un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteStudyCenter(ByVal studyCenter As StudyCenter, ByVal audit As AuditMessage) As ActionMessageResult(Of StudyCenter)

    ''' <summary>
    ''' Almacena o Actualiza un Centro de Estudio
    ''' </summary>
    ''' <param name="studyCenter">Centro de Estudio</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveStudyCenter(ByVal studyCenter As StudyCenter, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un Centro de Estuio
    ''' </summary>
    ''' <param name="code">Código del Centro de Estudio</param>
    ''' <returns>Centro de Estudio</returns>
    ''' <remarks></remarks>
    Function GetStudyCenter(ByVal code As String) As StudyCenter

End Interface
