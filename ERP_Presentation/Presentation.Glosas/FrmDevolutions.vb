'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz
' Created          : 2013-06-07

' Last Modified By : Rafael Eduardo Patiño Cabrera
' Last Modified On :  
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas.MVP

#End Region

''' <summary>
''' Vista del frontal de devoluciones
''' </summary>
Public Class FrmDevolutions
    Implements IDevolution

#Region "Fields"

    ''' <summary>
    ''' Bandera que me indica si la devolución es nueva
    ''' </summary>
    Private _isNew As Boolean = True
    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String
    ''' <summary>
    ''' Encapsula el cliente consultado
    ''' </summary> 
    Private _customer As Domain.Entities.Customer
    ''' <summary>
    ''' Entidad de la devolución
    ''' </summary>
    Private _devolution As Domain.Entities.GlosaDevolutionsReceptionC
    ''' <summary>
    ''' Entidad de la devolucion detalle
    ''' </summary>
    Private _rowInvoice As Domain.Entities.GlosaDevolutionsReceptionD
    ''' <summary>
    ''' Lista de detalles de devolución
    ''' </summary>
    Private _detailDevolution As List(Of Domain.Entities.GlosaDevolutionsReceptionD)
    ''' <summary>
    ''' Entidad de la movimientos devolución
    ''' </summary>
    Private _movementDevolution As Domain.Entities.GlosaMovementDevolutions
    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _modelDevolutions As MDevolutions
    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PDevolutions
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Bandera para saber si el registro esta bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Private FlagBlockRecord As Boolean

    ''' <summary>
    ''' factura de devolucion
    ''' </summary>
    ''' <remarks></remarks>
    Private ObjDevolution As GlosaDevolutionsReceptionD
    ''' <summary>
    ''' Variable de session
    ''' </summary>
    ''' <remarks></remarks>
    Private IndigoSession As SessionValues
    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.PortfolioSequence
    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequense As Int64
    ''' <summary>
    ''' Numero de factura
    ''' </summary>
    ''' <remarks></remarks>
    Dim _invoice As String
    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource
    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer
#End Region

#Region "Properties"

    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value>Valor que indica si se activan o inactivan</value>
    Public WriteOnly Property ActionsOnControls As ActionOnControlsTypeDevolutions Implements IDevolution.ActionsOnControls
        Set(value As ActionOnControlsTypeDevolutions)
            Select Case value
                Case ActionOnControlsTypeDevolutions.NewDevolution
                    Me._isNew = True
                    Me.INDConsecutiveBte.Enabled = False
                    Me.INDbteNit.Enabled = True
                    Me.INDDocumentNumberTxt.Enabled = True
                    Me.INDDocumentNumberTxt.Properties.ReadOnly = False
                    Me.INDCommentPce.Enabled = True
                    Me.INDCommentMem.Enabled = True
                    Me.INDCommentMem.Properties.ReadOnly = False
                    Me.INDDocumentDateTxt.Enabled = True
                    Me.INDDocumentDateTxt.Properties.ReadOnly = False
                    Me.INDDocumentDateTxt.EditValue = Date.Today
                    Me.INDDevolutionsDetailGdc.Enabled = True
                    Me.INDCompanyGle.Enabled = True
                    Me.INDbteNit.Focus()
                    Me.BarraBotones.RibbonPageProcesos.Visible = False
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.BarraBotones.StatusRecord = "1"
                    Me.BarraBotones.PrepareToolbar(eAction.Save)
                Case ActionOnControlsTypeDevolutions.UnconfirmedDevolution
                    Me._isNew = False
                    Me.INDConsecutiveBte.Enabled = False
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = True
                    Me.INDDocumentNumberTxt.Properties.ReadOnly = False
                    Me.INDCommentPce.Enabled = True
                    Me.INDCommentMem.Enabled = True
                    Me.INDCommentMem.Properties.ReadOnly = False
                    Me.INDDocumentDateTxt.Enabled = True
                    Me.INDDocumentDateTxt.Properties.ReadOnly = False
                    Me.INDDevolutionsDetailGdc.Enabled = True
                    Me.INDCompanyGle.Enabled = True
                    Me.INDCompanyGle.Focus()
                    Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcessWithPrint)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                Case ActionOnControlsTypeDevolutions.ConfirmedDevolution
                    Me._isNew = False
                    Me.INDConsecutiveBte.Enabled = False
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = True
                    Me.INDDocumentNumberTxt.Properties.ReadOnly = True
                    Me.INDCommentPce.Enabled = True
                    Me.INDCommentMem.Enabled = True
                    Me.INDCommentMem.Properties.ReadOnly = True
                    Me.INDDocumentDateTxt.Enabled = True
                    Me.INDDocumentDateTxt.Properties.ReadOnly = True
                    Me.INDDevolutionsDetailGdc.Enabled = True
                    Me.INDCompanyGle.Enabled = True
                    Me.INDDocumentNumberTxt.Focus()
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndAudit)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Case Else 'Listo para consultar
                    Me._isNew = True
                    Me.INDConsecutiveBte.Enabled = True
                    Me.INDbteNit.Enabled = False
                    Me.INDDocumentNumberTxt.Enabled = False
                    Me.INDDocumentNumberTxt.Properties.ReadOnly = False
                    Me.INDCommentPce.Enabled = False
                    Me.INDCommentMem.Enabled = False
                    Me.INDCommentMem.Properties.ReadOnly = False
                    Me.INDDocumentDateTxt.Enabled = False
                    Me.INDDocumentDateTxt.Properties.ReadOnly = False
                    Me.INDDocumentDateTxt.EditValue = Date.Today
                    Me.INDDevolutionsDetailGdc.Enabled = False
                    Me.BarraBotones.StatusRecord = "1"
                    Me.INDCompanyGle.Enabled = False
                    Me.INDConsecutiveBte.Focus()
                    If Me.FormSearchObjects IsNot Nothing AndAlso Me.FormSearchObjects.Visible = True Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
                    End If
            End Select
        End Set
    End Property


    ''' <summary>
    ''' Registra un mensaje en el visor de eventos
    ''' </summary>
    ''' <param name="Icono">Tipo de icono del mensaje</param>
    ''' <value>Mensaje a registrar</value>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' Propiedad que contiene el listado de Sedes o sucursales de parametros de interface glosas
    ''' </summary>
    Public Property DataSourceBranch As List(Of Domain.Entities.GlosasParametersInterface) Implements IDevolution.DataSourceBranch
        Get
            Return INDCompanyGle.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.GlosasParametersInterface))
            INDCompanyGle.Properties.DataSource = value
            If value IsNot Nothing AndAlso value.Count = 1 Then
                INDCompanyGle.EditValue = value(0).ContainerName
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de Facturas
    ''' </summary>
    Public Property DataSourceInvoices As List(Of SP_invoiceList_Result) Implements IDevolution.DataSourceInvoices
        Get
            Return INDInvoicesGdc.DataSource
        End Get
        Set(value As List(Of SP_invoiceList_Result))
            INDInvoicesGdc.DataSource = value
            INDInvoicesGdc.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de facturas seleccionadas
    ''' </summary>
    ''' <value>Lista de facturas seleccionadas</value>
    ''' <returns>Lista de facturas seleccionadas</returns>
    Public Property SelectedInvoices As Object Implements IDevolution.SelectedInvoices
        Get
            Return Me.INDDevolutionsDetailGdc.DataSource
        End Get
        Set(value As Object)
            Me.INDDevolutionsDetailGdc.DataSource = value
            Me.INDDevolutionsDetailGdc.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el listado de conceptos especificos
    ''' </summary>
    Public Property DataSourceSpecificConcepts As List(Of Domain.Entities.ConceptGlosas) Implements IDevolution.DataSourceSpecificConcepts
        Get
            Return Me.INDMotivGle.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.ConceptGlosas))
            Me.INDMotivGle.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para el tag del formulario
    ''' </summary>
    Public ReadOnly Property TagForm As String Implements IDevolution.TagForm
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Asigna la secuencia numerica de cartera para la realizacion de documentos de reclasificacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequense As Domain.Entities.PortfolioSequence Implements IDevolution.Sequense
        Get
            Return _sequense
        End Get
        Set(value As Domain.Entities.PortfolioSequence)
            _sequense = value
            Me.DicSequense.Clear()
            For Each seq As PortfolioSequenceDetail In Me._sequense.PortfolioSequenceDetail
                Me.DicSequense.Add(seq.Id, New List(Of String)())
            Next
        End Set
    End Property

#End Region

#Region "CRUD Operations"

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        Me.AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Elimina una devolución
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar
    End Sub

    ''' <summary>
    ''' Guarda la devolución
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        Try
            If Not Me.ValidateControls(Me._isNew) Then
                Exit Sub
            End If
            If Me._detailDevolution.Count = 0 OrElse Me._detailDevolution.TrueForAll(Function(x) x.ChangeTracker.State = ObjectState.Deleted) Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
                Exit Sub
            End If
            Me.AssignValues()
            If Me._devolution.ChangeTracker.State <> ObjectState.Unchanged Or Me._detailDevolution.Exists(Function(c) c.ChangeTracker.State = ObjectState.Added) Or Me._detailDevolution.Exists(Function(c) c.ChangeTracker.State = ObjectState.Deleted) Then
                Using model As New MDevolutions(Me.Tag)
                    Me.AsyncLoader(True)
                    Dim result = Await model.SaveDevolutionC(Me._devolution, Me._detailDevolution)
                    Me.AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                        Me._devolution.RadicatedConsecutive = result.ObjectEmbbeded.RadicatedConsecutive
                        Me._devolution.Id = result.ObjectEmbbeded.Id
                        Me.INDConsecutiveBte.Text = Me._devolution.RadicatedConsecutive
                        Me.ActionsOnControls = ActionOnControlsTypeDevolutions.UnconfirmedDevolution
                        Me.INDInvoiceNumberPce.Text = String.Empty
                        Me.AsyncLoader(True)
                        Using ModelDetail As New MDevolutions(Me.Tag)
                            Me._devolution = Await model.GetDevolutionByConsecutive(Me.INDConsecutiveBte.Text.Trim())
                            Me._detailDevolution = Await ModelDetail.ListDetailDevolutionById(Me._devolution.Id)
                            Me.SelectedInvoices = Me._detailDevolution
                        End Using
                        Me.AsyncLoader(False)
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcessWithPrint)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                        Me.BarraBotones.SetDocuments(Me._devolution.Id)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Confirmar la devolución datos radicado
    ''' </summary>
    Public Async Sub ConfirmRadicated()
        If Me._rowInvoice IsNot Nothing AndAlso Me._rowInvoice.Id > 0 Then
            Dim FreeInvoice As Boolean = False
            If _movementDevolution.TypeDevolution = enumDevolutionType.Injustificate Then
                If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    If _rowInvoice.GlosasParametersInterface.DevolutionInjustificate = enumInjustificateParameters.UserDefined Then
                        If MessageIndigo.Show(ResourceManager.GetString("FrmDevolution_FreeInvoice", "Glosas"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            FreeInvoice = True
                        End If
                    End If
                ElseIf Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
                    Dim ObjGlossParameters As TimeParameters = Await Me._modelDevolutions.GetTimeParameters("0", _idOperativeUnit)
                    If ObjGlossParameters.DevolutionInjustificate = enumInjustificateParameters.UserDefined Then
                        If MessageIndigo.Show(ResourceManager.GetString("FrmDevolution_FreeInvoice", "Glosas"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            FreeInvoice = True
                        End If
                    End If
                End If
            End If
            Using model As New MDevolutions(Me.Tag)
                Me.AsyncLoader(True)
                Dim listD As New List(Of GlosaDevolutionsReceptionD)
                listD.Add(_rowInvoice)
                Dim Injustificate As Boolean
                If Me.INDTypeRg.EditValue = 1 Then
                    Injustificate = False
                ElseIf Me.INDTypeRg.EditValue = 2 Then
                    Injustificate = True
                End If
                Dim result = Await model.ConfirmMovementDevolution(listD, _idCurrentSequense, Injustificate, FreeInvoice)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    _rowInvoice.State = 2
                    If _movementDevolution.TypeDevolution = "2" Then 'injustificada
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                        loadreportOffice()
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    End If
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Dim strMessage As New System.Text.StringBuilder()
                        For Each item As String In result.MessageResult
                            strMessage.AppendLine(item.ToString())
                        Next
                        Mensaje(EeventViewerImages.Informacion) = strMessage.ToString
                    Else
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                    End If
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        If result.MessageResult(0).ToString = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        Else
                            Dim strMessage As New System.Text.StringBuilder()
                            For Each item As String In result.MessageResult
                                strMessage.AppendLine(item.ToString())
                            Next
                            Mensaje(EeventViewerImages.Advertencia) = strMessage.ToString
                        End If
                    ElseIf Not String.IsNullOrEmpty(result.Message) Then
                        Mensaje(EeventViewerImages.MensajeError) = result.Message
                    End If
                End If
            End Using

        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.RadicadoSinGuardar, Eform.Devolution)
        End If
        INDDevolutionsDetailGdc.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Confirmar la devolución datos radicado
    ''' </summary>
    Public Async Sub ConfirmDevolution()
        If Me._detailDevolution.Count = 0 OrElse Me._detailDevolution.TrueForAll(Function(x) x.ChangeTracker.State = ObjectState.Deleted) Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(OficioSinDetalles)
            Exit Sub
        End If
        If Me._detailDevolution.Count > 0 AndAlso Not Me._detailDevolution.Exists(Function(c) c.State <> "2") Then
            With Me._devolution
                .ConfirmDate = Date.Now
                .State = "2"
            End With
            If Me._devolution.ChangeTracker.State <> ObjectState.Unchanged Then
                Using model As New MDevolutions(Me.Tag)
                    Me.AsyncLoader(True)
                    Dim result = Await model.ConfirmDevolution(Me._devolution)
                    Me.AsyncLoader(False)
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesConfirmadoCorrectamente)
                        SearchMode = False
                        Deshacer()
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End If
                End Using
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(FacturaSinConfirmarEnLista, RecepcionObjeciones)
        End If
    End Sub

    ''' <summary>
    ''' Guarda la devolución datos radicado
    ''' </summary>
    Public Async Sub GuardarRadicado()

        If Me.INDMotivGle.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyiMotivo.Text)
            Exit Sub
        End If

        If Me.INDMotivGle.EditValue Is Nothing AndAlso Me.INDTypeRg.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesDigiteDatos)
            Exit Sub
        End If

        If Me._movementDevolution.Id = 0 Then
            Me._movementDevolution.ChangeTracker.State = ObjectState.Added
        Else
            Me._movementDevolution.ChangeTracker.State = ObjectState.Modified
        End If
        With Me._movementDevolution
            .IdConceptGlosa = Me.INDMotivGle.EditValue
            .InvoiceNumber = _rowInvoice.InvoiceNumber
            .Comment = Me.INDCommentRadicatedMem.Text
            .Answer = Me.INDmeAnswer.Text
            .IdDevolutionsReceptionD = _rowInvoice.Id
            .TypeDevolution = Me.INDTypeRg.EditValue
            .DevolutionValue = 0
            .State = "1"
        End With
        Dim stateOperation = ""
        If Me._movementDevolution.ChangeTracker.State = ObjectState.Added Then
            stateOperation = "Nuevo"
        ElseIf Me._movementDevolution.ChangeTracker.State = ObjectState.Modified Then
            stateOperation = "Actualizar"
        End If
        If Me._movementDevolution.ChangeTracker.State <> ObjectState.Unchanged Then
            Using model As New MDevolutions(Me.Tag)
                Me.AsyncLoader(True)
                Dim result = Await model.SaveMovementDevolution(Me._movementDevolution)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    If stateOperation = "Nuevo" Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado)
                    End If
                    If stateOperation = "Actualizar" Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                    End If
                    If Me.INDTypeRg.EditValue = "1" Then  'justificada
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcessWithPrint)
                    End If

                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                    Me._movementDevolution = result.ObjectEmbbeded
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Deja el frontal listo para una nueva busqueda o insercion de datos
    ''' </summary>
    ''' 
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        Me.CleanControls()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        Dim detailString = " [ "
        For Each item In Me._detailDevolution
            If detailString.Length = 3 Then
                detailString = detailString & String.Format(obtenerRecurso(Eresources.FrmDevolutionMetaDataDetail, Eform.InfoMetaData), item.InvoiceNumber, item.PatientCode, item.PatientName.Trim.ToLower)
            Else
                detailString = " - " & detailString & String.Format(obtenerRecurso(Eresources.FrmDevolutionMetaDataDetail, Eform.InfoMetaData), item.InvoiceNumber, item.PatientCode, item.PatientName.Trim.ToLower)
            End If
        Next
        detailString = detailString & " ]"
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmDevolutionMetaData, Eform.InfoMetaData), Me._devolution.Customer.Nit, Me._devolution.Customer.Name.Trim.ToLower, Me._devolution.DocumentNumber, detailString), .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity =  "$#" & Me.Tag & "_" & Me._devolution.RadicatedConsecutive & "#$", .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(FrmDevolutionMetaDataTitle, InfoMetaData), Me._devolution.RadicatedConsecutive), .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmDevolutionMetaData, Eform.InfoMetaData), Me._devolution.Customer.Nit, Me._devolution.Customer.Name.Trim.ToLower, Me._devolution.DocumentNumber, detailString)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmDevolutionMetaDataTitle, Eform.InfoMetaData), Me._devolution.RadicatedConsecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UnblockeRecord()
        If record IsNot Nothing Then
            Using Model As New MDevolutions(Me.Tag)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
            Me.FlagBlockRecord = False
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Prepara el formulario para una nueva devolución
    ''' </summary>
    Private Sub NewDevolution()
        Me.CleanControls()
        Me.INDConsecutiveBte.Text = "Nuevo"
        Me.ActionsOnControls = ActionOnControlsTypeDevolutions.NewDevolution
        Me._devolution = New Domain.Entities.GlosaDevolutionsReceptionC
        Me.INDHeaderLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDDetailLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDDetailInvoicesLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        If DataSourceBranch IsNot Nothing AndAlso DataSourceBranch.Count = 1 Then
            INDCompanyGle.EditValue = DataSourceBranch(0).ContainerName
        End If
    End Sub

    ''' <summary>
    ''' Permite establecer la logica para los permisos de Guardar y Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        Me.BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls(Optional ByVal isNew As Boolean = False) As Boolean
        If isNew Then
            If Me.INDbteNit.EditValue Is Nothing Then
                Me.INDbteNit.Focus()
                Return False
            End If
            If Me.INDDocumentNumberTxt.Text.Trim().Equals(String.Empty) Then
                Me.INDDocumentNumberTxt.Focus()
                Return False
            End If
            If Me.INDDocumentDateTxt.Text.Trim().Equals(String.Empty) Then
                Me.INDDocumentDateTxt.Focus()
                Return False
            End If
        Else
            If Me.INDbteNit.EditValue Is Nothing Then
                Me.INDbteNit.Focus()
                Return False
            End If
            If Me.INDDocumentNumberTxt.Text.Trim().Equals(String.Empty) Then
                Me.INDDocumentNumberTxt.Text = Me._devolution.DocumentNumber.Trim()
            End If
            If Me.INDDocumentDateTxt.Text.Trim().Equals(String.Empty) Then
                Me.INDDocumentDateTxt.EditValue = Me._devolution.DocumentDate
            End If
        End If
        Return True
    End Function

    ''' <summary>
    ''' Asigna los valores de los controles al objeto Devoluciones
    ''' </summary>
    Private Sub AssignValues()
        If INDbteNit.EditValue IsNot Nothing Then
            Me._devolution.CustomerId = INDbteNit.EditValue
        End If
        If Me._devolution.DocumentNumber Is Nothing OrElse Not Me._devolution.DocumentNumber.Trim().Equals(Me.INDDocumentNumberTxt.Text.Trim()) Then
            Me._devolution.DocumentNumber = Me.INDDocumentNumberTxt.Text.Trim()
        End If
        Me._devolution.RadicatedDate = Date.Parse(Me.INDRadicateDateLbl.Text)
        If Object.Equals(Me._devolution.DocumentDate, Nothing) OrElse Not Me._devolution.DocumentDate.Equals(Date.Parse(Me.INDDocumentDateTxt.EditValue)) Then
            Me._devolution.DocumentDate = Date.Parse(Me.INDDocumentDateTxt.EditValue)
        End If
        If Me._devolution.Comment Is Nothing OrElse Not Me._devolution.Comment.Trim().Equals(Me.INDCommentMem.Text.Trim()) Then
            Me._devolution.Comment = Me.INDCommentMem.Text.Trim()
        End If
        If Me._devolution.State Is Nothing OrElse Me._devolution.State.Trim().Equals(String.Empty) Then
            Me._devolution.State = "1"
        End If
        Me._devolution.ReceivesDevolution = Me.INDtxtReceivesDevolution.Text
        Me._devolution.ReceivesDevolutionPosition = Me.INDtxtReceivesDevolutionPosition.Text
        Me._devolution.PersonSends = Me.INDtxtPersonSends.Text
        Me._devolution.PersonSendsPosition = Me.INDtxtPersonSendsPosition.Text
    End Sub

    ''' <summary>
    ''' Abre el frontal del busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        Select Case Me._openFindSenser.ToUpper()
            Case Me.INDConsecutiveBte.Name.ToUpper() 'Abre el frontal para buscar consecutivos
                With FormSearchObjects
                    .ListaColumnas = {New ColumnInfo With {.Caption = "Factura", .FieldName = "InvoiceNumber"}, New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName"}, New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate"}, New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "RadicatedConsecutive"}, New ColumnInfo With {.Caption = "Estado", .FieldName = "State"}}.ToList()
                    .ValorSolicitado = "RadicatedConsecutive"
                    .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.DevolucionC
                    BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    .FormParent = Me
                    .ShowSearch(False)
                End With
            Case Else 'Si esta sobre un control en donde no aplica el metodo buscar
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoAplicaBuscar, Eform.Comunes)
        End Select
        SearchMode = True
    End Sub


    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Select Case Me._openFindSenser.ToUpper()
            Case Me.INDConsecutiveBte.Name.ToUpper()
                If Not String.IsNullOrEmpty(ReturnValue) Then
                    Me.INDConsecutiveBte.Text = ReturnValue
                    Me.UnblockeRecord()
                    LoadControls()
                    Me.INDConsecutiveBte.Enabled = False
                    Me.INDCompanyGle.EditValue = Nothing
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Cargar los controles con los datos del objeto devolución
    ''' </summary>
    Private Async Sub LoadControls()
        Me.AsyncLoader(True)
        Using Model As New MDevolutions(Me.Tag)
            Me._devolution = Await Model.GetDevolutionByConsecutive(Me.INDConsecutiveBte.Text.Trim())
            If Not Me._devolution Is Nothing AndAlso Me._devolution.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, Me._devolution.Id)
                Me.BarraBotones.PrintReport(PrintReportAction.None, _devolution.Id, 0, {_devolution.Id, Me.BarraBotones.OperatingUnit})
                With Me._devolution
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    Me.INDbteNit.EditValue = .CustomerId
                    Me.INDbteNit.DisplayNullText = .Customer.Nit & " - " & .Customer.Name
                    Me.INDRadicateDateLbl.Text = .RadicatedDate.ToString("D", indigo.Culture)
                    Me.INDDocumentNumberTxt.Text = .DocumentNumber
                    Me.INDDocumentDateTxt.EditValue = .DocumentDate
                    Me.INDCommentMem.Text = .Comment
                    Me.INDtxtReceivesDevolution.Text = .ReceivesDevolution
                    Me.INDtxtReceivesDevolutionPosition.Text = .ReceivesDevolutionPosition
                    Me.INDtxtPersonSends.Text = .PersonSends
                    Me.INDtxtPersonSendsPosition.Text = .PersonSendsPosition
                    Me._customer = .Customer
                    Me._detailDevolution = Await Model.ListDetailDevolutionById(.Id)
                    Me.SelectedInvoices = Me._detailDevolution
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me._devolution.RadicatedConsecutive)
                    Me.BarraBotones.StatusRecordVisible = True
                    Me.AsyncLoader(False)
                    If .State.Equals("1") Then
                        Me.ActionsOnControls = ActionOnControlsTypeDevolutions.UnconfirmedDevolution
                        Me.BarraBotones.StatusRecord = "1"
                    ElseIf .State.Equals("2") Then
                        Me.ActionsOnControls = ActionOnControlsTypeDevolutions.ConfirmedDevolution
                        Me.BarraBotones.StatusRecord = "2"
                        Me.BarraBotones.DisableBarDocument()
                        Me.INDDeleteActionBtn.Enabled = False
                    ElseIf .State.Equals("4") Then
                        Me.ActionsOnControls = ActionOnControlsTypeDevolutions.invalidateDevolution
                        Me.BarraBotones.StatusRecord = "4"
                        Me.BarraBotones.DisableBarDocument()
                        Me.INDDeleteActionBtn.Enabled = False
                    End If
                End With
                If result.Id = 0 Then
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._devolution.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                    FlagBlockRecord = False
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    Me.FlagBlockRecord = True
                End If
                If Me.BarraBotones.StatusRecord <> "2" Then
                    Me.BarraBotones.SetDocuments(Me._devolution.Id)
                End If
                Me.GetDocumentIndexed(Me.Tag & "_" & Me._devolution.RadicatedConsecutive)
            Else
                Me.AsyncLoader(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.DevolucionNoExiste, Eform.Devolution)
                Me.INDConsecutiveBte.Focus()
                Me.INDConsecutiveBte.SelectAll()
            End If
        End Using
        If DataSourceBranch IsNot Nothing AndAlso DataSourceBranch.Count = 1 Then
            INDCompanyGle.EditValue = DataSourceBranch(0).ContainerName
        End If
    End Sub

    ''' <summary>
    ''' Metodo para borrar registros de detalle de Devolución
    ''' </summary>
    Private Async Sub DeleteInvoice()
        If Me.FlagBlockRecord = True Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
            Exit Sub
        End If
        If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If Me.INDDevolutionsInvoiceGdv.SelectedRowsCount > 1 Then

                Dim lista = New List(Of GlosaDevolutionsReceptionD)
                Dim listaEliminar = New List(Of GlosaDevolutionsReceptionD)

                For Each item As Integer In INDDevolutionsInvoiceGdv.GetSelectedRows()
                    If item > -1 Then
                        Dim data As GlosaDevolutionsReceptionD = TryCast(INDDevolutionsInvoiceGdv.GetRow(item), Domain.Entities.GlosaDevolutionsReceptionD)
                        If data IsNot Nothing Then
                            lista.Add(data)
                            listaEliminar.Add(data)
                        End If
                    End If
                Next

                For Each Data As GlosaDevolutionsReceptionD In lista 'limpiamos los iteam que aun no han sidio guardados
                    If Data.Id = 0 Then
                        If listaEliminar.Contains(Data) Then
                            Me._devolution.GlosaDevolutionsReceptionD.Remove(Data)
                            listaEliminar.Remove(Data)
                            _detailDevolution.Remove(Data)
                        End If
                    End If
                Next

                Dim result As ActionResult
                AsyncLoader(True)
                result = Await _modelDevolutions.DeleteListDevolution(listaEliminar)
                AsyncLoader(False)
                If result.StateResult = True Then
                    For Each item As GlosaDevolutionsReceptionD In listaEliminar
                        Me._detailDevolution.Remove(item)
                    Next
                    SelectedInvoices = _detailDevolution
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                Else
                    If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                        Dim strmessage As New StringBuilder
                        For Each item In result.MessageResult
                            strmessage.AppendLine(item.ToString)
                        Next
                        Mensaje(EeventViewerImages.Advertencia) = strmessage.ToString
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesContacteAdministrador, Eform.Comunes)
                    End If
                End If


            Else
                Dim aux As Domain.Entities.GlosaDevolutionsReceptionD = CType(Me.INDDevolutionsInvoiceGdv.GetRow(Me.INDDevolutionsInvoiceGdv.FocusedRowHandle), Domain.Entities.GlosaDevolutionsReceptionD)
                If aux IsNot Nothing Then
                    If aux.State <> 2 Then
                        If aux.ChangeTracker.State = ObjectState.Added Then
                            Me._detailDevolution.Remove(aux)
                            Me.SelectedInvoices = Me._detailDevolution.Where(Function(d) d.ChangeTracker.State <> ObjectState.Deleted).ToList
                            If DataSourceInvoices IsNot Nothing Then
                                Dim Obj As List(Of SP_invoiceList_Result) = Me.DataSourceInvoices.Where(Function(c) c.InvoiceNumber = aux.InvoiceNumber).ToList()
                                If Obj IsNot Nothing AndAlso Obj.Count > 0 Then
                                    Obj.ForEach(Function(x As SP_invoiceList_Result) x.Selection = False)
                                End If
                            End If
                        Else 'Eliminado
                            Me.SelectedInvoices = Me._detailDevolution.Where(Function(d) d.ChangeTracker.State <> ObjectState.Deleted).ToList
                            AsyncLoader(True)
                            Dim result = Await _modelDevolutions.deleteDevolutionD(aux)
                            AsyncLoader(False)
                            If result.StateResult = True Then
                                Me._detailDevolution.Remove(aux)
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoPuedeEliminarPorMovimiento, Eform.RecepcionObjeciones)
                            End If
                            If DataSourceInvoices IsNot Nothing Then
                                Dim Obj As List(Of SP_invoiceList_Result) = Me.DataSourceInvoices.Where(Function(c) c.InvoiceNumber = aux.InvoiceNumber).ToList()
                                If Obj IsNot Nothing AndAlso Obj.Count > 0 Then
                                    Obj.ForEach(Function(x As SP_invoiceList_Result) x.Selection = False)
                                End If
                            End If
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "factura Confirmada, no se puede eliminar"
                    End If
                Else
                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesSeleccioneRegistroEliminar)
                End If
            End If
            SelectedInvoices = _detailDevolution
        End If 'mensaje
    End Sub

    ''' <summary>
    ''' Pega en la rejilla de facturas los datos de la clipboard
    ''' </summary>
    Private Async Sub PasteToGridInvoices()
        If Me.INDbteNit IsNot Nothing AndAlso Me.INDbteNit.EditValue IsNot Nothing Then 'OrElse Objeto.State = 1 Then
            Dim list As List(Of String) = Me.GetInvoiceFromClipboard()
            If list.Count > 0 Then
                Dim _InterfaceId? As Integer
                Dim _result As New ActionResult(Of List(Of GlosaDevolutionsReceptionD))
                Me.AsyncLoader(True)
                If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                    If INDCompanyGle.EditValue Is Nothing Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                        Exit Sub
                    End If
                    Dim Company As Domain.Entities.GlosasParametersInterface = CType(INDCompanyGle.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
                    If Company IsNot Nothing Then
                        _InterfaceId = Company.Id
                        _result = Await _modelDevolutions.ValidateListInvoiceDevolutionSp(list, Me.INDbteNit.EditValue, Company.ContainerName)
                    Else
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                    End If
                Else
                    _InterfaceId = Nothing
                    _result = Await _modelDevolutions.ValidateListInvoiceDevolutionSp(list, Me.INDbteNit.EditValue, String.Empty)
                End If
                Me.AsyncLoader(False)
                Dim strMensaje As String = String.Empty
                If _result.MessageResult IsNot Nothing AndAlso _result.MessageResult.Count > 0 Then
                    For Each itemMensaje As String In _result.MessageResult
                        strMensaje += itemMensaje + Environment.NewLine
                    Next
                    Mensaje(EeventViewerImages.Advertencia) = strMensaje
                End If
                Dim listInvoice = _result.ObjectEmbbeded
                If listInvoice IsNot Nothing AndAlso listInvoice.Count > 0 Then
                    For Each item In listInvoice
                        item.GlosasParametersInterfaceId = _InterfaceId
                        Me._detailDevolution.Add(item)
                    Next
                    Me.INDDevolutionsDetailGdc.DataSource = Me._detailDevolution
                End If
                Me.INDDevolutionsDetailGdc.RefreshDataSource()
            Else
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Valida una cadena de caracteres descartando que tenga caracteres especiales
    ''' </summary>
    ''' <param name="str">Cadena a validar</param>
    ''' <returns>Valor que indica si la cadena es valida</returns>
    Private Function IsValidString(ByVal str As String) As Boolean
        Dim pattern As String = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZ0123456789-_ "
        For Each c As Char In str
            If Not pattern.Contains(c) Then
                Return False
            End If
        Next
        Return True
    End Function

    ''' <summary>
    ''' Obtiene una lista de numeros de facturas
    ''' almacenada en la Clipboard
    ''' </summary>
    ''' <returns>Lista de numeros de facturas</returns>
    Private Function GetInvoiceFromClipboard() As List(Of String)
        Dim list As New List(Of String)()
        If indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDCompanyGle.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If ObjCompany Is Nothing Then
                Return list
            End If
            If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length >= 1 Then
                        Dim invoiceNumber As String = item(0).Trim().ToUpper
                        If Me.IsValidString(invoiceNumber) Then
                            If Me.INDDevolutionsDetailGdc.DataSource Is Nothing Then
                                Me.INDDevolutionsDetailGdc.DataSource = New List(Of GlosaDevolutionsReceptionD)
                            End If
                            If Not invoiceNumber.Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = invoiceNumber Select i).Any AndAlso Not (From f As Domain.Entities.GlosaDevolutionsReceptionD In CType(Me.INDDevolutionsDetailGdc.DataSource, List(Of GlosaDevolutionsReceptionD)) Where f.InvoiceNumber.Contains(invoiceNumber) Select f).Any Then
                                Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(invoiceNumber, ObjCompany.AccountingMethod)
                                list.Add(invoiceNumberFixed)
                                Me.INDDevolutionsDetailGdc.DataSource = New List(Of GlosaDevolutionsReceptionD)
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If
        Else
            If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
                For Each line As String In Clipboard.GetText.Split(vbNewLine)
                    Dim item() As String = line.Trim.Split(vbTab)
                    If item.Length >= 1 Then
                        Dim invoiceNumber As String = item(0).Trim().ToUpper
                        If Me.IsValidString(invoiceNumber) Then
                            If Me.INDDevolutionsDetailGdc.DataSource Is Nothing Then
                                Me.INDDevolutionsDetailGdc.DataSource = New List(Of GlosaDevolutionsReceptionD)
                            End If
                            If Not invoiceNumber.Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = invoiceNumber Select i).Any AndAlso Not (From f As Domain.Entities.GlosaDevolutionsReceptionD In CType(Me.INDDevolutionsDetailGdc.DataSource, List(Of GlosaDevolutionsReceptionD)) Where f.InvoiceNumber.Contains(invoiceNumber) Select f).Any Then
                                list.Add(invoiceNumber)
                                Me.INDDevolutionsDetailGdc.DataSource = New List(Of GlosaDevolutionsReceptionD)
                            End If
                        End If
                    Else
                        Exit For
                    End If
                Next
            End If
        End If
        Return list
    End Function


    ''' <summary>
    ''' Deshace los cambios realizados
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        Me.CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        Me.UnblockeRecord()
        If Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Me.INDHeaderLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDDetailLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDDetailInvoicesLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        Me.INDConsecutiveBte.Text = String.Empty
        Me._detailDevolution = New List(Of GlosaDevolutionsReceptionD)
        Me._devolution = New GlosaDevolutionsReceptionC
        Me.INDCompanyGle.EditValue = Nothing
        Me.INDbteNit.EditValue = Nothing
        Me.INDbteNit.DisplayNullText = String.Empty
        Dim d As DateTime = GetServerDate()
        Me.INDRadicateDateLbl.Tag = d
        Me.INDRadicateDateLbl.Text = d.ToString("D", indigo.Culture)
        Me.INDDocumentDateTxt.EditValue = String.Empty
        Me.INDDocumentNumberTxt.Text = String.Empty
        Me.INDInvoiceNumberPce.Text = String.Empty
        Me.INDCommentMem.Text = String.Empty
        Me.INDtxtReceivesDevolution.Text = String.Empty
        Me.INDtxtReceivesDevolutionPosition.Text = String.Empty
        Me.INDtxtPersonSends.Text = String.Empty
        Me.INDtxtPersonSendsPosition.Text = String.Empty
        Me.INDDevolutionsDetailGdc.DataSource = Nothing
        Me.DataSourceInvoices = Nothing
        Me.INDMotivGle.EditValue = Nothing
        Me.INDCausaTxt.Text = String.Empty
        Me.INDTypeRg.EditValue = Nothing
        Me._doc = Nothing
        Me.INDCommentRadicatedMem.Text = String.Empty
        Me.INDmeAnswer.Text = String.Empty
        Me.BarraBotones.RibbonPageProcesos.Visible = False
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False
        INDDeleteActionBtn.Enabled = True
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.ActionsOnControls = ActionOnControlsTypeDevolutions.WaitingQuery
    End Sub


    ''' <summary>
    ''' Mostrar formulario con datos del radicado
    ''' </summary>
    Private Sub ShowDataRadicated()
        _rowInvoice = Me.INDDevolutionsInvoiceGdv.GetRow(Me.INDDevolutionsInvoiceGdv.FocusedRowHandle)
        If _rowInvoice.Id > 0 Then
            Me.INDHeaderLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDDetailLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDDetailInvoicesLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Using model As New MDevolutions(Me.Tag)
                Me.AsyncLoader(True)
                _invoice = _rowInvoice.InvoiceNumber
                _movementDevolution = model.GetMovementDevolution(_rowInvoice.InvoiceNumber, _rowInvoice.Id)
                Me.AsyncLoader(False)
                If _movementDevolution.Id > 0 Then
                    Me.INDTypeRg.EditValue = Convert.ToInt32(_movementDevolution.TypeDevolution)
                    Me.INDMotivGle.EditValue = _movementDevolution.IdConceptGlosa
                    Me.INDCommentRadicatedMem.Text = _movementDevolution.Comment
                    Me.INDmeAnswer.Text = _movementDevolution.Answer
                    Dim objSpecificConcept = CType(Me.INDMotivGle.GetSelectedDataRow(), Domain.Entities.ConceptGlosas)
                    Me.INDCausaTxt.Text = objSpecificConcept.NameGeneral
                End If
            End Using
            Me.INDLbInvoiceNumber.Text = _rowInvoice.InvoiceNumber
            Me.INDRadicatedNumberLbl.Text = _rowInvoice.RadicatedNumber
            Me.INDRadicatedDateLbl.Text = _rowInvoice.RadicatedDate
            Me.INDAnalystLbl.Text = _rowInvoice.UserNameInvoice
            If _rowInvoice.State = "2" Then 'confirmado
                If _movementDevolution.TypeDevolution = "2" Then 'injustificada
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                    loadreportOffice()
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                Me.BarraBotones.StatusRecord = "2"
            ElseIf _rowInvoice.State = "1" Then
                If _movementDevolution.Id > 0 Then
                    If _movementDevolution.TypeDevolution = "2" Then 'injustificada
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcessWithPrint)
                        loadreportOffice()
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                    End If
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
                Me.BarraBotones.StatusRecord = "1"
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.GuardarDevolucion, Eform.Devolution)
        End If

    End Sub


    Private Sub loadreportOffice()
        'Reporte oficio a nivel de factura
        Me.BarraBotones.PrintReport(PrintReportAction.None, _devolution.Id, 1411, {_devolution.Id, _invoice, BarraBotones.OperatingUnit.UnitName})
    End Sub
    ''' <summary>
    ''' Metodo para volver al formulario principal
    ''' </summary>
    Async Sub backMain()
        Me._invoice = String.Empty
        Me.BarraBotones.PrintReport(PrintReportAction.None, _devolution.Id, 0, {_devolution.Id, Me.BarraBotones.OperatingUnit})

        Me.INDHeaderLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDDetailLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDDetailInvoicesLycg.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcessWithPrint)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        If _devolution.State = "2" Then
            Me.BarraBotones.RibbonPageProcesos.Visible = False
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
            Me.BarraBotones.StatusRecord = "2"
        Else
            Me.BarraBotones.StatusRecord = "1"
        End If
        ' If Me._movementDevolution.State = "2" Then
        Me.AsyncLoader(True)
        Using ModelDetail As New MDevolutions(Me.Tag)
            Me._detailDevolution = Await ModelDetail.ListDetailDevolutionById(Me._devolution.Id)
            Me.SelectedInvoices = Me._detailDevolution
        End Using
        Me.AsyncLoader(False)
        ' End If
        Me.INDMotivGle.EditValue = Nothing
        Me.INDCausaTxt.Text = String.Empty
        Me.INDTypeRg.EditValue = Nothing
        Me.INDCommentRadicatedMem.Text = String.Empty
        Me.INDmeAnswer.Text = String.Empty
    End Sub

    ''' <summary>
    ''' Retorna el mensaje de estado de una factura
    ''' </summary>
    ''' <param name="ObservationInvoiceCode"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StateErp(ObservationInvoiceCode) As String
        Dim StrMensaje As String = String.Empty
        Select Case ObservationInvoiceCode
            Case "1"
                StrMensaje = "Factura Sin Radicar"
            Case "T"
                StrMensaje = "Factura Radicada Sin Confirmar"
            Case "6"
                StrMensaje = "Factura Anulada"
        End Select
        Return StrMensaje
    End Function

    ''' <summary>
    ''' Metodo para invalidar filtros
    ''' </summary>
    Private Sub InvalidFilter()
        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.SeleccionesCamposParaFiltros) '"Por favor seleccione Campos para poder agregar criterio filtro"
    End Sub

    ''' <summary>
    ''' Metodo para cargar los campos del advanced Filter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadAdvancedFilter(Optional ByVal ObjCompany As GlosasParametersInterface = Nothing)
        Dim ListObjectField As New List(Of ObjectField)
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then


            If ObjCompany.AccountingMethod = eTypeInterface.FoxPublic Or ObjCompany.AccountingMethod = eTypeInterface.FoxPrivate Then
                ListObjectField.Add(New ObjectField("N° Factura", "car.cemnumfac", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Codigo Contrato", "sal.GECCODIGO", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Fecha Radicado", "crc.ccrfecrad", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

                ListObjectField.Add(New ObjectField("Identificación", "com.gpacodigo", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Nombre Paciente", "com.gpanombre", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Apellido Paciente", "com.gpaapelli", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))


            ElseIf ObjCompany.AccountingMethod = eTypeInterface.NETPrivate Or ObjCompany.AccountingMethod = eTypeInterface.NETPublic Then


                ListObjectField.Add(New ObjectField("N° Factura", "car.cxcdocume", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Codigo Contrato", "CON.GDECODIGO", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Fecha Radicado", "crm.CRFFECRAD", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

                ListObjectField.Add(New ObjectField("Identificación", "pac.pacnumdoc", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Nombre Paciente", "pac.pacprinom", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

                ListObjectField.Add(New ObjectField("Apellido Paciente", "pac.pacpriape", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
                ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))
            End If
        ElseIf Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.Indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then

            ListObjectField.Add(New ObjectField("N° Factura", "car.invoicenumber", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Codigo Contrato", "contra.code", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Fecha Factura", "sal.InvoiceDate", eTypes.E_Date, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Between, "Entre"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterThan, "Mayor que"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.GreaterOrEquals, "Mayor o igual"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessThan, "Menor que"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.LessOrEquals, "Menor o igual"))

            ListObjectField.Add(New ObjectField("Identificación", "sal.PatientCode", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Nombre Paciente", "pac.IPPRINOMB", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))

            ListObjectField.Add(New ObjectField("Apellido Paciente", "pac.IPPRIAPEL", eTypes.E_String, New List(Of CriteriaAdvancedFilter)))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Equals, "Igual a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.NotEquals, "Diferente a"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.StartWith, "Inicia con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.EndWith, "Finaliza con"))
            ListObjectField(ListObjectField.Count - 1).ListCriteria.Add(New CriteriaAdvancedFilter(eCriteriaAdvancedFilter.Contains, "Contiene"))
        End If

        CtrAdvancedFilter.ListTopResults.Clear()
        CtrAdvancedFilter.ListTopResults.Add(50)
        CtrAdvancedFilter.ListTopResults.Add(100)
        CtrAdvancedFilter.ListTopResults.Add(150)
        CtrAdvancedFilter.ListTopResults.Add(200)
        Me.CtrAdvancedFilter.ListFields = ListObjectField

        RemoveHandler CtrAdvancedFilter.RunSearch, AddressOf RunSearch


        AddHandler CtrAdvancedFilter.RunSearch, AddressOf RunSearch
        AddHandler CtrAdvancedFilter.InvalidValues, AddressOf InvalidFilter

        INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarFiltros)
    End Sub

    ''' <summary>
    ''' Ejecutar busqueda
    ''' </summary>
    Private Async Sub RunSearch(sender As Object, e As RunSearchEventArgs)
        Dim _actionResult As New ActionResult(Of List(Of SP_invoiceList_Result))
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim Company As Domain.Entities.GlosasParametersInterface = CType(INDCompanyGle.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If Company IsNot Nothing Then
                AsyncLoader(True)
                _actionResult = Await _modelDevolutions.GetInvoicesAll(Company.ContainerName, INDbteNit.EditValue, String.Empty, e.QueryString, e.TopResult, "2")
                AsyncLoader(False)
            End If
        Else
            AsyncLoader(True)
            _actionResult = Await _modelDevolutions.GetInvoicesAll(String.Empty, INDbteNit.EditValue, String.Empty, e.QueryString, e.TopResult, "2")
            AsyncLoader(False)
        End If
        If Me.INDbteNit.EditValue IsNot Nothing Then
            If _actionResult.StateResult = True Then
                Me.DataSourceInvoices = _actionResult.ObjectEmbbeded
                INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarListaFacturas)
                Me.INDInvoicesGdc.Refresh()
            End If
        End If
    End Sub


    ''' <summary>
    ''' Funcion que agrega un item a la rejilla de detalle de recepcion
    ''' </summary>
    ''' <param name="itemInvoice"></param>
    Private Sub ItemAddGrid(ByVal itemInvoice As SP_invoiceList_Result)
        Dim interfaceId? As Integer
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDCompanyGle.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
            End If
            'validamos que la cuenta este configurada en los parametros de interfaz
            Dim ObjInterfaceParameter As GlosasParametersInterface = CType(INDCompanyGle.GetSelectedDataRow, GlosasParametersInterface)
            interfaceId = ObjInterfaceParameter.Id
            'validacion de facturas a traslado a cobro juridico. aplica solo para el version DGH NET sectro publico 
            If ObjInterfaceParameter.AccountingMethod = eTypeInterface.NETPublic Then
                If itemInvoice.TraslateJuridical IsNot Nothing AndAlso itemInvoice.TraslateJuridical > 0 Then
                    Mensaje(EeventViewerImages.Informacion) = "La factura " & itemInvoice.InvoiceNumber & " ya esta en proceso juridico"
                    Exit Sub
                End If
            End If
        Else
            interfaceId = Nothing
        End If
        'Declaro variable  tipo detalle factura
        Dim _TmpDevolutionReceptionD As GlosaDevolutionsReceptionD = New GlosaDevolutionsReceptionD
        With _TmpDevolutionReceptionD
            .GlosasParametersInterfaceId = interfaceId
            .InvoiceNumber = itemInvoice.InvoiceNumber
            .State = itemInvoice.State
            .InvoiceDate = itemInvoice.InvoiceDate
            .Ingress = itemInvoice.IngressNumber
            .ContractCode = itemInvoice.ContractCode
            .ContractName = itemInvoice.ContractName
            .PatientCode = itemInvoice.PatientCode
            .PatientName = itemInvoice.PatientName
            .RadicatedDate = itemInvoice.RadicatedDate
            .RadicatedNumber = itemInvoice.RadicatedNumber
            .UserNameInvoice = itemInvoice.UserNameInvoice
            .Comment = itemInvoice.Comment
            .BalanceInvoice = itemInvoice.BalanceInvoice
            .PlanCode = itemInvoice.CodePlan
        End With
        'Valido que la factura agregar no se encuentre ya en la lista
        Dim found As GlosaDevolutionsReceptionD = Nothing
        found = _detailDevolution.Find(Function(value As GlosaDevolutionsReceptionD) value.InvoiceNumber = itemInvoice.InvoiceNumber)
        If found Is Nothing Then
            _detailDevolution.Add(_TmpDevolutionReceptionD)
        End If
        SelectedInvoices = _detailDevolution
        INDDevolutionsDetailGdc.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCustomers()
        Using msearch As New MBusqueda
            DatasourceCustomers = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.Customers)
            INDbteNit.Datasource = DatasourceCustomers
        End Using
    End Sub

    Private Async Function INDbteNit_KeyDown(ByVal nit As String) As Task(Of Domain.Entities.Customer)
        _customer = Await _modelDevolutions.GetCustomerByNit(nit)
        If _customer IsNot Nothing AndAlso _customer.Id > 0 Then
            INDDocumentDateTxt.Focus()
        Else
            Me.INDbteNit.DisplayNullText = String.Empty
            Me.INDbteNit.EditValue = Nothing
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ClienteNoExite, Eform.Conciliation)
            Me.INDbteNit.Focus()
        End If
        Return _customer
    End Function

#End Region

#Region "Handlers"

#Region "FormClosing"
    ''' <summary>
    ''' Aqui se desbloquea el registro
    ''' </summary>
    Private Sub FrmDevolutions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.UnblockeRecord()
    End Sub
#End Region

#Region "Constructor"
    ''' <summary>
    ''' InitializeComponent
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDConsecutiveBte"
    End Sub
#End Region

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _isNew = Nothing
        _openFindSenser = Nothing
        _customer = Nothing
        _devolution = Nothing
        _rowInvoice = Nothing
        _detailDevolution = Nothing
        _movementDevolution = Nothing
        _modelDevolutions = Nothing
        _presenter = Nothing
        record = Nothing
        FlagBlockRecord = Nothing
        ObjDevolution = Nothing
        _sequense = Nothing
        _idCurrentSequense = Nothing
        _invoice = Nothing
        DatasourceCustomers = Nothing
        SearchMode = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    ''' Inicializa la vista del frontal de devolución
    ''' </summary>
    Private Async Sub FrmDevolutions_Load(sender As Object, e As EventArgs) Handles Me.Load
        '******Inicializar variables******'
        Me.indigo = SessionValues.Instance
        Me._isNew = True
        Me._modelDevolutions = New MDevolutions(Me.Tag)
        Me._doc = Nothing
        '*********************************'
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = AddressOf Me.INDbteNit_KeyDown
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me._funct = AddressOf GenerateDoc
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecordEnabled = False
        Me._presenter = New PDevolutions(Me)
        Me.INDDocumentDateTxt.Properties.MaxValue = Date.Today
        Me._presenter.Initializes()
        Me.INDRadicatedCtrNavigation.Group = Me.INDRadicatedLycg
        Me.IndigoGridControl1.SetHoldSize(Me.INDDevolutionsDetailGdc, True)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Permisos) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Native Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegractionFox Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNet Or Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.IntegrationNATIVE_MIGRATIONS Then
            INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Await Me._presenter.GetSequense()
            _idCurrentSequense = Me._sequense.PortfolioSequenceDetail(0).Id
        Else
            INDlyiContainer.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            _idCurrentSequense = 0
        End If
        Me.Deshacer()
        SearchMode = False
    End Sub
#End Region

#Region "EditValueChanged"

    Private Async Sub INDbteNit_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDbteNit.EditValueChanged
        If Me.INDbteNit.EditValue IsNot Nothing Then
            Me._customer = Await _modelDevolutions.GetCustomerById(Me.INDbteNit.EditValue)
        End If
    End Sub
    ''' <summary>
    ''' Aplicar Filtro Cada Ves Que se Escriba en el Txt de Buscar Factura
    ''' </summary>
    Private Sub INDInvoiceNumberPce_EditValueChanged(sender As Object, e As EventArgs) Handles INDInvoiceNumberPce.EditValueChanged
        INDInvoicesGdv.ApplyFindFilter(INDInvoiceNumberPce.Text)
    End Sub


    ''' <summary>
    ''' Evento cuando cambia el gridLookUpEdit de sucursal
    ''' </summary>
    Private Sub INDCompanyGle_EditValueChanged(sender As Object, e As EventArgs) Handles INDCompanyGle.EditValueChanged
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            Dim ObjCompany As Domain.Entities.GlosasParametersInterface = CType(INDCompanyGle.GetSelectedDataRow, Domain.Entities.GlosasParametersInterface)
            If ObjCompany IsNot Nothing Then
                LoadAdvancedFilter(ObjCompany)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento cuando cambia el valor del combo de motivos
    ''' </summary>
    Private Sub INDMotivGle_EditValueChanged_1(sender As Object, e As EventArgs) Handles INDMotivGle.EditValueChanged
        Dim objSpecificConcept = CType(Me.INDMotivGle.GetSelectedDataRow(), Domain.Entities.ConceptGlosas)
        If objSpecificConcept IsNot Nothing Then
            Me.INDCausaTxt.Text = objSpecificConcept.NameGeneral
        End If
    End Sub
#End Region

#Region "QueryPopup"
    ''' <summary>
    ''' Establecer Foco en la Busqueda De Facturas ERP
    ''' </summary>
    Private Sub INDInvoiceNumberPce_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDInvoiceNumberPce.QueryPopUp
        If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
            If INDCompanyGle.EditValue IsNot Nothing Then
                INDInvoiceNumberPce.Focus()
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUnaEmpresa, RecepcionObjeciones)
                e.Cancel = True
            End If
        Else
            LoadAdvancedFilter()
            INDInvoiceNumberPce.Focus()
        End If
    End Sub
    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleAccountStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbteNit_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDbteNit.QueryPopUp
        If INDbteNit.Datasource Is Nothing Then
            LoadXpoCustomers()
        End If
    End Sub
#End Region

#Region "GotFocus"
    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDConsecutiveBte.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub
#End Region

#Region "KeyDown"
    ''' <summary>
    ''' Abre el popUp del comentario al precionar enter o la tecla de espacio
    ''' </summary>
    Private Sub INDCommentPce_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDCommentPce.KeyDown
        If e.KeyCode.Equals(System.Windows.Forms.Keys.Enter) OrElse e.KeyCode.Equals(System.Windows.Forms.Keys.Space) Then
            Me.INDCommentPce.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Permite la busqueda de una devolución por su numero de consecutivo
    ''' o la creación de una nueva si no se proporciona uno.
    ''' </summary>
    Private Sub INDConsecutiveBte_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDConsecutiveBte.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(Me.INDConsecutiveBte.Text.Trim()) AndAlso Not String.IsNullOrWhiteSpace(Me.INDConsecutiveBte.Text.Trim()) AndAlso Not Me.INDConsecutiveBte.Text.Trim().Equals("") AndAlso Convert.ToInt64(Me.INDConsecutiveBte.Text.Trim()) > 0 Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoTienePermisos, Eform.Comunes)
                    Exit Sub
                End If
                Me.LoadControls() 'Consultamos el consecutivo
                Me.INDConsecutiveBte.Enabled = False
            Else
                Me.NewDevolution()
            End If
        End If
    End Sub


    ''' <summary>
    ''' Consulta un cliente por su nit
    ''' </summary>
    Private Async Sub INDNitBte_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteNit.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(Me.INDbteNit.Text.Trim()) AndAlso Not String.IsNullOrWhiteSpace(Me.INDbteNit.Text.Trim()) AndAlso Not Me.INDbteNit.Text.Trim().Equals("") AndAlso Convert.ToInt64(Me.INDbteNit.Text.Trim()) > 0 Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoTienePermisos, Eform.Comunes)
                    Exit Sub
                End If
                If Not String.IsNullOrEmpty(INDbteNit.Text.ToString) Then
                    Using model As New MDevolutions(Me.Tag)
                        Dim ctomer = Await model.GetCustomerByNit(Me.INDbteNit.Text.Trim())
                        If ctomer IsNot Nothing AndAlso ctomer.Name IsNot Nothing AndAlso Not ctomer.Name.Trim().Equals(String.Empty) Then
                            Me._customer = ctomer
                            If Me._customer Is Nothing Then
                                Me.INDbteNit.Focus()
                            End If
                            Me.INDDocumentNumberTxt.Focus()
                        Else
                            'Mensaje, El cliente no existe
                            Me.INDbteNit.Text = String.Empty
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesContacteAdministrador)
                        End If
                    End Using
                Else
                    Me.INDDocumentNumberTxt.Focus()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento al presionar una tecla sobre la caja de texto de Facturas
    ''' </summary>
    Private Async Sub INDInvoiceNumberTxt_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDInvoiceNumberPce.KeyDown
        If e.KeyCode.Equals(System.Windows.Forms.Keys.Enter) Then
            If Me.INDbteNit.EditValue Is Nothing Then
                Mensaje(EeventViewerImages.Advertencia) = "Seleccione un nit"
                Exit Sub
            End If
            Dim Invoice As SP_invoiceList_Result = Nothing
            Using Model As New MDevolutions(Me.Tag)
                If INDInvoiceNumberPce.Text <> String.Empty Then
                    If Me.indigo.IndigoGlossesIntegration = EGlossesIntegration.Integration Then
                        Dim objCompany = CType(Me.INDCompanyGle.GetSelectedDataRow(), Domain.Entities.GlosasParametersInterface)
                        Dim invoiceNumberFixed As String = Utils.FixInvoiceNumber(INDInvoiceNumberPce.Text.Trim(), objCompany.AccountingMethod)
                        AsyncLoader(True)
                        Invoice = Await Model.GetInvoice(objCompany.ContainerName.ToString().Trim, Me.INDbteNit.EditValue, invoiceNumberFixed, "", 2)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(True)
                        Invoice = Await Model.GetInvoice(String.Empty, Me.INDbteNit.EditValue, INDInvoiceNumberPce.Text.Trim(), "", 2)
                        AsyncLoader(False)
                    End If
                    If Invoice IsNot Nothing AndAlso Invoice.InvoiceNumber IsNot Nothing Then
                        ItemAddGrid(Invoice)
                        If DataSourceInvoices IsNot Nothing AndAlso DataSourceInvoices.Count > 0 Then
                            Dim Obj As List(Of SP_invoiceList_Result) = Me.DataSourceInvoices.Where(Function(c) c.InvoiceNumber = Invoice.InvoiceNumber).ToList()
                            If Obj IsNot Nothing AndAlso Obj.Count > 0 Then
                                Obj.ForEach(Function(x As SP_invoiceList_Result) x.Selection = True)
                            End If
                            SelectedInvoices = Me._detailDevolution.Where(Function(d) d.ChangeTracker.State <> ObjectState.Deleted)
                            Me.INDInvoicesGdc.RefreshDataSource()
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = "Factura No Existe o se encuentra en otro proceso"
                    End If
                End If
            End Using
        End If
    End Sub


    ''' <summary>
    ''' Cuando se oprima control + v se dispara el metodo de pegar facturas
    ''' </summary>
    Private Sub INDDevolutionsInvoiceGdv_KeyDown(sender As Object, e As KeyEventArgs) Handles INDDevolutionsInvoiceGdv.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Me.PasteToGridInvoices()
        End If
    End Sub
#End Region

#Region "Popup"

    ''' <summary>
    ''' Posiciona el foco en el primer campo del popUp
    ''' </summary>
    Private Sub INDCommentPce_Popup(sender As Object, e As EventArgs) Handles INDCommentPce.Popup
        Me.INDtxtReceivesDevolution.Focus()
    End Sub
#End Region

#Region "Click"
    ''' <summary>
    ''' Evento al agregar facturas
    ''' </summary>
    Private Sub INDAddInvoiceBtn_Click(sender As Object, e As EventArgs) Handles INDAddInvoiceBtn.Click
        If (Me.DataSourceInvoices IsNot Nothing) Then
            Dim itemInvoices As List(Of SP_invoiceList_Result) = Me.DataSourceInvoices.FindAll(Function(c) c.Selection = True)
            For i As Integer = 0 To itemInvoices.Count - 1
                Dim itemInvoice As SP_invoiceList_Result = itemInvoices(i)
                'Verifico que la factura este seleccionada
                If (itemInvoice.Selection = True) Then
                    Dim record = Me._detailDevolution.Where(Function(d) d.ChangeTracker.State = ObjectState.Deleted And d.InvoiceNumber = itemInvoice.InvoiceNumber).SingleOrDefault
                    If record IsNot Nothing Then
                        Me._detailDevolution.Remove(record)
                        record.ChangeTracker.State = ObjectState.Unchanged
                        Me._detailDevolution.Add(record)
                    Else
                        ItemAddGrid(itemInvoice)
                    End If
                End If
            Next
            SelectedInvoices = Me._detailDevolution.Where(Function(d) d.ChangeTracker.State <> ObjectState.Deleted).ToList()
        End If
    End Sub
    ''' <summary>
    ''' Evento Click sobre el boton eliminar de la rejilla de detalles de Devolución
    ''' </summary>
    Private Sub INDDeleteActionBtn_Click(sender As Object, e As EventArgs) Handles INDDeleteActionBtn.Click
        DeleteInvoice()
    End Sub
    ''' <summary>
    ''' Boton que permite ver datos de radicado
    ''' </summary>
    Private Sub INDRadicatedDetailActionBtn_Click(sender As Object, e As EventArgs) Handles INDRadicatedDetailActionBtn.Click
        ShowDataRadicated()
    End Sub
    ''' <summary>
    ''' Evento click en boton busqueda avanzada
    ''' </summary>
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles INDBtndisplayAdvancedSearch.Click
        If INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarFiltros)
        Else
            INDLytRejillaFacturas.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLytAddInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyiSelectAll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLytFiltrosAvanzada.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDBtndisplayAdvancedSearch.Text = obtenerRecurso(Eresources.OcultarListaFacturas)
        End If
    End Sub
#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Abre el frontal de busqueda para filtrar las devoluciones
    ''' </summary>
    Private Sub INDConsecutiveBte_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDConsecutiveBte.Properties.ButtonClick
        Me.AbrirBusqueda()
    End Sub
#End Region

#Region "ShowingEditor"
    ''' <summary>
    ''' Evento cuando se activa el editor de registros en la rejilla de facturas
    ''' </summary>
    Private Sub INDInvoicesGdv_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDInvoicesGdv.ShowingEditor
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(sender, DevExpress.XtraGrid.Views.Grid.GridView)
        Dim row As Object = view.GetRow(view.FocusedRowHandle)
        If (Me._detailDevolution.Exists(Function(c) c.InvoiceNumber = CType(row, SP_invoiceList_Result).InvoiceNumber And c.ChangeTracker.State <> ObjectState.Deleted)) Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "PopupMenuShowing"
    ''' <summary>
    ''' Evento al mostrar el menu contexto de la rejilla de detalles de Devolución
    ''' </summary>
    Private Sub INDDevolutionsInvoiceGdv_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDDevolutionsInvoiceGdv.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()
        Dim lista = New List(Of GlosaDevolutionsReceptionD)
        For Each item As Integer In INDDevolutionsInvoiceGdv.GetSelectedRows()
            If item > -1 Then
                Dim data As GlosaDevolutionsReceptionD = TryCast(INDDevolutionsInvoiceGdv.GetRow(item), Domain.Entities.GlosaDevolutionsReceptionD)
                If data IsNot Nothing And data.State <> 2 Then
                    lista.Add(data)
                End If
            End If
        Next
        If lista.Count > 1 Then
            e.Menu.Items.Add(New DXMenuItem("Eliminar Masivo", AddressOf DeleteInvoice, My.Resources.eliminarLineaAzul))
        Else
            e.Menu.Items.Add(New DXMenuItem("Ver Radicado", AddressOf ShowDataRadicated, My.Resources.modificarLineaAzul))
            If Me._devolution.State <> "2" Then
                e.Menu.Items.Add(New DXMenuItem("Eliminar Factura", AddressOf DeleteInvoice, My.Resources.eliminarLineaAzul))
                e.Menu.Items.Add(New DXMenuItem("Pegar Facturas", AddressOf PasteToGridInvoices, My.Resources.pegar32))
            End If
        End If
    End Sub
#End Region

#Region "DoubleClick"
    ''' <summary>
    ''' Abre el PopUpContainer de los detalles de factura
    ''' </summary>
    Private Sub INDDevolutionsInvoiceGdv_DoubleClick(sender As Object, e As EventArgs) Handles INDDevolutionsInvoiceGdv.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            If info.Column IsNot Nothing Then
                ShowDataRadicated()
            End If
        End If
    End Sub
#End Region

#Region "ClickBack"

    ''' <summary>
    ''' Evento para volver al formulario principal
    ''' </summary>
    Private Sub INDRadicatedCtrNavigation_ClickBack() Handles INDRadicatedCtrNavigation.ClickBack
        backMain()
    End Sub
#End Region

#Region "IdEntityLoaded"
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._devolution IsNot Nothing AndAlso Me._devolution.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDConsecutiveBte.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDConsecutiveBte.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDConsecutiveBte.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub
#End Region

#Region "Activated"
    Private Sub FrmDevolutions_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDConsecutiveBte.Focus()
    End Sub
#End Region

#Region "OpenFormButtonClick"
    Private Sub INDbteNit_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDbteNit.OpenFormButtonClick
        OpenForm(503, Nothing, True)
        LoadXpoCustomers()
    End Sub
#End Region

#End Region

#Region "ToolBar Events "

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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

    ''' <summary>
    ''' Ejecuta la opción de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.SearchMode = False
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        If Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Me.GuardarRadicado()
        Else
            Me.Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la opción confirmar
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Me.ConfirmRadicated()
        Else
            Me.ConfirmDevolution()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la opción nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.NewDevolution()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de personalizacion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        If Me.INDRadicatedMainCtrNavigation.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Me.GuardarRadicado()
        Else
            Me.Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la opción de reestablecer la definicion del frontal
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        Me.ResetLayout()
    End Sub

    Private Sub INDchkSelectAll_CheckedChanged(sender As Object, e As EventArgs) Handles INDchkSelectAll.CheckedChanged
        If DataSourceInvoices IsNot Nothing AndAlso DataSourceInvoices.Count > 0 Then
            If INDchkSelectAll.EditValue = True Then
                For Each item As SP_invoiceList_Result In DataSourceInvoices
                    item.Selection = True
                Next
            Else
                For Each item As SP_invoiceList_Result In DataSourceInvoices
                    item.Selection = False
                Next
            End If
            INDInvoicesGdc.RefreshDataSource()
        End If
    End Sub

#End Region

#Region "Enumeraciones"
    ''' <summary>
    ''' Enumeraciones de Parametros Devoluciones para el tipo INJUSTIFICADA
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumInjustificateParameters
        FreeInvoice = 1
        MaintainInvoice = 2
        UserDefined = 3
    End Enum

    ''' <summary>
    ''' tipo de devolucion
    ''' </summary>
    ''' <remarks></remarks>
    Enum enumDevolutionType
        Justificate = 1
        Injustificate = 2
    End Enum
#End Region

End Class

