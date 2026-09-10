'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Andres Alarcon
' Created          : 18-05-2023
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Presentation.Contract.MVP
Imports Presentation.Controls

#End Region

Public Class FrmPopUpServicesFeeDetail
    Implements IServiceFeeDetail

#Region "Events"

    Public Event AddServicesFeeDetail(sender As Object, e As AddServiceFeeEventArgs)

#End Region

#Region "Globals"

    ''' <summary>
    ''' Representa a la entidad de detalle de tarifa de productos
    ''' </summary>
    Public _serviceFeeDetail As ServiceFeeDetail

    ''' <summary>
    ''' Permite saber si se esta editando un registro
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Representa el presentador
    ''' </summary>
    Private _presenter As PServiceFeeDetail

    ''' <summary>
    ''' Representa la lista de los detalles ya agregados
    ''' </summary>
    Public ListServiceFeeDetail As List(Of ServiceFeeDetail)
#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el servicio
    ''' </summary>
    Public Property ServiceId As Integer Implements IServiceFeeDetail.ServiceId
        Get
            Return INDsleServices.EditValue
        End Get
        Set(value As Integer)
            INDsleServices.EditValue = value
        End Set
    End Property


#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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

    ''' <summary>
    ''' Handles the Load event of the FrmProductRateDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmProductRateDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        BarraBotones.StatusRecordVisible = False
        _presenter = New PServiceFeeDetail(Me)

        If EditMode Then
            LoadControls()
        Else
            CleanControls()
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        AsyncLoader(True)

        If Not ValidateControls() Then
            Exit Sub
        End If

        If Not EditMode Then
            If Me.ListServiceFeeDetail.Any(Function(x) x.ServiceId = ServiceId) Then
                Mensaje(EeventViewerImages.Advertencia) = "El Servicio " + INDsleServices.Text + " ya esta agregado"
                Exit Sub
            End If
        End If

        AssigningValues()
        RaiseEvent AddServicesFeeDetail(Nothing, New AddServiceFeeEventArgs With
            {
                .EditMode = EditMode,
                .ServiceFeeDetail = _serviceFeeDetail
            }
        )

        AsyncLoader(False)
        If EditMode Then
            Me.Close()
        End If
        Me.CleanControls()
    End Sub

    Private Sub AssigningValues()
        With _serviceFeeDetail

            .ServiceId = ServiceId
            .ServiceCodeName = INDsleServices.Text
            .Observations = INDTextObservations.Text
            .InitialDate = INDsleInitialDate.EditValue
            .FinalDate = INDsleFinalDate.EditValue
            .SalePrice = INDtxtSalesValue.EditValue

        End With
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Maneja la función cuando se le da click al boton (+) del label de productos
    ''' </summary>
    Private Sub INDsleServices_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleServices.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBillingConcept
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
        End If
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Handles the KeyDown event of the FrmProductRateDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmPopUpServicesFeeDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleServices control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleServices_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleServices.QueryPopUp
        If INDsleServices.Properties.DataSource Is Nothing Then
            INDsleServices.Properties.DataSource = _presenter.InitializeServices()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor de la fecha inicial
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeInitialDate_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInitialDate.EditValueChanged
        If INDsleInitialDate.EditValue IsNot Nothing Then
            INDsleFinalDate.Properties.MinValue = INDsleInitialDate.EditValue
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form por primera vez
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductRateDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleServices.Focus()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles cuando se esta editando
    ''' </summary>
    Private Sub LoadControls()
        INDbtnAdd.Text = ResourceManager.GetString("Edit")
        With _serviceFeeDetail

            INDsleServices.ReadOnly = True
            ServiceId = .ServiceId
            INDsleServices.Properties.NullText = .ServiceCodeName
            INDsleInitialDate.EditValue = .InitialDate
            INDsleFinalDate.EditValue = .FinalDate
            INDtxtSalesValue.EditValue = .SalePrice
            INDTextObservations.EditValue = .Observations

        End With
    End Sub

    ''' <summary>
    ''' Cleans the controls.
    ''' </summary>
    Private Sub CleanControls()

        INDsleServices.EditValue = Nothing
        INDsleServices.Properties.NullText = String.Empty
        INDsleInitialDate.EditValue = Nothing
        INDsleFinalDate.EditValue = Nothing
        INDtxtSalesValue.EditValue = Nothing
        INDTextObservations.EditValue = Nothing
        _serviceFeeDetail = New ServiceFeeDetail

    End Sub

    ''' <summary>
    ''' Validates the controls.
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForms() As String
        Dim errorList As New StringBuilder()

        If INDsleInitialDate.EditValue > INDsleFinalDate.EditValue Then
            errorList.AppendLine("La fecha inicial debe ser menor a la fecha final")
        End If

        Return errorList.ToString()
    End Function

    Private Async Sub INDsleServices_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleServices.EditValueChanged
        Try
            If INDsleServices.EditValue IsNot Nothing And Not EditMode Then
                AsyncLoader(True)
                Using model As New MIPSServiceGroup(Me.Tag)
                    Dim resultProduct = Await model.GetIPSServiceGroupById(ServiceId)

                    If resultProduct IsNot Nothing Then
                        INDtxtSalesValue.EditValue = resultProduct.Price
                        AsyncLoader(False)
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
        End Try
    End Sub
#End Region

End Class