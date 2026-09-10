'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 09-06-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IFixedAssetStatusService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllFixedAssetStatusAsset(audit As AuditMessage) As List(Of Domain.Entities.FixedAssetStatusAsset)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFixedAssetStatusAssetByCode(Code As String) As FixedAssetStatusAsset

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="FixedAssetStatusAsset">Objeto ResponsibleType</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveFixedAssetStatusAsset(FixedAssetStatusAsset As Domain.Entities.FixedAssetStatusAsset, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetStatusAsset)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="FixedAssetStatusAsset">Objeto ResponsibleType</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteFixedAssetStatusAsset(FixedAssetStatusAsset As Domain.Entities.FixedAssetStatusAsset, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeFixedAssetStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetStatusAsset)

End Interface
