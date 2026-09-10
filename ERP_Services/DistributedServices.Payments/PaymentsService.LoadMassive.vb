Imports Application.Payments
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

Partial Public Class PaymentsService

    ''' <summary>
    ''' Obtiene un determinado cargue masivo de cuentas por pagar
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    Function GetLoadMassive(ByVal code As String, ByVal audit As AuditMessage) As LoadMassive Implements IPaymentsLoadMassive.GetLoadMassive
        Using service As ILoadMassiveAdminService = Container.Current.Resolve(Of ILoadMassiveAdminService)()
            Return service.GetLoadMassive(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Confirma un cargue masivo de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    Function SaveLoadMassive(ByVal loadMassive As LoadMassive, dataBills As List(Of Domain.Base.Entities.ImportFileRow), dataDetails As List(Of LoadMassiveAccountPayableDetail), ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = False) As ActionResult(Of LoadMassive) Implements IPaymentsLoadMassive.SaveLoadMassive
        Using service As ILoadMassiveAdminService = Container.Current.Resolve(Of ILoadMassiveAdminService)()
            Return service.SaveLoadMassive(loadMassive, dataBills, dataDetails, audit, withCommit)
        End Using
    End Function

End Class