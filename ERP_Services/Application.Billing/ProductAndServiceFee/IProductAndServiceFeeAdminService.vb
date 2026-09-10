
#Region "Imports"

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base

#End Region

Public Interface IProductAndServiceFeeAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda un registro de tarifa de productos y servicios
    ''' </summary>
    <OperationContract()>
    Function SaveProductAndServiceFee(ProductAndServiceFee As ProductAndServiceFee, audit As AuditMessage) As ActionResult(Of ProductAndServiceFee)

    ''' <summary>
    ''' Actualiza el estado de un registro de tarifa de productos y servicios
    ''' </summary>
    <OperationContract()>
    Function UpdateStateProductAndServiceFee(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductAndServiceFee)

    ''' <summary>
    ''' Obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    <OperationContract()>
    Function GetProductAndServiceFeeByCode(code As String, audit As AuditMessage) As ProductAndServiceFee

    ''' <summary>
    ''' Obtiene una tarifa de productos y servicios por Id
    ''' </summary>
    <OperationContract()>
    Function GetProductAndServiceFeeById(Id As Integer, audit As AuditMessage) As ProductAndServiceFee

    ''' <summary>
    ''' Sets details fee product from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetProductFeeDetailFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As ActionResult(Of List(Of ProductFeeDetail))

    ''' <summary>
    ''' Sets details fee services from file.
    ''' </summary>
    ''' <param name="data"></param>
    ''' <returns></returns>
    <OperationContract()>
    Function SetServicesFeeDetailFromFile(dataimport As List(Of ImportFileRow), data As List(Of List(Of String))) As ActionResult(Of List(Of ServiceFeeDetail))

End Interface