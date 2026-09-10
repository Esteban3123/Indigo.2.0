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
Public Interface IFixedAssetResponsibleService

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function ListAllResponsible(audit As AuditMessage) As List(Of Domain.Entities.FixedAssetResponsible)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetResponsibleByCode(Code As String) As FixedAssetResponsible

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Responsible">Objeto FixedAssetResponsible</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SaveResponsible(Responsible As Domain.Entities.FixedAssetResponsible, idSequense As Int64, audit As AuditMessage) As ActionResult(Of FixedAssetResponsible)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Responsible">Objeto FixedAssetResponsible</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function DeleteResponsible(Responsible As Domain.Entities.FixedAssetResponsible, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetFunctionalUnitByCode(Code As String) As FunctionalUnit

    <OperationContract()>
    Function GetFuncionalUnitByIdUser(IdResponsible As Integer) As List(Of ResponsibleFunctionalUnit)

    <OperationContract()>
    Function ChangeStateFixedAssetResponsible(Code As String, State As Boolean, audit As AuditMessage) As ActionResult(Of FixedAssetResponsible)

End Interface
