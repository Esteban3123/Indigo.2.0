Imports Presentation.Billing.MVP
Imports Domain.Entities

Public Class FrmItemPackageDetail

#Region "Properties"

    ''' <summary>
    ''' toma el nombre del item empaquetado para establecerlo como titulo del modal
    ''' </summary>
    Public WriteOnly Property ItemPackaged As String
        Set(value As String)
            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Id del item empaquetado
    ''' </summary>
    Public Property ServiceOrderDetailDistributionId As Integer

    ''' <summary>
    ''' Id del detalle de la orden de servicio empaquetado
    ''' </summary>
    Public Property ServiceOrderDetailId As Integer

#End Region

#Region "Variables"

#End Region

#Region "Handlers"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ServiceOrderDetailDistributionId = Nothing
        ServiceOrderDetailId = Nothing
    End Sub


    ''' <summary>
    ''' Handles the Load event of the FrmItemPackageDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmItemPackageDetail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ServiceOrderDetailId > 0 Then
            LoadItemsPackaged()
        Else
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Handles the KeyDown event of the FrmItemPackageDetail control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub FrmItemPackageDetail_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Methods and Functions"
    Private Async Sub LoadItemsPackaged()
        'Consultamos como si desempaquetaramos
        Using model As New MServiceOrder(Me.Tag)
            Dim serviceOrderDetail = Await model.GetServiceOrderDetailById(ServiceOrderDetailId)
            If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.Id > 0 Then
                If serviceOrderDetail IsNot Nothing AndAlso serviceOrderDetail.ServiceOrderDetail1 IsNot Nothing AndAlso serviceOrderDetail.ServiceOrderDetail1.Count > 0 Then
                    Dim lstServiceOrderDetailDistribution As New List(Of ServiceOrderDetailDistribution)()
                    For Each sod As ServiceOrderDetail In serviceOrderDetail.ServiceOrderDetail1
                        Dim serviceOrderDetailDistribution As New ServiceOrderDetailDistribution
                        serviceOrderDetailDistribution.ItemCode = sod.ItemCode
                        serviceOrderDetailDistribution.ItemDescription = sod.ItemDescription
                        serviceOrderDetailDistribution.ItemQuantity = sod.InvoicedQuantity
                        serviceOrderDetailDistribution.ItemTotalSalesPrice = sod.TotalSalesPrice
                        serviceOrderDetailDistribution.GrandTotalSalesPrice = sod.GrandTotalSalesPrice
                        lstServiceOrderDetailDistribution.Add(serviceOrderDetailDistribution)
                    Next
                    GdcServices.DataSource = lstServiceOrderDetailDistribution
                End If
            End If
        End Using
    End Sub
#End Region

End Class