'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 08-04-2011
'
' Last Modified By : 26-06-2013
' Last Modified On : Kevin Garay Rodriguez
' Description      : Refactoring
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base
Imports Presentation.Base.Eresources
Imports Presentation.Base.Eform
Imports Presentation.Base.BaseClass
Imports Infrastructure.Data.Xpo.PayrollRepository
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Presentation.Treasury.MVP
Imports Presentation.Controls.MVP
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.XtraSpreadsheet
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
#End Region

''' <summary>
''' Formulario de corporaciones
''' </summary>
Public Class FrmPayrollBank
    Implements IBank, ICustomizableForm

#Region "Global properties and variables"

    ''' <summary>
    ''' The dt fields customizables
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
    ''' Variable que contiene la corporacion 
    ''' </summary>
    Dim Bank As Domain.Payroll.Entities.Bank

    Private _idCurrentSequence As Long

    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PBank

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As BlockRecord

    Dim BankFile As New List(Of Tuple(Of String, String))

    ''' <summary>
    ''' Lista de registros cuenta bancaria
    ''' </summary>
    ''' <remarks></remarks>

    Dim BankAccountRegistrationData As New List(Of Tuple(Of Int16, String))

    ''' <summary>
    ''' Lista de registros conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBankDetail As List(Of Domain.Payroll.Entities.BankDetail)


    ''' <summary>
    ''' Lista de registros conceptos
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBankAutomaticRecognitionRulesDetail As List(Of Domain.Payroll.Entities.BankAutomaticRecognitionRules)

    ''' <summary>
    ''' Lista de eliminados de registros BankDetail
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteBankDetail As List(Of Domain.Payroll.Entities.BankDetail)

    ''' <summary>
    ''' Lista de eliminados de registros BankAutomaticRecognitionRulesDetail
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListDeleteBankAutomaticRecognitionRulesDetail As List(Of Domain.Payroll.Entities.BankAutomaticRecognitionRules)

    ''' <summary>
    ''' Propiedad para el detalle de bancos
    ''' </summary>
    ''' <returns></returns>
    Public Property BankDetail As Domain.Payroll.Entities.BankDetail

    ''' <summary>
    ''' Propiedad para el detalle de BankAutomaticRecognitionRules
    ''' </summary>
    ''' <returns></returns>
    Public Property BankAutomaticRecognitionRules As Domain.Payroll.Entities.BankAutomaticRecognitionRules

    ''' <summary>
    ''' Permite saber si esta en modo de edición para el popup
    ''' (True=Edita, False=Guarda)
    ''' </summary>
    ''' <remarks></remarks>
    Public Property FlagBankDetail As Boolean

    ''' <summary>
    ''' XPInstantFeedbackSource que permite asignar datasource
    ''' </summary>
    ''' <returns></returns>
    Public Property BankConceptsXp As XPInstantFeedbackSource

    ''' <summary>
    ''' XPInstantFeedbackSource que permite asignar datasource
    ''' </summary>
    ''' <returns></returns>
    Public Property NoteConceptsXp As XPInstantFeedbackSource


    ''' <summary>
    ''' ruta del archivo de excel
    ''' </summary>
    ''' <remarks></remarks>
    Dim myStream As String = Nothing

    ''' <summary>
    ''' coleccion de filas que se van a precesar
    ''' </summary>
    ''' <remarks></remarks>
    Dim rows As RowCollection

    ''' <summary>
    ''' listado de las filas que se van a procesar y a validar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listRows As New Concurrent.ConcurrentBag(Of ImportFileRow)()

    ''' <summary>
    ''' listado de errores que se presentaron validando el archivo
    ''' </summary>
    ''' <remarks></remarks>
    Private listErrosImportFile As List(Of String())

    ''' <summary>
    ''' Variable para almacenar el ID de la cuenta contable 
    ''' </summary>
    Dim IdMainAccount

    ''' <summary>
    ''' Propiedad que contiene el codigo de la Corporacion
    ''' </summary>
    Public Property Code As String Implements IBank.Code
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
    ''' Propiedad que contiene el registro de cuenta bancaria
    ''' </summary>
    Public Property BankAccountRegistration As Boolean Implements IBank.BankAccountRegistration
        Get
            Return INDGleBankAccountRegistration.EditValue
        End Get
        Set(value As Boolean)
            INDGleBankAccountRegistration.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nombre de la Corporacion
    ''' </summary>
    Public Property NameBank As String Implements IBank.NameBank
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el estado del registro
    ''' </summary>
    Public Property Status As Boolean Implements IBank.Status
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

    Public ReadOnly Property MyTag As Object Implements IBank.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Private _sequence As Domain.Entities.TreasurySequence
    Public Property Sequence As Domain.Entities.TreasurySequence Implements IBank.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As TreasurySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.TreasurySequenceDetail In Me._sequence.TreasurySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' propiedad que contiene el numero de la cuenta
    ''' </summary>
    Public Property BankFileCode As String Implements IBank.BankFileCode
        Get
            Return INDGleBankFile.EditValue
        End Get
        Set(value As String)
            INDGleBankFile.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el codigo ACH
    ''' </summary>
    Public Property AchCodeBank As String Implements IBank.AchCodeBank
        Get
            Return INDtxtACHCode.Text
        End Get
        Set(value As String)
            INDtxtACHCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource de los terceros
    ''' </summary>
    Public WriteOnly Property ThirdPartyDataSource As XPInstantFeedbackSource Implements IBank.ThirdPartyDataSource
        Set(value As XPInstantFeedbackSource)
            INDGleThirdPartyId.Properties.DataSource = value
        End Set
    End Property


    ''' <summary>
    ''' esta propiedad proporciona una manera de acceder y configurar la fuente de datos utilizada
    ''' por el control INDSleBankConcepts, que parece estar relacionado con conceptos bancarios en la interfaz gráfica.
    ''' </summary>
    Public Property BankConceptsXPO As XPInstantFeedbackSource Implements IBank.BankConceptsXPO
        Get
            Return CType(INDSleBankConcepts.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleBankConcepts.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad proporciona una manera de acceder y configurar la fuente de datos utilizada
    ''' por el control INDGleNotaConcept, que parece estar relacionado con conceptos bancarios en la interfaz gráfica.
    ''' </summary>
    Public Property NoteConceptsXPO As XPInstantFeedbackSource Implements IBank.NoteConceptsXPO
        Get
            Return CType(INDGleNotaConcept.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDGleNotaConcept.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' esta propiedad proporciona una manera de acceder y configurar la fuente de datos utilizada
    ''' por el control INDSleCostCenter, que parece estar relacionado con conceptos bancarios en la interfaz gráfica.
    ''' </summary>
    Public Property CostCenterXpo As XPInstantFeedbackSource
        Get
            Return CType(INDSleCostCenter.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleCostCenter.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleBankConcepts
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleBankConcepts_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleBankConcepts.QueryPopUp
        If INDSleBankConcepts.Properties.DataSource Is Nothing Then
            LoadXpoBankConcepts()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDGleNotaConcept
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDGleNotaConcept_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleNotaConcept.QueryPopUp
        If INDGleNotaConcept.Properties.DataSource Is Nothing Then
            LoadXpoNoteConcepts()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento QueryPopUp del control INDSleCostCenter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCostCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSleCostCenter.QueryPopUp
        If INDSleCostCenter.Properties.DataSource Is Nothing Then
            LoadXpoCostCenter()
        End If
    End Sub

    ''' <summary>
    ''' Propiedad que contiene el Id del tercero
    ''' </summary>
    Public Property ThirdPartyId As Integer? Implements IBank.ThirdPartyId
        Get
            Return INDGleThirdPartyId.EditValue
        End Get
        Set(value As Integer?)
            INDGleThirdPartyId.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo de de Partida Pendiente en Conciliación
    ''' </summary>
    ''' <returns></returns>
    Public Property TypeOfItemPendingInReconciliation As Integer
        Get
            Return INDSleTypeOfPendingItemInReconciliation.EditValue
        End Get
        Set(value As Integer)
            INDSleTypeOfPendingItemInReconciliation.EditValue = value
        End Set
    End Property

#End Region

#Region "CRUD Base"

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.ICrudBase.Guardar
        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MBankPayroll(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of Domain.Payroll.Entities.Bank) = Await Model.SaveBankAsync(Me.Bank, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Bank.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Bank = result.ObjectEmbbeded
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
    End Sub

    ''' <summary>
    ''' Boton que permite pasar de activo a inactivo tambien de manera contraria
    ''' </summary>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.Bank.Code) Then
            Try
                Using model As New MBankPayroll(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Bank.State
                    Dim result As ActionResult(Of Domain.Payroll.Entities.Bank) = Await model.UpdateStateBankAsync(Me.Bank.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Bank = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Bank
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.2},
                              New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion", .ColumnWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.8}}.ToList
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
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.ICrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.ICrudBase.Deshacer
        CleanControls()
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

        If Me.Bank IsNot Nothing AndAlso Me.Bank.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using model As New MBankPayroll(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await model.DeleteBankAsync(Me.Bank)
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
    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' METODO: Item nuevo del control de usuario
    ''' </summary>
    Public Async Sub Nuevo() Implements Base.ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewBank()
        End If
    End Sub

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Bank = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        record = Nothing
        BankFile = Nothing
        ListBankDetail = Nothing
        ListBankAutomaticRecognitionRulesDetail = Nothing
        ListDeleteBankDetail = Nothing
        ListDeleteBankAutomaticRecognitionRulesDetail = Nothing
        FlagBankDetail = Nothing
        rows = Nothing
        listRows = Nothing
        myStream = Nothing

    End Sub


    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmCorporation_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        '******************************'
        Me._funct = AddressOf GenerateDoc
        Presenter = New PBank(Me)
        Presenter.Initializes()
        Presenter.GetSequence()
        LoadStatus()
        Deshacer()
        GleSize()
        CreateBankFile()
        CreateBankAccountRegistration()

        Dim fillingTypeOfItemPendingInReconciliation As New List(Of TypesOfItemPendingInReconciliation)
        fillingTypeOfItemPendingInReconciliation.Add(New TypesOfItemPendingInReconciliation With {.Id = 1, .Description = "Nota de gastos bancarios"})
        fillingTypeOfItemPendingInReconciliation.Add(New TypesOfItemPendingInReconciliation With {.Id = 2, .Description = "Terceros pendientes por identificar"})
        INDSleTypeOfPendingItemInReconciliation.Properties.DataSource = fillingTypeOfItemPendingInReconciliation
        INDRislTypeOfItemPendingInReconciliation.DataSource = fillingTypeOfItemPendingInReconciliation

        INDEsbBankDetail.AddRangeColumns("Código Concepto de Conciliación Bancaria", "Código de Extracto", "Detalle")
        INDEsbBankAutomaticRecognitionRules.AddRangeColumns("Código Reglas de reconocimento automatico", "Descripción de Transacción", "Código Concepto de Nota", "Código Centro de Costo")
        IndigoGridControl1.RefreshGrid(INDgcBankDetail)
        SetActionsGrid()
    End Sub

    ''' <summary>
    ''' Carga el datasource de los planos para bancos existentes
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBankFile()
        BankFile = New List(Of Tuple(Of String, String))
        BankFile.Add(New Tuple(Of String, String)("001", "Plano Bancolombia"))
        BankFile.Add(New Tuple(Of String, String)("002", "Plano AV Villas"))
        BankFile.Add(New Tuple(Of String, String)("003", "Plano Banco Popular"))
        BankFile.Add(New Tuple(Of String, String)("004", "Plano Banco Occidente"))
        BankFile.Add(New Tuple(Of String, String)("005", "Plano Banco BBVA"))
        BankFile.Add(New Tuple(Of String, String)("006", "Plano Banco Davivienda"))
        BankFile.Add(New Tuple(Of String, String)("007", "Plano Banco Caja Social"))
        BankFile.Add(New Tuple(Of String, String)("008", "Plano Banco de Bogotá"))
        BankFile.Add(New Tuple(Of String, String)("009", "Plano Bancolombia - SAP"))
        BankFile.Add(New Tuple(Of String, String)("010", "Plano Banco Itaú"))
        BankFile.Add(New Tuple(Of String, String)("011", "Plano Banco Cooperativo Coopcentral"))
        BankFile.Add(New Tuple(Of String, String)("012", "Plano Banco Scotiabank CRC"))
        BankFile.Add(New Tuple(Of String, String)("013", "Plano Banco GNV Sudameris"))
        BankFile.Add(New Tuple(Of String, String)("014", "Plano Banco Davivienda CRC"))
        BankFile.Add(New Tuple(Of String, String)("015", "Plano Banco BCT"))
        INDGleBankFile.Properties.DataSource = BankFile.ToList()
    End Sub

    ''' <summary>
    ''' Carga el datasource del registro de cuenta bancaria
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBankAccountRegistration()
        BankAccountRegistrationData = New List(Of Tuple(Of Int16, String))
        BankAccountRegistrationData.Add(New Tuple(Of Int16, String)(0, "Numérico"))
        BankAccountRegistrationData.Add(New Tuple(Of Int16, String)(1, "Alfanumérico"))
        INDGleBankAccountRegistration.Properties.DataSource = BankAccountRegistrationData.ToList()
    End Sub

    ''' <summary>
    ''' metodo para dar tamaño a algunos controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GleSize()
        INDGleThirdPartyId.Properties.PopupFormSize = New System.Drawing.Size(500, 280)
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
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
                    Await Me.NewBank()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' El propósito de esta función es establecer el enfoque en el control INDbteCode cuando el formulario se active o se muestre
    ''' </summary>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Text Is String.Empty Then
            INDbteCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Evento al cerrar el formulario
    ''' </summary>
    Private Sub FrmBank_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        'Me.ViewModeEditHold = True
        If Me.Bank IsNot Nothing AndAlso Me.Bank.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDbteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDbteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDGleThirdPartyId control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDGleThirdPartyId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleThirdPartyId.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.Initializes()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDGleThirdPartyId control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDGleNotaConcept_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleNotaConcept.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(624, Nothing, True)
        End If
    End Sub

#End Region

#Region "PasteToGrid"
    Private Async Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        If sender.Equals(INDgcBankDetail) Then
            AsyncLoader(True)
            Using model As New MBank(MyTag)
                Dim result = Await model.SetCopyPasteOrImportFileBankDetail(Nothing, e.Rows)

                If result.StatusCode = eStatusResult.EXCEPTION Then
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudieron cargar los datos a la rejilla"
                    AsyncLoader(False)
                    Me.Cursor = System.Windows.Forms.Cursors.Default
                    Exit Sub
                End If

                Dim listErrors = New List(Of String)
                If ListBankDetail IsNot Nothing AndAlso ListBankDetail.Count > 0 Then
                    For Each item In result.ObjectEmbbeded
                        ListBankDetail.Add(item)
                    Next
                Else
                    ListBankDetail = result.ObjectEmbbeded
                End If

                INDgcBankDetail.DataSource = ListBankDetail
                INDgcBankDetail.RefreshDataSource()
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                        transparent.ShowDialog(Me)
                    End Using
                End If
                Me.Cursor = System.Windows.Forms.Cursors.Default

            End Using
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pegar información en la rejilla Reglas de reconocimento automatico
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Async Sub IndigoGridControl2_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl2.PasteToGrid
        If sender.Equals(INDgcBankAutomaticRecognitionRulesDetail) Then
            AsyncLoader(True)
            If e.Rows.Count = 0 Then
                AsyncLoader(False)
                Exit Sub
            End If
            If e.Rows(0).Item(0).Contains("Código Reglas de reconocimento automatico") Then
                e.Rows.Remove(e.Rows.ElementAt(0))
            End If

            Using model As New MBank(MyTag)
                Dim result = Await model.SetBankAutomaticRecognitionRulesFromCopyandPaste(e.Rows)
                If result.MessageResult.Count > 0 Then
                    Using formulario As New FrmListErrors(result.MessageResult)
                        formulario.StartPosition = FormStartPosition.CenterParent
                        Dim transparent As New FrmTransparent(formulario, False)
                        transparent.ShowDialog(Me)
                    End Using
                End If
                If ListBankAutomaticRecognitionRulesDetail Is Nothing Then
                    ListBankAutomaticRecognitionRulesDetail = New List(Of Domain.Payroll.Entities.BankAutomaticRecognitionRules)
                End If
                ListBankAutomaticRecognitionRulesDetail.AddRange(result.ObjectEmbbeded)

                INDgcBankAutomaticRecognitionRulesDetail.DataSource = Nothing
                INDgcBankAutomaticRecognitionRulesDetail.DataSource = ListBankAutomaticRecognitionRulesDetail

            End Using

            AsyncLoader(False)
        End If
    End Sub
#End Region

#Region "ImportFile"
    Private Async Sub INDBtnImportBankDetail_Click(sender As Object, e As EventArgs) Handles INDBtnImportBankDetail.Click
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"
        AsyncLoader(True)
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Try
                'obtengo la rura del archivo
                myStream = openFileDialog1.FileName
                If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                    Dim sddf = New SpreadsheetControl()
                    sddf.AllowDrop = False
                    sddf.LoadDocument(myStream)
                    Dim workBook As IWorkbook = sddf.Document

                    rows = workBook.Worksheets(0).Rows
                    If rows.LastUsedIndex = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                        AsyncLoader(False)
                        Exit Sub
                    End If
                    Using model As New MBank(MyTag)
                        listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                        SetRow(1, rows.LastUsedIndex + 1)

                        Dim result = Await model.SetCopyPasteOrImportFileBankDetail(listRows.ToList(), Nothing)

                        If result.StatusCode = eStatusResult.EXCEPTION Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se pudieron cargar los datos a la rejilla"
                            AsyncLoader(False)
                            Me.Cursor = System.Windows.Forms.Cursors.Default
                            Exit Sub
                        End If

                        Dim listErrors = New List(Of String)
                        If ListBankDetail IsNot Nothing AndAlso ListBankDetail.Count > 0 Then
                            For Each item In result.ObjectEmbbeded
                                ListBankDetail.Add(item)
                            Next
                        Else
                            ListBankDetail = result.ObjectEmbbeded
                        End If

                        INDgcBankDetail.DataSource = ListBankDetail
                        INDgcBankDetail.RefreshDataSource()
                        If result.MessageResult.Count > 0 Then
                            Using formulario As New FrmListErrors(result.MessageResult)
                                formulario.StartPosition = FormStartPosition.CenterParent
                                Dim transparent As New FrmTransparent(formulario, False)
                                Me.Cursor = System.Windows.Forms.Cursors.Default
                                transparent.ShowDialog(Me)
                            End Using
                        End If
                        Me.Cursor = System.Windows.Forms.Cursors.Default
                    End Using
                End If
                AsyncLoader(False)
            Catch ex As Exception
                Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                AsyncLoader(False)
            End Try
        Else
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar clic en el boton del importar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBtnImportBankAutomaticRecognitionRules_Click(sender As Object, e As EventArgs) Handles INDBtnImportBankAutomaticRecognitionRules.Click
        Await ImportFile()
    End Sub
#End Region

#Region "Methods and functions"

    ''' <summary>
    ''' Metodo que se encarga de importar el File
    ''' </summary>
    ''' <returns></returns>
    Private Async Function ImportFile() As Task
        'Configuramos el cuadro de dialogo para importar el archivo
        Dim openFileDialog1 As New OpenFileDialog()
        openFileDialog1.InitialDirectory = "c:\"
        openFileDialog1.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
        openFileDialog1.FilterIndex = 2
        openFileDialog1.RestoreDirectory = True
        openFileDialog1.Title = "Importar Archivo"

        'Si el usuario cancela la operación
        If openFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.Cancel Then
            Exit Function
        End If

        AsyncLoader(True)
        Try
            'obtengo la rura del archivo
            myStream = openFileDialog1.FileName
            If (myStream Is Nothing OrElse myStream.Trim().Equals(String.Empty)) Then 'Si la ruta es vacía
                Mensaje(EeventViewerImages.Advertencia) = "Ruta de archivo vacía"
                AsyncLoader(False)
                Exit Function
            End If

            AsyncLoader(True)
            Await LoadImportFileBankAutomaticRecognitionRules()

            If listErrosImportFile IsNot Nothing AndAlso listErrosImportFile.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = $"El archivo presento error en {listErrosImportFile.Count} registros"

                Dim ErrorsExcel As New SpreadsheetControl
                ErrorsExcel.CreateNewDocument()
                ErrorsExcel.Document.Worksheets.ActiveWorksheet = ErrorsExcel.Document.Worksheets(0)
                Dim worksheet As Worksheet = ErrorsExcel.Document.Worksheets.ActiveWorksheet

                worksheet.Cells(0, 0).Value = "Código Reglas de reconocimento automatico"
                worksheet.Cells(0, 1).Value = "Descripción de Transacción"
                worksheet.Cells(0, 2).Value = "Código Concepto de Nota"
                worksheet.Cells(0, 3).Value = "Código Centro de Costo"
                worksheet.DefaultColumnWidth = 250

                Dim rows = 1
                For Each dato As String() In listErrosImportFile
                    Dim Columns = 0
                    For Each item In dato
                        worksheet.Cells(rows, Columns).Value = item
                        Columns += 1
                    Next
                    rows += 1
                Next

                Dim fileName As String = System.IO.Path.GetTempPath() & INDtxtName.EditValue & ".xlsx"
                ErrorsExcel.SaveDocument(fileName)
                System.Diagnostics.Process.Start(fileName)
            Else
                INDgcBankAutomaticRecognitionRulesDetail.DataSource = Nothing
                INDgcBankAutomaticRecognitionRulesDetail.DataSource = ListBankAutomaticRecognitionRulesDetail.ToList()
            End If
        Catch ex As Exception
            Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
            AsyncLoader(False)
        End Try
        AsyncLoader(False)
    End Function

    ''' <summary>
    ''' Metod que se encargar de sacar la informacion del archivo y enviarla a servicos
    ''' </summary>
    ''' <returns></returns>
    Private Function LoadImportFileBankAutomaticRecognitionRules() As Task
        Return Task.Factory.StartNew(Sub()
                                         listErrosImportFile = New List(Of String())
                                         Dim ssc = New SpreadsheetControl()
                                         ssc.AllowDrop = False
                                         ssc.LoadDocument(myStream)

                                         Dim workBook As IWorkbook = ssc.Document
                                         rows = workBook.Worksheets(0).Rows
                                         If rows.LastUsedIndex <= 0 Then
                                             Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                                             Exit Sub
                                         End If

                                         Using model As New MBank(Me.Tag.ToString())
                                             listRows = New Concurrent.ConcurrentBag(Of ImportFileRow)()

                                             SetRow(1, rows.LastUsedIndex + 1)

                                             Dim result = model.SetBankAutomaticRecognitionRulesFromFile(listRows.ToList())
                                             If result.ListMessageResult IsNot Nothing AndAlso result.ListMessageResult.Any() Then
                                                 listErrosImportFile.AddRange(result.ListMessageResult)
                                             End If
                                             If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Any() Then
                                                 If ListBankAutomaticRecognitionRulesDetail Is Nothing Then
                                                     ListBankAutomaticRecognitionRulesDetail = New List(Of Domain.Payroll.Entities.BankAutomaticRecognitionRules)
                                                 End If
                                                 ListBankAutomaticRecognitionRulesDetail.AddRange(result.ObjectEmbbeded)
                                             End If
                                         End Using
                                     End Sub)
    End Function



    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmBankMetaData, Eform.InfoMetaData), Me.Bank.Code, Me.Bank.Name, INDGleThirdPartyId.Text, INDGleBankFile.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.Bank.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmBankMetaDataTitle, Eform.InfoMetaData), Me.Bank.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmBankMetaData, Eform.InfoMetaData), Me.Bank.Code, Me.Bank.Name, INDGleThirdPartyId.Text, INDGleBankFile.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmBankMetaDataTitle, Eform.InfoMetaData), Me.Bank.Code)
            Return Me._doc
        End If
    End Function

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()

        If record IsNot Nothing AndAlso record.Id > 0 AndAlso record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MBank(Me.Tag)
                Await Model.DeleteBlockRecord(record)
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
        INDlyPayrollBank.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Code = String.Empty
        NameBank = String.Empty
        ThirdPartyId = -1
        AchCodeBank = String.Empty
        INDTxtCENIT.EditValue = String.Empty
        INDTxtCenitVerification.EditValue = String.Empty
        BankFileCode = -1
        Status = True
        Bank = Nothing
        Status = True
        ListBankDetail = Nothing
        ListDeleteBankDetail = Nothing
        ListBankAutomaticRecognitionRulesDetail = Nothing
        ListDeleteBankAutomaticRecognitionRulesDetail = Nothing
        FlagBankDetail = Nothing
        INDgcBankDetail.DataSource = Nothing
        INDgcBankAutomaticRecognitionRulesDetail.DataSource = Nothing
        INDGleBankAccountRegistration.EditValue = 0
        CleanControlsPopup()
        CleanControlsPopupAutomaticRecognitionRules()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyPayrollBank.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleBankConcepts
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoBankConcepts()
        Using msearch As New MBusqueda
            BankConceptsXp = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).PayrollService.ListBankConciliationConcept()
            INDSleBankConcepts.Properties.DataSource = BankConceptsXp
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control NoteConceptsXp
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoNoteConcepts()
        Using msearch As New MBusqueda
            NoteConceptsXp = Infrastructure.Data.Xpo.XpoServiceEx.Instance(indigo.TransactionalContainer).TreasuryService.ListNoteConcept(status:=1)
            INDGleNotaConcept.Properties.DataSource = NoteConceptsXp
        End Using
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleCostCenter
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoCostCenter()
        Using msearch As New MBusqueda
            INDSleCostCenter.Properties.View.Columns.Clear()
            Dim mainAccount = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MainAccountsXpo)($"Id = { IdMainAccount}")
            If mainAccount.HandlesCostCenterRestriction Then
                CostCenterXpo = Presenter.ListMainAccountRestriction(mainAccountId:=IdMainAccount)
                INDSleCostCenter.Properties.DisplayMember = "CostCenterId.CodeName"
                INDSleCostCenter.Properties.ValueMember = "CostCenterId.Id"
            Else
                CostCenterXpo = Presenter.LisAllCostCenter()
                INDSleCostCenter.Properties.DisplayMember = "CodeName"
                INDSleCostCenter.Properties.ValueMember = "Id"
            End If
            INDSleCostCenter.Properties.View.Columns.AddVisible(INDSleCostCenter.Properties.DisplayMember, "Centro de Costo")
            INDSleCostCenter.Properties.PopulateViewColumns()
        End Using
    End Sub

    ''' <summary>
    ''' Esta función configura las acciones disponibles en una cuadrícula (grid) de DevExpress. 
    ''' </summary>
    Private Sub SetActionsGrid()
        Dim _listActions As New List(Of eAcciones)() From {{eAcciones.Edit}, {eAcciones.Remove}}
        IndigoGridView1.SetListAcction(viewBankDetail, _listActions)
        IndigoGridView2.SetListAcction(viewBankAutomaticRecognitionRulesDetail, _listActions)
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    ''' 
    Public WriteOnly Property ActionsOnControls As Boolean Implements IBank.ActionsOnControls
        Set(value As Boolean)
            INDlyPayrollBank.BeginUpdate()
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDtxtACHCode.Enabled = value
            INDTxtCENIT.Enabled = value
            INDTxtCenitVerification.Enabled = value
            INDGleThirdPartyId.Enabled = value
            INDGleBankFile.Enabled = value
            INDgcBankDetail.Enabled = value
            INDgcBankAutomaticRecognitionRulesDetail.Enabled = value
            INDpceBankEdit.Enabled = value
            INDEsbBankDetail.Enabled = value
            INDBtnImportBankDetail.Enabled = value
            INDpceBankAutomaticRecognitionRules.Enabled = value
            INDEsbBankAutomaticRecognitionRules.Enabled = value
            INDBtnImportBankAutomaticRecognitionRules.Enabled = value
            INDGleBankAccountRegistration.Enabled = value
            INDlyPayrollBank.EndUpdate()
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
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MBankPayroll(CStr(Me.Tag))
                    AsyncLoader(True)
                    Bank = Await Model.GetBankAsync(INDbteCode.Text.Trim)
                    INDlyPayrollBank.BeginUpdate()
                    If Bank IsNot Nothing AndAlso Bank.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Bank.Id))
                        With Bank
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            Code = .Code
                            NameBank = .Name
                            ThirdPartyId = .ThirdPartyId
                            AchCodeBank = .AchCode
                            INDTxtCENIT.EditValue = .CenitCode
                            INDTxtCenitVerification.EditValue = .CenitVerification
                            BankFileCode = .BankFileCode
                            Status = .State
                            BankAccountRegistration = .BankAccountRegistration

                            ListBankDetail = .BankDetail.ToList
                            For Each itemDetail In ListBankDetail
                                With BankDetail
                                    INDSleBankConcepts.EditValue = itemDetail.BankId
                                    Dim bankConcepts = Model.GetBankConciliationConceptById(itemDetail.BankConciliationConceptsId)
                                    If bankConcepts IsNot Nothing Then
                                        itemDetail.Code = bankConcepts.Code
                                        itemDetail.Name = bankConcepts.Name
                                    End If
                                End With
                            Next
                            ListBankAutomaticRecognitionRulesDetail = .BankAutomaticRecognitionRules.ToList
                            INDgcBankAutomaticRecognitionRulesDetail.DataSource = .BankAutomaticRecognitionRules.ToList

                        End With

                        INDgcBankDetail.DataSource = Nothing
                        INDgcBankDetail.DataSource = ListBankDetail

                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Bank.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                New BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                    .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Bank.Id})
                                ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        Me.BarraBotones.SetDocuments(Bank.Id, Me.Tag.ToString(), Nothing, GetType(Domain.Payroll.Entities.Bank).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewBank()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = "El Banco no existe"
                            Code = String.Empty
                            INDbteCode.Focus()
                        End If
                    End If
                    INDlyPayrollBank.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        'If Code = String.Empty Then
        '    INDbteCode.Focus()
        '    ValidateControls = False
        '    Exit Function
        'End If
        If NameBank = String.Empty Then
            INDtxtName.Focus()
            ValidateControls = False
            Exit Function
        End If

        If INDGleThirdPartyId.Text = "" Then
            INDGleThirdPartyId.Focus()
            ValidateControls = False
            Exit Function
        End If
        If AchCodeBank = String.Empty Then
            INDtxtACHCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If BankFileCode = Nothing Then
            INDGleBankFile.Focus()
            ValidateControls = False
            Exit Function
        End If

    End Function

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Bank
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameBank
            .ThirdPartyId = ThirdPartyId
            .AchCode = AchCodeBank
            .CenitCode = INDTxtCENIT.EditValue
            .CenitVerification = INDTxtCenitVerification.EditValue
            .BankFileCode = BankFileCode
            .BankAccountRegistration = BankAccountRegistration
            .BankDetail.Clear()
            If ListBankDetail IsNot Nothing Then
                For Each itemdetail As Domain.Payroll.Entities.BankDetail In ListBankDetail
                    .BankDetail.Add(itemdetail)
                Next
            End If

            If ListDeleteBankDetail IsNot Nothing Then
                For Each itemdetail As Domain.Payroll.Entities.BankDetail In ListDeleteBankDetail
                    .BankDetail.Add(itemdetail)
                Next
            End If

            If ListBankAutomaticRecognitionRulesDetail IsNot Nothing Then
                For Each itemdetail As Domain.Payroll.Entities.BankAutomaticRecognitionRules In ListBankAutomaticRecognitionRulesDetail
                    .BankAutomaticRecognitionRules.Add(itemdetail)
                Next
            End If

            If ListDeleteBankAutomaticRecognitionRulesDetail IsNot Nothing Then
                For Each itemdetail As Domain.Payroll.Entities.BankAutomaticRecognitionRules In ListDeleteBankAutomaticRecognitionRulesDetail
                    .BankAutomaticRecognitionRules.Add(itemdetail)
                Next
            End If

            If .Id > 0 Then
                .MarkAsModified()
            End If

        End With
    End Sub

    ''' <summary>
    ''' Esta función parece realizar un procesamiento paralelo en una colección de filas de datos provenientes de una hoja de cálculo.
    ''' </summary>
    ''' <param name="indexSend"></param>
    ''' <param name="indexEnd"></param>
    Private Sub SetRow(indexSend As Integer, indexEnd As Integer)
        Dim objLock As New Object()
        Parallel.For(indexSend, indexEnd, Sub(x)
                                              SyncLock objLock
                                                  listRows.Add(New ImportFileRow With {.IndexRow = x + 1, .Row = rows.Item(x).SpreadsheetRowToList(4)})
                                              End SyncLock
                                          End Sub)
    End Sub

    ''' <summary>
    ''' esta función asincrónica crea un nuevo objeto de banco y realiza acciones relacionadas con la preparación de controles y la barra de herramientas
    ''' </summary>
    ''' <returns></returns>
    Public Async Function NewBank() As Task
        Bank = New Domain.Payroll.Entities.Bank() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.TreasurySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MCommonTreasury(CStr(Me.Tag))
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

#End Region

#Region "Bar buttons events"

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
        Buscar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
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

    ''' <summary>
    ''' esta función maneja el cambio de unidad operativa en el contexto de la creación de secuencias,
    ''' verifica si la unidad operativa seleccionada es válida para las configuraciones de secuencia
    ''' actuales y ajusta la barra de herramientas según la situación.
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.TreasurySequenceDetail IsNot Nothing Then
                If Not Me._sequence.TreasurySequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub
#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyPayrollBank.ShowCustomizationForm()
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
            INDlyPayrollBank.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            'Ejecuatamos la consulta
            Using Model As New MBank(Me.Tag)
                Dim dsFields As DataSet = Model.GetFieldsNULL()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyPayrollBank.Items.Count - 1
                        INDlyPayrollBank.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyPayrollBank.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyPayrollBank.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyPayrollBank.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs)
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyPayrollBank.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyPayrollBank.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyPayrollBank.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

#Region "ContexMenuActions"

    Private Sub BtnAdd_Click(sender As Object, e As EventArgs) Handles BtnAdd.Click
        If ValidateBankConcept() Then
            AddBankDetail()
        End If
    End Sub

    ''' <summary>
    ''' evento que se disapra a dar clic en boton agregar del popup de 
    ''' regla de reconocimento automaticas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BtnAddAutomaticRecognitionRulesEdit_Click(sender As Object, e As EventArgs) Handles BtnAddAutomaticRecognitionRulesEdit.Click
        If ValidateBankAutomaticRecognitionRules() Then
            AddAutomaticRecognitionRulesEdit()
        End If
    End Sub

    Private Function ValidateBankConcept() As Boolean

        Dim errors As New Text.StringBuilder
        If INDTxtExtractCode.EditValue Is Nothing Or INDTxtExtractCode.Text = "" Then
            errors.AppendLine("Código de Extracto vacio")
        End If

        If errors.Length = 0 Then
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Se valido los campos requeridos en el popup de
    ''' regla de reconocimento automaticas
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateBankAutomaticRecognitionRules() As Boolean

        Dim errors As New Text.StringBuilder
        If TypeOfItemPendingInReconciliation = 0 Then
            errors.AppendLine("No ha seleccionado Tipo de Partida Pendiente en Conciliación")
        End If

        If INDTxtDescriptionTransaction.EditValue Is Nothing Or INDTxtDescriptionTransaction.Text = "" Then
            errors.AppendLine("Descripción de transacción esta vació")
        End If

        If errors.Length = 0 Then
            Return True
        Else
            Mensaje(EeventViewerImages.Advertencia) = errors.ToString()
            Return False
        End If
    End Function

    Private Sub IndigoGridViewBankDetail_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions, IndigoGridView1.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditBankDetail()
            Case "Remove"
                DeleteBankDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara cuando se da clic en alguna accion de la rejilla 
    ''' de regla de reconocimento automaticas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridViewBankAutomaticRecognitionRulesDetail_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions, IndigoGridView2.Click_ButtonAction
        Dim senderTag = sender.Tag.ToString
        If TypeOf sender Is DevExpress.XtraEditors.SimpleButton Then
            senderTag = DirectCast(sender, DevExpress.XtraEditors.SimpleButton).Tag.ToString
        End If

        Select Case (senderTag)
            Case "Edit"
                EditBankAutomaticRecognitionRulesDetail()
            Case "Remove"
                DeleteBankAutomaticRecognitionRulesDetail()
        End Select
    End Sub

    Private Sub EditBankDetail()
        LoadXpoBankConcepts()
        FlagBankDetail = True
        BankDetail = CType(viewBankDetail.GetFocusedRow, Domain.Payroll.Entities.BankDetail)
        With BankDetail
            Me.INDTxtExtractCode.EditValue = .ExtractCode
            Me.INDTxtDetailBank.EditValue = .Detail
            Me.INDSleBankConcepts.EditValue = .BankConciliationConceptsId
            LoadXpoBankConcepts()
            INDpceBankEdit.ShowPopup()
        End With
        INDTxtExtractCode.Focus()

    End Sub

    ''' <summary>
    ''' edita los datos de la rejilla de regla de reconocimento automaticas
    ''' </summary>
    Private Sub EditBankAutomaticRecognitionRulesDetail()

        FlagBankDetail = True
        BankAutomaticRecognitionRules = CType(viewBankAutomaticRecognitionRulesDetail.GetFocusedRow, Domain.Payroll.Entities.BankAutomaticRecognitionRules)
        With BankAutomaticRecognitionRules

            TypeOfItemPendingInReconciliation = .TypeOfItemPendingInReconciliation
            Me.INDTxtDescriptionTransaction.EditValue = .DescriptionTransaction
            Me.INDGleNotaConcept.EditValue = .NoteConceptsId
            Me.INDGleNotaConcept.Properties.NullText = .NoteConceptCodeName
            Me.INDTxtAccountAccounting.EditValue = .MainAccountsId
            Me.INDTxtAccountAccounting.Text = .AccountingAccountNumberName
            Me.INDSleCostCenter.EditValue = .CostCenterId
            Me.INDSleCostCenter.Properties.NullText = .CostCenterCodeName
            INDpceBankAutomaticRecognitionRules.ShowPopup()
        End With

    End Sub



    Private Sub DeleteBankDetail()
        Dim Ild As Domain.Payroll.Entities.BankDetail = CType(viewBankDetail.GetFocusedRow, Domain.Payroll.Entities.BankDetail)
        If Ild.Id <> 0 Then
            If ListDeleteBankDetail Is Nothing Then
                ListDeleteBankDetail = New List(Of Domain.Payroll.Entities.BankDetail)
            End If
            Ild.MarkAsDeleted()
            ListDeleteBankDetail.Add(Ild)
        End If
        ListBankDetail.Remove(Ild)
        INDgcBankDetail.DataSource = Nothing
        INDgcBankDetail.DataSource = ListBankDetail
    End Sub

    ''' <summary>
    ''' Elimina los datos de la rejilla de regla de reconocimento automaticas
    ''' </summary>
    Private Sub DeleteBankAutomaticRecognitionRulesDetail()
        Dim Ild As Domain.Payroll.Entities.BankAutomaticRecognitionRules = CType(viewBankAutomaticRecognitionRulesDetail.GetFocusedRow, Domain.Payroll.Entities.BankAutomaticRecognitionRules)
        If Ild.Id <> 0 Then
            If ListDeleteBankAutomaticRecognitionRulesDetail Is Nothing Then
                ListDeleteBankAutomaticRecognitionRulesDetail = New List(Of Domain.Payroll.Entities.BankAutomaticRecognitionRules)
            End If
            Ild.MarkAsDeleted()
            ListDeleteBankAutomaticRecognitionRulesDetail.Add(Ild)
        End If
        ListBankAutomaticRecognitionRulesDetail.Remove(Ild)
        INDgcBankAutomaticRecognitionRulesDetail.DataSource = Nothing
        INDgcBankAutomaticRecognitionRulesDetail.DataSource = ListBankAutomaticRecognitionRulesDetail
    End Sub

    Private Sub AddBankDetail()
        Using model As New MBankPayroll(Me.Tag)

            Dim BankConcepts = model.GetBankConciliationConceptById(INDSleBankConcepts.EditValue)
            If FlagBankDetail = False Then
                Dim BankDetail As New Domain.Payroll.Entities.BankDetail

                With BankDetail
                    .BankConciliationConceptsId = BankConcepts.Id
                    .Code = BankConcepts.Code
                    .Name = BankConcepts.Name
                    .ExtractCode = INDTxtExtractCode.EditValue
                    .Detail = INDTxtDetailBank.EditValue

                End With
                If ListBankDetail Is Nothing Then
                    ListBankDetail = New List(Of Domain.Payroll.Entities.BankDetail)
                End If
                ListBankDetail.Add(BankDetail)

            Else
                With BankDetail
                    .BankConciliationConceptsId = BankConcepts.Id
                    .Code = BankConcepts.Code
                    .Name = BankConcepts.Name
                    .ExtractCode = INDTxtExtractCode.EditValue
                    .Detail = INDTxtDetailBank.EditValue
                End With
            End If


            INDgcBankDetail.DataSource = ListBankDetail
            INDgcBankDetail.RefreshDataSource()
            CleanControlsPopup()

        End Using

    End Sub

    ''' <summary>
    ''' Agrega la informacion selecionada del popup a la 
    ''' rejilla regla de reconocimento automaticas
    ''' </summary>
    Private Sub AddAutomaticRecognitionRulesEdit()
        Using model As New MBankPayroll(Me.Tag)

            If Not FlagBankDetail Then
                Dim BankAutomaticRecognitionRules As New Domain.Payroll.Entities.BankAutomaticRecognitionRules
                With BankAutomaticRecognitionRules
                    .TypeOfItemPendingInReconciliation = TypeOfItemPendingInReconciliation
                    .DescriptionTransaction = INDTxtDescriptionTransaction.EditValue
                    .NoteConceptsId = INDGleNotaConcept.EditValue
                    .NoteConceptCodeName = INDGleNotaConcept.Text
                    .MainAccountsId = IdMainAccount
                    .AccountingAccountNumberName = INDTxtAccountAccounting.Text
                    .CostCenterId = INDSleCostCenter.EditValue
                    .CostCenterCodeName = INDSleCostCenter.Text


                End With
                If ListBankAutomaticRecognitionRulesDetail Is Nothing Then
                    ListBankAutomaticRecognitionRulesDetail = New List(Of Domain.Payroll.Entities.BankAutomaticRecognitionRules)
                End If
                ListBankAutomaticRecognitionRulesDetail.Add(BankAutomaticRecognitionRules)

            Else
                With BankAutomaticRecognitionRules
                    .TypeOfItemPendingInReconciliation = TypeOfItemPendingInReconciliation
                    .DescriptionTransaction = INDTxtDescriptionTransaction.EditValue
                    .NoteConceptsId = INDGleNotaConcept.EditValue
                    .NoteConceptCodeName = INDGleNotaConcept.Text
                    .MainAccountsId = IdMainAccount
                    .AccountingAccountNumberName = INDTxtAccountAccounting.Text
                    .CostCenterId = INDSleCostCenter.EditValue
                    .CostCenterCodeName = INDSleCostCenter.Text
                End With
            End If


            INDgcBankAutomaticRecognitionRulesDetail.DataSource = ListBankAutomaticRecognitionRulesDetail
            INDgcBankAutomaticRecognitionRulesDetail.RefreshDataSource()
            CleanControlsPopupAutomaticRecognitionRules()

        End Using

    End Sub

    Private Sub CleanControlsPopup()
        FlagBankDetail = False
        Me.INDSleBankConcepts.EditValue = Nothing
        Me.INDTxtExtractCode.EditValue = Nothing
        Me.INDTxtDetailBank.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Limpia el Popup de regla de reconocimento automaticas
    ''' </summary>
    Private Sub CleanControlsPopupAutomaticRecognitionRules()
        FlagBankDetail = False

        TypeOfItemPendingInReconciliation = 0
        Me.INDTxtDescriptionTransaction.EditValue = Nothing
        Me.INDGleNotaConcept.EditValue = Nothing
        Me.INDGleNotaConcept.Properties.DataSource = Nothing
        Me.INDGleNotaConcept.Properties.NullText = Nothing
        Me.INDTxtAccountAccounting.EditValue = Nothing
        Me.INDTxtAccountAccounting.Properties.NullText = Nothing
        Me.INDSleCostCenter.EditValue = Nothing
        Me.INDSleCostCenter.Properties.NullText = Nothing
        Me.INDSleCostCenter.Properties.DataSource = Nothing
    End Sub

#End Region

#Region "CloseUp"
    ''' <summary>
    ''' Evento que se dispara al cerrar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBankEdit_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBankEdit.CloseUp
        If FlagBankDetail = True Then
            CleanControlsPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cerrar el popup de regla de reconocimento automaticas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceBankAutomaticRecognitionRules_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceBankAutomaticRecognitionRules.CloseUp
        If FlagBankDetail = True Then
            CleanControlsPopupAutomaticRecognitionRules()
        End If
    End Sub

    Private Sub INDSleBankConcepts_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleBankConcepts.EditValueChanged
        LoadXpoBankConcepts()
    End Sub

    ''' <summary>
    ''' Evento que se dispare al selecionar la nota de concepto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleNotaConcept_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleNotaConcept.EditValueChanged
        If INDGleNotaConcept.EditValue IsNot Nothing Then
            Dim noteConcept = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of NoteConceptXpo)($"Id = { INDGleNotaConcept.EditValue}")
            If noteConcept IsNot Nothing Then
                INDTxtAccountAccounting.EditValue = noteConcept.IdMainAccount?.NumberName
                IdMainAccount = noteConcept.IdMainAccount?.Id
                Dim mainAccount = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of MainAccountsXpo)($"Id = { IdMainAccount}")
                If mainAccount.HandlesCostCenter Then
                    LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If
                INDSleCostCenter.Properties.NullText = Nothing
                INDSleCostCenter.Properties.DataSource = Nothing
            End If
        End If
    End Sub
#End Region

End Class

''' <summary>
''' Clase que le dará valores al campo Tipos de Partida Pendiente en Conciliación
''' </summary>
Public Class TypesOfItemPendingInReconciliation
    Public Property Id As Integer
    Public Property Description As String
End Class