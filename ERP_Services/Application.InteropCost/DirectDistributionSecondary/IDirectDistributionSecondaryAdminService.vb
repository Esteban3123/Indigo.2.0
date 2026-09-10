'***********************************************************************
' Assembly         : Application.InteropCost
' Author           : Diego Andrés Roldán Lozano
' Created          : 22-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface IDirectDistributionSecondaryAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Obtiene un gasto general
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <returns></returns>

    Function GetDirectDistributionSecondary(ByVal code As String, ByVal audit As AuditMessage) As DirectDistributionSecondary

    ''' <summary>
    ''' Obtiene un gasto general por id
    ''' </summary>
    ''' <param name="id">The identifier.</param>
    ''' <returns></returns>
    Function GetDirectDistributionSecondaryById(id As Integer) As DirectDistributionSecondary

    ''' <summary>
    ''' Guarda un centro de produccion
    ''' </summary>
    Function SaveDirectDistributionSecondary(ByVal DirectDistributionSecondary As DirectDistributionSecondary, ListUpdateIds As List(Of Integer), ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of DirectDistributionSecondary)

    ''' <summary>
    ''' Obtiene el producido de los centros de produccion logisticos
    ''' </summary>
    ''' <param name="ProductionCenterId">Id del centro de Produccion</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDataImportLogisticProductionCenterById(ByVal ProductionCenterId As Integer) As ActionResult(Of List(Of LogisticsProductionCenterRecordDetail))
End Interface
