'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Diego Andrés Roldán
' Created          : 15-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.Data.Xpo

#End Region

''' <summary>
''' Presentador del frontal de tipos de documentos
''' </summary>
Public Class PDocumentType

#Region "Fields"

    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IDocumentType

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
    Public Sub New(ByRef view As IDocumentType)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using Model As New MDocumentType(_view.MyTag)
            Me._view.Sequense = Await Model.GetSequense()
        End Using
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource del search de libro oficial
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeBook() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListBookByStatus(True)
    End Function

#End Region

End Class
