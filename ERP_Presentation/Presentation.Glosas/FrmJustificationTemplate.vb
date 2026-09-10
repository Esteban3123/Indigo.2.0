'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan Diego Diaz 
' Created          : 18-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Presentation.Base
Imports Domain.Entities
Imports Presentation.Glosas.MVP
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources


''' <summary>
''' Formulario Plantillas de Justificación
''' </summary>
Public Class FrmJustificationTemplate
    Implements IJustificationTemplate

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene la plantilla de Justificación
    ''' </summary>
    Dim JustificationTemplate As JustificationTemplate
    ''' <summary>
    ''' Variable que contiene una lista de plantillas de Justificación 
    ''' según concepto
    ''' </summary>
    Dim ListJustificationTemplate As List(Of JustificationTemplate)
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
    ''' <summary>
    ''' Objeto concepto de glosas
    ''' </summary>
    Private concept As Domain.Entities.ConceptGlosas

#End Region

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
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
    ''' Propiedad para cargar datasource de Conceptos
    ''' </summary>
    ''' <value></value>
    Public Property DataSourceConcepts As List(Of Domain.Entities.ConceptGlosas) Implements IJustificationTemplate.DataSourceConcepts
        Get
            Return Me.INDConceptsGle.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Entities.ConceptGlosas))
            Me.INDConceptsGle.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IJustificationTemplate.ActionsOnControls
        Set(value As Boolean)
            If value = True Then
                INDConceptsGle.Focus()
            End If
        End Set
    End Property

#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        JustificationTemplate = Nothing
        ListJustificationTemplate = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        concept = Nothing
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    Private Sub FrmJustificationTemplate_Load(sender As Object, e As EventArgs) Handles Me.Load
        '******Inicializar variables*****'
        Me._doc = Nothing
        Me.session = SessionValues.Instance
        Me.Model = New MJustificationTemplate(Me.Tag)
        '********************************'

        'Me.Funct = AddressOf GenerateDoc
        Me.IndigoGridControl1.RefreshGrid(Me.INDTemplatesGc)
        Presenter = New PJustificationTemplate(Me)
        Presenter.Initializes(Me.Tag)
        JustificationTemplate = New JustificationTemplate
        Me.ActionsOnControls = False
    End Sub

    ''' <summary>
    ''' Evento click en el boton eliminar del registro de
    ''' plantillas de justificación
    ''' </summary>
    Private Sub INDDeleteBtn_Click(sender As Object, e As EventArgs) Handles INDDeleteBtn.Click
        JustificationTemplate = CType(Me.INDTemplatesGv.GetRow(Me.INDTemplatesGv.FocusedRowHandle), Domain.Entities.JustificationTemplate)
        Eliminar()
    End Sub

    ''' <summary>
    ''' Evento click en el boton editar del registro de
    ''' plantillas de justificación
    ''' </summary>
    Private Sub INDEditBtn_Click(sender As Object, e As EventArgs) Handles INDEditBtn.Click
        JustificationTemplate = CType(Me.INDTemplatesGv.GetRow(Me.INDTemplatesGv.FocusedRowHandle), Domain.Entities.JustificationTemplate)
        Editar()
    End Sub

    ''' <summary>
    ''' Evento al cambiar el valor del combo de conceptos
    ''' </summary>
    Private Sub INDConceptsGle_EditValueChanged(sender As Object, e As EventArgs) Handles INDConceptsGle.EditValueChanged
        AsyncLoader(True)
        concept = CType(Me.INDConceptsGle.GetSelectedDataRow(), Domain.Entities.ConceptGlosas)
        If concept IsNot Nothing Then
            Using Model As New MJustificationTemplate(Me.Tag)
                RefreshTemplates()
            End Using
            Me.INDAddBtn.Enabled = True
        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento click en el boton adicionar plantilla 
    ''' de justificación
    ''' </summary>
    Private Sub INDAddBtn_Click(sender As Object, e As EventArgs) Handles INDAddBtn.Click
        'Dim form As New FrmJustificationTemplateActions(Me.ListJustificationTemplate, Me.Tag, Me.concept)
        _formJustification = New FrmJustificationTemplateActions(Me.ListJustificationTemplate, Me.Tag, Me.concept)
        _formJustification.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTrans = New FrmTransparent(_formJustification, False)
        frmTrans.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTrans.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            RefreshTemplates()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.JustificationTemplate IsNot Nothing AndAlso Me.JustificationTemplate.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                'Me.INDCodeJustificationTxt.Text = Me.IdEntity.Trim()
                LoadDataForm(Me.IdEntity.Trim())
                'Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            LoadDataForm(Me.IdEntity.Trim())
            'Me.INDCodeJustificationTxt.Text = Me.IdEntity.Trim()
            'Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub

#End Region

#Region "Functions and Methods"

    Private Async Sub LoadDataForm(IdEntityAux As String)
        If Me._formJustification IsNot Nothing AndAlso Me._formJustification.Visible = True Then
            Me._formJustification.Close()
        End If
        Dim dataJustification As JustificationTemplate = Await Model.getJustificationTemplateByCode(IdEntityAux)
        Me.INDConceptsGle.EditValue = dataJustification.IdConcept
        Me.JustificationTemplate = dataJustification
        Editar()
    End Sub

    ''' <summary>
    ''' Refrescar rejilla de plantillas
    ''' </summary>
    Private Async Sub RefreshTemplates()
        Me.ListJustificationTemplate = Await Model.ListJustificationTemplateByConcept(concept.Id)
        Me.INDTemplatesGc.DataSource = Me.ListJustificationTemplate
        Me.INDTemplatesGv.RefreshData()
    End Sub

    Dim _formJustification As FrmJustificationTemplateActions

    ''' <summary>
    ''' Metodo Editar
    ''' </summary>
    Private Sub Editar()
        'Dim form As New FrmJustificationTemplateActions(Me.JustificationTemplate, Me.ListJustificationTemplate, Me.Tag, Me.concept)
        _formJustification = New FrmJustificationTemplateActions(Me.JustificationTemplate, Me.ListJustificationTemplate, Me.Tag, Me.concept)
        _formJustification.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim frmTrans = New FrmTransparent(_formJustification, False)
        frmTrans.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Dim result = frmTrans.ShowDialog()
        If result = System.Windows.Forms.DialogResult.OK Then
            RefreshTemplates()
        End If
    End Sub

#Region "CRUD"

    ''' <summary>
    ''' Metodo para Abrir Busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch

    End Sub

    ''' <summary>
    ''' Metodo para Buscar
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' Metodo para Deshacer
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    ''' <summary>
    ''' Metodo para Eliminar
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        If Me.JustificationTemplate IsNot Nothing Then
            If Me.JustificationTemplate.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MJustificationTemplate(Me.Tag)
                        AsyncLoader(True)
                        Dim result = Await Model.deleteJustificationTemplate(Me.JustificationTemplate)
                        AsyncLoader(False)
                        If result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Me.GetDocumentIndexed(Me.Tag & "_" & Me.JustificationTemplate.Code)
                            Await Me.DeleteDocumentIndexed()
                            Me.RefreshTemplates()
                            If Me.JustificationTemplate.IsDefault = True Then
                                If Me.ListJustificationTemplate.Count > 0 Then
                                    Dim JustificationDef = Me.ListJustificationTemplate.Where(Function(x) x.IsDefault = False).FirstOrDefault
                                    If JustificationDef IsNot Nothing Then
                                        JustificationDef.IsDefault = True
                                        Dim resultAux = Await Model.saveJustificationTemplate(JustificationDef)
                                    End If
                                End If
                            End If
                            Me.RefreshTemplates()
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
        Else
        End If
    End Sub

    ''' <summary>
    ''' Metodo para Guardar
    ''' </summary>
    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    ''' <summary>
    ''' Metodo para Logica Boton Actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Metodo para Nuevo
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

#End Region

#End Region

End Class