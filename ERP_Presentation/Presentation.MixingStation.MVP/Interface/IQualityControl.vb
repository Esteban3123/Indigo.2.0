'***********************************************************************
' Assembly         : Presentacion.MixingStation.MVP
' Author           : Andres Felipe Aros
' Created          : 07-10-2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports DevExpress.Xpo
#End Region

Public Interface IQualityControl
    Inherits ICrudBase

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el datasource de Causa de Reproceso
    ''' </summary>
    Property ReprocessingCauseDatasource As XPInstantFeedbackSource

#End Region
End Interface
