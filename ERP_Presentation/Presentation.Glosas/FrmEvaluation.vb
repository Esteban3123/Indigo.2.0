'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz
' Created          : 2013-07-02
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
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
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas.MVP

#End Region

''' <summary>
''' Vista del frontal de evaluación
''' </summary>
Public Class FrmEvaluation
    Implements IEvaluation

#Region "Fields"

    ''' <summary>
    ''' Conjunto de datos que contiene los campos personalizables
    ''' </summary>
    Private _customizableFields As DataTable
    ''' <summary>
    ''' Bandera usada para verificar si existe o no una definicion del funcional
    ''' </summary>
    Private _frontDefinicionExists As Boolean
    ''' <summary>
    ''' Ruta de las definiciones del layout
    ''' </summary>
    Private _pathFunctionalDefinitions As String
    ''' <summary>
    ''' Hilo para cargar las definiciones del funcional
    ''' </summary>
    Private WithEvents DefinitionsLoader As BackgroundWorker
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _myModel As MEvaluation
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PEvaluation
    ''' <summary>
    ''' Numero factura
    ''' </summary>
    Private _invoiceNumber As String

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
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Variable bandera para el registro bloqueado
    ''' </summary>
    Private _recordFlag As Boolean
    ''' <summary>
    ''' Control de trazabilidad
    ''' </summary>
    Friend WithEvents CtrTraceabilityControl As Presentation.Controls.CtrTraceabilityControl
    ''' <summary>
    ''' Objeto anonimo que encapsula los datos de la factura
    ''' </summary>
    Private _objDetail As Object
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Parametros de Glosas
    ''' </summary>
    Private _parameterGlosas As TimeParameters

#End Region

#Region "Properties"

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
    ''' Propiedad que contiene el listado de facturas.
    ''' </summary>
    Public Property DataSourceInvoices As Object Implements IEvaluation.DataSourceInvoices
        Get
            Return INDInvoicesGc.DataSource
        End Get
        Set(value As Object)
            INDInvoicesGc.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de detalles de facturas.
    ''' </summary>
    Public Property DataSourceDetail As List(Of GlosaInvoiceDetail) Implements IEvaluation.DataSourceDetail
        Get
            Return INDInvoiceDetailGdc.DataSource
        End Get
        Set(value As List(Of GlosaInvoiceDetail))
            INDInvoiceDetailGdc.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el código del usuario que bloqueo la factura.
    ''' </summary>
    Public Property CodeUserWithInvoice As String Implements IEvaluation.CodeUserWithInvoice
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
    Public Property NameUserWithInvoice As String Implements IEvaluation.NameUserWithInvoice
        Get
            Return Me._codeUserWithInvoice
        End Get
        Set(value As String)
            Me._codeUserWithInvoice = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmEvaluationMetaData, Eform.InfoMetaData), Me._objDetail.InvoiceNumber, Me._objDetail.Comment, Me._objDetail.PatientCode, Me._objDetail.PatientName, Me._objDetail.NitToPersist), .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me._objDetail.Id & "#$", .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmEvaluationMetaDataTitle, Eform.InfoMetaData), Me._objDetail.Id), .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmEvaluationMetaData, Eform.InfoMetaData), Me._objDetail.InvoiceNumber, Me._objDetail.Comment, Me._objDetail.PatientCode, Me._objDetail.PatientName, Me._objDetail.NitToPersist)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmEvaluationMetaDataTitle, Eform.InfoMetaData), Me._objDetail.Id)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Crea y carga el control de tiempo en la barra
    ''' </summary>
    Private Sub LoadXtraTrackControl()
        Me.CtrTraceabilityControl = New CtrTraceabilityControl()
        Me.CtrTraceabilityControl.Process = GlosasProcess.ObjectionEvaluation
        Me.CtrTraceabilityControl.Dock = DockStyle.Fill
        Me.AdditionalControlPanel.Controls.Add(Me.CtrTraceabilityControl)
    End Sub

    ''' <summary>
    ''' Permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Regresar a la rejilla principal
    ''' </summary>
    Private Sub back()
        Me.INDMainInvoiceDetailCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDInvoiceDetailLyg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        'Me.BarraBotones.RibbonPagEform.Visible = False
        LoadInvoice()
        Me.INDInvoicesGc.RefreshDataSource()
        'Me.CtrClinicalHistory.Visible = False
        Me.AdditionalControlPanel.Visible = False
        Me.INDInvoicesMainLgc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        DeleteBlockedRecord()
        Me.BarraBotones.EnableBarItems()
        record = Nothing
        Me._doc = Nothing
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await _myModel.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo encargado de expandir las vistas de la rejilla de detalle de facturas
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

    ''' <summary>
    ''' Metodo para mostrar los detalles de una factura
    ''' </summary>
    Private Async Sub ShowDetail()
        Me.AsyncLoader(True)
        Me.INDInvoicesMainLgc.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me._objDetail = DirectCast(Me.INDInvoicesGv.GetRow(Me.INDInvoicesGv.FocusedRowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow
        Dim objectMovements As List(Of GlosaMovementGlosa)
        Me.RepositoryCodeGlosaEvaluation.DataSource = Await Me._myModel.ListConceptsGlosaByTypes(New List(Of String) From {"2", "5", "6", "7", "8"})
        _invoiceNumber = _objDetail.InvoiceNumber
        objectMovements = Await Me._myModel.listMovementsByInvoiceAndResponsible(_objDetail.InvoiceNumber, SessionValues.Instance.UserIndigo)
        Me.CtrTraceabilityControl.Visible = True
        If _objDetail.DocumentType = "1" Then
            Me.CtrTraceabilityControl.Process = Presentation.Base.GlosasProcess.ObjectionEvaluation
        Else
            Me.CtrTraceabilityControl.Process = Presentation.Base.GlosasProcess.ReiterationEvaluation
        End If
        Me.CtrTraceabilityControl.Invoice = _objDetail.InvoiceNumber
        Me.CtrTraceabilityControl.Entity = _objDetail.CustomerId
        Me.CtrTraceabilityControl.CargarControl()
        Me.AdditionalControlPanel.Visible = True
        If _objDetail.DocumentType = "1" Then
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
            Me.ResponsibleThirdParty.VisibleIndex = 6
            Me.ConceptEval.VisibleIndex = 7
            Me.JustificationGlosa.VisibleIndex = 8
            Me.V1Instance.VisibleIndex = 9

            Me.CommentReiterationQX.Visible = False
            Me.V2InstanceQX.Visible = False
            Me.CommentReiterationQX.Visible = False
            Me.JustificationReiterationQX.Visible = False

            Me.CommentGlosaQX.Visible = True
            Me.V1InstanceQX.Visible = True
            Me.CommentGlosaQX.Visible = True
            Me.JustificationGlosaQX.Visible = True

            Me.ServiceQX.VisibleIndex = 1
            Me.ConceptGlosaQX.VisibleIndex = 2
            Me.CommentGlosaQX.VisibleIndex = 3
            Me.VMaxQX.VisibleIndex = 4
            Me.ValueGlosadoQX.VisibleIndex = 5
            Me.ResponsibleThirdPartyQX.VisibleIndex = 6
            Me.ConceptEvalQX.VisibleIndex = 7
            Me.JustificationGlosaQX.VisibleIndex = 8
            Me.V1InstanceQX.VisibleIndex = 9

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

        End If

        If _objDetail.DocumentType = "2" Then

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
            Me.ResponsibleThirdParty.Visible = 6
            Me.ConceptEval.VisibleIndex = 7
            Me.JustificationReiteration.VisibleIndex = 8
            Me.V2Instance.VisibleIndex = 9

            Me.CommentGlosaQX.Visible = False
            Me.V1InstanceQX.Visible = False
            Me.CommentGlosaQX.Visible = False
            Me.JustificationGlosaQX.Visible = False
            Me.ValueGlosadoQX.Visible = False

            Me.CommentReiterationQX.Visible = True
            Me.V2InstanceQX.Visible = True
            Me.CommentReiterationQX.Visible = True
            Me.JustificationReiterationQX.Visible = True

            Me.ServiceQX.VisibleIndex = 1
            Me.ConceptGlosaQX.VisibleIndex = 2
            Me.CommentReiterationQX.VisibleIndex = 3
            Me.VMaxQX.VisibleIndex = 4
            Me.ValueReiteratedQX.VisibleIndex = 5
            Me.ResponsibleThirdPartyQX.VisibleIndex = 6
            Me.ConceptEvalQX.VisibleIndex = 7
            Me.JustificationReiterationQX.VisibleIndex = 8
            Me.V2InstanceQX.VisibleIndex = 9

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
        Me.INDInvoiceNumberLbl.Text = _objDetail.InvoiceNumber
        Me.INDIngressNumberLbl.Text = _objDetail.IngressNumber
        Me.INDPatientLbl.Text = _objDetail.PatientName
        Me.INDStateCmbe.EditValue = _objDetail.State
        ExpandView()
        Me.INDMainInvoiceDetailCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDInvoiceDetailLyg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        record = Await _myModel.GetBlockRecord(Me.Tag, _objDetail.Id)
        Me.GetDocumentIndexed(Me.Tag & "_" & Me._objDetail.Id)
        If record.Id = 0 Then
            Me.BarraBotones.SetDocuments(_objDetail.Id)
            Dim state = New Domain.Base.Entities.ObjectChangeTracker
            state.State = Domain.Base.Entities.ObjectState.Added
            record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me._indigoSessionValues.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me._indigoSessionValues.UserIndigo, .IdRecord = _objDetail.Id}
            Dim operation = Await _myModel.SaveBlockRecord(record)
            record = operation.ObjectEmbbeded
            Me._recordFlag = False
        Else
            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
            Me._recordFlag = True
        End If
        If objectMovements.Count > 0 Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlySaveWithoutUndoAndFind)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.None)
        End If
        Me.AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Metodo para abrir un PopUp de búsqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la acción de buscar
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la acción de deshacer
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la acción de eliminar
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Sub ValidateListInvoice()
        For Each item As GlosaMovementGlosa In Me.INDInvoiceDetailGdc.DataSource
            If item.ListMovimientoAux.Count = 0 Then
                If item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or item.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
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
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la acción de guardar
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            Me.GlosaInvoiceDetailQX.CloseEditor()
            Me.INDInvoiceDetailGv.CloseEditor()
            ValidateListInvoice()
            Me.AsyncLoader(True)
            Dim listMOv As New List(Of GlosaMovementGlosa) ' = CType(Me.INDInvoiceDetailGv.DataSource, List(Of GlosaMovementGlosa))
            Dim result As New ActionResult
            'If listMOv IsNot Nothing AndAlso listMOv.Count > 0 AndAlso listMOv(0).InvoiceDetailIdQX IsNot Nothing AndAlso listMOv(0).InvoiceDetailIdQX > 0 Then
            '    result = Await Me._myModel.saveMov(listMOv(0).ListMovimientoAux)
            'Else
            '    result = Await Me._myModel.saveMov(Me.INDInvoiceDetailGv.DataSource)
            'End If
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
            result = Await Me._myModel.saveMov(listMOv, _idOperativeUnit)
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
                    back()
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Dim StrBuilder As New StringBuilder
                        For Each item As String In result.MessageResult
                            StrBuilder.AppendLine(item)
                        Next
                        Me.Mensaje(EeventViewerImages.MensajeError) = StrBuilder.ToString()
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                    Dim listMovementGlosa = Await Me._myModel.listMovementsByInvoiceAndResponsible(_invoiceNumber, _indigoSessionValues.UserIndigo)
                    If listMovementGlosa.Count > 0 Then
                        Me.INDInvoiceDetailGdc.DataSource = listMovementGlosa
                        Me.INDInvoiceDetailGdc.RefreshDataSource()
                        ExpandView()
                    Else
                        back()
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
            Me.AsyncLoader(False)
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la acción de guardar
    ''' </summary>
    Public Async Sub Confirmar()
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesPreguntaConfirmar, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.GlosaInvoiceDetailQX.CloseEditor()
            Me.INDInvoiceDetailGv.CloseEditor()
            ValidateListInvoice()
            Me.AsyncLoader(True)
            Dim result = Await Me._myModel.saveMov(Me.INDInvoiceDetailGv.DataSource, _idOperativeUnit)
            Me.AsyncLoader(False)
            If result.StateResult Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                back()
                'Reconsulto Objeciones
                Me.DataSourceInvoices = Await Me._myModel.ListObjectionReceptionDByResponsable(_indigoSessionValues.UserIndigo, _idOperativeUnit)
                Me.INDInvoicesGc.RefreshDataSource()
            Else
                If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo para ejecutar la acción de nuevo
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub

    ''' <summary>
    ''' Metodo para disparar el formulario de evaluacion general.
    ''' </summary>
    Async Sub ShowGeneralMovements()
        Dim lista = New List(Of GlosaMovementGlosa)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDInvoiceDetailGdc.FocusedView
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
            Dim generalEvaluation As Generalevaluation = New Generalevaluation("EVALUACION", lista, _option, _myModel, CStr(MyBase.Tag), _idOperativeUnit)
            generalEvaluation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            'logica para determinar si el formulario va manejar decimales.
            generalEvaluation.ManageDecimals = PopupManageDecimals()
            Dim result = generalEvaluation.ShowDialog(Me)
            If result = System.Windows.Forms.DialogResult.OK Then
                Dim data As List(Of GlosaMovementGlosa) = Await Me._myModel.listMovementsByInvoiceAndResponsible(invoice, _indigoSessionValues.UserIndigo)
                Me.INDInvoiceDetailGdc.DataSource = data
                Me.INDInvoiceDetailGdc.RefreshDataSource()
                ExpandView()
                'Guardar()
            End If
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

    ''' <summary>
    ''' Metodo encargado de mezclar los cambios en las entidades
    ''' </summary>
    Private Async Sub MergeEntities()
        Dim data As List(Of GlosaMovementGlosa) = Await Me._myModel.listMovementsByInvoiceAndResponsible(_invoiceNumber, SessionValues.Instance.UserIndigo)
        Dim source As List(Of GlosaMovementGlosa) = New List(Of GlosaMovementGlosa)
        For Each item As GlosaMovementGlosa In Me.INDInvoiceDetailGdc.DataSource
            source.Add(item)
        Next
        Me.INDInvoiceDetailGdc.DataSource = data
        ExpandView()
        For Each item As GlosaMovementGlosa In Me.INDInvoiceDetailGdc.DataSource
            If item.ListMovimientoAux.Count = 0 Then
                If source.Exists(Function(c) c.Id = item.Id) Then
                    Dim glosa As GlosaMovementGlosa = source.Where(Function(c) c.Id = item.Id).FirstOrDefault
                    item.JustificationGlosa = glosa.JustificationGlosa
                    item.CodeGlosaEvaluation = glosa.CodeGlosaEvaluation
                    Dim handler = INDInvoiceDetailGv.LocateByValue("Id", item.Id)
                    INDInvoiceDetailGv.SetRowCellValue(handler, Me.V1Instance, glosa.ValueAcceptedFirstInstance)
                End If
            Else
                Dim handler = INDInvoiceDetailGv.LocateByValue("Id", item.Id)
                Dim sourceQX As GlosaMovementGlosa = source.Where(Function(c) c.Id = item.Id).FirstOrDefault
                For Each itemQX As GlosaMovementGlosa In item.ListMovimientoAux
                    If sourceQX.ListMovimientoAux.Exists(Function(c) c.Id = itemQX.Id) Then
                        Dim glosa As GlosaMovementGlosa = sourceQX.ListMovimientoAux.Where(Function(c) c.Id = itemQX.Id).FirstOrDefault
                        itemQX.JustificationGlosa = glosa.JustificationGlosa
                        itemQX.CodeGlosaEvaluation = glosa.CodeGlosaEvaluation
                        'itemQX.ValueAcceptedFirstInstance = glosa.ValueAcceptedFirstInstance
                        Dim detailInvoicesQX As DevExpress.XtraGrid.Views.Grid.GridView = TryCast(Me.INDInvoiceDetailGv.GetDetailView(handler, Me.INDInvoiceDetailGv.GetRelationIndex(handler, "ListMovimientoAux")), DevExpress.XtraGrid.Views.Grid.GridView)
                        Dim handlerQX = detailInvoicesQX.LocateByValue("Id", itemQX.Id)
                        detailInvoicesQX.SetRowCellValue(handlerQX, Me.V1InstanceQX, glosa.ValueAcceptedFirstInstance)
                    End If
                Next
            End If
        Next
    End Sub

    ''' <summary>
    ''' Cargar las facturas del responsable logueado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadInvoice()
        AsyncLoader(True)
        Using model As New MBusqueda
            Me.DataSourceInvoices = model.ConsultarEntidades(eDataSource.ListEvaluationInvoiceByResponsible, _indigoSessionValues.UserIndigo)
        End Using
        'Me.DataSourceInvoices = Await Me._myModel.ListObjectionReceptionDByResponsable(_indigoSessionValues.UserIndigo)
        AsyncLoader(False)
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
                For Each item In INDInvoiceDetailGv.Columns
                    If item.DisplayFormat.FormatString = "C" OrElse item.DisplayFormat.FormatString = "c" Then
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
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

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Async Sub FrmEvaluation_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Aqui se carga el control de tiempo
        Me.LoadXtraTrackControl()
        '***Inicializar variables****'
        Me._indigoSessionValues = SessionValues.Instance
        Me.MovementsBlock = New List(Of Integer)
        '****************************'
        Me.IndigoGridControl1.SetHoldSize(Me.INDInvoiceDetailGdc, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDInvoicesGc, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDMoreInfoGc, True)
        Me.INDInvoiceDetailCtrNavigation.Group = Me.INDInvoiceDetailLyg
        Me.Funct = AddressOf GenerateDoc
        Me._doc = Nothing
        Me._presenter = New PEvaluation(Me)
        Me._myModel = New MEvaluation(Me.Tag)
        LoadInvoice()
        LoadThirdParty()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Await LoadParameters()
    End Sub

    ''' <summary>
    ''' Se guardan las modificaciones realizadas en la definicion del frontal
    ''' </summary>
    Private Sub INDDevolutionLyc_HideCustomization(sender As Object, e As EventArgs) Handles INDEvaluationLyc.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If Me.INDEvaluationLyc.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(Me._indigoSessionValues.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    Me.INDEvaluationLyc.SaveLayoutToXml(Me._pathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Evento al abrir el popup donde se encuentra el RichTextEdit
    ''' </summary>
    Private Async Sub RepositoryItemPopupContainerEdit2_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEdit2.QueryPopUp
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
            tmpComment = objeto.JustificationGlosa
        End If
        If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
            tmpComment = objeto.JustificationReiteration
        End If

        Using _JustificationEvaluation As New JustificationEvaluation(CodeGlosaEvaluation)
            _JustificationEvaluation.CommentHtml = tmpComment
            _JustificationEvaluation.ShowDialog(Me)
            If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationGlosa = _JustificationEvaluation.CommentHtml
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationGlosaText = _JustificationEvaluation.CommentOnlyText
            End If
            If objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or objeto.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationReiteration = _JustificationEvaluation.CommentHtml
                CType(viewQX.DataSource, List(Of GlosaMovementGlosa)).Where(Function(d) objeto.Id = d.Id).FirstOrDefault.JustificationReiterationText = _JustificationEvaluation.CommentOnlyText
            End If
        End Using

    End Sub



    ''' <summary>
    ''' Evento para eliminar la relacion en la rejilla principal
    ''' </summary>
    Private Sub INDInvoicesGv_MasterRowGetRelationCount(sender As Object, e As DevExpress.XtraGrid.Views.Grid.MasterRowGetRelationCountEventArgs) Handles INDInvoicesGv.MasterRowGetRelationCount
        e.RelationCount = 0
    End Sub

    ''' <summary>
    ''' Evento sobre la rejilla de detalles de factura que modifica los repositorios
    ''' </summary>
    Private Sub INDInvoiceDetailGv_CustomRowCellEdit(sender As Object, e As DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventArgs) Handles INDInvoiceDetailGv.CustomRowCellEdit
        Dim view = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim data = CType(view.GetRow(e.RowHandle), GlosaMovementGlosa)
        If data IsNot Nothing Then
            If (data.GlosaInvoiceDetailQX IsNot Nothing AndAlso data.ListMovimientoAux.Count > 0) Then
                If e.Column Is Me.VMaxQX OrElse e.Column Is Me.VMax OrElse e.Column Is Me.VMaxGlosa OrElse e.Column Is Me.VMaxGlosaQX OrElse e.Column Is Me.VMaxReiterated OrElse e.Column Is Me.VMaxReiteratedQX OrElse e.Column Is Me.V1Instance OrElse e.Column Is Me.V2Instance OrElse e.Column Is Me.ConceptEval OrElse e.Column Is Me.ConceptGlosa OrElse e.Column Is Me.CommentGlosa OrElse e.Column Is Me.CommentReiteration OrElse e.Column Is Me.JustificationGlosa OrElse e.Column Is Me.JustificationReiteration OrElse e.Column Is Me.MoreInfo OrElse e.Column Is Me.ResponsibleThirdParty Then
                    Dim rep As New DevExpress.XtraEditors.Repository.RepositoryItemPopupContainerEdit()
                    rep.Buttons.Clear()
                    rep.Buttons.Add(New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph))
                    rep.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor
                    e.RepositoryItem = rep
                End If
            End If
        End If
    End Sub

    Sub ValidateValueInline(rowHandle As Integer, IsGlosa As Boolean, isQX As Boolean, view As GridView)
        Dim VAlueMaxGlosa As Decimal
        Dim glosaMov As GlosaMovementGlosa
        Dim maxSum = 0
        Dim valMax = 0

        If isQX Then
            Dim mainView As GridView = Me.INDInvoiceDetailGv
            Dim detailView2 As GridView = Me.INDInvoiceDetailGdc.FocusedView
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

        Dim valMaxAux = valMax + maxSum
        Dim control = 0
        If valMaxAux < valInput Then

            If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType.AsInt = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov.GlosaEvaluationType = "2") _
                        AndAlso glosaMov.CodeGlosaEvaluation = ConceptsGlosaEvaluation.SubsanadaParcial) Then
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
                OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov.GlosaEvaluationType = "2") _
                            AndAlso glosaMov.CodeGlosaEvaluation = ConceptsGlosaEvaluation.SubsanadaParcial) Then
                If IsGlosa Then
                    If valInput = 0 OrElse glosaMov.ValueGlosado <= valInput Then
                        If isQX Then
                            view.SetRowCellValue(rowHandle, Me.V1InstanceQX, "")
                        Else
                            view.SetRowCellValue(rowHandle, Me.V1Instance, "")
                        End If
                        control = 1
                    End If
                Else
                    If valInput = 0 OrElse glosaMov.ValueReiterated <= valInput Then
                        If isQX Then
                            view.SetRowCellValue(rowHandle, Me.V2InstanceQX, "")
                        Else
                            view.SetRowCellValue(rowHandle, Me.V2Instance, "")
                        End If
                        control = 1
                    End If
                End If
            End If
            Dim valNew
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

            ' para el control de salod, que lo aceptado no supere lo glosado
            'Dim ObjMovMain = CType(view.DataSource, List(Of GlosaMovementGlosa)).Where(Function(c As GlosaMovementGlosa) c.MainGlosa = True And c.InvoiceDetailId = glosaMov.InvoiceDetailId)

            VAlueMaxGlosa = glosaMov.MaxValueAcceptedGeneral

            'lista de movimiento por servicio 
            Dim tmpListMov = CType(view.DataSource, List(Of GlosaMovementGlosa)).Where(Function(c As GlosaMovementGlosa) c.InvoiceDetailId = glosaMov.InvoiceDetailId)
            Dim listmov As New List(Of GlosaMovementGlosa)
            listmov.AddRange(tmpListMov.ToList)

            If IsGlosa Then
                Dim SumValueAccept As Decimal
                For i As Integer = 0 To listmov.Count - 1
                    If listmov(i).ValueAcceptedFirstInstance IsNot Nothing AndAlso listmov(i).ValueAcceptedFirstInstance > 0 Then
                        SumValueAccept += listmov(i).ValueAcceptedFirstInstance
                    End If
                Next

                SumValueAccept = SumValueAccept '+ valNew
                If SumValueAccept > VAlueMaxGlosa Then

                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorAceptadoMayorValorGlosado, Eform.Coordinacion), FormatCurrency(SumValueAccept.ToString(indigo.Culture)), FormatCurrency(VAlueMaxGlosa.ToString(indigo.Culture)))
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

                SumValueAccept = SumValueAccept '+ valNew
                If SumValueAccept > VAlueMaxGlosa Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(ValorAceptadoMayorValorGlosado, Eform.Coordinacion), FormatCurrency(SumValueAccept.ToString(indigo.Culture)), FormatCurrency(VAlueMaxGlosa.ToString(indigo.Culture)))
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
        Dim QXId As String = IIf(glosaMov.InvoiceDetailIdQX Is Nothing, Nothing, glosaMov.InvoiceDetailIdQX.ToString)
        Dim data
        If IsQX Then
            data = CType(view.DataSource, List(Of GlosaMovementGlosa)).Where(Function(c) c.InvoiceDetailId = glosaMov.InvoiceDetailId AndAlso c.InvoiceDetailIdQX = (QXId))
        Else
            data = CType(view.DataSource, List(Of GlosaMovementGlosa)).Where(Function(c) c.InvoiceDetailId = glosaMov.InvoiceDetailId AndAlso c.InvoiceDetailIdQX.Equals(QXId))
        End If
        For Each item As GlosaMovementGlosa In data
            If IsGlosa Then
                item.ValueGlosaMaxAccepted = "$" + item.ValueGlosado.ToString + " - $" + valMaxAux.ToString
            Else
                item.ValueReiteratedMaxAccepted = "$" + item.ValueReiterated.ToString + " - $" + valMaxAux.ToString
            End If
            item.MaxValueAccepted = valMaxAux.ToString
        Next
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando cambian ciertas celdas de la rejilla de detalles de factura
    ''' </summary>
    Private Sub INDInvoiceDetailGv_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles INDInvoiceDetailGv.CellValueChanged
        If e.Value IsNot Nothing Then
            If e.Column.FieldName = Me.V1Instance.FieldName Then
                If Convert.ToDecimal(e.Value) < 0 Or e.Value.ToString.Length > 15 Then
                    Me.INDInvoiceDetailGv.SetRowCellValue(e.RowHandle, Me.V1Instance, 0)
                Else
                    ValidateValueInline(e.RowHandle, True, False, INDInvoiceDetailGv)
                End If
            ElseIf e.Column.FieldName = Me.V2Instance.FieldName Then
                If Convert.ToDecimal(e.Value) < 0 Or e.Value.ToString.Length > 15 Then
                    Me.INDInvoiceDetailGv.SetRowCellValue(e.RowHandle, Me.V2Instance, 0)
                Else
                    ValidateValueInline(e.RowHandle, False, False, INDInvoiceDetailGv)
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
                    If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                        OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov.GlosaEvaluationType = "2") _
                                    AndAlso glosaMov.CodeGlosaEvaluation = ConceptsGlosaEvaluation.SubsanadaParcial) Then
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
    ''' Evento que se dispara cuando cambian ciertas celdas de la rejilla de detalles de factura QX
    ''' </summary>
    Private Sub GlosaInvoiceDetailQX_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GlosaInvoiceDetailQX.CellValueChanged
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
                If valInput = 0 Then 'AndAlso maxSum <> 0 Then
                    If (glosaMov?.GlosaEvaluationType?.AsInt IsNot Nothing AndAlso glosaMov.GlosaEvaluationType = ConceptsGlosaEvaluationByType.SubsanadaParcial) _
                        OrElse ((String.IsNullOrEmpty(glosaMov?.GlosaEvaluationType) OrElse glosaMov.GlosaEvaluationType = "2") _
                                    AndAlso glosaMov.CodeGlosaEvaluation = ConceptsGlosaEvaluation.SubsanadaParcial) Then
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

    ''' <summary>
    ''' Evento para cambiar el estilo de las celdas en la refilla de detalles de factura
    ''' </summary>
    Private Sub INDInvoiceDetailGv_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles INDInvoiceDetailGv.RowCellStyle
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
    ''' Evento para cambiar el estilo de las celdas en la refilla de detalles de factura QX
    ''' </summary>
    Private Sub GlosaInvoiceDetailQX_RowCellStyle(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs) Handles GlosaInvoiceDetailQX.RowCellStyle
        Dim View As DevExpress.XtraGrid.Views.Grid.GridView = sender
        If e.RowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Column.Name.Equals(Me.V1InstanceQX.Name) Then '"ValueAcceptedFirstInstance" Then
                Dim cell As String = View.GetRowCellDisplayText(e.RowHandle, Me.V1InstanceQX) ' View.Columns("ValueAcceptedFirstInstance"))
                If cell = "" Then
                    e.Appearance.BackColor = System.Drawing.Color.LightGray
                    e.Appearance.ForeColor = System.Drawing.Color.LightSlateGray
                Else
                    e.Appearance.BackColor = System.Drawing.Color.White
                    e.Appearance.ForeColor = System.Drawing.Color.DarkSlateGray
                End If
            End If
            If e.Column.Name.Equals(Me.V2InstanceQX.Name) Then
                Dim cellR As String = View.GetRowCellDisplayText(e.RowHandle, Me.V2InstanceQX) ' 
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

    ''' <summary>
    ''' Evento que se dispara cuando se habilita la edición de una celda
    ''' </summary>
    Private Sub INDInvoiceDetailGv_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDInvoiceDetailGv.ShowingEditor
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
    ''' Evento para establecer el menu contextual de la rejilla de detalles de factura
    ''' </summary>
    Private Sub INDInvoiceDetailGv_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDInvoiceDetailGv.PopupMenuShowing
        If e.HitInfo.RowHandle > 0 Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            If Not Me._recordFlag Then
                e.Menu.Items.Add(New DXMenuItem("Evaluación General", AddressOf ShowGeneralMovements, My.Resources.modificarLineaAzul))
                e.Menu.Items.Add(New DXMenuItem("Causante de glosa", AddressOf ShowResponsibleThirdParty, My.Resources.modificarLineaAzul))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al volver a la rejilla principal
    ''' </summary>
    Private Sub INDInvoiceDetailCtrNavigation_ClickBack() Handles INDInvoiceDetailCtrNavigation.ClickBack
        back()
    End Sub

    ''' <summary>
    ''' Evento al dar click en el boton de la rejilla
    ''' </summary>
    Private Sub INDDetailInvoiceBtn_Click(sender As Object, e As EventArgs) Handles INDDetailInvoiceBtn.Click
        ShowDetail()
    End Sub

    ''' <summary>
    ''' Evento para habilitar menu contextual de detalles de factura QX
    ''' </summary>
    Private Sub GlosaInvoiceDetailQX_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles GlosaInvoiceDetailQX.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            If Not _recordFlag Then
                e.Menu.Items.Add(New DXMenuItem("Evaluación General", AddressOf ShowGeneralMovements, My.Resources.modificarLineaAzul))
                e.Menu.Items.Add(New DXMenuItem("Causante de glosa", AddressOf ShowResponsibleThirdParty, My.Resources.modificarLineaAzul))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento al hacer click sobre la accion de ver detalle de la rejilla principal
    ''' </summary>
    Private Sub RepositoryItemButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit1.ButtonClick
        ShowDetail()
    End Sub

    ''' <summary>
    ''' Cargar el gridLookup de otros movimientos en cada fila
    ''' </summary>
    Private Sub RepositoryItemPopupContainerEdit3_QueryPopUp(sender As Object, e As CancelEventArgs) Handles RepositoryItemPopupContainerEdit3.QueryPopUp
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDInvoiceDetailGdc.FocusedView
        Dim handled = CType(Me.INDInvoiceDetailGdc.FocusedView, DevExpress.XtraGrid.Views.Base.ColumnView).FocusedRowHandle
        Dim data As GlosaMovementGlosa = CType(view.GetRow(handled), GlosaMovementGlosa)
        If data IsNot Nothing Then
            Me.INDMoreInfoGc.DataSource = data.OtherMovements
            Me.INDMoreInfoGc.RefreshDataSource()
            Me.CtrXtraInfo.ListProperties.Clear()
            If data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Valor Glosado", data.ValueGlosado.ToString("C0")))
            ElseIf data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or data.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                If data.ValueReiterated.HasValue Then
                    Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Valor Reiterado", data.ValueReiterated.Value.ToString("C0")))
                Else
                    Me.CtrXtraInfo.ListProperties.Add(New XtraInfoProperty("Valor Reiterado", ""))
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cerrando formulario
    ''' </summary>
    Private Sub FrmEvaluation_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento al cambiar el registro de glosa evaluación
    ''' </summary>
    Private Sub RepositoryCodeGlosaEvaluation_EditValueChanged(sender As Object, e As EventArgs) Handles RepositoryCodeGlosaEvaluation.EditValueChanged
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Me.INDInvoiceDetailGdc.FocusedView
        view.CloseEditor()
        Dim handler = view.FocusedRowHandle
        Dim row As GlosaMovementGlosa = CType(view.GetRow(handler), GlosaMovementGlosa)
        Dim lookup As DevExpress.XtraEditors.GridLookUpEdit = CType(sender, DevExpress.XtraEditors.GridLookUpEdit)
        Dim evaluationConcept = CType(lookup.GetSelectedDataRow(), Domain.Entities.ConceptGlosas)
        Dim value = lookup.EditValue

        If evaluationConcept Is Nothing Then
            Exit Sub
        End If

        'se establece el tipo de respuesta de glosa
        row.GlosaEvaluationType = evaluationConcept.Type

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.GlosaODevolucionInjustificada Then
            MovementsBlock.Remove(row.Id)
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.NoSubsanada Then
            MovementsBlock.Remove(row.Id)
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                Dim valueControl = 0
                If (row.MaxValueAccepted Is Nothing) Then
                    valueControl = 0
                Else
                    If row.ValueGlosado > row.MaxValueAccepted Then
                        valueControl = row.MaxValueAccepted
                    Else
                        valueControl = row.ValueGlosado
                    End If
                End If
                view.SetRowCellValue(handler, Me.V1Instance, valueControl)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                Dim valueControl = 0
                If (row.MaxValueAccepted Is Nothing) Then
                    valueControl = 0
                Else
                    If row.ValueGlosado > row.MaxValueAccepted Then
                        valueControl = row.MaxValueAccepted
                    Else
                        valueControl = row.ValueGlosado
                    End If
                End If
                view.SetRowCellValue(handler, Me.V2Instance, valueControl)
            End If
        End If

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.SubsanadaParcial Then
            MovementsBlock.Remove(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If CInt(evaluationConcept.HomologateTypeByCodeAndResponse) = ConceptsGlosaEvaluationByType.Subsanada Then
            MovementsBlock.Remove(row.Id)
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If

        If {ConceptsGlosaEvaluationByType.DevolucionInjustificada, ConceptsGlosaEvaluationByType.DevolucionJustificada}.ToList().Contains(CInt(evaluationConcept.HomologateTypeByCodeAndResponse)) Then
            MovementsBlock.Remove(row.Id)
            MovementsBlock.Add(row.Id)
            If row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionGlosa Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficio Then
                view.SetRowCellValue(handler, Me.V1Instance, 0)
            ElseIf row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEvaluacionReiteracion Or row.GlosaInvoiceDetail.GlosaObjectionsReceptionD.GlosaPortfolioGlosada.State = StatesGlosaPortfolio.PendienteEnvioDeOficioReiteracion Then
                view.SetRowCellValue(handler, Me.V2Instance, 0)
            End If
        End If
        CType(view.GetRow(handler), GlosaMovementGlosa).IdGlosaEvaluation = evaluationConcept.Id
    End Sub

    ''' <summary>
    ''' Evento al activar el editor sobre la vista de QX
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

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.IdEntity = String.Empty
    End Sub

#Region "ToolBar Events"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Guardar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConfirmarLiquidacion) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ConsultarLiquidacion) = True
        Me.BarraBotones.RibbonPageEdicion.Visible = False
        Me.BarraBotones.RibbonPagEform.Visible = False
        Me.BarraBotones.RibbonPageProcesos.Visible = False
    End Sub

    ''' <summary>
    ''' Ejecuta la opción guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción confirmar
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Me.Confirmar()
    End Sub

    ''' <summary>
    ''' Ejecuta el metodo de cargar datos en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_RefreshGrid() Handles BarraBotones.Click_RefreshGrid
        Me.LoadInvoice()
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


    Private Sub INDgdvInvoices_PopupMenuShowing(sender As Object, e As PopupMenuShowingEventArgs) Handles INDInvoicesGv.PopupMenuShowing
        If CType(sender, DevExpress.XtraGrid.Views.Grid.GridView).FocusedRowHandle <> DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If

            Dim lista = New List(Of String)
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

            view = Me.INDInvoicesGc.FocusedView
            For Each item As Integer In view.GetSelectedRows()
                If item > -1 Then
                    Dim TmpObj As DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread = TryCast(view.GetRow(item), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                    If TmpObj.OriginalRow IsNot Nothing AndAlso TmpObj.OriginalRow.id > 0 Then 'todos sin confirmar
                        lista.Add(TmpObj.OriginalRow.InvoiceNumber.ToString)
                    End If
                End If
            Next
            e.Menu.Items.Clear()
            e.Menu.Items.Add(New DXMenuItem("Evaluación General", AddressOf ShowGeneralInvoiceMovements, My.Resources.modificarLineaAzul))
        Else
            e.Menu.Items.Add(New DXMenuItem("Ver Detalle", AddressOf ShowDetail, My.Resources.modificarLineaAzul))
        End If
    End Sub

    Async Sub ShowGeneralInvoiceMovements()
        Dim lista = New List(Of String)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

        view = Me.INDInvoicesGc.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim TmpObj As DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread = TryCast(view.GetRow(item), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If TmpObj.OriginalRow IsNot Nothing AndAlso TmpObj.OriginalRow.id > 0 Then 'todos sin confirmar
                    lista.Add(TmpObj.OriginalRow.InvoiceNumber.ToString)
                End If
            End If
        Next

        If lista.Count > 0 Then
            Dim _option = 3 'general a nive de factura

            Using generalEvaluation As Generalevaluation = New Generalevaluation("EVALUACION", New List(Of GlosaMovementGlosa), _option, _myModel, CStr(MyBase.Tag), _idOperativeUnit, lista, True)
                generalEvaluation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                generalEvaluation.ManageDecimals = PopupManageDecimals()
                Dim result = generalEvaluation.ShowDialog(Me)
                If result = System.Windows.Forms.DialogResult.OK Then
                    LoadInvoice()
                    Me.AsyncLoader(False)
                End If
            End Using
        End If

    End Sub
#End Region


    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _customizableFields = Nothing
        _frontDefinicionExists = Nothing
        _pathFunctionalDefinitions = Nothing
        DefinitionsLoader = Nothing
        _myModel = Nothing
        _presenter = Nothing
        _invoiceNumber = Nothing
        MovementsBlock = Nothing
        _nameUserWithInvoice = Nothing
        _codeUserWithInvoice = Nothing
        record = Nothing
        _recordFlag = Nothing
        _objDetail = Nothing
        _idOperativeUnit = Nothing
    End Sub

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

    ''' <summary>
    ''' Evento double click sobre rejilla de oficio
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDInvoicesGv_DoubleClick(sender As Object, e As EventArgs) Handles INDInvoicesGv.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            ShowDetail()
        End If
    End Sub
    ''' <summary>
    ''' Metodo para saber si el popup va manejar decimales.
    ''' </summary>
    Private Function PopupManageDecimals() As Boolean
        If Me._parameterGlosas.ManageDecimals = False Then
            Return False
        Else
            Return True
        End If
    End Function
End Class


