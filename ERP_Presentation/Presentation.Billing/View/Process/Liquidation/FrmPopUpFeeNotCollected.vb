Imports System.Text
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Billing.MVP
Imports Infrastructure.Data.Xpo
Imports Domain.Entities
Imports System.Dynamic

Public Class FrmPopUpFeeNotCollected

#Region "Properties"
    ''' <summary>
    ''' Tipo de reporte
    ''' </summary>
    ''' <returns></returns>
    Public Property ReportType As Byte
        Get
            Return INDSleReportType.EditValue
        End Get
        Set(value As Byte)
            INDSleReportType.EditValue = value
        End Set
    End Property

    Private Property _listReportType As List(Of Tuple(Of Byte, String))
    ''' <summary>
    ''' Lista del tipo de reporte
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListReportType As List(Of Tuple(Of Byte, String))
        Get
            If _listReportType Is Nothing Then
                _listReportType = New List(Of Tuple(Of Byte, String)) _
                    From {New Tuple(Of Byte, String)(1, "Pago realizado en EAPB"),
                          New Tuple(Of Byte, String)(2, "Sin aporte de cuota")}
            End If
            Return _listReportType
        End Get
    End Property

    ''' <summary>
    ''' el valor de la cuota que se paga en la EAPB
    ''' </summary>
    ''' <returns></returns>
    Public Property Value As Decimal
        Get
            Return INDTeValue.EditValue
        End Get
        Set(value As Decimal)
            INDTeValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' registra las observaciones adicionales
    ''' </summary>
    ''' <returns></returns>
    Public Property Observations As String
        Get
            Return Me.INDMeObservations.EditValue
        End Get
        Set(value As String)
            Me.INDMeObservations.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que establece el Id del folio desde donde se lanza el form
    ''' </summary>
    ''' <returns></returns>
    Private Property _revenueControlDetailId As Integer
    Private Property _userCode As String
#End Region
#Region "Builder"
    Public Sub New(revenueControlDetailId As Integer?)

        ' This call is required by the designer.
        InitializeComponent()

        If revenueControlDetailId Is Nothing Then
            Me.Close()
        Else
            _revenueControlDetailId = revenueControlDetailId
            _userCode = SessionValues.Instance.UserIndigo
        End If

    End Sub
#End Region
#Region "Events"
    ''' <summary>
    ''' evento de cargue del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpFeeNotCollected_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDSleReportType.Properties.DataSource = Me.ListReportType
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpFeeNotCollected_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Me.INDSleReportType.Focus()
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmPopUpFeeNotCollected_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' evento cuando se cambia el tipo de reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleReportType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleReportType.EditValueChanged
        INDLciValue.HideControl(INDSleReportType.EditValue Is Nothing OrElse Me.ReportType = 2)
    End Sub

    ''' <summary>
    ''' evento click del boton guardar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDSbSaveFeeNotCollected_Click(sender As Object, e As EventArgs) Handles INDSbSaveFeeNotCollected.Click
        Try
            AsyncLoaderOwn(True)
            If Not ValidateFields() Then
                Exit Sub
            End If
            Dim feeNotCollected = Me.AssignValues()
            Using model As New MLiquidation()
                Dim obj As Object = New ExpandoObject
                obj.FeeNotCollected = feeNotCollected
                Dim Res = Await model.ExecuteActionMethod(Domain.Entities.LiquidationActionMethod.CreateFeeNotCollected, obj)
                If res Is Nothing OrElse Not res.StateResult Then
                    Me.ShowMessage(EeventViewerImages.Advertencia) = If(res?.Message, "No se pudo guardar")
                    Exit Sub
                End If
                Me.ShowMessage(EeventViewerImages.Informacion) = res?.Message
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            End Using
            Me.Close()
        Catch ex As Exception
            Me.ShowMessage(EeventViewerImages.MensajeError) = Utils.GetInnerExceptionMessageToString(ex)
        Finally
            AsyncLoaderOwn(False)
        End Try
    End Sub
#End Region

#Region "Methods"

    Public WriteOnly Property ShowMessage(ByVal Icon As Base.EeventViewerImages) As String
        Set(value As String)
            If Icon = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icon = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' metodo que valida los campos del popup
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateFields() As Boolean
        Dim errorList As New StringBuilder()
        If _revenueControlDetailId = 0 Then
            errorList.AppendLine("No hay un folio al cual asociar la información")
        End If
        If Me.INDSleReportType.EditValue Is Nothing Then
            errorList.AppendLine(Me.INDLciReportType.Text)
        End If

        If Me.ReportType = 1 AndAlso Me.Value = 0 Then
            errorList.AppendLine("Si selecciona la opcion Pago realizado en EAPB debe postular un valor")
        End If

        If errorList.Length > 0 Then
            Me.ShowMessage(EeventViewerImages.Advertencia) = String.Format("Existen campos sin diligenciar: " & vbCrLf & "{0}", errorList.ToString())
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' funcion que asigna los valores captados en el form y los devuelve en forma de entidad para guardarlos
    ''' </summary>
    ''' <returns></returns>
    Private Function AssignValues() As FeeNotCollected
        Dim feeNotCollected = New FeeNotCollected
        With feeNotCollected
            .RevenueControlDetailId = _revenueControlDetailId
            .ReportType = Me.ReportType
            .Value = Me.Value
            .Observations = If(Me.Observations, String.Empty)
            .CreationUser = _userCode
        End With
        Return feeNotCollected
    End Function

    Private Sub AsyncLoaderOwn(value As Boolean)
        Me.UseWaitCursor = value
        INDLciGenFeeNotCollected.Enabled = Not value
    End Sub
#End Region

End Class