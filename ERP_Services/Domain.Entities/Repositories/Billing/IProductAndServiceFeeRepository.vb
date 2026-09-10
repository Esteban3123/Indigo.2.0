Imports Domain.Base
Imports Domain.Entities

Public Interface IProductAndServiceFeeRepository
    Inherits IRepository(Of ProductAndServiceFee)

    ''' <summary>
    ''' obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductAndServiceFeeByCode(code As String) As ProductAndServiceFee

    ''' <summary>
    ''' obtiene una tarifa de productos y servicios por codigo
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetProductAndServiceFeeById(Id As Integer) As ProductAndServiceFee

    ''' <summary>
    ''' Copiar y pegar para los productos
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetProductFeeDetailFromFile(xmlObject As String) As List(Of SP_CopyAndPasteProductFeeDetails_Result)

    ''' <summary>
    ''' Copiar y pegar para los servicios
    ''' </summary>
    ''' <param name="XmlObject"></param>
    ''' <returns></returns>
    Function SetServiceProductFeeDetailFromFile(xmlObject As String) As List(Of SP_CopyAndPasteServiceFeeDetails_Result)

    ''' <summary>
    ''' Guarda una tarifa de productos y servicios
    ''' </summary>
    ''' <param name="EntityXml"></param>
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Function SP_SaveProductAndServiceFee(EntityXml As String, codeUser As String) As SP_SaveProductAndServiceFee_Result

End Interface
