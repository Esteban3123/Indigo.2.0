'***********************************************************************
' Assembly         : DistributedService.MixingStation
' Author           : Andres Alarcon
' Created          : 21-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Application.MixingStation
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Class MixingStationService
    Implements IMixingStationServicelineClearanceCriteria

    ''' <summary>
    ''' Guarda un criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function SaveLineClearanceCriteria(_lineClearanceCriteria As LineClearanceCriteria, idSequence As Int64, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria) Implements IMixingStationServicelineClearanceCriteria.SaveLineClearanceCriteria
        Using service As ILineClearanceCriteriaService = Container.Current.Resolve(Of ILineClearanceCriteriaService)()
            Return service.SaveLineClearanceCriteria(_lineClearanceCriteria, audit, idSequence)
        End Using
    End Function

    ''' <summary>
    ''' Elimina un criterio de despeje de linea
    ''' </summary>
    ''' <param name="_lineClearanceCriteria"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function DeleteLineClearanceCriteria(_lineClearanceCriteria As LineClearanceCriteria, audit As AuditMessage) As ActionResult Implements IMixingStationServicelineClearanceCriteria.DeleteLineClearanceCriteria
        Using service As ILineClearanceCriteriaService = Container.Current.Resolve(Of ILineClearanceCriteriaService)()
            Return service.DeleteLineClearanceCriteria(_lineClearanceCriteria, audit)
        End Using
    End Function

    ''' <summary>
    ''' Actualiza el estado de un criterio de despeje de linea
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function UpdateLineClearanceCriteria(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria) Implements IMixingStationServicelineClearanceCriteria.UpdateLineClearanceCriteria
        Using service As ILineClearanceCriteriaService = Container.Current.Resolve(Of ILineClearanceCriteriaService)()
            Return service.UpdateStateLineClearanceCriteria(code, state, audit)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un criterio de despeje de linea mediante el codigo
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    Public Function GetLineClearanceCriteriaByCode(code As String, audit As AuditMessage) As ActionResult(Of LineClearanceCriteria) Implements IMixingStationServicelineClearanceCriteria.GetLineClearanceCriteriaByCode
        Using service As ILineClearanceCriteriaService = Container.Current.Resolve(Of ILineClearanceCriteriaService)()
            Return service.GetLineClearanceCriteriaByCode(code, audit)
        End Using
    End Function

End Class
