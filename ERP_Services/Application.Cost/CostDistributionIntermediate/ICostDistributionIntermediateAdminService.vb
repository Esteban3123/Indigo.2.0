'***********************************************************************
' Assembly         : Application.Cost
' Author           : Diego Andrés Roldán Lozano
' Created          : 08-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ICostDistributionIntermediateAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Guarda una distribución intermedia
    ''' </summary>
    Function SaveDistributionIntermediate(ByVal distributionIntermediate As CostDistributionIntermediate, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Elimina una distribución intermedia
    ''' </summary>
    Function DeleteDistributionIntermediate(ByVal distributionIntermediate As CostDistributionIntermediate, ByVal audit As AuditMessage) As ActionResult

    ' ''' <summary>
    ' ''' Actualiza el estado del registro
    ' ''' </summary>
    Function UpdateStateDistributionIntermediate(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por codigo
    ''' </summary>
    Function GetDistributionIntermediate(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of CostDistributionIntermediate)

    ''' <summary>
    ''' Obtiene una distribucion Intermedia por id
    ''' </summary>
    Function GetDistributionIntermediateById(id As Integer) As CostDistributionIntermediate

    ''' <summary>
    ''' Lista los periodos anteriores que contienen datos al año y mes dados
    ''' </summary>
    Function ListPeriodWithDataByMaximumPeriodDistributionIntermediate(ByVal year As Integer, ByVal month As Integer) As List(Of String)

    ''' <summary>
    ''' Lista las distribuciones Intermedia por año y mes
    ''' </summary>
    Function ListDistributionIntermediateByYearMonth(ByVal year As Integer, ByVal month As Integer) As List(Of CostDistributionIntermediate)

End Interface