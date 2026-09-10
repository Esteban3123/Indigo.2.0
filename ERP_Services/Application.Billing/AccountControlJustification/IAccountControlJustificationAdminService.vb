#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAccountControlJustificationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion para guardar 
    ''' </summary>
    ''' <param name="_listAccountControlJustification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Function SaveAccountControlJustification(_listAccountControlJustification As List(Of AccountControlJustification), audit As AuditMessage) As ActionResult

End Interface
