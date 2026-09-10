'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Diego A. Roldán L.
' Created          : 2023-03-24
'
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Spreadsheet
Imports DevExpress.Xpo
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraLayout
Imports DevExpress.XtraSpreadsheet
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Infrastructure.Data.Xpo.SecurityRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.Inventory.MVP
#End Region

Public Class FrmRequestParam
    Implements IRequestParam

    Public Sub New()
        InitializeComponent()
        INDExpDetails.AddRangeColumns("Tipo", "Codigo Producto/Insumo", "Cantidad")
        INDSleFrequense.Properties.DataSource = Me.ListFrequency
    End Sub

#Region "Fields"
    ''' <summary>
    ''' bandera para identificar las secuencias de los días entre mes
    ''' </summary>
    Private daysProgrammed As Boolean = False

    ''' <summary>
    ''' Request param prodict
    ''' </summary>
    Private _requestParamProduct As RequestParamProduct = Nothing

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As InventorySequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Integer

    ''' <summary>
    ''' Id de la configuracion de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Long

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordInventory

    ''' <summary>
    ''' Representa el presentador de rangos uvr
    ''' </summary>
    ''' <remarks></remarks>
    Private _presenter As PRequestParam

    ''' <summary>
    ''' Representa dia vs control
    ''' </summary>
    Private dictionaryDaysLayout As Dictionary(Of String, List(Of LayoutControlItem))

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Inventory"

    ''' <summary>
    ''' representa la entidad de parámetros de solicitud
    ''' </summary>
    ''' <remarks></remarks>
    Private _requestParam As RequestParam


    ''' <summary>
    ''' Listado de usuarios autorizados
    ''' </summary>
    Private ListRequestParamAuthUser As List(Of ViewRequestParamAuthUserXpo)


#End Region

#Region "Properties"
    ''' <summary>
    ''' Estado del registro
    ''' </summary>
    ''' <returns></returns>
    Public Property Status As Boolean Implements IRequestParam.Status
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
    ''' Layout principal
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IRequestParam.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Habilita o deshabilita controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IRequestParam.ActionsOnControls
        Set(value As Boolean)
            INDLcRoot.BeginUpdate()
            INDBeCode.Enabled = Not value
            INDPceFunctionalUnit.Enabled = value
            INDPceWarehouse.Enabled = value
            INDSleFrequense.Enabled = value
            INDSleUnitTime.Enabled = value
            INDGleRequiredAuthorization.Enabled = value
            INDPceAdd.Enabled = value
            INDGcProduct.Enabled = value
            INDExpDetails.Enabled = value
            INDBtnImportFile.Enabled = value
            INDgcAuthorizers.Enabled = value
            INDLcRoot.EndUpdate()

            If value Then
                INDPceFunctionalUnit.Focus()
            Else
                INDBeCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Tag del formulario
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As String Implements IRequestParam.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Código
    ''' </summary>
    ''' <returns></returns>
    Public Property Code As String Implements IRequestParam.Code
        Get
            If (INDBeCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDBeCode.Text
            End If
        End Get
        Set(value As String)
            INDBeCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Requiere autorización
    ''' </summary>
    ''' <returns></returns>
    Public Property RequiredAuthorization As Boolean Implements IRequestParam.RequiredAuthorization
        Get
            Return INDGleRequiredAuthorization.EditValue
        End Get
        Set(value As Boolean)
            INDGleRequiredAuthorization.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Fecha inicial
    ''' </summary>
    ''' <returns></returns>
    Public Property InitialDate As Date? Implements IRequestParam.InitialDate
        Get
            Return INDDeInitialDate.EditValue
        End Get
        Set(value As Date?)
            INDDeInitialDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestParamFunctionalUnits As XPCollection Implements IRequestParam.RequestParamFunctionalUnits
        Get
            Return INDGcFunctionalUnit.DataSource
        End Get
        Set(value As XPCollection)
            INDGcFunctionalUnit.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Unidad de tiempo
    ''' </summary>
    ''' <returns></returns>
    Public Property UnitTime As Integer? Implements IRequestParam.UnitTime
        Get
            Return INDSleUnitTime.EditValue
        End Get
        Set(value As Integer?)
            INDSleUnitTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Frecuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Frecuency As Integer Implements IRequestParam.Frecuency
        Get
            Return INDSleFrequense.EditValue
        End Get
        Set(value As Integer)
            INDSleFrequense.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Mes
    ''' </summary>
    ''' <returns></returns>
    Public Property Month As Integer? Implements IRequestParam.Month
        Get
            Return INDSleMonth.EditValue
        End Get
        Set(value As Integer?)
            INDSleMonth.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del control frecuencias
    ''' </summary> 
    Dim fListFrequency As New List(Of Tuple(Of Integer, String))
    Private ReadOnly Property ListFrequency As List(Of Tuple(Of Integer, String))
        Get
            If fListFrequency?.Count = 0 Then
                For d = 0 To 30 Step 1
                    fListFrequency.Add(New Tuple(Of Integer, String)(d, IIf(d = 0, "Todos los días", d.ToString())))
                Next
            End If
            Return fListFrequency
        End Get
    End Property

    ''' <summary>
    ''' Días de la semana
    ''' </summary>
    ''' <returns></returns>
    Private Property DaysOfWeek As String
        Get
            Return INDCbeDaysOfWeek.EditValue
        End Get
        Set(value As String)
            INDCbeDaysOfWeek.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Describe la periodicidad
    ''' </summary>
    Private WriteOnly Property ProgrammedDescription As String
        Set(value As String)
            INDMeProgrammedDescription.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia
    ''' </summary>
    ''' <returns></returns>
    Public Property Sequence As InventorySequence Implements IRequestParam.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As InventorySequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As InventorySequenceDetail In Me._sequence.InventorySequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    ''' <summary>
    ''' Mensaje
    ''' </summary>
    ''' <param name="Icono"></param>
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

    ''' <summary>
    ''' Descripción de programación realizada
    ''' </summary>
    ''' <returns></returns>
    Public Property ParameterizationDescription() As String
        Get
            Return String.Empty
        End Get
        Set(value As String)
            INDMeProgrammedDescription.Properties.NullText = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del lunes en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property MondaySequence As String
        Get
            Return INDCcbMon.EditValue
        End Get
        Set(value As String)
            INDCcbMon.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del martes en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property TuesdaySequence As String
        Get
            Return INDCcbTue.EditValue
        End Get
        Set(value As String)
            INDCcbTue.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del miercoles en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property WednesdaySequence As String
        Get
            Return INDCcbWed.EditValue
        End Get
        Set(value As String)
            INDCcbWed.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del jueves en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property ThursdaySequence As String
        Get
            Return INDCcbThu.EditValue
        End Get
        Set(value As String)
            INDCcbThu.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del viernes en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property FridaySequence As String
        Get
            Return INDCcbFri.EditValue
        End Get
        Set(value As String)
            INDCcbFri.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del sabado en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property SaturdaySequence As String
        Get
            Return INDCcbSat.EditValue
        End Get
        Set(value As String)
            INDCcbSat.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Secuencia o periodicidad del domingo en el mes
    ''' </summary>
    ''' <returns></returns>
    Public Property SundaySequence As String
        Get
            Return INDCcbSun.EditValue
        End Get
        Set(value As String)
            INDCcbSun.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Almacenes
    ''' </summary>
    ''' <returns></returns>
    Public Property RequestParamWarehouse As XPCollection Implements IRequestParam.RequestParamWarehouse
        Get
            Return INDGcWarehouse.DataSource
        End Get
        Set(value As XPCollection)
            INDGcWarehouse.DataSource = value
        End Set
    End Property


#End Region

#Region "Enums"
    Public Enum eUnitTime
        Day = 1
        Week
        Month
        Year
    End Enum
#End Region

#Region "Methods and functions"
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    Public Async Sub Guardar() Implements ICrudBase.Guardar
        Try
            If Not ValidateControls() Then
                Exit Sub
            End If

            If Not AssigningValues() Then
                Exit Sub
            End If

            Using model As New MRequestParam(Tag.ToString())
                Me.AsyncLoader(True)
                Dim result = Await model.SaveRequestPara(_requestParam, _idCurrentSequence)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _requestParam.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._requestParam = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDBeCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Crea una nueva entidad
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If _sequence?.IsManual Then
            Deshacer()
        Else
            Await NewRequestParam()
        End If
    End Sub

    ''' <summary>
    ''' Limpia el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
    End Sub

    Public Async Sub Eliminar() Implements ICrudBase.Eliminar
        If _requestParam IsNot Nothing AndAlso _requestParam.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using model As New MRequestParam(Me.Tag.ToString())
                    AsyncLoader(True)
                    AssigningValues()
                    Dim result = Await model.DeleteRequestParam(_requestParam)
                    If result.StateResult Then
                        Await Me.DeleteDocumentIndexed()
                        AsyncLoader(False)
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("RecordDeleted")
                        Me.Deshacer()
                    Else
                        AsyncLoader(False)
                        If result.MessageResult(0) = "-999" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        ElseIf result.MessageResult(0) = "-000" Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorDependence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            End If
        End If
    End Sub

    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Requiere Aut.", .FieldName = "RequiredAuthorizationName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StateName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.25)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = eDataSource.ListRequestParam
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

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
            If INDBeCode.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDBeCode.Enabled = False
        End If
    End Sub

    Private Sub CleanControls()
        INDLcRoot.BeginUpdate()
        ActionsOnControls = False
        BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me.BarraBotones.StatusRecordVisible = False
        Me._doc = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()

        Status = True
        Code = String.Empty
        ProgrammedDescription = String.Empty

        INDGcProduct.DataSource = Nothing
        INDGcFunctionalUnit.DataSource = Nothing
        INDPceFunctionalUnit.Properties.NullText = ""

        INDGcWarehouse.DataSource = Nothing
        INDPceWarehouse.Properties.NullText = ""

        _requestParam = Nothing

        'Limpio control de programación
        daysProgrammed = False
        INDSleFrequense.EditValue = Nothing
        INDSleUnitTime.EditValue = Nothing
        DaysOfWeek = Nothing
        CleanControlSequenseDays()
        INDCbeDaysOfWeek.Properties.NullText = String.Empty
        INDSleUnitTime.Properties.NullText = String.Empty
        ShowHideLayaouts({INDLciDaysOfWeek, INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence _
                         , INDLciTue, INDLciTueSequence, INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence _
                         , INDLciFri, INDLciFriSequence, INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence _
                         , INDLciMonth, INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)

        INDgcAuthorizers.DataSource = Nothing
        GetAllUsers()
        INDLcRoot.EndUpdate()
        DeleteBlockedRecord()
    End Sub

    Private Sub CleanControlSequenseDays()
        MondaySequence = Nothing
        TuesdaySequence = Nothing
        WednesdaySequence = Nothing
        ThursdaySequence = Nothing
        FridaySequence = Nothing
        SaturdaySequence = Nothing
        SundaySequence = Nothing
    End Sub

    Function AssigningValues() As Boolean
        With _requestParam
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .State = Status
            .Frecuency = Frecuency
            .MeasuryUnitTime = UnitTime
            .InitialDate = InitialDate
            .RequiredAuthorization = RequiredAuthorization
            If .ClosedDate Is Nothing Then
                .ClosedDate = InitialDate
            End If
        End With
        _requestParam.RequestParamFuncionalUnit.Clear()
        Dim selected = CType(INDGcFunctionalUnit.DataSource, List(Of ViewRequestParamFunctionalUnitXpo)).Where(Function(m) m.IsSelected).ToList()
        selected.ForEach(Sub(m)
                             Dim item = New RequestParamFuncionalUnit With {
                                .Id = Convert.ToInt32(m.RequestParamFuncionalUnitId),
                                .RequestParamId = _requestParam.Id,
                                .FunctionalUnitId = m.FunctionalUnitId
                             }
                             If item.Id > 0 Then
                                 item.MarkAsModified()
                             End If
                             _requestParam.RequestParamFuncionalUnit.Add(item)
                         End Sub)

        _requestParam.RequestParamWarehouse.Clear()
        Dim selectedWarehouse = CType(INDGcWarehouse.DataSource, List(Of ViewRequestParamWarehouseXpo)).Where(Function(m) m.IsSelected).ToList()
        selectedWarehouse.ForEach(Sub(m)
                                      Dim item = New RequestParamWarehouse With {
                                .Id = Convert.ToInt32(m.RequestParamWarehouseId),
                                .RequestParamId = _requestParam.Id,
                                .WarehouseId = m.WarehouseId
                             }
                                      If item.Id > 0 Then
                                          item.MarkAsModified()
                                      End If
                                      _requestParam.RequestParamWarehouse.Add(item)
                                  End Sub)
        _requestParam.RequestParamDetailPeriodicity.Clear()
        If Not String.IsNullOrEmpty(DaysOfWeek) Then
            For Each d In DaysOfWeek.Split(",")
                Dim item = New RequestParamDetailPeriodicity With {
                    .IdRequestParam = _requestParam.Id,
                    .Month = Month,
                    .SequenceOfMonth = GetOrSetSequence(d.Trim(), 1),
                    .Days = d.Trim()
                    }
                _requestParam.RequestParamDetailPeriodicity.Add(item)
            Next
        End If

        Dim selectedUserAuth = CType(INDgcAuthorizers.DataSource, List(Of ViewRequestParamAuthUserDto)).ToList()
        Dim ListNewRequestParamAuthUser As New List(Of RequestParamAuthUser)
        Dim ListErrorsParamAuthUser As New List(Of String)


        For Each m In selectedUserAuth

            If m.UserPrincipalId IsNot Nothing Then
                If m.CodeType = 2 Then
                    If m.UserPrincipalId IsNot Nothing AndAlso m.UserAlternativeId IsNot Nothing Then
                        If m.UserPrincipalId = m.UserAlternativeId Then
                            ListErrorsParamAuthUser.Add($"En el almacen {m.CodeName} el autorizador secundario no puede ser igual al autorizador principal")
                        End If
                    End If
                End If
                If m.CodeType = 1 Then
                    If m.UserPrincipalId IsNot Nothing AndAlso m.UserAlternativeId IsNot Nothing Then
                        If m.UserPrincipalId = m.UserAlternativeId Then
                            ListErrorsParamAuthUser.Add($"En la unidad funcional {m.CodeName} el autorizador secundario no puede ser igual al autorizador principal")
                        End If
                    End If
                End If

                Dim item As New RequestParamAuthUser With {
                    .RequestParamId = _requestParam.Id
                }
                If m.IdRequestParamAuthUser > 0 Then
                    item.Id = m.IdRequestParamAuthUser
                End If

                If m.CodeType = 2 Then
                    item.WarehouseId = m.IdType
                    If m.UserPrincipalId IsNot Nothing And m.UserPrincipalId > 0 Then
                        item.UserPrincipalWarehouseId = m.UserPrincipalId
                    End If
                    If m.UserAlternativeId IsNot Nothing And m.UserAlternativeId > 0 Then
                        item.UserAlternateWarehouseId = m.UserAlternativeId
                    End If
                End If
                If m.CodeType = 1 Then
                    item.FunctionalUnitId = m.IdType
                    If m.UserPrincipalId IsNot Nothing And m.UserPrincipalId > 0 Then
                        item.UserPrincipalFunctionalUnitId = m.UserPrincipalId
                    End If
                    If m.UserAlternativeId IsNot Nothing And m.UserAlternativeId > 0 Then
                        item.UserAlternateFunctionalUnitId = m.UserAlternativeId
                    End If
                End If

                If m.CodeType = 1 Then
                    Dim existingNewItem = ListNewRequestParamAuthUser.FirstOrDefault(Function(x) x.FunctionalUnitId.HasValue = If(item.FunctionalUnitId Is Nothing, 0, item.FunctionalUnitId))
                    If existingNewItem Is Nothing Then
                        Dim existingOneWarehouse = ListNewRequestParamAuthUser.FirstOrDefault(Function(x) x.WarehouseId.HasValue AndAlso x.WarehouseId.Value > 0 AndAlso x.FunctionalUnitId Is Nothing)
                        If existingOneWarehouse IsNot Nothing Then
                            existingOneWarehouse.FunctionalUnitId = m.IdType
                            If m.UserPrincipalId IsNot Nothing And m.UserPrincipalId > 0 Then
                                existingOneWarehouse.UserPrincipalFunctionalUnitId = m.UserPrincipalId
                            End If
                            If m.UserAlternativeId IsNot Nothing And m.UserAlternativeId > 0 Then
                                existingOneWarehouse.UserAlternateFunctionalUnitId = m.UserAlternativeId
                            End If
                        Else
                            ListNewRequestParamAuthUser.Add(item)
                        End If
                    End If
                End If
                If m.CodeType = 2 Then
                    Dim existingNewItem = ListNewRequestParamAuthUser.FirstOrDefault(Function(x) x.WarehouseId.HasValue = If(item.WarehouseId Is Nothing, 0, item.WarehouseId))
                    If existingNewItem Is Nothing Then
                        Dim existingOneFunctionalUnit = ListNewRequestParamAuthUser.FirstOrDefault(Function(x) x.FunctionalUnitId.HasValue AndAlso x.FunctionalUnitId.Value > 0 AndAlso x.WarehouseId Is Nothing)
                        If existingOneFunctionalUnit IsNot Nothing Then
                            existingOneFunctionalUnit.WarehouseId = m.IdType
                            If m.UserPrincipalId IsNot Nothing And m.UserPrincipalId > 0 Then
                                existingOneFunctionalUnit.UserPrincipalWarehouseId = m.UserPrincipalId
                            End If
                            If m.UserAlternativeId IsNot Nothing And m.UserAlternativeId > 0 Then
                                existingOneFunctionalUnit.UserAlternateWarehouseId = m.UserAlternativeId
                            End If
                        Else
                            ListNewRequestParamAuthUser.Add(item)
                        End If
                    End If
                End If
            Else
                ' Validaciones basadas en el tipo de entidad
                ' Se verifica si el almacén o unidad funcional tiene usuarios parametrizados.
                ' Si no tiene usuarios parametrizados, se permite guardar la configuración,
                ' ya que se requiere que el valor de "requiere autorización" esté configurado primero.
                If m.CodeType = 2 Then
                    If GetAllUserByWarehouse(m.Code).Count > 0 Then
                        ListErrorsParamAuthUser.Add($"El almacén {m.CodeName}  no tiene asignado autorizador")
                    End If
                    If m.UserPrincipalId IsNot Nothing AndAlso m.UserAlternativeId IsNot Nothing Then
                        If m.UserPrincipalId = m.UserAlternativeId Then
                            ListErrorsParamAuthUser.Add($"En el almacén {m.CodeName} el autorizador secundario no puede ser igual al autorizador principal")
                        End If
                    End If
                End If
                If m.CodeType = 1 Then
                    If GetAllUserByFuncionalUnit(m.Code).Count > 0 Then
                        ListErrorsParamAuthUser.Add($"La unidad funcional  {m.CodeName}  no tiene asignado autorizador")
                    End If
                    If m.UserPrincipalId IsNot Nothing AndAlso m.UserAlternativeId IsNot Nothing Then
                        If m.UserPrincipalId = m.UserAlternativeId Then
                            ListErrorsParamAuthUser.Add($"En la unidad funcional {m.CodeName} el autorizador secundario no puede ser igual al autorizador principal")
                        End If
                    End If
                End If
            End If
        Next

        If ListErrorsParamAuthUser.Count > 0 Then
            Using formulario As New FrmListErrors(ListErrorsParamAuthUser)
                formulario.StartPosition = FormStartPosition.CenterParent
                Dim size As System.Drawing.Size
                size.Width = 950
                size.Height = 500
                formulario.Size = size
                Dim transparent As New FrmTransparent(formulario, False)
                transparent.ShowDialog(Me)
                Return False
            End Using
        End If
        If ListNewRequestParamAuthUser.Any Then
            _requestParam.RequestParamAuthUser.Clear()
            For Each item In ListNewRequestParamAuthUser
                _requestParam.RequestParamAuthUser.Add(item)
            Next
        End If
        Return True

    End Function

    ''' <summary>
    ''' Obtiene o establece las secuencia del día en el mes
    ''' </summary>
    ''' <param name="day"></param>
    ''' <param name="action">1: Obtener - 2: Asignar</param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Function GetOrSetSequence(day As String, action As Integer, Optional value As String = Nothing) As String
        Dim daysToCheck As String() = {MondaySequence, TuesdaySequence, WednesdaySequence, ThursdaySequence, FridaySequence, SaturdaySequence, SundaySequence}
        Dim valueInt As Integer

        If Not Integer.TryParse(day, valueInt) Then
            Return String.Empty
        End If

        If action = 1 Then
            Return daysToCheck(valueInt - 1)
        Else
            Dim propertiesMap As New Dictionary(Of String, DevExpress.XtraEditors.CheckedComboBoxEdit) From
            {
                {"1", INDCcbMon},
                {"2", INDCcbTue},
                {"3", INDCcbWed},
                {"4", INDCcbThu},
                {"5", INDCcbFri},
                {"6", INDCcbSat},
                {"7", INDCcbSun}
            }
            If propertiesMap.ContainsKey(day) Then
                Dim description = String.Join(", ", value.Split(","c).Select(AddressOf getDecriptionSequence))
                propertiesMap(day).EditValue = value
                propertiesMap(day).Properties.NullText = description
            End If
            Return String.Empty
        End If
    End Function

    ''' <summary>
    ''' Prepara los controles y realiza la logica para 
    ''' crear una nueva dependencia
    ''' </summary>
    Private Async Function NewRequestParam() As Task
        _requestParam = New RequestParam With {.State = True}
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.InventorySequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.InventorySequenceDetail.Any(Function(o) o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.InventorySequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit = Me._idOperativeUnit).Id
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
                        Using model As New MBlockRecordAndSequense(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenseGroup(CInt(Me._idCurrentSequence))
                        End Using
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
        Await LoadFunctionalUnits()
        Await LoadWarehouse()

    End Function

    Private Async Sub DeleteBlockedRecord()
        If _record Is Nothing Then Exit Sub
        Using Model As New MBlockRecordAndSequense(CStr(Me.Tag))
            If _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
                Await Model.DeleteBlockRecord(_record)
                _record = Nothing
            End If
        End Using
    End Sub

    Private Async Function LoadControls() As Task
        Try
            If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
                If Me.BarraBotones.PermiteConsultar = False Then
                    Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                    Exit Function
                End If

                Me.BarraBotones.StatusRecordVisible = True
                Using model As New MRequestParam(CStr(Me.Tag))
                    AsyncLoader(True)
                    _requestParam = Await model.GetRequestParamByCodeAsync(INDBeCode.Text.Trim)
                    If _requestParam IsNot Nothing AndAlso _requestParam.Id > 0 Then
                        Using ModelRecord As New MBlockRecordAndSequense(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_requestParam.Id))
                            With _requestParam
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                Code = .Code
                                Status = .State
                                Frecuency = .Frecuency
                                UnitTime = .MeasuryUnitTime
                                InitialDate = .InitialDate
                                RequiredAuthorization = .RequiredAuthorization
                                Month = _requestParam.RequestParamDetailPeriodicity?.FirstOrDefault()?.Month
                                If .MeasuryUnitTime = eUnitTime.Month OrElse .MeasuryUnitTime = eUnitTime.Year Then
                                    Me.daysProgrammed = True
                                Else
                                    Me.daysProgrammed = False
                                End If
                            End With

                            Await LoadFunctionalUnits()
                            Await LoadWarehouse()
                            LoadSelectedItemsDescription()
                            LoadSelectedItemsWarehouse()
                            INDGcProduct.DataSource = _requestParam.RequestParamProduct.ToList()
                            INDGcProduct.RefreshDataSource()
                            LoadPeriodicity(_requestParam.RequestParamDetailPeriodicity.ToList())
                            LoadAuthorizersUser()
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._requestParam.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordInventory With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .FormId = Me.Tag, .CodUser = Me.indigo.UserIndigo, .RecordId = _requestParam.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_requestParam.Id)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True
                        End Using
                        SetNullText()
                        WriteMemoEdit()
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewRequestParam()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDBeCode.Focus()
                        End If
                    End If
                End Using
            End If
        Catch ex As Exception
            AsyncLoader(False)
            INDBeCode.Enabled = False
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Carga listado  de usuarios autorizados dependendiendo de los almacenes/unidad funcional seleccionado
    ''' </summary>
    Private Sub LoadAuthorizersUser()
        Dim TempList = New List(Of ViewRequestParamAuthUserDto)
        Dim ListXpo = _presenter.ListRequestParamAuthUsers(Code)
        For Each item In ListXpo
            Dim itemDto As New ViewRequestParamAuthUserDto With {
                   .Id = item.id,
                   .IdRequestParamAuthUser = item.IdRequestParamAuthUser,
                   .IdType = item.IdType,
                   .Code = item.Code,
                   .CodeRequestParam = item.CodeRequestParam,
                   .CodeName = item.CodeName,
                   .Type = item.Type,
                   .CodeType = item.CodeType,
                   .IdRequestParam = item.IdRequestParam,
                   .UserPrincipalId = item.UserPrincipalId,
                   .UserAlternativeId = item.UserAlternativeId
               }
            TempList.Add(itemDto)
        Next
        GetAllUsers()
        INDgcAuthorizers.DataSource = TempList
        INDgvAuthorizers.Columns("Type").GroupIndex = 0
        INDgvAuthorizers.ExpandAllGroups()
    End Sub


    ''' <summary>
    ''' Obtiene todos los usuarios parametrizados en el sistema
    ''' </summary>
    Private Sub GetAllUsers()
        If INDRisleUserPrincipal.DataSource Is Nothing Then
            INDRisleUserPrincipal.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, Nothing)
        End If
        If INDRisleUserAlternative.DataSource Is Nothing Then
            INDRisleUserAlternative.DataSource = XpoServiceEx.Instance(indigo.SecurityContainer).SecurityService.GetCollection(Of UserXpo)(Nothing, Nothing)
        End If
    End Sub

    Private Function GetAllUserByWarehouse(Code As String)
        Return _presenter.ListWarehouseUser(Code)
    End Function


    Private Function GetAllUserByFuncionalUnit(Code As String)
        Return _presenter.ListFunctionalUnitUser(Code)
    End Function


    ''' <summary>
    ''' LLena el null test de los controles
    ''' </summary>
    Private Sub SetNullText()
        INDSleUnitTime.Properties.NullText = GetDescriptionUnitTime(UnitTime, Frecuency)
        If Not String.IsNullOrEmpty(DaysOfWeek) Then
            Dim description = String.Join(", ", DaysOfWeek.Split(","c).Select(AddressOf getDescriptionDays))
            INDCbeDaysOfWeek.Properties.NullText = description
        End If
        If Frecuency = 0 Then 'Si la frecuencia es todos los días entonces no aplico los controles
            INDSleUnitTime.Enabled = False
        Else
            INDSleUnitTime.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Code) Then
            Try
                Using model As New MRequestParam(Me.Tag.ToString())
                    AsyncLoader(True)
                    Dim state As Boolean = Not _requestParam.State
                    Dim result = Await model.ChangeStateRequestParam(Code, state)
                    AsyncLoader(False)
                    If result.StateResult = True Then
                        Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateState")
                        _requestParam = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        If result.MessageResult(0) = ErrorConcurrencia Then
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                        Else
                            Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                        End If
                    End If
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' actions grid
    ''' </summary>
    Private Sub addColumnActions()
        IndigoGridView1.SetListAcction(INDGvProduct, {eAcciones.Edit, eAcciones.Remove}.ToList())
        Dim col = INDGvProduct.Columns.FirstOrDefault(Function(m) m.Name = "colActions")
        If col IsNot Nothing Then col.Width = 80
    End Sub

    ''' <summary>
    ''' Load functional units
    ''' </summary>
    ''' <returns></returns>
    Private Async Function LoadFunctionalUnits() As Task
        Dim filter = If(_requestParam.Id = 0, $"RequestParamFuncionalUnitId is null", $"RequestParamId is null or RequestParamId={_requestParam.Id}")
        INDGcFunctionalUnit.DataSource = Await XpoServiceEx.Instance(indigo.TransactionalContainer) _
            .InventoryService _
            .GetCollectionAsync(Of ViewRequestParamFunctionalUnitXpo)(Nothing, filter, False)
    End Function


    ''' <summary>
    ''' Carga los almacenes
    ''' </summary>                                                                      
    ''' <returns></returns>
    Private Async Function LoadWarehouse() As Task
        Dim filter = If(_requestParam.Id = 0, $"RequestParamWarehouseId is null  ", $"RequestParamId is null or RequestParamId={_requestParam.Id}")
        filter = filter + " and StatusWarehouse = 1 "
        INDGcWarehouse.DataSource = Await XpoServiceEx.Instance(indigo.TransactionalContainer) _
         .InventoryService _
        .GetCollectionAsync(Of ViewRequestParamWarehouseXpo)(Nothing, filter, False)
    End Function

    ''' <summary>
    ''' Asigna los controles de periodicidad
    ''' </summary>
    ''' <param name="details"></param>
    Private Sub LoadPeriodicity(details As List(Of RequestParamDetailPeriodicity))
        PrepareControlsDays()
        If details IsNot Nothing Then
            Dim days As List(Of String) = details.Select(Function(s) s.Days).ToList()
            DaysOfWeek = String.Join(",", days)
            If UnitTime = eUnitTime.Month OrElse UnitTime = eUnitTime.Year Then
                For Each k In dictionaryDaysLayout.Keys
                    If days.Contains(k) Then
                        ShowHideLayaouts(dictionaryDaysLayout(k), DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        Dim sequenceOfMonth As String = details.Where(Function(w) w.Days = k).Select(Function(s) s.SequenceOfMonth).FirstOrDefault()
                        GetOrSetSequence(k, 2, sequenceOfMonth)
                    Else
                        ShowHideLayaouts(dictionaryDaysLayout(k), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                    End If
                Next
            End If
        End If
    End Sub

    ''' <summary>
    ''' set description imtes selected
    ''' </summary>
    Private Sub LoadSelectedItemsDescription()
        If INDGcFunctionalUnit.DataSource IsNot Nothing Then
            Dim count = TryCast(INDGcFunctionalUnit.DataSource, List(Of ViewRequestParamFunctionalUnitXpo)).LongCount(Function(m) m.IsSelected)
            INDPceFunctionalUnit.Properties.NullText = IIf(count > 1, $"{count} items seleccionados", $"{count} item seleccionado")
        End If
    End Sub

    Private Sub LoadSelectedItemsWarehouse()
        If INDGcWarehouse.DataSource IsNot Nothing Then
            Dim count = TryCast(INDGcWarehouse.DataSource, List(Of ViewRequestParamWarehouseXpo)).LongCount(Function(m) m.IsSelected)
            INDPceWarehouse.Properties.NullText = IIf(count > 1, $"{count} items seleccionados", $"{count} item seleccionado")
        End If
    End Sub

    Private Function GetSelectedItemsWarehouse()
        Return CType(INDGcWarehouse.DataSource, List(Of ViewRequestParamWarehouseXpo)).Where(Function(m) m.IsSelected).ToList()
    End Function


    Private collection As XPCollection(Of List(Of ViewRequestParamAuthUserXpo))

    Private Function GetSelectedItemsFuntionalUnit()
        Return CType(INDGcFunctionalUnit.DataSource, List(Of ViewRequestParamFunctionalUnitXpo)).Where(Function(m) m.IsSelected).ToList()
    End Function



    ''' <summary>
    ''' Adiciona fila en el listado de usuarios autorizados
    ''' Dependiendo de lo seleccionado en la unidad funcional o almacen
    ''' </summary>
    ''' <returns></returns>
    Private Function AddTemporaryRowAuthUser(CodeType As Integer)
        Dim ListWarehouse As List(Of ViewRequestParamWarehouseXpo) = GetSelectedItemsWarehouse()
        Dim ListFunctionalUnit As List(Of ViewRequestParamFunctionalUnitXpo) = GetSelectedItemsFuntionalUnit()
        Dim ListView As List(Of ViewRequestParamAuthUserDto) = CType(If(INDgcAuthorizers.DataSource Is Nothing, New List(Of ViewRequestParamAuthUserDto), INDgcAuthorizers.DataSource), List(Of ViewRequestParamAuthUserDto))
        Dim itemsToRemove As New List(Of ViewRequestParamAuthUserDto)

        ' Crear un diccionario para mejorar la búsqueda de códigos
        If CodeType = 1 Then
            Dim functionalUnitDict As New Dictionary(Of String, ViewRequestParamFunctionalUnitXpo)()
            For Each functionalUnit In ListFunctionalUnit
                functionalUnitDict(functionalUnit.FunctionalUnitCode) = functionalUnit
            Next
            For Each item In ListView
                If item.CodeType = 1 Then
                    ' Verifica si el código existe en la lista de unidades funcionales seleccionadas
                    Dim exists As Boolean = functionalUnitDict.ContainsKey(item.Code)
                    ' Si no existe en ListFunctionalUnit, añade el elemento a la lista de elementos a eliminar
                    If Not exists Then
                        itemsToRemove.Add(item)
                    End If
                End If
            Next
        ElseIf CodeType = 2 Then
            Dim warehouseDict As New Dictionary(Of String, ViewRequestParamWarehouseXpo)()
            For Each warehouse In ListWarehouse
                warehouseDict(warehouse.WarehouseCode) = warehouse
            Next
            For Each item In ListView
                If item.CodeType = 2 Then
                    Dim exists As Boolean = warehouseDict.ContainsKey(item.Code)
                    If Not exists Then
                        itemsToRemove.Add(item)
                    End If
                End If
            Next
        End If
        ' Elimina los elementos que no existen en la lista seleccionada
        For Each itemToRemove In itemsToRemove
            ListView.Remove(itemToRemove)
        Next

        If CodeType = 1 Then
            For Each functionalUnit In ListFunctionalUnit
                Dim exists As Boolean = ListView.Any(Function(x) x.Code = functionalUnit.FunctionalUnitCode)
                If Not exists Then
                    Dim itemDto As New ViewRequestParamAuthUserDto With {
                            .IdRequestParamAuthUser = 0,
                            .IdType = functionalUnit.Id,
                            .Code = functionalUnit.FunctionalUnitCode,
                            .CodeName = functionalUnit.FunctionalUnitCodeName,
                            .Type = "Unidad funcional",
                            .CodeType = 1,
                            .IdRequestParam = _requestParam.Id,
                            .UserPrincipalId = Nothing,
                            .UserAlternativeId = Nothing
                        }
                    ListView.Add(itemDto)
                End If
            Next
        ElseIf CodeType = 2 Then
            For Each warehouse In ListWarehouse
                Dim exists As Boolean = ListView.Any(Function(x) x.Code = warehouse.WarehouseCode)
                If Not exists Then
                    Dim itemDto As New ViewRequestParamAuthUserDto With {
                            .IdRequestParamAuthUser = 0,
                            .IdType = warehouse.Id,
                            .Code = warehouse.WarehouseCode,
                            .CodeName = warehouse.WarehouseCodeName,
                            .Type = "Almacén",
                            .CodeType = 2,
                            .IdRequestParam = _requestParam.Id,
                            .UserPrincipalId = Nothing,
                            .UserAlternativeId = Nothing
                        }
                    ListView.Add(itemDto)
                End If
            Next
        End If
        INDgcAuthorizers.DataSource = ListView
        INDgcAuthorizers.RefreshDataSource()
        INDgvAuthorizers.FocusedRowHandle = INDgvAuthorizers.RowCount
    End Function

    ''' <summary>
    ''' Este metodo me describe las periodicidadades seleccionadas
    ''' </summary>
    Private Sub WriteMemoEdit()
        Dim description As New StringBuilder
        description.Append("Se podrán hacer solicitudes")
        If Frecuency = 0 Then
            description.Append(" todos los días")
            ProgrammedDescription = description.ToString()
            Exit Sub
        Else
            description.Append(String.Format(" cada {0} {1} ", Frecuency, GetDescriptionUnitTime(UnitTime, Frecuency)))
        End If
        'Entre días
        If Frecuency > 0 AndAlso daysProgrammed AndAlso Not String.IsNullOrEmpty(DaysOfWeek) Then
            description.Append($" los días {WriteDaysOrSequence(DaysOfWeek)}")
        End If
        If dictionaryDaysLayout IsNot Nothing Then
            Dim daysToCheck As String() = {MondaySequence, TuesdaySequence, WednesdaySequence, ThursdaySequence, FridaySequence, SaturdaySequence, SundaySequence}
            For Each k In dictionaryDaysLayout.Keys
                If LayoutIsVisible(dictionaryDaysLayout(k)) Then
                    Dim valueInt As Integer
                    If Integer.TryParse(k, valueInt) Then
                        Dim sequenseCheck = daysToCheck(valueInt - 1)
                        If sequenseCheck IsNot Nothing AndAlso Not String.IsNullOrEmpty(sequenseCheck) Then
                            description.Append($", {WriteDaysOrSequence(sequenseCheck, 2)} {getDescriptionDays(k)} ")
                        End If
                    End If
                End If
            Next
        End If
        'relaciono el mes
        If Month IsNot Nothing AndAlso UnitTime = eUnitTime.Year Then
            description.Append($"del mes {IIf(Month > 0, getDescriptionMonth(Month), "")}")
        End If
        ProgrammedDescription = description.ToString()
    End Sub

    ''' <summary>
    ''' Me describe los días o secuencias seleccionadas
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Function WriteDaysOrSequence(value As String, Optional ref As Integer = 1) As String
        If value Is Nothing Then Return String.Empty
        Dim values As String() = value.Split(",")
        Dim chain As String = String.Join(", ", values.Select(Function(d) IIf(ref = 1, getDescriptionDays(d.Trim), getDecriptionSequence(d.Trim))))
        If values.Length > 1 Then
            'Busco la posición de la última coma
            Dim lastCommaIndex As Integer = chain.LastIndexOf(", ")
            chain = chain.Substring(0, lastCommaIndex) & " y " & chain.Substring(lastCommaIndex + 2)
        End If
        Return chain
    End Function

    ''' <summary>
    ''' Retorna los días descritos
    ''' </summary>
    ''' <param name="day"></param>
    ''' <returns></returns>
    Private Function getDescriptionDays(day As String) As String
        Select Case day
            Case "1"
                Return "Lunes"
            Case "2"
                Return "Martes"
            Case "3"
                Return "Miércoles"
            Case "4"
                Return "Jueves"
            Case "5"
                Return "Viernes"
            Case "6"
                Return "Sábado"
            Case "7"
                Return "Domingo"
            Case Else
                Return String.Empty
        End Select
    End Function

    ''' <summary>
    ''' Retorna las unidades de tiempo descritas
    ''' </summary>
    ''' <param name="UnitTime"></param>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Function GetDescriptionUnitTime(UnitTime As Integer?, value As Integer) As String
        Select Case UnitTime
            Case 1
                Return IIf(value > 1, "días", "día")
            Case 2
                Return IIf(value > 1, "semanas", "semana")
            Case 3
                Return IIf(value > 1, "meses", "mes")
            Case 4
                Return IIf(value > 1, "años", "año")
            Case Else
                Return String.Empty
        End Select
    End Function

    ''' <summary>
    ''' Obtiene descripción se las secuencias
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Private Function getDecriptionSequence(value As String) As String
        Select Case value.Trim()
            Case "1"
                Return " Primer"
            Case "2"
                Return " Segundo"
            Case "3"
                Return " Tercer"
            Case "4"
                Return " Cuarto"
            Case "5"
                Return " Último"
            Case Else
                Return String.Empty
        End Select
    End Function

    ''' <summary>
    ''' Obtiene la descripción de los meses
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    Public Function getDescriptionMonth(value As Integer) As String
        Dim months As String() = {"Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"}
        If value > 0 Then
            Return months(value - 1)
        Else
            Return String.Empty
        End If
    End Function
#End Region

#Region "Handlers"
    ''' <summary>
    ''' Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmRequestParam_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LayoutControls.SetIsCustomizable(Me.INDLcRoot, True)
        _idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        _doc = Nothing
        indigo = SessionValues.Instance
        _presenter = New PRequestParam(Me)
        _presenter.GetSequense()
        addColumnActions()
        addTypes()
        addUnitTime()
        addDaysOfWeek()
        listMonthOfYear()
        listSequenceOfWeek({INDCcbMon, INDCcbTue, INDCcbWed, INDCcbThu, INDCcbFri, INDCcbSat, INDCcbSun}.ToList())
        LoadStatus()
        Deshacer()
        LoadDictionary()
        INDDeInitialDate.Properties.MinValue = Me.GetDateServer()
        'Oculto los controles de programación
        ShowHideLayaouts({INDLciDaysOfWeek, INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence _
                         , INDLciTue, INDLciTueSequence, INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence _
                         , INDLciFri, INDLciFriSequence, INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence _
                         , INDLciMonth, INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        GetAllUsers()
    End Sub

    ''' <summary>
    ''' Disposing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _sequence = Nothing
        _idOperativeUnit = Nothing
        _idCurrentSequence = Nothing
        _record = Nothing
        _presenter = Nothing
        _requestParam = Nothing
        dictionaryDaysLayout = Nothing
    End Sub

    ''' <summary>
    ''' Closing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

    ''' <summary>
    ''' Key down
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub INDBeCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDBeCode.KeyDown
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
                    Await NewRequestParam()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

    ''' <summary>
    ''' Activated
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmBillingGroup_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDBeCode.Text Is String.Empty Then
            INDBeCode.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles the IdEntityLoaded event of the MyBase control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    Private Sub MyBase_IdEntityLoaded(sender As Object, e As EventArgs) Handles Me.IdEntityLoaded
        If Me._requestParam IsNot Nothing AndAlso Me._requestParam.Id > 0 Then
            If (MessageIndigo.Show(obtenerRecurso(Eresources.ComunesAbrirEntidad, Eform.Comunes), MessageType.Question, obtenerRecurso(Eresources.RegistroEnEdicion, Eform.Comunes), Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                DeleteBlockedRecord()
                Me.INDBeCode.Text = Me.IdEntity.Trim()
                Me.LoadControls()
            End If
        Else 'Realiza la consulta normal
            Me.INDBeCode.Text = Me.IdEntity.Trim()
            Me.LoadControls()
            If FormSearchObjects IsNot Nothing Then
                FormSearchObjects.Close()
            End If
        End If
        Me.IdEntity = String.Empty
    End Sub

    ''' <summary>
    ''' Types
    ''' </summary>
    Private Sub addTypes()
        INDGleType.Properties.DataSource = {
            New Tuple(Of Byte, String)(1, "Insumo"),
            New Tuple(Of Byte, String)(2, "Producto")
        }
    End Sub

    ''' <summary>
    ''' Agregas las unidades de tiempo
    ''' </summary>
    Private Sub addUnitTime()
        Dim DataSource As New List(Of Tuple(Of Integer, String))
        Dim maxUnitTime As Integer = If(Frecuency = 1, 3, 4)
        For i = 1 To maxUnitTime Step 1
            DataSource.Add(New Tuple(Of Integer, String)(i, Me.GetDescriptionUnitTime(i, 1).ToUpper()))
        Next
        INDSleUnitTime.Properties.DataSource = DataSource
    End Sub

    ''' <summary>
    ''' Agrega los días al control
    ''' </summary>
    Private Sub addDaysOfWeek()
        Dim DataSource As New List(Of Tuple(Of Integer, String))
        For i = 1 To 7 Step 1
            DataSource.Add(New Tuple(Of Integer, String)(i, getDescriptionDays(i.ToString())))
        Next
        INDCbeDaysOfWeek.Properties.DataSource = DataSource
    End Sub

    ''' <summary>
    ''' Establece el datasource de una lista de control tipo CheckedComboBoxEdit
    ''' </summary>
    ''' <param name="listcontrol"></param>
    Private Sub listSequenceOfWeek(listcontrol As List(Of CheckedComboBoxEdit))
        For Each control In listcontrol
            Dim DataSource As New List(Of Tuple(Of Integer, String))
            For i = 1 To 5 Step 1
                DataSource.Add(New Tuple(Of Integer, String)(i, getDecriptionSequence(i)))
            Next
            control.Properties.DataSource = DataSource
        Next
    End Sub

    ''' <summary>
    ''' Asigna el datasource del control mes
    ''' </summary>
    Private Sub listMonthOfYear()
        Dim DataSource As New List(Of Tuple(Of Integer, String))
        For i = 1 To 12 Step 1
            DataSource.Add(New Tuple(Of Integer, String)(i, getDescriptionMonth(i)))
        Next
        INDSleMonth.Properties.DataSource = DataSource
    End Sub

    ''' <summary>
    ''' Muestran u ocultan los controles de días de la semana previamente seleccionados
    ''' </summary>
    Private Sub PrepareControlsDays()
        If UnitTime Is Nothing Then Return
        Me.daysProgrammed = {eUnitTime.Week, eUnitTime.Month, eUnitTime.Year}.Contains(UnitTime)
        If UnitTime = eUnitTime.Month OrElse UnitTime = eUnitTime.Year Then
            If Not String.IsNullOrEmpty(DaysOfWeek) Then
                ShowHideLayaouts({INDLcIDayPrograrmmed, INDLciSequenceOfMonth}.ToList(), DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                Dim selectedDays As List(Of String) = DaysOfWeek.Split(",").Select(Function(d) d.Trim()).ToList()
                For Each k In dictionaryDaysLayout.Keys
                    If selectedDays.Contains(k) Then
                        ShowHideLayaouts(dictionaryDaysLayout(k), DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                    Else
                        ShowHideLayaouts(dictionaryDaysLayout(k), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                    End If
                Next
            Else
                ShowHideLayaouts({INDLcIDayPrograrmmed, INDLciSequenceOfMonth}.ToList(), DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            End If
        Else
            ShowHideLayaouts({INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence, INDLciTue, INDLciTueSequence _
                                     , INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence, INDLciFri, INDLciFriSequence _
                                     , INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End If
    End Sub

    ''' <summary>
    ''' Asignan los diccionarios
    ''' </summary>
    Private Sub LoadDictionary()
        dictionaryDaysLayout = New Dictionary(Of String, List(Of LayoutControlItem)) From
            {
             {"1", New List(Of LayoutControlItem) From {INDLciMon, INDLciMonSequence}},
             {"2", New List(Of LayoutControlItem) From {INDLciTue, INDLciTueSequence}},
             {"3", New List(Of LayoutControlItem) From {INDLciWed, INDlciWedSequence}},
             {"4", New List(Of LayoutControlItem) From {INDLciThu, INDLciThuSequence}},
             {"5", New List(Of LayoutControlItem) From {INDLciFri, INDLciFriSequence}},
             {"6", New List(Of LayoutControlItem) From {INDlciSat, INDlciSatSequence}},
             {"7", New List(Of LayoutControlItem) From {INDlciSun, INDlciSunSequence}}
            }
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
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
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
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDBeCode.ButtonClick
        OpenSearch()
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
        Deshacer()
        Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If _sequence?.Scope.Equals("OU") AndAlso _sequence.InventorySequenceDetail IsNot Nothing Then
                If Not _sequence.InventorySequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Closed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceFunctionalUnit_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceFunctionalUnit.CloseUp
        LoadSelectedItemsDescription()
        AddTemporaryRowAuthUser(1)
    End Sub

    Private Sub INDPceWarehouse_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDPceWarehouse.CloseUp
        LoadSelectedItemsWarehouse()
        AddTemporaryRowAuthUser(2)
    End Sub

    ''' <summary>
    ''' Import
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDBtnImportFile_Click(sender As Object, e As EventArgs) Handles INDBtnImportFile.Click
        Try
            Dim openFileDialog As New OpenFileDialog()
            openFileDialog.InitialDirectory = "c:\"
            openFileDialog.Filter = "Microsoft Excel 2003 (*.xls)|*.xls|Microsoft Excel 2007 (*.xlsx)|*.xlsx"
            openFileDialog.FilterIndex = 2
            openFileDialog.RestoreDirectory = True
            openFileDialog.Title = "Importar Archivo"
            AsyncLoader(True)

            If openFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Try
                    'obtengo la rura del archivo
                    Dim myStream = openFileDialog.FileName
                    If (myStream IsNot Nothing AndAlso Not myStream.Trim().Equals(String.Empty)) Then
                        Dim sddf = New SpreadsheetControl()
                        sddf.AllowDrop = False
                        sddf.LoadDocument(myStream)
                        Dim workBook As IWorkbook = sddf.Document
                        Dim rows = workBook.Worksheets(0).Rows
                        If rows.LastUsedIndex = 0 Then
                            Mensaje(EeventViewerImages.Advertencia) = "No se encontraron registros en el archivo"
                            AsyncLoader(False)
                            Exit Sub
                        End If

                        Dim data = LoadImportFileData(rows)
                        ImportExcelFile(data)
                    End If
                Catch ex As Exception
                    Mensaje(EeventViewerImages.Advertencia) = "No se pudo leer el archivo por " + Environment.NewLine + ex.Message
                    AsyncLoader(False)
                End Try
            Else
                AsyncLoader(False)
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Load data
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <returns></returns>
    Private Function LoadImportFileData(rows As RowCollection) As List(Of List(Of Object))
        Dim listRows As New List(Of List(Of Object))()
        Dim objLock As New Object()
        Parallel.For(2, rows.LastUsedIndex + 1, Sub(x)
                                                    SyncLock objLock
                                                        listRows.Add(rows.Item(x).SpreadsheetRowToList(3))
                                                    End SyncLock
                                                End Sub)

        Return listRows
    End Function

    ''' <summary>
    ''' Paste event
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridControl1_PasteToGrid(sender As DevExpress.XtraGrid.GridControl, e As PasteToGridEventArgs) Handles IndigoGridControl1.PasteToGrid
        Try
            AsyncLoader(True)
            ImportExcelFile(e.Rows.Select(Function(m) m.Select(Of Object)(Function(o) o).ToList()).ToList())
        Catch ex As Exception
            AsyncLoader(False)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Import file
    ''' </summary>
    ''' <param name="items"></param>
    Private Async Sub ImportExcelFile(items As List(Of List(Of Object)))
        Using model As New MRequestParam(Tag)
            Dim result = Await model.LoadRequestParamProductByImportDataAsync(items)
            'si ocurrio un error
            If result.StatusCode = eStatusResult.EXCEPTION Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                AsyncLoader(False)
                Me.Cursor = Cursors.Default
                Exit Sub
            End If

            Dim resultValidation = ValidateImportData(result.ObjectEmbbeded)

            If resultValidation.Item2.Any() Then
                result.MessageResult.AddRange(resultValidation.Item2)
            End If

            If resultValidation.Item3.Any() Then
                resultValidation.Item3.ForEach(Sub(item)
                                                   item.RequestParamId = _requestParam.Id
                                                   _requestParam.RequestParamProduct.Add(item)
                                               End Sub)
            End If

            INDGcProduct.DataSource = _requestParam.RequestParamProduct.ToList()
            INDGcProduct.RefreshDataSource()
            If result.MessageResult.Count > 0 Then
                Using formulario As New FrmListErrors(result.MessageResult)
                    formulario.StartPosition = FormStartPosition.CenterParent
                    Dim transparent As New FrmTransparent(formulario, False)
                    Me.Cursor = Cursors.Default
                    transparent.ShowDialog(Me)
                End Using
            End If
        End Using
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Validate data
    ''' </summary>
    ''' <param name="objectEmbbeded"></param>
    ''' <returns></returns>
    Private Function ValidateImportData(objectEmbbeded As List(Of RequestParamProduct)) As (Boolean, List(Of String), List(Of RequestParamProduct))
        Dim errors As New List(Of String)()
        Dim added = _requestParam.RequestParamProduct.ToList()
        Dim itemsChecked As New List(Of RequestParamProduct)()
        For Each item In objectEmbbeded
            Dim ok = True
            If item.Type = 1 Then
                If added.Any(Function(m) m.Type = 1 AndAlso m.SupplieId = item.SupplieId) Then
                    errors.Add($"El insumo ({item.ProductCodeName}) ya se encuentra en el listado")
                    ok = False
                End If
            Else
                If added.Any(Function(m) m.Type = 2 AndAlso m.ProductId = item.ProductId) Then
                    errors.Add($"El producto ({item.ProductCodeName}) ya se encuentra en el listado")
                    ok = False
                End If
            End If

            If ok Then itemsChecked.Add(item)
        Next

        Return (True, errors, itemsChecked)
    End Function

    ''' <summary>
    ''' Custom display text
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGvProduct_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDGvProduct.CustomColumnDisplayText
        If e.Column.Name = INDColType.Name AndAlso e.ListSourceRowIndex >= 0 Then
            If e.Value = 1 Then
                e.DisplayText = "Insumo"
            Else
                e.DisplayText = "Producto"
            End If
        End If
    End Sub

    ''' <summary>
    ''' Button action
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction, IndigoGridView1.ContexMenuActions
        If sender.Tag.ToString() = "Edit" Then
            EditSelectedDetail()
        ElseIf sender.Tag.ToString() = "Remove" Then
            RemoveSelectedDetail()
        End If
    End Sub

    ''' <summary>
    ''' Edit detail
    ''' </summary>
    Private Sub EditSelectedDetail()
        _requestParamProduct = INDGvProduct.GetFocusedObject(Of RequestParamProduct)()
        INDGleType.EditValue = _requestParamProduct.Type
        INDSleProduct.EditValue = _requestParamProduct.ProductId
        INDSleSupplie.EditValue = _requestParamProduct.SupplieId
        INDSpnQuantity.EditValue = _requestParamProduct.Quantity

        If _requestParamProduct.Type = 1 Then
            INDSleSupplie.Properties.NullText = _requestParamProduct.ProductCodeName
        Else
            INDSleProduct.Properties.NullText = _requestParamProduct.ProductCodeName
        End If

        INDSleProduct.Enabled = False
        INDSleSupplie.Enabled = False
        INDGleType.Enabled = False
        INDSbAdd.Text = "Editar"
        INDPceAdd.ShowPopup()
    End Sub

    ''' <summary>
    ''' Remove selected item
    ''' </summary>
    Private Sub RemoveSelectedDetail()
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Dim item = INDGvProduct.GetFocusedObject(Of RequestParamProduct)()
        item.MarkAsDeleted()
        INDGcProduct.DataSource = _requestParam.RequestParamProduct.ToList()
        INDGcProduct.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Evento cambiar tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleType.EditValueChanged
        If INDGleType.EditValue Is Nothing Then
            INDLciSupplie.HideLayout()
            INDLciProduct.HideLayout()
        ElseIf INDGleType.EditValue = 1 Then
            INDLciSupplie.ShowLayout()
            INDLciProduct.HideLayout()
            INDSleProduct.EditValue = Nothing
        Else
            INDLciSupplie.HideLayout()
            INDLciProduct.ShowLayout()
            INDSleSupplie.EditValue = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Load supplie
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleSupplie_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleSupplie.QueryPopUp
        If INDSleSupplie.Properties.DataSource Is Nothing Then
            INDSleSupplie.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of InventorySupplieXpo)($"SupplieStatus=True")
        End If
    End Sub

    ''' <summary>
    ''' Load product
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleProduct_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleProduct.QueryPopUp
        If INDSleProduct.Properties.DataSource Is Nothing Then
            INDSleProduct.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of InventoryProductXpo)($"Status=True And ProductTypeId.Class = 4")
        End If
    End Sub

    ''' <summary>
    ''' Add product
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSbAdd_Click(sender As Object, e As EventArgs) Handles INDSbAdd.Click
        If Not ValidateControlsPopup() Then
            INDGleType.Focus()
            Exit Sub
        End If

        If _requestParamProduct Is Nothing Then _requestParamProduct = New RequestParamProduct()

        _requestParamProduct.Type = INDGleType.EditValue
        _requestParamProduct.ProductId = INDSleProduct.EditValue
        _requestParamProduct.SupplieId = INDSleSupplie.EditValue
        _requestParamProduct.Quantity = INDSpnQuantity.EditValue

        If _requestParamProduct.Type = 1 Then
            _requestParamProduct.ProductCodeName = INDSleSupplie.Text
        Else
            _requestParamProduct.ProductCodeName = INDSleProduct.Text
        End If

        _requestParam.RequestParamProduct.Add(_requestParamProduct)

        INDGcProduct.DataSource = _requestParam.RequestParamProduct.ToList()
        INDGcProduct.RefreshDataSource()
        CleanControlsPopup()
        INDGleType.Focus()
    End Sub

    Private Function ValidateControlsPopup() As Boolean
        If INDGleType.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un tipo"
            Return False
        End If

        If INDGleType.EditValue = 1 AndAlso INDSleSupplie.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un insumo"
            Return False
        End If

        If INDGleType.EditValue = 2 AndAlso INDSleProduct.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione un producto"
            Return False
        End If

        If INDSpnQuantity.EditValue <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione una cantidad mayor a cero"
            Return False
        End If

        If INDGleType.EditValue = 1 Then
            If _requestParam.RequestParamProduct.Any(Function(m) (_requestParamProduct Is Nothing OrElse Not m.Equals(_requestParamProduct)) _
                                                     AndAlso m.Type = 1 AndAlso m.SupplieId = Integer.Parse(INDSleSupplie.EditValue)) Then
                Mensaje(EeventViewerImages.Advertencia) = "El insumo seleccionado ya se encuentra en el listado"
                Return False
            End If
        Else
            If _requestParam.RequestParamProduct.Any(Function(m) (_requestParamProduct Is Nothing OrElse Not m.Equals(_requestParamProduct)) _
                                                     AndAlso m.Type = 2 AndAlso m.ProductId = Integer.Parse(INDSleProduct.EditValue)) Then
                Mensaje(EeventViewerImages.Advertencia) = "El producto seleccionado ya se encuentra en el listado"
                Return False
            End If
        End If

        Return True
    End Function

    ''' <summary>
    ''' Closed to clean controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceAdd_Closed(sender As Object, e As DevExpress.XtraEditors.Controls.ClosedEventArgs) Handles INDPceAdd.Closed
        CleanControlsPopup()
    End Sub

    ''' <summary>
    ''' Clean controls popup
    ''' </summary>
    Private Sub CleanControlsPopup()
        _requestParamProduct = Nothing
        INDGleType.EditValue = Nothing
        INDSleProduct.EditValue = Nothing
        INDSleSupplie.EditValue = Nothing
        INDSleProduct.Properties.NullText = String.Empty
        INDSleSupplie.Properties.NullText = String.Empty
        INDSleProduct.Enabled = True
        INDSleSupplie.Enabled = True
        INDGleType.Enabled = True
        INDSpnQuantity.EditValue = 0
        INDSbAdd.Text = "Agregar"
    End Sub

    ''' <summary>
    ''' Query popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDPceAdd_QueryPopUp(sender As Object, e As EventArgs) Handles INDPceAdd.Popup
        INDGleType.Focus()
    End Sub

    ''' <summary>
    ''' Evento al cambiar el valor del control frecuencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleFrequense_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleFrequense.EditValueChanged
        If Frecuency = 0 Then 'Si la frecuencia es todos los días entonces no aplico los controles
            daysProgrammed = False
            INDSleUnitTime.Enabled = False
            UnitTime = Nothing
            ShowHideLayaouts({INDLciDaysOfWeek, INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence _
                         , INDLciTue, INDLciTueSequence, INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence _
                         , INDLciFri, INDLciFriSequence, INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence _
                         , INDLciMonth, INDLciInitialDate, INDLciUnitTime}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        Else
            ShowHideLayaouts({INDLciUnitTime}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            addUnitTime()
            INDSleUnitTime.Enabled = True
        End If
        WriteMemoEdit()
    End Sub

    ''' <summary>
    ''' Evento changed de dia de la semana
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCbeDaysOfWeek_EditValueChanged(sender As Object, e As EventArgs) Handles INDCbeDaysOfWeek.EditValueChanged
        PrepareControlsDays()
        WriteMemoEdit()
    End Sub

    ''' <summary>
    ''' Evento changed sobre unidad de tiempo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleUnitTime_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleUnitTime.EditValueChanged
        DaysOfWeek = Nothing
        INDCbeDaysOfWeek.RefreshEditValue()
        Select Case UnitTime
            Case eUnitTime.Day
                ShowHideLayaouts({INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                ShowHideLayaouts({INDLciDaysOfWeek, INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence _
                         , INDLciTue, INDLciTueSequence, INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence _
                         , INDLciFri, INDLciFriSequence, INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence _
                         , INDLciMonth, INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Case eUnitTime.Week
                ShowHideLayaouts({INDLciDaysOfWeek, INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                ShowHideLayaouts({INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence _
                        , INDLciTue, INDLciTueSequence, INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence _
                        , INDLciFri, INDLciFriSequence, INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence _
                        , INDLciMonth}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
            Case eUnitTime.Month
                ShowHideLayaouts({INDLciMonth, INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciMon, INDLciMonSequence _
                         , INDLciTue, INDLciTueSequence, INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence _
                         , INDLciFri, INDLciFriSequence, INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence
                         }.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
                ShowHideLayaouts({INDLciDaysOfWeek, INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            Case eUnitTime.Year
                ShowHideLayaouts({INDLciDaysOfWeek, INDLciMonth, INDLciInitialDate}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                ShowHideLayaouts({INDLcIDayPrograrmmed, INDLciSequenceOfMonth, INDLciTue, INDLciTueSequence _
                             , INDLciWed, INDlciWedSequence, INDLciThu, INDLciThuSequence, INDLciFri, INDLciFriSequence _
                             , INDlciSat, INDlciSatSequence, INDlciSun, INDlciSunSequence}.ToList, DevExpress.XtraLayout.Utils.LayoutVisibility.Never)
        End Select
        PrepareControlsDays()
        WriteMemoEdit()
    End Sub

    ''' <summary>
    ''' Evento changed sobre secuencias escogidas por día
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDCcbDaysSequences_EditValueChanged(sender As Object, e As EventArgs) Handles INDCcbMon.EditValueChanged, INDCcbTue.EditValueChanged, INDCcbWed.EditValueChanged, INDCcbThu.EditValueChanged,
                                                                                                INDCcbFri.EditValueChanged, INDCcbSat.EditValueChanged, INDCcbSun.EditValueChanged
        WriteMemoEdit()
    End Sub

    ''' <summary>
    ''' Evento changed mes
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSleMonth_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleMonth.EditValueChanged
        WriteMemoEdit()
    End Sub




    Private Sub INDRisleUserPrincipal_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDRisleUserPrincipal.QueryPopUp
        Dim editor As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        ' Obtén el GridControl asociado al SearchLookUpEdit
        Dim gridControl As DevExpress.XtraGrid.GridControl = CType(editor.Parent, DevExpress.XtraGrid.GridControl)
        ' Obtén la vista (GridView) del GridControl
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
        editor.Properties.DataSource = GetUsersByWrehouseOrFuncionalUnit(view)
        editor.Properties.View.RefreshData()
    End Sub


    ''' <summary>
    ''' Devuvelve informacion de los usuarios por almance o unidad funcional
    ''' </summary>
    ''' <param name="View"></param>
    ''' <returns></returns>
    Public Function GetUsersByWrehouseOrFuncionalUnit(ByVal View As DevExpress.XtraGrid.Views.Grid.GridView)
        Dim Users = Nothing
        Dim RowHandle As Integer = View.FocusedRowHandle
        If RowHandle >= 0 Then
            Dim CodeType As Integer = CInt(View.GetRowCellValue(RowHandle, "CodeType"))
            Dim CodeWarehouseOrFuntionalUnit As String = CStr(View.GetRowCellValue(RowHandle, "CodeName"))
            Dim Parts() As String = CodeWarehouseOrFuntionalUnit.Split("-")
            Dim Code = Parts(0).Trim()
            If CodeType = 2 Then
                Users = GetAllUserByWarehouse(Code)
            ElseIf CodeType = 1 Then
                Users = GetAllUserByFuncionalUnit(Code)
            End If
        End If
        Return Users
    End Function

    Private Sub INDRisleUserAlternative_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDRisleUserAlternative.QueryPopUp
        Dim editor As DevExpress.XtraEditors.SearchLookUpEdit = CType(sender, DevExpress.XtraEditors.SearchLookUpEdit)
        Dim gridControl As DevExpress.XtraGrid.GridControl = CType(editor.Parent, DevExpress.XtraGrid.GridControl)
        Dim view As DevExpress.XtraGrid.Views.Grid.GridView = CType(gridControl.MainView, DevExpress.XtraGrid.Views.Grid.GridView)
        editor.Properties.DataSource = GetUsersByWrehouseOrFuncionalUnit(view)
        editor.Properties.View.RefreshData()
    End Sub

    Private Sub INDGleRequiredAuthorization_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleRequiredAuthorization.EditValueChanged
        If Not RequiredAuthorization Then
            INDlcgAuthorizers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Else
            GetAllUsers()
            INDlcgAuthorizers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        End If
    End Sub




#End Region

End Class

''' <summary>
''' Simplificacion de clase de la vista
''' Se usa para gestionar los valores directamte
''' </summary>
Public Class ViewRequestParamAuthUserDto
    Public Property Id As Integer
    Public Property IdRequestParamAuthUser As Integer
    Public Property IdRequestParam As Integer
    Public Property IdType As Integer
    Public Property CodeRequestParam As String
    Public Property Code As String
    Public Property CodeName As String
    Public Property Type As String
    Public Property CodeType As Integer
    Public Property UserPrincipalId As Integer?
    Public Property UserAlternativeId As Integer?
End Class
