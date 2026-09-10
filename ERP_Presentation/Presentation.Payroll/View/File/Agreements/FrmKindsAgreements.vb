'***********************************************************************
' Assembly         : Presentacion.Payrol
' Author           : Rafael Eduardo Patiño
' Created          : 07-01-2014
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias Importadas"

Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Data.Linq
Imports DevExpress.Xpo
Imports Presentation.Controls.MVP

#End Region

Public Class FrmKindsAgreements
    Implements IKindsAgreements



#Region "Properties"
    ''' <summary>
    ''' Variable para poder acceder al modelo
    ''' </summary>
    ''' <remarks></remarks>
    Dim Model As New MKindsAgreements(Me.Tag)
    ''' <summary>
    ''' DataTable
    ''' </summary>
    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Variable que contiene una clase convenio
    ''' </summary>
    Dim KindsAgreements As KindsAgreements
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PKindsAgreements
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    ''' <summary>
    ''' Variable que verifica si esta parametrizado el concepto cxc
    ''' </summary>
    Dim PortfolioParameter As Boolean

#End Region

#Region "Variable Globales Propiedades Interfaz y Load"

    ''' <summary>
    ''' Variable para controlar el modo en que se abre el form
    ''' </summary>
    ''' <remarks></remarks>
    Dim ModoBusqueda As Boolean = False

    ''' <summary>
    ''' Obtiene  o asigna el codigo del convenio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CodeKindsAgreements As String Implements IKindsAgreements.CodeKindsAgreements
        Get
            Return Me.INDbtnCode.Text
        End Get
        Set(value As String)
            Me.INDbtnCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' obtiene o Asigna el Nombre de la clase de convenios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property NameKindsAgreements As String Implements IKindsAgreements.NameKindsAgreements
        Get
            Return Me.INDtxtName.Text
        End Get
        Set(value As String)
            Me.INDtxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Porpiedad que determina el estado de la clases de convenio
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StatusKindsAgreements As Boolean Implements IKindsAgreements.StatusKindsAgreements
        Get
            Return Me.BarraBotones.StatusRecord
        End Get
        Set(value As Boolean)
            Me.BarraBotones.StatusRecord = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource para las cuentas bancarias de entidades
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property ExpenseConceptDatasource As XPInstantFeedbackSource Implements IKindsAgreements.ExpenseConceptDatasource
        Get
            Return CType(INDSlConceptEgress.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlConceptEgress.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece Concepto de cuenta por cobrar
    ''' </summary>
    ''' <value>
    ''' The entity account datasource.
    ''' </value>
    Public Property AccountReceivableConceptDatasource As XPInstantFeedbackSource Implements IKindsAgreements.AccountReceivableConceptDatasource
        Get
            Return CType(INDSlConceptReceivable.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSlConceptReceivable.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que determina el estado que afecta cuenta por cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AffectsAccountsReceivable As Boolean Implements IKindsAgreements.AffectsAccountsReceivable
        Get
            Return INDGleAffectsAccountsReceivable.EditValue
        End Get
        Set(value As Boolean)
            INDGleAffectsAccountsReceivable.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que determina el estado el reclasifica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ReclassifyAccountsReceivable As Boolean Implements IKindsAgreements.ReclassifyAccountsReceivable
        Get
            Return INDGleReclassifyAccountsReceivable.EditValue
        End Get
        Set(value As Boolean)
            INDGleReclassifyAccountsReceivable.EditValue = value
        End Set
    End Property


#Region "Datasource"
    Private _FillingAffectsAccountsReceivable As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingAffectsAccountsReceivable As List(Of Tuple(Of Integer, String))
        Get
            If _FillingAffectsAccountsReceivable Is Nothing Then
                _FillingAffectsAccountsReceivable = New List(Of Tuple(Of Integer, String))
                _FillingAffectsAccountsReceivable.Add(New Tuple(Of Integer, String)(1, "Si"))
                _FillingAffectsAccountsReceivable.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _FillingAffectsAccountsReceivable
        End Get
    End Property

    Private _FillingReclassifyAccountsReceivable As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property FillingReclassifyAccountsReceivable As List(Of Tuple(Of Integer, String))
        Get
            If _FillingReclassifyAccountsReceivable Is Nothing Then
                _FillingReclassifyAccountsReceivable = New List(Of Tuple(Of Integer, String))
                _FillingReclassifyAccountsReceivable.Add(New Tuple(Of Integer, String)(1, "Si"))
                _FillingReclassifyAccountsReceivable.Add(New Tuple(Of Integer, String)(2, "No"))
            End If
            Return _FillingReclassifyAccountsReceivable
        End Get
    End Property
#End Region


    ''' <summary>
    ''' Funcion de carga inicial del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmKindsAgreements_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me.Model = New MKindsAgreements(Me.Tag)
        Me.indigo = SessionValues.Instance
        Me.Funct = AddressOf GenerateDoc
        Presenter = New PKindsAgreements(Me)
        '******************************'

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloGlosas.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If

        'Cargar GridLookUpEdit
        Me.INDGleAffectsAccountsReceivable.Properties.DataSource = FillingAffectsAccountsReceivable
        'Dar un valor por defecto a los GridLookEdit
        Me.INDGleAffectsAccountsReceivable.EditValue = 2

        Me.INDGleReclassifyAccountsReceivable.Properties.DataSource = FillingReclassifyAccountsReceivable
        Me.INDGleReclassifyAccountsReceivable.EditValue = 2

        Presenter.InitializeExpenseConcept()
        Presenter.InitializeAccountsReceivable()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' Metodo para abrir el formulario de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Codigo", .FieldName = "Code"}, New ColumnInfo With {.Caption = "Descripción", .FieldName = "Description"}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListKindsAgreements
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
        ModoBusqueda = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbtnCode.Text = ReturnValue
        If INDbtnCode.Text <> String.Empty Then
            LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.CleanControls()
        If Not ModoBusqueda Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        End If
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If KindsAgreements IsNot Nothing Then
            If KindsAgreements.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    AsyncLoader(True)
                    KindsAgreements.MarkAsDeleted()
                    Dim result As ActionMessageResult(Of KindsAgreements)
                    result = Await Model.DeleteKindsAgreements(KindsAgreements)

                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        ModoBusqueda = False
                        Deshacer()
                    Else
                        If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                        End If
                        AsyncLoader(False)
                    End If
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
            End If
        Else
        End If
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        AssigningValues()
        If KindsAgreements.ChangeTracker.State <> Domain.Base.Entities.ObjectState.Unchanged Then
            AsyncLoader(True)
            Dim result = Await Model.SaveKindsAgreements(KindsAgreements)
            AsyncLoader(False)
            If result.StateResult = True Then
                If KindsAgreements.ChangeTracker.State = ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf KindsAgreements.ChangeTracker.State = ObjectState.Modified Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                Else
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesContacteAdministrador)
                End If

                Me.KindsAgreements = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                CleanControls()
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
                ModoBusqueda = False
                Deshacer()
            Else
                If result.MessageResult.Count > 0 Then
                    If result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorConcurrencia, Comunes)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
                End If
            End If
        End If
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

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

    Public Sub Nuevo() Implements ICrudBase.Nuevo
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IKindsAgreements.ActionsOnControls
        Set(value As Boolean)
            INDbtnCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDSlConceptEgress.Enabled = value
            INDGleAffectsAccountsReceivable.Enabled = value
            INDGleReclassifyAccountsReceivable.Enabled = value
            INDSlConceptReceivable.Enabled = value
            INDSlAccounts.Enabled = value
            If value = True Then
                INDtxtName.Focus()
            Else
                INDbtnCode.Focus()
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
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmKindsAgrementsMetaData, Eform.InfoMetaData), Me.KindsAgreements.Code, Me.KindsAgreements.Description),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.KindsAgreements.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmKindsAgrementsMetaDataTitle, Eform.InfoMetaData), Me.KindsAgreements.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmKindsAgrementsMetaData, Eform.InfoMetaData), Me.KindsAgreements.Code, Me.KindsAgreements.Description)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmKindsAgrementsMetaDataTitle, Eform.InfoMetaData), Me.KindsAgreements.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        AsyncLoader(True)
        Using Model As New MKindsAgreements(Me.Tag)
            KindsAgreements = Await Model.GetKindsAgreements(Me.INDbtnCode.Text.Trim)
        End Using

        If Not KindsAgreements Is Nothing Then
            If KindsAgreements.Id > 0 Then
                Dim result = Await Model.GetBlockRecord(Me.Tag, KindsAgreements.Id)
                With KindsAgreements
                    'LogicaBotonActualizar(True)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), KindsAgreements.CreationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), KindsAgreements.CreationDate)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), KindsAgreements.ModificationUser)
                    Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), KindsAgreements.ModificationDate)
                    INDbtnCode.EditValue = .Code
                    INDtxtName.EditValue = .Description
                    INDSlConceptEgress.EditValue = .IdExpenseConcepts
                    Me.BarraBotones.StatusRecord = .State
                    INDGleAffectsAccountsReceivable.EditValue = If(.AffectsAccountsReceivable, 1, 2)
                    INDGleReclassifyAccountsReceivable.EditValue = If(.ReclassifyAccountsReceivable, 1, 2)
                    INDSlConceptReceivable.EditValue = .AccountReceivableConceptId
                    INDSlAccounts.Properties.NullText = (.AccountNumber & " - " & .AccountName)
                    INDSlAccounts.EditValue = .AccountId
                    PortfolioParameter = .PortfolioNoteConceptParameter
                End With
                Me.GetDocumentIndexed(Me.Tag & "_" & Me.KindsAgreements.Code)
                If result.Id = 0 Then
                    Me.BarraBotones.SetDocuments(KindsAgreements.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    record = New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = KindsAgreements.Id}
                    Dim operation = Await Model.SaveBlockRecord(record)
                    record = operation.ObjectEmbbeded
                Else
                    record = result
                    Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
                End If
            Else
                'LogicaBotonActualizar(False)
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
        Else
            'LogicaBotonActualizar(False)
            KindsAgreements = New KindsAgreements
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        End If
        AsyncLoader(False)
        ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
    End Function

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        ActionsOnControls = False
        INDbtnCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDSlConceptEgress.EditValue = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        KindsAgreements = Nothing
        Me.BarraBotones.StatusRecord = False
        Me.INDGleAffectsAccountsReceivable.EditValue = Nothing
        Me.INDGleReclassifyAccountsReceivable.EditValue = Nothing
        Me.INDSlConceptReceivable.EditValue = Nothing
        Me.INDSlAccounts.EditValue = Nothing
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDbtnCode.EditValue = String.Empty Then
            ValidateControls = False
        End If
        If INDtxtName.Text = String.Empty Then
            ValidateControls = False
        End If

        If INDGleReclassifyAccountsReceivable.EditValue = 1 Then
            If PortfolioParameter Then
                If INDGleReclassifyAccountsReceivable.EditValue = 1 And INDSlConceptReceivable.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe colocar un Concepto de Cuenta x Cobrar"
                    INDSlConceptReceivable.Focus()
                    ValidateControls = False
                End If

                If INDGleReclassifyAccountsReceivable.EditValue = 1 And INDSlAccounts.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "Debe colocar una Cuenta Contable x Cobrar"
                    INDSlAccounts.Focus()
                    ValidateControls = False
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = "Para Reclasificar Cxc debes parametrizar un Concepto Nota CxC "
                ValidateControls = False
            End If
        End If



    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With KindsAgreements
            .Code = INDbtnCode.EditValue
            .Description = INDtxtName.Text
            .IdExpenseConcepts = INDSlConceptEgress.EditValue
            .State = Me.BarraBotones.StatusRecord
            .AffectsAccountsReceivable = If(INDGleAffectsAccountsReceivable.EditValue = 1, 1, 0)
            .ReclassifyAccountsReceivable = If(INDGleReclassifyAccountsReceivable.EditValue = 1, 1, 0)
            .AccountReceivableConceptId = INDSlConceptReceivable.EditValue
            .AccountId = INDSlAccounts.EditValue
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


    Private Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(INDbtnCode.Text.ToString) Then
                LoadControls()
                If INDbtnCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
                INDbtnCode.Enabled = False
            End If
        End If
    End Sub

    Private Sub INDbtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbtnCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmKindsAgreements_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

#End Region

#Region "QueryPopup"
    Private Sub INDSlAccounts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlAccounts.QueryPopUp
        If Me.INDSlAccounts.Properties.DataSource Is Nothing Then
            Dim filter() As Object = {5, True}
            Using modelAccountsXPO As New MBusqueda
                INDSlAccounts.Properties.DataSource = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
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
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub
#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyKindsAgreements.ShowCustomizationForm()
    End Sub

    ''' <summary>
    ''' Evento DoWork que se utiliza para verificar si existe una definicion del xml del frontal
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.DoWorkEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles LoadhronousDefinitions.DoWork
        If CustomizacionFrontales.VerificaExisteDefinicionFrontal(PathFunctionalDefinitions) = True Then
            ExistDefinitionFront = True
        End If
    End Sub

    ''' <summary>
    ''' Evento RunWorkerCompleted que se utiliza para preguntar si encontro una definicion del frontal para posteriormente cargarla
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.ComponentModel.RunWorkerCompletedEventArgs" /> instance containing the event data.</param>
    Private Sub CargarDefinicionesAsincronas_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles LoadhronousDefinitions.RunWorkerCompleted
        If ExistDefinitionFront = True Then
            INDlyKindsAgreements.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyKindsAgreements.ShowCustomization
        Try
            'Ejecuatamos la consulta
            AsyncLoader(True)
            Dim dsFields As DataSet = Await Model.GetFieldsNULL
            AsyncLoader(False)
            If dsFields IsNot Nothing Then
                dtFieldsCustomizables = dsFields.Tables(0)
                For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                    For j As Integer = 0 To INDlyKindsAgreements.Items.Count - 1
                        If Object.Equals(INDlyKindsAgreements.Items.Item(j).Tag, Nothing) = False Then
                            If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyKindsAgreements.Items.Item(j).Tag.ToString.Trim Then
                                INDlyKindsAgreements.Items.Item(j).AllowHide = True
                            End If
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se cierra el formulario de customizacion y que guarda la definicion del layout en la ruta especificada de cada usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyKindsAgreements.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyKindsAgreements.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyKindsAgreements.SaveLayoutToXml(PathFunctionalDefinitions)
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorGuardarDefinicion)
                End If
            Catch ex As Exception
                IndigoManagementExceptions.HandleExceptionUI(ex, "UIPolicy")
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesErrorGuardarDefinicion)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Metodo para restablecer las definiciones del formulario gridLookUpEdit y Regillas
    ''' </summary>
    Private Sub ResetLayout()
        If My.Computer.FileSystem.DirectoryExists(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyKindsAgreements.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

#Region "Handlers"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ModoBusqueda = Nothing
        Model = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        KindsAgreements = Nothing
        Presenter = Nothing
        record = Nothing
        _FillingAffectsAccountsReceivable = Nothing
        _FillingReclassifyAccountsReceivable = Nothing
    End Sub
    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.KindsAgreements IsNot Nothing AndAlso Me.KindsAgreements.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub
#End Region


#Region "EditValueChanged"

    Private Sub INDGleAffectsAccountsReceivable_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleAffectsAccountsReceivable.EditValueChanged
        If INDGleAffectsAccountsReceivable.EditValue = 1 Then
            INDLciReclassifyAccountsReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciReclassifyAccountsReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDGleReclassifyAccountsReceivable.EditValue = Nothing
        End If

    End Sub

    Private Sub INDGleReclassifyAccountsReceivable_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleReclassifyAccountsReceivable.EditValueChanged
        If INDGleReclassifyAccountsReceivable.EditValue = 1 Then
            INDLciConceptReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciConceptReceivable.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSlConceptReceivable.EditValue = Nothing
            INDLciAccounts.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDSlAccounts.EditValue = Nothing
        End If
    End Sub

#End Region

End Class