Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ServiceModel

<ServiceContract()>
Public Interface IPaymentsLoadMassive

    ''' <summary>
    ''' Obtiene un determinado cargue masivo de cuentas por pagar
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetLoadMassive(ByVal code As String, ByVal audit As AuditMessage) As LoadMassive

    ''' <summary>
    ''' Guarda o Confirma un cargue masivo de cuentas por pagar
    ''' </summary>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveLoadMassive(ByVal loadMassive As LoadMassive, dataBills As List(Of Domain.Base.Entities.ImportFileRow), dataDetails As List(Of LoadMassiveAccountPayableDetail), ByVal audit As AuditMessage, Optional ByVal withCommit As Boolean = False) As ActionResult(Of LoadMassive)

End Interface