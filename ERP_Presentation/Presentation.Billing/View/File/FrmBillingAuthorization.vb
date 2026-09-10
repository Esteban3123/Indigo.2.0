'***********************************************************************
' Assembly         : Presentacion.Billing.MVP
' Author           : Juan Carlos Bermudez Gutierrez
' Created          : 23-04-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Billing.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Domain.Base.Entities
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports System.ComponentModel
Imports DevExpress.XtraEditors.Controls

#End Region

Public Class FrmBillingAuthorization
    Implements IBillingAuthorization

#Region "Properties"

    Public ReadOnly Property MyTag As String Implements IBillingAuthorization.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que retorna el layout para customizaciones
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IBillingAuthorization.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el nombre de la autorización de facturación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BillingAuthorizationName As String Implements IBillingAuthorization.BillingAuthorizationName
        Get
            Return INDtxtNameBillingAuthorization.EditValue
        End Get
        Set(value As String)
            INDtxtNameBillingAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo de la autorizacion de facturación|
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Code As String Implements IBillingAuthorization.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha de vigencia final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinalDate As Date? Implements IBillingAuthorization.FinalDate
        Get
            Return INDdeFinalDate.EditValue
        End Get
        Set(value As Date?)
            INDdeFinalDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  Obtiene o establece la fecha de vigencia Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialDate As Date? Implements IBillingAuthorization.InitialDate
        Get
            Return INDdeInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDdeInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el consecutivo actual de la autorizacion
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Consecutive As Long Implements IBillingAuthorization.Consecutive
        Get
            Return INDspnConsecutive.EditValue
        End Get
        Set(value As Long)
            INDspnConsecutive.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la factura final
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property FinalInvoice As Long Implements IBillingAuthorization.FinalInvoice
        Get
            Return INDspnFinalInvoice.EditValue
        End Get
        Set(value As Long)
            INDspnFinalInvoice.EditValue = value
        End Set
    End Property

    ''' <summary>
    '''  Obtiene o establece la Factura Inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialInvoice As Long Implements IBillingAuthorization.InitialInvoice
        Get
            Return INDspnInitialInvoice.EditValue
        End Get
        Set(value As Long)
            INDspnInitialInvoice.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el prefijo de la factura
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoicePrefix As String Implements IBillingAuthorization.InvoicePrefix
        Get
            Return INDtxtInvoicePrefix.EditValue
        End Get
        Set(value As String)
            INDtxtInvoicePrefix.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tipo de factura
    ''' </summary>
    ''' <value>1 - Digitalizada 2 - Manual</value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InvoiceType As Integer? Implements IBillingAuthorization.InvoiceType
        Get
            Return INDsleInvoiceType.EditValue
        End Get
        Set(value As Integer?)
            INDsleInvoiceType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la Fecha de la resolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResolutionDate As Date Implements IBillingAuthorization.ResolutionDate
        Get
            Return INDdeResolutionDate.EditValue
        End Get
        Set(value As Date)
            INDdeResolutionDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el Numero de la resolución
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ResolutionNumber As String Implements IBillingAuthorization.ResolutionNumber
        Get
            Return INDtxtResolutionNumber.EditValue
        End Get
        Set(value As String)
            INDtxtResolutionNumber.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Sequence As BillingSequence Implements IBillingAuthorization.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As BillingSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.BillingSequenceDetail In Me._sequence.BillingSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad que contiene el estado del registro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean Implements IBillingAuthorization.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
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
    ''' Obtiene o establece el id del usuario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdUser As Integer? Implements IBillingAuthorization.IdUser
        Get
            Return INDsleUsers.EditValue
        End Get
        Set(value As Integer?)
            INDsleUsers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la clave tecnica usada para la facturación electrónica
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TechnicalKey As String Implements IBillingAuthorization.TechnicalKey
        Get
            Return INDtxtTechnicalKey.EditValue
        End Get
        Set(value As String)
            INDtxtTechnicalKey.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de usuarios
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserXpo As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IBillingAuthorization.UserXpo
        Get
            Return CType(INDsleUsers.Properties.DataSource, DevExpress.Data.Linq.LinqInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDsleUsers.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Const"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Billing"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Objeto registro del parametro de contabilidad
    ''' </summary>
    Private _settingAccount As GeneralLedgerSettings

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordBilling

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PBillingAuthorization

    ''' <summary>
    ''' Representa la entidad de autorización de facturacion
    ''' </summary>
    ''' <remarks></remarks>
    Dim billingAuthorization As BillingAuthorization

    ''' <summary>
    ''' Listado del detallo de usarios autorizaciones de facturación
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBillingAuthorizationUser As List(Of BillingAuthorizationUser)

    ''' <summary>
    ''' Listado de eliminados de usarios autorizaciones de facturación
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteBillingAuthorizationUser As List(Of BillingAuthorizationUser)

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.BillingSequence

    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Usuario xpo
    ''' </summary>
    ''' <remarks></remarks>
    Private _usersXpo As Infrastructure.Data.Xpo.SecurityRepository.UserXpo

    ''' <summary>
    ''' Controla el editvalueChanged del control de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private ban As Boolean = False

    ''' <summary>
    ''' Identifica si se esta cargando el registro
    ''' </summary>
    ''' <remarks></remarks>
    Private isLoading As Boolean = False

    ''' <summary>
    ''' Variable que contiene la lista de tipos de factura
    ''' </summary>
    Dim ListInvoiceType As New List(Of Tuple(Of Integer, String))

#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.billingAuthorization IsNot Nothing AndAlso Me.billingAuthorization.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MBillingAuthorization(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteBillingAuthorization(Me.billingAuthorization)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Await Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateData() = False Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MBillingAuthorization(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of BillingAuthorization) = Await Model.SaveBillingAuthorization(Me.billingAuthorization, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If billingAuthorization.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.billingAuthorization = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub


    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence Is Nothing OrElse _sequence.Id = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
            Exit Sub
        End If
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewBillingAuthorization()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.12)},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.88)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListBillingAuthotization
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

#End Region

#Region "Events"

#Region "Load"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        record = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        billingAuthorization = Nothing
        ListBillingAuthorizationUser = Nothing
        ListDeleteBillingAuthorizationUser = Nothing
        _searchMode = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _usersXpo = Nothing
        ban = Nothing
        ListInvoiceType = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmBillingAuthorization_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PBillingAuthorization(Me)
        Presenter.GetSequense()

        Await LoadParameters()
        IndigoGridControl1.RefreshGrid(INDgcUsers)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(viewUsersGrid, ListActions)
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegarse el control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleUsers.QueryPopUp
        If INDsleUsers.Properties.DataSource Is Nothing Then
            Presenter.InitializeUsers()
        End If
    End Sub

#End Region

#Region "FrmClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBillingAuthorization_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewBillingAuthorization()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa cuando se presiona cualquier tecla de navegación, se usa para evitar que dentro de la navegación con las teclas arriba y abajo, llegue a seleccionarse numeros negativos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDspnInitialInvoice_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDspnInitialInvoice.KeyDown
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Evento que se activa cuando se presiona cualquier tecla de navegación, se usa para evitar que dentro de la navegación con las teclas arriba y abajo, llegue a seleccionarse numeros negativos.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDspnFinalInvoice_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDspnFinalInvoice.KeyDown
        If e.KeyValue = 38 Or e.KeyValue = 40 Then
            e.Handled = True
        End If
    End Sub

#End Region

#Region "Activated"

    ''' <summary>
    ''' Evento que se dispara al activarse el formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBillingAuthorization_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Enabled Then
            INDbtnCode.Focus()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara al presionar el boton del control de usuarios
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleUsers.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New Presentation.Security.FrmUsers With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeUsers()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        DeleteWarehouseUser()
    End Sub

    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        DeleteWarehouseUser()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleUsers_EditValueChanged(sender As Object, e As ChangingEventArgs) Handles INDsleUsers.EditValueChanging
        If e.NewValue IsNot Nothing AndAlso ban = False Then
            _usersXpo = INDsleUsers.GetFocusedRow(Of Infrastructure.Data.Xpo.SecurityRepository.UserXpo)()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el tipo de factura
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleInvoiceType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleInvoiceType.EditValueChanged
        If InvoiceType = 3 Then
            INDlciTechnicalKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlciTechnicalKey.AllowHide = False

            INDLciGetResolution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciGetResolution.AllowHide = False
        Else
            INDlciTechnicalKey.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlciTechnicalKey.AllowHide = True

            INDLciGetResolution.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciGetResolution.AllowHide = True
        End If
    End Sub

#End Region

#Region "EditValueChanging"

    Private Sub INDspnInitialInvoice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDspnInitialInvoice.EditValueChanging
        If Not Me.isLoading AndAlso Not String.IsNullOrEmpty(e.NewValue) Then
            If e.NewValue > INDspnFinalInvoice.EditValue Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "La factura inicial no puede ser mayor a la final"
            Else
                INDspnConsecutive.Properties.MinValue = e.NewValue
            End If
        End If
    End Sub

    Private Sub INDspnFinalInvoice_EditValueChanging(sender As Object, e As ChangingEventArgs) Handles INDspnFinalInvoice.EditValueChanging
        If Not Me.isLoading AndAlso Not String.IsNullOrEmpty(e.NewValue) Then
            If e.NewValue < INDspnInitialInvoice.EditValue Then
                e.Cancel = True
                Mensaje(EeventViewerImages.Advertencia) = "La factura final no puede ser menor a la inicial"
            Else
                INDspnConsecutive.Properties.MaxValue = e.NewValue
            End If
        End If
    End Sub

#End Region

#Region "Click"

    Private Async Sub INDbtnGetResolution_Click(sender As Object, e As EventArgs) Handles INDbtnGetResolution.Click
        If String.IsNullOrEmpty(ResolutionNumber) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar un número de resolución."
            Exit Sub
        End If
        AssigningValues()

        Try
            Using Model As New MBillingAuthorization(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result = Await Model.GetBillingAuthorizationResolution(BarraBotones.OperatingUnit.Id, Me.billingAuthorization)
                If result.StateResult Then
                    With result.ObjectEmbbeded
                        ResolutionDate = .ResolutionDate
                        InitialDate = .InitialDate
                        FinalDate = .FinalDate
                        InitialInvoice = .InitialInvoice
                        FinalInvoice = .FinalInvoice
                        InvoicePrefix = .InvoicePrefix
                        TechnicalKey = .TechnicalKey
                    End With
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
        Finally
            AsyncLoader(False)
        End Try
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click en el boton de agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddUser_Click(sender As Object, e As EventArgs) Handles INDbtnAddUser.Click
        If INDsleUsers.EditValue IsNot Nothing Then
            CreateBillingAuthorizationUser()
        Else
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SelectedUser", "Inventory")
            INDsleUsers.Focus()
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    Private Async Function LoadParameters() As Task
        Dim _tempOpretingUnitId As Integer
        If BarraBotones.OperatingUnit Is Nothing Then
            _tempOpretingUnitId = indigo.IndigoOperatingUnitId
        Else
            _tempOpretingUnitId = BarraBotones.OperatingUnit.Id
        End If
        Using model As New Presentation.Accounting.MVP.MSettingsAccount(MyTag)
            _settingAccount = Await model.GetSettingAccount(_tempOpretingUnitId)
            If _settingAccount Is Nothing OrElse _settingAccount.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se encontró parámetros de Generales de Contabilidad para la Unidad Operativa seleccionada"
            End If
        End Using
        InitializeTuples()
    End Function

    ''' <summary>
    ''' valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateData() As Boolean
        If ValidateControls() = False Then
            Return False
        End If

        'Valida que los valore no sean cero
        If InitialInvoice <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "La Factura Inicial no puede ser menor o igual 0."
            Return False
        End If

        'valida que la factura inicial no se mayor que la final
        If InitialInvoice > FinalInvoice Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmBillingAuthorization_RangeInvoice", NAME_MODULE))
            Return False
        End If

        'valida que el consecutivo de la factura sea diferente a 0 y este en el rango de facturas
        If Consecutive < 0 Or Consecutive < InitialInvoice Or Consecutive > FinalInvoice Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmBillingAuthorization_Consecutive", NAME_MODULE))
            Return False
        End If

        'Valida que se defina las fechas de vigencia de la resolución de facturación
        If InitialDate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar la Fecha Inicial de la vigencia de la Autorización de Facturación."
            Return False
        End If
        If FinalDate Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe ingresar la Fecha Final de la vigencia de la Autorización de Facturación."
            Return False
        End If

        'valida que la fecha de vigencia inicial no se mayor que la final
        If InitialDate > FinalDate Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FrmBillingAuthorization_DateRangeInvoice", NAME_MODULE))
            Return False
        End If

        If InvoiceType = 3 AndAlso String.IsNullOrEmpty(TechnicalKey) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe especificar la Clave Técnica."
            Return False
        End If

        Return True
    End Function

    ''' <summary>
    ''' Metodo que inicializa el datasource de las tuplas
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListInvoiceType = New List(Of Tuple(Of Integer, String))
        ListInvoiceType.Add(New Tuple(Of Integer, String)(1, "Digitalizada"))
        ListInvoiceType.Add(New Tuple(Of Integer, String)(2, "Manual"))

        'Valida si la empresa maneja Facturación Electronica
        If _settingAccount IsNot Nothing AndAlso _settingAccount.Id > 0 AndAlso _settingAccount.HandlesElectronicBilling = True Then
            ListInvoiceType.Add(New Tuple(Of Integer, String)(3, "Electrónica"))
        End If

        If _settingAccount?.HandlesSupportDocument = True Then
            ListInvoiceType.Add(New Tuple(Of Integer, String)(4, "Documento soporte"))
        End If

        INDsleInvoiceType.Properties.DataSource = ListInvoiceType.ToList
        INDsleInvoiceType.Properties.Buttons(1).Visible = False

    End Sub

    ''' <summary>
    ''' Bloquea o desbloquea los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBillingAuthorization.ActionsOnControls
        Set(value As Boolean)
            INDlcBase.BeginUpdate()
            INDbtnCode.Enabled = Not value
            INDtxtNameBillingAuthorization.Enabled = value
            INDtxtResolutionNumber.Enabled = value
            INDdeResolutionDate.Enabled = value
            INDtxtInvoicePrefix.Enabled = value
            INDspnInitialInvoice.Enabled = value
            INDspnFinalInvoice.Enabled = value
            INDdeInitialDate.Enabled = value
            INDdeFinalDate.Enabled = value
            INDsleInvoiceType.Enabled = value
            INDspnConsecutive.Enabled = value
            INDsleUsers.Enabled = value
            INDgcUsers.Enabled = value
            INDbtnAddUser.Enabled = value
            INDtxtTechnicalKey.Enabled = value
            INDlcBase.EndUpdate()
            If value Then
                INDtxtNameBillingAuthorization.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'If Me.billingAuthorization IsNot Nothing AndAlso Me.billingAuthorization.Id > 0 Then
        '    If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
        '        DeleteBlockedRecord()
        '        Me.INDbtnCode.Text = Me.IdEntity.Trim()
        '        Me.LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Me.INDbtnCode.Text = Me.IdEntity.Trim()
        '    Me.LoadControls()
        '    If FormSearchObjects IsNot Nothing Then
        '        FormSearchObjects.Close()
        '    End If
        'End If
        'Me.IdEntity = String.Empty



        Me.ViewModeEditHold = True
        If Me.billingAuthorization IsNot Nothing AndAlso Me.billingAuthorization.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.ICrudBase.Mensaje
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
    ''' Returns el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        Code = ReturnValue
        If Code <> String.Empty Then
            Await LoadControls()
            If INDbtnCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Genera el documento a Indexar
    ''' </summary>
    ''' <returns></returns>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.billingAuthorization.Code, Me.billingAuthorization.Name),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & CStr(Me.Tag) & "_" & Me.billingAuthorization.Code & "#$", .IdForm = CStr(Me.Tag),
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.billingAuthorization.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), Me.billingAuthorization.Code, Me.billingAuthorization.Name)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), Me.billingAuthorization.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()
        INDlcBase.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        isLoading = False
        Status = True
        Code = String.Empty
        BillingAuthorizationName = String.Empty
        ResolutionNumber = Nothing
        ResolutionDate = Me.GetDateServer()
        InvoicePrefix = Nothing
        InitialInvoice = 0
        FinalInvoice = 0
        InitialDate = Me.GetDateServer()
        FinalDate = Me.GetDateServer()
        InvoicePrefix = Nothing
        InvoiceType = Nothing
        Consecutive = 0
        INDspnConsecutive.Properties.MinValue = 0
        INDspnConsecutive.Properties.MaxValue = 0
        INDsleUsers.EditValue = Nothing
        INDgcUsers.DataSource = Nothing
        TechnicalKey = String.Empty

        INDspnConsecutive.Properties.ReadOnly = False
        ListBillingAuthorizationUser = Nothing
        ListDeleteBillingAuthorizationUser = Nothing
        billingAuthorization = Nothing
        'Limpiar controles

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlcBase.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Assignings the values.
    ''' </summary>
    Private Sub AssigningValues()
        With billingAuthorization
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = BillingAuthorizationName
            .ResolutionNumber = ResolutionNumber
            .ResolutionDate = ResolutionDate
            If InvoicePrefix Is Nothing Then
                .InvoicePrefix = ""
            Else
                .InvoicePrefix = InvoicePrefix
            End If
            .InitialInvoice = InitialInvoice
            .FinalInvoice = FinalInvoice
            .InitialDate = InitialDate
            .FinalDate = FinalDate
            .InvoiceType = InvoiceType
            .Consecutive = Consecutive

            If ListBillingAuthorizationUser IsNot Nothing Then
                For Each itemUser As BillingAuthorizationUser In ListBillingAuthorizationUser
                    .BillingAuthorizationUser.Add(itemUser)
                Next
            End If

            If InvoiceType = 3 Then
                .TechnicalKey = TechnicalKey
            Else
                .TechnicalKey = Nothing
            End If
        End With
    End Sub

    ''' <summary>
    ''' Elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedRecord() As Task
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MBillingAuthorization(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetBillingAuthorizationByCode(INDbtnCode.Text.Trim)
                    billingAuthorization = resultOperation.ObjectEmbbeded

                    LayoutControlGroup1.BeginUpdate()

                    If billingAuthorization IsNot Nothing AndAlso billingAuthorization.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(billingAuthorization.Id))
                            With billingAuthorization
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                                isLoading = True

                                'Llenar Entidad
                                Code = .Code
                                BillingAuthorizationName = .Name
                                ResolutionNumber = .ResolutionNumber
                                ResolutionDate = .ResolutionDate
                                InvoicePrefix = .InvoicePrefix
                                InitialInvoice = .InitialInvoice
                                FinalInvoice = .FinalInvoice
                                InitialDate = .InitialDate
                                FinalDate = .FinalDate
                                InvoiceType = .InvoiceType
                                Consecutive = .Consecutive
                                TechnicalKey = .TechnicalKey

                                INDspnConsecutive.Properties.MinValue = Consecutive
                                INDspnConsecutive.Properties.MaxValue = .FinalInvoice

                                ListBillingAuthorizationUser = .BillingAuthorizationUser.ToList

                                INDgcUsers.DataSource = Nothing
                                INDgcUsers.DataSource = ListBillingAuthorizationUser

                                Status = .Status

                                isLoading = False
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.billingAuthorization.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordBilling With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = billingAuthorization.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(billingAuthorization.Id, Me.Tag.ToString(), Nothing, GetType(BillingAuthorization).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBillingAuthorization()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    LayoutControlGroup1.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewBillingAuthorization() As Task
        billingAuthorization = New BillingAuthorization() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.BillingSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.BillingSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me.billingAuthorization.Code) Then
            Try
                Using model As New MBillingAuthorization(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me.billingAuthorization.Status
                    Dim result As ActionResult(Of BillingAuthorization) = Await model.ChangeStateBillingAuthorization(Me.billingAuthorization.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.billingAuthorization = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Crea el objeto para el listado de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBillingAuthorizationUser()
        If ListBillingAuthorizationUser Is Nothing Then
            ListBillingAuthorizationUser = New List(Of BillingAuthorizationUser)
        Else
            Dim cont As Integer = ListBillingAuthorizationUser.FindAll(Function(item) item.UserId = IdUser).ToList().Count
            If cont > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("UserDuplicated", "Inventory")
                INDsleUsers.Focus()
                Exit Sub
            End If
        End If
        Dim billingAuthorizationUser As New BillingAuthorizationUser
        With billingAuthorizationUser
            .UserId = _usersXpo.Id
            .UserCode = _usersXpo.UserCode
            .FullNameUser = _usersXpo.IdPerson.Fullname
        End With
        ListBillingAuthorizationUser.Add(billingAuthorizationUser)
        billingAuthorization.BillingAuthorizationUser.Add(billingAuthorizationUser)
        If billingAuthorization.Id > 0 Then
            billingAuthorization.MarkAsModified()
        End If
        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListBillingAuthorizationUser
        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UserAggregatedSatisfactory", "Inventory")
        INDsleUsers.EditValue = Nothing
        INDsleUsers.Focus()
    End Sub

    ''' <summary>
    ''' Elimina un usuario de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteWarehouseUser()
        Dim bau As BillingAuthorizationUser = CType(viewUsersGrid.GetFocusedRow, BillingAuthorizationUser)
        If bau.Id <> 0 Then
            If ListDeleteBillingAuthorizationUser Is Nothing Then
                ListDeleteBillingAuthorizationUser = New List(Of BillingAuthorizationUser)
            End If
            bau.MarkAsDeleted()
            ListDeleteBillingAuthorizationUser.Add(bau)
        End If
        ListBillingAuthorizationUser.Remove(bau)
        billingAuthorization.BillingAuthorizationUser.Remove(bau)
        If billingAuthorization.Id > 0 Then
            billingAuthorization.MarkAsModified()
        End If
        INDgcUsers.DataSource = Nothing
        INDgcUsers.DataSource = ListBillingAuthorizationUser
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Async Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        _searchMode = False
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Async Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.BillingSequenceDetail IsNot Nothing Then
                If Not Me._sequence.BillingSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If

            Await LoadParameters()
        End If
    End Sub

#End Region

End Class