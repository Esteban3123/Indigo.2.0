Imports System.IO
Imports DevExpress.XtraSplashScreen
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Base

Public Class CtrBalancedScorecardViewer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private waitForm As New SplashScreenManager(Me, GetType(Presentation.Controls.wfMain), False, True, ParentType.UserControl)
    Public Property BalancedSelected As CommonBalancedScorecardXpo

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

    Private IsLoading As Boolean = False
    Public Async Sub searchFirstItem()
        If DesignMode Then
            Exit Sub
        End If
        IsLoading = True
        Dim query As New System.Text.StringBuilder()
        query.AppendLine(" SELECT TOP 1 bg.Id, bg.UserGroup, sc.[Value], bg.BalancedScorecardId, sc.Name As BalancedScorecardName ")
        query.AppendLine(" FROM Common.BalancedScorecardByGroup bg													  ")
        query.AppendLine(" INNER JOIN Common.BalancedScorecard sc on bg.BalancedScorecardId = sc.Id					  ")
        query.AppendLine($" WHERE UserGroup = {SessionValues.Instance.UserGroup}									  ")
        Dim result As DataTable = Await Presentation.CloudAgent.IndigoConecta.Instancia.CurrentCloud _
            .IndigoBilling.ExecuteQueryDtAsync(query.ToString(), SessionValues.Instance.TransactionalContainer)
        If result.Rows.Count > 0 Then
            INDsleBalancedScorecard.Properties.NullText = result.Rows(0).Item("BalancedScorecardName").ToString()
            INDsleBalancedScorecard.EditValue = CInt(result.Rows(0).Item("BalancedScorecardId").ToString())

            Dim valueXml As String = result.Rows(0).Item("Value").ToString()
            Dim ms As New MemoryStream, m_Buffer() As Byte, m_XML As String = ""
            m_Buffer = System.Text.Encoding.UTF8.GetBytes(valueXml.Substring(1, valueXml.Length - 1))
            ms.Write(m_Buffer, 0, m_Buffer.Length)
            ms.Seek(0, SeekOrigin.Begin)
            DashboardViewer1.LoadDashboard(ms)
            ms.Flush()
            ms.Close()
            ms.Dispose()

        End If
        IsLoading = False
    End Sub

    Private Sub CtrBalancedScorecardViewer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        searchFirstItem()
        INDsleBalancedScorecard.Focus()
    End Sub

    Private Sub INDsleBalancedScorecard_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleBalancedScorecard.QueryPopUp
        If INDsleBalancedScorecard.Properties.DataSource Is Nothing Then
            INDsleBalancedScorecard.Properties.DataSource = XpoServiceEx.Instance(SessionValues.Instance.TransactionalContainer) _
                .CommonService.GetBalancedScorecard(SessionValues.Instance.UserGroup)
        End If
    End Sub

    Private Sub INDsleBalancedScorecard_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBalancedScorecard.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Delete Then
            INDsleBalancedScorecard.EditValue = Nothing
            INDsleBalancedScorecard.Properties.NullText = String.Empty
            INDsleBalancedScorecard.Properties.DataSource = Nothing
        End If
    End Sub

    Private Sub INDsleBalancedScorecard_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDsleBalancedScorecard.EditValueChanging
        If IsLoading Then
            Exit Sub
        End If
        If waitForm.IsSplashFormVisible Then
            waitForm.CloseWaitForm()
        End If
        waitForm.ShowWaitForm()
        If e.NewValue Is Nothing OrElse e.NewValue.ToString().Equals("") Then
            BalancedSelected = Nothing
            DashboardViewer1.Dashboard = Nothing
        Else
            BalancedSelected = CType(CType(INDgvBalancedscorecard.GetFocusedRow(), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, CommonBalancedScorecardXpo)
            Dim ms As New MemoryStream, m_Buffer() As Byte, m_XML As String = ""
            m_Buffer = System.Text.Encoding.UTF8.GetBytes(BalancedSelected.Value.Substring(1, BalancedSelected.Value.Length - 1))
            ms.Write(m_Buffer, 0, m_Buffer.Length)
            ms.Seek(0, SeekOrigin.Begin)
            DashboardViewer1.LoadDashboard(ms)
            ms.Flush()
            ms.Close()
            ms.Dispose()
        End If
        waitForm.CloseWaitForm()
    End Sub

End Class
