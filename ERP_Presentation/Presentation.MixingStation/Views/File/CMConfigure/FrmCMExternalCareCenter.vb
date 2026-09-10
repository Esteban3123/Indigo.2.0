'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 03/12/2020
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmCMExternalCareCenter

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddCMExternalCareCenterArgs(sender As Object, e As AddCMExternalCareCenter)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PCMConfig

    ''' <summary>
    ''' Permite saber si los datos estan cargando
    ''' </summary>
    Dim isLoading As Boolean

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public CMExternalCareCenter As CMExternalCareCenter

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListCMExternalCareCenterCompare As List(Of CMExternalCareCenter)

    ''' <summary>
    ''' Ids de lineas de producción
    ''' </summary>
    Public ProductionLinesIds As String

#End Region

#Region "Properties"

    ''' <summary>
    ''' Slide de mensajes
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    Public Property IdMixingStation As Integer?

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Sub LoadControls()
        isLoading = True

        With CMExternalCareCenter
            INDsleCustomer.EditValue = .CustomerId
            INDsleCustomer.Properties.NullText = .CustomerDescription

            INDsleExternalCareCenter.EditValue = .ExternalCareCenterId
            INDsleExternalCareCenter.Properties.NullText = .ExternalCareCenterDescription

            INDsleProductionLine.EditValue = .ProductionLineId
            INDsleProductionLine.Properties.NullText = .ProductionLineDescription
        End With

        isLoading = False
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPopupPackageDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PCMConfig()

        If EditModeDetail Then
            LoadControls()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmCMExternalCareCenter_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleCustomer.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega el producto al listado del paquete
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDBtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If Not ValidateControls() Then
            Exit Sub
        End If

        'Se valida que el item que se va a agregar no exista en el formulario principal
        If ListCMExternalCareCenterCompare IsNot Nothing AndAlso ListCMExternalCareCenterCompare.Count > 0 Then
            If (From x In ListCMExternalCareCenterCompare Where x.CustomerId = INDsleCustomer.EditValue AndAlso x.ExternalCareCenterId = INDsleExternalCareCenter.EditValue AndAlso x.ProductionLineId = INDsleProductionLine.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El item ya existe en la rejilla principal"
                INDsleCustomer.Focus()
                Exit Sub
            End If
        End If

        If EditModeDetail = False Then
            CMExternalCareCenter = New CMExternalCareCenter
            CMExternalCareCenter.Status = True
            CMExternalCareCenter.CMConfigurationId = IdMixingStation
        End If

        'Se valida que el item no este agregado en otra central de mezclas
        Dim productionLine = Presenter.GetProductionLineById(INDsleProductionLine.EditValue)
        Dim unitDoseTypeIds = productionLine.ProductionLineUnitDoseTypeXpo.Select(Function(m) m.Id_UnitDoseType.Id).ToList()

        Dim cmCenterAttentionXpo = Presenter.ValidateExternalCareCenterByUnitDoseId(CMExternalCareCenter.CMConfigurationId, INDsleExternalCareCenter.EditValue, unitDoseTypeIds)
        If cmCenterAttentionXpo IsNot Nothing Then
            Dim unitDoseType = cmCenterAttentionXpo.MixingStationProductionLineXpo.ProductionLineUnitDoseTypeXpo.Where(Function(m) unitDoseTypeIds.Contains(m.Id_UnitDoseType.Id)).FirstOrDefault()
            Mensaje(EeventViewerImages.Advertencia) = $"La línea de producción con tipo de dosis unitaria ({unitDoseType.Id_UnitDoseType.CodeDescription}) se encuentra asociada al mismo centro de atención en la central de mezclas ({cmCenterAttentionXpo.CMConfigurationId.CodeName})"
            Exit Sub
        End If

        With CMExternalCareCenter
            .CustomerId = INDsleCustomer.EditValue
            .CustomerDescription = INDsleCustomer.Text

            .ExternalCareCenterId = INDsleExternalCareCenter.EditValue
            .ExternalCareCenterDescription = INDsleExternalCareCenter.Text

            .ProductionLineId = INDsleProductionLine.EditValue
            .ProductionLineDescription = INDsleProductionLine.Text
        End With

        Dim args As New AddCMExternalCareCenter
        args.CMExternalCareCenter = CMExternalCareCenter
        args.EditMode = EditModeDetail
        RaiseEvent AddCMExternalCareCenterArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmCMExternalCareCenter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de clientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCustomer_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCustomer.QueryPopUp
        If INDsleCustomer.Properties.DataSource Is Nothing Then
            INDsleCustomer.Properties.DataSource = Presenter.InitializeCustomersWithExternalCareCenters()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de centros de atención externos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleExternalCareCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleExternalCareCenter.QueryPopUp
        If INDsleExternalCareCenter.Properties.DataSource Is Nothing AndAlso INDsleCustomer.EditValue IsNot Nothing Then
            INDsleExternalCareCenter.Properties.DataSource = Presenter.InitializeExternalCareCenterByCustomer(INDsleCustomer.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de lineas de producción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProductionLine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProductionLine.QueryPopUp
        If INDsleProductionLine.Properties.DataSource Is Nothing Then
            INDsleProductionLine.Properties.DataSource = Presenter.GetProductionLineByIds(ProductionLinesIds)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de cliente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCustomer_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCustomer.EditValueChanged
        INDsleExternalCareCenter.Properties.DataSource = Nothing
        INDsleExternalCareCenter.EditValue = Nothing
        INDsleExternalCareCenter.Properties.NullText = String.Empty

        If INDsleCustomer.EditValue IsNot Nothing And Not isLoading Then
            Dim ExternalCareacenterTest = Presenter.InitializeExternalCareCenterByCustomerXpo(INDsleCustomer.EditValue)
            If ExternalCareacenterTest Is Nothing OrElse ExternalCareacenterTest.Count = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "No existen centros de atención externos asociados al tercero seleccionado"
                Exit Sub
            End If

            If ExternalCareacenterTest.Count = 1 Then
                INDsleExternalCareCenter.EditValue = ExternalCareacenterTest.Select(Function(x) x.Id).FirstOrDefault
                INDsleExternalCareCenter.Properties.NullText = ExternalCareacenterTest.Select(Function(x) x.CodeDescription).FirstOrDefault
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de clientes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCustomer_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCustomer.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(503, Nothing, True)
            INDsleCustomer.Properties.DataSource = Presenter.InitializeCustomer()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de centros de atención externos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleExternalCareCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleExternalCareCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2201, Nothing, True)
            If INDsleCustomer.EditValue IsNot Nothing Then
                INDsleExternalCareCenter.Properties.DataSource = Presenter.InitializeExternalCareCenterByCustomer(INDsleCustomer.EditValue)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de lineas de producción
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleProductionLine_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleProductionLine.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2073, Nothing, True)
            INDsleProductionLine.Properties.DataSource = Presenter.GetProductionLineByIds(ProductionLinesIds)
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddCMExternalCareCenter
    Inherits EventArgs

    Property CMExternalCareCenter As CMExternalCareCenter

    Property EditMode As Boolean

End Class