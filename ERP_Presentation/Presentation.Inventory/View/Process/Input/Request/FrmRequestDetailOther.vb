'***********************************************************************
' Assembly         : Presentation.Inventory
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 21/06/2021
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP

#End Region

Public Class FrmRequestDetailOther

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRequestDetailOtherArgs(sender As Object, e As AddRequestDetailOther)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PInventoryRequest

    ''' <summary>
    ''' Permite saber si se esta editando el detalle
    ''' </summary>
    Public EditModeDetail As Boolean

    ''' <summary>
    ''' Detalle
    ''' </summary>
    Public RequestDetailOther As InventoryRequestDetailOther

    ''' <summary>
    ''' Se utiliza para validar
    ''' </summary>
    Public ListRequestOtherDetailCompare As New Domain.Entities.TrackableCollection(Of InventoryRequestDetailOther)

    ''' <summary>
    ''' Obtiene o establece el producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct

    ''' <summary>
    ''' Obtiene o establece el medicamento seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim atc As ATC

    ''' <summary>
    ''' Obtiene o establece el medicamento seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim supplie As InventorySupplie

    ''' <summary>
    ''' Listado de productos o Insumos por unidad funcional según parámetros de solicitudes
    ''' </summary>
    Public ListViewRequestParamXpo As List(Of ViewRequestParamXpo)

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
        With RequestDetailOther
            INDsleType.EditValue = .ComponentType

            If .InventoryProductId IsNot Nothing Then
                INDsleProduct.EditValue = .InventoryProductId
                INDsleProduct.Properties.NullText = .SourceCodeName
            End If

            If .ATCId IsNot Nothing Then
                INDsleATC.EditValue = .ATCId
                INDsleATC.Properties.NullText = .SourceCodeName

            End If

            If .SupplieId IsNot Nothing Then
                INDsleSupplie.EditValue = .SupplieId
                INDsleSupplie.Properties.NullText = .SourceCodeName
            End If

            INDseQuantity.EditValue = .Quantity
            INDmeObservation.EditValue = .Description
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
        Presenter = New PInventoryRequest()
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
        If ListRequestOtherDetailCompare IsNot Nothing AndAlso ListRequestOtherDetailCompare.Count > 0 Then
            If INDsleType.EditValue = 1 Then 'Medicamento
                If (From x In ListRequestOtherDetailCompare Where x.ATCId = INDsleATC.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente seleccionado ya existe en la rejilla principal"
                    INDsleATC.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 2 Then 'Insumo
                If (From x In ListRequestOtherDetailCompare Where x.SupplieId = INDsleSupplie.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente seleccionado ya existe en la rejilla principal"
                    INDsleSupplie.Focus()
                    Exit Sub
                End If
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                If (From x In ListRequestOtherDetailCompare Where x.InventoryProductId = INDsleProduct.EditValue Select x).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El componente seleccionado ya existe en la rejilla principal"
                    INDsleProduct.Focus()
                    Exit Sub
                End If
            End If
        End If

        'Se validan parámetros de las solicitudes
        If ListViewRequestParamXpo.Any() AndAlso INDsleType.EditValue <> 1 Then
            Dim requestByType As New List(Of ViewRequestParamXpo)
            If INDsleType.EditValue = 2 Then 'Insumo
                requestByType = ListViewRequestParamXpo.Where(Function(i) i.Type = 1 AndAlso i.SupplieId.Value = INDsleSupplie.EditValue).ToList()
            ElseIf INDsleType.EditValue = 3 Then 'Producto
                requestByType = ListViewRequestParamXpo.Where(Function(i) i.Type = 2 AndAlso i.ProductId.Value = INDsleProduct.EditValue).ToList()
            End If

            If Not requestByType.Any() Then
                Mensaje(EeventViewerImages.Advertencia) = "El componente no se encuentra parametrizado para la unidad funcional"
                Exit Sub
            End If

            If requestByType.Count > 1 Then
                Mensaje(EeventViewerImages.Advertencia) = "El componente no se encuentra parametrizado más de una vez para la unidad funcional"
                Exit Sub
            End If

            If INDseQuantity.EditValue > requestByType.First().Quantity Then
                Mensaje(EeventViewerImages.Advertencia) = "Cantidad no autorizada para este producto"
                Exit Sub
            End If
        End If

        If EditModeDetail = False Then
            RequestDetailOther = New InventoryRequestDetailOther
        End If

        With RequestDetailOther
            .ComponentType = INDsleType.EditValue
            .ComponentTypeName = INDsleType.Text

            'Medicines
            .ATCId = Nothing
            If INDlyItemATC.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .ATCId = INDsleATC.EditValue
                .SourceCodeName = INDsleATC.Text
                Using model As New MATC(Me.Tag)
                    Dim xpo = model.GetATCByIdSimple(.ATCId)
                    .consumptionUnit = If(xpo.Presentations IsNot Nothing, xpo.Presentations, "")
                End Using
            End If

            'Supplies
            .SupplieId = Nothing
            If INDlyItemSupplie.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .SupplieId = INDsleSupplie.EditValue
                .SourceCodeName = INDsleSupplie.Text

                Using model As New MInventorySupplie(Me.Tag)
                    Dim xpo = model.GetProductBySupplieEntityXpo(.SupplieId)
                    If xpo IsNot Nothing Then
                        .consumptionUnit = If(xpo.PackagingUnitId.CodeName IsNot Nothing, xpo.PackagingUnitId.CodeName, "UNIDAD")
                    End If
                End Using
            End If

            'Products
            .InventoryProductId = Nothing
            If INDlyItemProduct.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .InventoryProductId = INDsleProduct.EditValue
                Using model As New MInventoryProduct(Me.Tag)
                    product = model.GetInventoryProductByIdSimple(.InventoryProductId)
                End Using
                .SourceCodeName = INDsleProduct.Text
                .consumptionUnit = If(product.PackingUnitDescription IsNot Nothing, product.PackingUnitDescription, "")
            End If

            .Quantity = INDseQuantity.EditValue
            .OutstandingQuantity = INDseQuantity.EditValue
            .Description = INDmeObservation.Text
        End With


        Dim args As New AddRequestDetailOther
        args.RequestDetailOther = RequestDetailOther
        args.EditMode = EditModeDetail
        RaiseEvent AddRequestDetailOtherArgs(Nothing, args)
        Me.Close()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Enter al control de unidad de medida
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleMeasurementUnit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
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
            INDsleSupplie.Properties.DataSource = Presenter.DatasourceSuppliesByRequestParams(ListViewRequestParamXpo)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de productos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleProduct.QueryPopUp
        If INDsleProduct.Properties.DataSource Is Nothing Then
            INDsleProduct.Properties.DataSource = Presenter.DatasourceProductsByRequestParams(ListViewRequestParamXpo)
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

#End Region

#End Region

End Class

Public Class AddRequestDetailOther
    Inherits EventArgs

    Property RequestDetailOther As InventoryRequestDetailOther

    Property EditMode As Boolean

End Class