'***********************************************************************
' Assembly         : Application.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 06-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IProductionLineService
    Inherits IDisposable
    ''' <summary>
    ''' Lista todos los tipos de dosis unitaria
    ''' </summary>
    ''' <returns>Lista de tipos de dosis unitaria</returns>
    Function ListAllProductionLine(ByVal audit As AuditMessage) As List(Of ProductionLine)

    ''' <summary>
    ''' Guarda una lina de produccion
    ''' </summary>
    ''' <param name="productionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function SaveProductionLine(ByVal productionLine As ProductionLine, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLineUnitDoseType">The identifier.</param>
    Function SaveProductionLineUnitDoseType(ByVal productionLineUnitDoseType As ProductionLineUnitDoseType) As ActionResult(Of ProductionLineUnitDoseType)

    ''' <summary>
    ''' Elimina un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function DeleteProductionLine(ByVal productionLine As ProductionLine, ByVal audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Actualiza un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <param name="state">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function UpdateStateProductionLine(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit">The identifier.</param>
    ''' <returns></returns>
    Function GetProductionLine(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function GetProductionLineId(id As Integer, ByVal audit As AuditMessage) As ActionResult(Of ProductionLine)

    ''' <summary>
    ''' Obtiene los tipos de dosis unitarias
    ''' </summary>
    ''' <param name="Id_ProductionLine">The identifier.</param>
    ''' <param name="audit">The identifier.</param>
    Function ListAllProductionLineUnitDoseType(ByVal Id_ProductionLine As Integer, ByVal audit As AuditMessage) As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Validaciones para guardar una linea de produccion
    ''' </summary>
    ''' <param name="productionLineId"></param>
    ''' <returns></returns>
    Function ValidateProductionLineUnitDoseType(productionLineId As Integer, unitDoseTypeId As Integer) As ActionResult
End Interface