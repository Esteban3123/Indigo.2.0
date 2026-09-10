Imports DevExpress.XtraEditors
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports Presentation.Base
Imports Presentation.Inventory.MVP

Public Class CtrProduct

#Region "Properties and Variables"
    ''' <summary>
    ''' Gets or sets the bar code.
    ''' </summary>
    Public Property BarCode As String
        Get
            Return INDtxtBarCode.EditValue
        End Get
        Set(value As String)
            INDtxtBarCode.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the inventory product.
    ''' </summary>
    Public Property InventoryProduct As InventoryProduct

    ''' <summary>
    ''' Gets or sets the list bar code.
    ''' </summary>
    Public Property ListBarCode As List(Of ProductBarcode)

    ''' <summary>
    ''' Evento para tomar el producto del frontal principal
    ''' </summary>
    Public Event TakeInventoryProduct()
#End Region

#Region "Events"

    ''' <summary>
    ''' Handles the Load event of the CtrProduct control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrProduct_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim listActions As New List(Of eAcciones)()
        listActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvBarCode, listActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvBarCode.Columns
            If col.Name = "colActions" Then
                col.Width = 200
            End If
        Next
    End Sub

    ''' <summary>
    ''' Handles the MouseEnter event of the PopupContainerEdit1 control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PopupContainerEdit1_MouseEnter(sender As Object, e As EventArgs) Handles INDpceExistence.MouseEnter, INDpceSuppliers.MouseEnter, INDpceBarCode.MouseEnter
        ' CType(sender, PopupContainerEdit).BackColor = System.Drawing.Color.FromArgb(CType(CType(30, Byte), Integer), CType(CType(178, Byte), Integer), CType(CType(253, Byte), Integer))
    End Sub

    ''' <summary>
    ''' Handles the MouseLeave event of the INDpceExistence control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDpceExistence_MouseLeave(sender As Object, e As EventArgs) Handles INDpceExistence.MouseLeave, INDpceSuppliers.MouseLeave, INDpceBarCode.MouseLeave
        ' CType(sender, PopupContainerEdit).BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(223, Byte), Integer))
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddBarCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAddBarCode_Click(sender As Object, e As EventArgs) Handles INDsbAddBarCode.Click
        If ListBarCode Is Nothing Then
            ListBarCode = New List(Of ProductBarcode)()
        End If
        Dim _barCode As New ProductBarcode()
        With _barCode
            .CreationDate = GetDateServer()
            .Barcode = BarCode
        End With
        InventoryProduct.ProductBarcode.Add(_barCode)
        ListBarCode.Add(_barCode)
        BarCode = String.Empty
        INDtxtBarCode.Focus()
        INDgcBarCode.DataSource = ListBarCode
        INDgcBarCode.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Handles the ButtonAction event of the IndigoGridView1_Click control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Dim _barCode As ProductBarcode = CType(INDgvBarCode.GetFocusedRow(), ProductBarcode)
        _barCode.ChangeTracker.State = Domain.Base.Entities.ObjectState.Deleted
        ListBarCode = InventoryProduct.ProductBarcode.ToList()
        INDgcBarCode.DataSource = ListBarCode
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDpceBarCode control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDpceBarCode_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceBarCode.QueryPopUp
        RaiseEvent TakeInventoryProduct()
        If InventoryProduct IsNot Nothing Then
            ListBarCode = InventoryProduct.ProductBarcode.ToList()
            INDgcBarCode.DataSource = ListBarCode
            INDtxtBarCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the QueryPopUp event of the INDPceExistence
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceExistence_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceExistence.QueryPopUp
        RaiseEvent TakeInventoryProduct()
        If InventoryProduct IsNot Nothing Then
            Using modelPhysical As New MCtrPhysicalInventory("304")
                Dim ListPhyisicalInventory = modelPhysical.GetListPhysicalInventoryByProduct(InventoryProduct.Id)
                INDGcPhysicalInventory.DataSource = ListPhyisicalInventory
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceSuppliers_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDpceSuppliers.QueryPopUp
        RaiseEvent TakeInventoryProduct()
        If InventoryProduct IsNot Nothing Then
            Using modelPhysical As New MCtrPhysicalInventory("304")
                INDgcSuppliers.DataSource = modelPhysical.GetListSuppliers(InventoryProduct.Id)
            End Using
        End If
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Obtiene la fecha del servidor
    ''' </summary>
    Public Function GetDateServer() As DateTime
        Using _model As New MformBase()
            Return _model.GetDateServer
        End Using
    End Function
#End Region

    Private Sub Control_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
        AddHandler DevExpress.LookAndFeel.UserLookAndFeel.Default.StyleChanged, AddressOf Control_StyleChanged
    End Sub

    Private Sub Control_StyleChanged(sender As Object, e As EventArgs)
        Me.ApplyStyleSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveSkinName)
    End Sub

    Private Sub ApplyStyleSkin(ByVal skinName As String)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControl4)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem5)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem6)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LayoutControlItem2)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(LabelControl1)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceExistence)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceBarCode)
        Presentation.Resources.ThemeResourceManager.ApplyStyleThemeToControl(INDpceSuppliers)
    End Sub

End Class
