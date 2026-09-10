Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IPositionLevelAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los niveles.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllPositionLevel() As List(Of PositionLevel)

    ''' <summary>
    ''' Elimina un nivel
    ''' </summary>
    ''' <param name="Detail">el nivel</param>
    ''' <returns></returns>
    Function DeletePositionLevel(ByVal Detail As PositionLevel, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' graba un nivel
    ''' </summary>
    ''' <param name="Detail">el nivel</param>
    ''' <returns></returns>
    Function SavePositionLevel(ByVal Detail As PositionLevel, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PositionLevel)
    ''' <summary>
    ''' consulta un nivel
    ''' </summary>
    ''' <param name="codeConcept">el codigo del nivel</param>
    ''' <returns></returns>
    Function GetPositionLevel(ByVal codeConcept As String) As PositionLevel


    Function ChangeStatePositionLevel(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of PositionLevel)

End Interface
