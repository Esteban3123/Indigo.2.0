Imports Infrastructure.Data.Xpo
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.Xpo
Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports Domain.Crystal.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

Public Class FrmRateBed

#Region "Properties"
    Property BedId As Integer
        Get
            Return CType(INDsleCode.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleCode.EditValue = value
        End Set
    End Property

    Property BedXpo As XPInstantFeedbackSource
        Get
            Return INDsleCode.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCode.Properties.DataSource = value
        End Set
    End Property

    Dim form As FrmAddBedRate
#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        form = Nothing
    End Sub


    Private Sub FrmRateBed_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.indigo = SessionValues.Instance
        Me.ToolBar.Hide()
        CreateTipoLiquidacion()
        Dim listAction As New List(Of eAcciones)
        listAction.Add(eAcciones.Edit)
        listAction.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDgvBedRate, listAction)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvBedRate.Columns
            If col.Name = "colActions" Then
                col.Width = 120
            End If
        Next
        form = New FrmAddBedRate()
        IndigoGridView1.MoreInfoColunmns(INDgvBedRate)
    End Sub

    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        OpenPopUpAddBedRate()
    End Sub

    Private Sub INDsleCode_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCode.EditValueChanged
        If INDsleCode.EditValue IsNot Nothing Then
            INDsbAdd.Enabled = True
            SearchBedRateByBedCode()
        Else
            INDsbAdd.Enabled = False
        End If
    End Sub

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag)
            Case "Edit", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Edit")
                OpenPopUpAddBedRate(True)
            Case "Remove", Infrastructure.CrossCutting.Resources.ResourceManager.GetString("Remove")
                RemoveBedRate()
        End Select
    End Sub
#End Region

#Region "Methods"
    Private Sub OpenPopUpAddBedRate(Optional edit As Boolean = False)
        If INDsleCode.EditValue IsNot Nothing Then
            form.CODICAMAS = BedId
            form.BedDescription = INDsleCode.Text
            form.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            form.BedRateDatasource = CType(INDgcBedRates.DataSource, List(Of CHGENTARI))
            If edit Then
                Dim bedRate As CHGENTARI = CType(INDgvBedRate.GetFocusedRow(), CHGENTARI)
                form.IsEdit = True
                form.BedRate = bedRate
                form.BedRateDatasource = CType(INDgcBedRates.DataSource, List(Of CHGENTARI)).Where(Function(o) o.CODCONCEC <> bedRate.CODCONCEC).ToList()
            End If
            AddHandler form.OnRefreshBedRateDatasource, AddressOf SearchBedRateByBedCode
            Dim transparent As New FrmTransparent(form, False)
            transparent.ShowDialog(Me)
        End If
    End Sub

    Private Async Sub RemoveBedRate()
        Dim bedRate As CHGENTARI = CType(INDgvBedRate.GetFocusedRow(), CHGENTARI)
        If bedRate IsNot Nothing AndAlso bedRate.CODCONCEC > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MBedRate(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteBedRateAsync(bedRate)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                            Await SearchBedRateByBedCode()
                            AsyncLoader(False)
                        Else
                            If result.Message IsNot Nothing Then
                                Dim listError As New StringBuilder()
                                listError.AppendLine(ResourceManager.GetString("ErrorListMessage"))
                                listError.AppendLine(result.Message)
                                Mensaje(EeventViewerImages.MensajeError) = result.Message
                            End If
                        End If
                    End Using
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            End If
        End If
    End Sub

    Private Async Function SearchBedRateByBedCode() As Task
        AsyncLoader(True)
        Using model As New MBedRate(Me.Tag)
            Dim res = Await model.GetBedRatebyBedCodeAsync(BedId)
            If res.StateResult Then
                Me.INDgcBedRates.DataSource = res.ObjectEmbbeded
                form.BedRateDatasource = res.ObjectEmbbeded
            End If
        End Using
        AsyncLoader(False)
        'Me.INDgcBedRates.DataSource = XpoService.GetBedRateByBedId(Me.indigo.HisContainer, BedId)
    End Function

    Private Sub INDsleCode_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCode.QueryPopUp
        If BedXpo Is Nothing Then
            Me.BedXpo = XpoServiceEx.Instance(indigo.HisContainer).CrystalService.ListAllBeds()
        End If
    End Sub

    'Tipo de liquidacion de estancia
    '1: Observacion Urgencias
    '2: Recuperacion Post-Quirurgico
    '3: Hospitalaria
    Private Sub CreateTipoLiquidacion()
        Dim tipLiquid As New List(Of Tuple(Of Byte, String))()
        tipLiquid.Add(New Tuple(Of Byte, String)(1, "Observacion Urgencias"))
        tipLiquid.Add(New Tuple(Of Byte, String)(2, "Recuperacion Post-Quirurgico"))
        tipLiquid.Add(New Tuple(Of Byte, String)(3, "Hospitalaria"))
        INDrptTipLiquidac.DataSource = tipLiquid
    End Sub

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

End Class