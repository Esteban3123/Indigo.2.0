
'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz M.
' Created          : 01-08-2013
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
Public Class PJustificationTemplate

#Region "variables y constructor "
    ''' <summary>
    ''' Variable utilizada para instanciar la interfaz
    ''' </summary>
    Dim _view As IJustificationTemplate
    ''' <summary>
    ''' Variable que se utiliza para tratar a la Plantilla de Justificación como un Objeto
    ''' </summary>
    Dim JustificationTemplate As Object
    ''' <summary>
    ''' Variable que se utiliza para instanciar la clase singlenton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz.
    ''' </summary>
    ''' <param name="iview">The iview.</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IJustificationTemplate)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = iview
        End If
    End Sub


    ''' <summary>
    ''' Metodo que se utiliza para Iniciar algunas propiedades de la vista de JustificationTemplate
    ''' </summary>
    Public Async Sub Initializes(Tag As String)
        Using Model As New MJustificationTemplate(Tag)
            Dim listTypes As New List(Of String)
            listTypes.Add("1")
            listTypes.Add("2")
            _view.DataSourceConcepts = Await Model.getConcepts(listTypes)

        End Using

    End Sub


#End Region

End Class
