'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Juan F. Tamayo
' Created          : 2013-05-07
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-05-07
' Description      : Presentador del frontal de registro de objeciones
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Presentation.Base

#End Region

''' <summary>
''' Presentador del frontal de registro de objeciones
''' </summary>
Public Class PRegisterObjection

#Region "Fields"

    ''' <summary>
    ''' Referencia a la interfaz del frontal de registro de objeciones
    ''' </summary>
    Private _view As IRegisterObjection
    ''' <summary>
    ''' Objeto de la autorizacion
    ''' </summary>
    Private _objection As Object
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
    Public Sub New(ByRef view As IRegisterObjection)
        If view Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me._view = view
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los registros de objecion del detalle de factura
    ''' </summary>
    Public Async Sub LoadDataAsync()
        Using model As New MRegisterObjection()
            Me._view.IsLoaded = False
            Me._view.AsyncLoader(True)
            Me._view.GeneralConceptDataSource = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListXPInstantFeedbackSource(Of CommonConceptGlosas)($"Type='1'")
            Me._view.ResponsibleDataSource = Await model.ListResponsiblesAll()

            If Me._view.GeneralGlosa <> True Then
                If Me._view.InvoiceDetaildId AndAlso Me._view.InvoiceDetaildQXId > 0 Then
                    Me._view.MovementGlosaSource = Await model.ListMovementGlosaQx(Me._view.InvoiceDetaildId, Me._view.InvoiceDetaildQXId)
                Else
                    Me._view.MovementGlosaSource = Await model.ListMovementGlosa(Me._view.InvoiceDetaildId)
                End If
            Else
                Me._view.MovementGlosaSource = New List(Of Domain.Entities.GlosaMovementGlosa)
            End If


            Me._view.AsyncLoader(False)
            Me._view.ActionsOnControls = False
            Me._view.IsLoaded = True
        End Using
    End Sub

    ''' <summary>
    ''' Carga todos los responsables
    ''' </summary>
    Public Async Function LoadResponsibles() As Task
        Using model As New MRegisterObjection()
            Me._view.ResponsibleDataSource = Nothing
            Me._view.ResponsibleDataSource = Await model.ListResponsiblesAll()
        End Using
    End Function
#End Region

End Class


