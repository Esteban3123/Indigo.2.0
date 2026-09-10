'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 30-01-2016
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
Public Interface IFixedAssetTrademarkService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllTrademark(session As Infrastructure.CrossCutting.Base.SessionValues, audit As AuditMessage) As List(Of Domain.Entities.FixedAssetTrademark)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetTrademarkByCode(Code As String, session As SessionValues) As FixedAssetTrademark

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveTrademark(Trademark As Domain.Entities.FixedAssetTrademark, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetTrademark)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteTrademark(Trademark As Domain.Entities.FixedAssetTrademark, audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeFixedAssetTrademarkStatus(Code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetTrademark)

End Interface
