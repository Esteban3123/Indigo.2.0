'***********************************************************************
' Assembly         : DistributedServices.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ServiceModel
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

<ServiceContract()> _
Public Interface IPartsAccesoriesConsumablesService

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function ListAllPartsAccesoriesConsumables(session As SessionValues) As List(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function GetPartsAccesoriesConsumablesByCode(Code As String, session As SessionValues) As PartsAccesoriesConsumables

    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()>
    Function SavePartsAccesoriesConsumables(Empresa As String, PartsAccesoriesConsumables As Domain.Maintenance.Entities.PartsAccesoriesConsumables, audit As Infrastructure.CrossCutting.Base.AuditMessage) As ActionResult(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="session"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    <OperationContract()> _
    Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As PartsAccesoriesConsumables, session As SessionValues) As Boolean

End Interface
