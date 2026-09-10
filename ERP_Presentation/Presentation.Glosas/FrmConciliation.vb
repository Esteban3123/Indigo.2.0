'***********************************************************************
' Assembly         : Presentacion.Glosas
' Author           : Juan F. Tamayo
' Created          : 2013-04-20
'
' Last Modified By : Juan F. Tamayo
' Last Modified On : 2013-04-20
' Description      : Vista del del frontal de conciliación
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
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Mask
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
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
''' Vista del frontal de conciliación
''' </summary>
Public Class FrmConciliation
    Implements IConciliation

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Glosas"

#End Region

#Region "Enums"

    ''' <summary>
    ''' Enumeración de grupos en el formulario
    ''' </summary>
    Private Enum EGroups

        ''' <summary>
        ''' Representa el grupo raiz del formulario
        ''' </summary>
        Root = 0
        ''' <summary>
        ''' Representa el grupo de detalles de facturas
        ''' </summary>
        InvoiceDetails
        ''' <summary>
        ''' Representa el grupo de conciliar detalle de factura
        ''' </summary>
        ConciliateInvoiceDetail

    End Enum

#End Region

#Region "Constructor"

    ''' <summary>
    ''' initialization
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New()
        InitializeComponent()
        _openFindSenser = "INDbteConsecutive"
    End Sub

#End Region

#Region "Variables"

    ''' <summary>
    ''' Control de trazabilidad
    ''' </summary>
    Private _ctrTraceabilityControl As Presentation.Controls.CtrTraceabilityControl

    ''' <summary>
    ''' Referencia al modelo
    ''' </summary>
    Private _myModel As MConciliation

    ''' <summary>
    ''' Referencia al presentador
    ''' </summary>
    Private _presenter As PConciliation

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _blockRecord As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Parametros de Glosas
    ''' </summary>
    Private _parameterGlosas As TimeParameters

    ''' <summary>
    ''' Entidad de la conciliacion
    ''' </summary>
    Private _conciliation As Domain.Entities.ConciliationC

    ''' <summary>
    ''' Encapsula el cliente consultado
    ''' </summary> 
    Private _customer As Domain.Entities.Customer

    ''' <summary>
    ''' Encapsula el participante que se va a modificar
    ''' </summary>
    Private _currentParticipant As Domain.Entities.ConciliationParticipants

    ''' <summary>
    ''' Lista detalle de la conciliacion
    ''' </summary>
    Private _listConciliationDetail As List(Of Domain.Entities.ConciliationD)

    ''' <summary>
    ''' Detalle de conciliacion
    ''' </summary>
    ''' <remarks></remarks>
    Private _conciliationDetail As Domain.Entities.ConciliationD

    ''' <summary>
    ''' Almacena el nombre del control que hizo el llamado al metodo buscar
    ''' </summary>
    Private _openFindSenser As String

    ''' <summary>
    ''' Variable para saber si el frontal abre por modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' valor maximo que esta pendiente de conciliar
    ''' </summary>
    ''' <remarks></remarks>
    Private _valueConstMaximoPending As Decimal

    ''' <summary>
    ''' control de saldos nuevos en rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private _valControlNew As Decimal

    ''' <summary>
    ''' Valor pendiente a conciliar
    ''' </summary>
    ''' <remarks></remarks>
    Private valuePending As Decimal

    ''' <summary>
    ''' Objeto openFileDialog
    ''' </summary>
    Private _fileOpener As New OpenFileDialog

    ''' <summary>
    ''' ruta excel a cargar
    ''' </summary>
    ''' <remarks></remarks>
    Private _rutaExcel As String


    ''' <summary>
    ''' Encapsula el grupo actual en el que se encuentra el foco
    ''' </summary>
    Private _currentActiveGroup As EGroups

#End Region

#Region "Properties"

    ''' <summary>
    ''' Asigna el valor de activo o inactivo a los controles
    ''' </summary>
    ''' <value>Valor que indica si se activan o inactivan</value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IConciliation.ActionsOnControls
        Set(value As Boolean)
            Dim statusRegistered = (_conciliation Is Nothing OrElse _conciliation.State = "1")

            Me.INDbteConsecutive.Enabled = Not value
            Me.INDdetDateDocument.Enabled = value
            Me.INDbteNit.Enabled = value
            Me.INDtxtDocument.Enabled = value
            Me.INDpceComment.Enabled = value
            Me.INDmemComment.Enabled = value

            Me.INDpceParticipants.Enabled = value
            Me.INDtxtParticipantName.Enabled = If(statusRegistered, value, False)
            Me.INDtxtParticipantPosition.Enabled = If(statusRegistered, value, False)
            Me.INDrdgParticipantType.Enabled = If(statusRegistered, value, False)
            Me.INDbtnAddParticipant.Enabled = If(statusRegistered, value, False)

            Me.INDsleSearchInvoice.Enabled = If(statusRegistered, value, False)
            Me.INDbtnAddInvoice.Enabled = If(statusRegistered, value, False)
            Me.INDbtnLoad.Enabled = If(statusRegistered, value, False)
            Me.INDbtnDeleteInvoice.Enabled = If(statusRegistered, value, False)
            Me.INDgdcInvoices.Enabled = value

            Me.INDbteNit.Focus()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el numero consecutivo de la conciliacion
    ''' </summary>
    ''' <value>Numero de la conciliacion</value>
    ''' <returns>El numero de la conciliacion</returns>
    Public Property Consecutive As Long Implements IConciliation.Consecutive
        Get
            Return Convert.ToInt64(Me.INDbteConsecutive.Text.Trim())
        End Get
        Set(value As Long)
            Me.INDbteConsecutive.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fecha de conciliacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    Public Property DateConciliation As Date Implements IConciliation.DateConciliation
        Get
            Return Convert.ToDateTime(Me.INDtxtToday.Tag)
        End Get
        Set(value As Date)
            Me.INDtxtToday.Tag = value
            Me.INDtxtToday.Text = value.ToString("D", indigo.Culture)
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la fecha del oficio
    ''' </summary>
    ''' <value>Fecha del oficio</value>
    ''' <returns>Fecha del oficio</returns>
    Public Property DateDocument As Date Implements IConciliation.DateDocument
        Get
            Return Convert.ToDateTime(Me.INDdetDateDocument.EditValue)
        End Get
        Set(value As Date)
            Me.INDdetDateDocument.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el nit de la entidad
    ''' </summary>
    ''' <value>Nit de la entidad</value>
    ''' <returns>Nit de la entidad</returns>
    Public Property Nit As String Implements IConciliation.Nit
        Get
            Return Me.INDbteNit.EditValue
        End Get
        Set(value As String)
            Me.INDbteNit.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el codigo o numero del oficio
    ''' </summary>
    ''' <value>Codigo o numero del oficio</value>
    ''' <returns>Codigo o numero del oficio</returns>
    Public Property Document As String Implements IConciliation.Document
        Get
            Return Me.INDtxtDocument.Text.Trim()
        End Get
        Set(value As String)
            Me.INDtxtDocument.Text = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna el comentario de observacion de la conciliacion
    ''' </summary>
    ''' <value>Comentario de observacion</value>
    ''' <returns>Comentario de observacion</returns>
    Public Property Comment As String Implements IConciliation.Comment
        Get
            Return Me.INDmemComment.Text.Trim()
        End Get
        Set(value As String)
            Me.INDmemComment.Text = value.Trim()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna los participantes
    ''' </summary>
    ''' <value>Lista de participantes</value>
    ''' <returns>La lista de participantes</returns>
    Public Property Participants As Object Implements IConciliation.Participants
        Get
            Return Me.INDgdcParticipants.DataSource
        End Get
        Set(value As Object)
            Me.INDgdcParticipants.DataSource = (From p In CType(value, Domain.Entities.TrackableCollection(Of Domain.Entities.ConciliationParticipants)) Where p.ChangeTracker.State <> ObjectState.Deleted Select p).ToList()
            Me.INDgdcParticipants.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la lista de facturas seleccionadas
    ''' </summary>
    ''' <value>Lista de facturas seleccionadas</value>
    ''' <returns>Lista de facturas seleccionadas</returns>
    Public Property SelectedInvoices As Object Implements IConciliation.SelectedInvoices
        Get
            Return Me.INDgdcInvoices.DataSource
        End Get
        Set(value As Object)
            Me.INDgdcInvoices.DataSource = (From p In CType(value, List(Of Domain.Entities.ConciliationD)) Where p.ChangeTracker.State <> ObjectState.Deleted Select p).ToList()
            Me.INDgdcInvoices.RefreshDataSource()
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Asigna el ID del concepto de Aceptación Jerarquíco
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdResponseHierarchy As Integer?
        Get
            Return INDgleResponseHierarchy.EditValue
        End Get
        Set(value As Integer?)
            Me.INDgleResponseHierarchy.EditValue = value
        End Set
    End Property

    Private Property DatasourceResponseHierarchy As Object
        Get
            Return INDgleResponseHierarchy.Datasource
        End Get
        Set(value As Object)
            Me.INDgleResponseHierarchy.Datasource = value
        End Set
    End Property

#End Region

#Region "Datasources"

    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceCustomers As XPInstantFeedbackSource

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

#End Region

#Region "ICrud Bas"

    ''' <summary>
    ''' Abre el frontal de busqueda
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        Me.OpenSearch()
    End Sub

    ''' <summary>
    ''' Abre el frontal del busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If

        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Consecutivo", .FieldName = "ConciliationConsecutive"},
                        New ColumnInfo With {.Caption = "Fecha", .FieldName = "DocumentDate"},
                        New ColumnInfo With {.Caption = "Entidad", .FieldName = "NitName"},
                        New ColumnInfo With {.Caption = "Factura", .FieldName = "InvoiceNumber"},
                        New ColumnInfo With {.Caption = "Estado", .FieldName = "State"}}.ToList()
            .ValorSolicitado = "ConciliationConsecutive"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ConciliationC
            .FormParent = Me
            .ShowSearch(False)
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedRecord()
        If Not String.IsNullOrEmpty(ReturnValue) Then
            Me.INDbteConsecutive.Text = ReturnValue
            Await LoadControls()
            If INDbteConsecutive.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteConsecutive.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Deshace los cambios realizados
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        Me.CleanControls()
    End Sub

    ''' <summary>
    ''' Deja el frontal listo para una nueva busqueda o insercion de datos
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
        Me.NewConciliation()
    End Sub

    ''' <summary>
    ''' Guarda la conciliacion
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            Select Case Me._currentActiveGroup
                Case EGroups.Root
                    If _conciliation.State <> "3" Then
                        If Me.ValidateControls() = False Then
                            Exit Sub
                        End If

                        If (From d In Me._listConciliationDetail Where d.ChangeTracker.State <> ObjectState.Deleted Select d).Count = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ConciliacionSinDetalles, Eform.Conciliation)
                            Exit Sub
                        End If
                    End If

                    Me.AssigningValues()

                    'Bandera para saber si se guardo algo e indexar
                    Dim flagSaved As Boolean = False
                    Dim showMessage As Boolean = False

                    Me.AsyncLoader(True)
                    If Me._conciliation.ChangeTracker.State <> ObjectState.Unchanged Then
                        Using model As New MConciliation(Me.Tag)
                            Dim result = Await model.SaveConciliationC(Me._conciliation)
                            If result.StateResult Then
                                Me._conciliation = result.ObjectEmbbeded
                                Me.INDbteConsecutive.Text = Me._conciliation.ConciliationConsecutive
                                Me.Participants = Me._conciliation.ConciliationParticipants

                                Me.INDbteNit.Enabled = False

                                flagSaved = True
                                showMessage = result.StateResult
                            Else
                                Mensaje(EeventViewerImages.Advertencia) = result.Message
                                Me.AsyncLoader(False)
                                Exit Sub
                            End If
                        End Using
                    End If

                    If _conciliation.State <> "3" Then
                        Dim result = Await SavelistConciliationDetail()
                        If result.StateResult Then
                            flagSaved = True
                            showMessage = result.StateResult
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = result.Message
                            Me.AsyncLoader(False)
                            Exit Sub
                        End If
                    End If

                    If flagSaved Then
                        Me.BarraBotones.SetDocuments(Me._conciliation.Id)
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    End If

                    Me.AsyncLoader(False)

                    If showMessage Then
                        If Me._conciliation.State = "3" Then
                            Mensaje(EeventViewerImages.Informacion) = "La conciliación fue anulada correctamente"
                            Me.Deshacer()
                        Else
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesActualizado)
                        End If
                    End If
                Case EGroups.ConciliateInvoiceDetail
                    Me.SaveConciliationDetail()
            End Select
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Envia a guardar la conciliacion del detalle de factura actualmente seleccionado
    ''' </summary>
    Private Async Sub SaveConciliationDetail()
        Dim list = TryCast(Me.INDgdcConciliateInvoiceDetail.DataSource, List(Of Domain.Entities.GlosaMovementGlosa))
        If list IsNot Nothing AndAlso list.Count > 0 Then
            For Each m As Domain.Entities.GlosaMovementGlosa In list
                If m.ValPendingIPSconciliation > 0 AndAlso m.IdResponseHierarchyConciliation Is Nothing Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un concepto de aceptación"
                    Exit Sub
                End If
                m.ConciliationCId = _conciliation.Id
                Dim valueAcceptedIPSconciliation = m.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedIPSconciliation)
                Dim valueAcceptedEAPBconciliation = m.GlosaMovementGlosaConciliation.Where(Function(d) d.State = 2).Sum(Function(d) d.ValueAcceptedEAPBconciliation)
                m.ValueAcceptedIPSconciliation = valueAcceptedIPSconciliation + m.ValPendingIPSconciliation
                m.ValueAcceptedEAPBconciliation = valueAcceptedEAPBconciliation + m.ValPendingEAPBconciliation
            Next
            Using model As New MConciliation(Me.Tag)
                Me.AsyncLoader(True)
                Dim result = Await model.SaveConciliationInvoiceDetails(list)
                Me.AsyncLoader(False)
                If result.StateResult Then
                    Me.RefreshInformation()
                    Me.GoToProcess(EGroups.InvoiceDetails)
                Else
                    If result.MessageResult(0) = "-999" Then
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesErrorConcurrencia)
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(Eresources.ComunesContacteAdministrador)
                    End If
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la accion de confirmar dependiendo del contexto en que se
    ''' encuantre situado el proceso.
    ''' </summary>
    Private Async Sub ConfirmProcess()
        Try
            Select Case Me._currentActiveGroup
                Case EGroups.Root
                    If (From i As Domain.Entities.ConciliationD In CType(Me.INDgdcInvoices.DataSource, List(Of Domain.Entities.ConciliationD)) Where i.State = "-1" Select i).Any Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FaltanFacturasPorPersistir, Eform.Conciliation)
                        Exit Sub
                    End If
                    If (From i As Domain.Entities.ConciliationD In CType(Me.INDgdcInvoices.DataSource, List(Of Domain.Entities.ConciliationD)) Where i.State = "1" Select i).Any Then
                        Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FaltanFacturasPorConciliar, Eform.Conciliation)
                        Exit Sub
                    End If
                    If (From d In Me._listConciliationDetail Where d.ChangeTracker.State <> ObjectState.Deleted Select d).Count = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ConciliacionSinDetalles, Eform.Conciliation)
                        Exit Sub
                    Else
                        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                            Using model As New MConciliation(Me.Tag)
                                Me.AsyncLoader(True)
                                Await SavelistConciliationDetail()
                                Dim ConfirmDate = Await Me._myModel.GetServerDate()
                                Me._conciliation.ConfirmUser = SessionValues.Instance.UserIndigoId()
                                Me._conciliation.ConfirmDate = ConfirmDate
                                Me._conciliation.State = "2"
                                Dim result = Await model.SaveConciliationC(Me._conciliation)
                                If result.StateResult Then
                                    Me.Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesConfirmadoCorrectamente)
                                Else
                                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                                End If
                                Me.AsyncLoader(False)
                            End Using
                            Me._searchMode = False
                            Me.Deshacer()
                        End If
                    End If
                Case EGroups.InvoiceDetails
                    Dim msgvalueAcceptEAPB As Decimal
                    Dim msgvalueAcceptIPS As Decimal
                    For Each d As Domain.Entities.GlosaInvoiceDetail In CType(Me.INDgdcInvoiceDetails.DataSource, List(Of Domain.Entities.GlosaInvoiceDetail))
                        msgvalueAcceptIPS = msgvalueAcceptIPS + d.CalculateAcceptedIPSConciliation_PartialConciliation()
                        msgvalueAcceptEAPB = msgvalueAcceptEAPB + d.CalculateAcceptedEAPBConciliation_PartialConciliation()
                    Next
                    If msgvalueAcceptIPS = 0 And msgvalueAcceptEAPB = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se a registrado ningún movimiento, no se puede confirmar"
                        Exit Sub
                    End If
                    If MessageIndigo.Show(String.Format(obtenerRecurso(Eresources.ConciliacionConfirmarFactura, Eform.Conciliation), Me.INDlblInvoiceNumber.Text.Trim(), msgvalueAcceptIPS.ToString("C2", indigo.Culture), msgvalueAcceptEAPB.ToString("C2", indigo.Culture)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                        Using model As New MConciliation(Me.Tag)
                            Me.AsyncLoader(True)
                            Dim result = Await model.ConfirmConciliationInvoice(Me._conciliation.Id, Me.INDlblInvoiceNumber.Text.Trim(), _idOperativeUnit)
                            If result.StateResult Then
                                Dim aux = TryCast(Me.INDgdvInvoiceDetails.GetRow(Me.INDgdvInvoiceDetails.FocusedRowHandle), Domain.Entities.GlosaInvoiceDetail)
                                'Controlamos el mensaje de la ejecucuion de la interfaz
                                If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                    Dim strMensaje As New StringBuilder
                                    For Each item As String In result.MessageResult
                                        strMensaje.AppendLine(item)
                                    Next
                                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                                    Mensaje(EeventViewerImages.Informacion) = strMensaje.ToString
                                End If
                                CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD).State = "2"
                                'Se agrega para que al momento de confirmar la factura, se confirme el estado de cada detalle
                                '============================================================================================
                                Me.SelectedInvoices = Await GetListConciliationDetailAdded(_listConciliationDetail)
                                Me.INDgdcInvoices.RefreshDataSource()
                                Me.GoToProcess(EGroups.Root)
                            ElseIf result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                Dim strMensaje As New StringBuilder
                                For Each item As String In result.MessageResult
                                    strMensaje.AppendLine(item)
                                Next
                                Mensaje(EeventViewerImages.Advertencia) = strMensaje.ToString
                            Else
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                            End If
                            Me.AsyncLoader(False)
                        End Using
                    End If
                    ' End If
                Case EGroups.ConciliateInvoiceDetail 'Esta opcion se inhabilita para quitar la funcionalidad de confirmar cada detalle
            End Select
        Catch ex As Exception
            Me.AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Posiciona el estado del frontal en el proceso deseado
    ''' </summary>
    ''' <param name="state">El grupo en el que quiero posicionar el frontal</param>
    Private Sub GoToProcess(ByVal state As EGroups)
        Select Case state
            Case EGroups.Root
                Me.INDlycgInvoiceDetailsMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgHeader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.LogicaBotonActualizar(True)
                Me.BarraBotones.StatusRecord = Me._conciliation.State
                Me._currentActiveGroup = EGroups.Root
            Case EGroups.ConciliateInvoiceDetail
                Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgInvoiceDetailsMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloDetalleFactura, Eform.Conciliation)
                Me.INDlycgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgHeader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.BarraBotones.PrepareToolbar(eAction.Process)
                Me._currentActiveGroup = EGroups.InvoiceDetails
            Case EGroups.InvoiceDetails
                Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloDetalleFactura, Eform.Conciliation)

                If Me._conciliation.State = "1" Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
                    Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = True
                    Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = True
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = False
                    Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = False
                End If
                Me._currentActiveGroup = EGroups.InvoiceDetails
        End Select
    End Sub

    ''' <summary>
    ''' No se implementa
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' No se usa
    ''' </summary>
    Public Sub Eliminar() Implements ICrudBase.Eliminar

    End Sub

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

#End Region

#Region "Methods"

    ''' <summary>
    ''' Crea y carga el control de tiempo en la barra
    ''' </summary>
    Private Sub LoadXtraTrackControl()
        If Me._ctrTraceabilityControl Is Nothing Then
            Me._ctrTraceabilityControl = New CtrTraceabilityControl()
            Me._ctrTraceabilityControl.Process = GlosasProcess.Conciliation
            Me._ctrTraceabilityControl.Dock = DockStyle.Fill
            Me.AdditionalControlPanel.Controls.Add(Me._ctrTraceabilityControl)
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "-3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-2", .StatusName = ResourceManager.GetString("StateConciliated"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StateUnconciliated"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
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
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                'Grid detalle
                For Each item In INDgdvInvoiceDetails.Columns
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                'Grid detalle QX
                For Each item In INDgdvInvoiceDetailsQX.Columns
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                'Grid detalle conciliaciones
                For Each item In INDgdvConciliateInvoiceDetail.Columns
                    If item.DisplayFormat.FormatString = "C2" OrElse item.DisplayFormat.FormatString = "c2" Then
                        item.DisplayFormat.FormatString = "C0"
                        item.SummaryItem.DisplayFormat = "Total: {0:c0}"
                    End If
                Next
                'Summary de los grupos QX
                For Each item1 As GridSummaryItem In INDgdvInvoiceDetailsQX.GroupSummary
                    item1.DisplayFormat = "Total: {0:c0}"
                Next
                'Summary de los grupos NO QX
                For Each item1 As GridSummaryItem In INDgdvInvoiceDetails.GroupSummary
                    item1.DisplayFormat = "Total: {0:c0}"
                Next
                INDRptConsValAceptIPS.Mask.MaskType = MaskType.Numeric
                INDRptConsValAceptIPS.Mask.EditMask = "N00"
            End If
        End Using
    End Function

    ''' <summary>
    ''' Limpiar los controles del frontal
    ''' </summary>
    Private Async Sub CleanControls()
        INDlycRoot.BeginUpdate()
        Await Me.DeleteBlockedRecord()

        Me.INDbteConsecutive.Text = String.Empty
        Dim d As DateTime = GetServerDate()
        Me.INDtxtToday.Tag = d
        Me.INDtxtToday.Text = d.ToString("D", indigo.Culture)
        Me.INDdetDateDocument.Text = String.Empty
        Me.INDbteNit.EditValue = Nothing
        Me.INDbteNit.DisplayNullText = String.Empty
        Me.INDtxtDocument.Text = String.Empty
        Me.INDmemComment.Text = String.Empty

        Me.INDtxtParticipantName.Text = String.Empty
        Me.INDtxtParticipantPosition.Text = String.Empty
        Me.INDbtnAddParticipant.Tag = "ADD"
        Me.INDrdgParticipantType.EditValue = Nothing
        Me.INDgdcParticipants.DataSource = Nothing

        Me.INDsleSearchInvoice.Text = String.Empty
        Me.INDsleSearchInvoice.Properties.DataSource = Nothing
        Me.INDgdcInvoices.DataSource = New List(Of Domain.Entities.ConciliationD)

        If _ctrTraceabilityControl IsNot Nothing Then
            Me._ctrTraceabilityControl.Visible = False
        End If
        Me.AdditionalControlPanel.Visible = False
        Me.ActionsOnControls = False

        Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloDetalleFactura, Eform.Conciliation)
        Me._currentActiveGroup = EGroups.InvoiceDetails
        Me.INDlycgInvoiceDetailsMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDlycgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDlycgHeader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me._currentActiveGroup = EGroups.Root

        Me._doc = Nothing
        Me._conciliation = New Domain.Entities.ConciliationC()
        Me._listConciliationDetail = New List(Of Domain.Entities.ConciliationD)()

        Me.BarraBotones.HideOptionReport()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.StatusRecordVisible = False

        INDlycRoot.EndUpdate()

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    ''' <summary>
    ''' Limpia los controles del control popUp de participantes
    ''' dejandolo listo para agregar o modificar uno nuevo
    ''' </summary>
    Public Sub CleanParticipantControls()
        Me.INDtxtParticipantName.Text = String.Empty
        Me.INDtxtParticipantPosition.Text = String.Empty
        Me.INDrdgParticipantType.EditValue = Nothing
        Me.INDtxtParticipantName.Focus()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls(Optional ByVal isNew As Boolean = False) As Boolean
        If Me.INDbteNit.EditValue.Trim().Equals(String.Empty) OrElse Convert.ToInt64(Me.INDbteNit.EditValue.Trim()) <= 0 Then
            Me.INDbteNit.Focus()
            Return False
        End If
        If Me.INDtxtDocument.Text.Trim().Equals(String.Empty) Then
            Me.INDtxtDocument.Focus()
            Return False
        End If
        If Me.INDdetDateDocument.Text.Trim().Equals(String.Empty) Then
            Me.INDdetDateDocument.Focus()
            Return False
        End If
        If Me.INDgdvParticipants.RowCount <= 0 Then
            Me.INDpceParticipants.ShowPopup()
            Me.INDtxtParticipantName.Focus()
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Desbloquea el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function DeleteBlockedRecord() As Task
        If _blockRecord IsNot Nothing AndAlso _blockRecord.Id > 0 Then
            Using Model As New MConciliation(Me.Tag)
                Await Model.DeleteBlockRecord(_blockRecord)
            End Using
            _blockRecord = Nothing
        End If
    End Function

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        Dim detailString As String = " [ "
        For Each d As Domain.Entities.ConciliationD In Me._listConciliationDetail
            If detailString.Length = 3 Then
                detailString &= String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.GlosaPortfolioGlosada.PatientCode, d.GlosaPortfolioGlosada.PatientName)
            Else
                detailString &= " - " & String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContentDetail", NAME_MODULE), d.InvoiceNumber.Trim(), d.GlosaPortfolioGlosada.PatientCode, d.GlosaPortfolioGlosada.PatientName)
            End If
        Next
        detailString &= " ]"
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._conciliation.Nit.Trim(), Me._conciliation.NitName.Trim().ToLower(), Me._conciliation.DocumentNumber.Trim(), detailString), .CreationDate = dateServer, .CreationUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName, .DocumentType = IndexedDocumentType.File, .IdEntity = "$#" & Me.Tag & "_" & Me._conciliation.ConciliationConsecutive & "#$", .IdForm = Me.Tag, .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._conciliation.ConciliationConsecutive), .Update = dateServer, .UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = SessionValues.Instance.UserIndigo & "-" & SessionValues.Instance.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me._conciliation.Nit.Trim(), Me._conciliation.NitName.Trim().ToLower(), Me._conciliation.DocumentNumber.Trim(), detailString)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me._conciliation.ConciliationConsecutive)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Prepara el formulario para una nueva conciliacion
    ''' </summary>
    Private Sub NewConciliation()
        Me.INDbteConsecutive.Text = obtenerRecurso(Eresources.NuevaConciliacionLabel, Eform.Conciliation)

        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True

        BarraBotones.StatusRecord = "1"
        BarraBotones.StatusRecordVisible = True
        Me._conciliation = New Domain.Entities.ConciliationC With {.State = "1"}
        Me.ActionsOnControls = True
    End Sub

    ''' <summary>
    ''' Cargar los controles con los datos del objeto Company
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Consecutive) AndAlso Not String.IsNullOrWhiteSpace(Consecutive) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MConciliation(Me.Tag)
                    Me.AsyncLoader(True)
                    INDlycRoot.BeginUpdate()
                    Me._conciliation = Await Model.GetConciliationByConsecutive(Me.INDbteConsecutive.Text.Trim())
                    If Me._conciliation IsNot Nothing AndAlso Me._conciliation.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True
                        _blockRecord = Await Model.GetBlockRecord(Me.Tag, Me._conciliation.Id)
                        With Me._conciliation
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ConfirmationDate"), .ConfirmDate)

                            Me.INDbteNit.DisplayNullText = .NitName
                            Me.DateConciliation = .ConciliationDate
                            Me.DateDocument = .DocumentDate
                            Me.Document = .DocumentNumber
                            Me.Nit = .Nit
                            Me.Comment = .Comment
                            Me.Participants = .ConciliationParticipants
                            Me.BarraBotones.StatusRecord = .State
                            Me.BarraBotones.StatusRecordVisible = True

                            Me._customer = New Domain.Entities.Customer With {.Nit = Me._conciliation.Nit.ToString().Trim(), .Name = Me._conciliation.NitName.ToString().Trim()}
                            Me.INDsleSearchInvoice.Properties.DataSource = Model.ListXpoInvoicesByNitGlosaPortfolio(Me._customer.Nit.Trim())
                            Me._listConciliationDetail = Await Model.ListDetailConciliationById(.Id)
                            Me.SelectedInvoices = Me._listConciliationDetail
                            Me.INDgdcInvoices.RefreshDataSource()
                        End With
                        Me.GetDocumentIndexed(Me.Tag & "_" & Me._conciliation.ConciliationConsecutive)
                        If _blockRecord.Id = 0 Then
                            _blockRecord = (Await Model.SaveBlockRecord(
                                New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _conciliation.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _blockRecord.CodUser, _blockRecord.NameUser, _blockRecord.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _blockRecord.CodUser)
                        End If

                        Me.ActionsOnControls = True
                        Me.INDbteNit.Enabled = False

                        Select Case _conciliation.State
                            Case "1"
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmAnnular)
                            Case Else
                                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        End Select

                        Me.INDbteConsecutive.Focus()
                        Me.BarraBotones.SetDocuments(Me._conciliation.Id)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                        Me.BarraBotones.PrintReport(PrintReportAction.None, Me._conciliation.Id, 0, {Me._conciliation.Id, Me.BarraBotones.OperatingUnit})

                        Me.AsyncLoader(False)
                    Else
                        Me.AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ConciliacionNoExiste, Eform.Conciliation)

                        Consecutive = Nothing
                        Deshacer()
                        Me.INDbteConsecutive.Focus()
                    End If
                    INDlycRoot.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteConsecutive.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Asigna los valores de los controles al objeto Company
    ''' </summary>
    Private Sub AssigningValues()
        With _conciliation
            .Nit = Me.INDbteNit.EditValue
            .NitName = Me._customer.Nit & " - " & Me._customer.Name
            .DocumentNumber = Me.INDtxtDocument.Text.Trim()
            .ConciliationDate = Date.Parse(Me.INDtxtToday.Tag)
            .DocumentDate = Date.Parse(Me.INDdetDateDocument.EditValue)
            .Comment = Me.INDmemComment.Text.Trim()

            If .Id > 0 Then
                .MarkAsModified()
            End If
        End With
    End Sub

    ''' <summary>
    ''' Habilita la edicion en el participante seleccionado en la rejilla
    ''' </summary>
    Private Sub EditParticipant()
        Me.INDbtnAddParticipant.Tag = "EDIT"
        Me.INDbtnAddParticipant.Text = obtenerRecurso(Eresources.TextoBotonModificarParticipante, Eform.Conciliation)
        Me._currentParticipant = CType(Me.INDgdvParticipants.GetRow(Me.INDgdvParticipants.FocusedRowHandle), Domain.Entities.ConciliationParticipants)
        Me.INDtxtParticipantName.Text = Me._currentParticipant.FullName.Trim().ToUpper()
        Me.INDtxtParticipantPosition.Text = Me._currentParticipant.Position.Trim().ToUpper()
        Me.INDrdgParticipantType.EditValue = Me._currentParticipant.Type.Trim()
        Me.INDtxtParticipantName.Focus()
        Me.INDtxtParticipantName.SelectAll()
    End Sub

    ''' <summary>
    ''' Elimina un participante seleccionado
    ''' </summary>
    Private Sub DeleteParticipant()
        If Me._conciliation IsNot Nothing AndAlso Me._conciliation.State <> 2 Then
            If Me.INDgdvParticipants.SelectedRowsCount > 0 Then
                If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminarRegistro, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Dim aux = CType(Me.INDgdvParticipants.GetRow(Me.INDgdvParticipants.FocusedRowHandle), Domain.Entities.ConciliationParticipants)
                    If aux.ChangeTracker.State = ObjectState.Added Then
                        Me._conciliation.ConciliationParticipants.Remove(aux)
                        Me.Participants = Me._conciliation.ConciliationParticipants
                    Else
                        Me._conciliation.ConciliationParticipants.Remove(aux)
                        aux.ChangeTracker.State = ObjectState.Deleted
                        Me._conciliation.ConciliationParticipants.Add(aux)
                        If Me._conciliation.ChangeTracker.State = ObjectState.Unchanged Then
                            Me._conciliation.ChangeTracker.State = ObjectState.Modified
                        End If
                        Me.Participants = Me._conciliation.ConciliationParticipants
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Get la factura ya conciliada con la informacion actualiazada y solo las add 
    ''' </summary>
    Private Async Function GetListConciliationDetailAdded(listConciliationDetailTmp As List(Of ConciliationD)) As Task(Of List(Of ConciliationD))

        Dim ListConciliation = Await _myModel.ListDetailConciliationById(Me._conciliation.Id) ' Obtiene los valores actualizados de la conciliacion como esta guardada en BD

        For Each item In ListConciliation
            If listConciliationDetailTmp.FirstOrDefault(Function(x) x.Id = item.Id).ChangeTracker.State = ObjectState.Deleted Then
                item.ChangeTracker.State = ObjectState.Deleted
            End If
        Next

        Return ListConciliation
    End Function
    ''' <summary>
    ''' Guarda los detalles de la conciliación
    ''' </summary>
    ''' <returns></returns>
    Private _isSavingConciliationDetail As Boolean = False

    Private Async Function SavelistConciliationDetail() As Task(Of ActionResult)
        ' Evita reentradas del save
        If _isSavingConciliationDetail Then
            Return New ActionResult With {.StateResult = False, .Message = "El guardado de detalle de conciliación ya está en curso."}
        End If
        _isSavingConciliationDetail = True
        Try

        If (From d In Me._listConciliationDetail Where d.ChangeTracker.State = ObjectState.Added Or d.ChangeTracker.State = ObjectState.Deleted Select d).Any Then
            Using model As New MConciliation(Me.Tag)
                For Each detail As Domain.Entities.ConciliationD In (From d As Domain.Entities.ConciliationD In Me._listConciliationDetail Where d.ChangeTracker.State = ObjectState.Added Select d).ToList()
                    detail.ConciliationCId = Me._conciliation.Id
                    detail.State = "1"
                Next

                    ' Validacion anti-duplicados
                    If Me._conciliation IsNot Nothing AndAlso Me._conciliation.Id > 0 Then
                    Dim persisted As List(Of Domain.Entities.ConciliationD) = Await model.ListDetailConciliationById(Me._conciliation.Id)
                    Dim persistedActiveInvoices As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                    If persisted IsNot Nothing Then
                        For Each p In persisted
                            If p.State IsNot Nothing AndAlso p.State.Trim() = "1" AndAlso Not String.IsNullOrWhiteSpace(p.InvoiceNumber) Then
                                persistedActiveInvoices.Add(p.InvoiceNumber.Trim())
                            End If
                        Next
                    End If

                    Dim seenInBatch As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                    Dim duplicatesRemoved As New List(Of String)
                    For Each item As Domain.Entities.ConciliationD In Me._listConciliationDetail.Where(Function(d) d.ChangeTracker.State = ObjectState.Added).ToList()
                        Dim invoice As String = If(item.InvoiceNumber, String.Empty).Trim()
                        If invoice.Length = 0 Then Continue For
                        If persistedActiveInvoices.Contains(invoice) OrElse Not seenInBatch.Add(invoice) Then
                            Me._listConciliationDetail.Remove(item)
                            duplicatesRemoved.Add(invoice)
                        End If
                    Next

                    If duplicatesRemoved.Count > 0 Then
                        Dim sample As String = String.Join(", ", duplicatesRemoved.Take(10))
                        If duplicatesRemoved.Count > 10 Then sample &= "..."
                        Me.Mensaje(EeventViewerImages.Advertencia) = "Se detectaron " & duplicatesRemoved.Count.ToString() &
                            " factura(s) duplicada(s) que ya existen en la conciliación. No se guardaron: " & sample
                        Me.SelectedInvoices = Me._listConciliationDetail
                        Me.INDgdcInvoices.RefreshDataSource()
                        Return New ActionResult With {.StateResult = False, .Message = "Existen facturas duplicadas; la conciliación no fue guardada."}
                    End If
                End If

                Dim result = Await model.SaveConciliationD(Me._listConciliationDetail)
                If result.StateResult Then
                    Me._listConciliationDetail = Await model.ListDetailConciliationById(Me._conciliation.Id)
                    Me.SelectedInvoices = Me._listConciliationDetail
                    Me.INDgdcInvoices.RefreshDataSource()

                    Return New ActionResult With {.StateResult = True}
                Else
                    Return New ActionResult With {.StateResult = False, .Message = result.Message}
                End If
            End Using
        End If

        Return New ActionResult With {.StateResult = True}
        Finally
            _isSavingConciliationDetail = False
        End Try
    End Function

#Region "Copy & Paste"

    ''' <summary>
    ''' Pega en la rejilla de facturas los datos de la clipboard
    ''' </summary>
    Private Async Sub PasteToGridInvoices()
        If Me.INDbteNit IsNot Nothing AndAlso Not Me.INDbteNit.EditValue.Trim().Equals(String.Empty) Then
            If Me._conciliation.State = "1" Then
                Dim list As List(Of String) = Me.GetInvoiceFromClipboard()
                If list.Count > 0 Then
                    Using model As New MConciliation(Me.Tag)
                        Me.AsyncLoader(True)
                        Dim listInvoice = Await model.ListInvoiceByNitAndListData(Me.INDbteNit.EditValue.Trim(), list)
                        If listInvoice IsNot Nothing AndAlso listInvoice.Count > 0 Then
                            Me._listConciliationDetail.AddRange(listInvoice)
                            Me.SelectedInvoices = Me._listConciliationDetail
                            Me.INDsleSearchInvoice.EditValue = Nothing
                        End If
                        Me.AsyncLoader(False)
                    End Using
                    Me.INDsleSearchInvoice.Focus()
                End If
            End If
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.DebeSeleccionarTercero, Eform.Conciliation)
        End If
    End Sub

    ''' <summary>
    ''' Obtiene una lista de numeros de facturas
    ''' almacenada en la Clipboard
    ''' </summary>
    ''' <returns>Lista de numeros de facturas</returns>
    Private Function GetInvoiceFromClipboard() As List(Of String)
        Dim list As New List(Of String)()
        If Clipboard.GetText IsNot Nothing AndAlso Not Clipboard.GetText.Trim().Equals(String.Empty) Then
            For Each line As String In Clipboard.GetText.Split(vbNewLine)
                Dim item() As String = line.Trim.Split(vbTab)
                If item.Length >= 1 Then
                    If Me.IsValidString(item(0).Trim()) Then
                        If Not item(0).Trim().Equals(String.Empty) AndAlso Not (From i As String In list Where i.Trim() = item(0).Trim() Select i).Any AndAlso Not (From f As Domain.Entities.ConciliationD In Me._listConciliationDetail Where f.InvoiceNumber = item(0).Trim() Select f).Any Then
                            list.Add(item(0).Trim())
                        End If
                    End If
                Else
                    Exit For
                End If
            Next
        End If
        Return list
    End Function

    ''' <summary>
    ''' Valida si una cadena de caracteres contiene caracteres validos
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
    ''' Función para crear el objeto openFileDialog
    ''' </summary>
    Private Async Sub GetOpenFileDialog()
        _fileOpener.CheckPathExists = True
        _fileOpener.CheckFileExists = True
        _fileOpener.Filter = "Excel Files(.xlsx)|*.xlsx| Excel Files(.xls)|*.xls| " &
                             "Excel Files(*.xlsm)|*.xlsm"
        _fileOpener.Multiselect = False
        _fileOpener.AddExtension = True
        _fileOpener.ValidateNames = True
        'fileOpener.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        _fileOpener.InitialDirectory = Infrastructure.CrossCutting.Base.Window.Utils.DeskTopFolder()
        If (_fileOpener.ShowDialog(Me) = DialogResult.OK) Then
            If _fileOpener.FileName <> String.Empty Then
                Dim fileInfo = New System.IO.FileInfo(_fileOpener.FileName)
                _rutaExcel = _fileOpener.FileName
                Using FrmLoad As New FrmLoadexcel
                    FrmLoad.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                    FrmLoad.PathFile = _rutaExcel
                    FrmLoad.Size = New System.Drawing.Size(800, 600)
                    FrmLoad.ModuleName = "CON"
                    AssigningValues()
                    FrmLoad.ConciliationC = Me._conciliation
                    FrmLoad.ShowDialog()
                    If FrmLoad.ConciliationC.ConciliationConsecutive > 0 Then
                        Me.INDbteConsecutive.Text = FrmLoad.ConciliationC.ConciliationConsecutive
                        Await LoadControls()
                    End If
                End Using
            End If
        End If
    End Sub

#End Region

    ''' <summary>
    ''' Elimina una factura seleccionado
    ''' </summary>
    Private Sub DeleteInvoice()
        If Me.INDgdvInvoices.SelectedRowsCount > 0 Then
            Dim aux As Domain.Entities.ConciliationD = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD)
            If aux.State.Trim() <> "2" Then
                If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesEliminarRegistro, Eform.Comunes), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    If aux.ChangeTracker.State = ObjectState.Added Then
                        Me._listConciliationDetail.Remove(aux)
                        Me.SelectedInvoices = Me._listConciliationDetail
                    Else 'Eliminado
                        Me._listConciliationDetail.Remove(aux)
                        aux.ChangeTracker.State = ObjectState.Deleted
                        Me._listConciliationDetail.Add(aux)
                        Me.SelectedInvoices = Me._listConciliationDetail
                    End If
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Carga y muestra los detalles de la factura seleccionada en la rejilla
    ''' </summary>
    Private Async Sub LoadAndDisplayInvoiceDetails()
        'Aqui se carga el control de tiempo
        Me.LoadXtraTrackControl()
        If Me.INDgdvInvoices.SelectedRowsCount > 0 Then
            _conciliationDetail = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD)
            Dim details As New List(Of Domain.Entities.GlosaInvoiceDetail)()
            If _conciliationDetail.ChangeTracker.State <> ObjectState.Added Then
                'Aqui la logica de para cargar los detalles de factura
                Me.INDlblInvoiceNumber.Text = _conciliationDetail.InvoiceNumber
                Me.INDlblEntryNumber.Text = _conciliationDetail.GlosaPortfolioGlosada.IngressNumber
                Me.INDlblPatient.Text = _conciliationDetail.GlosaPortfolioGlosada.PatientName
                Me.BarraBotones.StatusRecord = ("-" & _conciliationDetail.State.Trim())

                If Me.BarraBotones.StatusRecord = "-1" Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
                    Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = True
                    Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = True
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = False
                    Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = False
                End If

                RefreshInformation()
                Me._ctrTraceabilityControl.Visible = True
                Me._ctrTraceabilityControl.Invoice = _conciliationDetail.InvoiceNumber
                Dim customerTrazability = Await Me._myModel.GetCustomerByNit(_conciliationDetail.ConciliationC.Nit)
                Me._ctrTraceabilityControl.Entity = customerTrazability.Id
                Me._ctrTraceabilityControl.CargarControl()
                Me.AdditionalControlPanel.Visible = True
                '==========================================================================

                Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

                Me.INDlycgInvoiceDetailsMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloDetalleFactura, Eform.Conciliation)
                Me.INDlycgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgHeader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me._currentActiveGroup = EGroups.InvoiceDetails
            Else
                'Se da un feedback al usuario
                Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FacturaSinGuardar, Eform.Conciliation)
            End If
        End If
        ExpandDetailQx()
    End Sub

    Private Sub ExpandDetailQx()
        If Me.INDgdcInvoiceDetails.DataSource IsNot Nothing Then
            For i As Integer = 0 To Me.INDgdcInvoiceDetails.DataSource.count - 1
                Me.INDgdvInvoiceDetails.ExpandMasterRow(i, "GlosaInvoiceDetailQX")
            Next
        End If
    End Sub

    ''' <summary>
    ''' Carga y muestra los datos del detalle a conciliar
    ''' </summary>
    Private Sub ConciliateDetail()
        'Cargar los datos aqui
        Dim aux = TryCast(Me.INDgdvInvoiceDetails.GetRow(Me.INDgdvInvoiceDetails.FocusedRowHandle), Domain.Entities.GlosaInvoiceDetail)
        If aux IsNot Nothing Then 'ColConsAceptEAPB
            If _conciliationDetail IsNot Nothing Then
                If _conciliationDetail.State = "1" Then
                    If aux.StateRecord = "3" Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = False
                        Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = False
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.Save)
                        Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = True
                        Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = True
                    End If
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = False
                    Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = False
                End If
            End If
            Me.ColConsServiceCode.FieldName = "GlosaInvoiceDetail.ServiceCode"
            Me.ColConsServiceName.FieldName = "GlosaInvoiceDetail.ServiceName"
            Me.INDgdcConciliateInvoiceDetail.DataSource = aux.GlosaMovementGlosa.ToList
            Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDlbMaximunValuePending.Text = ManageDecimalsFun(aux.ValuePendingConciliationTmpFacade)
            Me._valueConstMaximoPending = aux.ValuePendingConciliationTmpFacade
            Me._valControlNew = aux.ValuePendingConciliationTmpFacade
            Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloConciliacionDetalleFactura, Eform.Conciliation)
            Me._currentActiveGroup = EGroups.ConciliateInvoiceDetail
            Me.INDgdvConciliateInvoiceDetail.Focus()
            Me.INDgdvConciliateInvoiceDetail.FocusedColumn = Me.ColConsValAceptIPS
            Me.INDgdvConciliateInvoiceDetail.FocusedRowHandle = 0
            Me.INDgdvConciliateInvoiceDetail.ShowEditor()
        End If
        '  End If
    End Sub

    ''' <summary>
    ''' Carga y muestra los datos del detalle Qx a conciliar
    ''' </summary>
    Private Sub ConciliateDetailQx()
        'Cargar los datos aqui
        Dim detailQx As GridView = Me.INDgdcInvoiceDetails.FocusedView
        Dim aux = TryCast(detailQx.GetRow(detailQx.FocusedRowHandle), Domain.Entities.GlosaInvoiceDetailQX)
        If aux IsNot Nothing Then
            If _conciliationDetail IsNot Nothing Then
                If _conciliationDetail.State = "1" Then
                    If aux.StateRecord = "3" Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = False
                        Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = False
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.Save)
                        Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = True
                        Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = True
                    End If
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Me.ColConsValAceptIPS.OptionsColumn.AllowEdit = False
                    Me.ColConsAceptEAPB.OptionsColumn.AllowEdit = False
                End If
            End If
            Me.ColConsServiceCode.FieldName = "GlosaInvoiceDetailQX.ServiceCode"
            Me.ColConsServiceName.FieldName = "GlosaInvoiceDetailQX.ServiceName"
            Me.INDgdcConciliateInvoiceDetail.DataSource = (From m As Domain.Entities.GlosaMovementGlosa In aux.GlosaInvoiceDetail.GlosaMovementGlosa Where m.InvoiceDetailIdQX = aux.Id And m.MainGlosa = True Select m).ToList()
            Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Me.INDlbMaximunValuePending.Text = ManageDecimalsFun(aux.ValuePendingConciliationTmpFacade)
            Me._valueConstMaximoPending = aux.ValuePendingConciliationTmpFacade
            Me._valControlNew = aux.ValuePendingConciliationTmpFacade
            Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloConciliacionDetalleFactura, Eform.Conciliation)
            Me._currentActiveGroup = EGroups.ConciliateInvoiceDetail
        End If
        'End If
    End Sub

    ''' <summary>
    ''' Actualiza los datos mostrados en los labels de valores de la factura en la conciliacion
    ''' </summary>
    Private Async Sub RefreshInformation()
        Dim aux As Domain.Entities.ConciliationD = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD)
        If aux IsNot Nothing Then
            AsyncLoader(True)
            Dim Details = Await _myModel.ListGlosaInvoiceDetails(aux.InvoiceNumber, "CON", Me._conciliation.Id)
            Me.INDgdcInvoiceDetails.DataSource = Details
            AsyncLoader(False)
        End If

        valuePending = 0
        Dim valuePartialPayments As Decimal
        Dim ObjectionValue As Decimal
        Dim FirstInstanceAceptedValue As Decimal
        Dim SecondInstanceAceptedValue As Decimal
        Dim valueIPSconciliation As Decimal
        Dim valueEAPBconciliation As Decimal
        Dim valuereiterated As Decimal

        'Cargamos la estructura con la sumatoria de los datos totales de la factura para mostrar.
        'También se calcula el estado que tendra el detalle de la factura dependiendo del estado
        'que tengan sus movimientos
        For Each d As Domain.Entities.GlosaInvoiceDetail In CType(Me.INDgdcInvoiceDetails.DataSource, List(Of Domain.Entities.GlosaInvoiceDetail))
            ObjectionValue += d.CalculatevalueGlosado()
            FirstInstanceAceptedValue += d.CalculateAcceptedFirtsInstance()
            valuereiterated += d.CalculatevalueReiterated()
            SecondInstanceAceptedValue += d.CalculateAcceptedIPSSecondInstance()
            valueIPSconciliation += d.ValueAcceptedIPSconciliationFacade
            valueEAPBconciliation += d.ValueAcceptedEAPBconciliationFacade
            valuePartialPayments += d.CalculatePartialPayments()
            valuePending += d.ValuePendingConciliationFacade

            If d.GlosaInvoiceDetailQX IsNot Nothing AndAlso d.GlosaInvoiceDetailQX.Count > 0 Then
                If d.ValueAcceptedEAPBconciliationFacade = 0 And d.ValueAcceptedIPSconciliationFacade = 0 Then
                    d.StateRecord = 2  'azul
                ElseIf _conciliationDetail.State = 2 And d.ValuePendingConciliationFacade = 0 Then
                    d.StateRecord = 3  'verde
                ElseIf _conciliationDetail.State = 1 And d.ValuePendingConciliationFacade = 0 Then
                    d.StateRecord = IIf((From m As Domain.Entities.GlosaMovementGlosa In d.GlosaMovementGlosa Where m.MainGlosa = True And m.State = 6 Select m).Any, 3, 1)
                ElseIf _conciliationDetail.State = 1 And d.ValuePendingConciliationFacade > 0 Then
                    d.StateRecord = 1 'naranja
                End If

                For Each qx As Domain.Entities.GlosaInvoiceDetailQX In d.GlosaInvoiceDetailQX
                    'para marcar estado en los QX
                    If qx.ValueAcceptedEAPBconciliationFacade = 0 And qx.ValueAcceptedIPSconciliationFacade = 0 Then
                        qx.StateRecord = 2  'azul
                    ElseIf _conciliationDetail.State = 2 And qx.ValuePendingConciliationFacade = 0 Then
                        qx.StateRecord = 3  'verde
                    ElseIf _conciliationDetail.State = 1 And qx.ValuePendingConciliationFacade = 0 Then
                        qx.StateRecord = IIf((From m As Domain.Entities.GlosaMovementGlosa In qx.GlosaMovementGlosa Where m.MainGlosa = True And m.State = 6 Select m).Any, 3, 1)
                    ElseIf _conciliationDetail.State = 1 And qx.ValuePendingConciliationFacade > 0 Then
                        qx.StateRecord = 1 'naranja
                    End If
                Next
            Else
                If d.ValueAcceptedEAPBconciliationFacade = 0 And d.ValueAcceptedIPSconciliationFacade = 0 Then
                    d.StateRecord = 2  'azul
                ElseIf _conciliationDetail.State = 2 And d.ValuePendingConciliationFacade = 0 Then
                    d.StateRecord = 3  'verde
                ElseIf _conciliationDetail.State = 1 And d.ValuePendingConciliationFacade = 0 Then
                    d.StateRecord = IIf((From m As Domain.Entities.GlosaMovementGlosa In d.GlosaMovementGlosa Where m.MainGlosa = True And m.State = 6 Select m).Any, 3, 1) 'naranja o verde
                ElseIf _conciliationDetail.State = 1 And d.ValuePendingConciliationFacade > 0 Then
                    d.StateRecord = 1 'naranja
                End If
            End If
        Next
        Me.INDlblValGlosa.Text = ManageDecimalsFun(ObjectionValue)
        Me.INDlblValAceptIPSGlosa.Text = ManageDecimalsFun(FirstInstanceAceptedValue)
        Me.INDlblValReite.Text = ManageDecimalsFun(valuereiterated)
        Me.INDlblValAceptReite.Text = ManageDecimalsFun(SecondInstanceAceptedValue)
        Me.INDlblValAceptIPSCons.Text = ManageDecimalsFun(valueIPSconciliation)
        Me.INDlblValPartialPayments.Text = ManageDecimalsFun(valuePartialPayments)
        Me.INDlblValAceptEAPB.Text = ManageDecimalsFun(valueEAPBconciliation)
        Me.INDlblValPending.Text = ManageDecimalsFun(valuePending)
        Me.INDlbMaximunValuePending.Text = ManageDecimalsFun(valuePending)
        valuePartialPayments = ManageDecimalsFun(valuePartialPayments)
        Me.INDgdcInvoiceDetails.RefreshDataSource()
        ExpandDetailQx()
    End Sub

    ''' <summary>
    ''' Funcion para cada vez que asignen un valor a los label's (Manjo de decimales)
    ''' </summary>
    ''' <param name="Value"></param>
    ''' <returns></returns>
    Private Function ManageDecimalsFun(Value As Decimal)
        If _parameterGlosas.ManageDecimals = False Then
            Return Value.MoneyFormat(numberDecimal:=0)
        Else
            Return Value.MoneyFormat
        End If
    End Function

    Async Sub ShowGeneralInvoiceMovements()
        Dim listaInvoice = New List(Of String)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing
        view = Me.INDgdcInvoices.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim ObjD As ConciliationD = TryCast(view.GetRow(item), ConciliationD)
                If ObjD IsNot Nothing And ObjD.State = 1 Then
                    listaInvoice.Add(ObjD.InvoiceNumber)
                End If
            End If
        Next
        If listaInvoice.Count > 0 Then
            Dim PermisoGuardar As Boolean
            If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Guardar) = True Then
                PermisoGuardar = True
            End If
            Using generalEvaluation As GeneralConciliation = New GeneralConciliation(_myModel, PermisoGuardar, _idOperativeUnit, _conciliation.Id, listaInvoice, Nothing)
                generalEvaluation.Size = New System.Drawing.Size(532, 596)
                generalEvaluation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim result = generalEvaluation.ShowDialog(Me)
                If result = System.Windows.Forms.DialogResult.OK Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado, Eform.Comunes)
                End If
                Me.AsyncLoader(True)
                Me._listConciliationDetail = Await _myModel.ListDetailConciliationById(Me._conciliation.Id)
                Me.SelectedInvoices = Me._listConciliationDetail
                Me.INDgdcInvoices.RefreshDataSource()
                Me.AsyncLoader(False)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para disparar el formulario de evaluacion general.
    ''' </summary>
    Sub ShowGeneralMovements()
        Dim lista = New List(Of GlosaInvoiceDetail)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

        view = Me.INDgdcInvoiceDetails.FocusedView
        For Each item As Integer In view.GetSelectedRows()
            If item > -1 Then
                Dim data As GlosaInvoiceDetail = TryCast(view.GetRow(item), Domain.Entities.GlosaInvoiceDetail)
                If data IsNot Nothing AndAlso data.StateRecord <> 3 Then
                    lista.Add(data)
                End If
            End If
        Next

        If lista.Count > 0 Then
            Dim PermisoGuardar As Boolean
            If Me.BarraBotones.PermissionsForm.ContainsKey(PermissionsActionsForm.Guardar) = True Then
                PermisoGuardar = True
            End If
            Using generalEvaluation As GeneralConciliation = New GeneralConciliation(_myModel, PermisoGuardar, _idOperativeUnit, _conciliation.Id, Nothing, lista)
                generalEvaluation.Size = New System.Drawing.Size(532, 596)
                generalEvaluation.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim result = generalEvaluation.ShowDialog(Me)
                If result = System.Windows.Forms.DialogResult.OK Then
                    If Me.INDlblInvoiceNumber.Text <> String.Empty Then
                        RefreshInformation()
                    End If
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(Eresources.ComunesGuardado, Eform.Comunes)
                End If
            End Using
        End If
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    ''' <summary>
    ''' Inicializa la vista del frontal de conciliacion
    ''' </summary>
    Private Async Sub FrmConciliation_Load(sender As Object, e As EventArgs) Handles Me.Load
        '******Inicializar variables******'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me._myModel = New MConciliation(Me.Tag)
        Me._presenter = New PConciliation(Me)
        '*********************************?
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me.INDbteNit.View.OptionsView.ShowGroupPanel = False
        Me.INDbteNit.FuncQueryOnKeyEnterPressed = AddressOf Me.INDbteNit_KeyDown
        Me.INDgleResponseHierarchy.View.OptionsView.ShowGroupPanel = False
        '*********************************?
        Me._searchMode = False
        Me._currentActiveGroup = EGroups.Root
        '*********************************?
        Me.INDdetDateDocument.Properties.MaxValue = Date.Today
        Me.INDlyiConceptsAndComments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDbtnAddParticipant.Text = obtenerRecurso(Eresources.TextoBotonAgregarParticipante, Eform.Conciliation)
        Me.INDnvbInvoiceDetails.Group = Me.INDlycgInvoiceDetails

        Me.IndigoGridControl1.SetHoldSize(Me.INDgdcInvoices, True)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgdcInvoices, True)
        Me.IndigoGridControl1.SetHoldSize(Me.INDgdcConciliateInvoiceDetail, True)
        Me.IndigoGridControl1.SetHotTrack(Me.INDgdcConciliateInvoiceDetail, True)

        AsyncLoader(True)
        Await LoadParameters()
        AsyncLoader(False)

        Me.Deshacer()
        Me.LoadStatus()
    End Sub

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _myModel = Nothing
        _presenter = Nothing
        _idOperativeUnit = Nothing
        _blockRecord = Nothing
        _conciliation = Nothing
        _customer = Nothing
        _currentParticipant = Nothing
        _listConciliationDetail = Nothing
        _conciliationDetail = Nothing

        _fileOpener = Nothing
        _rutaExcel = Nothing
        _valueConstMaximoPending = Nothing
        _valControlNew = Nothing
        _currentActiveGroup = Nothing
        valuePending = Nothing

        _searchMode = Nothing
        _openFindSenser = Nothing

        DatasourceCustomers = Nothing
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmConciliation_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDbteConsecutive.Focus()
    End Sub

#End Region

#Region "Closed"

    ''' <summary>
    ''' Capturamos el comentario y concepto de aceptación 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceCommentsAndConcepts_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDpceCommentsAndConcepts.Closed
        Dim row As GlosaMovementGlosa = Me.INDgdvConciliateInvoiceDetail.GetRow(Me.INDgdvConciliateInvoiceDetail.FocusedRowHandle)
        If INDtxtCommentsConciliation.Text <> String.Empty Then
            row.RationaleConciliation = INDtxtCommentsConciliation.Text
        End If
        If IdResponseHierarchy IsNot Nothing Then
            row.IdResponseHierarchyConciliation = IdResponseHierarchy
        End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Aqui se desbloquea el registro
    ''' </summary>
    Private Async Sub FrmConciliation_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Await Me.DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Permite la busqueda de una conciliacion por su numero de consecutivo
    ''' o la creación de una nueva si no se proporciona uno.
    ''' </summary>
    Private Async Sub INDbteConsecutive_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteConsecutive.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If Not String.IsNullOrEmpty(Me.INDbteConsecutive.Text.Trim()) AndAlso Not String.IsNullOrWhiteSpace(Me.INDbteConsecutive.Text.Trim()) AndAlso Not Me.INDbteConsecutive.Text.Trim().Equals("") AndAlso Convert.ToInt64(Me.INDbteConsecutive.Text.Trim()) > 0 Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ComunesNoTienePermisos, Eform.Comunes)
                    Exit Sub
                End If
                Await Me.LoadControls() 'Consultamos el consecutivo
            Else
                Me.NewConciliation()
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Consulta un cliente por su nit
    ''' </summary>
    Private Async Function INDbteNit_KeyDown(ByVal Nit As String) As Task(Of Domain.Entities.Customer)
        Dim ctomer As Domain.Entities.Customer = Nothing
        If Not String.IsNullOrEmpty(Nit) AndAlso Not String.IsNullOrWhiteSpace(Nit.Trim()) Then
            ctomer = Await _myModel.GetCustomerByNit(Nit)
            If ctomer.Name IsNot Nothing AndAlso Not ctomer.Name.Trim().Equals(String.Empty) Then
                Me._customer = ctomer
                If Me._customer IsNot Nothing Then
                    Me.INDsleSearchInvoice.Properties.DataSource = _myModel.ListXpoInvoicesByNitGlosaPortfolio(Me._customer.Nit.Trim())
                Else
                    Me.INDbteNit.Focus()
                End If
                Me.INDdetDateDocument.Focus()
            Else
                'Mensaje, El cliente no existe
                If Me._customer Is Nothing Then
                    Me.INDbteNit.DisplayNullText = String.Empty
                    Me.INDbteNit.EditValue = Nothing
                Else
                    Me.INDbteNit.EditValue = Me._customer.Nit.Trim()
                End If
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.ClienteNoExite, Eform.Conciliation)
            End If
        End If
        Return ctomer
    End Function

    ''' <summary>
    ''' Abre el popUp del comentario al precionar enter o la tecla de espacio
    ''' </summary>
    Private Sub INDpceComment_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceComment.KeyDown
        If e.KeyCode.Equals(System.Windows.Forms.Keys.Enter) OrElse e.KeyCode.Equals(System.Windows.Forms.Keys.Space) Then
            Me.INDpceComment.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Abre el popUp de participantes al precionar enter o la tecla de espacio
    ''' </summary>
    Private Sub INDpceParticipants_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceParticipants.KeyDown
        If e.KeyCode.Equals(System.Windows.Forms.Keys.Enter) OrElse e.KeyCode.Equals(System.Windows.Forms.Keys.Space) Then
            Me.INDpceParticipants.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se implementa la funcionalidad de pegar facturas desde la clipboard
    ''' </summary>
    Private Sub INDgdcInvoices_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDgdcInvoices.KeyDown
        If e.Control AndAlso e.KeyCode = Keys.V Then
            Me.PasteToGridInvoices()
        End If
    End Sub

#End Region

#Region "GotFocus"

    ''' <summary>
    ''' Asigna el nombre del sender a la variable _openFindSender para habilitar
    ''' la funcionalidad de buscar en el control que corresponda
    ''' </summary>
    Private Sub OpenFindSender_GotFocus(sender As Object, e As EventArgs) Handles INDbteConsecutive.GotFocus, INDsleSearchInvoice.GotFocus
        Me._openFindSenser = CType(sender, System.Windows.Forms.Control).Name.ToUpper()
    End Sub

#End Region

#Region "QueryPopup"

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

    ''' <summary>
    ''' determina si deb o no solicitar conceptos jerarquícos de Aceptación
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceCommentsAndConcepts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDpceCommentsAndConcepts.QueryPopUp
        Dim row As GlosaMovementGlosa = Me.INDgdvConciliateInvoiceDetail.GetRow(Me.INDgdvConciliateInvoiceDetail.FocusedRowHandle)
        INDtxtCommentsConciliation.Text = String.Empty
        If row.RationaleConciliation IsNot Nothing Then
            INDtxtCommentsConciliation.Text = row.RationaleConciliation
        End If
        If row.ValPendingIPSconciliation <> 0 Then
            If row.IdResponseHierarchyConciliation IsNot Nothing Then
                Dim responseHierarchy = _myModel.GetResponseHierarchyById(row.IdResponseHierarchyConciliation)
                IdResponseHierarchy = row.IdResponseHierarchyConciliation
                INDgleResponseHierarchy.DisplayNullText = responseHierarchy.CodeName
            End If

            INDlyiConceptsAndComments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDgleResponseHierarchy.Focus()
        Else
            INDlyiConceptsAndComments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            IdResponseHierarchy = Nothing
        End If
        Me.INDgleResponseHierarchy.View.OptionsView.ShowGroupPanel = False
    End Sub

    Private Sub INDgleResponseHierarchy_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDgleResponseHierarchy.QueryPopUp
        If DatasourceResponseHierarchy Is Nothing Then
            DatasourceResponseHierarchy = _myModel.ListResponseHierarchy()
        End If
    End Sub

    Private Sub INDrepPopUpmoreInfo_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPopUpmoreInfo.QueryPopUp
        Dim data As Domain.Entities.ConciliationD = CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD)

        Me.CtrXtraInfo1.ListProperties.Clear()
        If data.GlosaPortfolioGlosada.ValueAcceptedEAPBconciliation.HasValue Then
            Me.CtrXtraInfo1.ListProperties.Add(New XtraInfoProperty("Id Paciente", data.GlosaPortfolioGlosada.PatientCode))
        Else
            Me.CtrXtraInfo1.ListProperties.Add(New XtraInfoProperty("Id Paciente", ""))
        End If

        If data.GlosaPortfolioGlosada.ValueAcceptedIPSconciliation.HasValue Then
            Me.CtrXtraInfo1.ListProperties.Add(New XtraInfoProperty("Paciente", data.GlosaPortfolioGlosada.PatientName))
        Else
            Me.CtrXtraInfo1.ListProperties.Add(New XtraInfoProperty("Paciente", ""))
        End If

        Me.CtrXtraInfo1.ListProperties.Add(New XtraInfoProperty("Numero Ingreso", data.GlosaPortfolioGlosada.IngressNumber.ToString))
        Me.CtrXtraInfo1.ListProperties.Add(New XtraInfoProperty("Nombre Usuario", data.GlosaPortfolioGlosada.UserNameInvoice.ToString))
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Posiciona el foco en el primer campo del popUp
    ''' </summary>
    Private Sub INDpceComment_Popup(sender As Object, e As EventArgs) Handles INDpceComment.Popup
        Me.INDmemComment.Focus()
    End Sub

    ''' <summary>
    ''' Posiciona el foco en el primer campo del popUp
    ''' </summary>
    Private Sub INDpceParticipants_Popup(sender As Object, e As EventArgs) Handles INDpceParticipants.Popup
        Me.INDtxtParticipantName.Focus()
    End Sub

    ''' <summary>
    ''' Aqui se controla que no se despliqegue el popup de la columna de acciones
    ''' cuando el detalle tiene detalles Qx
    ''' </summary>
    Private Sub INDrepPopUpDetailActions_Popup(sender As Object, e As EventArgs) Handles INDrepPopUpDetailActions.Popup
        Dim aux = TryCast(Me.INDgdvInvoiceDetails.GetRow(Me.INDgdvInvoiceDetails.FocusedRowHandle), Domain.Entities.GlosaInvoiceDetail)
        If aux IsNot Nothing Then
            If aux.GlosaInvoiceDetailQX IsNot Nothing AndAlso aux.GlosaInvoiceDetailQX.Count > 0 Then
                CType(sender, PopupContainerEdit).ClosePopup()
                Me.INDgdvInvoiceDetails.SetMasterRowExpanded(Me.INDgdvInvoiceDetails.FocusedRowHandle, Not Me.INDgdvInvoiceDetails.GetMasterRowExpanded(Me.INDgdvInvoiceDetails.FocusedRowHandle))
            End If
        End If
    End Sub

#End Region

#Region "PopupMenuShowing"

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgdvParticipants_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgdvParticipants.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()
        e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuEditarParticipante, Eform.Conciliation), AddressOf EditParticipant, My.Resources.modificarLineaAzul))
        e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuEliminarParticipante, Eform.Conciliation), AddressOf DeleteParticipant, My.Resources.eliminarLineaAzul))
    End Sub

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgdvInvoices_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgdvInvoices.PopupMenuShowing
        If Me.INDgdvInvoices.SelectedRowsCount > 0 AndAlso Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle) IsNot Nothing AndAlso (CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD).ChangeTracker.State = ObjectState.Unchanged Or CType(Me.INDgdvInvoices.GetRow(Me.INDgdvInvoices.FocusedRowHandle), Domain.Entities.ConciliationD).ChangeTracker.State = ObjectState.Modified) Then
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            If Me._conciliation.State = "1" Then
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuPegarFacturaConciliacion, Eform.Conciliation), AddressOf PasteToGridInvoices, My.Resources.pegar32))
            End If

            Dim lista = New List(Of String)
            Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

            view = Me.INDgdcInvoices.FocusedView
            For Each item As Integer In view.GetSelectedRows()
                If item > -1 Then
                    Dim ObjD As ConciliationD = TryCast(view.GetRow(item), ConciliationD)
                    If ObjD IsNot Nothing And ObjD.State = 1 Then 'todos sin confirmar
                        lista.Add(ObjD.InvoiceNumber)
                    End If
                End If
            Next
            If lista.Count > 1 Then
                e.Menu.Items.Add(New DXMenuItem("Conciliación General", AddressOf ShowGeneralInvoiceMovements, My.Resources.modificarLineaAzul))
            Else
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuConciliarFactura, Eform.Conciliation), AddressOf LoadAndDisplayInvoiceDetails, My.Resources.modificarLineaAzul))
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuEliminarFacturaConciliacion, Eform.Conciliation), AddressOf DeleteInvoice, My.Resources.eliminarLineaAzul))
            End If

        Else
            If e.Menu Is Nothing Then
                Exit Sub
            End If
            e.Menu.Items.Clear()
            If Me._conciliation.State = "1" Then
                e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuPegarFacturaConciliacion, Eform.Conciliation), AddressOf PasteToGridInvoices, My.Resources.pegar32))
            End If
        End If
    End Sub

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgdvInvoiceDetails_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgdvInvoiceDetails.PopupMenuShowing
        If Me.INDgdvInvoiceDetails IsNot Nothing And Me.INDgdvInvoiceDetails.FocusedRowHandle > 0 Then
            If Not Me.INDgdvInvoiceDetails.IsGroupRow(Me.INDgdvInvoiceDetails.FocusedRowHandle) AndAlso TryCast(Me.INDgdvInvoiceDetails.GetRow(Me.INDgdvInvoiceDetails.FocusedRowHandle), Domain.Entities.GlosaInvoiceDetail).GlosaInvoiceDetailQX IsNot Nothing AndAlso Not TryCast(Me.INDgdvInvoiceDetails.GetRow(Me.INDgdvInvoiceDetails.FocusedRowHandle), Domain.Entities.GlosaInvoiceDetail).GlosaInvoiceDetailQX.Count > 0 Then
                If e.Menu Is Nothing Then
                    Exit Sub
                End If

                Dim lista = New List(Of GlosaInvoiceDetail)
                Dim view As DevExpress.XtraGrid.Views.Grid.GridView = Nothing

                view = Me.INDgdcInvoiceDetails.FocusedView
                For Each item As Integer In view.GetSelectedRows()
                    If item > -1 Then
                        Dim ObjD As GlosaInvoiceDetail = TryCast(view.GetRow(item), GlosaInvoiceDetail)
                        If ObjD IsNot Nothing AndAlso ObjD.StateRecord <> 3 Then
                            lista.Add(ObjD)
                        End If
                    End If
                Next

                e.Menu.Items.Clear()
                If lista.Count > 1 Then
                    If _conciliationDetail IsNot Nothing AndAlso _conciliationDetail.State <> 2 Then
                        e.Menu.Items.Add(New DXMenuItem("Conciliación General", AddressOf ShowGeneralMovements, My.Resources.modificarLineaAzul))
                    End If
                Else
                    e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuConciliarDetalle, Eform.Conciliation), AddressOf ConciliateDetail, My.Resources.modificarLineaAzul))
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Muestra el menu de opciones
    ''' </summary>
    Private Sub INDgdvInvoiceDetailsQX_PopupMenuShowing(sender As Object, e As DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs) Handles INDgdvInvoiceDetailsQX.PopupMenuShowing
        If e.Menu Is Nothing Then
            Exit Sub
        End If
        e.Menu.Items.Clear()
        e.Menu.Items.Add(New DXMenuItem(obtenerRecurso(Eresources.MenuConciliarDetalle, Eform.Conciliation), AddressOf ConciliateDetailQx, My.Resources.modificarLineaAzul))
    End Sub

#End Region

#Region "EditValueChanged"

    Private Async Sub INDbteNit_EditValueChanged(sender As Object, e As EditValueChangedEventArgs) Handles INDbteNit.EditValueChanged
        If Me.INDbteNit.EditValue IsNot Nothing Then
            Me._customer = Await _myModel.GetCustomerByNit(Me.INDbteNit.EditValue)
            If _customer IsNot Nothing AndAlso _customer.Id > 0 Then
                Using model As New MConciliation(Me.Tag)
                    Me.INDsleSearchInvoice.Properties.DataSource = model.ListXpoInvoicesByNitGlosaPortfolio(Me._customer.Nit)
                End Using
            End If
        End If
    End Sub

    ''' <summary>
    ''' Aqui se controla que cuando el detalle ya se ha conciliado y se ha guardado,
    ''' no sea posible dejar el comentario vacio
    ''' </summary>
    Private Sub INDrepComment_EditValueChanged(sender As Object, e As EventArgs)
        Dim obj = TryCast(Me.INDgdvConciliateInvoiceDetail.GetRow(Me.INDgdvConciliateInvoiceDetail.FocusedRowHandle), Domain.Entities.GlosaMovementGlosa)
        If obj IsNot Nothing AndAlso obj.State IsNot Nothing AndAlso obj.State = 5 AndAlso CType(sender, MemoExEdit).EditValue IsNot Nothing AndAlso CType(sender, MemoExEdit).EditValue.ToString().Trim().Equals(String.Empty) Then
            CType(sender, MemoExEdit).EditValue = IIf(obj.RationaleConciliation Is Nothing, "-", obj.RationaleConciliation.Trim())
        End If
        Me.INDgdvConciliateInvoiceDetail.PostEditor()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el frontal de busqueda para filtrar las conciliaciones
    ''' </summary>
    Private Sub INDbteConsecutive_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteConsecutive.Properties.ButtonClick
        Me.OpenSearch()
    End Sub

    Private Sub INDbteNit_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDbteNit.OpenFormButtonClick
        OpenForm(503, Nothing, True)
        LoadXpoCustomers()
    End Sub

    ''' <summary>
    ''' Aqui se concilia un detalle de factura
    ''' </summary>
    Private Sub INDrepDetailActions_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDrepDetailActions.ButtonClick
        Dim mainView2 As GridView = INDgdvInvoiceDetails
        Dim detailView3 As GridView = TryCast(mainView2.GetDetailView(mainView2.FocusedRowHandle, mainView2.GetRelationIndex(mainView2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
        Dim OBJGlosaInvoiceDetail As GlosaInvoiceDetail = INDgdvInvoiceDetails.GetFocusedRow
        If OBJGlosaInvoiceDetail.GlosaInvoiceDetailQX.Count > 0 Then
            If detailView3 IsNot Nothing Then
                If detailView3.IsFocusedView = False Then
                    MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                    Exit Sub
                End If
            Else
                MessageIndigo.Show(obtenerRecurso(GloseDetalleQX, RecepcionObjeciones), MessageType.Warning, Me.Text)
                Exit Sub
            End If
        Else
            Me.ConciliateDetail()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se concilia un detalle QX de factura
    ''' </summary>
    Private Sub INDrepDetailQxActions_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDrepDetailQxActions.ButtonClick
        Me.ConciliateDetailQx()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega o modifica un participante en la lista de la cabecera
    ''' </summary>
    Private Sub INDbtnAddParticipant_Click(sender As Object, e As EventArgs) Handles INDbtnAddParticipant.Click
        If Me.INDbtnAddParticipant.Tag.ToString().Trim().Equals("ADD") Then
            If Not Me.INDtxtParticipantName.Text.Trim().Equals(String.Empty) Then
                If Not Me.INDtxtParticipantPosition.Text.Trim().Equals(String.Empty) Then
                    If Me.INDrdgParticipantType.EditValue IsNot Nothing Then
                        If Not Me._conciliation.ConciliationParticipants.Any(Function(p As Domain.Entities.ConciliationParticipants) p.FullName.Replace(" ", "").ToUpper().Equals(Me.INDtxtParticipantName.Text.Replace(" ", "").ToUpper())) Then
                            Me._conciliation.ConciliationParticipants.Add(New Domain.Entities.ConciliationParticipants With {.ConciliationCId = Me._conciliation.Id, .FullName = Me.INDtxtParticipantName.Text.Trim().ToUpper(), .Position = Me.INDtxtParticipantPosition.Text.Trim().ToUpper(), .Type = Me.INDrdgParticipantType.EditValue.ToString()})
                            If Me._conciliation.ChangeTracker.State = ObjectState.Unchanged Then
                                Me._conciliation.ChangeTracker.State = ObjectState.Modified
                            End If
                            Me.Participants = Me._conciliation.ConciliationParticipants
                        End If
                        Me.CleanParticipantControls()
                    Else
                        Me.INDrdgParticipantType.Focus()
                    End If
                Else
                    Me.INDtxtParticipantPosition.Focus()
                End If
            Else
                Me.INDtxtParticipantName.Focus()
            End If
        Else 'Entonces es modificar
            Me.INDbtnAddParticipant.Tag = "ADD"
            If Not Me.INDtxtParticipantName.Text.Trim().Equals(String.Empty) Then
                If Not Me.INDtxtParticipantPosition.Text.Trim().Equals(String.Empty) Then
                    Me._conciliation.ConciliationParticipants.Remove(Me._currentParticipant)
                    If Not Me._currentParticipant.FullName.Trim().ToUpper().Equals(Me.INDtxtParticipantName.Text.Trim().ToUpper()) Then
                        Me._currentParticipant.FullName = Me.INDtxtParticipantName.Text.Trim()
                    End If
                    If Not Me._currentParticipant.Position.Trim().ToUpper().Equals(Me.INDtxtParticipantPosition.Text.Trim().ToUpper()) Then
                        Me._currentParticipant.Position = Me.INDtxtParticipantPosition.Text.Trim()
                    End If
                    If Not Me._currentParticipant.Type.Trim().Equals(Me.INDrdgParticipantType.EditValue.ToString()) Then
                        Me._currentParticipant.Type = Me.INDrdgParticipantType.EditValue.ToString()
                    End If
                    Me._conciliation.ConciliationParticipants.Add(Me._currentParticipant)
                    If Me._conciliation.ChangeTracker.State = ObjectState.Unchanged Then
                        Me._conciliation.ChangeTracker.State = ObjectState.Modified
                    End If
                    Me.Participants = Me._conciliation.ConciliationParticipants
                    Me.INDbtnAddParticipant.Text = obtenerRecurso(Eresources.TextoBotonAgregarParticipante, Eform.Conciliation)
                    Me.CleanParticipantControls()
                Else
                    Me.INDtxtParticipantPosition.Text = Me._currentParticipant.Position.Trim()
                End If
            Else
                Me.INDtxtParticipantName.Text = Me._currentParticipant.FullName.Trim()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Ejecuta el modo de edicion del participante seleccionado
    ''' </summary>
    Private Sub INDbtnEditParticipant_Click(sender As Object, e As EventArgs) Handles INDbtnEditParticipant.Click
        Me.EditParticipant()
    End Sub
    ''' <summary>
    ''' Ejecuta la eliminacion del participante seleccionado
    ''' </summary>
    Private Sub INDbtnDeleteParticipant_Click(sender As Object, e As EventArgs) Handles INDbtnDeleteParticipant.Click
        Me.DeleteParticipant()
    End Sub

    ''' <summary>
    ''' Realiza la busqueda de la factura seleccionada y la agrega a la rejilla
    ''' </summary>
    Private Async Sub INDbtnAddInvoice_Click(sender As Object, e As EventArgs) Handles INDbtnAddInvoice.Click
        If Me.INDsleSearchInvoice.EditValue IsNot Nothing AndAlso Not Me.INDsleSearchInvoice.Text.Trim().Equals(String.Empty) Then
            Dim invoiceToAdd As String = Me.INDsleSearchInvoice.Text.Trim()
            If (From p In Me._listConciliationDetail Where p.InvoiceNumber.Equals(invoiceToAdd) Select p).Any Then
                'Mensaje de que ya existe la factura!
                Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FacturaYaExiste, Eform.Conciliation)
                Me.INDsleSearchInvoice.EditValue = Nothing
            Else

                Me.AsyncLoader(True)

                ' Verificacion contra BD
                If Me._conciliation IsNot Nothing AndAlso Me._conciliation.Id > 0 Then
                    Dim persisted = Await _myModel.ListDetailConciliationById(Me._conciliation.Id)
                    If persisted IsNot Nothing AndAlso persisted.Any(Function(p) p.State IsNot Nothing AndAlso p.State.Trim() = "1" AndAlso
                                                                                   Not String.IsNullOrWhiteSpace(p.InvoiceNumber) AndAlso
                                                                                   p.InvoiceNumber.Trim().Equals(invoiceToAdd, StringComparison.OrdinalIgnoreCase)) Then
                        Me.AsyncLoader(False)
                        Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FacturaYaExiste, Eform.Conciliation)
                        Me.INDsleSearchInvoice.EditValue = Nothing
                        Me.INDsleSearchInvoice.Focus()
                        Return
                    End If
                End If

                Dim auxInvoice = Await _myModel.GetInvoiceByNumber(Me._customer.Nit.Trim(), invoiceToAdd, Nothing)
                If auxInvoice IsNot Nothing AndAlso auxInvoice.Id > 0 Then
                    Me._listConciliationDetail.Add(New Domain.Entities.ConciliationD With {.ConciliationCId = Me._conciliation.Id, .GlosaPortfolioGlosada = auxInvoice, .GlosaPortfolioId = auxInvoice.Id, .InvoiceNumber = Me.INDsleSearchInvoice.Text.Trim(), .State = "-1"})
                    Me.SelectedInvoices = Me._listConciliationDetail
                    Me.INDgdcInvoices.RefreshDataSource()
                    Me.INDsleSearchInvoice.EditValue = Nothing
                    If Me._conciliation IsNot Nothing AndAlso Me._conciliation.Id > 0 Then
                        Me.BarraBotones.PrepareToolbar(eAction.UpdateAndProcess)
                    End If
                Else
                    'La factura no existe
                    Me.Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.FacturaNoExisteAlAgregar, Eform.Conciliation)
                End If
                Me.AsyncLoader(False)

            End If
            Me.INDsleSearchInvoice.Focus()
        Else
            Me.INDsleSearchInvoice.Focus()
            Me.INDsleSearchInvoice.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Elimina la factura seleccionada en la rejilla
    ''' </summary>
    Private Sub INDbtnDeleteInvoice_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDbtnDeleteInvoice.Click
        Me.DeleteInvoice()
    End Sub

    ''' <summary>
    ''' Carga el detalle de una factura y muestra el popUp
    ''' </summary>
    Private Sub INDbtnEditInvoice_ButtonClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles INDbtnEditInvoice.Click
        Me.LoadAndDisplayInvoiceDetails()
    End Sub
    ''' <summary>
    ''' Abre formulario de Carga de Datos de excel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnLoad_Click(sender As Object, e As EventArgs) Handles INDbtnLoad.Click
        Me.GetOpenFileDialog()
    End Sub

#End Region

#Region "DoubleClick"

    ''' <summary>
    ''' Inicia la edicion del participante seleccionado en la rejilla
    ''' </summary>
    Private Sub INDgdcParticipants_DoubleClick(sender As Object, e As EventArgs) Handles INDgdcParticipants.DoubleClick
        If Me.INDgdvParticipants.SelectedRowsCount > 0 Then
            Me.EditParticipant()
        End If
    End Sub

    ''' <summary>
    ''' Abre el PopUpContainer de los detalles de factura
    ''' </summary>
    Private Sub INDgcvObjetions_DoubleClick(sender As Object, e As EventArgs) Handles INDgdvInvoices.DoubleClick
        Dim view As GridView = CType(sender, GridView)
        Dim pt As Point = view.GridControl.PointToClient(Control.MousePosition)
        Dim info As GridHitInfo = view.CalcHitInfo(pt)
        ' se valida que este sobre una fila
        If info.InRow OrElse info.InRowCell Then
            If info.Column IsNot Nothing Then
                LoadAndDisplayInvoiceDetails()
            End If
        End If
    End Sub

#End Region

#Region "ClickBack"

    ''' <summary>
    ''' Ejecuta una accion al hacer click sobre el botón Atrás
    ''' </summary>
    Private Async Sub INDnvbInvoiceDetails_ClickBack() Handles INDnvbInvoiceDetails.ClickBack
        Select Case Me._currentActiveGroup
            Case EGroups.ConciliateInvoiceDetail
                Me.AdditionalControlPanel.Visible = True
                Me.INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlyiConciliateDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlyiInvoiceDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgInvoiceDetails.Text = obtenerRecurso(Eresources.TituloDetalleFactura, Eform.Conciliation)
                Me.INDlbMaximunValuePending.Text = ManageDecimalsFun(valuePending)

                If Me.BarraBotones.StatusRecord = "-1" Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If

                Me.BarraBotones.RibbonPagEform.Visible = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Actualizar) = True

                Me._currentActiveGroup = EGroups.InvoiceDetails
                IdResponseHierarchy = Nothing
            Case EGroups.InvoiceDetails
                _ctrTraceabilityControl.Visible = False
                Me.AdditionalControlPanel.Visible = False
                Me.INDlycgInvoiceDetailsMain.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Me.INDlycgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Me.INDlycgHeader.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                Me.AsyncLoader(True)
                Me.SelectedInvoices = Await GetListConciliationDetailAdded(_listConciliationDetail)
                Me.INDgdcInvoices.RefreshDataSource()
                Me.AsyncLoader(False)

                Me.BarraBotones.StatusRecord = Me._conciliation.State.Trim()
                If Me.BarraBotones.StatusRecord = "1" Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyProcess)
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If

                Me._currentActiveGroup = EGroups.Root
        End Select
    End Sub

#End Region

#Region "SelectionChanged"

    ''' <summary>
    ''' Se ejecuta para realizar la sumatoria de las celdas seleccionadas
    ''' en la vista principal
    ''' </summary>
    Private Sub INDgdvInvoiceDetails_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles INDgdvInvoiceDetails.SelectionChanged
        Dim sum As Decimal = 0
        For Each c As GridCell In Me.INDgdvInvoiceDetails.GetSelectedCells()
            Dim aux As Decimal = 0
            If Decimal.TryParse(Me.INDgdvInvoiceDetails.GetRowCellValue(c.RowHandle, c.Column), aux) Then
                sum += aux
            End If
        Next
        Me.INDlbMaximunValuePending.Text = ManageDecimalsFun(sum)
    End Sub

    ''' <summary>
    ''' Se ejecuta para realizar la sumatoria de las celdas seleccionadas
    ''' en la vista secundaria
    ''' </summary>
    Private Sub INDgdvInvoiceDetailsQX_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles INDgdvInvoiceDetailsQX.SelectionChanged
        Dim Currency As Boolean = True
        Dim Sum As Decimal
        '   Dim detailview2 As GridView = INDgdcInvoiceDetails.FocusedView
        Dim detailView3 As GridView = INDgdcInvoiceDetails.FocusedView 'TryCast(detailview2.GetDetailView(detailview2.FocusedRowHandle, detailview2.GetRelationIndex(detailview2.FocusedRowHandle, "GlosaInvoiceDetailQX")), GridView)
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
            Me.INDlblSumaryCellsSeleccted.Text = ManageDecimalsFun(Sum)
        Else
            Me.INDlblSumaryCellsSeleccted.Text = Sum.MoneyFormat(0)
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta para realizar la sumatoria de las celdas seleccionadas
    ''' en la vista principal de la rejilla de conciliacion
    ''' </summary>
    Private Sub INDgdvConciliateInvoiceDetail_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles INDgdvConciliateInvoiceDetail.SelectionChanged
        Dim sum As Decimal = 0
        For Each c As GridCell In Me.INDgdvConciliateInvoiceDetail.GetSelectedCells()
            If c.Column.ColumnEdit Is Nothing Then
                Dim aux As Decimal = 0
                If Decimal.TryParse(Me.INDgdvConciliateInvoiceDetail.GetRowCellValue(c.RowHandle, c.Column), aux) Then
                    sum += aux
                End If
            End If
        Next
        Me.INDlblSumaryCellsSeleccted.Text = ManageDecimalsFun(sum)
    End Sub

#End Region

#Region "TxtFindControlEnter"

    ''' <summary>
    ''' Captura el evento enter en el control TextEdit de la caja de busqueda
    ''' en los controles SearchLookUpEdit que extienden su funcionalidad
    ''' </summary>
    Private Sub INDSearchLookUpControlExtended_TxtFindControlEnter(sender As Object, e As TxtFindControlEnterEventArgs) Handles INDSearchLookUpControlExtended.TxtFindControlEnter
        If Not e.TxtFind.Text.Trim().Equals(String.Empty) AndAlso e.BaseView.RowCount > 1 Then
            e.BaseView.Focus()
            e.BaseView.FocusedRowHandle = e.BaseView.GetRowHandle(0)
        ElseIf Not e.TxtFind.Text.Trim().Equals(String.Empty) AndAlso e.BaseView.RowCount = 1 AndAlso e.BaseView.GetRowCellValue(0, "InvoiceNumber") IsNot Nothing AndAlso e.BaseView.GetRowCellValue(0, "InvoiceNumber").GetType().FullName <> "DevExpress.Data.NotLoadedObject" Then
            Dim aux As New String(e.BaseView.GetRowCellValue(0, "InvoiceNumber"))
            Me.INDsleSearchInvoice.ClosePopup()
            Me.INDsleSearchInvoice.EditValue = aux
            Me.INDsleSearchInvoice.Text = aux
        End If
    End Sub

#End Region

#Region "MasterRowGetRelationCount"

    ''' <summary>
    ''' Controlo que solo se muestre la subvista de detalles Qx
    ''' </summary>
    Private Sub INDgdvInvoiceDetails_MasterRowGetRelationCount(sender As Object, e As MasterRowGetRelationCountEventArgs) Handles INDgdvInvoiceDetails.MasterRowGetRelationCount
        e.RelationCount = 2
    End Sub

#End Region

#Region "CellValueChanged"

    ''' <summary>
    ''' Se valida que el valor ingresado en la celda de valor aceptado IPS no sea mayor al glosado o reiterado
    ''' y tambien se calcula el valor aceptado por la EAPB
    ''' </summary>
    Private Sub INDgdvConciliateInvoiceDetail_CellValueChanged(sender As Object, e As CellValueChangedEventArgs) Handles INDgdvConciliateInvoiceDetail.CellValueChanged
        If e.Column.FieldName = "ValPendingIPSconciliation" OrElse e.Column.FieldName = "ValPendingEAPBconciliation" Then
            'Obtengo el movimiento que se esta conciliando
            Dim glosaMov As GlosaMovementGlosa = CType(Me.INDgdvConciliateInvoiceDetail.GetRow(e.RowHandle), GlosaMovementGlosa)
            Dim valInput = Convert.ToDecimal(INDgdvConciliateInvoiceDetail.GetRowCellValue(e.RowHandle, e.Column.FieldName))

            'Obtengo todos los movimientos asociados
            Dim listmov = CType(Me.INDgdvConciliateInvoiceDetail.DataSource, List(Of GlosaMovementGlosa))

            'si no tiene valor pendiemte, asignamos null
            If glosaMov.ValuePendingConciliation Is Nothing OrElse glosaMov.ValuePendingConciliation = 0 Then
                glosaMov.ValPendingIPSconciliation = Nothing
                glosaMov.ValPendingEAPBconciliation = Nothing
                glosaMov.ValPendingConciliation = Nothing
                IdResponseHierarchy = Nothing
                Exit Sub
            End If

            'Esto se realiza para evitar el problema de los decimales ya que en la regilla se muestra redondeado
            If valInput = Math.Round(glosaMov.ValuePendingConciliation.Value, 0) Then
                valInput = glosaMov.ValuePendingConciliation
            End If

            'que el valor ingresado no sea un numero negativo
            If valInput < 0 Then
                glosaMov.ValPendingIPSconciliation = 0
                glosaMov.ValPendingEAPBconciliation = 0
                glosaMov.ValPendingConciliation = 0
                IdResponseHierarchy = Nothing
                Exit Sub
            End If

            'que el valor ingresado no sea mayor al pendiente por conciliar
            If valInput > glosaMov.ValuePendingConciliation Then
                glosaMov.ValPendingIPSconciliation = 0
                glosaMov.ValPendingEAPBconciliation = 0
                glosaMov.ValPendingConciliation = 0
                IdResponseHierarchy = Nothing
                Exit Sub
            End If

            Dim ValPendingIPSconciliation = If(e.Column.FieldName = "ValPendingIPSconciliation", valInput, glosaMov.ValPendingIPSconciliation)
            Dim ValPendingEAPBconciliation = If(e.Column.FieldName = "ValPendingEAPBconciliation", valInput, glosaMov.ValPendingEAPBconciliation)
            Dim calculatePending = (ValPendingIPSconciliation = 0 AndAlso ValPendingEAPBconciliation = 0) OrElse (e.Column.FieldName = "ValPendingIPSconciliation" AndAlso ValPendingEAPBconciliation > 0) OrElse (e.Column.FieldName = "ValPendingEAPBconciliation" AndAlso ValPendingIPSconciliation > 0)

            If Not calculatePending OrElse (glosaMov.ValuePendingConciliation < (ValPendingIPSconciliation + ValPendingEAPBconciliation)) Then
                ValPendingIPSconciliation = If(e.Column.FieldName = "ValPendingIPSconciliation", valInput, glosaMov.ValuePendingConciliation - valInput)
                ValPendingEAPBconciliation = If(e.Column.FieldName = "ValPendingEAPBconciliation", valInput, glosaMov.ValuePendingConciliation - valInput)
            End If

            Dim totalConciliate As Decimal = 0
            For Each mov As GlosaMovementGlosa In listmov
                If mov.Id <> glosaMov.Id Then
                    totalConciliate += mov.ValPendingIPSconciliation
                    totalConciliate += mov.ValPendingEAPBconciliation
                    totalConciliate += mov.ValPendingConciliation
                End If
            Next

            'Si se excede el valor máximo permitido para el detalle
            If (ValPendingIPSconciliation + ValPendingEAPBconciliation + totalConciliate) > _valueConstMaximoPending Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmConciliation_SumatoriaMayorSaldo", "Glosas"), totalConciliate, _valueConstMaximoPending)

                glosaMov.ValPendingIPSconciliation = 0
                glosaMov.ValPendingEAPBconciliation = 0
                glosaMov.ValPendingConciliation = 0
                IdResponseHierarchy = Nothing
                Exit Sub
            End If

            glosaMov.ValPendingIPSconciliation = ValPendingIPSconciliation
            glosaMov.ValPendingEAPBconciliation = ValPendingEAPBconciliation
            glosaMov.ValPendingConciliation = glosaMov.ValuePendingConciliation - (glosaMov.ValPendingIPSconciliation + glosaMov.ValPendingEAPBconciliation)
            glosaMov.IdResponseHierarchyConciliation = If(glosaMov.ValPendingIPSconciliation > 0, IdResponseHierarchy, Nothing)
            Me.INDgdvConciliateInvoiceDetail.RefreshData()
        End If
    End Sub

#End Region

#Region "CustomRowFilter"

    ''' <summary>
    ''' ocultamo fila donde los qx no tenga moviminetos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgdvInvoiceDetailsQX_CustomRowFilter(sender As Object, e As RowFilterEventArgs) Handles INDgdvInvoiceDetailsQX.CustomRowFilter
        Dim Obj As GlosaInvoiceDetailQX = TryCast(TryCast(sender, GridView).GetRow(e.ListSourceRow), GlosaInvoiceDetailQX)
        If Obj IsNot Nothing AndAlso Obj.GlosaMovementGlosa.Count = 0 Then
            e.Visible = False
            e.Handled = True
        End If
    End Sub

#End Region

#Region "IdEntityLoaded"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._conciliation IsNot Nothing AndAlso Me._conciliation.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                If Me._blockRecord IsNot Nothing AndAlso Me.INDbteConsecutive.Text = Me.IdEntity.Trim Then
                    Return
                End If
                Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteConsecutive.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
        End If
        Me.IdEntity = String.Empty
    End Sub

#End Region

#End Region

#Region "Buttons Bar"

    ''' <summary>
    ''' Aqui se cargan los permisos que tiene el frontal en la barra
    ''' </summary>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de buscar
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar
        Me.Buscar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
        Me.Deshacer()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion nuevo
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Me.Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion guardar
    ''' </summary>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        _conciliation.State = "1"
        Me.Guardar()
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion de actualizar
    ''' </summary>
    Private Async Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        If INDlycgConcilateInvoiceDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            Me.SaveConciliationDetail()
        Else
            _conciliation.State = "1"
            Me.Guardar()
        End If
    End Sub

    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            _conciliation.State = "3"
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta la ccion de confirmar dependiendo del contexto en que se encuentre situado el proceso
    ''' </summary>
    Private Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.ConfirmProcess()
        End If
    End Sub

    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Me.ConfirmProcess()
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta la opcion eliminar
    ''' </summary>
    Private Sub BarraBotones_ClickEliminar() Handles BarraBotones.ClickEliminar
        Me.Eliminar()
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, Me._conciliation.Id, 0, {Me._conciliation.Id, Me.BarraBotones.OperatingUnit})
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            Await Me.LoadParameters()
        End If
    End Sub

#End Region

End Class