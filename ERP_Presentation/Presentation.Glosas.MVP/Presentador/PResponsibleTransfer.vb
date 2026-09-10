'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
#End Region

''' <summary>
''' Esta presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PResponsibleTransfer

#Region "Builder and fields"
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz.
    ''' </summary>
    Dim View As IResponsibleTransfer
    ''' <summary>
    ''' Variable utilizada para instanciar el modelo.
    ''' </summary>
    Dim Model As MResponsibleTransfer
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton.
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IResponsibleTransfer)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Metodo para inicializar valores en la presentación
    ''' </summary>
    Public Async Sub Initialize(TagForm As String)
        Model = New MResponsibleTransfer(TagForm)
        Me.View.DataSourceResponsibleGle = Await Model.GetResponsibleALL()
    End Sub


#End Region

End Class