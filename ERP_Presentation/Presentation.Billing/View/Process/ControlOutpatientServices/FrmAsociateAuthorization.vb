'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/06/2020
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Billing.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls
Imports Presentation.Base
Imports Domain.Base.Entities
Imports System.Text
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BillingRepository
Imports Presentation.Common.MVP
Imports System.Threading
Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.Data.Xpo.AuthorizationRepository
#End Region

Public Class FrmAsociateAuthorization

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PControlOutpatientServices

    ''' <summary>
    ''' Utilizado para cancelar el asyncrono
    ''' </summary>
    Private tokenAsync As CancellationTokenSource

    ''' <summary>
    ''' Código centro atención
    ''' </summary>
    Public CareCenterCode As String

    ''' <summary>
    ''' Códigos de unidades funcionales
    ''' </summary>
    Public FunctionalUnitCodes As String

    ''' <summary>
    ''' Código del paciente
    ''' </summary>
    Public PatientCode As String

    ''' <summary>
    ''' Listado de códigos cups
    ''' </summary>
    Public ListCupsCodes As List(Of String)

    ''' <summary>
    ''' Listado que se devuelve al seleccionar un item de la rejilla y aceptar
    ''' </summary>
    Public ListViewTraceabilityPaperworkAuthorizedXpo As List(Of ViewTraceabilityPaperworkAuthorizedXpo)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
        Set(value As String)

            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Ejecuta la consulta para traer los datos
    ''' </summary>
    Private Sub ExecuteGetList()
        INDviewInfo.ShowLoadingPanel()
        tokenAsync = New CancellationTokenSource()
        Task.Factory.StartNew(Sub()
                                  Dim result = Presenter.ListAuthorizations(ListCupsCodes, PatientCode, FunctionalUnitCodes, CareCenterCode)
                                  If Not tokenAsync.IsCancellationRequested Then
                                      INDgcInfo.SafeInvoke(Sub()
                                                               INDviewInfo.HideLoadingPanel()
                                                               INDgcInfo.DataSource = result
                                                           End Sub)
                                  End If
                              End Sub, tokenAsync.Token)
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAsociateAuthorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Presenter = New PControlOutpatientServices()
        ExecuteGetList()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el botón de importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAccept_Click(sender As Object, e As EventArgs) Handles INDbtnAccept.Click
        If INDviewInfo.LoadingPanelVisible Then
            Mensaje(EeventViewerImages.Advertencia) = "No ha terminado de cargar la rejilla"
            Exit Sub
        End If

        Dim ListDetails = (From x In INDviewInfo.GetSelectedRows() Select CType(INDviewInfo.GetRow(x), ViewTraceabilityPaperworkAuthorizedXpo)).ToList()

        If ListDetails Is Nothing OrElse ListDetails.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item"
            Exit Sub
        End If

        ListViewTraceabilityPaperworkAuthorizedXpo = ListDetails
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAsociateAuthorization_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If tokenAsync IsNot Nothing Then
            tokenAsync.Cancel()
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAsociateAuthorization_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Evento que se dispara al seleccionar el check de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewInfo_SelectionChanged(sender As Object, e As DevExpress.Data.SelectionChangedEventArgs) Handles INDviewInfo.SelectionChanged
        Dim infoRowSelected As ViewTraceabilityPaperworkAuthorizedXpo = INDviewInfo.GetFocusedRow()
        Dim rowHandleInfoSelected = INDviewInfo.FocusedRowHandle()
        Dim listRowHandles = INDviewInfo.GetSelectedRows()
        If listRowHandles.Length > 0 Then
            For i = 0 To listRowHandles.Count - 1 Step 1
                If rowHandleInfoSelected <> listRowHandles(i) Then
                    Dim row As ViewTraceabilityPaperworkAuthorizedXpo = INDviewInfo.GetRow(listRowHandles(i))
                    If row IsNot Nothing Then
                        If infoRowSelected.ServiceCode = row.ServiceCode Then
                            INDviewInfo.UnselectRow(listRowHandles(i))
                        End If
                    End If
                End If
            Next
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddAssociateAuthorization
    Inherits EventArgs

    ''' <summary>
    ''' Listado de detalles que se seleccionaron
    ''' </summary>
    ''' <returns></returns>
    Property ListViewTraceabilityPaperworkAuthorizedXpo As List(Of ViewTraceabilityPaperworkAuthorizedXpo)

    ''' <summary>
    ''' Campo que se utiliza en control servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Property arg As Object

    ''' <summary>
    ''' Campo que se utiliza en control servicios ambulatorios
    ''' </summary>
    ''' <returns></returns>
    Property sender As Object

End Class