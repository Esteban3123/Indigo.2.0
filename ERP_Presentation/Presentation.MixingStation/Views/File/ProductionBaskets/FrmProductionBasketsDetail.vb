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
Imports DevExpress.XtraEditors.Controls
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

Public Class FrmProductionBasketsDetail

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProductionBasketsDetailArgs(sender As Object, e As AddProductionBasketsDetail)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PProductionBaskets

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public ProductionBasketsDetail As ProductionBasketsDetail

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListProductionBasketsDetailCompare As List(Of ProductionBasketsDetail)

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
        With ProductionBasketsDetail
            INDsleType.EditValue = .ComponentType

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

            INDseQuantity.EditValue = .Quantity
            INDsleMeasurementUnit.EditValue = .MeasurementUnitId
            INDsleMeasurementUnit.Properties.NullText = .MeasureUnitDescription
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
        INDsleType.Properties.DataSource = ListComponentType
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub LoadMeasurementUnit()
        If INDsleType.EditValue IsNot Nothing Then
            If INDsleType.EditValue = 2 Then 'insumo
                INDsleMeasurementUnit.Properties.DataSource = Presenter.ListMeasureUnitByType(3)
            Else
                Using Model As New MBusqueda
                    INDsleMeasurementUnit.Properties.DataSource = Model.ConsultarEntidades(eDataSource.ListMeasureUnit)
                End Using
            End If
        End If
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
        Presenter = New PProductionBaskets()
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
    Private Sub FrmProductionBaskets_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
        If ListProductionBasketsDetailCompare IsNot Nothing AndAlso ListProductionBasketsDetailCompare.Count > 0 Then
            If INDsleType.EditValue = 1 Then 'Medicamento
                If (From x In ListProductionBasketsDetailCompare Where x.AtcId = INDsleATC.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente seleccionado ya existe en la rejilla principal"
                    INDsleATC.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 2 Then 'Insumo
                If (From x In ListProductionBasketsDetailCompare Where x.SupplieId = INDsleSupplie.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente seleccionado ya existe en la rejilla principal"
                    INDsleSupplie.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                If (From x In ListProductionBasketsDetailCompare Where x.ProductId = INDsleProduct.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente seleccionado ya existe en la rejilla principal"
                    INDsleProduct.Focus()
                    Exit Sub
                End If
            End If
        End If

        If EditModeDetail = False Then
            ProductionBasketsDetail = New ProductionBasketsDetail
        End If
        With ProductionBasketsDetail
            .ComponentType = INDsleType.EditValue
            .ComponentTypeName = INDsleType.Text

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

            .Quantity = INDseQuantity.EditValue
            .MeasurementUnitId = INDsleMeasurementUnit.EditValue
            .MeasureUnitDescription = INDsleMeasurementUnit.Text
        End With

        Dim args As New AddProductionBasketsDetail
        args.ProductionBasketsDetail = ProductionBasketsDetail
        args.EditMode = EditModeDetail
        RaiseEvent AddProductionBasketsDetailArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Enter al control de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasurementUnit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDsleMeasurementUnit.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDbtnAdd.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al oprimir escape para cerrar el popup del listado de productos
    ''' </summary>
    Private Sub FrmProductionBaskets_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub INDsleMeasurementUnit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMeasurementUnit.QueryPopUp
        If INDsleMeasurementUnit.Properties.DataSource Is Nothing Then
            LoadMeasurementUnit()
        End If
    End Sub

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

#End Region

#Region "EditValueChanging"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDsleType.EditValueChanging
        If e.OldValue = 2 OrElse e.NewValue = 2 Then
            INDsleMeasurementUnit.Properties.DataSource = Nothing
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
            ElseIf INDsleType.EditValue = 2 Then 'Insumo
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemSupplie.AllowHide = False
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemProduct.AllowHide = True
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemATC.AllowHide = True
                INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemSupplie.AllowHide = True
                INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemProduct.AllowHide = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSleProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleProduct.EditValueChanged
        If INDsleProduct.EditValue IsNot Nothing Then
            Using model As New MInventoryProduct(Me.Tag)
                Dim resultProduct = Await model.GetProductByIdXpo(INDsleProduct.EditValue)
                If resultProduct IsNot Nothing Then
                    If resultProduct.MeasurementUnitId IsNot Nothing Then
                        INDsleMeasurementUnit.EditValue = resultProduct.MeasurementUnitId.Id
                        INDsleMeasurementUnit.Properties.NullText = String.Format("{0} - {1}", resultProduct.MeasurementUnitId.Code, resultProduct.MeasurementUnitId.Name)
                    End If
                End If
            End Using
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddProductionBasketsDetail
    Inherits EventArgs

    Property ProductionBasketsDetail As ProductionBasketsDetail

    Property EditMode As Boolean

End Class