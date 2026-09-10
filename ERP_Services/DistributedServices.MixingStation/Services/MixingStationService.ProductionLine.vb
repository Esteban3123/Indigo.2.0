'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Judy Andrea Díaz Reyes
' Created          : 21-05-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
Imports System.ServiceModel
Imports DistributedServices.MixingStation
#End Region

Partial Class MixingStationService
    Implements IMixingStationServiceProductionLine

    ''' <summary>
    ''' Actualiza un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllProductionLine(audit As AuditMessage) As List(Of ProductionLine) Implements IMixingStationServiceProductionLine.ListAllProductionLine
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.ListAllProductionLine(audit)
        End Using
    End Function

    ''' <summary>
    ''' Guarda un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLineUnitDoseType"></param>
    Public Function SaveProductionLineUnitDoseType(productionLineUnitDoseType As ProductionLineUnitDoseType) As ActionResult(Of ProductionLineUnitDoseType) Implements IMixingStationServiceProductionLine.SaveProductionLineUnitDoseType
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.SaveProductionLineUnitDoseType(productionLineUnitDoseType)
        End Using
    End Function

    ''' <summary>
    ''' Guarda una linea de produccion
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveProductionLine(productionLine As ProductionLine, idSequence As Int64, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IMixingStationServiceProductionLine.SaveProductionLine
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.SaveProductionLine(productionLine, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="productionLine"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteProductionLine(productionLine As ProductionLine, audit As AuditMessage) As ActionResult Implements IMixingStationServiceProductionLine.DeleteProductionLine
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.DeleteProductionLine(productionLine, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un tipo de dosis unitaria
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateProductionLine(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IMixingStationServiceProductionLine.UpdateProductionLine
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.UpdateStateProductionLine(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetProductionLine(code As String, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IMixingStationServiceProductionLine.GetProductionLine
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.GetProductionLine(code, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetProductionLineId(id As Integer, audit As AuditMessage) As ActionResult(Of ProductionLine) Implements IMixingStationServiceProductionLine.GetProductionLineId
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.GetProductionLineId(id, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un tipo de dosis unitaria por id
    ''' </summary>
    ''' <param name="Id_ProductionLine">The identifier.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function ListAllProductionLineUnitDoseType(Id_ProductionLine As Integer, audit As AuditMessage) As List(Of Tuple(Of Integer, String)) Implements IMixingStationServiceProductionLine.ListAllProductionLineUnitDoseType
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.ListAllProductionLineUnitDoseType(Id_ProductionLine, audit)
        End Using
    End Function

    ''' <summary>
    ''' Valida linea de produccion
    ''' </summary>
    ''' <param name="productionLineId"></param>
    ''' <returns></returns>
    Public Function ValidateProductionLineUnitDoseType(productionLineId As Integer, unitDoseTypeId As Integer) As ActionResult Implements IMixingStationServiceProductionLine.ValidateProductionLineUnitDoseType
        Using service As IProductionLineService = Container.Current.Resolve(Of IProductionLineService)()
            Return service.ValidateProductionLineUnitDoseType(productionLineId, unitDoseTypeId)
        End Using
    End Function
End Class
