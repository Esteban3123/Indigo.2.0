#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Servicio de guardado
    ''' </summary>
    ''' <param name="_listAccountControlJustification"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveAccountControlJustification(_listAccountControlJustification As List(Of AccountControlJustification), audit As AuditMessage) As ActionResult Implements IBillingServiceAccountControlJustification.SaveAccountControlJustification
        Using service As IAccountControlJustificationAdminService = Container.Current.Resolve(Of IAccountControlJustificationAdminService)()
            Return service.SaveAccountControlJustification(_listAccountControlJustification, audit)
        End Using
    End Function

End Class
