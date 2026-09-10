'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Oscar Stiven Astudillo
' Created          : 2024-01-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Drawing
Imports System.Text
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.Inventory.FrmConsignmentCostList
Imports Presentation.Inventory.MVP
#End Region

Public Class FrmConsignmentCostListDetail


#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el datasource del producto
    ''' </summary>
    Private Property ProductDatasource As XPInstantFeedbackSource
        Get
            Return CType(INDSluProduct.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSluProduct.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del producto 
    ''' </summary>
    Public Property ProductId As Integer
        Get
            Return INDSluProduct.EditValue
        End Get
        Set(value As Integer)
            INDSluProduct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Id del producto 
    ''' </summary>
    Public Property CostNew As Double
        Get
            Return INDTeCostNew.EditValue
        End Get
        Set(value As Double)
            INDTeCostNew.EditValue = value
        End Set
    End Property

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
#End Region


#Region "Variables"

    ''' <summary>
    ''' Representa al formulario padre
    ''' </summary>
    Public FrmConsignmentCostListDetail
    ''' <summary>
    ''' Objeto del detalle
    ''' </summary>
    Public ConsignmentCostListDetail As ConsignmentCostListDetail

    ''' <summary>
    ''' Permite saber si se esta editando un registro
    ''' </summary>
    Public EditMode As Boolean

    ''' <summary>
    ''' Representa la lista de los detalles ya agregados
    ''' </summary>
    Public ListConsignmentProducts As List(Of ConsignmentCostListDetail)

    ''' <summary>
    ''' Representa el presentador del frm
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PConsignmentCostList


    ''' <summary>
    ''' Representa el tipo de producto - insumo - producto
    ''' </summary>
    Dim ProductType As String

#End Region

#Region "Events"
    Public Event AddConsignmentProductDetail(sender As Object, e As AddConsignmentProductEventArgs)
#End Region



#Region "Methods"


    ''' <summary>
    ''' Obtiene la informacion para mostrar los productos
    ''' </summary>
    Private Sub InitializeProducts()
        ProductDatasource = Presenter.ListProductsBySupplierWarehouse()
    End Sub
    ''' <summary>
    ''' Devuelve listado de errores 
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForms() As String
        Dim errorList As New StringBuilder()
        If (ProductId = 0) Then
            errorList.AppendLine(String.Format(ResourceManager.GetString("FieldEmptyName"), "Producto"))
        End If
        If (CostNew <= 0) Then
            errorList.AppendLine(String.Format("No puede ser un número negativo", "Costo nuevo"))
        End If
        If Not EditMode Then
            If ListConsignmentProducts.Any(Function(x) x.ProductId = ProductId AndAlso x.CostNew = CostNew) Then
                errorList.AppendLine("El producto ya existe con el mismo costo y no se agregó: " + INDSluProduct.Text)
            End If
        End If
        Return errorList.ToString()
    End Function


    ''' <summary>
    '''  Adiciona un nuevo producto
    ''' </summary>
    ''' <returns></returns>
    Private Function AddProduct()
        Dim errors = ValidateControlsForms()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Function
        End If
        AssigningValues()
        RaiseEvent AddConsignmentProductDetail(Nothing, New AddConsignmentProductEventArgs With
           {
               .EditMode = EditMode,
               .ConsignmentCostListDetail = ConsignmentCostListDetail
           }
       )
        If EditMode Then
            Me.Close()
        End If
        CleanControls()
    End Function


    ''' <summary>
    ''' Asigna los valores que se van a guardar
    ''' Obitiene el tipo de producto, para poder agrupar en la rejilla
    ''' </summary>
    Private Sub AssigningValues()
        Dim type = Presenter.GetProductType(ProductId)
        If type.ProductTypeId.Class = 2 Then
            ProductType = "Producto"
        Else
            ProductType = "Insumo"
        End If
        With ConsignmentCostListDetail
            .ProductId = ProductId
            .CostNew = CostNew
            .ProductCodeName = INDSluProduct.Text
            .ProductType = ProductType
        End With
    End Sub

    ''' <summary>
    ''' limpia controles del formulario
    ''' </summary>
    Private Sub CleanControls()
        ProductId = Nothing
        CostNew = Nothing
        ConsignmentCostListDetail = New ConsignmentCostListDetail
    End Sub

#End Region


#Region "Handlers"
    Private Sub INDSluProduct_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSluProduct.QueryPopUp
        If ProductDatasource Is Nothing Then
            InitializeProducts()
        End If
    End Sub

    Private Sub INDSluProduct_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSluProduct.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmProducts
                Formulario.ViewModeEditHold = True
                Formulario.MinimizeBox = False
                Formulario.MaximizeBox = False
                Formulario.Size = New Size(780, 700)
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                InitializeProducts()
            End Using
        End If
    End Sub
#End Region


#Region "Load"
    Private Sub FrmConsignmentCostListDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CtrBarraBotones2.OperatingUnitVisible = False
        CtrBarraBotones2.PrepareToolbar(eAction.OnlyFind)
        CtrBarraBotones2.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        CtrBarraBotones2.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ActiveInactive) = True
        CtrBarraBotones2.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        CtrBarraBotones2.StatusRecordVisible = False
        Presenter = New PConsignmentCostList(Nothing, False)
        CleanControls()
    End Sub

    Private Sub INDSbSaveProduct_Click(sender As Object, e As EventArgs) Handles INDSbSaveProduct.Click
        AddProduct()
    End Sub


#End Region

End Class