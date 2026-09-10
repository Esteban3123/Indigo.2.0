Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository

Public Class PRawMaterialDevolution

    ''' <summary>
    ''' interfaz de la vista
    ''' </summary>
    Private _iview As IRawMaterialDevolution

    ''' <summary>
    ''' constructor
    ''' </summary>
    ''' <param name="iview"></param>
    Public Sub New(iview As IRawMaterialDevolution)
        _iview = iview
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Function GetSequence() As Task
        Using model As New MBlockRecordAndSequenceMixingStation(Me._iview.MyTag)
            Me._iview.Sequence = Await model.GetSequence()
        End Using
    End Function

    ''' <summary>
    ''' Lista todas las campañas
    ''' </summary>
    ''' <returns></returns>
    Public Function ListAllCampaignsXPOByStatus(status As Byte()) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
            .MixingStationService.ListXPInstantFeedbackSource(Of CampaignDetailXpo)(filter:=$"CampaignStatus In ({String.Join(",", status)})")
    End Function
End Class
