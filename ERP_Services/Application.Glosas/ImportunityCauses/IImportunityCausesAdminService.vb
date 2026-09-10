Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IImportunityCausesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene un concepto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetImportunityCausesByCode(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Obtiene un Concepto por ID
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function GetImportunityCausesById(ByVal Id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Guarda o Actualiza un registro
    ''' </summary>
    ''' <param name="ImportunityCauses">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">entidad Vacio</exception>
    Function SaveImportunityCauses(ByVal ImportunityCauses As ImportunityCauses, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function ChangeStateImportunityCauses(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ImportunityCauses)

    ''' <summary>
    ''' Elimina  un registro
    ''' </summary>
    ''' <param name="ImportunityCauses">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    ''' <exception cref="System.ArgumentNullException">company Vacio</exception>
    Function DeleteImportunityCauses(ByVal ImportunityCauses As ImportunityCauses, ByVal audit As AuditMessage) As ActionResult(Of ImportunityCauses)

End Interface
