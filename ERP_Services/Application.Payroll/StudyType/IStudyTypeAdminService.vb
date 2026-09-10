Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IStudyTypeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todos los tipos de estudio
    ''' </summary>
    ''' <returns>Lista de tipos de estudio</returns>
    Function ListAllStudyType() As List(Of StudyType)

    ''' <summary>
    ''' Elimina un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    Function DeleteStudyType(ByVal studyType As StudyType, ByVal audit As AuditMessage) As ActionMessageResult(Of StudyType)

    ''' <summary>
    ''' Guarda o edita un tipo de estudio
    ''' </summary>
    ''' <param name="studyType">Tipo de estudio</param>
    ''' <returns></returns>
    Function SaveStudyType(ByVal studyType As StudyType, ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' Obtiene un tipo de estudio especifico
    ''' </summary>
    ''' <param name="code">Código de el tipo de estudio</param>
    ''' <returns> Tipo de estudio</returns>
    Function GetStudyType(ByVal code As String) As StudyType

End Interface
