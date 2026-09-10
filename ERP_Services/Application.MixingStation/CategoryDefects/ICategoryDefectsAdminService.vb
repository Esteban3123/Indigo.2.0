'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Andres Alarcon 
' Created          : 26/08/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities


Public Interface ICategoryDefectsAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda o Actualiza un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveCategoryDefects(ByVal CategoryDefects As DefectClassificationGroup, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DefectClassificationGroup)

    ''' <summary>
    ''' Elimina un grupo
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteCategoryDefects(ByVal CategoryDefects As DefectClassificationGroup, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Obtiene un grupo por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetCategoryDefects(ByVal code As String, ByVal audit As AuditMessage) As DefectClassificationGroup

    ''' <summary>
    ''' Obtiene un grupo uvr por id
    ''' </summary>
    ''' <returns></returns>
    Function GetCategoryDefectsById(ByVal id As Integer) As DefectClassificationGroup

End Interface
