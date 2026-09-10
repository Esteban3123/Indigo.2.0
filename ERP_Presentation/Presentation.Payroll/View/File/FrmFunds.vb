'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 17-04-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports Domain.Base.Entities
Imports Presentation.Common
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo

#End Region
''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario Fondos 
''' </summary>
''' <remarks></remarks>
Public Class FrmFunds
    Implements IFunds

#Region "Variables globales, load, y propiedades"
    Dim dtFieldsCustomizables As DataTable

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord

    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' Varaible que contiene la entidad de los fondos
    ''' </summary> 
    Dim Funds As Fund
    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFunds(MyBase.Tag)
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFunds
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As Domain.Entities.PayrollSequence

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Propiedad que contiene el codigo del fondo
    ''' </summary>
    Public Property CodeFund As String Implements IFunds.CodeFund
        Get
            If (INDBteCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBteCode.Text
            End If
        End Get
        Set(value As String)
            INDBteCode.Text = value
        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IFunds.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property
    Public ReadOnly Property MyTag As Object Implements IFunds.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    Public Property Sequence As Domain.Entities.PayrollSequence Implements IFunds.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As Domain.Entities.PayrollSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.PayrollSequenceDetail In Me._sequence.PayrollSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el nit o identificacion del tercero
    ''' </summary>
    Public Property ThirdPartyId As String Implements IFunds.ThirdPartyId
        Get
            Return INDSLUpThirdPartyId.EditValue
        End Get
        Set(value As String)
            INDSLUpThirdPartyId.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el nombre del tercero
    ''' </summary>
    Public Property NameCustomer As String Implements IFunds.NameCustomer
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el tipo del fondo, donde 0 es publico y 1 privado
    ''' </summary>
    Public Property TypeFund As String Implements IFunds.TypeFund
        Get
            If Not INDRbTypeFund.EditValue Then
                Return "0"
            Else
                Return "1"
            End If
        End Get
        Set(value As String)
            If value = "0" Then
                INDRbTypeFund.EditValue = False
            ElseIf value = "1" Then
                INDRbTypeFund.EditValue = True
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el check cesantias
    ''' </summary>
    Public Property Unemployment As Boolean Implements IFunds.Unemployment
        Get
            If INDChkFund.Items.Item(0).CheckState = System.Windows.Forms.CheckState.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDChkFund.Items.Item(0).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el check salud
    ''' </summary>
    Public Property Health As Boolean Implements IFunds.Health
        Get
            If INDChkFund.Items.Item(1).CheckState = System.Windows.Forms.CheckState.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDChkFund.Items.Item(1).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el check pension
    ''' </summary>
    Public Property Pension As Boolean Implements IFunds.Pension
        Get
            If INDChkFund.Items.Item(2).CheckState = System.Windows.Forms.CheckState.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDChkFund.Items.Item(2).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el check Riesgo
    ''' </summary>
    Public Property Risk As Boolean Implements IFunds.Risk
        Get
            If INDChkFund.Items.Item(3).CheckState = System.Windows.Forms.CheckState.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDChkFund.Items.Item(3).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el check IIS
    ''' </summary>
    Public Property CompensationFund As Boolean Implements IFunds.IIS
        Get
            If INDChkFund.Items.Item(4).CheckState = System.Windows.Forms.CheckState.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDChkFund.Items.Item(4).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad ACCAI para  Ley 2381 de 2024
    ''' </summary>
    ''' <returns></returns>
    Public Property ACCAI As Boolean Implements IFunds.ACCAI
        Get
            If INDChkFund.Items.Item(5).CheckState = System.Windows.Forms.CheckState.Checked Then
                Return True
            Else
                Return False
            End If
        End Get
        Set(value As Boolean)
            If value = True Then
                INDChkFund.Items.Item(5).CheckState = System.Windows.Forms.CheckState.Checked
            End If
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el codigo MiniSalud
    ''' </summary>
    Public Property CodeHealth As String Implements IFunds.CodeHealth
        Get
            Return INDtxtMinistryCode.Text()
        End Get
        Set(value As String)
            INDtxtMinistryCode.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el estado de el fondo
    ''' </summary>
    Public Property Status As Boolean Implements IFunds.StateFund
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
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFunds.ActionsOnControls
        Set(value As Boolean)
            INDlyFunds.BeginUpdate()
            INDBteCode.Enabled = Not value
            INDSLUpThirdPartyId.Enabled = value
            INDtxtName.Enabled = value
            INDtxtMinistryCode.Enabled = value
            INDChkFund.Enabled = value
            INDChkFund.Enabled = value
            INDRbTypeFund.Enabled = value
            INDSpPeriodDays.Enabled = value
            INDSlMainAccountReceivable.Enabled = value
            INDlyFunds.EndUpdate()
            If value = True Then
                INDSLUpThirdPartyId.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el datasource del grid look up terceros
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property DataSourceThirdParty As List(Of Domain.Entities.ThirdParty) Implements IFunds.DataSourceThirdParty
        Set(value As List(Of Domain.Entities.ThirdParty))
            'INDGleThirdPartyId.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' datasource de xpo de clientes
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DatasourceThirdPartyXpo As XPInstantFeedbackSource
    ''' <summary>
    ''' Metodo que carga el load del formulario 
    ''' </summary>
    Private Async Sub FrmFunds_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '****Inicializar variables*****'
        'Me._doc = Nothing
        'Me._funct = AddressOf GenerateDoc

        'Me.INDSLUpThirdPartyId.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSLUpThirdPartyId_KeyDown
        'Me.INDSLUpThirdPartyId.View.OptionsView.ShowGroupPanel = False

        'Cargamos de manera asincrona definiciones del funcional
        'PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCusearchModestomizables\", Me.Name, "\ModuloPayrollFunds.", Me.Name, ".xml")
        'LoadhronousDefinitions = New BackgroundWorker
        'If LoadhronousDefinitions.IsBusy = False Then
        '    LoadhronousDefinitions.RunWorkerAsync()
        'End If
        'Presenter = New PFunds(Me)
        'INDGleThirdPartyId.Properties.DataSource = Presenter.initializes()

        'Using Model As New MThirdParty(MThirdParty.TAG)
        '    INDGleThirdPartyId.Properties.DataSource = Infrastructure.CrossCutting.Base.eDataSource.ListAllThirdParty
        'End Using

        'Await Presenter.initializes()
        'LoadStatus()
        'Deshacer()
        'GleSize()


        'Me.LayoutControls.SetIsCustomizable(Me.INDLcBillingGroup, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance
        Presenter = New PFunds(Me)
        Presenter.GetSequence()

        Me.INDSLUpThirdPartyId.FuncQueryOnKeyEnterPressed = AddressOf Me.INDSLUpThirdPartyId_KeyDown
        Me.INDSLUpThirdPartyId.View.OptionsView.ShowGroupPanel = False

        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCusearchModestomizables\", Me.Name, "\ModuloPayrollFunds.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        'Presenter.LoadDefinitionLayout()
        LoadStatus()
        Deshacer()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded

        Me.ViewModeEditHold = True
        If Me.Funds IsNot Nothing AndAlso Me.Funds.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBteCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBteCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty

    End Sub

    Dim searchMode As Boolean = False
#End Region

#Region "Metodos Funciones Propiedades"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.Funds.Code, Me.Funds.Name, INDSLUpThirdPartyId.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.Funds.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.Funds.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFundMetaData, Eform.InfoMetaData), Me.Funds.Code, Me.Funds.Name, INDSLUpThirdPartyId.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFundMetaDataTitle, Eform.InfoMetaData), Me.Funds.Code)
            Return Me._doc
        End If
    End Function

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
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Public Sub GleSize()
        'INDSLUpThirdPartyId.Properties.PopupFormSize = New Drawing.Size(500, 200)
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Funds
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = CodeFund
            .ThirdPartyId = ThirdPartyId
            .Name = NameCustomer
            .Type = TypeFund
            .Unemployment = Unemployment
            .Health = Health
            .Pension = Pension
            .Risk = Risk
            .CompensationFund = CompensationFund
            .MinistryCode = CodeHealth
            .Term = CInt(INDSpPeriodDays.EditValue)
            .MainAccountReceivableId = CInt(INDSlMainAccountReceivable.EditValue)
            .PensionACCAI = ACCAI
        End With
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        'Me.BarraBotones.StatusRecordVisible = True
        'StateFund = True
        'AsyncLoader(True)
        'Using Model As New MFunds(MyBase.Tag)
        '    Funds = Await Model.GetFundsAsync(INDBteCode.Text)
        'End Using
        'If Not Funds Is Nothing Then
        '    If Funds.Id > 0 Then
        '        Dim result = Await Model.GetBlockRecord(Me.Tag, Funds.Id)
        '        With Funds
        '            LogicaBotonActualizar(True)
        '            Me.BarraBotones.PrepareToolbar(eAction.UpdateOrDelete)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Funds.CreationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Funds.CreationDate)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Funds.ModificationUser)
        '            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), IIf(Funds.ModificationDate Is Nothing, Nothing, Funds.ModificationDate))
        '            CodeFund = .Code
        '            ThirdPartyId = .ThirdPartyId
        '            NameCustomer = .Name
        '            TypeFund = .Type
        '            Unemployment = .Unemployment
        '            Pension = .Pension
        '            Health = .Health
        '            Risk = .Risk
        '            CompensationFund = .CompensationFund
        '            CodeHealth = .MinistryCode
        '            StateFund = .State
        '            INDSLUpThirdPartyId.DisplayNullText = .NameThirdPartyFund
        '        End With
        '        Me.GetDocumentIndexed(Me.Tag & "_" & Me.Funds.Code)
        '        If result.Id = 0 Then
        '            Me.BarraBotones.SetDocuments(Funds.Id)
        '            Dim state = New Domain.Base.Entities.ObjectChangeTracker
        '            state.State = Domain.Base.Entities.ObjectState.Added
        '            record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Funds.Id}
        '            Dim operation = Await Model.SaveBlockRecord(record)
        '            record = operation.ObjectEmbbeded
        '        Else
        '            record = result
        '            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), result.CodUser, result.NameUser, result.BlockDate)
        '            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, result.CodUser)
        '        End If
        '    Else
        '        LogicaBotonActualizar(False)
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        '    End If
        'Else
        '    Funds = New Fund
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
        'End If
        'AsyncLoader(False)
        'ActionsOnControls = True

        If Not String.IsNullOrEmpty(CodeFund) AndAlso Not String.IsNullOrWhiteSpace(CodeFund) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MFunds(CStr(Me.Tag))
                    AsyncLoader(True)
                    Funds = Await Model.GetFundsAsync(INDBteCode.Text)
                    INDlyFunds.BeginUpdate()
                    If Funds IsNot Nothing AndAlso Funds.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        'Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                        record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(Funds.Id))
                        With Funds
                            LayoutControls.SetCustomFieldsValue(.CustomProperties)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                            Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                            CodeFund = .Code
                            ThirdPartyId = .ThirdPartyId
                            NameCustomer = .Name
                            TypeFund = .Type
                            Unemployment = .Unemployment
                            Pension = .Pension
                            Health = .Health
                            Risk = .Risk
                            CompensationFund = .CompensationFund
                            CodeHealth = .MinistryCode
                            Status = .State
                            ACCAI = If(.PensionACCAI Is Nothing, False, .PensionACCAI)
                            INDSLUpThirdPartyId.DisplayNullText = .NameThirdPartyFund
                            INDSpPeriodDays.EditValue = .Term
                            INDSlMainAccountReceivable.EditValue = .MainAccountReceivableId
                            INDSlMainAccountReceivable.Properties.NullText = .NameAccountReceivable
                            'Llenar Entidad
                        End With
                        'Llenar NullText
                        Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.Funds.Code)
                        If record.Id = 0 Then
                            record = (Await Model.SaveBlockRecord(
                                    New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Funds.Id})
                                    ).ObjectEmbbeded
                        Else
                            Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                            Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                        End If
                        'Me.BarraBotones.SetDocuments(ObjEntity.Id)
                        Me.BarraBotones.SetDocuments(Funds.Id, Me.Tag.ToString(), Nothing, GetType(Fund).Name)
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        AsyncLoader(False)
                        ActionsOnControls = True
                        'End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewFund()
                        Else
                            'Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Me.Mensaje(EeventViewerImages.Advertencia) = "Código de Fondos no existe."
                            Code = String.Empty
                            INDBteCode.Focus()
                        End If
                    End If
                    INDlyFunds.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function
    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()


        INDlyFunds.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        Status = True
        INDBteCode.Text = String.Empty
        INDtxtName.Text = String.Empty
        INDSLUpThirdPartyId.DisplayNullText = String.Empty
        INDSLUpThirdPartyId.EditValue = Nothing
        INDtxtMinistryCode.Text = String.Empty
        INDChkFund.Items.Item(0).CheckState = System.Windows.Forms.CheckState.Unchecked
        INDChkFund.Items.Item(1).CheckState = System.Windows.Forms.CheckState.Unchecked
        INDChkFund.Items.Item(2).CheckState = System.Windows.Forms.CheckState.Unchecked
        INDChkFund.Items.Item(3).CheckState = System.Windows.Forms.CheckState.Unchecked
        INDChkFund.Items.Item(4).CheckState = System.Windows.Forms.CheckState.Unchecked
        INDChkFund.Items.Item(5).CheckState = System.Windows.Forms.CheckState.Unchecked
        INDRbTypeFund.EditValue = False
        INDSlMainAccountReceivable.EditValue = Nothing
        INDSpPeriodDays.EditValue = 0
        INDSlMainAccountReceivable.Properties.NullText = String.Empty
        'Limpiar controles
        Funds = Nothing

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyFunds.EndUpdate()
        DeleteBlockedRecord()
    End Sub
    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If INDBteCode.Text = String.Empty Then
            INDBteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDtxtName.Text = String.Empty Then
            INDtxtName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDChkFund.Items.Item(0).CheckState = System.Windows.Forms.CheckState.Unchecked And INDChkFund.Items.Item(1).CheckState = System.Windows.Forms.CheckState.Unchecked And INDChkFund.Items.Item(2).CheckState = System.Windows.Forms.CheckState.Unchecked And INDChkFund.Items.Item(3).CheckState = System.Windows.Forms.CheckState.Unchecked And INDChkFund.Items.Item(4).CheckState = System.Windows.Forms.CheckState.Unchecked And INDChkFund.Items.Item(5).CheckState = System.Windows.Forms.CheckState.Unchecked Then
            INDChkFund.Focus()
            ValidateControls = False
            Exit Function
        End If
        If INDtxtMinistryCode.Text = String.Empty Then
            ValidateControls = False
            Exit Function
        End If
        If INDSlMainAccountReceivable.Text = String.Empty Then
            ValidateControls = False
            Exit Function
        End If
    End Function
    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' Evento para consultar el fondo en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteDepCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
        'If Not String.IsNullOrEmpty(INDBteCode.Text.ToString) Then
        '    If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '        Await LoadControls()
        '        If INDBteCode.Enabled = False Then
        '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        '            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
        '        End If
        '        INDBteCode.Enabled = False
        '    End If
        'End If


        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(CodeFund.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(CodeFund) Then
                    Await Me.NewFund()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Async Function NewFund() As Task
        Funds = New Fund() With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.PayrollSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.CodeFund = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.CodeFund = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequensePayroll(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.CodeFund = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.CodeFund = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Handles the ButtonClick event of the INDGleThirdPartyId control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDGleThirdPartyId_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.initializes()
            End Using
        End If
    End Sub
#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Fondos.
    ''' </summary>
    Public Async Sub Nuevo() Implements IcrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewFund()
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Fondos.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Fondos.
    ''' </summary>
    Public Async Sub Eliminar() Implements IcrudBase.Eliminar
        'Dim result As ActionMessageResult(Of Fund)
        'If Funds IsNot Nothing Then
        '    If Funds.Id > 0 Then
        '        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
        '            Using Model As New MFunds(MyBase.Tag)
        '                AsyncLoader(True)
        '                result = Await Model.DeleteFundsAsync(Funds)
        '                If result.StateResult = True Then
        '                    Await Me.DeleteDocumentIndexed()
        '                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
        '                    AsyncLoader(False)
        '                    searchMode = False
        '                    Deshacer()
        '                Else
        '                    AsyncLoader(False)
        '                    If result.MessageResult.ElementAt(0).CodeMessage = "c-0000" Then
        '                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
        '                    Else
        '                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '                    End If
        '                End If
        '            End Using
        '        End If
        '    Else
        '        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        '    End If
        'Else
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneRegistroEliminar, Eform.Comunes)
        'End If

        'Valido si es un formulario Fundacional y si está Activo el sistema de Fundacionales
        If Utils.IsFoundational(Me.Tag, indigo) Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("DeleteFoundational")
            Exit Sub
        End If

        If Me.Funds IsNot Nothing AndAlso Me.Funds.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFunds(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteFundsAsync(Me.Funds)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDBteCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item Guardar del control de Fondos.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        'If ValidateControls() = False Then
        '    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
        '    Exit Sub
        'End If
        'AsyncLoader(True)
        'AssigningValues()
        'Using Model As New MFunds(MyBase.Tag)
        '    If Await Model.SaveFundsAsync(Funds) = True Then
        '        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
        '        If Funds.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
        '        ElseIf Funds.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Funds.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
        '            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
        '        End If
        '        AsyncLoader(False)
        '        searchMode = False
        '        Deshacer()
        '    Else
        '        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
        '        AsyncLoader(False)
        '    End If
        'End Using

        If Not ValidateControls() Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MFunds(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of Fund) = Await Model.SaveFundsAsync(Me.Funds, Me._idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If Funds.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me.Funds = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBteCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        If Not String.IsNullOrEmpty(Me.Funds.Code) Then
            Try
                Using model As New MFunds(Me.Tag)
                    Dim state As Boolean = Status = eActionsStatusRecords.Active
                    AsyncLoader(True)
                    Dim result As ActionResult(Of Fund) = Await model.ChangeState(Me.Funds.Code, state)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me.Funds = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDBteCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Funds
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
        INDBteCode.Text = ReturnValue
        If INDBteCode.Text <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Fondos.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        'If searchMode = False Then
        '    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'Else
        '    If indigo.UserViewMode = True Then
        '        Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        '    End If
        'End If
    End Sub
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
#End Region

#Region "bar buttons and events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        record = Nothing
        ExistDefinitionFront = Nothing
        Funds = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub

    ''' <summary>
    '''Evento load de la barra de fondos.
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
        searchMode = False
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
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Enabled Then
            INDBteCode.Focus()
        End If
    End Sub
    Private Sub BarraBotones_ChangeOperatingUnit(operatingUnit As Domain.Entities.OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.PayrollSequenceDetail IsNot Nothing Then
                If Not Me._sequence.PayrollSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
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
        INDlyFunds.ShowCustomizationForm()
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
            INDlyFunds.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyFunds.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MFunds(MyBase.Tag)
                Dim dsFields As DataSet = model.GetNullFields()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyFunds.Items.Count - 1
                        INDlyFunds.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyFunds.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyFunds.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyFunds.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyFunds.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyFunds.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyFunds.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyFunds.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFunds_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub


    Private Sub INDSLUpThirdPartyId_OpenFormButtonClick(sender As Object, e As EventArgs) Handles INDSLUpThirdPartyId.OpenFormButtonClick
        OpenForm(532, Nothing, True)
        LoadXpoThirdParty()
    End Sub

    ''' <summary>
    ''' metodo para Cargar el data source Del Control INDSleAccountStart
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadXpoThirdParty()
        Using msearch As New MBusqueda
            DatasourceThirdPartyXpo = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ThirdParty)
            INDSLUpThirdPartyId.Datasource = DatasourceThirdPartyXpo
        End Using
    End Sub

#Region "QueryPopUp"
    Private Sub INDSLUpThirdPartyId_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSLUpThirdPartyId.QueryPopUp
        If INDSLUpThirdPartyId.Datasource Is Nothing Then
            LoadXpoThirdParty()
        End If
    End Sub

    Private Sub INDSlMainAccountReceivable_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSlMainAccountReceivable.QueryPopUp
        If Me.INDSlMainAccountReceivable.Properties.DataSource Is Nothing Then
            Dim filter() As Object = {5, True}
            Using modelAccountsXPO As New MBusqueda
                INDSlMainAccountReceivable.Properties.DataSource = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"
    Private Sub INDSlMainAccountReceivable_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSlMainAccountReceivable.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPopupPUC With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Using msearch As New MBusqueda()
                Dim filter() As Object = {5, True}
                INDSlMainAccountReceivable.Properties.DataSource = msearch.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
            End Using
        End If
    End Sub
#End Region

    Private Async Function INDSLUpThirdPartyId_KeyDown(ByVal nit As String) As Task(Of Domain.Payroll.Entities.ThirdParty)
        Dim ThirdPartyTmp = Await Model.GetThirdPartyAsync(nit)
        If ThirdPartyTmp IsNot Nothing AndAlso ThirdPartyTmp.Id > 0 Then
            INDtxtName.Focus()
        Else
            Me.INDSLUpThirdPartyId.DisplayNullText = String.Empty
            Me.INDSLUpThirdPartyId.EditValue = Nothing
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.DebeSeleccionarTercero, Eform.Conciliation)
            INDSLUpThirdPartyId.Focus()
        End If
        Return CustomerTmp
    End Function

End Class