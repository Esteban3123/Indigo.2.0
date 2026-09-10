Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Popup
Imports Domain.Entities
Imports Presentation.Base
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

Public Class FrmPopupReleaseLine

    Public Property CampaignDetailId As Integer
    Public Property CMConfigurationId As Integer
    Private releaseLine As ReleaseLine

    Public Property PermissionsForm As Dictionary(Of Integer, String)
        Get
            Return BarraBotones.PermissionsForm
        End Get
        Set(value As Dictionary(Of Integer, String))
            BarraBotones.PermissionsForm = value
        End Set
    End Property

    Public WriteOnly Property ProductionLineCodeName As String
        Set(value As String)
            INDTeProductionLine.EditValue = value
        End Set
    End Property

    Public WriteOnly Property UnitDoseTypeCodeName As String
        Set(value As String)
            INDTeUnitDoseTye.EditValue = value
        End Set
    End Property

    Public Property UnitDoseTypeMSClass As Integer
    Public Property CampaignDetailStatus As Byte
    Public Property CurrentUserRole As UserRoleMixingStation
    Public Property AuxiliarExists As Boolean

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Async Sub FrmPopupReleaseLine_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadAdecuationAreas()
        EnableControls()
        Await InitForm()
    End Sub

    Private Sub EnableControls()
        INDLciConditioningItem1.HideLayout()
        INDLciConditioningItem2.HideLayout()
        INDLciConditioningItem3.HideLayout()
        INDLciConditioningItem4.HideLayout()
        INDLciConditioningItem5.HideLayout()
        INDLciConditioningItem6.HideLayout()
        INDLciConditioningItem7.HideLayout()
        INDLciConditioningItem8.HideLayout()
        INDLciAdecuationItem1.HideLayout()
        INDLciAdecuationItem2.HideLayout()
        INDLciAdecuationItem3.HideLayout()
        INDLciAdecuationItem4.HideLayout()
        INDLciAdecuationItem5.HideLayout()
        INDLciAdecuationItem6.HideLayout()
        INDLciAdecuationItem7.HideLayout()
        INDLciAdecuationItem8.HideLayout()
        INDLciAdecuation.HideLayout()
        INDLciAcondition.HideLayout()

        If CurrentUserRole = UserRoleMixingStation.QF_Produccion OrElse CurrentUserRole = UserRoleMixingStation.QF_Calidad OrElse CurrentUserRole = UserRoleMixingStation.DirectorTecnico _
            OrElse Not AuxiliarExists Then
            INDLciAdecuationItem1.ShowLayout()
            INDLciAdecuationItem2.ShowLayout()
            INDLciAdecuationItem3.ShowLayout()
            INDLciAdecuationItem4.ShowLayout()
            INDLciAdecuationItem5.ShowLayout()
            INDLciAdecuationItem6.ShowLayout()
            INDLciAdecuationItem7.ShowLayout()
            INDLciAdecuationItem8.ShowLayout()
            INDLciAdecuation.ShowLayout()
        End If

        If CurrentUserRole = UserRoleMixingStation.Auxiliar OrElse CurrentUserRole = UserRoleMixingStation.QF_Calidad OrElse CurrentUserRole = UserRoleMixingStation.DirectorTecnico _
            OrElse Not AuxiliarExists Then
            INDLciConditioningItem1.ShowLayout()
            INDLciConditioningItem2.ShowLayout()
            INDLciConditioningItem3.ShowLayout()
            INDLciConditioningItem4.ShowLayout()
            INDLciConditioningItem5.ShowLayout()
            INDLciConditioningItem6.ShowLayout()
            INDLciConditioningItem7.ShowLayout()
            INDLciConditioningItem8.ShowLayout()
            INDLciAcondition.ShowLayout()
        End If
    End Sub

    Private Sub FrmPopupReleaseLine_KeyDown(sender As Object, e As Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    Private Async Function InitForm() As Task
        Await CleanControls()
        'Consultamos la liberacion por id de la campaña
        Await LoadReleaseLine()
    End Function

    Public Async Function CleanControls() As Task
        AsyncLoader(True)
        Using model As New Controls.MVP.MformBase()
            Dim now = Await model.GetDateServerAsync()
            INDDeCentralMezclas.EditValue = now
            INDDeEncendidoAires.EditValue = now
            INDDeEncendidoCPI.EditValue = now
            INDDeInicioPreparacion.EditValue = now
        End Using
        AsyncLoader(False)
        '2, "NPT"
        '3, "Antibioticoterapia"
        '4, "Citostático"
        '5, "Reempaque"
        '7, "Reenvase"
        '9, "Magistral"
        '10, "Otros estériles"

        INDGleAdecuantionArea.EditValue = CByte(IIf({1, 2, 3, 4, 7, 8, 9}.Contains(UnitDoseTypeMSClass), 1, 2))
        INDGleApplyPressure.EditValue = CByte(INDGleAdecuantionArea.EditValue) = 1 AndAlso UnitDoseTypeMSClass <> 2
        INDGleApplySpeed.EditValue = UnitDoseTypeMSClass = 2
        INDGleApplySpeed.EditValue = 0

        INDSleWorkingArea.EditValue = Nothing
        INDGleAdecuationItem1.EditValue = Nothing
        INDGleAdecuationItem2.EditValue = Nothing
        INDGleAdecuationItem3.EditValue = Nothing
        INDGleAdecuationItem4.EditValue = Nothing
        INDGleAdecuationItem5.EditValue = Nothing
        INDGleAdecuationItem6.EditValue = Nothing
        INDGleAdecuationItem7.EditValue = Nothing
        INDGleAdecuationItem8.EditValue = Nothing
        INDGleConditioningItem1.EditValue = Nothing
        INDGleConditioningItem2.EditValue = Nothing
        INDGleConditioningItem3.EditValue = Nothing
        INDGleConditioningItem4.EditValue = Nothing
        INDGleConditioningItem5.EditValue = Nothing
        INDGleConditioningItem6.EditValue = Nothing
        INDGleConditioningItem7.EditValue = Nothing
        INDGleConditioningItem8.EditValue = Nothing
        INDMeCommentary.EditValue = Nothing

        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False
    End Function

    Public Sub LoadAdecuationAreas()
        INDGleAdecuantionArea.Properties.DataSource = {New Tuple(Of Byte, String)(1, "- Esteril -"), New Tuple(Of Byte, String)(2, "- No Esteril -")}.ToList()
    End Sub

    Public Sub AssigValues()
        With releaseLine
            .CampaignDetailId = CampaignDetailId
            .WorkingAreaId = INDSleWorkingArea.EditValue
            .AirIgnitionTime = INDDeEncendidoAires.EditValue
            .CPIIgnitionTime = INDDeEncendidoCPI.EditValue
            .EntryMixingStationTime = INDDeCentralMezclas.EditValue
            .PreparationStartTime = INDDeInicioPreparacion.EditValue
            .IsSterile = CByte(INDGleAdecuantionArea.EditValue) = 1
            .AdequacyItem1 = INDGleAdecuationItem1.EditValue
            .AdequacyItem2 = INDGleAdecuationItem2.EditValue
            .AdequacyItem3 = INDGleAdecuationItem3.EditValue
            .AdequacyItem4 = INDGleAdecuationItem4.EditValue
            .AdequacyItem5 = INDGleAdecuationItem5.EditValue
            .AdequacyItem6 = INDGleAdecuationItem6.EditValue
            .AdequacyItem7 = INDGleAdecuationItem7.EditValue
            .AdequacyItem8 = INDGleAdecuationItem8.EditValue
            .ConditioningItem1 = INDGleConditioningItem1.EditValue
            .ConditioningItem2 = INDGleConditioningItem2.EditValue
            .ConditioningItem3 = INDGleConditioningItem3.EditValue
            .ConditioningItem4 = INDGleConditioningItem4.EditValue
            .ConditioningItem5 = INDGleConditioningItem5.EditValue
            .ConditioningItem6 = INDGleConditioningItem6.EditValue
            .ConditioningItem7 = INDGleConditioningItem7.EditValue
            .ConditioningItem8 = INDGleConditioningItem8.EditValue
            .ApplyItem9 = INDGleApplyPressure.EditValue
            .ApplyItem11 = INDGleApplySpeed.EditValue
            .ValueItem9 = INDTePresure.EditValue
            .ValueItem11 = INDGleApplySpeed.EditValue
            .Observation = INDMeCommentary.EditValue
        End With
    End Sub

    Private Async Function LoadReleaseLine() As Task
        Try
            AsyncLoader(True)
            Using model As New MCampaign(Tag)
                releaseLine = Await model.GetReleaseLineByCampaignDetailId(CampaignDetailId)
                If releaseLine Is Nothing Then
                    releaseLine = New ReleaseLine()
                Else
                    LoadControls(releaseLine)
                End If
            End Using
            AsyncLoader(False)

            If CampaignDetailStatus = 6 Then
                ReadOnlyControls(True)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Function

    Private Sub LoadControls(releaseLine As ReleaseLine)
        With releaseLine
            INDSleWorkingArea.EditValue = .WorkingAreaId
            INDDeEncendidoAires.EditValue = .AirIgnitionTime
            INDDeEncendidoCPI.EditValue = .CPIIgnitionTime
            INDDeCentralMezclas.EditValue = .EntryMixingStationTime
            INDDeInicioPreparacion.EditValue = .PreparationStartTime
            INDGleAdecuantionArea.EditValue = IIf(.IsSterile, 1, 2)
            INDGleAdecuationItem1.EditValue = .AdequacyItem1
            INDGleAdecuationItem2.EditValue = .AdequacyItem2
            INDGleAdecuationItem3.EditValue = .AdequacyItem3
            INDGleAdecuationItem4.EditValue = .AdequacyItem4
            INDGleAdecuationItem5.EditValue = .AdequacyItem5
            INDGleAdecuationItem6.EditValue = .AdequacyItem6
            INDGleAdecuationItem7.EditValue = .AdequacyItem7
            INDGleAdecuationItem8.EditValue = .AdequacyItem8
            INDGleConditioningItem1.EditValue = .ConditioningItem1
            INDGleConditioningItem2.EditValue = .ConditioningItem2
            INDGleConditioningItem3.EditValue = .ConditioningItem3
            INDGleConditioningItem4.EditValue = .ConditioningItem4
            INDGleConditioningItem5.EditValue = .ConditioningItem5
            INDGleConditioningItem6.EditValue = .ConditioningItem6
            INDGleConditioningItem7.EditValue = .ConditioningItem7
            INDGleConditioningItem8.EditValue = .ConditioningItem8
            INDGleApplyPressure.EditValue = .ApplyItem9
            INDGleApplySpeed.EditValue = .ApplyItem11
            INDTePresure.EditValue = .ValueItem9
            INDGleApplySpeed.EditValue = .ValueItem11
            INDMeCommentary.EditValue = .Observation
            INDSleWorkingArea.Properties.NullText = .WorkingAreaCodeName
        End With
        Me.BarraBotones.PrintReportByIdReport(PrintReportAction.None, releaseLine.Id, 2228, 157, releaseLine.Id)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
    End Sub

    Private Async Sub Guardar()
        Try
            If Not ValidateControls() Then
                Return
            End If

            If Not ValidateCheckItems() Then
                Return
            End If

            AssigValues()
            AsyncLoader(True)

            Using model As New MCampaign(Tag)
                Dim res = Await model.SaveReleaseLine(releaseLine)

                If res.StateResult Then
                    ShowMessage(Domain.Base.Entities.eStatusResult.SUCCESS) = "Se ha generado la liberación de la línea correctamente"
                    DialogResult = Windows.Forms.DialogResult.OK
                Else
                    ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = res.Message
                End If
            End Using
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    Private Function ValidateCheckItems() As Boolean
        Dim sb As New StringBuilder()
        Dim sbres As New StringBuilder()

        If CurrentUserRole = UserRoleMixingStation.QF_Produccion OrElse CurrentUserRole = UserRoleMixingStation.DirectorTecnico OrElse Not AuxiliarExists Then
            If Not CBool(INDGleAdecuationItem1.EditValue) Then sb.AppendLine(INDLblItem1.Text)

            If CBool(INDGleAdecuationItem2.EditValue) Then sb.AppendLine(INDLblItem2.Text)

            If Not CBool(INDGleAdecuationItem3.EditValue) Then sb.AppendLine(INDLblItem3.Text)

            If Not CBool(INDGleAdecuationItem4.EditValue) Then sb.AppendLine(INDLblItem4.Text)

            If Not CBool(INDGleAdecuationItem5.EditValue) Then sb.AppendLine(INDLblItem5.Text)

            If Not CBool(INDGleAdecuationItem6.EditValue) Then sb.AppendLine(INDLblItem6.Text)

            If Not CBool(INDGleAdecuationItem7.EditValue) Then sb.AppendLine(INDLblItem7.Text)

            If Not CBool(INDGleAdecuationItem8.EditValue) Then sb.AppendLine(INDLblItem8.Text)

            If CBool(INDGleApplyPressure.EditValue) AndAlso CDec(INDTePresure.EditValue) > 1.5 Then sb.AppendLine(INDLblItem9.Text)

            If CBool(INDGleApplySpeed.EditValue) AndAlso (CDec(INDGleApplySpeed.EditValue) < 0.36 OrElse CDec(INDGleApplySpeed.EditValue) > 0.54) Then sb.AppendLine(INDLblItem10.Text)

            If sb.Length > 0 Then
                sbres.AppendLine($"Las siguientes variables del área de adecuación no han sido superadas: {vbCrLf}{sb.ToString()}")
            End If
        End If

        sb.Clear()

        If CurrentUserRole = UserRoleMixingStation.Auxiliar OrElse CurrentUserRole = UserRoleMixingStation.DirectorTecnico OrElse Not AuxiliarExists Then
            If Not CBool(INDGleConditioningItem1.EditValue) Then sb.AppendLine(INDLblItem1.Text)

            If CBool(INDGleConditioningItem2.EditValue) Then sb.AppendLine(INDLblItem2.Text)

            If Not CBool(INDGleConditioningItem3.EditValue) Then sb.AppendLine(INDLblItem3.Text)

            If Not CBool(INDGleConditioningItem4.EditValue) Then sb.AppendLine(INDLblItem4.Text)

            If Not CBool(INDGleConditioningItem5.EditValue) Then sb.AppendLine(INDLblItem5.Text)

            If Not CBool(INDGleConditioningItem6.EditValue) Then sb.AppendLine(INDLblItem6.Text)

            If Not CBool(INDGleConditioningItem7.EditValue) Then sb.AppendLine(INDLblItem7.Text)

            If Not CBool(INDGleConditioningItem8.EditValue) Then sb.AppendLine(INDLblItem8.Text)

            If sb.Length > 0 Then
                sbres.AppendLine($"Las siguientes variables del área de acondicionamiento no han sido superadas: {vbCrLf}{sb.ToString()}")
            End If

            If sbres.Length > 0 Then
                ShowMessage(Domain.Base.Entities.eStatusResult.WARNING) = sbres.ToString()
                Return False
            End If
        End If

        Return True
    End Function

#Region "Events"
    Private Sub INDSleWorkingArea_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleWorkingArea.QueryPopUp
        If INDSleWorkingArea.Properties.DataSource Is Nothing Then
            Using model As New MCampaign(Tag)
                INDSleWorkingArea.Properties.DataSource = model.ListWorkingAreas(True, CMConfigurationId)
            End Using
        End If
    End Sub

    Private Async Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Await CleanControls()
    End Sub

    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    Private Sub INDGleApplyPressure_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleApplyPressure.EditValueChanged
        INDTePresure.Enabled = (INDGleApplyPressure.EditValue Is Nothing OrElse CBool(INDGleApplyPressure.EditValue))
        If Not INDTePresure.Enabled Then
            INDTePresure.EditValue = 0
            INDTePresure.Properties.MinValue = 0
            INDPnlPressure.BackColor = Color.Transparent
        Else
            INDTePresure.Properties.MinValue = 0.2
        End If
    End Sub

    Private Sub INDGleApplySpeed_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleApplySpeed.EditValueChanged
        INDGleApplySpeed.Enabled = (INDGleApplySpeed.EditValue Is Nothing OrElse CBool(INDGleApplySpeed.EditValue))
        If Not INDGleApplySpeed.Enabled Then
            INDGleApplySpeed.EditValue = 0
            INDPnlSpeed.BackColor = Color.Transparent
        End If
    End Sub

    Private Sub INDTePresure_EditValueChanged(sender As Object, e As EventArgs) Handles INDTePresure.EditValueChanged
        Dim value = CDec(INDTePresure.EditValue)
        Select Case value
            Case <= 1.5
                INDPnlPressure.BackColor = Color.FromArgb(56, 145, 60)
            Case Else
                INDPnlPressure.BackColor = Color.FromArgb(211, 47, 47)
        End Select
    End Sub

    Private Sub INDTeSpeed_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleApplySpeed.EditValueChanged
        Dim value = CDec(INDGleApplySpeed.EditValue)
        If value >= 0.36 AndAlso value <= 0.54 Then
            INDPnlSpeed.BackColor = Color.FromArgb(56, 145, 60)
        Else
            INDPnlSpeed.BackColor = Color.FromArgb(211, 47, 47)
        End If
    End Sub

#Region "Don't close popup date"
    Private allowClose As Boolean = False
    Private Sub INDDe_Popup(sender As Object, e As EventArgs) Handles INDDeCentralMezclas.Popup, INDDeEncendidoAires.Popup, INDDeEncendidoCPI.Popup, INDDeInicioPreparacion.Popup
        Dim form As PopupDateEditForm = CType(sender, DateEdit).GetPopupEditForm()
        AddHandler form.Calendar.OkClick, AddressOf Calendar_OkClick
    End Sub

    Private Sub Calendar_OkClick(sender As Object, e As EventArgs)
        allowClose = True
        INDDeCentralMezclas.ClosePopup()
        INDDeEncendidoAires.ClosePopup()
        INDDeEncendidoCPI.ClosePopup()
        INDDeInicioPreparacion.ClosePopup()
        'CType(sender, PopupCalendarControl).ClosePopup()
        allowClose = False
    End Sub

    Private Sub INDDe_QueryCloseUp(sender As Object, e As CancelEventArgs) Handles INDDeCentralMezclas.QueryCloseUp, INDDeEncendidoAires.QueryCloseUp, INDDeEncendidoCPI.QueryCloseUp, INDDeInicioPreparacion.QueryCloseUp
        If allowClose Then Return
        e.Cancel = True
    End Sub
#End Region

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir del abarra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, releaseLine.Id, 2228, releaseLine.Id)
    End Sub

#End Region

    Public Enum UserRoleMixingStation
        DirectorTecnico = 0
        QF_Calidad = 1
        QF_Produccion
        Auxiliar
        No_Allowed
    End Enum

    Private Async Sub INDSleWorkingArea_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleWorkingArea.EditValueChanged
        If INDSleWorkingArea.EditValue Is Nothing Then
            INDTeLastCampaign.EditValue = Nothing
        Else
            Using model As New MCampaign(Tag)
                Dim res = Await model.GetLastReleaseLineUsedByWorkingAreaIdAsync(releaseLine.Id, INDSleWorkingArea.EditValue)
                INDTeLastCampaign.EditValue = res?.CampaignDetail?.CampaignNumber
            End Using
        End If
    End Sub
End Class