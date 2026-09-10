'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan Diego Diaz
' Created          : 2013-06-07
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base

#End Region

''' <summary>
''' Presentador del frontal de devolución
''' </summary>
Public Class PDevolutions

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de devolución
    ''' </summary>
    Private _view As IDevolution
    ''' <summary>
    ''' Objeto de la devolución
    ''' </summary>
    Private _devolution As Object
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues = SessionValues.Instance

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor de la clase
    ''' </summary>
    ''' <param name="view">Referencia a la vista</param>
    Public Sub New(ByRef view As IDevolution)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para Iniciar algunas propiedades de la vista de Devoluciones
    ''' </summary>
    Public Async Sub Initializes()
        Using Model As New MDevolutions(Me._view.TagForm)
            _view.DataSourceBranch = Model.GetBranchAll()
            _view.DataSourceSpecificConcepts = Await Model.ListConceptsGlosaByTypes(New List(Of String) From {"3", "9", "10"})
        End Using

    End Sub

#End Region

#Region "Metodos"
    ''' <summary>
    ''' obtiene la secuencia numerica
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function GetSequense() As Task
        Using model As New MRadicateInvoice("509") 'tag de radicacion de cuentas con la que esta registrado la secuencia de reclasificacion
            Me._view.Sequense = Await model.GetSequense()
        End Using
    End Function
#End Region
   
End Class