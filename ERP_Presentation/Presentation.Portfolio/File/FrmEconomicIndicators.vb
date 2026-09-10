'***********************************************************************
' Assembly         : Presentacion.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 02-04-2014
'
' Last Modified By :  
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Portfolio.MVP
Imports Presentation.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Controls
Imports System.Text
Imports Presentation.Base.Eresources
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform

#End Region

Public Class FrmEconomicIndicators
    Implements IEconomicIndicator, ICustomizableForm

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Portfolio"

#End Region

#Region "Variable Globales Propiedades Interfaz"
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PortfolioSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Obtiene o establece el codigo del indicador economico
    ''' </summary>
    ''' <value>
    ''' codigo
    ''' </value>
    Public Property Code As String Implements IEconomicIndicator.Code
        Get
            If INDBtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew")) Then
                Return String.Empty
            Else
                Return INDBtnCode.Text
            End If
        End Get
        Set(value As String)
            INDBtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' obtien o establece el interes financiero
    ''' </summary>
    ''' <value>
    ''' interes financiro
    ''' </value>
    Public Property FinancialInterests As Decimal Implements IEconomicIndicator.FinancialInterests
        Get
            Return INDSpinFinancialInterests.EditValue
        End Get
        Set(value As Decimal)
            INDSpinFinancialInterests.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el interes de mora
    ''' </summary>
    ''' <value>
    ''' interes de mora
    ''' </value>
    Public Property LatePaymentInterest As Decimal Implements IEconomicIndicator.LatePaymentInterest
        Get
            Return INDSpinLatePaymentInterest.EditValue
        End Get
        Set(value As Decimal)
            INDSpinLatePaymentInterest.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' obtine o establece el mes
    ''' </summary>
    ''' <value>
    ''' mes
    ''' </value>
    Public Property Month As String Implements IEconomicIndicator.Month
        Get
            Return INDDteMonth.Text
        End Get
        Set(value As String)
            INDDteMonth.EditValue = value

        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el estado del registro
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [status]; otherwise, <c>false</c>.
    ''' </value>
    Public Property Status As Boolean Implements IEconomicIndicator.Status
        Get
            Return Me.BarraBotones.StatusRecord
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
    ''' obtiene o establece el año
    ''' </summary>
    ''' <value>
    ''' año
    ''' </value>
    Public Property Year As String Implements IEconomicIndicator.Year
        Get
            Return INDDteYear.Text
        End Get
        Set(value As String)
            INDDteYear.EditValue = value

        End Set
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IEconomicIndicator.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value>
    ''' Secuencia numerica del formulario
    ''' </value>
    ''' <returns>La secuencia numerica del formulario</returns>
    Public Property Sequence As PortfolioSequence Implements IEconomicIndicator.Sequense
        Get
            Return Me._sequence
        End Get
        Set(value As PortfolioSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PortfolioSequenceDetail In Me._sequence.PortfolioSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IEconomicIndicator.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' entidad de indicadores economicos
    ''' </summary>
    Dim economicIndicators As EconomicIndicator
    ''' <summary>
    ''' modelo de indicaores economicos
    ''' </summary>
    Dim model As MEconomicIndicator
    ''' <summary>
    ''' presentador de indicadores economicos
    ''' </summary>
    Dim presenter As PEconomicIndicator
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Private _indigoSession As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecordPortfolio
    ''' <summary>
    ''' Variable para controlar si el formulario esta en modo busqueda
    ''' </summary>
    Private _searchMode As Boolean
#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    '''   <c>true</c> if [actions on controls]; otherwise, <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IEconomicIndicator.ActionsOnControls
        Set(value As Boolean)
            INDLycEconomicIndicators.BeginUpdate()
            INDBtnCode.Enabled = Not value
            INDDteYear.Enabled = value
            INDDteMonth.Enabled = value
            INDSpinFinancialInterests.Enabled = value
            INDSpinLatePaymentInterest.Enabled = value
            Me.BarraBotones.StatusRecordVisible = value
            INDLycEconomicIndicators.EndUpdate()
            If value = True Then
                INDDteYear.Focus()
            Else
                INDBtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        CleanControls()
        'If _searchMode = False Then
        '    'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        '    Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
        'INDBtnCode.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        'If economicIndicators IsNot Nothing AndAlso economicIndicators.Id > -1 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        Using model As New MEconomicIndicator(Me.Tag.ToString())
        '            economicIndicators.MarkAsDeleted()
        '            AsyncLoader(True)
        '            Dim result = Await model.DeleteEconomicIndicator(economicIndicators)
        '            If result.StateResult = True Then
        '                Await Me.DeleteDocumentIndexed()
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
        '                AsyncLoader(False)
        '                _searchMode = False
        '                Deshacer()
        '            Else
        '                If result.MessageResult(0) = "-999" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                    AsyncLoader(False)
        '                ElseIf result.MessageResult(0) = "-000" Then
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
        '                    AsyncLoader(False)
        '                Else
        '                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                    AsyncLoader(False)
        '                End If
        '            End If
        '        End Using
        '    End If
        'End If


        If Me.economicIndicators IsNot Nothing AndAlso Me.economicIndicators.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MEconomicIndicator(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteEconomicIndicator(Me.economicIndicators)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Exit Sub
        'End If
        'AssigningValues()
        'Try
        '    Using model As New MEconomicIndicator(Me.Tag.ToString())
        '        Dim auxEconomicIndicator = Await model.GetEconomicIndicator(Year, Month)
        '        If auxEconomicIndicator.Id > 0 And auxEconomicIndicator.Id <> economicIndicators.Id Then
        '            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("EconomicIndicatorYearMonth", NAME_MODULE)
        '            Exit Sub
        '        End If
        '        AsyncLoader(True)
        '        Dim Result = Await model.SaveEconomicIndicator(economicIndicators, _idCurrentSequense)
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            economicIndicators = Result.ObjectEmbbeded
        '            If economicIndicators.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '                'Se descarta la secuencia numerica usada
        '                If Not Me._sequense.IsManual AndAlso Not Me._sequense.Sequential Then
        '                    Me.DicSequense(Me._sequense.PortfolioSequenceDetail(0).Id).RemoveAt(0)
        '                End If
        '                Mensaje(EeventViewerImages.Informacion) = String.Format(ResourceManager.GetString("SavedWithCode"), Me.economicIndicators.Code)
        '            Else
        '                Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
        '            End If
        '            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '            _searchMode = False
        '            Me.Deshacer()
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '                AsyncLoader(False)
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '                AsyncLoader(False)
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try




        If Not ValidateControls() Then
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MEconomicIndicator(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of EconomicIndicator) = Await Model.SaveEconomicIndicator(Me.economicIndicators, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If economicIndicators.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.economicIndicators = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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

    Public Async Sub Nuevo() Implements Base.IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewEconomicIndicator()
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = 50}, New ColumnInfo With {.Caption = "Año", .FieldName = "Year", .ColumnWidth = 50}, New ColumnInfo With {.Caption = "Mes", .FieldName = "Moth", .ColumnWidth = 50}, New ColumnInfo With {.Caption = "Interes Financiero", .FieldName = "FinancialInterests", .ColumnWidth = 50}, New ColumnInfo With {.Caption = "Interes de Mora", .FieldName = "LatePaymentInterest", .ColumnWidth = 50}}.ToList()
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.EconomicIndicator
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
        'Code = ReturnValue
        'If Code <> String.Empty Then
        '    Await LoadControls()
        '    If INDBtnCode.Enabled = False Then
        '        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '    End If
        '    INDBtnCode.Enabled = False
        'End If

        DeleteBlockedRecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If Not INDBtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBtnCode.Enabled = False
        End If
    End Sub
#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        economicIndicators = Nothing
        model = Nothing
        presenter = Nothing
        record = Nothing
        _searchMode = Nothing
    End Sub


    ''' <summary>
    ''' Evento load del funcional
    ''' </summary>    
    Private Sub FrmEconomicIndicators_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Me.LayoutControls.SetIsCustomizable(Me.INDLycEconomicIndicators, True)
        '****Inicializar variables*****'
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        '******************************'
        Me.Funct = AddressOf GenerateDoc
        Me._indigoSession = SessionValues.Instance
        presenter = New PEconomicIndicator(Me)
        presenter.LoadDefinitionLayout()
        presenter.GetSequense()
        Me.LoadStatus()
        Deshacer()
        ' Me.BarraBotones.StatusRecordVisible = True
        '_searchMode = False
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el formulario y elimina el registro bloquedo
    ''' </summary>
    Private Sub FrmEconomicIndicators_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el codigo y consulta el registro
    ''' </summary>    
    Private Async Sub INDBtnCode_KeyDown(sender As Object, e As KeyEventArgs) Handles INDBtnCode.KeyDown
        ''If e.KeyCode = Keys.Enter Then
        ''    If String.IsNullOrEmpty(Code) Then
        ''        Me.NewEconomicIndicator()
        ''    Else
        ''        Await Me.LoadControls()
        ''    End If
        ''End If

        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    If Me._sequense.IsManual Then
        '        If Not String.IsNullOrEmpty(Code) Then
        '            Await Me.LoadControls()
        '        End If
        '    Else
        '        If String.IsNullOrEmpty(Code) Then
        '            Me.NewEconomicIndicator()
        '        Else
        '            Await Me.LoadControls()
        '        End If
        '    End If
        'End If



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
                    Await Me.NewEconomicIndicator()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar el boton de busqueda del control y lanza el formulario de busqueda
    ''' </summary>    
    Private Sub INDBtnCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Evento para poner el foco al codigo cuando el formulario este activo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmEconomicIndicators_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBtnCode.Enabled Then
            INDBtnCode.Focus()
        End If
    End Sub
#End Region

#Region "Metodos Funciones Propiedades"
    ''' <summary>
    ''' Metodo para generar la indexacion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer()
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), economicIndicators.Code, economicIndicators.Year, economicIndicators.Month), _
                .CreationDate = dateServer, .CreationUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & economicIndicators.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), economicIndicators.Code), _
                .Update = dateServer, .UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me._indigoSession.UserIndigo & "-" & Me._indigoSession.UserIndigoName
            Me._doc.Content = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexContent", NAME_MODULE), economicIndicators.Code, economicIndicators.Year, economicIndicators.Month)
            Me._doc.Title = String.Format(ResourceManager.GetString(Me.GetType().Name & "_IndexTitle", NAME_MODULE), economicIndicators.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        'INDLycEconomicIndicators.BeginUpdate()
        'BarraBotones.CleanAuditBasic()
        'ActionsOnControls = False
        'INDBtnCode.Text = String.Empty
        'INDDteMonth.EditValue = Nothing
        'INDDteMonth.Text = String.Empty
        'INDDteYear.EditValue = Nothing
        'INDDteYear.Text = String.Empty
        'INDSpinFinancialInterests.EditValue = 0
        'INDSpinLatePaymentInterest.EditValue = 0
        'Me.BarraBotones.StatusRecordVisible = False
        'economicIndicators = Nothing
        'DeleteBlockedRecord()
        'Me._doc = Nothing
        ' Me.BarraBotones.EnableBarItems()
        'Me.BarraBotones.DisableBarDocument()
        'INDLycEconomicIndicators.EndUpdate()





        INDLycEconomicIndicators.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        ActionsOnControls = False
        INDBtnCode.Text = String.Empty
        INDDteMonth.EditValue = Nothing
        INDDteMonth.Text = String.Empty
        INDDteYear.EditValue = Nothing
        INDDteYear.Text = String.Empty
        INDSpinFinancialInterests.EditValue = 0
        INDSpinLatePaymentInterest.EditValue = 0
        'Limpiar controles
        economicIndicators = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDLycEconomicIndicators.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With economicIndicators
            .CustomProperties = Me.LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Year = Year
            .Month = Month
            If FinancialInterests < 0 Then
                Dim aux = FinancialInterests.ToString.Replace("-", "")
                .FinancialInterests = CDec(aux)
            Else
                .FinancialInterests = FinancialInterests
            End If

            If LatePaymentInterest < 0 Then
                Dim aux = LatePaymentInterest.ToString.Replace("-", "")
                .LatePaymentInterest = aux
            Else
                .LatePaymentInterest = LatePaymentInterest
            End If
        End With
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Private Async Sub DeleteBlockedRecord()
        'If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
        '    Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
        '        Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '        Await model.DeleteBlockRecord(record)
        '        record = Nothing
        '    End Using
        'Else
        '    Me.BarraBotones.EnableBarItems()
        'End If


        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                Await model.DeleteBlockRecord(record)
                record = Nothing
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para cargar los controles con el registro consultado
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadControls() As Task
        'If Me.BarraBotones.PermiteConsultar = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
        '    Exit Function
        'End If

        'Using model As New MEconomicIndicator(Me.Tag.ToString())
        '    AsyncLoader(True)
        '    economicIndicators = Await model.GetEconomicIndicatorByCode(Code)
        '    INDLycEconomicIndicators.BeginUpdate()
        'End Using

        'Using model As New MBlockRecordAndSequense(Me.Tag.ToString())
        '    If Not economicIndicators Is Nothing AndAlso economicIndicators.Id > 0 Then
        '        Me.BarraBotones.StatusRecordVisible = True
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), economicIndicators.CreationUser)
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), economicIndicators.CreationDate)
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), economicIndicators.ModificationUser)
        '        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), economicIndicators.ModificationDate)
        '        Dim result = Await model.GetBlockRecord(Me.Tag, Me.economicIndicators.Id)
        '        Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '        With economicIndicators
        '            Me.LayoutControls.SetCustomFieldsValue(.CustomProperties)
        '            INDDteYear.EditValue = CDate("01/01/" + .Year)
        '            INDDteMonth.EditValue = CDate("01/" + .Month + "/9999")
        '            FinancialInterests = .FinancialInterests
        '            LatePaymentInterest = .LatePaymentInterest
        '            Status = .Status
        '        End With
        '        BarraBotones.SetDocuments(economicIndicators.Id)
        '        AsyncLoader(False)
        '        ActionsOnControls = True
        '        INDDteYear.Focus()
        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.economicIndicators.Code)
        '        If result IsNot Nothing AndAlso result.Id = 0 Then
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New BlockRecordPortfolio With {.BlockDate = DateTime.Now, .ChangeTracker = state, .NameUser = SessionValues.Instance.UserIndigoName, .IdForm = Me.Tag, .CodUser = SessionValues.Instance.UserIndigo, .IdRecord = economicIndicators.Id}
        '            Dim operation = Await model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), result.CodUser, result.NameUser, result.BlockDate)
        '            record = result
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    Else
        '        'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '        'Code = String.Empty
        '        'INDBtnCode.Focus()
        '        AsyncLoader(False)
        '        If Me._sequense.IsManual Then
        '            Me.NewEconomicIndicator()
        '        Else
        '            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
        '            Code = String.Empty
        '            Deshacer()
        '            INDBtnCode.Focus()
        '        End If
        '    End If
        'End Using
        'INDLycEconomicIndicators.EndUpdate()





        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MEconomicIndicator(CStr(Me.Tag))
                    AsyncLoader(True)
                    economicIndicators = Await Model.GetEconomicIndicatorByCode(INDBtnCode.Text.Trim)
                    INDLycEconomicIndicators.BeginUpdate()
                    If economicIndicators IsNot Nothing AndAlso economicIndicators.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(economicIndicators.Id))
                            With economicIndicators
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad

                                INDDteYear.EditValue = CDate("01/01/" + .Year)
                                INDDteMonth.EditValue = CDate("01/" + .Month + "/9999")
                                FinancialInterests = .FinancialInterests
                                LatePaymentInterest = .LatePaymentInterest
                                Status = .Status
                            End With
                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.economicIndicators.Code)
                            If record.Id = 0 Then
                                record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordPortfolio With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = economicIndicators.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                            End If
                            'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                            Me.BarraBotones.SetDocuments(economicIndicators.Id, Me.Tag.ToString(), Nothing, GetType(EconomicIndicator).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewEconomicIndicator()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBtnCode.Focus()
                        End If
                    End If
                    INDLycEconomicIndicators.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear un nuevo indicador economico
    ''' </summary>
    Private Async Function NewEconomicIndicator() As Task
        'economicIndicators = New EconomicIndicator()
        'If Me._sequense.IsManual Then
        '    Me.ActionsOnControls = True
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        'Else
        '    If Me._sequense.Scope.Equals("O") Then 'El ambito es a nivel de organización
        '        Me._idCurrentSequense = Me._sequense.PortfolioSequenceDetail(0).Id
        '    ElseIf Me._sequense.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
        '        If Me._sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue) Then
        '            Me._idCurrentSequense = Me._sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = Me.BarraBotones.OperatingUnitValue).SingleOrDefault().Id
        '        Else
        '            Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '            Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '            Exit Function
        '        End If
        '    End If
        '    If Not Me._sequense.Sequential Then
        '        If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
        '            If Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '            Else
        '                Using model As New MBlockRecordAndSequense(Me.Tag)
        '                    Me.DicSequense(Me._idCurrentSequense) = Await model.GetNumericSequenseGroup(Me._idCurrentSequense)
        '                End Using
        '                If Me.DicSequense(Me._idCurrentSequense) IsNot Nothing AndAlso Me.DicSequense(Me._idCurrentSequense).Count > 0 Then
        '                    PrepareToolbar(Me.DicSequense(Me._idCurrentSequense)(0))
        '                Else
        '                    Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("InvalidPatternSequense")
        '                End If
        '            End If
        '        Else
        '            PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '        End If
        '    Else
        '        PrepareToolbar(ResourceManager.GetString("LabelOrTextboxNew"))
        '    End If
        'End If







        economicIndicators = New EconomicIndicator() With {.Status = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PortfolioSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        Me.Code = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
#End Region

#Region "Handlers"

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    ''' 
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'If economicIndicators IsNot Nothing AndAlso economicIndicators.Id > 0 Then
        '    If MessageIndigo.Show(ResourceManager.GetString("OpenFromVituelContent"), MessageType.Question, ResourceManager.GetString("OpenFromVituelTitle"), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '        DeleteBlockedRecord()
        '        Code = Me.IdEntity.Trim()
        '        Await LoadControls()
        '    End If
        'Else 'Realiza la consulta normal
        '    Code = Me.IdEntity.Trim()
        '    Await LoadControls()
        'End If
        'Me.IdEntity =  String.Empty
        'If INDBtnCode.Enabled = False Then
        '    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        'End If




        If Me.economicIndicators IsNot Nothing AndAlso Me.economicIndicators.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBtnCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBtnCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
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
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click_ active inactive.
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        'Try
        '    Using model As New MEconomicIndicator(Me.Tag.ToString())
        '        AsyncLoader(True)
        '        Dim Result As New ActionResult(Of EconomicIndicator)
        '        Select Case CType(Me.BarraBotones.StatusRecord, eActionsStatusRecords)
        '            Case eActionsStatusRecords.Active
        '                Result = Await model.ChangeState(Code, True)
        '            Case eActionsStatusRecords.Inactive
        '                Result = Await model.ChangeState(Code, False)
        '        End Select
        '        AsyncLoader(False)
        '        If Result.StateResult = True Then
        '            economicIndicators = Result.ObjectEmbbeded
        '            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
        '        Else
        '            If Result.MessageResult(0) = ErrorConcurrencia Then
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
        '            Else
        '                Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
        '            End If
        '        End If
        '    End Using
        'Catch ex As Exception
        '    Throw ex
        '    AsyncLoader(False)
        'End Try

        If Not String.IsNullOrEmpty(Me.economicIndicators.Code) Then
            Try
                Using model As New MEconomicIndicator(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not economicIndicators.Status
                    Dim result As ActionResult(Of EconomicIndicator) = Await model.ChangeState(Me.economicIndicators.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.economicIndicators = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDBtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        'If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.PortfolioSequenceDetail IsNot Nothing Then
        '    If Me._sequense.PortfolioSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
        '        Me._idOperativeUnit = Me._sequense.PortfolioSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    Else
        '        Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        '    End If
        'End If


        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PortfolioSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PortfolioSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

End Class