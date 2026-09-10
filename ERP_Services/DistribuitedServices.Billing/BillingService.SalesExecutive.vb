
#Region "Imports"
Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Partial Class BillingService

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por Id
    ''' </summary>
    '''<param name="Id">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetSalesExecutiveById(Id As Integer, audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutive.GetSalesExecutiveById
        Using service As ISalesExecutiveAdminService = Container.Current.Resolve(Of ISalesExecutiveAdminService)()
            Return service.GetSalesExecutiveById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un ejecutivo de ventas por codigo
    ''' </summary>
    '''<param name="Code">Código del Rubro</param>
    ''' <returns></returns>
    Public Function GetSalesExecutiveByCode(Code As String, audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutive.GetSalesExecutiveByCode
        Using service As ISalesExecutiveAdminService = Container.Current.Resolve(Of ISalesExecutiveAdminService)()
            Return service.GetSalesExecutiveByCode(Code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda o Actualiza un ejecutivo de ventas
    ''' </summary>
    ''' <param name="SalesExecutive">la entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function SaveSalesExecutive(SalesExecutive As SalesExecutive, audit As AuditMessage, idSequence As Int64) As ActionResult(Of SalesExecutive) Implements ISalesExecutive.SaveSalesExecutive
        Using service As ISalesExecutiveAdminService = Container.Current.Resolve(Of ISalesExecutiveAdminService)()
            Return service.SaveSalesExecutive(SalesExecutive, audit, idSequence)
        End Using
    End Function
    ''' <summary>
    ''' Metodo para cambiar de estado la entidad
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ChangeStateSalesExecutive(Id As Integer, state As Boolean, audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutive.ChangeStateSalesExecutive
        Using service As ISalesExecutiveAdminService = Container.Current.Resolve(Of ISalesExecutiveAdminService)()
            Return service.ChangeStateSalesExecutive(Id, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un registro
    ''' </summary>
    ''' <param name="SalesExecutive">La entidad</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Public Function DeleteSalesExecutive(SalesExecutive As SalesExecutive, audit As AuditMessage) As ActionResult(Of SalesExecutive) Implements ISalesExecutive.DeleteSalesExecutive
        Using service As ISalesExecutiveAdminService = Container.Current.Resolve(Of ISalesExecutiveAdminService)()
            Return service.DeleteSalesExecutive(SalesExecutive, audit)
        End Using
    End Function
End Class
