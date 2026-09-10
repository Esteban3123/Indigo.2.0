Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Presentation.Controls.MVP

Public Class FrmPercentPayment

    ''' <summary>
    ''' Obtiene o establece el título del formulario
    ''' </summary>
    ''' <value>
    ''' The title.
    ''' </value>
    Public Property Title As String
        Get
            Return Me.Text
        End Get
        Set(value As String)

            Me.Text = value
        End Set
    End Property

    ''' <summary>
    ''' tipo de visualizacion (1 - pago total, 2 - pago porcentual)
    ''' </summary>
    Property Type As Integer

    ''' <summary>
    ''' Obtiene o establece el datasource de conceptos de pagos
    ''' </summary>
    ''' <value>
    ''' The paymet concept datasource.
    ''' </value>
    Public Property PaymetConceptDatasource As LinqInstantFeedbackSource
        Get
            Return CType(INDslePaymentConcept.Properties.DataSource, LinqInstantFeedbackSource)
        End Get
        Set(value As LinqInstantFeedbackSource)
            INDslePaymentConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the payment percent.
    ''' </summary>
    ''' <value>
    ''' The payment percent.
    ''' </value>
    Public Property PaymentPercent As Integer
        Get
            Return CType(INDtxtPaymentPercent.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDtxtPaymentPercent.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Occurs when [payment concept].
    ''' </summary>
    Public Event PaymentPercentEvent(sender As Object, e As PayPercentEventArgs)

    ''' <summary>
    ''' obtiene o establece el id del concepto de pago
    ''' </summary>
    ''' <value>
    ''' The payment concept identifier.
    ''' </value>
    Property PaymentConceptId As Integer
        Get
            Return INDslePaymentConcept.EditValue
        End Get
        Set(value As Integer)
            INDslePaymentConcept.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Handles the Click event of the INDsbAcept control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsbAcept_Click(sender As Object, e As EventArgs) Handles INDsbAcept.Click
        Dim eventPayment As New PayPercentEventArgs
        eventPayment.percentValue = PaymentPercent
        eventPayment.Type = Me.Type
        eventPayment.PaymentConceptId = Convert.ToInt32(INDslePaymentConcept.EditValue)
        RaiseEvent PaymentPercentEvent(sender, eventPayment)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the FrmPercentPayment control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPercentPayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using ModelXpo As New MBusqueda
            Dim filter As Object() = {True}
            Dim _listPaymentConcept As LinqInstantFeedbackSource = ModelXpo.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAllPaymentConceptByState, filter)
            PaymetConceptDatasource = _listPaymentConcept
        End Using
        If Type = 2 Then
            INDliPaymentConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliPaymentPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDliPaymentConcept.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliPaymentPercent.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub
End Class