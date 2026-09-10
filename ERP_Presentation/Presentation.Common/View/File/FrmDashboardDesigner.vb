Imports System.IO
Imports System.Windows.Forms
Imports DevExpress.DashboardWin
Imports DevExpress.XtraSplashScreen
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.CloudAgent

Public Class FrmDashboardDesigner

    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)

    Public Sub New()
        InitializeComponent()
        AddHandler CtrDashBoardDesign1.INDDashboardDesigner.DashboardSaving, AddressOf INDDashboardDesigner_DashboardSaving
        AddHandler CtrDashBoardDesign1.INDDashboardDesigner.DashboardOpening, AddressOf INDDashboardDesigner_DashboardOpening
        AddHandler CtrDashBoardDesign1.INDDashboardDesigner.DashboardClosing, AddressOf INDDashboardDesigner_DashboardClosing
    End Sub

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

    Private Sub FrmDashboardDesigner_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Async Sub INDDashboardDesigner_DashboardSaving(sender As Object, e As DevExpress.DashboardWin.DashboardSavingEventArgs)
        If e.Command = DevExpress.DashboardWin.DashboardSaveCommand.Save Then
            e.Handled = True
            If waitForm.IsSplashFormVisible Then
                waitForm.CloseWaitForm()
            End If
            waitForm.ShowWaitForm()
            Dim ms As New MemoryStream, m_Buffer() As Byte, m_XML As String = ""
            CtrDashBoardDesign1.INDDashboardDesigner.Dashboard.SaveToXml(ms)
            m_Buffer = ms.ToArray()
            ms.Flush()
            ms.Close()
            m_XML = System.Text.Encoding.UTF8.GetString(m_Buffer)
            ms.Dispose()

            If BalancedSelected Is Nothing OrElse BalancedSelected.Id = 0 Then
                Using modal As New FrmBalancedScorecardName()
                    Dim frmTransparent As New FrmTransparent(modal, False)
                    If frmTransparent.ShowDialog(Me) = DialogResult.OK Then
                        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBalancedScorecardAsync(0, modal.BalancedScoreCardName, m_XML, SessionValues.Instance)
                        If res.StateResult Then
                            Mensaje(EeventViewerImages.Informacion) = "El registro se guardó con éxito"
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = res.Message
                        End If
                    End If
                    waitForm.CloseWaitForm()
                End Using
            Else
                Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.SaveBalancedScorecardAsync(BalancedSelected.Id, "", m_XML, SessionValues.Instance)
                If res.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = "El registro se actualizó con éxito"
                Else
                    Mensaje(EeventViewerImages.Advertencia) = res.Message
                End If
                waitForm.CloseWaitForm()
            End If
        End If
    End Sub

    Public Property BalancedSelected As Infrastructure.Data.Xpo.CommonRepository.CommonBalancedScorecardXpo

    Private Sub INDDashboardDesigner_DashboardOpening(sender As Object, e As DashboardOpeningEventArgs)
        e.Handled = True
        Using modal As New FrmBalancedScoreCardList()
            Dim frmTransparent As New FrmTransparent(modal, False)
            If frmTransparent.ShowDialog(Me) = DialogResult.OK Then
                BalancedSelected = modal.BalancedSelected
                'Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoCommonERP.LoadBalancedScorecardAsync(modal.BalancedScoreCardName, m_XML, SessionValues.Instance)
                Dim ms As New MemoryStream, m_Buffer() As Byte, m_XML As String = ""
                m_Buffer = System.Text.Encoding.UTF8.GetBytes(modal.BalancedSelected.Value.Substring(1, modal.BalancedSelected.Value.Length - 1))
                ms.Write(m_Buffer, 0, m_Buffer.Length)
                ms.Seek(0, SeekOrigin.Begin)
                CtrDashBoardDesign1.INDDashboardDesigner.Dashboard.LoadFromXml(ms)
                ms.Flush()
                ms.Close()
                ms.Dispose()
            End If
        End Using
    End Sub

    Private Sub INDDashboardDesigner_DashboardClosing(sender As Object, e As DashboardClosingEventArgs)
        BalancedSelected = Nothing
    End Sub

    Private Function InvokeMessageBox() As DialogResult
        Return SaveFileDialog1.ShowDialog(Me)
    End Function

End Class