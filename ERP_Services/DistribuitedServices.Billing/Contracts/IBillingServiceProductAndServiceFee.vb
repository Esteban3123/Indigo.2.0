#Region "Imports"

Imports System.ServiceModel
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities

#End Region

<ServiceContract()>
Public Interface IBillingServiceProductAndServiceFee

#Region "Methods"

    ''' <summary>
    ''' obtiene una tarifa de productos y servicios por Id
    ''' </summary>
    ''' <param name="Id">Código</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductAndServiceFeeById(Id As Integer, audit As AuditMessage) As Domain.Entities.ProductAndServiceFee

    ''' <summary>
    ''' obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    ''' <param name="code">Código</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductAndServiceFeeByCode(code As String, audit As AuditMessage) As Domain.Entities.ProductAndServiceFee

    ''' <summary>
    ''' Sets details fee products from Copy and Paste.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetProductFeeDetailFromCopyandPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of Domain.Entities.ProductFeeDetail))

    ''' <summary>
    ''' Sets details fee product from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetProductFeeDetailFromFile(data As List(Of ImportFileRow)) As ActionResult(Of List(Of Domain.Entities.ProductFeeDetail))

    ''' <summary>
    ''' Sets details invoice from Copy and Paste.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetServiceFeeDetailFromCopyandPaste(data As List(Of List(Of String))) As ActionResult(Of List(Of Domain.Entities.ServiceFeeDetail))

    ''' <summary>
    ''' Sets details fee services from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetServiceFeeDetailFromFile(data As List(Of ImportFileRow)) As ActionResult(Of List(Of Domain.Entities.ServiceFeeDetail))

    ''' <summary>
    ''' Guarda una tarifa de productos y servicios
    ''' </summary>
    ''' <param name="ProductAndServiceFee"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SaveProductAndServiceFee(ProductAndServiceFee As Domain.Entities.ProductAndServiceFee, audit As AuditMessage) As ActionResult(Of Domain.Entities.ProductAndServiceFee)

    ''' <summary>
    ''' Actualiza el estado de una tarifa de productos y servicios
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function UpdateStateProductAndServiceFee(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Domain.Entities.ProductAndServiceFee)

#End Region

End Interface
