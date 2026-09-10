'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jose Luis Rojas
' Created          : 07-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Payroll.MVP
Imports Domain.Payroll.Entities
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Common
Imports Presentation.Controls
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Resources

#End Region

''' <summary>
''' Manejo del frontal
''' </summary>
Public Class FrmPosition
    Implements IPosition

#Region "Globals & Properties"


    Private ModoBusqueda As Boolean
    Private record As Domain.Entities.BlockRecord

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
    ''' Variable que contiene el cargo
    ''' </summary>
    Dim Position As Position

    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker

    ''' <summary>
    ''' Variable para poder acceder al Modelo
    ''' </summary>
    Dim Model As New MPosition(MyBase.Tag)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PPosition

    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Propiedad del codigo del cargo
    ''' </summary>
    Public Property PositionCode As String Implements IPosition.PositionCode
        Get
            Return INDbteCode.Text
        End Get
        Set(value As String)
            INDbteCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del nombre del cargo
    ''' </summary>
    Public Property PositionName As String Implements IPosition.PositionName
        Get
            Return INDtxtName.Text
        End Get
        Set(value As String)
            INDtxtName.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del nivel del cargo
    ''' </summary>
    Public Property PositionLevelId As String Implements IPosition.PositionLevelId
        Get
            Return INDglePositionLevel.EditValue
        End Get
        Set(value As String)
            INDglePositionLevel.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del nivel del cargo datasource
    ''' </summary>
    Public Property PositionLevels As List(Of Domain.Payroll.Entities.PositionLevel) Implements IPosition.PositionLevels
        Get
            Return INDglePositionLevel.Properties.DataSource
        End Get
        Set(value As List(Of Domain.Payroll.Entities.PositionLevel))
            INDglePositionLevel.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del riesgo profesional del cargo
    ''' </summary>
    Public Property ProfessionalRiskLevelId As String Implements IPosition.ProfessionalRiskLevelId
        Get
            Return INDgleProfessionalRisk.EditValue
        End Get
        Set(value As String)
            INDgleProfessionalRisk.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del riesgo profesional del cargo datasource
    ''' </summary>
    Public Property ProfessionalRiskLevels As List(Of ProfessionalRisk) Implements IPosition.ProfessionalRiskLevels
        Get
            Return INDgleProfessionalRisk.Properties.DataSource
        End Get
        Set(value As List(Of ProfessionalRisk))
            INDgleProfessionalRisk.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del salario minimo del cargo
    ''' </summary>
    Public Property MinBasicSalary As String Implements IPosition.MinBasicSalary
        Get
            Return INDtxtMinBasicSalary.EditValue
        End Get
        Set(value As String)
            INDtxtMinBasicSalary.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del salario maximo del cargo
    ''' </summary>
    Public Property MaxBasicSalary As String Implements IPosition.MaxBasicSalary
        Get
            Return INDtxtMaxBasicSalary.EditValue
        End Get
        Set(value As String)
            INDtxtMaxBasicSalary.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del horas minimas del cargo
    ''' </summary>
    Public Property MinHourAmount As String Implements IPosition.MinHourAmount
        Get
            Return INDtxtMinHourAmount.Text
        End Get
        Set(value As String)
            INDtxtMinHourAmount.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del horas maximas del cargo
    ''' </summary>
    Public Property MaxHourAmount As String Implements IPosition.MaxHourAmount
        Get
            Return INDtxtMaxHourAmount.Text
        End Get
        Set(value As String)
            INDtxtMaxHourAmount.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del simulacion del cargo
    ''' </summary>
    Public Property Simulation As Boolean Implements IPosition.Simulation
        Get
            Return INDchkSimulation.EditValue
        End Get
        Set(value As Boolean)
            INDchkSimulation.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del maneja cuadro de turnos del cargo
    ''' </summary>
    Public Property HandlesTurnsChart As Boolean Implements IPosition.HandlesTurnsChart
        Get
            Return INDchkHandlesTurnsChart.EditValue
        End Get
        Set(value As Boolean)
            INDchkHandlesTurnsChart.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del autorizacion para recargo nocturno del cargo
    ''' </summary>
    Public Property NightlyChargeAuthorization As Boolean Implements IPosition.NightlyChargeAuthorization
        Get
            Return INDchkNightlyChargeAuthorization.EditValue
        End Get
        Set(value As Boolean)
            INDchkNightlyChargeAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del estado del cargo
    ''' </summary>
    Public Property Status As Boolean Implements IPosition.Status
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Propiedad de Gastos de Representación
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property RepresentationCost As Boolean Implements IPosition.RepresentationCost
        Get
            Return Me.INDchkRepresentationCost.EditValue
        End Get
        Set(value As Boolean)
            INDchkRepresentationCost.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del Código INS
    ''' </summary>
    Public Property INSCode As String Implements IPosition.INSCode
        Get
            Return INDtxtINSCode.Text
        End Get
        Set(value As String)
            INDtxtINSCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad del Código CCSS
    ''' </summary>
    Public Property CCSSCode As String Implements IPosition.CCSSCode
        Get
            Return INDtxtCCSSCode.Text
        End Get
        Set(value As String)
            INDtxtCCSSCode.Text = value
        End Set
    End Property


#End Region

#Region "ICRUD"

    ''' <summary>
    ''' Abre el control de busqueda
    ''' </summary>
    Public Sub AbrirBusqueda() Implements Base.IcrudBase.OpenSearch
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.Position
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Codigo"}, New ColumnInfo() With {.Caption = "Descripción", .FieldName = "Descripcion"}}.ToList
            BarraBotones.PrepareToolbar(eAction.New)
            .FormParent = Me
            .ShowSearch()
            ModoBusqueda = True
        End With
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
            LoadControls()
            If INDbteCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbteCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' METODO: item buscar del control de usuario
    ''' </summary>
    Public Sub Buscar() Implements Base.IcrudBase.Buscar
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' METODO: item deshacer del control de usuario
    ''' </summary>
    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        If ModoBusqueda = False Then
            CleanControls()
        End If

    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Async Sub Eliminar() Implements Base.IcrudBase.Eliminar
        Dim actionResult As ActionMessageResult(Of Position)
        If Position IsNot Nothing Then
            If Position.Id > 0 Then
                If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                    Using Model As New MPosition(MyBase.Tag)
                        AsyncLoader(True)
                        actionResult = Await Model.DeletePositionAsync(Position)
                        If actionResult.StateResult = True Then
                            Await Me.DeleteDocumentIndexed()
                            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesEliminado)
                            ModoBusqueda = False
                            CleanControls()
                        Else
                            For Each action As MessageResult In actionResult.MessageResult
                                If action.CodeMessage = "c-0000" Then
                                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesErrorDependencia)
                                    AsyncLoader(False)
                                Else
                                    Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
                                End If
                            Next
                        End If
                        AsyncLoader(False)
                    End Using
                    ActionsOnControls = False
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
                End If
            Else
                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneUnCargo, Eform.Cargo)
            End If
        Else
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(Eresources.SeleccioneUnCargo, Eform.Cargo)
        End If
    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control del usuario
    ''' </summary>
    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar
        If ValidateControls() = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Exit Sub
        End If
        If CDec(INDtxtMinBasicSalary.EditValue) > CDec(INDtxtMaxBasicSalary.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ValorMinimoMaximo)
            Exit Sub
        End If

        If CDec(INDtxtMinHourAmount.EditValue) > CDec(INDtxtMaxHourAmount.EditValue) Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ValorMaximoMinimoHoras)
            Exit Sub
        End If
        AssigningValues()
        Using Model As New MPosition(MyBase.Tag)
            AsyncLoader(True)
            If Await Model.SavePositionAsync(Position) = True Then
                If Position.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)
                ElseIf Position.ChangeTracker.State = Domain.Base.Entities.ObjectState.Modified Or Position.ChangeTracker.State = Domain.Base.Entities.ObjectState.Unchanged Then
                    Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesActualizado)
                End If
            Else
                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(ComunesContacteAdministrador)
            End If
            Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
            AsyncLoader(False)
            ModoBusqueda = False
            Deshacer()
        End Using
    End Sub

    ''' <summary>
    '''  este permite establecer la logica para los permisos de Guardar y Actualizar 
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements Base.IcrudBase.LogicaBotonActualizar
        BarraBotones.LogicaBotonActualizar = existeDatos
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

    ''' <summary>
    ''' METODO: Item nuevo del control de usuario
    ''' </summary>
    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo
        CleanControls()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Función para generar la data de indexación
    ''' </summary>
    Private Function GenerateDoc() As IndexedDocument2
        Dim dateServer = Me.GetDateServer
        If Me._doc Is Nothing Then
            Me._doc = New IndexedDocument2 With { _
                .Content = String.Format(obtenerRecurso(Eresources.FrmPositionMetaData, Eform.InfoMetaData), Me.Position.Code, Me.Position.Name, INDglePositionLevel.Text, INDgleProfessionalRisk.Text), _
                .CreationDate = dateServer, .CreationUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName, .DocumentType = IndexedDocumentType.File, _
                .IdEntity =  "$#" & Me.Tag & "_" & Me.Position.Code & "#$", .IdForm = Me.Tag, _
                .Title = String.Format(obtenerRecurso(Eresources.FrmPositionMetaDataTitle, Eform.InfoMetaData), Me.Position.Code), _
                .Update = dateServer, .UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName}
            Return Me._doc
        Else
            Me._doc.Update = dateServer
            Me._doc.UpdateUser = Me.indigo.UserIndigo & "-" & Me.indigo.UserIndigoName
            Me._doc.Content = String.Format(obtenerRecurso(Eresources.FrmPositionMetaData, Eform.InfoMetaData), Me.Position.Code, Me.Position.Name, INDglePositionLevel.Text, INDgleProfessionalRisk.Text)
            Me._doc.Title = String.Format(obtenerRecurso(Eresources.FrmPositionMetaDataTitle, Eform.InfoMetaData), Me.Position.Code)
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
      Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' Metodo que sirve para limpiar los controles del frontal
    ''' </summary>
    Private Sub CleanControls()
        ActionsOnControls = False
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
        PositionCode = String.Empty
        PositionName = String.Empty
        ProfessionalRiskLevelId = Nothing
        PositionLevelId = Nothing
        MinBasicSalary = String.Empty
        MaxBasicSalary = String.Empty
        MinHourAmount = String.Empty
        MaxHourAmount = String.Empty
        INSCode = String.Empty
        CCSSCode = String.Empty
        Simulation = Nothing
        HandlesTurnsChart = Nothing
        NightlyChargeAuthorization = Nothing
        RepresentationCost = Nothing
        INDtxtResolutionName.EditValue = Nothing
        INDtxtAfterResolutionName.EditValue = Nothing
        Me.BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        INDbteCode.Focus()
        Position = New Position
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.CleanAuditBasic()
    End Sub

    ''' <summary>
    ''' establece el valor ControlAcciones
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IPosition.ActionsOnControls
        Set(value As Boolean)
            INDbteCode.Enabled = Not value
            INDtxtName.Enabled = value
            INDglePositionLevel.Enabled = value
            INDgleProfessionalRisk.Enabled = value
            INDtxtMinBasicSalary.Enabled = value
            INDtxtMaxBasicSalary.Enabled = value
            INDtxtMinHourAmount.Enabled = value
            INDtxtMaxHourAmount.Enabled = value
            INDchkSimulation.Enabled = value
            INDchkHandlesTurnsChart.Enabled = value
            INDchkNightlyChargeAuthorization.Enabled = value
            INDchkRepresentationCost.Enabled = value
            INDtxtResolutionName.Enabled = value
            INDtxtAfterResolutionName.Enabled = value
            INDtxtINSCode.Enabled = value
            INDtxtCCSSCode.Enabled = value
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
        ShowSocialSecurityInformation()
        Me.BarraBotones.StatusRecordVisible = True
        AsyncLoader(True)
        Using Model As New MPosition(MyBase.Tag)
            Position = Await Model.GetPositionAsync(PositionCode)
            If Not Position Is Nothing Then
                If Position.Id > 0 Then
                    Dim result = Await Model.GetBlockRecord(Me.Tag, Position.Id)
                    With Position
                        'LogicaBotonActualizar(True)
                        'Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), Position.CreationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), Position.CreationDate)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), Position.ModificationUser)
                        Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), Position.ModificationDate)
                        PositionCode = .Code
                        PositionName = .Name
                        ProfessionalRiskLevelId = .ProfessionalRiskLevelId
                        PositionLevelId = .PositionLevelId
                        MinBasicSalary = .MinBasicSalary
                        MaxBasicSalary = .MaxBasicSalary
                        MinHourAmount = .MinHourAmount
                        MaxHourAmount = .MaxHourAmount
                        Simulation = .Simulation
                        HandlesTurnsChart = .HandlesTurnsChart
                        NightlyChargeAuthorization = .NightlyChargeAuthorization
                        RepresentationCost = .RepresentationCost
                        INSCode = .INSCode
                        CCSSCode = .CCSSCode
                        Status = .State
                        INDtxtResolutionName.EditValue = .ResolutionNumber
                        INDtxtAfterResolutionName.EditValue = .BeforeResolutionNumber
                    End With
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                    Me.GetDocumentIndexed(Me.Tag & "_" & Me.Position.Code)
                    If result.Id = 0 Then
                        Me.BarraBotones.SetDocuments(Position.Id)
                        Dim state = New Domain.Base.Entities.ObjectChangeTracker
                        state.State = Domain.Base.Entities.ObjectState.Added
                        record = New Domain.Entities.BlockRecord With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = Position.Id}
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
                Position = New Position With {.State = True}
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            End If
            AsyncLoader(False)
        End Using
        ActionsOnControls = True
    End Function

    ''' <summary>
    ''' Valida que los campos esten diligenciados
    ''' </summary>
    Private Function ValidateControls() As Boolean
        ValidateControls = True
        If PositionCode.Equals(String.Empty) Then
            INDbteCode.Focus()
            ValidateControls = False
            Exit Function
        End If
        If PositionName.Equals(String.Empty) Then
            INDtxtName.Focus()
            ValidateControls = False
            Exit Function
        End If
        If PositionLevelId = Nothing Then
            INDglePositionLevel.Focus()
            ValidateControls = False
            Exit Function
        End If
        If ProfessionalRiskLevelId = Nothing Then
            INDgleProfessionalRisk.Focus()
            ValidateControls = False
            Exit Function
        End If
        If MinBasicSalary.Equals(String.Empty) Then
            INDtxtMinBasicSalary.Focus()
            ValidateControls = False
            Exit Function
        End If
        If MaxBasicSalary.Equals(String.Empty) Then
            INDtxtMaxBasicSalary.Focus()
            ValidateControls = False
            Exit Function
        End If
        If MinHourAmount.Equals(String.Empty) Then
            INDtxtMinHourAmount.Focus()
            ValidateControls = False
            Exit Function
        End If
        If MaxHourAmount.Equals(String.Empty) Then
            INDtxtMaxHourAmount.Focus()
            ValidateControls = False
            Exit Function
        End If

        If LayoutControlGroup6.Visible = True Then
            If INSCode.Equals(String.Empty) Then
                INDtxtINSCode.Focus()
                ValidateControls = False
                Exit Function
            End If
            If CCSSCode.Equals(String.Empty) Then
                INDtxtCCSSCode.Focus()
                ValidateControls = False
                Exit Function
            End If
        End If

        'If Simulation.Equals(Nothing) Then
        '    ValidateControls = False
        'End If
        'If HandlesTurnsChart.Equals(Nothing) Then
        '    ValidateControls = False
        'End If
        'If NightlyChargeAuthorization.Equals(Nothing) Then
        '    ValidateControls = False
        'End If
    End Function

    '''' <summary>
    '''' Metodo que muestra/oculta el Layout de Información de seguridad social
    '''' </summary>
    Private Sub ShowSocialSecurityInformation()
        If Me.indigo.Culture.Name = "es-CR" Then
            LayoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            LayoutControlGroup6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INSCode = Nothing
            CCSSCode = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para asignar los valores de los controles al objeto
    ''' </summary>
    Private Sub AssigningValues()
        With Position
            .Code = PositionCode
            .Name = PositionName
            .ProfessionalRiskLevelId = ProfessionalRiskLevelId
            .PositionLevelId = PositionLevelId
            .MinBasicSalary = MinBasicSalary.Replace("$ ", "")
            .MaxBasicSalary = MaxBasicSalary.Replace("$ ", "")
            .MinHourAmount = MinHourAmount
            .MaxHourAmount = MaxHourAmount
            .Simulation = Simulation
            .HandlesTurnsChart = HandlesTurnsChart
            .NightlyChargeAuthorization = NightlyChargeAuthorization
            .RepresentationCost = RepresentationCost
            .INSCode = INSCode
            .CCSSCode = CCSSCode
            .ResolutionNumber = INDtxtResolutionName.EditValue
            .BeforeResolutionNumber = INDtxtAfterResolutionName.EditValue
            .State = Status
        End With
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(PositionCode) Then
            Using model As New MPosition(Me.Tag)
                AsyncLoader(True)
                Dim state As Boolean = Not Position.State
                Dim Result = Await model.ChangeStatePosition(PositionCode, state)
                AsyncLoader(False)
                If Result = True Then
                    Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                    Me.Position.State = state
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")

                End If
            End Using
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

#End Region

#Region "Events"
    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
    End Sub
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ModoBusqueda = Nothing
        record = Nothing
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Position = Nothing
        Model = Nothing
        Presenter = Nothing
    End Sub
    ''' <summary>
    ''' Evento Load donde se ejecutan el asincrono para levantar la definicion del layout
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmPosition_Load(sender As Object, e As EventArgs) Handles Me.Load
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = AddressOf GenerateDoc

        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayroll", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PPosition(Me)
        SetCurrencyFormat(Presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        Presenter.Initializes()
        LoadStatus()
        Deshacer()
        ShowSocialSecurityInformation()

        'Valido si la entidad es pública para Mostrar el Control del Número de Resolución
        If indigo.IndigoCompanyType = 2 Then
            INDlyItemResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyItemAfterResolutionNumber.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub

    ''' <summary>
    ''' Elimina el reg bloqueado cuando se cierra el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPosition_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Aqui se hace la logica para consultar la entidad
    ''' </summary>
    Private Async Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        Me.ViewModeEditHold = True
        If Me.Position IsNot Nothing AndAlso Me.Position.Id > 0 Then

            If Not (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If

        If record IsNot Nothing AndAlso record.Id > 0 Then
            If record.CodUser = indigo.UserIndigo Then
                DeleteBlockedRecord()
            End If
        End If
        INDbteCode.Text = Me.IdEntity.Trim()
        Await Me.LoadControls()
        Me.IdEntity = String.Empty
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)

    End Sub


    ''' <summary>
    ''' Abre el formulariopara crear niveles de cargo
    ''' </summary>
    Private Sub INDgleProfessionalRisk_Properties_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleProfessionalRisk.Properties.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmProfessionalRisk
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                AddHandler Formulario.FormClosed, AddressOf PopupForms_Closed
                Formulario.ShowDialog()
            End Using
        End If
    End Sub

    Private Sub PopupForms_Closed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs)
        Presenter.Initializes()
    End Sub

    ''' <summary>
    ''' Evento para consultar la persona en el evento keydown de la identificacion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="Windows.Forms.KeyEventArgs"/> instance containing the event data.</param>
    Private Async Sub INDbteCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbteCode.KeyDown
        If Not String.IsNullOrEmpty(INDbteCode.Text.ToString) Then
            If e.KeyCode = System.Windows.Forms.Keys.Enter Then
                Await LoadControls()
                If INDbteCode.Enabled = False Then
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
                    BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.ControlBiometrico) = True
                End If
                INDbteCode.Enabled = False
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            AbrirBusqueda()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que me abre el frontal de busqueda desde el control.
    ''' </summary>
    Private Sub INDbteCode_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbteCode.ButtonClick
        AbrirBusqueda()
    End Sub

    ''' <summary>
    ''' eventio que abre el form de creacion de niveles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDglePositionLevel_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDglePositionLevel.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmPositionLevel
                Formulario.ViewModeEditHold = True
                Dim frm As New FrmTransparent(Formulario, False)
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                frm.ShowDialog()
                Presenter.Initializes()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento que abre el form de busqueda de profesionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleProfessionalRisk_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDgleProfessionalRisk.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmProfessionalRisk
                Formulario.ViewModeEditHold = True
                Dim frm As New FrmTransparent(Formulario, False)
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                frm.ShowDialog()
                Presenter.Initializes()
            End Using
        End If
    End Sub
#End Region

#Region "Bar Buttons Events"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbteCode.Enabled Then
            INDbteCode.Focus()
        End If
    End Sub
    ''' <summary>
    ''' Evento barra de botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    '''Evento load de la barra de botones
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
        ModoBusqueda = False
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

#Region "Customization"

    ''' <summary>
    ''' Metodo para abrir el formulario de customizar el frontal
    ''' </summary>
    Private Sub CustomizationOpen()
        INDlyCtrPosition.ShowCustomizationForm()
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
            INDlyCtrPosition.RestoreLayoutFromXml(PathFunctionalDefinitions)
        End If
    End Sub
    ''' <summary>
    ''' Evento que se utiliza para abrir el frontal de customizacion de funcionales verifica si tiene permisos para posteriormente permitir customizar
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    Private Sub INDlyUsuarios_ShowCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrPosition.ShowCustomization
        Try
            'Ejecuatamos la consulta
            Using model As New MPosition(MyBase.Tag)
                Dim dsFields As DataSet = model.GetFieldsNULL
                If dsFields IsNot Nothing Then
                    dtFieldsCustomizables = dsFields.Tables(0)
                    For j As Integer = 0 To INDlyCtrPosition.Items.Count - 1
                        INDlyCtrPosition.Items.Item(j).AllowHide = False
                        For i As Integer = 0 To dtFieldsCustomizables.Rows.Count - 1
                            If Object.Equals(INDlyCtrPosition.Items.Item(j).Tag, Nothing) = False Then
                                If dtFieldsCustomizables.Rows(i).Item("NAME").ToString.Trim = INDlyCtrPosition.Items.Item(j).Tag.ToString.Trim Then
                                    INDlyCtrPosition.Items.Item(j).AllowHide = True
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
    Private Sub INDLyCentroAtencion_HideCustomization(ByVal sender As Object, ByVal e As System.EventArgs) Handles INDlyCtrPosition.HideCustomization
        'Preguntamos si el layout ha sido modificado en el algun momento para posteriormente guardar la definicion
        If INDlyCtrPosition.IsModified = True Then
            Try
                If CustomizacionFrontales.VerificaCarpetaDefinicionFuncionales(String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name)) = True Then
                    INDlyCtrPosition.SaveLayoutToXml(PathFunctionalDefinitions)
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
            INDlyCtrPosition.RestoreDefaultLayout()
            Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesLayoutRestablecido)
        End If
    End Sub

#End Region

    Private Sub CtrNavigationControlPanel1_Paint(sender As Object, e As System.Windows.Forms.PaintEventArgs)

    End Sub
End Class