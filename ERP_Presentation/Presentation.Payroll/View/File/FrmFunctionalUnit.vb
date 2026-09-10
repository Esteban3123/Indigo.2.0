'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 24-04-2013
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
Imports Domain.Security.Entities
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports Domain.Base.Entities
Imports Presentation.Security.MVP
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Common
Imports DevExpress.Xpo
Imports Presentation.Common.MVP
Imports Presentation.CloudAgent
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository


#End Region

Public Class FrmFunctionalUnit
    Implements IFunctionalUnit

#Region "Variables globales, Propiedades y Load"

    Public Const NAME_MODULE As String = "Payroll"

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListUnitType As New List(Of Tuple(Of Integer, String))

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
    ''' Varaible que contiene la entidad de las unidades de negocio
    ''' </summary> 
    Dim FunctionalUnit As FunctionalUnit

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MFunctionalUnit(MyBase.Tag)
    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PFunctionalUnit
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker



    Private _idCurrentSequence As Long
    Private _idOperativeUnit As Integer

    Public ReadOnly Property MyTag As String Implements IFunctionalUnit.MyTag
        Get
            Return Me.Tag.ToString()
        End Get
    End Property


    Private _sequence As Domain.Entities.PayrollSequence
    Public Property Sequence As Domain.Entities.PayrollSequence Implements IFunctionalUnit.Sequence
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
    ''' Propiedad que contiene el codigo de la unidad funcional
    ''' </summary>
    Public Property Code As String Implements IFunctionalUnit.Code
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
    ''' <summary>
    ''' Propiedad que contiene el nombre de la unidad de negocio
    ''' </summary>
    Public Property NameFuncUnit As String Implements IFunctionalUnit.NameFuncUnit
        Get
            Return INDNameFuncUnit.Text
        End Get
        Set(value As String)
            INDNameFuncUnit.Text = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el id de la sucursal
    ''' </summary>
    Public Property BranchOfficeId As Integer Implements IFunctionalUnit.BranchOfficeId
        Get
            Return INDGleBranchOfficeFuncUnit.EditValue
        End Get
        Set(value As Integer)
            INDGleBranchOfficeFuncUnit.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Propiedad que contiene el id del centro de costo
    ''' </summary>
    Public Property CostCenterId As Integer Implements IFunctionalUnit.CostCenterId
        Get
            Return INDSearchLookCostCenter.EditValue
        End Get
        Set(value As Integer)
            INDSearchLookCostCenter.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que contiene el id del centro de costo
    ''' </summary>
    Public Property AccountingStructureId As Integer? Implements IFunctionalUnit.AccountingStructureId
        Get
            Return INDSearchLookAccountingStructure.EditValue
        End Get
        Set(value As Integer?)
            INDSearchLookAccountingStructure.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' propiedad que asigna el listado de las sucursales al control grid look up edit
    ''' </summary>
    Public WriteOnly Property DatasourceBranchOffice As List(Of BranchOffice) Implements IFunctionalUnit.DatasourceBranchOffice
        Set(value As List(Of BranchOffice))
            INDGleBranchOfficeFuncUnit.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que Contiene el estado de la unidad de negocio
    ''' </summary>
    Public Property StateFuncUnit As Boolean Implements IFunctionalUnit.StateFuncUnit
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
    ''' Obtiene o establece el tipo de unidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UnitType As Integer Implements IFunctionalUnit.UnitType
        Get
            Return INDsleUnitType.EditValue
        End Get
        Set(value As Integer)
            INDsleUnitType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la hora de entrega por servicio farmaceutico
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DeliveryTimePharmaService As DateTime? Implements IFunctionalUnit.DeliveryTimePharmaService
        Get
            Return INDTmeDeliveryTimePharmaService.EditValue
        End Get
        Set(value As DateTime?)
            INDTmeDeliveryTimePharmaService.EditValue = value
        End Set
    End Property
    'Property TurnDatasource As XPInstantFeedbackSource Implements IFunctionalUnit.TurnDatasource
    '    Get
    '        Return CType(INDsleTurno.Properties.DataSource, XPInstantFeedbackSource)
    '    End Get
    '    Set(value As XPInstantFeedbackSource)
    '        INDsleTurno.Properties.DataSource = value
    '    End Set
    'End Property
    '''' <summary>
    '''' Obtiene o establece los turnos
    '''' </summary>
    'Public Property TurnId As Integer Implements IFunctionalUnit.TurnId
    '    Get
    '        Return CType(INDsleTurno.EditValue, Integer)
    '    End Get
    '    Set(value As Integer)
    '        INDsleTurno.EditValue = value
    '    End Set
    'End Property
    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IFunctionalUnit.ActionsOnControls
        Set(value As Boolean)
            INDlyFunctionalUnit.BeginUpdate()

            INDBteCode.Enabled = Not value
            INDGleBranchOfficeFuncUnit.Enabled = value
            INDSearchLookCostCenter.Enabled = value
            INDSearchLookProductionCenter.Enabled = value
            INDSearchLookAccountingStructure.Enabled = value
            INDNameFuncUnit.Enabled = value
            INDSLookUpUser.Enabled = value
            INDbtnAdd.Enabled = value
            INDSleAuthorizedUser.Enabled = value
            INDBtnAuthorizedUser.Enabled = value
            INDgcUser.Enabled = value
            INDGcAuthorizedUser.Enabled = value
            INDsleUnitType.Enabled = value
            INDTmeDeliveryTimePharmaService.Enabled = value
            INDSleUserAuthorized.Enabled = value
            INDBtnAddUserAuthorized.Enabled = value
            INDGcUserAuthorizationRequest.Enabled = value
            INDlyFunctionalUnit.EndUpdate()

            If value Then
                INDGleBranchOfficeFuncUnit.Focus()
            Else
                INDBteCode.Focus()
            End If
        End Set
    End Property

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListUnitType = Nothing
        dtFieldsCustomizables = Nothing
        record = Nothing
        ExistDefinitionFront = Nothing
        FunctionalUnit = Nothing
        Model = Nothing
        Presenter = Nothing
        PathFunctionalDefinitions = Nothing
        _idCurrentSequence = Nothing
        _idOperativeUnit = Nothing
    End Sub
    ''' <summary>
    ''' Handles the Load event of the FrmFunctionalUnit control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmFunctionalUnit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc
        Me.indigo = SessionValues.Instance

        ''Cargamos de manera asincrona definiciones del funcional
        'PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollFunctionalUnit.", Me.Name, ".xml")
        'LoadhronousDefinitions = New BackgroundWorker
        'If LoadhronousDefinitions.IsBusy = False Then
        '    LoadhronousDefinitions.RunWorkerAsync()
        'End If
        Presenter = New PFunctionalUnit(Me)
        GLESize()
        Presenter.Initializes()
        Presenter.GetSequence()
        Using model As New MFunctionalUnit(MyBase.Tag)
            INDSearchLookCostCenter.Properties.DataSource = model.ListAllCostCenter()
            INDSearchLookAccountingStructure.Properties.DataSource = model.ListAllAccountingStructure()
            INDSearchLookProductionCenter.Properties.DataSource = model.ListProductionCenter()
            ''Se quita esta condición porque Christian Salazar dijo que estaba mal
            ''Únicamente si la Nómina está integrada con el ERP nuestro
            'If indigo.IndigoPayrollIntegration = "1" Then
            '    INDSearchLookProductionCenter.Properties.DataSource = model.ListProductionCenter()
            '    INDlyItemProductionCenter.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            'End If
        End Using
        Using model As New MEntityAccount(Me.Tag)
            Me.INDSLookUpUser.FuncQueryOnKeyEnterPressed = AddressOf model.GetUserByCode
            Me.INDSleAuthorizedUser.FuncQueryOnKeyEnterPressed = AddressOf model.GetUserAuthorizedByCode
            Me.INDSleUserAuthorized.FuncQueryOnKeyEnterPressed = AddressOf model.GetUserAuthorizedByCode
        End Using
        LoadStatus()
        'LoadTurn()
        Deshacer()
        InitializeTuples()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me.FunctionalUnit IsNot Nothing AndAlso Me.FunctionalUnit.Id > 0 Then
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

#End Region

#Region "Metodos Funciones Propiedades"
    'Sub LoadTurn()
    '    Presenter.InitializeTurn()
    'End Sub
    Public Async Function GetUserByCode(ByVal code As String) As Task(Of Domain.Security.Entities.User)
        Dim res = Await IndigoConecta.Instancia.CurrentCloud.IndigoSeguridad.GetUserByCodeUserAsync(code, indigo, Nothing)
        If res Is Nothing OrElse res.Id = 0 Then
            res = New Domain.Security.Entities.User()
            res.UserCode = code
        Else
            res.PersonFullName = res.Person.Fullname
        End If
        Return res
    End Function

    ''' <summary>
    ''' Metodo que inicializa las tuplas de los search
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuples()
        ListUnitType = New List(Of Tuple(Of Integer, String))
        ListUnitType.Add(New Tuple(Of Integer, String)(1, "Urgencias"))
        ListUnitType.Add(New Tuple(Of Integer, String)(2, "Hospitalizacion"))
        ListUnitType.Add(New Tuple(Of Integer, String)(3, "Apoyo Dx"))
        ListUnitType.Add(New Tuple(Of Integer, String)(4, "Apoyo Terapeutico"))
        ListUnitType.Add(New Tuple(Of Integer, String)(5, "Unidades de Cuidado Intensivo Adulto"))
        ListUnitType.Add(New Tuple(Of Integer, String)(6, "Unidades de Cuidado Intermedio Adulto"))
        ListUnitType.Add(New Tuple(Of Integer, String)(7, "Unidades de Cuidado Intensivo Pediatrica"))
        ListUnitType.Add(New Tuple(Of Integer, String)(8, "Unidades de Cuidado Intermedio Pediatrica"))
        ListUnitType.Add(New Tuple(Of Integer, String)(9, "Unidades de Cuidado Intensivo Neonatal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(10, "Unidades de Cuidado Intermedio Neonatal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(11, "Unidades de Cuidado Basico Neonatal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(12, "Unidad Renal"))
        ListUnitType.Add(New Tuple(Of Integer, String)(13, "Unidad Oncologica"))
        ListUnitType.Add(New Tuple(Of Integer, String)(14, "Unidad Medicina Nuclear"))
        ListUnitType.Add(New Tuple(Of Integer, String)(15, "Consulta Externa"))
        ListUnitType.Add(New Tuple(Of Integer, String)(16, "Unidad Mental"))
        ListUnitType.Add(New Tuple(Of Integer, String)(17, "Unidad de Quemados"))
        ListUnitType.Add(New Tuple(Of Integer, String)(18, "Unidad de Cuidado Paliativo"))
        ListUnitType.Add(New Tuple(Of Integer, String)(19, "Cirugia"))
        ListUnitType.Add(New Tuple(Of Integer, String)(20, "Laboratorio"))
        ListUnitType.Add(New Tuple(Of Integer, String)(21, "Cardiologia No Invasiva"))
        ListUnitType.Add(New Tuple(Of Integer, String)(22, "Cardiologia Invasiva"))
        ListUnitType.Add(New Tuple(Of Integer, String)(23, "Gineco-Obstetricia"))
        ListUnitType.Add(New Tuple(Of Integer, String)(24, "Consulta Externa - Gineco-Obstetricia"))
        ListUnitType.Add(New Tuple(Of Integer, String)(25, "Otras"))
        INDsleUnitType.Properties.DataSource = ListUnitType.ToList
    End Sub

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With {
                .Content = String.Format(obtenerRecurso(Eresources.FrmFunctionalUnitMetaData, Eform.InfoMetaData), Me.FunctionalUnit.Code, Me.FunctionalUnit.Name, INDGleBranchOfficeFuncUnit.Text, INDSearchLookCostCenter.Text),
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File,
                .IdEntity = "$#" & Me.Tag & "_" & Me.FunctionalUnit.Code & "#$", .IdForm = Me.Tag,
                .Title = String.Format(obtenerRecurso(Eresources.FrmFunctionalUnitMetaDataTitle, Eform.InfoMetaData), Me.FunctionalUnit.Code),
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmFunctionalUnitMetaData, Eform.InfoMetaData), Me.FunctionalUnit.Code, Me.FunctionalUnit.Name, INDGleBranchOfficeFuncUnit.Text, INDSearchLookCostCenter.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmFunctionalUnitMetaDataTitle, Eform.InfoMetaData), Me.FunctionalUnit.Code)
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
    ''' Método para el tamaño de el popup de los Gridlookupedit
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GLESize()
        INDGleBranchOfficeFuncUnit.Properties.PopupFormSize = New Drawing.Size(500, 200)
    End Sub
    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With FunctionalUnit
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = NameFuncUnit
            .BranchOfficeId = BranchOfficeId
            .CostCenterId = INDSearchLookCostCenter.EditValue
            .ProductionCenterId = INDSearchLookProductionCenter.EditValue
            .AccountingStructureId = INDSearchLookAccountingStructure.EditValue
            .UnitType = UnitType
            .DeliveryTimePharmaService = Me.DeliveryTimePharmaService
        End With
    End Sub

    Private Async Function NewFunctionalUnit() As Task
        FunctionalUnit = New FunctionalUnit() With {.State = True}
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
                        Using model As New Accounting.MVP.MStatementFolio(CStr(Me.Tag))
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
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        'Dim listStates As New List(Of StatusRecord)()
        'listStates.Add(New StatusRecord With {.StatusValue = True, .StatusName = obtenerRecurso(ComunesActivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(138, Byte), Integer), CType(CType(23, Byte), Integer))})
        'listStates.Add(New StatusRecord With {.StatusValue = False, .StatusName = obtenerRecurso(ComunesInactivo), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(210, Byte), Integer), CType(CType(71, Byte), Integer), CType(CType(38, Byte), Integer))})
        'Me.BarraBotones.States = listStates
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If

            Using Model As New MFunctionalUnit(CStr(Me.Tag))
                AsyncLoader(True)
                FunctionalUnit = Await Model.GetFuncUnitAsync(INDBteCode.Text.Trim())
                INDSLookUpUser.Datasource = Model.ListAllUser(indigo.SecurityContainer)
                INDSleAuthorizedUser.Datasource = Model.ListAllUser(indigo.SecurityContainer)
                INDSleUserAuthorized.Datasource = Model.ListAllUser(indigo.SecurityContainer)
                INDlyFunctionalUnit.BeginUpdate()
                If FunctionalUnit IsNot Nothing AndAlso FunctionalUnit.Id > 0 Then
                    Me.BarraBotones.StatusRecordVisible = True
                    record = Await Model.GetBlockRecord(CStr(Me.Tag), CStr(FunctionalUnit.Id))
                    With FunctionalUnit
                        LayoutControls.SetCustomFieldsValue(.CustomProperties)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)
                        INDgcUser.DataSource = .FunctionalUnitResponsible
                        INDGcAuthorizedUser.DataSource = .FunctionalUnitUser
                        INDGcUserAuthorizationRequest.DataSource = .FunctionalUnitUserAuthorizationRequest
                        Code = .Code
                        NameFuncUnit = .Name
                        UnitType = .UnitType
                        BranchOfficeId = .BranchOfficeId
                        CostCenterId = .CostCenterId
                        AccountingStructureId = .AccountingStructureId
                        INDSearchLookProductionCenter.EditValue = .ProductionCenterId
                        StateFuncUnit = .State
                        Me.DeliveryTimePharmaService = .DeliveryTimePharmaService
                        'If .Turn IsNot Nothing Then
                        '    TurnId = .Turn
                        'End If
                        LoadUnitUserAuthorizationRequest()
                    End With

                    Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me.FunctionalUnit.Code)
                    If record.Id = 0 Then
                        record = (Await Model.SaveBlockRecord(
                            New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = FunctionalUnit.Id})
                            ).ObjectEmbbeded
                    Else
                        Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), record.CodUser, record.NameUser, record.BlockDate)
                        Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, record.CodUser)
                    End If
                    Me.BarraBotones.SetDocuments(FunctionalUnit.Id, Me.Tag.ToString(), Nothing, GetType(FunctionalUnit).Name)
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    AsyncLoader(False)
                    ActionsOnControls = True
                Else
                    AsyncLoader(False)
                    If Me._sequence.IsManual Then
                        Await Me.NewFunctionalUnit()
                    Else
                        Me.Mensaje(EeventViewerImages.Advertencia) = "La Unidad Funcional no existe"
                        Code = String.Empty
                        INDBteCode.Focus()
                    End If
                End If
                INDlyFunctionalUnit.EndUpdate()
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        INDlyFunctionalUnit.BeginUpdate()
        ActionsOnControls = False

        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing

        Code = String.Empty
        NameFuncUnit = String.Empty
        INDGleBranchOfficeFuncUnit.Text = String.Empty
        INDGleBranchOfficeFuncUnit.EditValue = Nothing
        INDSearchLookCostCenter.EditValue = Nothing
        INDSearchLookProductionCenter.EditValue = Nothing
        INDSearchLookAccountingStructure.EditValue = Nothing
        INDSLookUpUser.EditValue = Nothing
        INDSleAuthorizedUser.EditValue = Nothing
        INDSleUserAuthorized.EditValue = Nothing
        Me.DeliveryTimePharmaService = Nothing
        INDLciDeliveryTimePharmaService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyRequestAuthorization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        INDlyRequestAuthorization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDgcUser.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDgcUser)
        INDGcAuthorizedUser.DataSource = Nothing
        INDGcUserAuthorizationRequest.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcAuthorizedUser)
        StateFuncUnit = True
        UnitType = Nothing
        FunctionalUnit = Nothing
        'TurnId = Nothing
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If

        INDlyFunctionalUnit.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControlsFuntionalUnit() As Boolean
        ValidateControlsFuntionalUnit = True
        'If Code = String.Empty Then
        '    INDBteCode.Focus()
        '    ValidateControlsFuntionalUnit = False
        '    Exit Function
        'End If
        If NameFuncUnit = String.Empty Then
            INDNameFuncUnit.Focus()
            ValidateControlsFuntionalUnit = False
            Exit Function
        End If
        If INDGleBranchOfficeFuncUnit.EditValue Is Nothing OrElse String.IsNullOrEmpty(INDGleBranchOfficeFuncUnit.EditValue) Then
            INDGleBranchOfficeFuncUnit.Focus()
            ValidateControlsFuntionalUnit = False
            Exit Function
        End If
        If INDSearchLookCostCenter.EditValue Is Nothing Then
            INDGleBranchOfficeFuncUnit.Focus()
            ValidateControlsFuntionalUnit = False
            Exit Function
        End If
        If INDSearchLookAccountingStructure.EditValue Is Nothing Then
            INDGleBranchOfficeFuncUnit.Focus()
            ValidateControlsFuntionalUnit = False
            Exit Function
        End If
    End Function

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDBteCodeFuncUnit_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDBteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' Evento para consultar la unidad funcional en el evento keydown del codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDBteCodeFuncUnit_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBteCode.KeyDown
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
                    Await Me.NewFunctionalUnit()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBteCode.Text Is String.Empty Then
            INDBteCode.Focus()
        End If
    End Sub

    Private Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit1.Click
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim responsible = CType(INDGvAuthorizedUser.GetFocusedRow(), FunctionalUnitUser)
            responsible.MarkAsDeleted()
            FunctionalUnit.MarkAsModified()
        End If
    End Sub
    Private Sub RepositoryItemButtonEdit2_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit2.Click
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim responsible = CType(INDGvUserAuthorizationRequest.GetFocusedRow(), FunctionalUnitUserAuthorizationRequest)
            responsible.MarkAsDeleted()
            FunctionalUnit.MarkAsModified()
        End If
    End Sub

    Private Sub INDGvAuthorizedUser_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvAuthorizedUser.CustomDrawCell
        If e.Column.Name = ColAuthorizedUserAction.Name Then
            e.DisplayText = obtenerRecurso(EliminarRegistro)
        End If
    End Sub
    Private Sub INDGvUserAuthorizationRequest_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDGvUserAuthorizationRequest.CustomDrawCell
        If e.RowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle Then
            ' Verificar si la columna es "ColAuthorizedUserAction"
            If e.Column.FieldName = "ColAuthorizedUserAction" Then
                ' Ocultar el texto en el cuadro de búsqueda
                e.DisplayText = String.Empty
            End If
        Else
            If e.Column.Name = INDGcUserAction.Name Then
                e.DisplayText = "Eliminar"
            End If
        End If
    End Sub
    ''' <summary>
    ''' Carga el segmento Autorizacion de solicitudes
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadUnitUserAuthorizationRequest()
        Dim requiresAuthorization As String = Code

        'If requiresAuthorization.HasValue Then
        Dim RequestParam = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.GetXPOObject(Of ViewRequestParamFunctionalUnitXpo)($"FunctionalUnitCode='{requiresAuthorization}'")

        'LayoutControlGroup1.BeginUpdate()
        If RequestParam IsNot Nothing Then
            If Not RequestParam.RequiredAuthorization Then
                INDlyRequestAuthorization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Else
                INDlyRequestAuthorization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End If
        End If
        'LayoutControlGroup1.EndUpdate()
    End Function
#End Region

#Region "CRUD Operations"
    ''' <summary>
    ''' METODO: Item Nuevo del control de Unidades funcionales.
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me.Sequence Is Nothing OrElse Me.Sequence.Id = 0 OrElse Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewFunctionalUnit()
        End If
    End Sub
    ''' <summary>
    ''' METODO: Item buscar del control de Unidades funcionales.
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
        AbrirBusqueda()
    End Sub
    ''' <summary>
    ''' METODO: Item Eliminar del control de Unidades funcionales.
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If Me.FunctionalUnit IsNot Nothing AndAlso Me.FunctionalUnit.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MFunctionalUnit(MyBase.Tag)
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteFuncUnitAsync(Me.FunctionalUnit)
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
    ''' METODO: Item Guardar del control de Unidades funcionales.
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If ValidateControlsFuntionalUnit() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        AssigningValues()
        Try
            AsyncLoader(True)
            Dim result As ActionResult(Of FunctionalUnit) = Await Model.SaveFuncUnitAsync(Me.FunctionalUnit, Me._idCurrentSequence)
            If result.StatusCode = eStatusResult.SUCCESS Then
                If FunctionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                        Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                    End If
                End If
                Me.FunctionalUnit = result.ObjectEmbbeded
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)
                Deshacer()
            Else
                AsyncLoader(False)
                INDBteCode.Enabled = False
            End If
            ShowMessage(result.StatusCode) = result.Message
        Catch ex As Exception
            AsyncLoader(False)
            INDBteCode.Enabled = False
            Throw ex
        End Try
    End Sub

    Public Async Sub UpdateState()
        If Not String.IsNullOrEmpty(Me.FunctionalUnit.Code) Then
            Try
                Using model As New MFunctionalUnit(MyBase.Tag)
                    Dim state As Boolean
                    Select Case StateFuncUnit
                        Case CBool(eActionsStatusRecords.Active)
                            state = True
                        Case CBool(eActionsStatusRecords.Inactive)
                            state = False
                    End Select
                    AsyncLoader(True)
                    If (Await model.UpdateStateFunctionalUnitAsync(Me.FunctionalUnit.Code, state)).StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                        AsyncLoader(False)
                    Else
                        AsyncLoader(False)
                        INDBteCode.Enabled = False
                        Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                    End If
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
    Public Sub AbrirBusqueda() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            If INDGleBranchOfficeFuncUnit.EditValue <> Nothing Then
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnitByBranchOffice
                .FiltroBusqueda = INDGleBranchOfficeFuncUnit.EditValue
            Else
                .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit
            End If
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}, New ColumnInfo() With {.Caption = "Código Sucursal", .FieldName = "BranchOfficeId.Codigo"}, New ColumnInfo() With {.Caption = "Nombre Sucursal", .FieldName = "BranchOfficeId.Descripcion"}, New ColumnInfo() With {.Caption = "Código Empresa", .FieldName = "BranchOfficeId.CompanyId.Codigo"}, New ColumnInfo() With {.Caption = "Nombre Empresa", .FieldName = "BranchOfficeId.CompanyId.Descripcion"}}.ToList
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
        If Code <> String.Empty Then
            Await LoadControls()
            If INDBteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de Unidades funcionales.
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub
    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
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
    ''' este permite establecer la logica para los permisos de Guardar y Actualizar   
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
    End Sub
    ''' <summary>
    ''' Metodo para abrir form sucursales en popup
    ''' </summary>
    Private Sub INDGleBranchOfficeFuncUnit_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDGleBranchOfficeFuncUnit.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBranchOffice
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Presenter.Initializes()
            End Using
        End If
    End Sub
#End Region

#Region "bar buttons and events"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        ' Extraer el ID del formulario correctamente, manejando el caso cuando viene con "__" + idEntity
        Dim formTag As String = CStr(MyBase.Tag)
        If formTag.Contains("__") Then
            formTag = formTag.Split(New String() {"__"}, StringSplitOptions.None)(0)
        End If
        BarraBotones.ActualizarPermisosBarra(formTag)
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

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    Private Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        UpdateState()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        ResetLayout()
    End Sub

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmFunctionalUnit_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Model.Dispose()
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDSearchLookCostCenter control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSearchLookCostCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSearchLookCostCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmCostCenter
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using model As New MFunctionalUnit(MyBase.Tag)
                    INDSearchLookCostCenter.Properties.DataSource = model.ListAllCostCenter()
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDSearchLookAccountingStructure control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDSearchLookAccountingStructure_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSearchLookAccountingStructure.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmAccountingStructure
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                Using model As New MFunctionalUnit(MyBase.Tag)
                    INDSearchLookAccountingStructure.Properties.DataSource = model.ListAllAccountingStructure()
                End Using
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta al dar click sobre agregar usuario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click, INDBtnAuthorizedUser.Click, INDBtnAddUserAuthorized.Click
        If sender.Name = INDBtnAuthorizedUser.Name Then
            If INDSleAuthorizedUser.EditValue IsNot Nothing Then

                If FunctionalUnit.FunctionalUnitUser.Where(Function(x) x.UserId = INDSleAuthorizedUser.EditValue).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra agregado"
                    Return
                End If

                Dim AuthorizedUser As New FunctionalUnitUser()
                AuthorizedUser.UserId = INDSleAuthorizedUser.EditValue
                AuthorizedUser.User = INDSleAuthorizedUser.Text

                Using ModeloUsuario As New MUsuario()
                    Dim user As User = Await ModeloUsuario.GetUserById(INDSleAuthorizedUser.EditValue)
                    If user Is Nothing OrElse user.Id = 0 Then
                        AuthorizedUser.Fullname = ""
                        AuthorizedUser.User = ""
                        AuthorizedUser.UserCode = String.Empty
                        AuthorizedUser.UserId = 0
                    Else
                        AuthorizedUser.User = user.UserCode
                        AuthorizedUser.Fullname = user.Person.Fullname
                        AuthorizedUser.UserCode = user.UserCode
                        AuthorizedUser.UserId = user.Id
                    End If
                End Using

                FunctionalUnit.FunctionalUnitUser.Add(AuthorizedUser)
                If FunctionalUnit.ChangeTracker.State = ObjectState.Unchanged Then
                    FunctionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
                INDSleAuthorizedUser.EditValue = Nothing
                INDGcAuthorizedUser.DataSource = FunctionalUnit.FunctionalUnitUser
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUsuario, UnidadesFuncionales)
            End If
        ElseIf sender.Name = "INDbtnAdd" Then
            If INDSLookUpUser.EditValue IsNot Nothing Then

                If FunctionalUnit.FunctionalUnitResponsible.Where(Function(x) x.UserId = INDSLookUpUser.EditValue).Count > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(UsuarioYaAgregado, UnidadesFuncionales), INDSLookUpUser.Text)
                    Return
                End If

                Dim responsible As New FunctionalUnitResponsible()
                responsible.UserId = INDSLookUpUser.EditValue
                responsible.User = INDSLookUpUser.Text

                Using ModeloUsuario As New MUsuario()
                    Dim user As User = Await ModeloUsuario.GetUserById(INDSLookUpUser.EditValue)
                    If user Is Nothing OrElse user.Id = 0 Then
                        responsible.Fullname = ""
                        responsible.User = ""
                    Else
                        responsible.User = user.UserCode
                        responsible.Fullname = user.Person.Fullname
                    End If
                End Using

                FunctionalUnit.FunctionalUnitResponsible.Add(responsible)
                If FunctionalUnit.ChangeTracker.State = ObjectState.Unchanged Then
                    FunctionalUnit.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified
                End If
                INDSLookUpUser.EditValue = Nothing
                INDgcUser.DataSource = FunctionalUnit.FunctionalUnitResponsible '.FunctionalUnitResponsible
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUsuario, UnidadesFuncionales)
            End If
        ElseIf sender.Name = INDBtnAddUserAuthorized.Name Then
            If INDSleUserAuthorized.EditValue IsNot Nothing Then

                If FunctionalUnit.FunctionalUnitUserAuthorizationRequest.Any(Function(x) x.UserId = INDSleUserAuthorized.EditValue) Then
                    Mensaje(EeventViewerImages.Advertencia) = "El usuario ya se encuentra agregado"
                    Return
                End If

                Dim AuthorizedUser As New FunctionalUnitUserAuthorizationRequest()
                AuthorizedUser.UserId = INDSleUserAuthorized.EditValue

                Using ModeloUsuario As New MUsuario()
                    Dim user As User = Await ModeloUsuario.GetUserById(INDSleUserAuthorized.EditValue)
                    If user Is Nothing OrElse user.Id = 0 Then
                        Mensaje(EeventViewerImages.Advertencia) = "Usuario no encontrado"
                        Return
                    Else
                        AuthorizedUser.Fullname = user.Person.Fullname
                        AuthorizedUser.UserCode = user.UserCode
                    End If
                End Using

                FunctionalUnit.FunctionalUnitUserAuthorizationRequest.Add(AuthorizedUser)

                If FunctionalUnit.Id > 0 Then
                    FunctionalUnit.MarkAsModified()
                End If

                INDSleUserAuthorized.EditValue = Nothing
                INDGcUserAuthorizationRequest.DataSource = FunctionalUnit.FunctionalUnitUserAuthorizationRequest
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(SeleccioneUsuario, UnidadesFuncionales)
            End If
        End If

    End Sub

    Private Sub INDgvUser_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvUser.CustomDrawCell
        If e.Column.Name = INDcolUserAction.Name Then
            e.DisplayText = obtenerRecurso(EliminarRegistro)
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta cuando quiero eliminar un usuario reponsable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDRepositoryButtonEditAction_Click(sender As Object, e As EventArgs) Handles INDRepositoryButtonEditAction.Click
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim responsible = CType(INDgvUser.GetFocusedRow(), FunctionalUnitResponsible)
            responsible.MarkAsDeleted()
            FunctionalUnit.MarkAsModified()
        End If
    End Sub

#End Region

#Region "Customize"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyFunctionalUnit.ShowCustomizationForm()
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
            INDlyFunctionalUnit.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyFunctionalUnit_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyFunctionalUnit.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MFunctionalUnit(MyBase.Tag)
                Dim dsFields As DataSet = model.GetFieldsNULL()
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyFunctionalUnit.Items.Count - 1
                        INDlyFunctionalUnit.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyFunctionalUnit.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyFunctionalUnit.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyFunctionalUnit.Items.Item(j).AllowHide = True
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
    Private Sub INDlyFunctionalUnit_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyFunctionalUnit.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyFunctionalUnit.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyFunctionalUnit.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyFunctionalUnit.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub


#End Region

    Private Sub INDSearchLookProductionCenter_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDSearchLookProductionCenter.QueryPopUp
        If INDSearchLookProductionCenter.Properties.DataSource Is Nothing Then
            INDSearchLookProductionCenter.Properties.DataSource = Model.ListProductionCenter()
        End If
    End Sub

    Private Sub INDSearchLookProductionCenter_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSearchLookProductionCenter.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(1201, Nothing, True)
            INDSearchLookProductionCenter.Properties.DataSource = Model.ListProductionCenter()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control tipo de unidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDslePOSProduct_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleUnitType.EditValueChanged
        'Hospitalización Codigo 2
        'Unidad de Cuidados Intensivos Adulto Código 5
        'Unidad de Cuidados Intermedios Adulto Código 6 
        'Unidad de Cuidados Intensivos Pediátrico Código  7 
        'Unidad de Cuidados Intermedios Pediátrico Código 8 
        'Unidad de Cuidados Intensivos Neonatal Código 9 
        'Unidad de Cuidados Intermedios Neonatal Código 10 
        'Unidad de Cuidados Básicos Neonatal Código 11 
        'Gineco-Obstetricia 23.
        If UnitType = 2 OrElse UnitType = 5 OrElse UnitType = 6 OrElse UnitType = 7 OrElse UnitType = 8 OrElse UnitType = 9 OrElse UnitType = 10 OrElse UnitType = 11 OrElse UnitType = 23 Then
            INDLciDeliveryTimePharmaService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            INDLciDeliveryTimePharmaService.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.DeliveryTimePharmaService = Nothing
        End If
    End Sub

End Class