#Region "Imports"

Imports Application.Billing
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Microsoft.Practices.Unity
Imports Domain.Entities

#End Region

Partial Class BillingService
    Implements IBillingServiceProductAndServiceFee

    ''' <summary>
    ''' obtiene una tarifa de productos y servicios por Id
    ''' </summary>
    ''' <param name="Id">Código</param>
    ''' <returns></returns>
    Public Function GetProductAndServiceFeeById(Id As Integer, audit As AuditMessage) As Domain.Entities.ProductAndServiceFee Implements IBillingServiceProductAndServiceFee.GetProductAndServiceFeeById
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.GetProductAndServiceFeeById(Id, audit)
        End Using
    End Function

    ''' <summary>
    ''' obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <returns></returns>
    Public Function GetProductAndServiceFeeByCode(code As String, audit As AuditMessage) As Domain.Entities.ProductAndServiceFee Implements IBillingServiceProductAndServiceFee.GetProductAndServiceFeeByCode
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.GetProductAndServiceFeeByCode(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Sets details products fee from Copy and Paste.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetProductFeeDetailFromCopyandPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of Domain.Entities.ProductFeeDetail)) Implements IBillingServiceProductAndServiceFee.SetProductFeeDetailFromCopyandPaste
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.SetProductFeeDetailFromFile(Nothing, data)
        End Using
    End Function

    ''' <summary>
    ''' Sets details Productfee from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetProductFeeDetailFromFile(data As List(Of ImportFileRow)) As ActionResult(Of List(Of Domain.Entities.ProductFeeDetail)) Implements IBillingServiceProductAndServiceFee.SetProductFeeDetailFromFile
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.SetProductFeeDetailFromFile(data, Nothing)
        End Using
    End Function

    ''' <summary>
    ''' Sets details service fee from Copy and Paste.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetServiceFeeDetailFromCopyandPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of Domain.Entities.ServiceFeeDetail)) Implements IBillingServiceProductAndServiceFee.SetServiceFeeDetailFromCopyandPaste
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.SetServicesFeeDetailFromFile(Nothing, data)
        End Using
    End Function

    ''' <summary>
    ''' Sets details service fee from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    Public Function SetServiceFeeDetailFromFile(data As List(Of ImportFileRow)) As ActionResult(Of List(Of Domain.Entities.ServiceFeeDetail)) Implements IBillingServiceProductAndServiceFee.SetServiceFeeDetailFromFile
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.SetServicesFeeDetailFromFile(data, Nothing)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una tarifa de productos y servicios
    ''' </summary>
    ''' <param name="ProductAndServiceFee"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveProductAndServiceFee(ProductAndServiceFee As Domain.Entities.ProductAndServiceFee, audit As AuditMessage) As ActionResult(Of Domain.Entities.ProductAndServiceFee) Implements IBillingServiceProductAndServiceFee.SaveProductAndServiceFee
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.SaveProductAndServiceFee(ProductAndServiceFee, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un registro de tarifa de productos y servicios
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="session"></param>
    ''' <returns></returns>
    Public Function UpdateStateProductAndServiceFee(code As String, state As Boolean, session As AuditMessage) As ActionResult(Of Domain.Entities.ProductAndServiceFee) Implements IBillingServiceProductAndServiceFee.UpdateStateProductAndServiceFee
        Using service As IProductAndServiceFeeAdminService = Container.Current.Resolve(Of IProductAndServiceFeeAdminService)()
            Return service.UpdateStateProductAndServiceFee(code, state, session)
        End Using
    End Function

End Class
