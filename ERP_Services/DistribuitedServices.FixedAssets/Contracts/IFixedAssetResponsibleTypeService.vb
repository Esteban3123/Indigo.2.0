'***********************************************************************
' Assembly         : DistributedServices.FixedAsset
' Author           : Daniel Eduardo Arévalo
' Created          : 04-01-2016
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
Public Interface IFixedAssetResponsibleTypeService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllResponsibleType(audit As AuditMessage) As List(Of Domain.Entities.ResponsibleType)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetResponsibleTypeByCode(Code As String) As ResponsibleType

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="ResponsibleType">Objeto ResponsibleType</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveResponsibleType(ResponsibleType As Domain.Entities.ResponsibleType, idSequense As Int64, audit As AuditMessage) As ActionResult(Of ResponsibleType)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="ResponsibleType">Objeto ResponsibleType</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteResponsibleType(ResponsibleType As Domain.Entities.ResponsibleType, audit As AuditMessage) As ActionResult


    ''' <summary>
    ''' Función para Actualizar estado
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ChangeFixedAssetResponsibleTypeStatus(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of ResponsibleType)

End Interface
