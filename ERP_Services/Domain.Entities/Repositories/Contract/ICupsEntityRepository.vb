'************************************************************
' Assembly         : Domain.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 25/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface ICupsEntityRepository
    Inherits IRepository(Of CupsEntity)

    ''' <summary>
    ''' Obtiene una entidad cups por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsEntity(code As String) As CupsEntity

    ''' <summary>
    ''' Obtiene una entidad cups por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsEntityById(id As Integer, Optional ByVal tracking As Boolean = True) As CUPSEntity

    ''' <summary>
    ''' Obtiene una entidad cups por id con asNoTracking y sin agregados
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetCupsEntityByIdSimple(id As Integer) As CUPSEntity

    ''' <summary>
    ''' Gets the cups entity by identifier includes.
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="includes">The includes.</param>
    ''' <returns></returns>
    Function GetCupsEntityByIdIncludes(id As Integer, includes() As String) As CUPSEntity

    ''' <summary>
    ''' Permite guardar y actualizar los cups
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SP_SaveCupsEntity(Xml As String, CodeUser As String) As SP_SaveCupsEntity_Result

    ''' <summary>
    ''' Valida la descripción que se va a eliminar
    ''' </summary>
    ''' <param name="CUPSEntityContractDescriptionId"></param>
    ''' <returns></returns>
    Function SP_ValidateDescriptionsInCrystal(CUPSEntityContractDescriptionId As Integer) As SP_ValidateDescriptionsInCrystal_Result

    ''' <summary>
    ''' Valida el cups cuando se va agregar una descripción
    ''' </summary>
    ''' <param name="CUPSEntityCode"></param>
    ''' <returns></returns>
    Function SP_ValidateCUPSInCrystal(CUPSEntityCode As String) As SP_ValidateCUPSInCrystal_Result

    ''' <summary>
    ''' Obtiene la entidad por id
    ''' </summary>
    ''' <param name="quotationServiceOrderDetailId"></param>
    ''' <returns></returns>
    Function GetQuotationServiceOrderDetailById(quotationServiceOrderDetailId As Integer) As QuotationServiceOrderDetail

    ''' <summary>
    ''' Obtiene el id de la descripción relacionada
    ''' </summary>
    ''' <param name="CupsEntityContractDescriptionId"></param>
    ''' <returns></returns>
    Function GetContractDescriptionIdByCupsEntityContractDescription(CupsEntityContractDescriptionId As Integer) As Integer?

    ''' <summary>
    ''' Obtiene una lista de cups por codigos
    ''' </summary>
    ''' <param name="ListCode"></param>
    ''' <returns></returns>
    Function GetListCupsEntityBycodes(ListCode As List(Of String)) As List(Of CUPSEntity)

    ''' <summary>
    ''' Obtiene los identificadores CUPS cuyo tipo de servicio se encuentra permitido.
    ''' </summary>
    ''' <param name="cupsEntityIds">Identificadores CUPS que participan en la causación.</param>
    ''' <param name="serviceTypes">Tipos de servicio permitidos.</param>
    Function GetCupsEntityIdsByServiceTypes(cupsEntityIds As List(Of Integer), serviceTypes As List(Of Byte)) As List(Of Integer)

    ''' <summary>
    ''' Obtiene  la descripción relacionada
    ''' </summary>
    ''' <param name="CupsCode"></param>
    ''' <returns></returns>
    Function GetListCupsEntityContractDescription(CupsCode As String, CodeDescription As String) As List(Of CUPSEntityContractDescriptions)

    ''' <summary>
    ''' Consulta el id de la descripción relacionada
    ''' </summary>
    ''' <param name="CupsCode"></param>
    ''' <returns></returns>
    Function GetCupsEntityWithContractDescriptions(CupsCode As String) As CUPSEntity

    Function GetListCupsEntityWithContractDescriptions(listCupsCode As List(Of String)) As List(Of CUPSEntity)

End Interface
