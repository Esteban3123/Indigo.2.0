Imports Presentation.Controls.MVP
Imports Presentation.Inventory.MVP
Imports Domain.Entities

Public Class PopUpAddLevel

#Region "Properties and variables"

    ''' <summary>
    ''' Obtiene o establece la unidad de conversion
    ''' </summary>
    Public Property ConversionUnit As Long
        Get
            Return INDtxtConversionUnit.EditValue
        End Get
        Set(value As Long)
            INDtxtConversionUnit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del producto
    ''' </summary>
    Public Property ProductId As Integer
        Get
            Return INDsleProduct.EditValue
        End Get
        Set(value As Integer)
            INDsleProduct.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Evento que se ejecuta para agregar el nivel
    ''' </summary>
    Public Event AddProductLevel(ByVal sender As Object, ByVal e As EventArgs)
#End Region

#Region "Events"
    ''' <summary>
    ''' Handles the Load event of the PopUpAddLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub PopUpAddLevel_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        InitializeProduct()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the Click event of the INDsbAddLevel control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub INDsbAddLevel_Click(sender As Object, e As EventArgs) Handles INDsbAddLevel.Click
        If ProductId <> 0 Then
            Dim _product As InventoryProduct
            Using Model As New MInventoryProduct("304")
                _product = Await Model.GetInventoryProductById(ProductId)
            End Using
            If _product IsNot Nothing AndAlso _product.Id > 0 Then
                Dim eventSend As New AddProductLevelEventArgs()
                eventSend.InventoryProductLevel = _product
                eventSend.ConversionUnit = ConversionUnit
                RaiseEvent AddProductLevel(sender, eventSend)
                Me.Close()
            End If
        End If
    End Sub
#End Region

#Region "Methods and functions"
    ''' <summary>
    ''' Deshacers this instance.
    ''' </summary>
    Private Sub Deshacer()

    End Sub

    ''' <summary>
    ''' Initializes the product.
    ''' </summary>
    Private Sub InitializeProduct()
        Using Model As New MBusqueda
            Dim filter() As Object = {1}
            INDsleProduct.Properties.DataSource = Model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListInventoryProductByProductType, filter)
        End Using
    End Sub
#End Region

#Region "BarButton Events"

#End Region

End Class