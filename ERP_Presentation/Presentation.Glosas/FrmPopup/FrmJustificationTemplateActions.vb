'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz 
' Created          : 18-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Presentation.Glosas.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources


''' <summary>
''' Formulario para las acciones de plantillas
''' de justificación
''' </summary>
Public Class FrmJustificationTemplateActions
    Implements IJustificationTemplate

#Region "Fields"

    ''' <summary>
    ''' Objeto Plantilla de Justificación
    ''' </summary>
    ''' <remarks></remarks>
    Private _justificationTemplate As JustificationTemplate
    ''' <summary>
    ''' Objeto Plantilla de Justificación Auxiliar
    ''' </summary>
    ''' <remarks></remarks>
    Private _justificationTemplateAux As JustificationTemplate
    ''' <summary>
    ''' Lista de Plantillas de Justificación
    ''' </summary>
    ''' <remarks></remarks>
    Private _listJustificationTemplate As List(Of JustificationTemplate)
    ''' <summary>
    ''' Tag del formulario principal
    ''' </summary>
    ''' <remarks></remarks>
    Private _tagForm As String
    ''' <summary>
    ''' Concepto seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Private _concept As Domain.Entities.ConceptGlosas
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MJustificationTemplate
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PJustificationTemplate
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Variable de sesión
    ''' </summary>
    Private session As SessionValues

#End Region

#Region "Constructors"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="justificationTemplate">Objeto Plantilla de Justificación</param>
    ''' <param name="listJustificationTemplate">Lista de Plantillas de Justificación</param>
    ''' <param name="tagForm">Tag del Formulario</param>
    ''' <param name="concept">Concepto</param>
    Sub New(justificationTemplate As JustificationTemplate, listJustificationTemplate As List(Of JustificationTemplate), tagForm As String, concept As Domain.Entities.ConceptGlosas)
        ' This call is required by the designer.
        InitializeComponent()
        Me._justificationTemplate = justificationTemplate
        Me._listJustificationTemplate = listJustificationTemplate
        Me._tagForm = tagForm
        Me._concept = concept
        Me._justificationTemplateAux = Nothing
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="listJustificationTemplate">Lista de Plantillas de Justificación</param>
    ''' <param name="tagForm">Tag del Formulario</param>
    ''' <param name="concept">Concepto</param>
    Sub New(listJustificationTemplate As List(Of JustificationTemplate), tagForm As String, concept As Domain.Entities.ConceptGlosas)
        ' This call is required by the designer.
        InitializeComponent()
        Me._justificationTemplate = New JustificationTemplate
        Me._listJustificationTemplate = listJustificationTemplate
        Me._tagForm = tagForm
        Me._concept = concept
        Me._justificationTemplateAux = Nothing
    End Sub

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad Mensaje
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
        Set(ByVal value As String)
            
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
    ''' Propiedad ActionsOnControls
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IJustificationTemplate.ActionsOnControls
        Set(value As Boolean)
    
        End Set
    End Property

    ''' <summary>
    ''' Propiedad DataSourceConcepts
    ''' </summary>
    Public Property DataSourceConcepts As List(Of Domain.Entities.ConceptGlosas) Implements IJustificationTemplate.DataSourceConcepts

#End Region

#Region "BarraBotones Handlers"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(Me._tagForm))
        If Me._justificationTemplate IsNot Nothing AndAlso Me._justificationTemplate.Id > 0 Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        Me.INDConceptTxt.Text = Me._concept.ConceptCodeName
        If Me._justificationTemplate IsNot Nothing AndAlso Me._justificationTemplate.Id > 0 Then
            LoadControls()
            If Not Me._listJustificationTemplate.Count > 0 Then
                Me.INDDefaultRdg.BeginInvoke(New Action(Of Boolean)(AddressOf SetValueRdg), {True})
            End If
        End If
        Me.INDCodeTxt.Focus()
    End Sub

    ''' <summary>
    ''' Evento click para Guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento click para Actualizar
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento click para Eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Evento click para Deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

#End Region

#Region "Functions and Methods"

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        If Me._justificationTemplate IsNot Nothing AndAlso Me._justificationTemplate.Id > 0 Then
            Me.UnblockeRecord()
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Else
            INDNameTxt.Text = Nothing
            Me.INDDefaultRdg.EditValue = Nothing
            Me.INDJustificationRtx.HtmlText = String.Empty
            Me._justificationTemplate = Nothing
            Me._justificationTemplateAux = Nothing
            Me._doc = Nothing
            Me.UnblockeRecord()
            Me.BarraBotones.DisableBarDocument()
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub UnblockeRecord()
        If record IsNot Nothing AndAlso record.Id > 0 Then
            Using Model As New MJustificationTemplate(Me._tagForm)
                Await Model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Dim objectGlosas = Me._concept
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmJustificationTemplateMetaData, Eform.InfoMetaData), Me._justificationTemplate.Code, objectGlosas.NameGeneral, Me._justificationTemplate.Name), .CreationDate = dateServer, .CreationUser = Me.session.UserIndigo & "-" & Me.session.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity =  "$#" & Me._tagForm & "_" & Me._justificationTemplate.Code & "#$", .IdForm = Me._tagForm, .Title = String.Format(obtenerRecurso(Eresources.FrmJustificationTemplateMetaDataTitle, Eform.InfoMetaData), Me._justificationTemplate.Code), .Update = dateServer, .UpdateUser = Me.session.UserIndigo & "-" & Me.session.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.session.UserIndigo & "-" & Me.session.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmJustificationTemplateMetaData, Eform.InfoMetaData), Me._justificationTemplate.Code, Me._justificationTemplate.ConceptGlosas.NameGeneral, Me._justificationTemplate.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmJustificationTemplateMetaDataTitle, Eform.InfoMetaData), Me._justificationTemplate.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDCodeTxt.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDNameTxt.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDDefaultRdg.EditValue Is Nothing Then
            ValidateControls = False
        End If
        If INDJustificationRtx.HtmlText = String.Empty Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Me._justificationTemplate
            .Code = Me.INDCodeTxt.Text
            .IdConcept = Me._concept.Id
            .Justification = Me.INDJustificationRtx.HtmlText
            .Name = Me.INDNameTxt.Text
            .IsDefault = Me.INDDefaultRdg.EditValue
        End With

        If Not Me._listJustificationTemplate.Exists(Function(x) x.IsDefault = True) Then
            Me._justificationTemplate.IsDefault = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo para posicionar el valor del 
    ''' radio group "Es por defecto"
    ''' </summary>
    Private Sub SetValueRdg(valRdg As Boolean)
        Me.INDDefaultRdg.EditValue = valRdg
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        Using Model As New MJustificationTemplate(Me._tagForm)
            If Not Me._justificationTemplate Is Nothing Then
                If Me._justificationTemplate.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(Me._tagForm, Me._justificationTemplate.Id)
                    With _justificationTemplate
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        Me.INDConceptTxt.Text = Me._concept.ConceptCodeName
                        Me.INDCodeTxt.Text = .Code
                        Me.INDNameTxt.Text = .Name
                        Me.INDDefaultRdg.EditValue = .IsDefault
                        Me.INDJustificationRtx.HtmlText = .Justification
                    End With

                    Me.GetDocumentIndexed(Me._tagForm & "_" & Me._justificationTemplate.Code)

                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(Me._justificationTemplate.Id, Me._tagForm)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me._tagForm, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = Me._justificationTemplate.Id}
                        Dim operation = Await Model.SaveBlockRecord(record)
                        record = operation.ObjectEmbbeded
                    Else
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                        Me.INDDefaultRdg.Enabled = False
                    End If
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                End If
            Else
                Me._justificationTemplate = New JustificationTemplate
            End If
        End Using
    End Sub

#Region "CRUD"

    ''' <summary>
    ''' Metodo guardar plantilla de justificación.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            AssigningValues()
            Dim state = Me._justificationTemplate.ChangeTracker.State
            If Me._justificationTemplate.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged Then
                Using Model As New MJustificationTemplate(Me._tagForm)
                    AsyncLoader(True)
                    Dim result = Await Model.saveJustificationTemplate(Me._justificationTemplate)
                    AsyncLoader(False)
                    If result.StateResult = True Then

                        Me._justificationTemplate = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        If state = Domain.Base.Entities.ObjectState.Added Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                        ElseIf state = Domain.Base.Entities.ObjectState.Modified Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        End If
                        If Me._justificationTemplateAux IsNot Nothing Then
                            If Me._justificationTemplate.IsDefault = True Then
                                Me._justificationTemplateAux.IsDefault = False
                                Dim resultAux = Await Model.saveJustificationTemplate(Me._justificationTemplateAux)
                            End If
                        End If
                        Deshacer()
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                    Else
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        ElseIf result.MessageResult(0) = "-888" Then
                            Mensaje(EeventViewerImages.Advertencia) = "Ya existe un registro con el código diligenciado"
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo eliminar plantilla de justificación.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        Try
        If Me._justificationTemplate IsNot Nothing Then
            If Me._justificationTemplate.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MJustificationTemplate(Me._tagForm)
                        AsyncLoader(True)
                        Dim result = Await Model.deleteJustificationTemplate(Me._justificationTemplate)
                        AsyncLoader(False)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Await Me.DeleteDocumentIndexed()
                            If Me._justificationTemplate.IsDefault = True Then
                                If Me._listJustificationTemplate.Count > 0 Then
                                    Dim JustificationDef = Me._listJustificationTemplate.Where(Function(x) x.IsDefault = False).FirstOrDefault
                                    If JustificationDef IsNot Nothing Then
                                        JustificationDef.IsDefault = True
                                        Dim resultAux = Await Model.saveJustificationTemplate(JustificationDef)
                                    End If
                                End If
                            End If
                            Me.UnblockeRecord()
                            Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        Else
                            If result.MessageResult(0) = "-999" Then
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                            ElseIf result.MessageResult(0) = "-000" Then
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorDependencia, Comunes)
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                        End If
                    End Using
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
            End If
        End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo nuevo para plantilla de justificación.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' Metodo logica boton actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Metodo abrir busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Metodo buscar
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo deshacer
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        Me.CleanControls()
    End Sub

#End Region

#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _justificationTemplate = Nothing
        _justificationTemplateAux = Nothing
        _listJustificationTemplate = Nothing
        _tagForm = Nothing
        _concept = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub FrmJustificationTemplateActions_Load(sender As Object, e As EventArgs) Handles Me.Load
        '******Inicializar variables*****'
        Me._doc = Nothing
        Me.session = SessionValues.Instance
        Me.Model = New MJustificationTemplate(Me._tagForm)
        '********************************'
        Me.Funct = AddressOf GenerateDoc
    End Sub

    ''' <summary>
    ''' Aqui se desbloquea el registro
    ''' </summary>
    Private Sub FrmJustificationTemplate_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.UnblockeRecord()
    End Sub

    ''' <summary>
    ''' Evento al cambiar el valor del radio grupo 
    ''' "Es por defecto""
    ''' </summary>
    Private Sub INDDefaultRdg_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDDefaultRdg.EditValueChanging
        ' If record IsNot Nothing AndAlso record.Id > 0 Then
        If e.NewValue = True And e.OldValue IsNot Nothing Then
            Me._justificationTemplateAux = Me._listJustificationTemplate.Where(Function(x) x.IsDefault = True).FirstOrDefault
            If Me._listJustificationTemplate IsNot Nothing AndAlso Me._listJustificationTemplate.Count > 0 AndAlso Me._justificationTemplateAux IsNot Nothing Then
                If Not Me._justificationTemplateAux.Id = Me._justificationTemplate.Id Then
                    If MessageIndigo.Show(obtenerRecurso(PlantillaExistente, Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Else
                        Me.INDDefaultRdg.BeginInvoke(New Action(Of Boolean)(AddressOf SetValueRdg), {False})
                    End If
                End If
            End If
        Else
            Me._justificationTemplateAux = Nothing
            If Not Me._listJustificationTemplate.Exists(Function(x) x.IsDefault = True) Then
                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(PlantillaPorDefecto, Comunes)
                Me.INDDefaultRdg.BeginInvoke(New Action(Of Boolean)(AddressOf SetValueRdg), {True})
            End If
        End If
        'End If
    End Sub

#End Region

End Class