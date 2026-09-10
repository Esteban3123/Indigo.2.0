'***********************************************************************
' Assembly         : Presentacion.Payroll.MVP
' Author           : Cristhian Mauricio Salazar Narvaez
' Created          : 23-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Common.MVP
Public Class PScheduleTemplate

#Region "Variables y constructores"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IScheduleTemplate

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">IScheduleTemplate</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IScheduleTemplate)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

End Class
