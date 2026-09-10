'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz
' Created          : 2013-07-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Domain.Base.Entities


''' <summary>
''' Formulario modal para evaluación general
''' </summary>
Public Class GeneralConciliation

#Region "Fields"

    Private _model As MConciliation
    Private _savePermission As Boolean
    Private _idOperativeUnit As Int32
    Private _conciliationCId As Integer
    Private _option As Boolean
    Private _dictionaryInvoiceSelected As New Dictionary(Of String, List(Of GlosaInvoiceDetail))

    Private _valueAcceptedIPS As Decimal
    Private _valueAcceptedEAPB As Decimal
    Private _valuePending As Decimal

#End Region

#Region "Builders"

    ''' <summary>
    ''' Constructor para el formulario
    ''' </summary>
    Public Sub New(ByVal model As MConciliation, ByVal savePermission As Boolean, ByVal idOperativeUnit As Integer, ByVal conciliationCId As Integer, ByVal lisInvoiceSelected As List(Of String), ByVal listInvoiceDetailSelected As List(Of GlosaInvoiceDetail))
        InitializeComponent()

        _model = model
        _savePermission = savePermission
        _idOperativeUnit = idOperativeUnit
        _conciliationCId = conciliationCId

        If lisInvoiceSelected IsNot Nothing Then
            _option = True
            For Each invoiceNumber As String In lisInvoiceSelected
                If Not _dictionaryInvoiceSelected.ContainsKey(invoiceNumber) Then
                    _dictionaryInvoiceSelected.Add(invoiceNumber, New List(Of GlosaInvoiceDetail))
                End If
            Next
        ElseIf listInvoiceDetailSelected IsNot Nothing Then
            _option = False
            Dim invoiceNumber = listInvoiceDetailSelected.FirstOrDefault().InvoiceNumber
            _dictionaryInvoiceSelected.Add(invoiceNumber, listInvoiceDetailSelected)
        End If
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
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

    Public Property ValPendingIPSconciliation As Decimal
        Get
            Return INDTxtValueAcceptedIPS.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValueAcceptedIPS.EditValue = value
        End Set
    End Property

    Public Property ValPendingEAPBconciliation As Decimal
        Get
            Return INDTxtValueAcceptedEAPB.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValueAcceptedEAPB.EditValue = value
        End Set
    End Property

    Public Property ValPendingConciliation As Decimal
        Get
            Return INDTxtValuePending.EditValue
        End Get
        Set(value As Decimal)
            INDTxtValuePending.EditValue = value
        End Set
    End Property

#End Region

#Region "Metodos"

    Private Async Function LoadInvoiceDetails() As Task
        If _option Then
            AsyncLoader(True)
            Dim invoiceNumbers = _dictionaryInvoiceSelected.Keys.ToList()
            For Each invoiceNumber As String In invoiceNumbers
                Dim listGlosaInvoiceDetail = Await Me._model.ListGlosaInvoiceDetailForGeneralConciliation(invoiceNumber)
                _dictionaryInvoiceSelected(invoiceNumber) = listGlosaInvoiceDetail
            Next
            AsyncLoader(False)
        End If
    End Function

    Private Function ValidateControls() As Boolean
        If _savePermission = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisosGuardar, Comunes)
            Return False
        End If

        If ValPendingIPSconciliation < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor aceptado por la IPS no puede ser menor de 0"
            Return False
        End If

        If ValPendingEAPBconciliation < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor aceptado por la EAPB no puede ser menor de 0"
            Return False
        End If

        If ValPendingIPSconciliation = 0 AndAlso ValPendingEAPBconciliation = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor aceptado por la IPS y la EAPB no puede ser 0"
            Return False
        End If

        If ValPendingConciliation < 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "El valor pendiente de conciliar no puede ser menor de 0"
            Return False
        End If

        If ValPendingIPSconciliation > 0 AndAlso Me.INDSleResponseHierarchyId.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un concepto de aceptación"
            Return False
        End If

        If String.IsNullOrEmpty(INDMeComment.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un comentario"
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' conciliacion general a nivel de detalle de factura
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ConciliationGeneral_LevelDetail(WithConfirm As Boolean) As Task
        Try
            AsyncLoader(True)

            Dim strrMensajeGeneral As StringBuilder = New StringBuilder()
            Dim strrMensajeError As StringBuilder = New StringBuilder()

            Dim pendingIPSconciliation As Decimal = ValPendingIPSconciliation
            Dim pendingEAPBconciliation As Decimal = ValPendingEAPBconciliation

            For Each invoiceNumber As String In _dictionaryInvoiceSelected.Keys
                Dim invoicedetails = _dictionaryInvoiceSelected(invoiceNumber)
                Dim listaToSave As List(Of GlosaMovementGlosa) = New List(Of GlosaMovementGlosa)

                For Each detail In invoicedetails
                    Dim maxConciliate = detail.CalculateValuePending(2)
                    Dim listGlosaMovementGlosa As List(Of GlosaMovementGlosa) = detail.GlosaMovementGlosa.ToList()

                    For Each glosaMov In listGlosaMovementGlosa
                        Dim distribuiteIPSconciliation As Decimal = 0
                        Dim distribuiteEAPBconciliation As Decimal = 0

                        If glosaMov.ValuePendingConciliation IsNot Nothing AndAlso glosaMov.ValuePendingConciliation > 0 Then
                            If maxConciliate > 0 AndAlso pendingIPSconciliation > 0 Then
                                distribuiteIPSconciliation = If(glosaMov.ValuePendingConciliation > pendingIPSconciliation, pendingIPSconciliation, glosaMov.ValuePendingConciliation)
                            End If
                            If maxConciliate > 0 AndAlso pendingEAPBconciliation > 0 AndAlso glosaMov.ValuePendingConciliation > distribuiteIPSconciliation Then
                                distribuiteEAPBconciliation = If((glosaMov.ValuePendingConciliation - distribuiteIPSconciliation) > pendingEAPBconciliation, pendingEAPBconciliation, (glosaMov.ValuePendingConciliation - distribuiteIPSconciliation))
                            End If

                            glosaMov.ConciliationCId = Me._conciliationCId
                            glosaMov.ValPendingIPSconciliation = distribuiteIPSconciliation
                            glosaMov.ValPendingEAPBconciliation = distribuiteEAPBconciliation
                            glosaMov.ValPendingConciliation = glosaMov.ValuePendingConciliation - (glosaMov.ValPendingIPSconciliation + glosaMov.ValPendingEAPBconciliation)
                            glosaMov.IdResponseHierarchyConciliation = If(glosaMov.ValPendingIPSconciliation > 0, INDSleResponseHierarchyId.EditValue, Nothing)
                            glosaMov.RationaleConciliation = Me.INDMeComment.Text
                            glosaMov.RationaleDateConciliation = Date.Now

                            Dim valueAcceptedIPSconciliationConfirm = glosaMov.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                            Dim valueAcceptedEAPBconciliationConfirm = glosaMov.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedEAPBconciliation)
                            glosaMov.ValueAcceptedIPSconciliation = valueAcceptedIPSconciliationConfirm + glosaMov.ValPendingIPSconciliation
                            glosaMov.ValueAcceptedEAPBconciliation = valueAcceptedEAPBconciliationConfirm + glosaMov.ValPendingEAPBconciliation

                            listaToSave.Add(glosaMov)

                            pendingIPSconciliation = pendingIPSconciliation - distribuiteIPSconciliation
                            pendingEAPBconciliation = pendingEAPBconciliation - distribuiteEAPBconciliation
                            maxConciliate = maxConciliate - distribuiteIPSconciliation - distribuiteEAPBconciliation
                        End If
                    Next
                Next

                If listaToSave.Count > 0 Then
                    Dim resultSaveMovement As ActionResult = Await Me._model.SaveConciliationInvoiceDetails(listaToSave)
                    If resultSaveMovement.StateResult = True Then
                        If WithConfirm Then
                            Dim result As ActionResult = Await Me._model.ConfirmConciliationInvoice(_conciliationCId, invoiceNumber, _idOperativeUnit)
                            If result IsNot Nothing Then
                                If result.StateResult = True Then
                                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                        For Each item As String In result.MessageResult
                                            strrMensajeGeneral.AppendLine("(" & invoiceNumber & ") " & item)
                                        Next
                                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                                        strrMensajeGeneral.AppendLine("(" & invoiceNumber & ") " & result.Message)
                                    End If
                                ElseIf result.StateResult = False Then
                                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                        For Each item As String In result.MessageResult
                                            strrMensajeError.AppendLine("(" & invoiceNumber & ") " & item)
                                        Next
                                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                                        strrMensajeError.AppendLine("(" & invoiceNumber & ") " & result.Message)
                                    End If
                                End If
                            Else
                                strrMensajeError.AppendLine(obtenerRecurso(ComunesContacteAdministrador))
                            End If
                        Else
                            If resultSaveMovement.MessageResult IsNot Nothing AndAlso resultSaveMovement.MessageResult.Count > 0 Then
                                For Each item As String In resultSaveMovement.MessageResult
                                    strrMensajeGeneral.AppendLine("(" & invoiceNumber & ") " & item)
                                Next
                            ElseIf Not String.IsNullOrEmpty(resultSaveMovement.Message) Then
                                strrMensajeGeneral.AppendLine("(" & invoiceNumber & ") " & resultSaveMovement.Message)
                            End If
                        End If
                    Else
                        If resultSaveMovement.MessageResult IsNot Nothing AndAlso resultSaveMovement.MessageResult.Count > 0 Then
                            For Each item As String In resultSaveMovement.MessageResult
                                strrMensajeError.AppendLine("(" & invoiceNumber & ") " & item)
                            Next
                        ElseIf Not String.IsNullOrEmpty(resultSaveMovement.Message) Then
                            strrMensajeError.AppendLine("(" & invoiceNumber & ") " & resultSaveMovement.Message)
                        End If
                    End If
                End If
            Next

            If strrMensajeError.Length > 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = strrMensajeError.ToString
            End If
            If strrMensajeGeneral.Length > 0 Then
                Me.Mensaje(EeventViewerImages.Informacion) = strrMensajeGeneral.ToString
            End If
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Function

#End Region

#Region "Handlers"

#Region "Load"

    Private Async Sub GeneralConciliation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveWithoutUndoAndFind)
        If Me._option Then
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
        End If
        '*********************************?
        Await LoadInvoiceDetails()
        '*********************************?
        _valueAcceptedIPS = 0
        _valueAcceptedEAPB = 0
        _valuePending = 0
        For Each invoiceNumber As String In _dictionaryInvoiceSelected.Keys
            Dim invoicedetails = _dictionaryInvoiceSelected(invoiceNumber)
            For Each detail In invoicedetails
                Dim listGlosaMovementGlosa As List(Of GlosaMovementGlosa) = detail.GlosaMovementGlosa.ToList()
                For Each glosaMovementGlosa In listGlosaMovementGlosa
                    _valueAcceptedIPS = _valueAcceptedIPS + glosaMovementGlosa.ValPendingIPSconciliation
                    _valueAcceptedEAPB = _valueAcceptedEAPB + glosaMovementGlosa.ValPendingEAPBconciliation
                    _valuePending = _valuePending + glosaMovementGlosa.CalculateValuePending(2)
                Next
            Next
        Next
        '*********************************?
        INDTxtValueAcceptedIPS.EditValue = _valueAcceptedIPS
        INDTxtValueAcceptedEAPB.EditValue = _valueAcceptedEAPB
        INDTxtValuePending.EditValue = _valuePending - _valueAcceptedIPS - _valueAcceptedEAPB
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model = Nothing
        _savePermission = Nothing
        _idOperativeUnit = Nothing
        _conciliationCId = Nothing
        _dictionaryInvoiceSelected = Nothing
    End Sub

#End Region

#Region "Activated"

    Private Sub GeneralConciliation_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDTxtValueAcceptedIPS.Focus()
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDSleConcepts_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleResponseHierarchyId.QueryPopUp
        If INDSleResponseHierarchyId.Properties.DataSource Is Nothing Then
            INDSleResponseHierarchyId.Properties.DataSource = _model.ListResponseHierarchy()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDTxtValueAccepted_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDTxtValueAcceptedIPS.EditValueChanging, INDTxtValueAcceptedEAPB.EditValueChanging
        If e.NewValue < 0 Then
            e.Cancel = True
        End If

        Dim newValue As Decimal = 0
        If Not Decimal.TryParse(e.NewValue.ToString().Replace(indigo.Culture.NumberFormat.CurrencyGroupSeparator, indigo.Culture.NumberFormat.NumberDecimalSeparator), Globalization.NumberStyles.AllowDecimalPoint, indigo.Culture, newValue) Then
            e.Cancel = True
            Exit Sub
        End If

        If newValue > Me._valuePending Then
            e.Cancel = True
        End If
    End Sub

    Private Sub INDTxtValueAcceptedIPS_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValueAcceptedIPS.EditValueChanged
        INDLciResponseHierarchyId.HideControl(Not (INDTxtValueAcceptedIPS.EditValue > 0))
        Dim calculatePending = (ValPendingIPSconciliation = 0 AndAlso ValPendingEAPBconciliation = 0) OrElse (ValPendingEAPBconciliation > 0)

        If Not calculatePending OrElse (Me._valuePending < (ValPendingIPSconciliation + ValPendingEAPBconciliation)) Then
            ValPendingEAPBconciliation = Me._valuePending - ValPendingIPSconciliation
        End If

        ValPendingConciliation = Me._valuePending - (ValPendingIPSconciliation + ValPendingEAPBconciliation)
    End Sub

    Private Sub INDTxtValueAcceptedEAPB_EditValueChanged(sender As Object, e As EventArgs) Handles INDTxtValueAcceptedEAPB.EditValueChanged
        Dim calculatePending = (ValPendingIPSconciliation = 0 AndAlso ValPendingEAPBconciliation = 0) OrElse (ValPendingIPSconciliation > 0)

        If Not calculatePending OrElse (Me._valuePending < (ValPendingIPSconciliation + ValPendingEAPBconciliation)) Then
            ValPendingIPSconciliation = Me._valuePending - ValPendingEAPBconciliation
        End If

        ValPendingConciliation = Me._valuePending - (ValPendingIPSconciliation + ValPendingEAPBconciliation)
    End Sub

#End Region

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra("522")
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Async Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Await ConciliationGeneral_LevelDetail(False)
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Await ConciliationGeneral_LevelDetail(True)
    End Sub

#End Region

End Class