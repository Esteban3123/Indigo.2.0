'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan F. Tamayo
' Created          : 2013-07-22
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-07-22
' Description      : Vista del del frontal de coordinación
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Text
Imports System.Windows.Forms
Imports ClosedXML.Excel
Imports DevExpress.Data
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.GlosasRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas.MVP

#End Region


''' <summary> 
''' Vista del del frontal de coordinación
''' </summary>
Public Class FrmCoordination
    Implements ICoordination

#Region "Enums"

    ''' <summary>
    ''' Enumeración que indica el nivel en el que me ecuentro en el frontal
    ''' </summary>
    Private Enum EGroups
        ''' <summary>
        ''' Representa el grupo raiz del formulario
        ''' </summary>
        Root
        ''' <summary>
        ''' Representa el grupo de oficio seleccionado, listando sus facturas
        ''' </summary>
        SelectedDocument
        ''' <summary>
        ''' Representa el grupo de factura seleccionada, listando sus detalles
        ''' </summary>
        SelectedInvoice
    End Enum

#End Region

#Region "Fields"

    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues

    ''' <summary>
    ''' Control de trazabilidad
    ''' </summary>
    Friend WithEvents CtrTraceabilityControl As Presentation.Controls.CtrTraceabilityControl

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _model As MCoodination

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PCoordination

    ''' <summary>
    ''' Bandera para indicar que el frontal ya se cargo
    ''' </summary>
    Private _loaded As Boolean = False

    ''' <summary>
    ''' Parametros de Glosas
    ''' </summary>
    Private _parameterGlosas As TimeParameters

    ''' <summary>
    ''' Objeto del oficio seleccionado
    ''' </summary>
    Private _currentDocument As Domain.Entities.GlosaObjectionsReceptionC

    Private _objetoD As Domain.Entities.GlosaObjectionsReceptionD

    ''' <summary>
    ''' Objeción detalle
    ''' </summary>
    Private objetoD As Object

    ''' <summary>
    ''' Numero factura
    ''' </summary>
    Private _invoiceNumber As String

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Lista de movimientos bloqueados
    ''' </summary>
    Dim MovementsBlock As List(Of Integer)

    ''' <summary>
    ''' Nombre del usuario que bloquea la factura
    ''' </summary>
    Private _nameUserWithInvoice As String

    ''' <summary>
    ''' Código del usuario que bloquea la factura.
    ''' </summary>
    Private _codeUserWithInvoice As String

    ''' <summary>
    ''' Variable bandera para el registro bloqueado
    ''' </summary>
    Private _recordFlag As Boolean

    ''' <summary>
    ''' Tag formulario Evaluación
    ''' </summary>
    Private _tagEvaluation As String

    ''' <summary>
    ''' Grupo en el que se encuentra ubicado el proceso
    ''' </summary>
    Private _currentGroup As EGroups

    ''' <summary>
    ''' Objeto openFileDialog
    ''' </summary>
    Private _fileOpener As New OpenFileDialog

#End Region

#Region "Properties"

    ''' <summary>
    ''' Asigna el estado a los controles de la barra de herramientas segun el grupo seleccionado
    ''' </summary>
    ''' <param name="confirmed">Valor opcional que indica si el oficio seleccionado se encuentra confirmado</param>
    ''' <value>Grupo seleccionado</value>
    Private WriteOnly Property ControlsStatus(Optional ByVal confirmed As Boolean = False) As EGroups
        Set(value As EGroups)
            Me._currentGroup = value
            Select Case value
                Case EGroups.Root
                    Me.INDlcygNavInvoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlycgDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
                    Me.INDlciExportImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Case EGroups.SelectedDocument
                    Me.INDlycgInvoices.Text = obtenerRecurso(Eresources.TituloDetalleDeOficioCoordinacion, Eform.Coordinacion)
                    Me.INDlyciNumberInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciPatientName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciInvoiceState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciEntryNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyciTotalGlosado.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyciTotalCobradoEPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyciSelectionSum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlycipatienteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    Me.INDlyciConsecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciSysDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciDocumentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciDocumentNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciNit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciDocumentState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciInvoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlcygNavInvoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlycgDocuments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlciExportImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
                Case EGroups.SelectedInvoice
                    Me.INDlycgInvoices.Text = obtenerRecurso(Eresources.TituloDetalleDeFacturaCoordinacion, Eform.Coordinacion)
                    Me.INDlyciConsecutive.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciSysDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciDocumentDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciDocumentNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciNit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciEntity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciComment.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciDocumentState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    Me.INDlyciInvoices.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                    Me.INDlyciNumberInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciPatientName.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciInvoiceState.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciEntryNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlyciDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyciTotalGlosado.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyciTotalCobradoEPS.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyciSelectionSum.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlycipatienteValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Me.INDlciExportImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End Select
        End Set
    End Property

    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' Asigna un valor que indica que se esta realizacion una operacion asincrona
    ''' </summary>
    ''' <value>Valor</value>
    Public WriteOnly Property AsyncOperation As Boolean Implements ICoordination.AsyncOperation
        Set(value As Boolean)
            Me.AsyncLoader(value)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de oficios
    ''' </summary>
    ''' <value>Lista de oficios</value>
    ''' <returns>La lista de oficios</returns>
    Public Property Documents As XPInstantFeedbackSource Implements ICoordination.Documents
        Get
            Return CType(Me.INDgdcDocuments.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            Me.INDgdcDocuments.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el oficio seleccionado de la rejilla
    ''' </summary>
    ''' <value>Oficio seleccionado de la rejilla</value>
    ''' <returns>El oficio seleccionado de la rejilla</returns>
    Public Property SelectedDocument As Object Implements ICoordination.SelectedDocument
        Get
            Return Me.INDgdcInvoices.DataSource
        End Get
        Set(value As Object)
            Me._currentDocument = value
            Me.BarraBotones.SetDocuments(_currentDocument.Id)
            'Aqui va el resto de la asignación
            Me.INDlblConsecutive.Text = Me._currentDocument.RadicatedConsecutive
            Me.INDlblSysDate.Text = Me._currentDocument.RadicatedDate.ToString("D", indigo.Culture)
            Me.INDlblDocumentDate.Text = Me._currentDocument.DocumentDate.ToString("D", indigo.Culture)
            Me.INDlblDocumentNumber.Text = Me._currentDocument.DocumentNumber.Trim()
            Me.INDlblNit.Text = Me._currentDocument.Customer.Nit.Trim()
            Me.INDlblEntity.Text = Me._currentDocument.Customer.Name.Trim()
            Me.INDlblComment.Text = Me._currentDocument.Comment.Trim()
            Me.INDimcDocumentState.EditValue = Me._currentDocument.State

            INDgdcInvoices.DataSource = Nothing
            Me.INDgdvInvoices.ShowLoadingPanel()
            Task.Factory.StartNew(Sub()
                                      Dim result As List(Of ViewCoordinationGlosaObjectionXpo) = Nothing
                                      Try
                                          result = _model.ListGlosasObjectionD(_currentDocument.Id)
                                          INDgdcInvoices.BeginInvoke(Sub()
                                                                         INDgdcInvoices.DataSource = If(result IsNot Nothing AndAlso result.Count > 0, result, Nothing)
                                                                         INDgdvInvoices.HideLoadingPanel()
                                                                     End Sub)
                                      Catch ex As Exception
                                          INDgdcInvoices.BeginInvoke(Sub()
                                                                         INDgdvInvoices.HideLoadingPanel()
                                                                         Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                     End Sub)
                                      End Try
                                  End Sub)

            Me.GetDocumentIndexed("508" & "_" & Me._currentDocument.RadicatedConsecutive)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de detalles de factura
    ''' </summary>
    ''' <value>Lista de detalles de factura</value>
    ''' <returns>La lista de detalles de factura</returns>
    Public Property InvoiceDetails As Object Implements ICoordination.InvoiceDetails
        Get
            Return TryCast(Me.INDInvoiceDetailGdc.DataSource, List(Of Domain.Entities.GlosaInvoiceDetail))
        End Get
        Set(value As Object)
            Me.INDInvoiceDetailGdc.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el código del usuario que bloqueo la factura.
    ''' </summary>
    Public Property CodeUserWithInvoice As String Implements ICoordination.CodeUserWithInvoice
        Get
            Return Me._nameUserWithInvoice
        End Get
        Set(value As String)
            Me._nameUserWithInvoice = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre del usuario que bloqueo la factura.
    ''' </summary>
    Public Property NameUserWithInvoice As String Implements ICoordination.NameUserWithInvoice
        Get
            Return Me._codeUserWithInvoice
        End Get
        Set(value As String)
            Me._codeUserWithInvoice = value
        End Set
    End Property

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Este método no aplica para éste frontal
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Este método no aplica para éste frontal
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' Este método no aplica para éste frontal
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo

    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
        Me.SaveWithState(1)
    End Sub

    ''' <summary>
    ''' Realiza el guardado de los movimientos de una factura seleccionada
    ''' </summary>
    Public Async Sub SaveWithState(StateEvaluation As Byte)
        Try
            If Me.INDInvoiceDetailGv.DataRowCount > 0 Then
                Me.GlosaInvoiceDetailQX.CloseEditor()
                Me.INDInvoiceDetailGv.CloseEditor()
                Dim listvalidate As List(Of String) = ValidateListInvoice()
                Dim strBuilderValidate As New StringBuilder
                If listvalidate IsNot Nothing AndAlso listvalidate.Count > 0 Then
                    strBuilderValidate.AppendLine("Debe seleccionar un concepto de Jerarquía de Aceptación para los siguientes item:")
                    For Each item As String In listvalidate
                        strBuilderValidate.AppendLine(item)
                    Next
                    strBuilderValidate.AppendLine("No se pudo guardar")
                    Mensaje(EeventViewerImages.Advertencia) = strBuilderValidate.ToString()
                    Exit Sub
                End If
                Me.AsyncLoader(True)
                Dim listMOv As New List(Of GlosaMovementGlosa) '= CType(Me.INDInvoiceDetailGv.DataSource, List(Of GlosaMovementGlosa))
                Dim result As ActionResult
                For Each item As GlosaMovementGlosa In Me.INDInvoiceDetailGv.DataSource
                    If listMOv.Contains(item) = False Then
                        listMOv.Add(item)
                    End If
                    For Each itemQx As GlosaMovementGlosa In item.ListMovimientoAux
                        If listMOv.Contains(itemQx) = False Then
                            listMOv.Add(itemQx)
                        End If
                    Next
                Next

                For Each item In listMOv
                    item.StateEvaluation = StateEvaluation
                Next

                result = Await Me._model.saveMov(listMOv, _idOperativeUnit)
                If result IsNot Nothing Then
                    If result.StateResult = True Then
                        Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado, Eform.Comunes)
                        If result.MessageResult IsNot Nothing Then
                            Dim StrBuilder As New StringBuilder
                            For Each item As String In result.MessageResult
                                StrBuilder.AppendLine(item)
                            Next
                            If StrBuilder.ToString() <> String.Empty Then
                                Me.Mensaje(EeventViewerImages.Informacion) = StrBuilder.ToString()
                            End If
                        End If
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        If result.StateResult = True Then
                            For Each Item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
                                If Item.InvoiceNumber = Me._invoiceNumber Then
                                    Item.StateRecord = True
                                End If
                            Next
                            If objetoD.DocumentType = "1" Then
                                For Each Item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
                                    If Item.InvoiceNumber = Me._invoiceNumber Then
                                        Item.StatePortfolio = "3"
                                    End If
                                Next
                            ElseIf objetoD.DocumentType = "2" Then
                                For Each Item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
                                    If Item.InvoiceNumber = Me._invoiceNumber Then
                                        Item.StatePortfolio = "6"
                                    End If
                                Next
                            End If
                            Me.INDgdcInvoices.RefreshDataSource()
                            Me._currentGroup = EGroups.SelectedInvoice
                            back()
                        End If
                    ElseIf result.StateResult = False Then
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            Dim StrBuilder As New StringBuilder
                            For Each item As String In result.MessageResult
                                StrBuilder.AppendLine(item)
                            Next
                            Me.Mensaje(EeventViewerImages.Advertencia) = StrBuilder.ToString()
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                        Dim listMovementGlosa = Await Me._model.listMovementsByInvoiceAndResponsible(_invoiceNumber, "")
                        If listMovementGlosa.Count > 0 Then
                            Me.INDInvoiceDetailGdc.DataSource = listMovementGlosa
                            Me.INDInvoiceDetailGdc.RefreshDataSource()
                            ExpandView()
                        Else
                            If objetoD.DocumentType = "1" Then
                                For Each Item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
                                    If Item.InvoiceNumber = Me._invoiceNumber Then
                                        Item.StatePortfolio = "3"
                                    End If
                                Next
                            ElseIf objetoD.DocumentType = "2" Then
                                For Each Item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
                                    If Item.InvoiceNumber = Me._invoiceNumber Then
                                        Item.StatePortfolio = "6"
                                    End If
                                Next
                            End If
                            Me._currentGroup = EGroups.SelectedInvoice
                            back()
                        End If
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
                Me.AsyncLoader(False)
            End If

        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Confirma los movimientos
    ''' </summary>
    Private Async Sub Confirm()
        Try
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Me.SaveWithState(2)
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Confirma el oficio validando que todas las facturas se encuentren confirmadas
    ''' </summary>
    Private Sub ConfirmDocument()
        If ValidateDocument() Then
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.FacturasSinConfirmarComunes)
            Return
        End If
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim pointPopup = New System.Drawing.Point((Me.DesktopBounds.Width / 2) - (Me.PopupControlContainer1.Width / 2), (Me.DesktopBounds.Height / 2) - (Me.PopupControlContainer1.Height / 2))
            Me.INDSendDocumentDateDte.Properties.MinValue = Me._currentDocument.RadicatedDate
            PopupControlContainer1.ShowPopup(pointPopup)
        End If
    End Sub

    ''' <summary>
    ''' Valida que los movimientos se encuentren bien diligenciados
    ''' </summary>
    Public Function ValidateListInvoice() As List(Of String)
        Dim ListString As New List(Of String)
        For Each item As GlosaMovementGlosa In Me.INDInvoiceDetailGdc.DataSource
            If item.ListMovimientoAux.Count = 0 Then
                If item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                    If item.ValueAcceptedFirstInstance IsNot Nothing AndAlso item.ValueAcceptedFirstInstance > 0 AndAlso item.ConceptGlosas IsNot Nothing Then

                        Dim homologateType = CInt(item.GlosaEvaluationType)
                        Dim requiereConceptoAceptacion = {ConceptsGlosaEvaluationByType.NoSubsanada, ConceptsGlosaEvaluationByType.SubsanadaParcial}.Contains(homologateType)

                        If requiereConceptoAceptacion AndAlso item.IdResponseHierarchyGlosa Is Nothing Then
                            ListString.Add(item.GlosaInvoiceDetail.ServiceCode & " - " & item.GlosaInvoiceDetail.ServiceName)
                        End If
                    End If
                    If (item.CodeGlosaEvaluation IsNot Nothing) AndAlso (item.ValueAcceptedFirstInstance IsNot Nothing) Then
                        item.TempState = item.State
                        item.State = StatesGlosaMovements.GlosaEvaluada
                        If item.MaxValueAccepted = 0 Then
                            item.OtherMovements.ForEach(Sub(c)
                                                            If c.ValueAcceptedFirstInstance Is Nothing Then
                                                                c.State = StatesGlosaMovements.GlosaEvaluada
                                                            End If
                                                        End Sub)
                        End If
                    End If
                ElseIf item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                    If item.ValueAcceptedSecondInstance IsNot Nothing AndAlso item.ValueAcceptedSecondInstance > 0 AndAlso item.IdResponseHierarchyReiteration Is Nothing Then
                        ListString.Add(item.GlosaInvoiceDetail.ServiceCode & " - " & item.GlosaInvoiceDetail.ServiceName)
                    End If
                    If (item.CodeGlosaEvaluation IsNot Nothing) AndAlso (item.ValueAcceptedSecondInstance IsNot Nothing) Then
                        item.TempState = item.State
                        item.State = StatesGlosaMovements.ReiteracionEvaluada
                        If item.MaxValueAccepted = 0 Then
                            item.OtherMovements.ForEach(Sub(c)
                                                            If c.ValueAcceptedSecondInstance Is Nothing Then
                                                                c.State = StatesGlosaMovements.ReiteracionEvaluada
                                                            End If
                                                        End Sub)
                        End If
                    End If
                End If
            Else
                For Each itemAux As GlosaMovementGlosa In item.ListMovimientoAux
                    If item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                        If itemAux.ValueAcceptedFirstInstance IsNot Nothing AndAlso itemAux.ValueAcceptedFirstInstance > 0 AndAlso itemAux.IdResponseHierarchyGlosa Is Nothing Then
                            ListString.Add(item.GlosaInvoiceDetail.ServiceCode & " - " & item.GlosaInvoiceDetail.ServiceName)
                        End If
                        If (itemAux.CodeGlosaEvaluation IsNot Nothing) AndAlso (itemAux.ValueAcceptedFirstInstance IsNot Nothing) Then
                            itemAux.TempState = itemAux.State
                            itemAux.State = StatesGlosaMovements.GlosaEvaluada
                            If itemAux.MaxValueAccepted = 0 Then
                                itemAux.OtherMovements.ForEach(Sub(c)
                                                                   If c.ValueAcceptedFirstInstance Is Nothing Then
                                                                       c.State = StatesGlosaMovements.GlosaEvaluada
                                                                   End If
                                                               End Sub)
                            End If
                        End If
                    ElseIf item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                        If itemAux.ValueAcceptedSecondInstance IsNot Nothing AndAlso itemAux.ValueAcceptedSecondInstance > 0 AndAlso itemAux.IdResponseHierarchyReiteration Is Nothing Then
                            ListString.Add(item.GlosaInvoiceDetail.ServiceCode & " - " & item.GlosaInvoiceDetail.ServiceName)
                        End If
                        If (itemAux.CodeGlosaEvaluation IsNot Nothing) AndAlso (itemAux.ValueAcceptedSecondInstance IsNot Nothing) Then
                            itemAux.TempState = itemAux.State
                            itemAux.State = StatesGlosaMovements.ReiteracionEvaluada
                            If itemAux.MaxValueAccepted = 0 Then
                                itemAux.OtherMovements.ForEach(Sub(c)
                                                                   If c.ValueAcceptedSecondInstance Is Nothing Then
                                                                       c.State = StatesGlosaMovements.ReiteracionEvaluada
                                                                   End If
                                                               End Sub)
                            End If
                        End If
                    End If
                Next
            End If
        Next
        Return ListString
    End Function

    ''' <summary>
    ''' Permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    ''' <param name="existeDatos">Valor que indica si existen datos para actualizar</param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Este método no aplica para éste frontal
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Este método no aplica para éste frontal
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Crea y carga el control de tiempo en la barra
    ''' </summary>
    Private Sub LoadXtraTrackControl()
        Me.CtrTraceabilityControl = New CtrTraceabilityControl()
        Me.CtrTraceabilityControl.Process = GlosasProcess.ObjectionCoordination
        Me.CtrTraceabilityControl.Dock = DockStyle.Fill
        Me.AdditionalControlPanel.Controls.Add(Me.CtrTraceabilityControl)
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await _model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Cargar Oficio de Glosa 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DocumentsLoad()
        Me.AsyncOperation = True
        Me.Documents = Me._model.ListDocuments()
        Me.AsyncOperation = False
    End Sub

    ''' <summary>
    ''' Valida que todas la facturas se encuentren confirmadas
    ''' </summary>
    Public Function ValidateDocument() As Boolean
        Dim control = False
        For Each Item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
            If Item.StateRecord = False Then
                control = True
            End If
        Next
        Return control
    End Function

    ''' <summary>
    ''' Muestra el detalle del oficio seleccionado
    ''' </summary>
    Private Async Sub DetailDocument()
        If Me.INDgdvDocuments.SelectedRowsCount > 0 Then
            Dim aux = Me.INDgdvDocuments.GetRow(Me.INDgdvDocuments.FocusedRowHandle).OriginalRow
            If aux IsNot Nothing Then
                Me.AsyncLoader(True)
                Dim obj = Await Me._model.GetDocumentById(aux.Id, _idOperativeUnit)
                Me.SelectedDocument = obj
                Me.ControlsStatus(IIf(aux.State.ToString().Trim().Equals(StatesGlosaObjectionC.ConfirmadoRadicado), False, True)) = EGroups.SelectedDocument
                Me.AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que controla el regresar dependiendo de la ubicación
    ''' </summary>
    Sub back()
        Select Case Me._currentGroup
            Case EGroups.SelectedDocument
                Me.ControlsStatus = EGroups.Root
            Case EGroups.SelectedInvoice
                DeleteBlockedRecord()
                record = Nothing
                Me.BarraBotones.EnableBarItems()
                Me.BarraBotones.DisableBarDocument()
                Me._doc = Nothing
                Me.AdditionalControlPanel.Visible = False
                UpdateDocument()
                Me.ControlsStatus = EGroups.SelectedDocument
        End Select
    End Sub

    ''' <summary>
    ''' Cuando se confrima actualizamos OBJC para cargar datos actualizados de cartera
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UpdateDocument()
        If Me.INDgdvDocuments.SelectedRowsCount > 0 Then
            Dim aux = Me.INDgdvDocuments.GetRow(Me.INDgdvDocuments.FocusedRowHandle).OriginalRow
            If aux IsNot Nothing Then
                Me.AsyncLoader(True)
                Dim obj = Await Me._model.GetDocumentById(aux.Id, _idOperativeUnit)
                Me.SelectedDocument = obj
                Me.AsyncLoader(False)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Muestra el detalle de la factura seleccionada
    ''' </summary>
    Private Async Sub DetailInvoice()
        Me.AsyncLoader(True)
        objetoD = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Object)
        Dim objectMovements As List(Of GlosaMovementGlosa)
        _invoiceNumber = objetoD.InvoiceNumber
        objectMovements = Await Me._model.listMovementsByInvoiceAndResponsible(objetoD.InvoiceNumber, "")
        If objetoD.DocumentType.ToString() = GlosaDocumentType.GlosaFirst Then

            Me.CommentReiteration.Visible = False
            Me.V2Instance.Visible = False
            Me.CommentReiteration.Visible = False
            Me.JustificationReiteration.Visible = False
            Me.ValueReiterated.Visible = False

            Me.CommentGlosa.Visible = True
            Me.V1Instance.Visible = True
            Me.CommentGlosa.Visible = True
            Me.JustificationGlosa.Visible = True
            Me.ValueGlosado.Visible = True

            Me.Service.VisibleIndex = 1
            Me.ConceptGlosa.VisibleIndex = 2
            Me.CommentGlosa.VisibleIndex = 3
            Me.VMax.VisibleIndex = 4
            Me.ValueGlosado.VisibleIndex = 5
            Me.ConceptEval.VisibleIndex = 6
            Me.ResponsibleThirdParty.VisibleIndex = 7
            Me.JustificationGlosa.VisibleIndex = 8
            Me.V1Instance.VisibleIndex = 9
            Me.MoreInfo.VisibleIndex = 10


            Me.CommentReiterationQX.Visible = False
            Me.V2InstanceQX.Visible = False
            Me.CommentReiterationQX.Visible = False
            Me.JustificationReiterationQX.Visible = False
            Me.ValueReiteratedQX.Visible = False

            Me.CommentGlosaQX.Visible = True
            Me.V1InstanceQX.Visible = True
            Me.CommentGlosaQX.Visible = True
            Me.JustificationGlosaQX.Visible = True
            Me.ValueGlosadoQX.Visible = True

            Me.ServiceQX.VisibleIndex = 1
            Me.ConceptGlosaQX.VisibleIndex = 2
            Me.CommentGlosaQX.VisibleIndex = 3
            Me.VMaxQX.VisibleIndex = 4
            Me.ValueGlosadoQX.VisibleIndex = 5
            Me.ConceptEvalQX.VisibleIndex = 6
            Me.ResponsibleThirdPartyQX.VisibleIndex = 7
            Me.JustificationGlosaQX.VisibleIndex = 8
            Me.V1InstanceQX.VisibleIndex = 9
            Me.MoreInfoQX.VisibleIndex = 10

            Me.InfoCommentReiteration.Visible = False
            Me.InfoResponsibleReiteration.Visible = False
            Me.InfoV2Instance.Visible = False

            Me.InfoCommentGlosa.Visible = True
            Me.InfoResponsibleGlosa.Visible = True
            Me.InfoV1Instance.Visible = True

            Me.InfoCommentGlosa.VisibleIndex = 1
            Me.InfoResponsibleGlosa.VisibleIndex = 2
            Me.InfoV1Instance.VisibleIndex = 3
            Me.InfoConceptCodeName.VisibleIndex = 4

            'Me.controlITEM

        End If

        If objetoD.DocumentType.ToString() = GlosaDocumentType.Reiteration Then

            Me.CommentReiteration.Visible = True
            Me.V2Instance.Visible = True
            Me.CommentReiteration.Visible = True
            Me.JustificationReiteration.Visible = True
            Me.ValueReiterated.Visible = True

            Me.CommentGlosa.Visible = False
            Me.V1Instance.Visible = False
            Me.CommentGlosa.Visible = False
            Me.JustificationGlosa.Visible = False
            Me.ValueGlosado.Visible = False

            Me.Service.VisibleIndex = 1
            Me.ConceptGlosa.VisibleIndex = 2
            Me.CommentReiteration.VisibleIndex = 3
            Me.VMax.VisibleIndex = 4
            Me.ValueReiterated.VisibleIndex = 5
            Me.ConceptEval.VisibleIndex = 6
            Me.ResponsibleThirdParty.VisibleIndex = 7
            Me.JustificationReiteration.VisibleIndex = 8
            Me.V2Instance.VisibleIndex = 9
            Me.MoreInfo.VisibleIndex = 10

            Me.CommentGlosaQX.Visible = False
            Me.V1InstanceQX.Visible = False
            Me.CommentGlosaQX.Visible = False
            Me.JustificationGlosaQX.Visible = False
            Me.ValueGlosadoQX.Visible = False

            Me.CommentReiterationQX.Visible = True
            Me.V2InstanceQX.Visible = True
            Me.CommentReiterationQX.Visible = True
            Me.JustificationReiterationQX.Visible = True
            Me.ValueReiteratedQX.Visible = True

            Me.ServiceQX.VisibleIndex = 1
            Me.ConceptGlosaQX.VisibleIndex = 2
            Me.CommentReiterationQX.VisibleIndex = 3
            Me.VMaxQX.VisibleIndex = 4
            Me.ValueReiteratedQX.VisibleIndex = 5
            Me.ConceptEvalQX.VisibleIndex = 6
            Me.ResponsibleThirdPartyQX.VisibleIndex = 7
            Me.JustificationReiterationQX.VisibleIndex = 8
            Me.V2InstanceQX.VisibleIndex = 9
            Me.MoreInfoQX.VisibleIndex = 10

            Me.InfoCommentGlosa.Visible = False
            Me.InfoResponsibleGlosa.Visible = False
            Me.InfoV1Instance.Visible = False

            Me.InfoCommentReiteration.Visible = True
            Me.InfoResponsibleReiteration.Visible = True
            Me.InfoV2Instance.Visible = True

            Me.InfoCommentReiteration.VisibleIndex = 1
            Me.InfoResponsibleReiteration.VisibleIndex = 2
            Me.InfoV2Instance.VisibleIndex = 3
            Me.InfoConceptCodeName.VisibleIndex = 4

        End If
        Me.INDInvoiceDetailGdc.DataSource = objectMovements
        Me.INDlblNumberInvoice.Text = objetoD.InvoiceNumber
        Me.INDlblEntryNumber.Text = objetoD.IngressNumber
        Me.INDlblPatientName.Text = objetoD.PatientName
        Me.INDiceInvoiceState.EditValue = objetoD.State.ToString()
        Me.ControlsStatus = EGroups.SelectedInvoice

        Me.INDlblPatienteValue.Text = ManageDecimalsFun(objetoD.InvoiceValuePacient)
        Me.INDlblTotalGlosado.Text = ManageDecimalsFun(objetoD.ValueGlosado)
        Me.INDlblTotalEAPB.Text = ManageDecimalsFun(objetoD.InvoiceValueEntity)

        'Oculto el boton exportar-importar
        Me.INDlciExportImport.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'Me.INDPCImportExport.Visible = False

        'sio el oficio esta confirmado, es decir, 3-Pendiente envio de oficio No debe mostrar Boton de guardar
        ' ya que, ya se creo el movimineto contable 
        If objetoD.StatePortfolio = "3" OrElse objetoD.StatePortfolio = "6" Then
            Me.BarraBotones.PrepareToolbar(eAction.None)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirmWithoutUndoAndFind)
        End If
        'Me.CtrClinicalHistory.PatientDocument = objeto.GlosaPortfolioGlosada.PatientCode.Trim
        If objetoD.DocumentType.ToString() = GlosaDocumentType.GlosaFirst Then
            Me.CtrTraceabilityControl.Process = Presentation.Base.GlosasProcess.ObjectionCoordination
        Else
            Me.CtrTraceabilityControl.Process = Presentation.Base.GlosasProcess.ReiterationCoordination
        End If
        Me.CtrTraceabilityControl.Invoice = objetoD.InvoiceNumber
        Me.CtrTraceabilityControl.Entity = objetoD.CustomerId
        Me.CtrTraceabilityControl.CargarControl()
        Me.AdditionalControlPanel.Visible = True
        ExpandView()
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = False

        Me.GetDocumentIndexed(Me.Tag & "_" & Me.objetoD.Id)

        record = Await _model.GetBlockRecord(_tagEvaluation, objetoD.Id)
        If record.Id = 0 Then
            Me.BarraBotones.SetDocuments(objetoD.Id)
            ' Me.BarraBotones.PrepareToolbar(eAction.OnlyHideAudit)
            Dim state = New Domain.Base.Entities.ObjectChangeTracker
            state.State = Domain.Base.Entities.ObjectState.Added
            record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me._indigoSessionValues.UserIndigoName, .IdForm = _tagEvaluation, .CodUser = Me._indigoSessionValues.UserIndigo, .IdRecord = objetoD.Id}
            Dim operation = Await _model.SaveBlockRecord(record)
            record = operation.ObjectEmbbeded
            Me._recordFlag = False
        Else
            If record.CodUser.Trim().Equals(_indigoSessionValues.UserIndigo.Trim()) Then
                Me._recordFlag = False
            Else
                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                Me._recordFlag = True
            End If
        End If

        If Me.objetoD.StateRecord = True Then
            Me.BarraBotones.RibbonPagEform.Visible = False
            Me.BarraBotones.DisableBarDocument()
        Else
            Me.BarraBotones.RibbonPagEform.Visible = True
        End If
        Me.AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Carga la lista de todos los conceptos de glosa
    ''' </summary>
    Private Async Sub LoadConcepts()
        Me.AsyncLoader(True)
        'El dos corresponde al tipo de conceptos de devoluciones
        Dim list = Await Me._model.ListConceptsGlosaByTypes(New List(Of String) From {"2", "5", "6", "7", "8"})
        If list IsNot Nothing Then
            Me.INDCodeGlosaEvaluationGle.DataSource = list
        End If
        Me.AsyncLoader(False)
    End Sub


    ''' <summary>
    ''' Cargar los terceros
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadThirdParty()
        Using model As New MBusqueda
            Me.RepositoryResponsibleThirdParty.DataSource = model.ConsultarEntidades(eDataSource.ListAllThirdParty)
        End Using
    End Sub

    ''' <summary>
    ''' Expandir vista
    ''' </summary>
    Private Sub ExpandView()
        Me.INDInvoiceDetailGv.BeginUpdate()
        Dim dataRowCount As Integer = INDInvoiceDetailGv.DataRowCount
        Dim rHandle As Integer
        For rHandle = 0 To dataRowCount - 1
            INDInvoiceDetailGv.SetMasterRowExpanded(rHandle, True)
            Dim detailInvoicesQX As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(Me.INDInvoiceDetailGv.GetDetailView(rHandle, Me.INDInvoiceDetailGv.GetRelationIndex(rHandle, "ListMovimientoAux")), DevExpress.XtraGrid.Views.Grid.GridView)
            If detailInvoicesQX IsNot Nothing Then
                Dim dataRowCountQX As Integer = detailInvoicesQX.DataRowCount
                Dim rHandleQX As Integer
                For rHandleQX = 0 To dataRowCountQX - 1
                    detailInvoicesQX.SetMasterRowExpanded(rHandleQX, True)
                Next
            End If
        Next
        Me.INDInvoiceDetailGv.EndUpdate()
    End Sub

    Async Sub ShowGeneralInvoiceMovements()
        Dim lista = New List(Of String)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

        view = Me.INDgdcInvoices.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim ObjD As Object = TryCast(view.GetRow(item), Object)
                If ObjD IsNot Nothing AndAlso ObjD.Id > 0 Then
                    lista.Add(ObjD.InvoiceNumber.ToString)
                End If
            End If
        Next

        If lista.Count() = 0 Then
            Dim ObjD As Object = TryCast(view.GetRow(view.FocusedRowHandle), Object)
            If ObjD IsNot Nothing AndAlso ObjD.Id > 0 Then
                lista.Add(ObjD.InvoiceNumber.ToString)
            End If
        End If

        If lista.Count > 0 Then
            Dim _option = 3 'general a nivel de factura
            Using generalEvaluation As Generalevaluation = New Generalevaluation("COORDINACION", New List(Of GlosaMovementGlosa), _option, _model, CStr(MyBase.Tag), _idOperativeUnit, lista, True)
                generalEvaluation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim result = generalEvaluation.ShowDialog(Me)
                If result = System.Windows.Forms.DialogResult.OK Then
                    Me.AsyncLoader(True)
                    Dim obj = Await Me._model.GetDocumentById(Me._currentDocument.Id, _idOperativeUnit)
                    Me.SelectedDocument = obj
                    Me.AsyncLoader(False)

                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para disparar el formulario de evaluacion general.
    ''' </summary>
    Async Sub ShowGeneralMovements()
        Dim lista = New List(Of GlosaMovementGlosa)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

        view = Me.INDInvoiceDetailGdc.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim data As GlosaMovementGlosa = TryCast(view.GetRow(item), Domain.Entities.GlosaMovementGlosa)
                lista.Add(data)
            End If
        Next

        If lista.Count > 0 Then
            Dim invoice As String = ""
            If view.GetSelectedRows.Count > 0 Then
                invoice = lista.Item(0).InvoiceNumber
            End If
            Dim _option = 0
            If view.Name = INDInvoiceDetailGv.Name Then '"INDInvoiceDetailGv" 
                _option = 1
            ElseIf view.Name = GlosaInvoiceDetailQX.Name Then '"GlosaInvoiceDetailQX" 
                _option = 2
            End If

            Using generalEvaluation As Generalevaluation = New Generalevaluation("COORDINACION", lista, _option, _model, CStr(MyBase.Tag), _idOperativeUnit)
                generalEvaluation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim result = generalEvaluation.ShowDialog(Me)
                If result = System.Windows.Forms.DialogResult.OK Then
                    'Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado, Eform.Comunes)
                    Dim data As List(Of GlosaMovementGlosa) = Await Me._model.listMovementsByInvoiceAndResponsible(invoice, "")
                    Me.INDInvoiceDetailGdc.DataSource = data
                    Me.INDInvoiceDetailGdc.RefreshDataSource()
                    ExpandView()
                    ' Guardar()
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para disparar el formulario de causante de glosa.
    ''' </summary>
    Sub ShowResponsibleThirdParty()
        Dim lista = New List(Of GlosaMovementGlosa)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDInvoiceDetailGdc.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim data As GlosaMovementGlosa = TryCast(view.GetRow(item), Domain.Entities.GlosaMovementGlosa)
                lista.Add(data)
            End If
        Next
        If lista.Count > 0 Then
            Dim ResponsibleThirdParty As FrmResponsibleThirdPartyPopUp = New FrmResponsibleThirdPartyPopUp()
            ResponsibleThirdParty.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Dim result = ResponsibleThirdParty.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                For Each item As GlosaMovementGlosa In lista
                    item.ResponsibleThirdPartyId = ResponsibleThirdParty.ResponsibleThirdPartyId
                Next
                ExpandView()
            End If
        End If
    End Sub

    Sub ValidateValueInline(rowHandle As Integer, IsGlosa As Boolean, isQX As Boolean, view As GridView)
        Dim VAlueMaxGlosa As Decimal
        Dim glosaMov As GlosaMovementGlosa
        Dim maxSum As Decimal = 0
        Dim valMax As Decimal = 0
        If isQX Then
            Dim mainView As GridView = Me.INDInvoiceDetailGv
            Dim detailView2 As GridView = Me.INDInvoiceDetailGdc.FocusedView
            ' Dim detailView2 As GridView = TryCast(mainView.GetDetailView(mainView.FocusedRowHandle, mainView.GetRelationIndex(mainView.FocusedRowHandle, "ListMovimientoAux")), GridView)
            glosaMov = CType(detailView2.GetFocusedRow, GlosaMovementGlosa)
            maxSum = IIf(view.GetRowCellValue(rowHandle, Me.MaxAuxQX) IsNot Nothing, view.GetRowCellValue(rowHandle, Me.MaxAuxQX), 0)
            valMax = IIf(view.GetRowCellValue(rowHandle, Me.VMaxQX) IsNot Nothing AndAlso Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.VMaxQX)) > 0, Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.VMaxQX)), 0)
        Else
            glosaMov = CType(Me.INDInvoiceDetailGv.GetRow(rowHandle), GlosaMovementGlosa)
            maxSum = IIf(view.GetRowCellValue(rowHandle, Me.MaxAux) IsNot Nothing, view.GetRowCellValue(rowHandle, Me.MaxAux), 0)
            valMax = IIf(view.GetRowCellValue(rowHandle, Me.VMax) IsNot Nothing AndAlso Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.VMax)) > 0, Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.VMax)), 0)
        End If

        Dim valInput
        If IsGlosa Then
            If isQX Then
                valInput = Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.V1InstanceQX))
            Else
                valInput = Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.V1Instance))
            End If
        Else
            If isQX Then
                valInput = Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.V2InstanceQX))
            Else
                valInput = Convert.ToDecimal(view.GetRowCellValue(rowHandle, Me.V2Instance))
            End If
        End If
        Dim valMaxAux As Decimal = valMax + maxSum
        Dim control As Decimal = 0
        If valMaxAux < valInput Then

            If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType.AsInt = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov.GlosaEvaluationType = "2") _
                            AndAlso glosaMov.CodeGlosaEvaluation = CStr(ConceptsGlosaEvaluation.SubsanadaParcial)) Then
                If IsGlosa Then
                    If isQX Then
                        view.SetRowCellValue(rowHandle, Me.V1InstanceQX, "")
                    Else
                        view.SetRowCellValue(rowHandle, Me.V1Instance, "")
                    End If
                Else
                    If isQX Then
                        view.SetRowCellValue(rowHandle, Me.V2InstanceQX, "")
                    Else
                        view.SetRowCellValue(rowHandle, Me.V2Instance, "")
                    End If
                End If
                If valMax = 0 Then
                    ForMaxAccepted(IsGlosa, glosaMov, valMaxAux, rowHandle, isQX, view)
                    If isQX Then
                        view.SetRowCellValue(rowHandle, Me.MaxAuxQX, 0)
                    Else
                        view.SetRowCellValue(rowHandle, Me.MaxAux, 0)
                    End If
                End If
            Else
                If IsGlosa Then
                    If isQX Then
                        view.SetRowCellValue(rowHandle, Me.V1InstanceQX, valMax)
                    Else
                        view.SetRowCellValue(rowHandle, Me.V1Instance, valMax)
                    End If
                Else
                    If isQX Then
                        view.SetRowCellValue(rowHandle, Me.V2InstanceQX, valMax)
                    Else
                        view.SetRowCellValue(rowHandle, Me.V2Instance, valMax)
                    End If
                End If
            End If
        Else
            If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType.AsInt = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                    OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov?.GlosaEvaluationType = "2") _
                                AndAlso glosaMov.CodeGlosaEvaluation = CStr(ConceptsGlosaEvaluation.SubsanadaParcial)) Then
                If IsGlosa Then
                    If valInput = 0 OrElse glosaMov.ValueGlosado < valInput Then
                        If isQX Then
                            view.SetRowCellValue(rowHandle, Me.V1InstanceQX, "")
                        Else
                            view.SetRowCellValue(rowHandle, Me.V1Instance, "")
                        End If
                        control = 1
                    End If
                Else
                    If valInput = 0 OrElse glosaMov.ValueReiterated < valInput Then
                        If isQX Then
                            view.SetRowCellValue(rowHandle, Me.V2InstanceQX, "")
                        Else
                            view.SetRowCellValue(rowHandle, Me.V2Instance, "")
                        End If
                        control = 1
                    End If
                End If
            End If

            Dim valNew As Decimal
            If control = 0 Then
                valNew = valMaxAux - valInput
            Else
                valNew = valMaxAux
            End If
            ForMaxAccepted(IsGlosa, glosaMov, valNew, rowHandle, isQX, view)
            If control = 1 Then
                If isQX Then
                    view.SetRowCellValue(rowHandle, Me.MaxAuxQX, 0)
                Else
                    view.SetRowCellValue(rowHandle, Me.MaxAux, 0)
                End If
            Else
                If isQX Then
                    view.SetRowCellValue(rowHandle, Me.MaxAuxQX, valInput)
                Else
                    view.SetRowCellValue(rowHandle, Me.MaxAux, valInput)
                End If
            End If

            view.RefreshData()

            'lista de movimiento por servicio 
            Dim tmpListMov = CType(view.DataSource, List(Of GlosaMovementGlosa)).Where(Function(c As GlosaMovementGlosa) c.InvoiceDetailId = glosaMov.InvoiceDetailId)
            Dim listmov As New List(Of GlosaMovementGlosa)
            listmov.AddRange(tmpListMov.ToList)

            VAlueMaxGlosa = listmov.Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True).ToList().Sum(Function(c As GlosaMovementGlosa) c.ValueGlosado)

            If IsGlosa Then
                Dim SumValueAccept As Decimal
                For i As Integer = 0 To listmov.Count - 1
                    If listmov(i).ValueAcceptedFirstInstance IsNot Nothing AndAlso listmov(i).ValueAcceptedFirstInstance > 0 Then
                        SumValueAccept += listmov(i).ValueAcceptedFirstInstance
                    End If
                Next

                SumValueAccept = SumValueAccept + valNew
                If SumValueAccept > VAlueMaxGlosa Then

                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorAceptadoMayorValorGlosado, Eform.Coordinacion), ManageDecimalsFun(SumValueAccept.ToString(indigo.Culture)), ManageDecimalsFun(VAlueMaxGlosa.ToString(indigo.Culture)))
                    For i As Integer = 0 To listmov.Count - 1
                        listmov(i).ValueAcceptedFirstInstance = 0
                        listmov(i).MaxValueAccepted = VAlueMaxGlosa
                        listmov(i).ValueAux = 0
                    Next
                    Exit Sub
                End If
            Else
                'reiteracion 
                Dim SumValueAccept As Decimal
                For i As Integer = 0 To listmov.Count - 1
                    If listmov(i).ValueAcceptedSecondInstance IsNot Nothing AndAlso listmov(i).ValueAcceptedSecondInstance > 0 Then
                        SumValueAccept += listmov(i).ValueAcceptedSecondInstance
                    End If
                Next


                SumValueAccept = SumValueAccept + valNew
                If SumValueAccept > VAlueMaxGlosa Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorAceptadoMayorValorGlosado, Eform.Coordinacion), ManageDecimalsFun(SumValueAccept.ToString(indigo.Culture)), ManageDecimalsFun(VAlueMaxGlosa.ToString(indigo.Culture)))
                    For i As Integer = 0 To listmov.Count - 1
                        listmov(i).ValueAcceptedSecondInstance = 0
                        listmov(i).MaxValueAccepted = VAlueMaxGlosa
                        listmov(i).ValueAux = 0
                    Next
                    Exit Sub
                End If
            End If
            view.RefreshData()
        End If
    End Sub

    Sub ForMaxAccepted(IsGlosa As Boolean, glosaMov As GlosaMovementGlosa, valMaxAux As Object, rowHandle As Integer, IsQX As Boolean, view As GridView)

        Dim qxId As Integer? = glosaMov.InvoiceDetailIdQX
        Dim source = CType(view.DataSource, List(Of GlosaMovementGlosa))

        Dim data = If(IsQX,
                           source.Where(Function(c) c.InvoiceDetailId = glosaMov.InvoiceDetailId AndAlso
                               ((Not c.InvoiceDetailIdQX.HasValue AndAlso Not qxId.HasValue) OrElse
                                (c.InvoiceDetailIdQX.HasValue AndAlso qxId.HasValue AndAlso c.InvoiceDetailIdQX.Value = qxId.Value))),
                           source.Where(Function(c) c.InvoiceDetailId = glosaMov.InvoiceDetailId AndAlso
                               ((Not c.InvoiceDetailIdQX.HasValue AndAlso Not qxId.HasValue) OrElse
                                (c.InvoiceDetailIdQX.HasValue AndAlso qxId.HasValue AndAlso c.InvoiceDetailIdQX.Value = qxId.Value)))
                     )

        For Each item As GlosaMovementGlosa In data
            If IsGlosa Then
                item.ValueGlosaMaxAccepted = "$" & item.ValueGlosado.ToString() & " - $" & valMaxAux.ToString()
            Else
                item.ValueReiteratedMaxAccepted = "$" & item.ValueReiterated.ToString() & " - $" & valMaxAux.ToString()
            End If
            item.MaxValueAccepted = valMaxAux.ToString()
        Next
    End Sub


    ''' <summary>
    ''' Logica para cargar los controles dependiendo si maneja o no decimales.
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadParameters() As Task
        Using model As New MTimeParameters(Me.Tag)
            Me._parameterGlosas = Await model.GetTimeParameters("0", Me._idOperativeUnit)
            If Me._parameterGlosas Is Nothing OrElse Me._parameterGlosas.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de glosas para la unidad operativa seleccionada"
                Exit Function
            ElseIf Me._parameterGlosas.ManageDecimals = False Then 'No maneja decimales.
                'Grid cabecera
                For Each item In INDgdvInvoices.Columns
                    If item.DisplayFormat.FormatString = "C" OrElse item.DisplayFormat.FormatString = "c" Then
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                RepositoryItemMoneyValue.Mask.EditMask = "N00"
            End If
        End Using
    End Function

    ''' <summary>
    ''' Funcion para cada vez que asignen un valor a los label's (Manjo de decimales)
    ''' </summary>
    ''' <param name="Expression"></param>
    ''' <returns></returns>
    Private Function ManageDecimalsFun(Expression As Object)
        If _parameterGlosas.ManageDecimals = False Then
            Return FormatCurrency(Expression, NumDigitsAfterDecimal:=0)
        Else
            Return FormatCurrency(Expression, 2)
        End If
    End Function

    Private Sub PrintReportsInvoice()
        objetoD = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Object)
        Dim watermark As Boolean
        If objetoD.StateRecord = 0 Then
            watermark = True
        Else
            watermark = False
        End If
        If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.ImprimirReporte) Then
            Me.BarraBotones.PrintReport(PrintReportAction.ViewPrinting, Me._currentDocument.Id, 0, {Me.objetoD.InvoiceNumber, True, Me.BarraBotones.OperatingUnit, watermark})
        Else
            Mensaje(EeventViewerImages.Advertencia) = "No tiene permiso para imprimir"
        End If
    End Sub

    ''' <summary>
    ''' Método que abre el form para reasignar usuario
    ''' </summary>
    Private Async Sub OpenFormAssignImportunityCause()
        Dim listViewCoordinationGlosaObjectionXpo As New List(Of ViewCoordinationGlosaObjectionXpo)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

        view = Me.INDgdcInvoices.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim ObjD As ViewCoordinationGlosaObjectionXpo = TryCast(view.GetRow(item), Object)
                listViewCoordinationGlosaObjectionXpo.Add(ObjD)
            End If
        Next

        Using formulario As New FrmAssignImportunityCause()
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.Width = 450
            formulario.Height = 250
            formulario.ToolBar.Visible = False
            formulario.ListViewCoordinationGlosaObjectionXpo = listViewCoordinationGlosaObjectionXpo
            Dim result = formulario.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                Me.AsyncLoader(True)
                Dim obj = Await Me._model.GetDocumentById(Me._currentDocument.Id, _idOperativeUnit)
                Me.SelectedDocument = obj
                Me.AsyncLoader(False)
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Agregar un comentario
    ''' </summary>
    ''' <param name="cell"></param>
    ''' <param name="comment"></param>
    ''' <param name="worksheet"></param>
    Private Sub WriteComments(cell As String, comment As String, worksheet As IXLWorksheet)
        If Not String.IsNullOrEmpty(comment) Then
            Dim celda As IXLCell = worksheet.Cell(cell)
            celda.Comment.AddText(comment)
        End If
    End Sub

    ''' <summary>
    ''' Genera la estructura de las columnas y filas para plarmarlo en el excel
    ''' </summary>
    ''' <param name="dtExport"></param>
    ''' <returns></returns>
    Private Function GenerateFileStructure(dtExport As DataTable) As DataTable
        Dim dataTable As New DataTable
        dataTable.Columns.Add("Factura")
        dataTable.Columns.Add("CodigoMovimiento")
        dataTable.Columns.Add("Fecha Factura")
        dataTable.Columns.Add("N° Radicado")
        dataTable.Columns.Add("Fecha Radicación")
        dataTable.Columns.Add("Oficio Recepción")
        dataTable.Columns.Add("Fecha Oficio")
        dataTable.Columns.Add("Paciente")
        dataTable.Columns.Add("Ingreso")
        dataTable.Columns.Add("Fecha Ingreso")
        dataTable.Columns.Add("Código Servicio")
        dataTable.Columns.Add("Servicio")
        dataTable.Columns.Add("Servicio QX")
        dataTable.Columns.Add("Código Centro Costo")
        dataTable.Columns.Add("Nombre Centro Costo")
        dataTable.Columns.Add("Codigo Glosa")
        dataTable.Columns.Add("Concepto Glosa")
        dataTable.Columns.Add("Comentario Glosa")
        dataTable.Columns.Add("Valor Glosa")
        dataTable.Columns.Add("Pago Parcial")
        dataTable.Columns.Add("Responsable")
        dataTable.Columns.Add("Código Concepto Respuesta")
        dataTable.Columns.Add("Valor Aceptado")
        dataTable.Columns.Add("Justificación Glosa")
        dataTable.Columns.Add("Jerarquia de Aceptación")
        dataTable.Columns.Add("Causante de Glosa")
        For Each item As DataRow In dtExport.Rows
            Dim row As DataRow = dataTable.NewRow()
            row.Item("Factura") = item("InvoiceNumber")
            row.Item("CodigoMovimiento") = item("MovementId")
            row.Item("Fecha Factura") = item("InvoiceDate")
            row.Item("N° Radicado") = item("RadicatedNumber")
            row.Item("Fecha Radicación") = item("RadicatedDate")
            row.Item("Oficio Recepción") = item("ObjectionRadicatedConsecutive")
            row.Item("Fecha Oficio") = item("ObjectionRadicatedDate")
            row.Item("Paciente") = item("PatientName")
            row.Item("Ingreso") = item("IngressNumber")
            row.Item("Fecha Ingreso") = item("IngressDate")
            row.Item("Código Servicio") = item("ServiceCode")
            row.Item("Servicio") = item("ServiceName")
            row.Item("Servicio QX") = item("ServiceCodeQX")
            row.Item("Código Centro Costo") = item("CostCenterCode")
            row.Item("Nombre Centro Costo") = item("CostCenterName")
            row.Item("Codigo Glosa") = item("CodeGlosa")
            row.Item("Concepto Glosa") = item("NameSpecific")
            row.Item("Comentario Glosa") = item("RationaleGlosa")
            row.Item("Valor Glosa") = item("ValueGlosado")
            row.Item("Pago Parcial") = item("ValuePayments")
            row.Item("Responsable") = item("ResponsableName")
            row.Item("Código Concepto Respuesta") = String.Empty
            row.Item("Valor Aceptado") = String.Empty
            row.Item("Justificación Glosa") = String.Empty
            row.Item("Jerarquia de Aceptación") = String.Empty
            row.Item("Causante de Glosa") = String.Empty
            dataTable.Rows.Add(row)
        Next
        Return dataTable
    End Function

    ''' <summary>
    ''' Evento para actualizar la rejilla de facturas
    ''' </summary>
    Private Async Sub RefreshCoordinationDocument()
        Try
            AsyncLoader(True)
            Dim dataGridInvoices = TryCast(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
            If dataGridInvoices IsNot Nothing Then
                Dim newDataSource As New List(Of GlosaMovementGlosa)
                ' Lista de tareas asincrónicas para obtener los nuevos datos
                Dim tasks As New List(Of Task(Of List(Of GlosaMovementGlosa)))()
                For Each item In dataGridInvoices
                    'newDataSource = Await _model.listMovementsByInvoiceAndResponsible(item.InvoiceNumber, String.Empty)
                    tasks.Add(_model.listMovementsByInvoiceAndResponsible(item.InvoiceNumber, String.Empty))
                Next
                ' Esperar a que todas las tareas se completen
                Dim allResults = Await Task.WhenAll(tasks)
                ' Agregar los resultados a la nueva lista
                For Each result In allResults
                    newDataSource.AddRange(result)
                Next

                If newDataSource.Count > 0 Then
                    Me.BeginInvoke(Sub()
                                       Me.INDInvoiceDetailGdc.DataSource = Nothing
                                       Me.INDInvoiceDetailGdc.DataSource = newDataSource
                                   End Sub)
                End If
            End If
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
            MessageIndigo.Show(ex.Message, MessageType.Errores, "")
        End Try
    End Sub

    ''' <summary>
    ''' Limpia campos del pop-up de justificación.
    ''' </summary>
    ''' <param name="row">Movimiento de glosa al que se le limpiarán los campos</param>
    Private Sub CleanJustificationEvaluation(row As GlosaMovementGlosa)
        row.IdResponseHierarchyGlosa = Nothing
        row.IdResponseHierarchyReiteration = Nothing
    End Sub
    ''' <summary>
    ''' Limpia los campos necesarios al cambiar un concepto en la rejilla del detalle de la factura.
    ''' </summary>
    ''' <param name="row"></param>
    ''' <param name="view"></param>
    ''' <param name="rowHandle"></param>
    Private Sub CleanDetailInvoice(row As GlosaMovementGlosa, view As DevExpress.XtraGrid.Views.Grid.GridView, rowHandle As Integer)
        row.GlosaEvaluationType = Nothing
        MovementsBlock.Remove(row.Id)
        If row.ListMovimientoAux.Count = 0 Then

            Dim isQXView As Boolean = view.Name = GlosaInvoiceDetailQX.Name

            If isQXView Then
                view.SetRowCellValue(rowHandle, Me.V1InstanceQX, 0)
                view.SetRowCellValue(rowHandle, Me.V2InstanceQX, 0)
            Else
                view.SetRowCellValue(rowHandle, Me.V1Instance, 0)
                view.SetRowCellValue(rowHandle, Me.V2Instance, 0)
            End If
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        FormSearchObjects = Nothing
        _model = Nothing
        _loaded = Nothing
        _presenter = Nothing
        _currentGroup = Nothing
        _currentDocument = Nothing
        _invoiceNumber = Nothing
        MovementsBlock = Nothing
        _nameUserWithInvoice = Nothing
        _codeUserWithInvoice = Nothing
        objetoD = Nothing
        _objetoD = Nothing
        record = Nothing
        _recordFlag = Nothing
        _tagEvaluation = Nothing
        _idOperativeUnit = Nothing
        _fileOpener = Nothing
    End Sub

    ''' <summary>
    ''' Aqui se realiza la carga del frontal
    ''' </summary>
    Private Async Sub FrmCoordination_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Aqui se carga el control de tiempo
        Me.LoadXtraTrackControl()
        '****Inicializar variables*****'
        Me._indigoSessionValues = SessionValues.Instance
        Me.MovementsBlock = New List(Of Integer)
        '******************************'
        Me.ActionReport = AddressOf Me.BarraBotones.PrintReport
        Me.IndigoGridControl1.SetHoldSize(Me.INDInvoiceDetailGdc, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgdcDocuments, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgdcInvoices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDMoreInfoGc, True)
        ' Me.Funct = AddressOf GenerateDoc
        Me._doc = Nothing
        Me.ControlsStatus = EGroups.Root
        Me.CtrNavigation.Group = Me.INDlycgInvoices
        Me._presenter = New PCoordination(Me)
        Me._tagEvaluation = "534"
        Me._model = New MCoodination(Me._tagEvaluation)
        Me.LoadConcepts()
        Me.LoadThirdParty()
        Me.DocumentsLoad()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await LoadParameters()
    End Sub

#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' Aqui se muestra el menu contextual para la rejilla de ofcicios
    ''' </summary>
    ''' 
    Private Sub INDgdvDocuments_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgdvDocuments.PopupMenuShowing
        If e.HitInfo.RowHandle > 0 Then
            'If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            If Me.INDgdvDocuments.SelectedRowsCount > 0 Then
                If e.Menu Is Nothing Then
                    Exit Sub
                End If
                e.Menu.Items.Clear()
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuDetalleDeOficio, Eform.Coordinacion), AddressOf DetailDocument, My.Resources.EditarAzul32))
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento sobre la rejilla de movimientos QX para mostrar el menu contextual
    ''' </summary>
    Private Sub GlosaInvoiceDetailQX_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles GlosaInvoiceDetailQX.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            If objetoD.StateRecord = False Then
                e.Menu.Items.Clear()
                If Not _recordFlag Then
                    e.Menu.Items.Add(New DXMenuItem("Evaluación General", AddressOf ShowGeneralMovements, My.Resources.modificarLineaAzul))
                    e.Menu.Items.Add(New DXMenuItem("Causante de glosa", AddressOf ShowResponsibleThirdParty, My.Resources.modificarLineaAzul))
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento sobre la rejilla de movimientos para mostrar el menu contextual
    ''' </summary>
    Private Sub INDInvoiceDetailGv_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDInvoiceDetailGv.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            If objetoD.StateRecord = False Then
                e.Menu.Items.Clear()
                If Not _recordFlag Then
                    e.Menu.Items.Add(New DXMenuItem("Evaluación General", AddressOf ShowGeneralMovements, My.Resources.modificarLineaAzul))
                    e.Menu.Items.Add(New DXMenuItem("Causante de glosa", AddressOf ShowResponsibleThirdParty, My.Resources.modificarLineaAzul))
                End If
            End If
        End If
    End Sub

    Private Sub INDgdvInvoices_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDgdvInvoices.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If

            Dim lista = New List(Of Object)
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

            view = Me.INDgdcInvoices.FocusedView
            For Each item As Integer In view.GetSelectedRows()
                If item > -1 Then
                    Dim ObjD As Object = TryCast(view.GetRow(item), Object)
                    If ObjD IsNot Nothing AndAlso ObjD.StateRecord = False Then 'todos sin confirmar
                        lista.Add(ObjD)
                    End If
                End If
            Next

            e.Menu.Items.Clear()
            If lista.Count > 0 Then
                e.Menu.Items.Add(New DXMenuItem("Evaluación General", AddressOf ShowGeneralInvoiceMovements, My.Resources.modificarLineaAzul))
                If lista.Count = 1 Then
                    e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuDetalleDeFactura, Eform.Coordinacion), AddressOf DetailInvoice, My.Resources.EditarAzul32))
                    e.Menu.Items.Add(New DXMenuItem("Imprimir", AddressOf PrintReportsInvoice, My.Resources.Print_32x32_blue))
                End If
                e.Menu.Items.Add(New DXMenuItem("Asignar Causa Inoportunidad", AddressOf OpenFormAssignImportunityCause, My.Resources.EditarAzul32))
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Aqui se ejecuta la accion de mostrar el detalle del oficio seleccionado en la rejilla
    ''' </summary>
    Private Sub INDrepbteDocumentActions_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDrepbteDocumentActions.ButtonClick
        Me.DetailDocument()
    End Sub

    ''' <summary>
    ''' Aqui se ejecuta la accion de mostrar el detalle de la factura seleccionada en la rejilla
    ''' </summary>
    Private Sub INDrepbteInvoiceActions_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDrepbteInvoiceActions.ButtonClick
        Me.DetailInvoice()
    End Sub

#End Region

#Region "ClickBack"

    ''' <summary>
    ''' Aqui se controla el regreso al nivel superior del proceso dependiendo en
    ''' cual me encuentre
    ''' </summary>
    Private Sub CtrNavigation_ClickBack() Handles CtrNavigation.ClickBack
        back()
    End Sub

#End Region

#Region "ShowingEditor"

    ''' <summary>
    ''' Se evita la edición en filas que tiene detalles Qx
    ''' </summary>
    Private Sub INDgdvInvoiceDetails_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDInvoiceDetailGv.ShowingEditor
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim row As GlosaMovementGlosa = view.GetRow(view.FocusedRowHandle)
        If CType(row, GlosaMovementGlosa).ListMovimientoAux.Count > 0 Then
            e.Cancel = True
        End If
        If view.FocusedColumn.Name.Equals(Me.V1Instance.Name) Or view.FocusedColumn.Name.Equals(Me.V2Instance.Name) Then
            If MovementsBlock.Contains(row.Id) Or row.CodeGlosaEvaluation Is Nothing Then
                e.Cancel = True
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al activarse el editor de las celdas sobre la rejilla 
    ''' </summary>
    Private Sub GlosaInvoiceDetailQX_ShowingEditor(sender As Object, e As CancelEventArgs) Handles GlosaInvoiceDetailQX.ShowingEditor
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim row As GlosaMovementGlosa = view.GetRow(view.FocusedRowHandle)
        If view.FocusedColumn.Name.Equals(Me.V1InstanceQX.Name) Or view.FocusedColumn.Name.Equals(Me.V2InstanceQX) Then
            If MovementsBlock.Contains(row.Id) Or row.CodeGlosaEvaluation Is Nothing Then
                e.Cancel = True
            End If
        End If
    End Sub

#End Region

#Region "ShownEditor"

    ''' <summary>
    ''' Maneja cuando se muestra el editor para convertir valores vacíos a 0 temporalmente y evitar errores de conversión
    ''' </summary>
    Private Sub INDInvoiceDetailGv_ShownEditor(sender As Object, e As EventArgs) Handles INDInvoiceDetailGv.ShownEditor
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)

        ' Solo procesar si es una columna de valor aceptado
        If view.FocusedColumn IsNot Nothing AndAlso (view.FocusedColumn.FieldName = Me.V1Instance.FieldName OrElse view.FocusedColumn.FieldName = Me.V2Instance.FieldName) Then
            Dim currentValue = view.GetRowCellValue(view.FocusedRowHandle, view.FocusedColumn)

            ' Si el valor es cadena vacía, establecerlo a 0 temporalmente para el editor
            ' Esto evita el error de conversión cuando RepositoryItemMoneyValue intenta convertir ""
            If currentValue IsNot Nothing AndAlso currentValue.ToString() = "" Then
                If view.ActiveEditor IsNot Nothing Then
                    view.ActiveEditor.EditValue = 0
                End If
            End If
        End If
    End Sub

#End Region

#Region "CustomRowCellEdit"

    ''' <summary>
    ''' Aqui se controla que se deshabilite el repositorio si la fila es maestra
    ''' </summary>
    Private Sub INDgdvInvoiceDetails_CustomRowCellEdit(sender As Object, e As CustomRowCellEditEventArgs) Handles INDInvoiceDetailGv.CustomRowCellEdit
        Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim data = CType(view.GetRow(e.RowHandle), GlosaMovementGlosa)
        If data IsNot Nothing Then
            If (data.GlosaInvoiceDetailQX IsNot Nothing AndAlso data.ListMovimientoAux.Count > 0) Then
                If e.Column Is Me.VMax OrElse e.Column Is Me.VMaxQX OrElse e.Column Is Me.VMaxGlosa OrElse e.Column Is Me.VMaxGlosaQX OrElse e.Column Is Me.VMaxReiterated OrElse e.Column Is Me.VMaxReiteratedQX OrElse e.Column Is Me.V1Instance OrElse e.Column Is Me.V2Instance OrElse e.Column Is Me.ConceptEval OrElse e.Column Is Me.ConceptGlosa OrElse e.Column Is Me.CommentGlosa OrElse e.Column Is Me.CommentReiteration OrElse e.Column Is Me.JustificationGlosa OrElse e.Column Is Me.JustificationReiteration OrElse e.Column Is Me.MoreInfo OrElse e.Column Is Me.ResponsibleThirdParty Then
                    Dim rep As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
                    rep.Buttons.Clear()
                    rep.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph))
                    rep.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
                    e.RepositoryItem = rep
                End If
            End If
        End If
    End Sub

#End Region

#Region "CellValueChanged"

    ''' <summary>
    ''' Aqui se valida que no se ingresen valores superiores al valor glosado o reiterado
    ''' </summary>
    Private Sub INDgdvInvoiceDetails_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles INDInvoiceDetailGv.CellValueChanged
        If e.Value IsNot Nothing Then
            If e.Column.FieldName = Me.V1Instance.FieldName Then
                If Convert.ToDecimal(e.Value) < 0 Or e.Value.ToString.Length > 15 Then
                    Me.INDInvoiceDetailGv.SetRowCellValue(e.RowHandle, Me.V1Instance, 0)
                Else
                    ValidateValueInline(e.RowHandle, True, False, INDInvoiceDetailGv)
                End If

                ' Si el valor es 0 y el concepto permite edición, convertir a "" para la validación de estilo
                Dim currentValue = Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.V1Instance)
                If currentValue IsNot Nothing AndAlso Convert.ToDecimal(currentValue) = 0 Then
                    Dim row = CType(Me.INDInvoiceDetailGv.GetRow(e.RowHandle), GlosaMovementGlosa)
                    If row IsNot Nothing AndAlso Not MovementsBlock.Contains(row.Id) Then
                        Me.INDInvoiceDetailGv.SetRowCellValue(e.RowHandle, Me.V1Instance, "")
                    End If
                End If
            ElseIf e.Column.FieldName = Me.V2Instance.FieldName Then
                If Convert.ToDecimal(e.Value) < 0 Or e.Value.ToString.Length > 15 Then
                    Me.INDInvoiceDetailGv.SetRowCellValue(e.RowHandle, Me.V2Instance, 0)
                Else
                    ValidateValueInline(e.RowHandle, False, False, INDInvoiceDetailGv)
                End If

                ' Si el valor es 0 y el concepto permite edición, convertir a "" para la validación de estilo
                Dim currentValue = Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.V2Instance)
                If currentValue IsNot Nothing AndAlso Convert.ToDecimal(currentValue) = 0 Then
                    Dim row = CType(Me.INDInvoiceDetailGv.GetRow(e.RowHandle), GlosaMovementGlosa)
                    If row IsNot Nothing AndAlso Not MovementsBlock.Contains(row.Id) Then
                        Me.INDInvoiceDetailGv.SetRowCellValue(e.RowHandle, Me.V2Instance, "")
                    End If
                End If
            End If
        Else
            Dim aux = Me.INDInvoiceDetailGv.GetRow(e.RowHandle)
            If aux IsNot Nothing Then
                Dim glosaMov As GlosaMovementGlosa = CType(aux, GlosaMovementGlosa)
                Dim maxSum = IIf(Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.MaxAux) IsNot Nothing, Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.MaxAux), 0)
                Dim valMax = IIf(Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.VMax) IsNot Nothing AndAlso Convert.ToDecimal(Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.VMax)) > 0, Convert.ToDecimal(Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.VMax)), 0)
                Dim valInput
                If e.Column.FieldName = Me.V1Instance.FieldName Then
                    valInput = Convert.ToDecimal(Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.V1Instance))
                Else
                    valInput = Convert.ToDecimal(Me.INDInvoiceDetailGv.GetRowCellValue(e.RowHandle, Me.V2Instance))
                End If
                Dim valMaxAux = valMax + maxSum
                Dim control = 0
                If valInput = 0 Then 'AndAlso maxSum <> 0 Then
                    If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType.AsInt = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                            OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov?.GlosaEvaluationType = "2") _
                                    AndAlso glosaMov.CodeGlosaEvaluation = CStr(ConceptsGlosaEvaluation.SubsanadaParcial)) Then
                        If e.Column.FieldName = Me.V1Instance.FieldName Then
                            ForMaxAccepted(True, glosaMov, valMaxAux, e.RowHandle, False, INDInvoiceDetailGv)
                        Else
                            ForMaxAccepted(False, glosaMov, valMaxAux, e.RowHandle, False, INDInvoiceDetailGv)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se valida que no se ingresen valores superiores al valor glosado o reiterado
    ''' </summary>
    Private Sub INDgdvInvoiceDetailQXs_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles GlosaInvoiceDetailQX.CellValueChanged
        If e.Value IsNot Nothing Then
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            If e.Column.FieldName = Me.V1InstanceQX.FieldName Then
                If Convert.ToDecimal(e.Value) < 0 Or e.Value.ToString.Length > 15 Then
                    view.SetRowCellValue(e.RowHandle, Me.V1InstanceQX, 0)
                Else
                    ValidateValueInline(e.RowHandle, True, True, view)
                End If
            ElseIf e.Column.FieldName = Me.V2InstanceQX.FieldName Then
                If Convert.ToDecimal(e.Value) < 0 Or e.Value.ToString.Length > 15 Then
                    view.SetRowCellValue(e.RowHandle, Me.V2InstanceQX, 0)
                Else
                    ValidateValueInline(e.RowHandle, False, True, view)
                End If
            End If
        Else
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
            Dim aux = view.GetRow(e.RowHandle)
            If aux IsNot Nothing Then
                Dim glosaMov As GlosaMovementGlosa = CType(aux, GlosaMovementGlosa)
                Dim maxSum = IIf(view.GetRowCellValue(e.RowHandle, Me.MaxAuxQX) IsNot Nothing, view.GetRowCellValue(e.RowHandle, Me.MaxAuxQX), 0)
                Dim valMax = IIf(view.GetRowCellValue(e.RowHandle, Me.VMaxQX) IsNot Nothing AndAlso Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, Me.VMaxQX)) > 0, Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, Me.VMaxQX)), 0)
                Dim valInput
                If e.Column.FieldName = Me.V1InstanceQX.FieldName Then
                    valInput = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, Me.V1InstanceQX))
                Else
                    valInput = Convert.ToDecimal(view.GetRowCellValue(e.RowHandle, Me.V2InstanceQX))
                End If
                Dim valMaxAux = valMax + maxSum
                Dim control = 0
                If valInput = 0 Then
                    If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType.AsInt = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                        OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov?.GlosaEvaluationType = "2") _
                                   AndAlso glosaMov.CodeGlosaEvaluation = CStr(ConceptsGlosaEvaluation.SubsanadaParcial)) Then
                        If e.Column.FieldName = Me.V1InstanceQX.FieldName Then
                            ForMaxAccepted(True, glosaMov, valMaxAux, e.RowHandle, False, view)
                        Else
                            ForMaxAccepted(False, glosaMov, valMaxAux, e.RowHandle, False, view)
                        End If
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Aqui se guarda en el comentario de justificación en el datasource
    ''' </summary>
    Private Sub INDreppceComment_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRepositoryJustificationPce.QueryPopUp
        Dim viewQX = Me.INDInvoiceDetailGdc.FocusedView
        Dim handled = CType(Me.INDInvoiceDetailGdc.FocusedView, DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle
        Dim objeto As GlosaMovementGlosa
        objeto = TryCast(viewQX.GetRow(handled), Domain.Entities.GlosaMovementGlosa)
        Dim CodeGlosaEvaluation As String
        If objeto.CodeGlosaEvaluation Is Nothing Then
            CodeGlosaEvaluation = "0"
        Else
            CodeGlosaEvaluation = objeto.CodeGlosaEvaluation
        End If


        Dim tmpComment As String = String.Empty
        If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
            tmpComment = objeto.JustificationGlosaText 'IIf(objeto.GlosaInvoiceDetail.GlosaMovementGlosa.Where(Function(c) c.Id = objeto.Id).SingleOrDefault.JustificationGlosa Is Nothing, String.Empty, objeto.GlosaInvoiceDetail.GlosaMovementGlosa.Where(Function(c) c.Id = objeto.Id).SingleOrDefault.JustificationGlosa)
        End If
        If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
            tmpComment = objeto.JustificationReiterationText 'IIf(objeto.GlosaInvoiceDetail.GlosaMovementGlosa.Where(Function(c) c.Id = objeto.Id).SingleOrDefault.JustificationReiteration Is Nothing, String.Empty, objeto.GlosaInvoiceDetail.GlosaMovementGlosa.Where(Function(c) c.Id = objeto.Id).SingleOrDefault.JustificationReiteration)
        End If




        Using _JustificationEvaluation As New JustificationEvaluation(CodeGlosaEvaluation)
            If objeto.JustificationGlosaText Is Nothing Or objeto.JustificationReiterationText Is Nothing Then
                _JustificationEvaluation.haveText = False
            Else
                _JustificationEvaluation.haveText = True
            End If
            _JustificationEvaluation.CommentHtml = tmpComment
            ' Asignar el valor del concepto de aceptación en la justificación si existe un valor previamente cargado
            If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                Dim itemMov = CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault
                If itemMov IsNot Nothing AndAlso itemMov.IdResponseHierarchyGlosa.HasValue Then
                    _JustificationEvaluation.IdResponseHierarchy = itemMov.IdResponseHierarchyGlosa
                End If
            ElseIf objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                Dim itemMov = CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault
                If itemMov IsNot Nothing AndAlso itemMov.IdResponseHierarchyReiteration.HasValue Then
                    _JustificationEvaluation.IdResponseHierarchy = itemMov.IdResponseHierarchyReiteration
                End If
            End If
            _JustificationEvaluation.ShowDialog(Me)
            If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationGlosa = _JustificationEvaluation.CommentHtml
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationGlosaText = _JustificationEvaluation.CommentOnlyText
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.IdResponseHierarchyGlosa = _JustificationEvaluation.IdResponseHierarchy
            End If
            If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationReiteration = _JustificationEvaluation.CommentHtml
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationReiterationText = _JustificationEvaluation.CommentOnlyText
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.IdResponseHierarchyReiteration = _JustificationEvaluation.IdResponseHierarchy
            End If
        End Using
    End Sub

    ''' <summary>
    ''' Evento al abrir el campo justificacion en la rejilla de movimientos
    ''' </summary>
    Private Sub INDRepositoryMoreInfoPce_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDRepositoryMoreInfoPce.QueryPopUp
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDInvoiceDetailGdc.FocusedView
        Dim handled = CType(Me.INDInvoiceDetailGdc.FocusedView, DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle
        Dim data As GlosaMovementGlosa = CType(view.GetRow(handled), GlosaMovementGlosa)
        If data IsNot Nothing Then
            Me.INDMoreInfoGc.DataSource = data.OtherMovements
            Me.INDMoreInfoGc.RefreshDataSource()
            Me.CtrXtraInfo.ListProperties.Clear()
            If data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Valor Glosado", data.ValueGlosado.ToString("C0")))
                Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Responsable Glosa", data.Responsible1.Name))
            ElseIf data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                If data.ValueReiterated.HasValue Then
                    Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Valor Reiterado", data.ValueReiterated.Value.ToString("C0")))
                    Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Responsable Reiteración", data.Responsible.Name))
                Else
                    Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Valor Reiterado", ""))
                    Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Responsable Reiteración", ""))
                End If
            End If
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento al cambiar los valores en el gridLookUp de conceptos de evaluación
    ''' </summary>
    Private Sub INDCodeGlosaEvaluationGle_EditValueChanged(sender As Object, e As EventArgs) Handles INDCodeGlosaEvaluationGle.EditValueChanged
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDInvoiceDetailGdc.FocusedView
        view.CloseEditor()
        Dim handler = view.FocusedRowHandle
        Dim row As GlosaMovementGlosa = CType(view.GetRow(handler), GlosaMovementGlosa)
        Dim lookup As DevExpress.XtraEditors.GridLookUpEdit = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)
        Dim value = lookup.EditValue
        Dim evaluationConcept = CType(lookup.GetSelectedDataRow(), Domain.Entities.ConceptGlosas)

        If evaluationConcept Is Nothing Then
            Exit Sub
        End If

        CleanJustificationEvaluation(row)
        CleanDetailInvoice(row, view, handler)

        'se establece el tipo de respuesta de glosa
        row.GlosaEvaluationType = evaluationConcept.Type

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.GlosaODevolucionInjustificada Then
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.NoSubsanada Then
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                'como es aceptacion total se postula el valor total glosado
                view.SetRowCellValue(handler, Me.V1Instance, row.ValueGlosado)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                'Dim valueControl = 0
                'If (row.MaxValueAccepted Is Nothing) Then
                '    valueControl = 0
                'Else
                '    If row.ValueGlosado > row.MaxValueAccepted Then
                '        valueControl = row.MaxValueAccepted
                '    Else
                '        valueControl = row.ValueGlosado
                '    End If
                'End If
                'como es aceptacion total se postula el valor total glosado
                view.SetRowCellValue(handler, Me.V2Instance, row.ValueGlosado)
            End If
        End If

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.SubsanadaParcial Then
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.Subsanada Then
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If {ConceptsGlosaEvaluationByType.DevolucionInjustificada, ConceptsGlosaEvaluationByType.DevolucionJustificada}.Contains(CInt(evaluationConcept.HomologateTypeByCodeAndResponse)) Then
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
            CType(view.GetRow(handler), GlosaMovementGlosa).IdGlosaEvaluation = evaluationConcept.Id
        End If
        If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
            CType(view.GetRow(handler), GlosaMovementGlosa).IdReiterationEvaluation = evaluationConcept.Id
        End If

        view.RefreshRow(handler)
    End Sub

#End Region

#Region "RowCellStyle"

    ''' <summary>
    ''' Evento para dar estilo a las filas de la rejilla de movimientos
    ''' </summary>
    Private Sub INDInvoiceDetailGv_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles INDInvoiceDetailGv.RowCellStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If e.RowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            Dim data = CType(View.GetRow(e.RowHandle), GlosaMovementGlosa)
            If data.ListMovimientoAux.Count = 0 Then
                If e.Column.FieldName = "ValueAcceptedFirstInstance" Then
                    Dim cell As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("ValueAcceptedFirstInstance"))
                    If cell = "" Then
                        e.Appearance.BackColor = System.Drawing.Color.LightGray
                        e.Appearance.ForeColor = System.Drawing.Color.LightSlateGray
                    Else
                        e.Appearance.BackColor = System.Drawing.Color.White
                        e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
                    End If
                End If
                If e.Column.FieldName = "ValueAcceptedSecondInstance" Then
                    If e.RowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
                        Dim cellR As String = View.GetRowCellDisplayText(e.RowHandle, View.Columns("ValueAcceptedSecondInstance"))
                        If cellR = "" Then
                            e.Appearance.BackColor = System.Drawing.Color.LightGray
                            e.Appearance.ForeColor = System.Drawing.Color.LightSlateGray
                        Else
                            e.Appearance.BackColor = System.Drawing.Color.White
                            e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
                        End If
                    End If
                End If
            End If
        Else
            e.Appearance.BackColor = System.Drawing.Color.LightBlue
            e.Appearance.BackColor2 = System.Drawing.Color.LightBlue
        End If
    End Sub

    ''' <summary>
    ''' Evento para dar estilo a las filas de la rejilla de movimientos QX
    ''' </summary>
    Private Sub GlosaInvoiceDetailQX_RowCellStyle(sender As Object, e As RowCellStyleEventArgs) Handles GlosaInvoiceDetailQX.RowCellStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If e.RowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Column.Name.Equals(Me.V1InstanceQX.Name) Then
                Dim cell As String = View.GetRowCellDisplayText(e.RowHandle, Me.V1InstanceQX)
                If cell = "" Then
                    e.Appearance.BackColor = System.Drawing.Color.LightGray
                    e.Appearance.ForeColor = System.Drawing.Color.LightSlateGray
                Else
                    e.Appearance.BackColor = System.Drawing.Color.White
                    e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
                End If
            End If
            If e.Column.Name.Equals(Me.V2InstanceQX.Name) Then
                Dim cellR As String = View.GetRowCellDisplayText(e.RowHandle, Me.V2InstanceQX)
                If cellR = "" Then
                    e.Appearance.BackColor = System.Drawing.Color.LightGray
                    e.Appearance.ForeColor = System.Drawing.Color.LightSlateGray
                Else
                    e.Appearance.BackColor = System.Drawing.Color.White
                    e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
                End If
            End If
        Else
            e.Appearance.BackColor = System.Drawing.Color.LightBlue
            e.Appearance.BackColor2 = System.Drawing.Color.LightBlue
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento para confirmar y guardar los datos de un oficio
    ''' </summary>
    Private Async Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Try
            If Me.INDSendDocumentDateDte.EditValue IsNot Nothing AndAlso Me.INDUserRadicateTxt.Text <> "" Then
                SimpleButton2.Enabled = False
                Dim serverDate = Await _model.GetServerDate
                With Me._currentDocument
                    .DateResponsePostDocument = serverDate
                    .DateRadicatedDocumentReply = Me.INDSendDocumentDateDte.EditValue
                    .DocumentCommentRadicated = Me.INDSendDocumentCommentMte.Text
                    .ReceivesTheSettled = Me.INDUserRadicateTxt.Text
                End With
                Dim ListTuppleInvocie As New List(Of String)
                For Each item As ViewCoordinationGlosaObjectionXpo In CType(Me.INDgdcInvoices.DataSource, List(Of ViewCoordinationGlosaObjectionXpo))
                    ListTuppleInvocie.Add(item.InvoiceNumber)
                Next
                Me.AsyncLoader(True)
                Dim result = Await Me._model.SaveDocument(Me._currentDocument, ListTuppleInvocie)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                    Me.AsyncOperation = True
                    Me.Documents = Me._model.ListDocuments()
                    Me.AsyncOperation = False
                    Me._currentGroup = EGroups.Root
                    Me.ControlsStatus = Me._currentGroup
                    Me.INDSendDocumentDateDte.EditValue = Nothing
                    Me.INDUserRadicateTxt.Text = ""
                    Me.INDSendDocumentCommentMte.Text = ""
                    SimpleButton2.Enabled = True
                    PopupControlContainer1.HidePopup()
                    If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.ImprimirReporte) Then
                        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesImprimir, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            Me.BarraBotones.PrintReport(PrintReportAction.ViewPrinting, Me._currentDocument.Id, 0, {Me._currentDocument.Id, False, Me.BarraBotones.OperatingUnit, False})
                        End If
                    End If
                Else
                    SimpleButton2.Enabled = True
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Dim strg As New StringBuilder
                        For Each item In result.MessageResult
                            strg.AppendLine(item.ToString())
                        Next
                        Mensaje(EeventViewerImages.Advertencia) = strg.ToString()
                    End If
                End If
            Else
                SimpleButton2.Enabled = True
                If Me.INDSendDocumentDateDte.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiDateConfirm.Text)
                    Exit Sub
                End If
                If Me.INDUserRadicateTxt.Text = String.Empty Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiUserConfirm.Text)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        Finally
            SimpleButton2.Enabled = True
        End Try
    End Sub

    ''' <summary>
    ''' Evento click para cerrar el popup de datos de confirmación
    ''' </summary>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        PopupControlContainer1.HidePopup()
    End Sub

    ''' <summary>
    ''' Evento click sobre el boton exportar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDbtnExport_Click(sender As Object, e As EventArgs) Handles INDbtnExport.Click
        If _currentDocument.Id = 0 Then
            Return
        End If

        Dim result = Await _model.ExportCoordinationGlosa(Me._currentDocument.Id)

        If result.Tables(0).Rows.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron datos para generar la información"
            Return
        End If

        Dim datos As DataTable = result.Tables(0)
        AsyncLoader(True)

        ' Guardar el libro en un archivo
        Dim fileName As String = Path.GetTempFileName() & ".xlsx"

        Try
            Await Task.Run(Sub()
                               Using workbook As New XLWorkbook()
                                   ' Agregar una hoja al libro
                                   Dim worksheet = workbook.Worksheets.Add("Datos")
                                   Dim _structure = Me.GenerateFileStructure(datos)

                                   Dim column As Integer = 1

                                   For Each col As DataColumn In _structure.Columns
                                       worksheet.Cell(1, column).Value = col.ColumnName
                                       column += 1
                                   Next

                                   Dim fila As Integer = 2

                                   For Each row As DataRow In _structure.Rows
                                       column = 1

                                       For Each col As DataColumn In _structure.Columns
                                           Dim value As String = row(col.ColumnName).ToString()
                                           If value.Length > 32767 Then
                                               value = value.Substring(0, 32767)
                                           End If
                                           worksheet.Cell(fila, column).Value = value
                                           column += 1
                                       Next

                                       fila += 1
                                   Next

                                   Dim listConcepts = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListConceptGlosasByFilter($"Type in ('2','5','6','7','8','9','10')")
                                   Dim listResponseHierarchy = XpoServiceEx.Instance(_indigoSessionValues.TransactionalContainer).GlosasService.ListCollectionResponseHierarchy
                                   WriteComments("V1", String.Join(" | ", listConcepts), worksheet)
                                   WriteComments("Y1", String.Join(String.Empty, listResponseHierarchy), worksheet)

                                   workbook.SaveAs(fileName)
                               End Using
                           End Sub)

            If System.IO.File.Exists(fileName) Then
                System.Diagnostics.Process.Start(fileName)
            End If
        Catch ex As Exception
            ShowMessage(EeventViewerImages.Advertencia) = IndigoManagementExceptions.GetExceptionDetails(ex)
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Clic sobre el boton cargar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnLoad_Click(sender As Object, e As EventArgs) Handles INDbtnLoad.Click
        _fileOpener.CheckPathExists = True
        _fileOpener.CheckFileExists = True
        _fileOpener.Filter = "Excel Files(.xlsx)|*.xlsx| Excel Files(.xls)|*.xls| " &
                             "Excel Files(*.xlsm)|*.xlsm"
        _fileOpener.Multiselect = False
        _fileOpener.AddExtension = True
        _fileOpener.ValidateNames = True
        If (_fileOpener.ShowDialog(Me) = DialogResult.OK) Then
            If _fileOpener.FileName <> String.Empty Then
                Dim fileInfo = New System.IO.FileInfo(_fileOpener.FileName)
                Dim rutaExcel = _fileOpener.FileName
                Using FrmLoad As New FrmLoadexcel
                    FrmLoad.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    FrmLoad.PathFile = rutaExcel
                    FrmLoad.Size = New System.Drawing.Size(800, 600)
                    FrmLoad.ModuleName = "COR"
                    'AddHandler FrmLoad.RefreshDocument, AddressOf RefreshCoordinationDocument
                    FrmLoad.ShowDialog()
                End Using
            End If
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Cerrando formulario
    ''' </summary>
    Private Sub FrmCoordination_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.IdEntity = String.Empty
    End Sub

#End Region

#Region "DoubleClick"

    ''' <summary>
    ''' Evento double click sobre rejilla de oficio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgdvDocuments_DoubleClick(sender As Object, e As EventArgs) Handles INDgdvDocuments.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            Me.DetailDocument()
        End If
    End Sub

    ''' <summary>
    ''' Evento double click sobre rejilla de factura
    ''' </summary>
    Private Sub INDgcvInvoices_DoubleClick(sender As Object, e As EventArgs) Handles INDgdvInvoices.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            DetailInvoice()
        End If
    End Sub

#End Region

#Region "SelectionChanged"
    Private Sub INDInvoiceDetailGv_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles INDInvoiceDetailGv.SelectionChanged
        Dim Currency As Boolean = True
        Dim sum As Decimal = 0
        For Each c As GridCell In Me.INDInvoiceDetailGv.GetSelectedCells()
            If c.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric Then
                sum += Convert.ToDecimal(Me.INDInvoiceDetailGv.GetRowCellValue(c.RowHandle, c.Column))
                If c.Column.DisplayFormat.FormatString = String.Empty Then
                    Currency = False
                End If
            End If
        Next
        If Currency = True Then
            Me.INDtxtTotalSelection.Text = ManageDecimalsFun(sum)
        Else
            Me.INDtxtTotalSelection.Text = FormatNumber(sum, 2)
        End If
    End Sub

    Private Sub GlosaInvoiceDetailQX_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles GlosaInvoiceDetailQX.SelectionChanged
        Dim Currency As Boolean = True
        Dim Sum As Decimal
        Dim detailview2 As GridView = INDInvoiceDetailGv
        Dim detailView3 As GridView = TryCast(detailview2.GetDetailView(detailview2.FocusedRowHandle, detailview2.GetRelationIndex(detailview2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        If detailView3 IsNot Nothing Then
            For Each c As GridCell In detailView3.GetSelectedCells()
                If c.Column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric Then
                    Sum += Convert.ToDecimal(detailView3.GetRowCellValue(c.RowHandle, c.Column))
                    If c.Column.DisplayFormat.FormatString = String.Empty Then
                        Currency = False
                    End If
                End If
            Next
        End If
        If Currency = True Then
            Me.INDtxtTotalSelection.Text = ManageDecimalsFun(Sum)
        Else
            Me.INDtxtTotalSelection.Text = FormatNumber(Sum, 2)
        End If
    End Sub

#End Region

#Region "ToolBar Events"

    ''' <summary>
    ''' Se ejecuta la ccion de confirmar dependiendo del contexto en que se encuentre situado el proceso
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar, BarraBotones.Click_GuardarConfirmar
        If Me._currentGroup = EGroups.SelectedDocument Then
            ConfirmDocument()
        End If
        If Me._currentGroup = EGroups.SelectedInvoice Then
            Me.Confirm()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarLiquidacion) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
        Me.BarraBotones.RibbonPageEdicion.Visible = False
        Me.BarraBotones.RibbonPageProcesos.Visible = False
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Aqui se reasignan los estados a los botones de la barra de herramientas segun el proceso a realizar
    ''' </summary>
    Private Sub BarraBotones_VerificaPermisoCustomizar() Handles BarraBotones.VerificaPermisoCustomizar
        Me.ControlsStatus = Me._currentGroup
    End Sub

    ''' <summary>
    ''' Ejecuta la accion de actualizar rejilla 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_RefreshGrid() Handles BarraBotones.Click_RefreshGrid
        DocumentsLoad()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
        End If
    End Sub

#End Region

#End Region

End Class