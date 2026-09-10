'***********************************************************************
' Assembly         : Presentacion.Common.MVP
' Author           : Jose Luis Rojas
' Created          : 11-04-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.CloudAgent.IndigoReference.Common
Imports Presentation.Accounting.MVP
#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PCountry

#Region "Variables and constructor"

    ''' <summary>
    ''' Variable para instanciar la interfaz
    ''' </summary>
    Dim view As ICountry

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByVal iview As ICountry)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.view = iview
        End If
    End Sub

    Public Async Sub GetSequense()
        Using model As New MStatementFolio(Me.view.MyTag)
            Me.view.Sequence = Await model.GetSequense()
        End Using
    End Sub

    Public Async Sub LoadDefinitionLayout()
        Await Me.view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

#End Region

End Class
