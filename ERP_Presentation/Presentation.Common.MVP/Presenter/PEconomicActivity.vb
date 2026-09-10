'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base

Public Class PEconomicActivity

    ''' <summary>
    ''' Vista de centro de costos
    ''' </summary>
    Private _View As IEconomicActivity

    ''' <summary>
    ''' Instancia la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' construye el presentador de centro de costos
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef view As IEconomicActivity)
        If (view Is Nothing = True) Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _View = view
    End Sub
End Class
