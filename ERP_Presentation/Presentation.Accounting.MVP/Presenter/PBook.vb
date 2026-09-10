'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 11/11/2015
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
#End Region

Public Class PBook


#Region "Fields"
    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IBook

    ''' <summary>
    ''' Referencia a los valores de sesión
    ''' </summary>
    Dim _indigo As SessionValues
#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista del frontal</param>
    Public Sub New(ByRef view As IBook)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

    Public Async Sub GetSequense()
        Using model As New MRetentionConcept(Me._view.MyTag)
            Me._view.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Sub InitializeCurrency()
        Using Model As New MBusqueda
            Me._view.Currency = Model.ConsultarEntidades(eDataSource.Currency)
        End Using
    End Sub

#End Region

End Class