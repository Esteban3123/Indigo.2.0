'***********************************************************************
' Assembly         : Presentation.MixingStation
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/02/2021
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

Public Class FrmRequestUnitDoseExternalCareCenterMaquila

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRequestUnitDoseExternalCareCenterMaquilaArgs(sender As Object, e As AddRequestUnitDoseExternalCareCenterMaquila)

#End Region

#Region "Variables"
    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PRequestUnitDoseExternalCareCenter

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public RequestUnitDoseExternalCareCenterMaquila As RequestUnitDoseExternalCareCenterMaquila

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListRequestUnitDoseExternalCareCenterMaquilaCompare As List(Of RequestUnitDoseExternalCareCenterMaquila)

    ''' <summary>
    ''' Id de la Central de Mezclas 
    ''' </summary>
    Public CMConfigurationId As Integer

    ''' <summary>
    ''' Id del Centro Externo 
    ''' </summary>
    Public ExternalCareCenterId As Integer

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim listType = New List(Of Tuple(Of Integer, String))
        listType.Add(New Tuple(Of Integer, String)(1, "Medicamento"))
        listType.Add(New Tuple(Of Integer, String)(2, "Paquete"))
        INDsleType.Properties.DataSource = listType
    End Sub

    ''' <summary>
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Sub LoadControls()
        With RequestUnitDoseExternalCareCenterMaquila
            INDsleType.EditValue = .Type

            If .ATCId IsNot Nothing Then
                INDsleATC.EditValue = .ATCId
                INDsleATC.Properties.NullText = .ItemDescription
            End If

            If .PackageId IsNot Nothing Then
                INDslePackage.EditValue = .PackageId
                INDslePackage.Properties.NullText = .ItemDescription
            End If

            INDsleUnitDoseType.EditValue = .UnitDoseTypeId
            INDsleUnitDoseType.Properties.NullText = .UnitDoseTypeCodeName
            INDseQuantity.EditValue = .Quantity
        End With
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
    Private Sub FrmRequestUnitDoseExternalCareCenterMaquila_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Visible = False
        Presenter = New PRequestUnitDoseExternalCareCenter()
        InitializeTuples()

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
    Private Sub FrmRequestUnitDoseExternalCareCenterMaquila_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleType.Focus()
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
        If ListRequestUnitDoseExternalCareCenterMaquilaCompare IsNot Nothing AndAlso ListRequestUnitDoseExternalCareCenterMaquilaCompare.Count > 0 Then
            If INDsleType.EditValue = 1 Then 'Medicamento
                If (From x In ListRequestUnitDoseExternalCareCenterMaquilaCompare Where x.ATCId = INDsleATC.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleATC.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 2 Then 'Paquete
                If (From x In ListRequestUnitDoseExternalCareCenterMaquilaCompare Where x.PackageId = INDslePackage.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleUnitDoseType.Focus()
                    Exit Sub
                End If
            End If
        End If

        If EditModeDetail = False Then
            RequestUnitDoseExternalCareCenterMaquila = New RequestUnitDoseExternalCareCenterMaquila
        End If
        With RequestUnitDoseExternalCareCenterMaquila
            .Type = INDsleType.EditValue
            .TypeName = INDsleType.Text

            .ATCId = Nothing
            If INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ATCId = INDsleATC.EditValue
                .ItemDescription = INDsleATC.Text
            End If

            .PackageId = Nothing
            If INDlyItemPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .PackageId = INDslePackage.EditValue
                .ItemDescription = INDslePackage.Text
            End If

            .UnitDoseTypeId = INDsleUnitDoseType.EditValue
            .UnitDoseTypeCodeName = INDsleUnitDoseType.Text
            .Quantity = INDseQuantity.EditValue
        End With

        Dim args As New AddRequestUnitDoseExternalCareCenterMaquila
        args.RequestUnitDoseExternalCareCenterMaquila = RequestUnitDoseExternalCareCenterMaquila
        args.EditMode = EditModeDetail
        RaiseEvent AddRequestUnitDoseExternalCareCenterMaquilaArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmRequestUnitDoseExternalCareCenterMaquila_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDsleManufacturer control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    ''' INDSleMeasurementUnit
    Private Sub INDslePackage_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePackage.QueryPopUp
        If INDslePackage.Properties.DataSource Is Nothing Then
            INDslePackage.Properties.DataSource = Presenter.InitializePackageByUnitDoseTypeId(INDsleUnitDoseType.EditValue)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
        If INDsleATC.Properties.DataSource Is Nothing Then
            INDsleATC.Properties.DataSource = Presenter.InitializeATC()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUnitDoseType.QueryPopUp
        If INDsleType.EditValue = 1 Then
            If INDsleUnitDoseType.Properties.DataSource Is Nothing Then
                INDsleUnitDoseType.Properties.DataSource = Presenter.InitializeUnitDoseTypeMed()
            End If
        Else
            If INDsleUnitDoseType.Properties.DataSource Is Nothing Then
                INDsleUnitDoseType.Properties.DataSource = Presenter.ListUnitDoseType(CMConfigurationId, ExternalCareCenterId)
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de tipo dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUnitDoseType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
            INDsleUnitDoseType.Properties.DataSource = Presenter.InitializeUnitDoseType()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de atc
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleATC_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleATC.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(343, Nothing, True)
            INDsleATC.Properties.DataSource = Presenter.InitializeATC()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de paquetes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePackage_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePackage.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2065, Nothing, True)
            INDslePackage.Properties.DataSource = Presenter.InitializePackage()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Se dispara al cambiar el valor del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue IsNot Nothing Then
            INDsleUnitDoseType.Properties.DataSource = Nothing
            If INDsleType.EditValue = 1 Then 'Medicamento
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemATC.AllowHide = False

                INDlyItemPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPackage.AllowHide = True
            Else 'Paquete
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True

                INDlyItemPackage.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPackage.AllowHide = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al cambiar el valor del control de Dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleUnitDoseType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUnitDoseType.EditValueChanged
        INDslePackage.Properties.DataSource = Nothing
    End Sub

#End Region

#End Region

End Class

Public Class AddRequestUnitDoseExternalCareCenterMaquila
    Inherits EventArgs

    Property RequestUnitDoseExternalCareCenterMaquila As RequestUnitDoseExternalCareCenterMaquila

    Property EditMode As Boolean

End Class