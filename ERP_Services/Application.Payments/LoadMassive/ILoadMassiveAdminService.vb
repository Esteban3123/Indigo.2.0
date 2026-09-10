Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

Public Interface ILoadMassiveAdminService
    Inherits IDisposable

    Function GetLoadMassive(ByVal code As String, ByVal audit As AuditMessage) As LoadMassive

    Function SaveLoadMassive(ByVal loadMassive As LoadMassive, dataBills As List(Of Domain.Base.Entities.ImportFileRow), dataDetails As List(Of LoadMassiveAccountPayableDetail), ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = False) As ActionResult(Of LoadMassive)

End Interface