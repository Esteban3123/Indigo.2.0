'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Hector Rodriguez Rubiano
' Created          : 24-04-2019
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports System.Drawing
Imports Presentation.Base
Imports DevExpress.Xpo
Imports Presentation.Inventory.MVP
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.CrossCutting.Base
Imports System.Text
Imports Presentation.Accounting.MVP
Imports Presentation.Controls.MVP
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

Public Class PopUpProductPurchaseRequest

    #Region "Builder"

    ''' <summary>
    ''' Inicializa una isntancia de la clase
    ''' </summary>
    ''' <param name="_Detaill"></param>
    ''' <param name="_listProducts"></param>
    ''' <remarks></remarks>
    Public Sub New(_ReadOnly As Boolean, _Detaill As PurchaseRequestDetail, _listProducts As TrackableCollection(Of PurchaseRequestDetail))
        InitializeComponent()
        
        If _Detaill IsNot Nothing Then
            INDSmbAdd.Text = ResourceManager.GetString("Edit")
            product = _Detaill.InventoryProduct
            editModeForm = True
            Detail = _Detaill
        End If
        If _listProducts IsNot Nothing Then
            For Each item In _listProducts
                ListProduct.Add(item)
            Next
        End If
        OnlyRead = _ReadOnly
        If OnlyRead Then INDSmbAdd.Text = "Salir"
    End Sub

#End Region

#Region "Globals"

    ''' <summary>
    ''' Obtiene o establece el producto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Dim product As InventoryProduct

    ''' <summary>
    ''' Obtiene o establece el listado de los detalles agregados
    ''' </summary>
    ''' <remarks></remarks>
    Private ListProduct As TrackableCollection(Of PurchaseRequestDetail) = New TrackableCollection(Of PurchaseRequestDetail)

    ''' <summary>
    ''' Objeto que establece el producto a agregar
    ''' </summary>
    ''' <remarks></remarks>
    Dim Detail As PurchaseRequestDetail

    ''' <summary>
    ''' Define si el formulario esta editando o no
    ''' </summary>
    ''' <remarks></remarks>
    Dim editModeForm As Boolean = False

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' Define si el form es solo para visualizar
    ''' </summary>
    ''' <remarks></remarks>
    Dim OnlyRead As Boolean

    Dim ProductId As Integer = 0

    
#End Region

#Region "Methods"

    ''' <summary>
    ''' Abre el formulario para adicion de productos
    ''' </summary>
    ''' <param name="form"></param>
    ''' <remarks></remarks>
    Private Sub OpenFormDialog(form As FormBase)
        form.ViewModeEditHold = True
        form.Size = New Size(800, 700)
        form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        form.MaximizeBox = False
        form.MinimizeBox = False
        Dim transparent = New FrmTransparent(form, False)
        transparent.ShowDialog()
    End Sub

    ''' <summary>
    ''' Limpia los controles para agregar un producto nuevo
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        product = Nothing
        INDPpceProduct.Text = String.Empty
        INDTxeUnit.Text = String.Empty
        INDTxeMeasureUnit.Text = String.Empty
        INDSpeQuantity.EditValue = 0
        INDMmeDescription.Text = String.Empty
        INDPpceProduct.Focus()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    Private Sub EnabledControls()
        INDPpceProduct.Enabled = False
        INDTxeUnit.Enabled = False
        INDTxeMeasureUnit.Enabled = False
        INDSpeQuantity.Enabled = False
        INDMmeDescription.Enabled = False
    End Sub

    ''' <summary>
    ''' Lanza la consulta del producto para poderlo seleccionar
    ''' sin necesidad de desplegar el control de producto
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function AssignProductWithCode() As Task
        If INDPpceProduct.Text.Trim <> String.Empty Then
            'Separo el string escrito en el control de producto
            Dim arrayCodeProduct As String() = INDPpceProduct.Text.Split(" - ")
            'Capturo el codigo del producto
            Dim codeProduct As String = arrayCodeProduct(0).Trim

            If codeProduct Is String.Empty Then
                If product IsNot Nothing Then
                    INDPpceProduct.Text = product.Code + " - " + product.Name
                End If
                Exit Function
            End If

            Using model As New MInventoryProduct(Me.Tag)
                'Consulto el producto para armar el objeto SelectProductEventArgs
                Dim resultProduct = Await model.GetInventoryProduct(INDPpceProduct.Text.Trim)
                If resultProduct.StateResult = False Then 'Si devuelve un error
                    Mensaje(EeventViewerImages.Advertencia) = resultProduct.MessageResult(0)
                    CleanControls()
                    Exit Function
                End If
                'Valido que exista el producto
                If resultProduct.ObjectEmbbeded IsNot Nothing AndAlso resultProduct.ObjectEmbbeded.Id = 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto no existe")
                    CleanControls()
                    Exit Function
                End If
                'Valido que el producto no este inactivo
                If resultProduct.ObjectEmbbeded.Status = False Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format("El producto " + resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name + " está inactivo")
                    CleanControls()
                    Exit Function
                End If
                'Instancio el objeto SelectProductEventArgs
                Dim obj As New SelectProductEventArgs
                obj.ProductId = resultProduct.ObjectEmbbeded.Id
                obj.CodeNameProduct = resultProduct.ObjectEmbbeded.Code + " - " + resultProduct.ObjectEmbbeded.Name
                'Invoco la funcion que realiza el resto de la logica
                CtrProducts1_SelectProduct(Nothing, obj)
            End Using
        ElseIf INDPpceProduct.Text.Trim = String.Empty Then
            If product IsNot Nothing Then
                INDPpceProduct.Text = product.Code + " - " + product.Description
            End If
        End If
    End Function

#End Region

#Region "Functions"


#End Region

#Region "Events"

#Region "EditValueChanging"
    ''' <summary>
    ''' Funcion para limitar cantidad de caracteres en control memoEdit
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDMmeDescription_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDMmeDescription.EditValueChanging
        If e.NewValue Is Nothing Then
            Return
        End If
        Dim maxLength As Integer = 300
        Dim edit As DevExpress.XtraEditors.MemoEdit = TryCast(sender, DevExpress.XtraEditors.MemoEdit)
        For Each str As String In edit.Lines
            If str.Length > maxLength Then
                e.Cancel = True
                Return
            End If
        Next str
    End Sub
#End Region

    ''' <summary>
    ''' Carga el popup al iniciar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub PopupProductPurchaseRequest_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        BarraBotones.OperatingUnitVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyFind)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        BarraBotones.StatusRecordVisible = True
        CleanControls()

        If Detail IsNot Nothing Then
            ProductId = Detail.InventoryProduct.Id
            Await Me.LoadControls()
        End If
    End Sub

    ''' <summary>
    ''' Define el foco cuando el control esta activo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub PopupProductsContract_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        If product Is Nothing Then
            INDPpceProduct.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Agrega el producto al listado de la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSmbAdd_Click(sender As Object, e As EventArgs) Handles INDSmbAdd.Click
        If Me.OnlyRead Then
            Me.Close
        Else
            AddInventoryProduct()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de producto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDPpceProduct_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDPpceProduct.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            Await AssignProductWithCode()
        End If
    End Sub

#End Region

    Private Async Sub CtrProducts1_SelectProduct(sender As Object, e As SelectProductEventArgs) Handles CtrProducts1.SelectProduct
        Try
            AsyncLoader(True)
            INDPpceProduct.Text = e.CodeNameProduct
            INDPpceProduct.Focus()
            INDPpceProduct.ClosePopup()
            Await GetProduct(e.ProductId)
            AsyncLoader(False)
            INDSpeQuantity.Focus()
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try

    End Sub

    Private Sub INDPpceProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDPpceProduct.QueryPopUp
        CtrProducts1.SetDataSourceProduct()
    End Sub

    Private Sub INDPpceProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDPpceProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using form As New FrmProducts
                OpenFormDialog(form)
            End Using
        End If
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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

    Private Sub AddInventoryProduct()
        Try
            INDSmbAdd.Enabled = False
            '********************* Valida los Campos esten diligenciados ****************'
            If ValidateControls() = False OrElse product Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("FieldEmpty")
                INDSmbAdd.Enabled = True
                INDPpceProduct.Focus()
                Exit Sub
            End If
            If INDSpeQuantity.EditValue = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "La cantidad debe ser mayor a 0"
                INDSmbAdd.Enabled = True
                Exit Sub
            End If
            '********************* Defino el evento de retorno ****************'
            Dim args As AddPurchaseRequestDetailEventArgs
            '********************* Verifico si el item es para actualziar o agregar ****************'
            If editModeForm Then
                If Detail.Id > 0 Then
                    Detail.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                    product.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
                Detail.InventoryProduct = product
                Detail.InventoryProductId = product.Id
                Detail.Quantity = INDSpeQuantity.EditValue
                Detail.OutstandingQuantity = INDSpeQuantity.EditValue
                Detail.Description = INDMmeDescription.EditValue
                Detail.DescriptionProduct = product.Code + " - " + product.Name
                Detail.ManufacturerName = product.ManufacturerDescription
                Detail.HealthRegistration = product.HealthRegistration
                Detail.Presentation = product.Presentation
                Detail.consumptionUnit = product.MeasureUnitDescription
                                
                args = New AddPurchaseRequestDetailEventArgs
                args.ItemPurchaseRequestDetail = Detail
                args.ListPurchaseRequestDetail = Nothing
            Else
                If ListProduct Is Nothing Then
                    ListProduct = New TrackableCollection(Of PurchaseRequestDetail)
                End If
                If ListProduct.Count <> 0 Then
                    For Each contractProduct In ListProduct.Where(Function(p) p.InventoryProductId IsNot Nothing AndAlso p.InventoryProductId.GetValueOrDefault > 0)
                        If contractProduct.InventoryProduct.Code = product.Code Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("DistributionLineDetailExist")
                            INDSmbAdd.Enabled = True
                            CleanControls()
                            Exit Sub
                        End If
                    Next
                End If

                Detail = New PurchaseRequestDetail()

                Detail.InventoryProduct = product
                Detail.InventoryProductId = product.Id
                Detail.Quantity = INDSpeQuantity.EditValue
                Detail.OutstandingQuantity = INDSpeQuantity.EditValue
                Detail.Description = INDMmeDescription.EditValue
                Detail.DescriptionProduct = product.Code + " - " + product.Name
                Detail.ManufacturerName = product.ManufacturerDescription
                Detail.HealthRegistration = product.HealthRegistration
                Detail.Presentation = product.Presentation
                Detail.consumptionUnit = product.MeasureUnitDescription
                Detail.Status = 0
                Detail.StatusName = "Registrado"


                ListProduct.Add(Detail)

                args = New AddPurchaseRequestDetailEventArgs
                args.ListPurchaseRequestDetail = ListProduct
                args.ItemPurchaseRequestDetail = Detail
            End If

            Detail = Nothing
            CleanControls()
            RaiseEvent AddProduct(Nothing, args)
            INDSmbAdd.Enabled = True
            INDPpceProduct.Focus()
            If editModeForm Then
                Me.Close()
            End If
        Catch ex As Exception
            INDSmbAdd.Enabled = True
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' evento publico para agregar un producto a la solicitud de compra
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddProduct(sender As Object, e As AddPurchaseRequestDetailEventArgs)

    Private Async Function LoadControls() As Task
        If Me.OnlyRead Then EnabledControls()
        Await GetProduct(ProductId)
        If product IsNot Nothing Then
            INDPpceProduct.Text = product.Code + " - " + product.Name
        End If
        INDSpeQuantity.EditValue = Detail.Quantity
        INDMmeDescription.EditValue = Detail.Description
    End Function

    

    Private Async Function GetProduct(ProductId As Integer) As Task
        Using model As New MInventoryProduct(Me.Tag)
            product = Await model.GetInventoryProductById(ProductId)

            INDTxeUnit.Text = product.PackingUnitDescription
            INDTxeMeasureUnit.Text = product.MeasureUnitDescription
            
        End Using
    End Function

    Private Sub PopupProductsPurchaseRequest_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Sub PopupProductsPurchaseRequest_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If (Not Me.OnlyRead) AndAlso product IsNot Nothing Then
            If Not MessageIndigo.Show(ResourceManager.GetString("CloseForm", "Payments"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                e.Cancel = True
            End If
        End If
    End Sub


End Class