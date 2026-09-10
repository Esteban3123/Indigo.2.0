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

Public Class FrmContractExternalClientsDetail

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddContractExternalClientsDetailArgs(sender As Object, e As AddContractExternalClientsDetail)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PContractExternalClients

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public ContractExternalClientsDetail As ContractExternalClientsDetail

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListContractExternalClientsDetailCompare As List(Of ContractExternalClientsDetail)

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
    ''' Carga los datos del producto en el formulario
    ''' </summary>
    Private Sub LoadControls()
        With ContractExternalClientsDetail
            INDsleType.EditValue = .Type

            If .ProductId IsNot Nothing Then
                INDsleProduct.EditValue = .ProductId
                INDsleProduct.Properties.NullText = .SourceCodeName
            End If

            If .AtcId IsNot Nothing Then
                INDsleATC.EditValue = .AtcId
                INDsleATC.Properties.NullText = .SourceCodeName
            End If

            If .SupplieId IsNot Nothing Then
                INDsleSupplie.EditValue = .SupplieId
                INDsleSupplie.Properties.NullText = .SourceCodeName
            End If

            If .CUPSEntityId IsNot Nothing Then
                INDsleCUPSEntity.EditValue = .CUPSEntityId
                INDsleCUPSEntity.Properties.NullText = .SourceCodeName
            End If

            INDsleSuppliedBy.EditValue = .SuppliedBy

        End With
    End Sub

    ''' <summary>
    ''' Inicializa los combos quemados
    ''' </summary>
    Private Sub InitializeTuples()
        Dim ListComponentType = New List(Of Tuple(Of Byte, String))()
        ListComponentType.Add(New Tuple(Of Byte, String)(1, "Medicamento"))
        ListComponentType.Add(New Tuple(Of Byte, String)(2, "Insumo"))
        ListComponentType.Add(New Tuple(Of Byte, String)(3, "Producto"))
        ListComponentType.Add(New Tuple(Of Byte, String)(4, "Servicio"))
        INDsleType.Properties.DataSource = ListComponentType

        Dim listSuppliedBy = New List(Of Tuple(Of Byte, String))()
        listSuppliedBy.Add(New Tuple(Of Byte, String)(1, "Cliente"))
        listSuppliedBy.Add(New Tuple(Of Byte, String)(2, "Cliente y Central Mezclas"))
        INDsleSuppliedBy.Properties.DataSource = listSuppliedBy

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
        Presenter = New PContractExternalClients()
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
    Private Sub FrmContractExternalClientsDetail_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
        If ListContractExternalClientsDetailCompare IsNot Nothing AndAlso ListContractExternalClientsDetailCompare.Count > 0 Then
            If INDsleType.EditValue = 1 Then 'Medicamento
                If (From x In ListContractExternalClientsDetailCompare Where x.AtcId = INDsleATC.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleATC.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 2 Then 'Insumo
                If (From x In ListContractExternalClientsDetailCompare Where x.SupplieId = INDsleSupplie.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleSupplie.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                If (From x In ListContractExternalClientsDetailCompare Where x.ProductId = INDsleProduct.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleProduct.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 4 Then 'Servicios
                If (From x In ListContractExternalClientsDetailCompare Where x.CUPSEntityId = INDsleCUPSEntity.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El item seleccionado ya existe en la rejilla principal"
                    INDsleCUPSEntity.Focus()
                    Exit Sub
                End If
            End If
        End If

        If EditModeDetail = False Then
            ContractExternalClientsDetail = New ContractExternalClientsDetail
        End If
        With ContractExternalClientsDetail
            .Type = INDsleType.EditValue
            .TypeName = INDsleType.Text

            .ProductId = Nothing
            If INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ProductId = INDsleProduct.EditValue
                .SourceCodeName = INDsleProduct.Text
            End If

            .AtcId = Nothing
            If INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .AtcId = INDsleATC.EditValue
                .SourceCodeName = INDsleATC.Text
            End If

            .SupplieId = Nothing
            If INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SupplieId = INDsleSupplie.EditValue
                .SourceCodeName = INDsleSupplie.Text
            End If

            .CUPSEntityId = Nothing
            If INDlyItemCUPSEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .CUPSEntityId = INDsleCUPSEntity.EditValue
                .SourceCodeName = INDsleCUPSEntity.Text
            End If

            .SuppliedBy = INDsleSuppliedBy.EditValue
            .SuppliedByName = INDsleSuppliedBy.Text
        End With

        Dim args As New AddContractExternalClientsDetail
        args.ContractExternalClientsDetail = ContractExternalClientsDetail
        args.EditMode = EditModeDetail
        RaiseEvent AddContractExternalClientsDetailArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmContractExternalClientsDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de medicamentos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMedicine_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleATC.QueryPopUp
        If INDsleATC.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleATC.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListATC)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de insumos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplie_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSupplie.QueryPopUp
        If INDsleSupplie.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleSupplie.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListInventorySupplie)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleProduct.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListInventoryProductByProductType, "4")
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se dispara al desplegar el control de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPSEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCUPSEntity.QueryPopUp
        If INDsleCUPSEntity.Properties.DataSource Is Nothing Then
            Using Model As New MBusqueda
                INDsleCUPSEntity.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListCupsEntityByStatus, True)
            End Using
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de componente
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If INDsleType.EditValue IsNot Nothing Then
            If INDsleType.EditValue = 1 Then 'Medicamento
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemATC.AllowHide = False
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplie.AllowHide = True
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.AllowHide = True
                INDlyItemCUPSEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCUPSEntity.AllowHide = True
                INDlyItemSuppliedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf INDsleType.EditValue = 2 Then 'Insumo
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSupplie.AllowHide = False
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.AllowHide = True
                INDlyItemCUPSEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCUPSEntity.AllowHide = True
                INDlyItemSuppliedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplie.AllowHide = True
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemProduct.AllowHide = False
                INDlyItemCUPSEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemCUPSEntity.AllowHide = True
                INDlyItemSuppliedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            ElseIf INDsleType.EditValue = 4 Then 'Servicios
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplie.AllowHide = True
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.AllowHide = True
                INDlyItemCUPSEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemCUPSEntity.AllowHide = False
                INDlyItemSuppliedBy.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "BUttonClick"

    ''' <summary>
    ''' Abre el form de cups
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCUPSEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCUPSEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(970, Nothing, True)
            Using Model As New MBusqueda
                INDsleCUPSEntity.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListCupsEntityByStatus, True)
            End Using
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddContractExternalClientsDetail
    Inherits EventArgs

    Property ContractExternalClientsDetail As ContractExternalClientsDetail

    Property EditMode As Boolean

End Class