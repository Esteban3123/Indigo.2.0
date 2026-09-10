'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Diego A. Roldán Lozano
' Created          : 2021-09-16
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Public Class PMixingStationSetting

    ''' <summary>
    ''' view
    ''' </summary>
    Private _iview As IMixingStationSetting

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="iview"></param>
    Public Sub New(iview As IMixingStationSetting)
        _iview = iview
    End Sub
End Class
