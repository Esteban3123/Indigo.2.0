'***********************************************************************
' Assembly         : Presentacion.Accouting.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 08/06/2017
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
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
#End Region

Public Class PMassiveReplication

#Region "Fields"
    ''' <summary>
    ''' Instancia de la interface
    ''' </summary>
    Dim _view As IMassiveReplication

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
    Public Sub New(ByRef view As IMassiveReplication)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._indigo = SessionValues.Instance
            Me._view = view
        End If
    End Sub

    ''' <summary>
    ''' Layout
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me._view.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource del libro origen
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBookOrigin()
        _view.BookOriginXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListBookByStatusXpCollection(True)
    End Sub

    ''' <summary>
    ''' Inicializa el libro origen
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeBookDestination(bookOriginId As Integer)
        _view.BookDestinationXpo = XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListBookByStatusAndNotInBookId(True, bookOriginId)
    End Sub

    ''' <summary>
    ''' Inicializa el tipo de comprobante
    ''' </summary>
    ''' <remarks></remarks>
    Public Function InitializeJournalVoucherType() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(_indigo.TransactionalContainer).AccountingService.ListDocumentTypes(True)
    End Function

#End Region

End Class