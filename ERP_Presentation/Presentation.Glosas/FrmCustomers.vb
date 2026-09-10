#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Presentation.Controls.MVP
Imports Presentation.Glosas.MVP

#End Region

Public Class FrmCustomers
    Implements ICustomers

#Region "Variables"

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As MCustomers

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim _presenter As PCustomers

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Variable que contiene el Cliente
    ''' </summary>
    Dim _customer As Domain.Entities.Customer

    ''' <summary>
    ''' Variable que contiene la retención del cliente
    ''' </summary>
    Dim _customerRetention As Domain.Entities.CustomerRetention

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

#End Region

#Region "Properties"

    ''' <summary>
    ''' Esta propiedad contiene el codigo del Tercero
    ''' </summary>
    Public Property NitCustomers As String Implements ICustomers.NitCustomers
        Get
            Return INDbteNitCutomer.Text
        End Get
        Set(value As String)
            INDbteNitCutomer.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad contiene el nombre del Tercero
    ''' </summary>
    Public Property NameCustomers As String Implements ICustomers.NameCustomers
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el codigo EPS del tercero
    ''' </summary>
    ''' <value>Código EPS del tercero</value>
    ''' <returns>El código EPS del tercero</returns>
    Public Property EPSCodeCustomers As String Implements ICustomers.EPSCodeCustomers
        Get
            Return Me.INDtxtEPSCode.EditValue
        End Get
        Set(value As String)
            Me.INDtxtEPSCode.EditValue = value?.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la cuenta por cobrar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property MainAccountReceivableId As Integer Implements ICustomers.MainAccountReceivableId
        Get
            Return INDsleMainAccountReceivableId.EditValue
        End Get
        Set(value As Integer)
            INDsleMainAccountReceivableId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el plazo en dias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Term As Integer Implements ICustomers.Term
        Get
            Return INDspnTerm.EditValue
        End Get
        Set(value As Integer)
            INDspnTerm.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene la aplicacion del Tercero
    ''' </summary>
    Public Property StatusCustomers As Boolean Implements ICustomers.StatusCustomers
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
    ''' Obtiene o establece la clasificación de deterioro de facturación básica
    ''' </summary>
    Public Property BasicBillingClassificationId As Integer? Implements ICustomers.BasicBillingClassificationId
        Get
            Return INDsleBasicBillingClassification.EditValue
        End Get
        Set(value As Integer?)
            INDsleBasicBillingClassification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' DataSource del campo Clasificación factura Básica
    ''' </summary>
    ''' <returns></returns>
    Public Property BasicBillingClassificationXpo As XPInstantFeedbackSource Implements ICustomers.BasicBillingClassificationXpo
        Get
            Return INDsleBasicBillingClassification.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleBasicBillingClassification.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la clasificación de deterioro de facturación de salud
    ''' </summary>
    Public Property HealthInvoiceClassificationId As Integer? Implements ICustomers.HealthInvoiceClassificationId
        Get
            Return INDsleHealthInvoiceClassification.EditValue
        End Get
        Set(value As Integer?)
            INDsleHealthInvoiceClassification.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' DataSource del campo Clasificación Factura Salud
    ''' </summary>
    ''' <returns></returns>
    Public Property HealthInvoiceClassificationXpo As XPInstantFeedbackSource Implements ICustomers.HealthInvoiceClassificationXpo
        Get
            Return INDsleHealthInvoiceClassification.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleHealthInvoiceClassification.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' Obtiene o establece el datasource de los conceptos de nota de cartera
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Public Property PortfolioNoteConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICustomers.PortfolioNoteConceptXpo
        Get
            Return CType(INDSlePortfolioNoteConceptId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSlePortfolioNoteConceptId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el datasource de los conceptos de retencion
    ''' </summary>
    ''' <value>
    ''' The bank datasource.
    ''' </value>
    Public Property RetentionConceptXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ICustomers.RetentionConceptXpo
        Get
            Return CType(INDSleRetentionConceptId.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleRetentionConceptId.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Nit", .FieldName = "Nit"},
                              New ColumnInfo With {.Caption = "Descripción", .FieldName = "Name"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "State"}}.ToList()
            .ValorSolicitado = "Nit"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Customers
            .FormParent = Me
            .ShowSearch(False)
        End With
    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Sub Nuevo() Implements Base.ICrudBase.Nuevo
        CleanControls()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        Try
            If ValidateLocalControls() = False Then
                Exit Sub
            End If
            AssigningValues()
            Using Model As New MCustomers(Me.Tag)
                AsyncLoader(True)
                Dim Result = Await Model.SaveCustomers(_customer)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                    Me._customer = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Deshacer()
                Else
                    If Result.MessageResult IsNot Nothing Then
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = Result.Message
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.ICrudBase.Eliminar

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If _customer IsNot Nothing Then
            If _customer.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MCustomers(Me.Tag)
                        AsyncLoader(True)
                        Dim Result = Await Model.DeleteCustomers(_customer)
                        AsyncLoader(False)
                        If Result.StateResult = True Then
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            Await Me.DeleteDocumentIndexed()
                            Me.CleanControls()
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                        Else
                            If Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = ErrorConcurrencia Then
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                            ElseIf Result.MessageResult IsNot Nothing AndAlso Result.MessageResult(0) = ErrorDependencia Then
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorDependencia)
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
            Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(SeleccioneUnConceptoGeneral, ConceptosGenerales)
        End If
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICustomers.ActionsOnControls
        Set(value As Boolean)
            INDlycRoot.BeginUpdate()

            INDbteNitCutomer.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtEPSCode.Enabled = value
            INDsleThirdParty.Enabled = value
            INDsleMainAccountReceivableId.Enabled = value
            INDspnTerm.Enabled = value
            INDsleBasicBillingClassification.Enabled = value
            INDsleHealthInvoiceClassification.Enabled = value
            INDPceRetention.Enabled = value
            INDGcRetentions.Enabled = value

            INDlycRoot.EndUpdate()

            If value = True Then
                INDtxtName.Focus()
            Else
                INDbteNitCutomer.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Async Sub CleanControls()
        INDlycRoot.BeginUpdate()
        Await DeleteBlockedRecord()

        INDbteNitCutomer.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtEPSCode.Text = String.Empty
        INDsleThirdParty.DisplayNullText = String.Empty
        INDsleThirdParty.EditValue = Nothing
        INDsleMainAccountReceivableId.EditValue = Nothing
        INDsleMainAccountReceivableId.Properties.NullText = String.Empty
        INDspnTerm.EditValue = Nothing
        INDsleBasicBillingClassification.EditValue = Nothing
        INDsleHealthInvoiceClassification.EditValue = Nothing
        CleanControlsCustomerRetention()

        INDGcRetentions.DataSource = Nothing

        Me._doc = Nothing
        Me._customer = Nothing
        ActionsOnControls = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlycRoot.EndUpdate()
    End Sub

    Private Sub CleanControlsCustomerRetention()
        INDSlePortfolioNoteConceptId.EditValue = Nothing
        INDSlePortfolioNoteConceptId.Properties.NullText = String.Empty
        INDSleRetentionConceptId.EditValue = Nothing
        INDSleRetentionConceptId.Properties.NullText = String.Empty

        Me._customerRetention = Nothing
    End Sub

    ''' <summary>
    ''' Aqui se realiza la busqueda de una entidad
    ''' </summary>
    Private Async Sub FindEntity()
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        Await LoadControls()
        If INDbteNitCutomer.Enabled = False Then
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        End If
        INDbteNitCutomer.Enabled = False
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>

    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        If Not String.IsNullOrEmpty(NitCustomers) AndAlso Not String.IsNullOrWhiteSpace(NitCustomers) Then
            Try
                Using Model As New MCustomers(Me.Tag)
                    AsyncLoader(True)
                    INDlycRoot.BeginUpdate()
                    _customer = Await Model.GetCustomerWithouState(INDbteNitCutomer.Text)
                    If Not _customer Is Nothing AndAlso _customer.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        _blockRecord = Await Model.GetBlockRecord(Me.Tag, Me._customer.Id)
                        With _customer
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                            NitCustomers = .Nit
                            NameCustomers = .Name
                            INDsleThirdParty.DisplayNullText = (.ThirdParty.Nit & " - " & .ThirdParty.Name)
                            INDsleThirdParty.EditValue = .ThirdPartyId
                            EPSCodeCustomers = .EPSCode
                            INDsleMainAccountReceivableId.Properties.NullText = (.MainAccounts.Number & " - " & .MainAccounts.Name)
                            MainAccountReceivableId = .MainAccountReceivableId
                            Term = .Term

                            BasicBillingClassificationId = .BasicBillingDeteriorationClassificationId
                            HealthInvoiceClassificationId = .HealthInvoiceDeteriorationClassificationId

                            StatusCustomers = .State

                            INDGcRetentions.DataSource = Nothing
                            INDGcRetentions.DataSource = .CustomerRetention
                        End With

                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._customer.Nit)

                        If _blockRecord.Id = 0 Then
                            _blockRecord = (Await Model.SaveBlockRecord(
                                    New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _customer.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                        End If

                        Me.BarraBotones.SetDocuments(_customer.Id)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        Me.BarraBotones.StatusRecordVisible = True
                        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        _customer = New Domain.Entities.Customer With {.State = True}

                        AsyncLoader(False)
                        ActionsOnControls = True
                    End If
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteNitCutomer.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._customer IsNot Nothing AndAlso Me._customer.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me._blockRecord IsNot Nothing AndAlso Me.INDbteNitCutomer.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbteNitCutomer.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteNitCutomer.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        INDbteNitCutomer.Text = ReturnValue
        If INDbteNitCutomer.Text <> String.Empty Then
            Await LoadControls()
            If INDbteNitCutomer.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteNitCutomer.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmCustomersMetaData, Eform.InfoMetaData), Me._customer.Nit, Me._customer.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me._customer.Nit & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(FrmCustomersMetaDataTitle, InfoMetaData), Me._customer.Nit),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCustomersMetaData, Eform.InfoMetaData), Me._customer.Nit, Me._customer.Name)
            Me._doc.Title = String.Format(obtenerRecurso(FrmCustomersMetaDataTitle, InfoMetaData), Me._customer.Nit)
            Return Me._doc
        End If
    End Function

    Private Function ValidateLocalControls() As Boolean
        If INDsleMainAccountReceivableId Is Nothing Or INDsleMainAccountReceivableId.EditValue <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe colocar una Cuenta Contable x Cobrar"
            INDsleMainAccountReceivableId.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidateControlsCustomerRetentionPopup() As Boolean
        Dim listErrors As New StringBuilder

        If INDSlePortfolioNoteConceptId.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un concepto de nota de cartera.")
            INDSlePortfolioNoteConceptId.Focus()
        End If

        If INDSleRetentionConceptId.EditValue Is Nothing Then
            listErrors.AppendLine("Debe seleccionar un concepto de retención.")
            INDSleRetentionConceptId.Focus()
        End If

        If Me._customer.CustomerRetention IsNot Nothing AndAlso Me._customer.CustomerRetention.Any() Then
            If Me._customer.CustomerRetention.Any(Function(d) d.PortfolioNoteConceptId = INDSlePortfolioNoteConceptId.EditValue AndAlso d.RetentionConceptId = INDSleRetentionConceptId.EditValue) Then
                listErrors.AppendLine("Ya existe una retención agregada con el concepto de nota y el concepto de retención.")
            End If
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString()
            Return False
        End If

        Return True
    End Function

    Private Sub AddCustomerRetention()
        If Not ValidateControlsCustomerRetentionPopup() Then
            Exit Sub
        End If

        _customerRetention = New Domain.Entities.CustomerRetention With
        {
            .PortfolioNoteConceptId = INDSlePortfolioNoteConceptId.EditValue,
            .PortfolioNoteConceptCodeName = INDSlePortfolioNoteConceptId.Text,
            .RetentionConceptId = INDSleRetentionConceptId.EditValue,
            .RetentionConceptCodeName = INDSleRetentionConceptId.Text,
            .Status = True
        }

        Me._customer.CustomerRetention.Add(_customerRetention)
        Mensaje(EeventViewerImages.Informacion) = "Retención agregada correctamente"

        INDGcRetentions.DataSource = _customer.CustomerRetention
        INDGcRetentions.RefreshDataSource()

        CleanControlsCustomerRetention()
        INDPceRetention.ShowPopup()
        INDSlePortfolioNoteConceptId.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With _customer
            .Nit = INDbteNitCutomer.Text
            .Name = INDtxtName.Text
            .EPSCode = INDtxtEPSCode.Text
            .ThirdPartyId = INDsleThirdParty.EditValue
            .MainAccountReceivableId = MainAccountReceivableId
            .Term = Term
            .BasicBillingDeteriorationClassificationId = BasicBillingClassificationId
            .HealthInvoiceDeteriorationClassificationId = HealthInvoiceClassificationId
        End With
    End Sub

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 Then
            Using Model As New MCustomers(Me.Tag)
                Await Model.DeleteBlockRecord(_blockRecord)
                _blockRecord = Nothing
            End Using
        Else
            Me.BarraBotones.EnableBarItems()
        End If
    End Function

    ''' <summary>
    ''' Función que obtiene los parámetros de Cuentas por Cobrar
    ''' </summary>
    ''' <returns></returns>
    Private Async Function GetSettingPortfolio() As Task(Of SettingPortfolio)
        Using model As New Portfolio.MVP.MSettingPortfolio(Me.Tag)
            Return Await model.GetSettingPortfolioByIdOperatingUnitAsync(_idOperativeUnit)
        End Using
    End Function

    ''' <summary>
    ''' Función que evalúa si se debe mostrar el campo de Clasificación de Cartera
    ''' </summary>
    Private Async Function ShowPortfolioClassificationFields() As Task
        Dim settingPortfolio As SettingPortfolio = Await GetSettingPortfolio()

        INDLciBasicBillingClassification.HideControl(True)
        INDLciHealthInvoiceClassification.HideControl(True)

        If settingPortfolio?.ApplyDeteriorationByClassification Then
            INDLciBasicBillingClassification.HideControl(False)
            _presenter.InitializeBasicBillingClassification()
            Dim rulesDeteriorationClassification As RulesDeteriorationClassification = settingPortfolio.RulesDeteriorationClassification.FirstOrDefault()
            If rulesDeteriorationClassification?.HealthPortfolioClassification = 2 Then
                INDLciHealthInvoiceClassification.HideControl(False)
                _presenter.InitializeHealthInvoiceClassification()
            End If
        End If
    End Function

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el formularios
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub FrMCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.INDlycRoot.Dock = DockStyle.Fill
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.Funct = AddressOf GenerateDoc
        Model = New MCustomers(Me.Tag)
        _presenter = New PCustomers(Me)
        _idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Await ShowPortfolioClassificationFields()

        Me.INDsleThirdParty.FuncQueryOnKeyEnterPressed = AddressOf Model.GetThirdPartyAsync
        IndigoGridControl1.RefreshGrid(INDGcRetentions)
        LoadStatus()
        Deshacer()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        Model = Nothing
        _presenter = Nothing
        _blockRecord = Nothing
        _customer = Nothing
        _customerRetention = Nothing
    End Sub

#End Region

#Region "Closing"

    ''' <summary>
    ''' Aqui se desbloquea el registro
    ''' </summary>
    Private Async Sub FrmCustomers_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Await Me.DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteNitCutomer.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The instance containing the event data.</param>
    Private Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteNitCutomer.KeyDown
        If Not String.IsNullOrEmpty(INDbteNitCutomer.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Me.FindEntity()
                If Me.INDbteNitCutomer.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                End If
            End If
        End If
    End Sub

#End Region

#Region "Activated"

    Private Sub FrmCustomers_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteNitCutomer.Text Is String.Empty Then
            INDbteNitCutomer.Focus()
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleThirdParty_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleThirdParty.QueryPopUp
        If Me.INDsleThirdParty.Datasource Is Nothing Then
            Using model As New MBusqueda()
                Me.INDsleThirdParty.Datasource = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub

    Private Sub INDsleMainAccountReceivableId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleMainAccountReceivableId.QueryPopUp
        If Me.INDsleMainAccountReceivableId.Properties.DataSource Is Nothing Then
            Dim filter() As Object = {5, True}
            Using modelAccountsXPO As New MBusqueda
                INDsleMainAccountReceivableId.Properties.DataSource = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
    End Sub

    Private Sub INDSlePortfolioNoteConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlePortfolioNoteConceptId.QueryPopUp
        If PortfolioNoteConceptXpo Is Nothing Then
            _presenter.InitializePortfolioNoteConcept()
        End If
    End Sub

    Private Sub INDSleRetentionConceptId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleRetentionConceptId.QueryPopUp
        If RetentionConceptXpo Is Nothing Then
            Using model As New MBusqueda()
                _presenter.InitializeRetentionConcept()
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

#Region "Handlers"

    Private Sub INDsleThirdParty_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDsleThirdParty.OpenFormButtonClick
        Using Formulario As New FrmThirdParty
            Formulario.ViewModeEditHold = True
            Formulario.StartPosition = FormStartPosition.CenterParent
            Dim transparent As New FrmTransparent(Formulario, False)
            transparent.ShowDialog()
            Using msearch As New MBusqueda()
                INDsleThirdParty.Datasource = msearch.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End Using
    End Sub

    Private Sub INDsleThirdParty_InternalError(sender As Object, e As InternalErrorEventArgs) Handles INDsleThirdParty.InternalError
        Me.Mensaje(EeventViewerImages.MensajeError) = e.Exception.Message
    End Sub

    Private Sub INDsleThirdParty_EndPerformQueryFunction(sender As Object, e As EndPerformQueryFunctionEventArgs) Handles INDsleThirdParty.EndPerformQueryFunction
        If e.Result Is Nothing OrElse e.Result.Id = 0 Then
            If INDsleThirdParty.OldSelectedObject IsNot Nothing Then
                INDsleThirdParty.DisplayNullText = (INDsleThirdParty.OldSelectedObject.Nit & " - " & INDsleThirdParty.OldSelectedObject.Name)
                INDsleThirdParty.EditValue = INDsleThirdParty.OldEditValue
            ElseIf Me._customer IsNot Nothing AndAlso _customer.ThirdParty IsNot Nothing Then
                INDsleThirdParty.DisplayNullText = (_customer.ThirdParty.Nit & " - " & _customer.ThirdParty.Name)
                INDsleThirdParty.EditValue = _customer.ThirdPartyId
            Else
                INDsleThirdParty.DisplayNullText = String.Empty
                INDsleThirdParty.EditValue = Nothing
            End If
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("MessageThirdPartyDontExists")
        End If
    End Sub

#End Region

    Private Sub INDsleMainAccountReceivableId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleMainAccountReceivableId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Using msearch As New MBusqueda()
                Dim filter() As Object = {5, True}
                INDsleMainAccountReceivableId.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
    End Sub

    Private Sub INDsleBasicBillingClassification_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBasicBillingClassification.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(89029, Nothing, True)
        End If
    End Sub

    Private Sub INDsleHealthInvoiceClassification_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleHealthInvoiceClassification.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(89029, Nothing, True)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbRetentionAdd_Click(sender As Object, e As EventArgs) Handles INDsbRetentionAdd.Click
        AddCustomerRetention()
    End Sub

#End Region

#End Region

#Region "Bar Button Events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click guardar.
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click eliminar.
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Eliminar()
    End Sub

    ''' <summary>
    ''' Activar - inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If _customer IsNot Nothing Then
            With _customer
                .State = Not _customer.State
            End With
        End If
        Guardar()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        Me.OpenCustomize()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

#End Region

End Class