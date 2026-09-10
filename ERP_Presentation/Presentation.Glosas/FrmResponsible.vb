'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Jorge Leonardo Vernaza
' Created          : 06-04-2011
'
' Last Modified By : Juan Diego Diaz 
' Last Modified On : 2013-07-02
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Librerias Importadas"

Imports Presentation.Glosas.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.Text

#End Region

''' <summary>
''' Clase que contiene todo el comportamiento de la vista
''' </summary>
Public Class FrmResponsible
    Implements IResponsible
#Region "Variable Globales Propiedades Interfaz y Load"

    ''' <summary>
    ''' propiedad que contiene la aplicacion del Responsable
    ''' </summary>
    Public Property Status As Boolean Implements IResponsible.StatusResponsible
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del Responsable
    ''' </summary>
    Public Property NameResponsible As String Implements IResponsible.NameResponsible
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el tipo
    ''' </summary>
    Public Property ERPCodeResponsible As String Implements IResponsible.ERPCodeResponsible
        Get
            Return INDtxtCodeERP.Text
        End Get
        Set(value As String)
            INDtxtCodeERP.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el codigo del Responsable
    ''' </summary>
    Public Property CodeResponsible As String Implements IResponsible.CodeResponsible
        Get
            Return INDbteCode.Text
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el tipo del Responsable
    ''' </summary>
    Public Property ChargeResponsible As String Implements IResponsible.ChargeResponsible
        Get
            Return INDtxtCharge.Text
        End Get
        Set(value As String)
            INDtxtCharge.Text = value
        End Set
    End Property


    ''' <summary>
    ''' Variable que contiene el Responsable
    ''' </summary>
    Dim Responsible As Responsible
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MResponsible
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PResponsible
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigoAux As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definición del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrMResponsible_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Model = New MResponsible(Me.Tag)
        Me.indigoAux = SessionValues.Instance
        '******************************'
        Me.ActionReport = AddressOf Me.BarraBotones.PrintReport
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PResponsible(Me)
        Me.LoadStatus()
        Me.BarraBotones.StatusRecordVisible = True
        Me.ActionsOnControls = False
        Deshacer()
        SearchMode = False
    End Sub

#End Region

#Region "CRUD Base"
    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        Try
            If ValidateControls() = False Then
                Exit Sub
            End If
            AssigningValues()
            If Responsible.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged Or Me.Responsible.State <> Me.Status Then

                Try
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.Responsible.State
                    Me.Responsible.State = state
                    Dim result = Await Me.Model.SaveResponsible(Responsible)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                        Me.Responsible = result.ObjectEmbbeded
                        AsyncLoader(False)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        CleanControls()
                        Me.DialogResult = System.Windows.Forms.DialogResult.OK
                        'Me.ShowPrintOption(PrintReportAction.None, Responsible.Id, 0, "767")
                    Else
                        AsyncLoader(False)
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                        Else
                            Dim stringMessage As New StringBuilder
                            For Each item As String In result.MessageResult
                                stringMessage.AppendLine(item)
                            Next
                            Mensaje(EeventViewerImages.Advertencia) = stringMessage.ToString()
                        End If
                    End If
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                Catch ex As Exception
                    Throw ex
                    AsyncLoader(False)
                End Try
            Else
                Mensaje(EeventViewerImages.Informacion) = "No hay cambios registrados"
            End If
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        If Responsible IsNot Nothing Then
            If Responsible.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    Responsible.MarkAsDeleted()
                    Dim result = Await Model.DeleteResponsible(Responsible)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                        Await Me.DeleteDocumentIndexed()
                        Me.CleanControls()
                    Else
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorDependencia, Comunes)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
        If FormSearchObjects IsNot Nothing Then FormSearchObjects.Close()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        If Not (FormSearchObjects IsNot Nothing AndAlso FormSearchObjects.Visible = True) Then
            FormSearchObjects = New FrmBusqueda
            AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
            With FormSearchObjects
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Responsible
                BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                .FormParent = Me
                .ShowSearch(False)
            End With
            SearchMode = True
        End If
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text <> String.Empty Then
            If Me.record IsNot Nothing AndAlso Me.Responsible IsNot Nothing AndAlso Me.Responsible.Code = ReturnValue AndAlso Me.INDbteCode.Text = ReturnValue Then
                Return
            End If
            DeleteBlockedRecord()
            LoadControls()
            INDbteCode.Enabled = False
        End If
    End Sub


    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        If SearchMode Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

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

#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then 'If Me._docIndexed Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaData, Eform.InfoMetaData), Me.Responsible.Code, Me.Responsible.Name, Me.Responsible.CodeUser, Me.Responsible.Charge), .CreationDate = dateServer, .CreationUser = Me.indigoAux.UserIndigo & "-" & Me.indigoAux.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity =  "$#" & Me.Tag & "_" & Me.Responsible.Code & "#$", .IdForm = Me.Tag, .Title = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaDataTitle, Eform.InfoMetaData), Me.Responsible.Code), .Update = dateServer, .UpdateUser = Me.indigoAux.UserIndigo & "-" & Me.indigoAux.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigoAux.UserIndigo & "-" & Me.indigoAux.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaData, Eform.InfoMetaData), Me.Responsible.Code, Me.Responsible.Name, Me.Responsible.CodeUser, Me.Responsible.Charge)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmResponsibleMetaDataTitle, Eform.InfoMetaData), Me.Responsible.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtCharge.Text = String.Empty
        INDtxtCodeERP.EditValue = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        Status = True
        Responsible = Nothing
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IResponsible.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtCodeERP.Enabled = value
            INDtxtCharge.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Sub LoadControls()
        ActionsOnControls = True
        AsyncLoader(True)
        Responsible = Await Model.GetResponsible(INDbteCode.Text)
        AsyncLoader(False)
        If Not Responsible Is Nothing Then
            If Responsible.Id > 0 Then
                Me.BarraBotones.StatusRecordVisible = True
                Dim result = Await Model.GetBlockRecord(Me.Tag, Responsible.Id)
                With Responsible
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                    CodeResponsible = .Code
                    ChargeResponsible = .Charge
                    ERPCodeResponsible = .CodeUser
                    NameResponsible = .Name
                    Status = .State
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.Responsible.Code)

                If result.Id = 0 Then
                    Me.BarraBotones.PrintReport(PrintReportAction.None, Responsible.Id, 0, "767")
                    Me.BarraBotones.SetDocuments(Responsible.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigoAux.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigoAux.UserIndigo, .IdRecord = Responsible.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    If Not (Me.record IsNot Nothing AndAlso Me.record.CodUser = Me.indigoAux.UserIndigo AndAlso Me.record.IdRecord = Me.Responsible.Id) Then
                        record = result
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                    End If
                End If
            Else
                Me.BarraBotones.StatusRecordVisible = True
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            Responsible = New Responsible With {.State = True}
        End If
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbteCode.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtName.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtCharge.Text = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtCodeERP.EditValue Is Nothing Then
            ValidateControls = False
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Responsible
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeResponsible
            .Name = NameResponsible
            .CodeUser = ERPCodeResponsible
            .Charge = ChargeResponsible
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Await Model.DeleteBlockRecord(record)
            record = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        SearchMode = False
        Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Guardar()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Responsible.Id, 0, "767")
    End Sub


#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Responsible = Nothing
        Model = Nothing
        Presenter = Nothing
        record = Nothing
        SearchMode = Nothing
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Responsible IsNot Nothing AndAlso Me.Responsible.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me.record IsNot Nothing AndAlso Me.INDbteCode.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
        End If
        Me.IdEntity =  String.Empty
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                LoadControls()
                If INDbteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDbteCode.Enabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmResponsible_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

End Class