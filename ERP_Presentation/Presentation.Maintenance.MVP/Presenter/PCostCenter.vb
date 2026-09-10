'***********************************************************************
' Assembly         : Presentation.Common.MVP
' Author           : Cristhian Mauricio Salazar
' Created          : 16-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base

Public Class PCostCenter

    ''' <summary>
    ''' Vista de centro de costos
    ''' </summary>
    Private _View As ICostCenter

    ''' <summary>
    ''' Instancia la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' construye el presentador de centro de costos
    ''' </summary>
    ''' <param name="view"></param>
    ''' <remarks></remarks>
    Public Sub New(ByRef view As ICostCenter)
        If (view Is Nothing = True) Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        _View = view
    End Sub

    Public Async Sub GetSequence()
        Using model As New MBlockRecordAndSequenseMaintenance(_View.MyTag.ToString())
            Me._View.Sequence = Await model.GetSequense()
        End Using
    End Sub
End Class
