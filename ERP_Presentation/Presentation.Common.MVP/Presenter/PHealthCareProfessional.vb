'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 11-02-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Security.MVP
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal de profesionales
''' </summary>
Public Class PHealthCareProfessional
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IHealthCareProfessional

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IHealthCareProfessional)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

End Class
