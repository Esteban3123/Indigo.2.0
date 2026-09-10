'***********************************************************************
' Assembly         : DistributedServices.MixinStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05/08/2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.ServiceModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base


<ServiceContract()>
Public Interface IMixingStationServiceProductionLine

    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    <OperationContract()>
    Function ListAllProductionLine(audit As AuditMessage) As List(Of ProductionLine)

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function SaveProductionLine(ByVal productionLine As ProductionLine, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLineUnitDoseType">The identifier.</param>
    <OperationContract()>
    Function SaveProductionLineUnitDoseType(ByVal productionLineUnitDoseType As ProductionLineUnitDoseType) As ActionResult(Of ProductionLineUnitDoseType)

    ''' <summary>
    ''' Elimina un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function DeleteProductionLine(ByVal productionLine As ProductionLine, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    <OperationContract()>
    Function UpdateProductionLine(ByVal code As String, ByVal state As Boolean, audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    <OperationContract()>
    Function GetProductionLine(ByVal code As String, audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    <OperationContract()>
    Function GetProductionLineId(id As Integer, audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="Id_ProductionLine">The identifier.</param>
    <OperationContract()>
    Function ListAllProductionLineUnitDoseType(Id_ProductionLine As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Validaciones para guardar una linea de produccion
    ''' </summary>
    ''' <param name="productionLineId"></param>
    ''' <returns></returns>
    <OperationContract>
    Function ValidateProductionLineUnitDoseType(productionLineId As Integer, unitDoseTypeId As Integer) As ActionResult

End Interface
