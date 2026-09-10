
#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceAccountControlJustification
    ''' <summary>
    ''' contrato para exponer el servicio de guardado
    ''' </summary>
    ''' <param name="_listAccountControlJustification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveAccountControlJustification(_listAccountControlJustification As List(Of AccountControlJustification), audit As AuditMessage) As ActionResult

End Interface
