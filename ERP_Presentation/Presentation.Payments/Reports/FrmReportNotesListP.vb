#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Infrastructure.Data.Xpo.PaymentsRepository
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP

#End Region

Public Class FrmReportNotesListP

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"
    Public Property ProoftCloseXpoThirdParty As XPInstantFeedbackSource
    Public Property ProoftCloseXpoNotes As XPInstantFeedbackSource

    Private _FillingStatus As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingStatus As List(Of Tuple(Of Integer, String))
        Get
            If _FillingStatus Is Nothing Then
                _FillingStatus = New List(Of Tuple(Of Integer, String))
                _FillingStatus.Add(New Tuple(Of Integer, String)(1, "Registrado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(2, "Confirmado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(3, "Anulado"))
                _FillingStatus.Add(New Tuple(Of Integer, String)(4, "Todos"))
            End If
            Return _FillingStatus
        End Get
    End Property

    Private _FillingTypeReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingTypeReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingTypeReport Is Nothing Then
                _FillingTypeReport = New List(Of Tuple(Of Integer, String))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(1, "Listado Notas"))
                _FillingTypeReport.Add(New Tuple(Of Integer, String)(2, "Documento Notas"))
            End If
            Return _FillingTypeReport
        End Get
    End Property

    Private _FillingNatureReport As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingNatureReport As List(Of Tuple(Of Integer, String))
        Get
            If _FillingNatureReport Is Nothing Then
                _FillingNatureReport = New List(Of Tuple(Of Integer, String))
                _FillingNatureReport.Add(New Tuple(Of Integer, String)(1, "Débito"))
                _FillingNatureReport.Add(New Tuple(Of Integer, String)(2, "Crédito"))
                _FillingNatureReport.Add(New Tuple(Of Integer, String)(3, "Todas"))
            End If
            Return _FillingNatureReport
        End Get
    End Property

    Private criteria As String = Nothing

#End Region

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyStart()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyStart.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdPartyEnd()
        Using msearch As New MBusqueda
            ProoftCloseXpoThirdParty = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListThirdPartyReport)
            INDSleThirdPartyEnd.Datasource = ProoftCloseXpoThirdParty
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleNotesStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoNotesStart()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdSupplier.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND IdSupplier.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoNotes = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsNotesReportFilter, criteria)
            INDSleNotesStart.Datasource = ProoftCloseXpoNotes
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleNotesEnd
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoNotesEnd()
        criteria = Nothing

        If (INDGleStatusReport.EditValue IsNot Nothing And INDGleStatusReport.EditValue <> 4) Then
            criteria = "Status = " & INDGleStatusReport.EditValue
        End If

        If (INDSleThirdPartyStart.EditValue IsNot Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing) Then
            If (criteria Is Nothing) Then
                criteria = "IdSupplier.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            Else
                criteria &= " AND IdSupplier.IdThirdParty.Nit >= '" & INDSleThirdPartyStart.EditValue & "' AND IdSupplier.IdThirdParty.Nit <= '" & INDSleThirdPartyEnd.EditValue & "'"
            End If
        End If

        Using msearch As New MBusqueda
            ProoftCloseXpoNotes = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListPaymentsNotesReportFilter, criteria)
            INDSleNotesEnd.Datasource = ProoftCloseXpoNotes
        End Using
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDDateStart.EditValue Is Nothing Or INDDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDateStart.Focus()
            Validations = False
        ElseIf Me.INDDateStart.EditValue > INDDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDateEnd.Focus()
            Validations = False
        End If

        'Valida Terceros
        If INDSleThirdPartyStart.EditValue Is Nothing And INDSleThirdPartyEnd.EditValue IsNot Nothing Or INDSleThirdPartyEnd.EditValue Is Nothing And INDSleThirdPartyStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        ElseIf INDSleThirdPartyEnd.EditValue < INDSleThirdPartyStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblThirdParty.Text)
            Me.INDSleThirdPartyStart.Focus()
            Validations = False
        End If

        'Valida Notas
        If INDSleNotesStart.EditValue Is Nothing And INDSleNotesEnd.EditValue IsNot Nothing Or INDSleNotesEnd.EditValue Is Nothing And INDSleNotesStart.EditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblNotes.Text)
            Me.INDSleNotesStart.Focus()
            Validations = False
        ElseIf INDSleNotesEnd.EditValue < INDSleNotesStart.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblNotes.Text)
            Me.INDSleNotesStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _pucModel.Dispose()
        _pucModel = Nothing
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento click del control INDSbGenerareReport
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click

        If Me.ValidateControlsReports Then
            AsyncLoader(True)
            If INDGleTypeReport.EditValue = 1 Then
                Dim reporte As New rptNotesListP

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue,
                                             INDGleNatureReport.EditValue, INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                                             INDSleNotesStart.EditValue, INDSleNotesEnd.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            Else
                Dim reporte As New rptNotesDocumentP

                reporte.ParametrosReporte = {INDDateStart.EditValue, INDDateEnd.EditValue, INDGleStatusReport.EditValue,
                             INDGleNatureReport.EditValue, INDSleThirdPartyStart.EditValue, INDSleThirdPartyEnd.EditValue,
                             INDSleNotesStart.EditValue, INDSleNotesEnd.EditValue}

                INDDvViewReport.DocumentSource = reporte
                reporte.CargarDataSource()
                reporte.CreateDocument(True)
                AsyncLoader(False)
                If DirectCast(reporte.DataSource, ICollection).Count > 0 Then
                    Me.INDLcBase.Visible = False
                    Me.INDCncNavigation.Visible = False
                    Me.INDPcViewReport.Visible = True
                    INDDvViewReport.Show()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                    Me.INDDateStart.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack del Control INDCnBack
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnBack_ClickBack() Handles INDCnBack.ClickBack
        INDLcBase.Visible = True
        INDCncNavigation.Visible = True
        INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento shown del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportNotesListP_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        'Cargar el GridLookUpEdit
        Me.INDGleStatusReport.Properties.DataSource = FillingStatus
        Me.INDGleTypeReport.Properties.DataSource = FillingTypeReport
        Me.INDGleNatureReport.Properties.DataSource = FillingNatureReport

        'Asigna un valor por defecto a GridLookUpEdit
        Me.INDGleStatusReport.EditValue = 4
        Me.INDGleTypeReport.EditValue = 1
        Me.INDGleNatureReport.EditValue = 3

    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyStart.QueryPopUp
        If INDSleThirdPartyStart.Datasource Is Nothing Then
            LoadXpoThirdPartyStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleThirdPartyEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleThirdPartyEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleThirdPartyEnd.QueryPopUp
        If INDSleThirdPartyEnd.Datasource Is Nothing Then
            LoadXpoThirdPartyEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleNotesStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleNotesStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleNotesStart.QueryPopUp
        If INDSleNotesStart.Datasource Is Nothing Then
            LoadXpoNotesStart()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del Control INDSleNotesEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleNotesEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleNotesEnd.QueryPopUp
        If INDSleNotesEnd.Datasource Is Nothing Then
            LoadXpoNotesEnd()
        End If
    End Sub

    Private Sub INDGleStatusReport_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleStatusReport.EditValueChanged
        LoadXpoNotesStart()
        LoadXpoNotesEnd()
    End Sub

    Private Sub INDSleThirdPartyStart_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyStart.EditValueChanged
        LoadXpoNotesStart()
        LoadXpoNotesEnd()
    End Sub

    Private Sub INDSleThirdPartyEnd_EditValueChanged(sender As Object, e As Presentation.Controls.EditValueChangedEventArgs) Handles INDSleThirdPartyEnd.EditValueChanged
        LoadXpoNotesStart()
        LoadXpoNotesEnd()
    End Sub

    Private Sub FrmReportNotesListP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleThirdPartyStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleThirdPartyEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetThirdPartyAsync
        Me.INDSleNotesStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPaymentsNote
        Me.INDSleNotesEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetPaymentsNote
    End Sub

    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

End Class