'***********************************************************************
' Assembly         : Presentacion.Common
' Author           : Sumit sarkar
' Created          : 09-04-2019
'
' Last Modified By : William Otalora
' Last Modified On : 12-10-2022
' Description      : Add Rounding Type GridLookUpEdit
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Data.Async.Helpers
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.CommonRepository
Imports Presentation.Accounting.MVP
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common.MVP
Imports Presentation.Controls

#End Region

''' <summary>
''' Contiene el comportamiento de la vista del frontal Monedas
''' </summary>
Public Class FrmCurrency
    Implements ICurrency, ICustomizableForm

#Region "Globals & Properties"

    Public Const NAME_MODULE As String = "Commons"

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
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    Private Property ModoBusqueda As Boolean

    ''' <summary>
    ''' Variable que contiene la lista de tipos de redondeo
    ''' </summary>
    Dim ListRoundingType As New List(Of Tuple(Of Byte, String))

    ''' <summary>
    ''' Propiedad que contiene el estado de lo controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements ICurrency.ActionsOnControls
        Set(value As Boolean)
            INDlyCurrency.BeginUpdate()

            INDbteCode.Enabled = Not value
            INDsleCurrency.Enabled = value
            INDtxtName.Enabled = value
            INDtxtAbbreviation.Enabled = value
            INDgleRoundingType.Enabled = value
            INDrbgCurrencyExchange.Enabled = value
            INDrbgRateVariation.Enabled = value
            INDgcCurrency.Enabled = value
            INDlyCurrency.EndUpdate()
            INDPceAddValueCurrency.Enabled = If(Indigo.OfficialCurrencyId = ISO4217Id, value, Not value)

            If value = True Then
                INDsleCurrency.Focus()
            Else
                INDbteCode.Focus()
            End If

        End Set
    End Property

    ''' <summary>
    ''' Entidad de paises
    ''' </summary>
    Dim Currency As Currency

    ''' <summary>
    ''' Lista de Entidad trm
    ''' </summary>
    Dim Trm As Domain.Entities.TrackableCollection(Of TRM)

    ''' <summary>
    ''' Variable para acceder al presentador
    ''' </summary>
    Dim Presenter As PCurrency

    Private _idCurrentSequence As Long

    Private _idOperativeUnit As Integer
    ''' <summary>
    ''' Encapsula la entidad del tipo de documento
    ''' </summary>
    Private _Currency As New Currency
    ''' <summary>
    ''' Encapsula la entidad del tipo de documento
    ''' </summary>
    Private _generalLedgerIva As New GeneralLedgerIVA

    ''' <summary>
    ''' Variable de la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para controlar el registro bloqueado en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Dim record As BlockRecord

    ''' <summary>
    ''' Propiedad que establece el codigo del Monedas
    ''' </summary>
    Public Property Code As String Implements ICurrency.Code
        Get
            If (INDbteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbteCode.Text
            End If
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el valor del campo segun iso4217
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyISO4217 As XPInstantFeedbackSource Implements ICurrency.CurrencyISO4217
        Get
            Return TryCast(INDsleCurrency.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrency.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que establece el valor del campo segun iso4217
    ''' </summary>
    ''' <returns></returns>
    Public Property CurrencyISO4217filter As XPInstantFeedbackSource Implements ICurrency.CurrencyISO4217filter
        Get
            Return TryCast(INDsleCurrencyToConvert.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCurrencyToConvert.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el nombre de la Monedas
    ''' </summary>
    Public Property CurrencyName As String Implements ICurrency.CurrencyName
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la abreviatura de la Monedas
    ''' </summary>
    Public Property Abbreviation As String Implements ICurrency.Abbreviation
        Get
            Return INDtxtAbbreviation.Text
        End Get
        Set(value As String)
            INDtxtAbbreviation.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la tasa de cambio automatica de la Monedas
    ''' </summary>
    Public Property CurrencyExchange As Boolean Implements ICurrency.CurrencyExchange
        Get
            Return CBool(INDrbgCurrencyExchange.EditValue)
        End Get
        Set(value As Boolean)
            INDrbgCurrencyExchange.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la variacion de la tasa de cambio automatica de la Monedas
    ''' </summary>
    Public Property RateVariation As Boolean Implements ICurrency.RateVariation
        Get
            Return CBool(INDrbgRateVariation.EditValue)
        End Get
        Set(value As Boolean)
            INDrbgRateVariation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el tipo de redondeo de la Monedas
    ''' </summary>
    Public Property RoundingType As String Implements ICurrency.RoundingType
        Get
            Return INDgleRoundingType.EditValue
        End Get
        Set(value As String)
            INDgleRoundingType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el estado de las Monedas
    ''' </summary>
    Public Property Status As Boolean Implements ICurrency.Status
        'Get
        '    Return Me.BarraBotones.StatusRecord
        'End Get
        'Set(value As Boolean)
        '    Me.BarraBotones.StatusRecord = value
        'End Set

        Get
            Return CBool(Me.BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = False Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece la fecha en que se tomó de medicion para la tasa representativa del mercado
    ''' </summary>
    Public Property MeasurementDate As Date Implements ICurrency.MeasurementDate
        Get
            Return INDdteMeasurementDate.DateTime
        End Get
        Set(value As Date)
            INDdteMeasurementDate.DateTime = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el valor de la tasa representativa del mercado
    ''' </summary>
    Public Property Value As Double Implements ICurrency.Value
        Get
            Return INDtxtValue.EditValue
        End Get
        Set(value As Double)
            INDtxtValue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece el valor de la tasa representativa del mercado
    ''' </summary>
    Public Property CurrencyToConvertId As Integer?
        Get
            Return INDsleCurrencyToConvert.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrencyToConvert.EditValue = value
        End Set
    End Property

    Private _sequence As GeneralLedgerSequence
    Public Property Sequence As GeneralLedgerSequence Implements ICurrency.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As GeneralLedgerSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.GeneralLedgerSequenceDetail In Me._sequence.GeneralLedgerSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
        'Get
        '    Throw New NotImplementedException()
        'End Get
        'Set(value As GeneralLedgerSequence)
        '    Throw New NotImplementedException()
        'End Set
    End Property

    ''' <summary>
    ''' propiedad que retorna o establece el Id de la tabla ISO4217
    ''' </summary>
    ''' <returns></returns>
    Property ISO4217Id As Integer? Implements ICurrency.ISO4217Id
        Get
            Return INDsleCurrency.EditValue
        End Get
        Set(value As Integer?)
            INDsleCurrency.EditValue = value
        End Set
    End Property
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        Currency = Nothing
        Presenter = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        record = Nothing
    End Sub

    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCurrency_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyCurrency, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        'Me.Model = New MCurrency(Me.Tag)
        Me.Indigo = SessionValues.Instance
        '******************************'
        Me._funct = AddressOf GenerateDoc
        LoadStatus()
        Presenter = New PCurrency(Me)
        Presenter.GetSequense()
        InitializeSearch()

        'Presenter.LoadDefinitionLayout()
        Deshacer()
    End Sub

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
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
                    Await Me.NewCurrency()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control .
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.Currency IsNot Nothing AndAlso Me.Currency.Id > 0 Then
            If MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity
                Await Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Await Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Evento para capturar el cierre del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmDepartaments_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub
#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el formulario de busquedas
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Codigo de las Monedas", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Nombre de las Monedas", .FieldName = "CurrencyName"}}.ToList()
            .ValorSolicitado = "Codigo"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Currency
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        DeleteBlockedRecord()
        INDbteCode.Text = ReturnValue
        If INDbteCode.Text IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Ejecuta el metodo de abrirbusqueda
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
        'Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el pais seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.Currency IsNot Nothing AndAlso Me.Currency.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    AsyncLoader(True)
                    Using model As New MCurrency(MCurrency.TAG)
                        Dim result = Await model.DeleteCurrencyAsync(Me.Currency)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
        'Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Guarda el pais
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Using model As New MCurrency(MCurrency.TAG)
                Dim result As ActionResult(Of Currency) = Await model.SaveCurrencyAsync(Me.Currency, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Currency.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Currency = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbteCode.Enabled = False
            Throw ex
        End Try
        'Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Muestre y escribe el mensaje de retorno por las operaciones
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
            'Throw New NotImplementedException()
        End Set
    End Property

    ''' <summary>
    ''' Esta función define una propiedad de solo lectura llamada MyTag,
    ''' que implementa la interfaz ICurrency.MyTag. La propiedad MyTag permite
    ''' acceder al valor de la propiedad Tag del formulario o del objeto que implementa la interfaz ICurrency.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As String Implements ICurrency.MyTag
        Get
            Return Me.Tag.ToString()
        End Get
    End Property


    ''' <summary>
    ''' Esta función implementa la propiedad de solo lectura MyLayoutControl de una interfaz ICurrency.
    ''' La propiedad devuelve un objeto del tipo IndigoLayoutControl que representa el control de diseño
    ''' utilizado en el formulario o clase que implementa la interfaz ICurrency.
    ''' </summary>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ICurrency.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Limpia el formulario para iniciar 
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewCurrency()
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmCurrencyMetaData, Eform.InfoMetaData), Me.Currency.Code, Me.Currency.Name),
                .CreationDate = dateServer, .CreationUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Currency.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmCurrencyMetaDataTitle, Eform.InfoMetaData), Me.Currency.Code),
                .Update = dateServer, .UpdateUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.Indigo.UserIndigo & "-" & Me.Indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmCurrencyMetaData, Eform.InfoMetaData), Me.Currency.Code, Me.Currency.Name)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmCurrencyMetaDataTitle, Eform.InfoMetaData), Me.Currency.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.Indigo.UserIndigo) Then
            Using model As New MCurrency(MCurrency.TAG)
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyCurrency.BeginUpdate()
        ActionsOnControls = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Status = True
        INDbteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDtxtAbbreviation.Text = String.Empty
        INDrbgCurrencyExchange.Text = String.Empty
        INDrbgRateVariation.Text = String.Empty
        INDgleRoundingType.Text = String.Empty
        INDdteMeasurementDate.EditValue = Nothing
        INDtxtValue.EditValue = Nothing
        INDsleCurrency.EditValue = Nothing
        INDsleCurrency.Properties.NullText = String.Empty
        INDgcCurrency.DataSource = Nothing


        Currency = Nothing
        Status = True

        If Indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyCurrency.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Currency
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code

            .Name = CurrencyName
            .Abbreviation = Abbreviation
            .CurrencyExchange = CurrencyExchange
            .RateVariation = RateVariation
            .RoundingType = RoundingType
            .ISO4217Id = Me.ISO4217Id
            If Trm IsNot Nothing Then
                If (Indigo.OfficialCurrencyId = .Id) Then
                    .TRM1 = Trm
                Else
                    .TRM = Trm
                End If
            End If
        End With
    End Sub

    ''' <summary>
    ''' Esta función NewCurrency crea una nueva instancia de la clase Currency y realiza una serie de operaciones
    ''' y verificaciones basadas en las propiedades y configuraciones del objeto actual y del sistema.
    ''' </summary>
    Private Async Function NewCurrency() As Task
        Currency = New Currency() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.GeneralLedgerSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MStatementFolio(CStr(Me.Tag))
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
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Using Model As New MCurrency(CStr(Me.Tag))
                AsyncLoader(True)
                Currency = Await Model.GetCurrencyAsync(INDbteCode.Text.Trim)
                INDlyCurrency.BeginUpdate()
                If Currency IsNot Nothing AndAlso Currency.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True
                    record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Currency.Id))
                    With Currency
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                        Code = .Code
                        ISO4217Id = .ISO4217Id
                        INDsleCurrency.Properties.NullText = .ISO4217.CurrencyName
                        _Currency.Code = .Code
                        CurrencyName = .Name
                        Status = .State
                        Me.Abbreviation = .Abbreviation
                        Me.CurrencyExchange = .CurrencyExchange
                        Me.RateVariation = .RateVariation
                        Me.RoundingType = .RoundingType

                        If Indigo.OfficialCurrencyId = .Id Then
                            Presenter.InitializerISO4217($"Id <> {ISO4217Id}")
                            If .TRM1?.LastOrDefault IsNot Nothing Then
                                Trm = .TRM1
                                Me.MeasurementDate = .TRM1?.LastOrDefault?.MeasurementDate
                                Me.Value = .TRM1.LastOrDefault.Value
                                Me.CurrencyToConvertId = .TRM1.LastOrDefault.CurrencyId
                            End If
                            INDPceAddValueCurrency.Enabled = True
                        Else
                            If .TRM?.LastOrDefault IsNot Nothing Then
                                Trm = .TRM
                                Me.MeasurementDate = .TRM.LastOrDefault.MeasurementDate
                                Me.Value = .TRM.LastOrDefault.Value
                                Me.CurrencyToConvertId = .TRM.LastOrDefault.OfficialCurrencyId
                            End If
                            INDPceAddValueCurrency.Enabled = False
                        End If
                        INDgcCurrency.DataSource = Trm
                    End With

                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Currency.Code)
                    If record.Id = 0 Then
                        record = (Await Model.SaveBlockRecord(
                            New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                .NameUser = Me.Indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.Indigo.UserIndigo, .IdRecord = Currency.Id})
                            ).ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(Currency.Id, Me.Tag.ToString(), Nothing, GetType(Currency).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    AsyncLoader(False)
                    ActionsOnControls = True
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewCurrency()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                        Code = String.Empty
                        INDbteCode.Focus()
                    End If
                End If
                INDgcCurrency.RefreshDataSource()
                INDlyCurrency.EndUpdate()
            End Using
        End If
    End Function

    ''' <summary>
    ''' Llena el datasource de los controles de tipo de redondeo
    ''' </summary>
    Private Sub InitializeSearch()
        ListRoundingType = New List(Of Tuple(Of Byte, String))
        ListRoundingType.Add(New Tuple(Of Byte, String)(1, "0,01 A dos decimales"))
        ListRoundingType.Add(New Tuple(Of Byte, String)(2, "0,1 A un decimal"))
        ListRoundingType.Add(New Tuple(Of Byte, String)(3, "1 Ninguno"))
        ListRoundingType.Add(New Tuple(Of Byte, String)(4, "10 A la decena"))
        ListRoundingType.Add(New Tuple(Of Byte, String)(5, "100 A la centena"))
        ListRoundingType.Add(New Tuple(Of Byte, String)(6, "1000 A la milésima"))
        INDgleRoundingType.Properties.DataSource = ListRoundingType.ToList
    End Sub

#End Region

#Region "Eventos Barra Botones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
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
        ModoBusqueda = False
        Me.Deshacer()
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
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Evento para cambiar el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Esta función UpdateState actualiza el estado de una entidad de moneda (Currency). 
    ''' </summary>
    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me._Currency.Code) Then
            Try
                Using Model As New MCurrency(Me.Tag)
                    AsyncLoader(True)
                    Dim stateCard As Boolean = Not Me._Currency.State
                    Dim Result = Await Model.UpdateCurrency(Me._Currency.Code, stateCard)
                    AsyncLoader(False)
                    If Result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        Me._Currency = Result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If Result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                INDbteCode.Enabled = False
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
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

    ''' <summary>
    ''' Esta función BarraBotones_ChangeOperatingUnit maneja el evento ChangueOperatingUnit de la barra de botones.
    ''' </summary>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.GeneralLedgerSequenceDetail IsNot Nothing Then
                If Not Me._sequence.GeneralLedgerSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo cuando se selecciona una opcion de la iso4217
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCurrency.EditValueChanged
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            Exit Sub
        End If

        Dim ISO4217 = DirectCast(DirectCast(SearchLookUpEdit1View.GetFocusedRow, ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.CommonRepository.ISO4217Xpo)
        Me.Abbreviation = ISO4217.CodeAbbreviation
        Me.CurrencyName = ISO4217.CurrencyName
    End Sub

    ''' <summary>
    ''' Metodo para mostrar los datos de iso4217
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleCurrency_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrency.QueryPopUp
        If INDsleCurrency.Properties.DataSource Is Nothing Then
            Presenter.InitializerISO4217()
        End If
    End Sub
#End Region

#Region "Customizar"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCurrency.ShowCustomizationForm()
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
            INDlyCurrency.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Async Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCurrency.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MCurrency(Me.Tag)
                Dim dsFields As DataSet = Await model.GetFieldsNULLAsync()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCurrency.Items.Count - 1
                        INDlyCurrency.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCurrency.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCurrency.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCurrency.Items.Item(j).AllowHide = True
                                End If
                            End If
                        Next
                    Next
                End If
            End Using
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCurrency.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCurrency.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCurrency.SaveLayoutToXml(PathFunctionalDefinitions)
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
        If My.Computer.FileSystem.DirectoryExists(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
            My.Computer.FileSystem.DeleteDirectory(String.Concat(Indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name), Microsoft.VisualBasic.FileIO.DeleteDirectoryOption.DeleteAllContents)
            INDlyCurrency.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


    ''' <summary>
    ''' esta función maneja el evento de solicitud de abrir el menú desplegable (QueryPopUp) del control de selección de moneda
    ''' (INDsleCurrencyToConvert). Si el origen de datos del control es nulo y hay un CurrencyToConvertId válido,
    ''' llama al presentador para inicializar el control de selección de moneda utilizando un filtro específico.
    ''' </summary>
    Private Sub INDsleCurrencyToConvert_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCurrencyToConvert.QueryPopUp
        If INDsleCurrencyToConvert.Properties.DataSource Is Nothing Then
            If CurrencyToConvertId IsNot Nothing Then
                Presenter.InitializerISO4217($"Id <> {ISO4217Id}")
            End If
        End If
    End Sub


    ''' <summary>
    ''' Esta función maneja el evento Click del botón INDSbAdd. Cuando el botón INDSbAdd es clicado, se llama a la función addNewTRM().
    ''' </summary>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        addNewTRM()
    End Sub

    ''' <summary>
    ''' Esta serie de funciones addNewTRM() y CleanPopUp() están relacionadas con la adición de una nueva TRM (Tasa Representativa del Mercado) en la aplicación. 
    ''' </summary>
    Sub addNewTRM()

        If INDsleCurrencyToConvert.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes especificar la moneda"
            INDsleCurrency.ShowPopup()
            Exit Sub
        End If
        If String.IsNullOrEmpty(INDtxtValue.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debes indicar un valor"
            INDtxtValue.Focus()
            Exit Sub
        End If

        If (From x In Trm Where x.MeasurementDate = MeasurementDate Select x).Count > 0 Then
            Dim y = (From x In Trm Where x.MeasurementDate = MeasurementDate Select x).First
            With y
                .ValueOfficialToCurrency = Value
            End With
        Else
            Trm.Add(New Domain.Entities.TRM With {.MeasurementDate = INDdteMeasurementDate.DateTime, .CurrencyId = CurrencyToConvertId, .Value = Math.Round(1 / Value, 5), .OfficialCurrencyId = Indigo.OfficialCurrencyId, .ValueOfficialToCurrency = Value})
        End If
        INDgcCurrency.RefreshDataSource()
        CleanPopUp()
    End Sub

    ''' <summary>
    ''' La función CleanPopUp() realiza una tarea específica de restablecer o "limpiar" las variables CurrencyToConvertId y Value.
    ''' </summary>
    Sub CleanPopUp()
        CurrencyToConvertId = Nothing
        Value = 0
    End Sub


#End Region
End Class