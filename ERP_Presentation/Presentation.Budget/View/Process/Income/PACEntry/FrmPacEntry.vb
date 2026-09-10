'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez Gutierrez 
' Created          : 19/08/2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.Windows.Forms
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmPacEntry
    Implements IAnnualizedCashFlow

#Region "Build"

    Public ctrTmp As CtrInfoEntity

    Public Sub New()
        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoEntity()
        ' This call is required by the designer.
        InitializeComponent()
        ctrTmp.SetTotalValues(AddressOf getValues)
        ctrTmp.RefreshInfo()
        ctrTmp.PopupContainerControlEntity = INDpccChangeEntity
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getValues() As Tuple(Of Integer, String, Integer, String, String)
        Return New Tuple(Of Integer, String, Integer, String, String)(BudgetEntitiesId, _EntityDescription, BudgetaryValidityId, _Year, statusValidity)
    End Function

#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

    ''' <summary>
    ''' Variable para conocer si el formulario abre por modo busqueda
    ''' </summary>
    Dim SearchMode As Boolean

    ''' <summary>
    ''' Presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PAnnualizedCashFlow

    ''' <summary>
    ''' Mes Ingresos
    ''' </summary>
    ''' <remarks></remarks>
    Dim _IncomeMonth As Integer

    ''' <summary>
    ''' Año vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _Year As String

    ''' <summary>
    ''' Estado de a vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim statusValidity As String

    ''' <summary>
    ''' descripcion de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Dim _EntityDescription As String

    ''' <summary>
    ''' Contiene la lista de entidad de pac
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListAnnualizedCashFlow As List(Of AnnualizedCashFlow)

    ''' <summary>
    ''' Variable que controla el registro bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequense As Domain.Entities.BudgetSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Variable de sesiones
    ''' </summary>
    ''' <remarks></remarks>
    Dim _indigoSession As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable para saber si guarda, actualiza o confirma (1 = guarda, 2 = Actualiza, 3 = confirmar, 4 = Anular)
    ''' </summary>
    ''' <remarks></remarks>
    Dim state As Byte

    ''' <summary>
    ''' Listado de rubros por agregar al presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim listBudgetNew As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Variable para saber si la vigencia Maneja Control PAC (True = si Maneja, False = No Maneja)
    ''' </summary>
    ''' <remarks></remarks>
    Dim PACControlFlag As Boolean

    ''' <summary>
    ''' Variable para asignar el estado al listado de pac
    ''' </summary>
    ''' <remarks></remarks>
    Dim status As Byte

    ''' <summary>
    ''' Variable que define el tipo de evento para la impresión del reporte
    ''' </summary>
    ''' <remarks></remarks>
    Dim varImp As Integer

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer Implements IAnnualizedCashFlow.BudgetaryValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection Implements IAnnualizedCashFlow.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAnnualizedCashFlow.BudgetEntitiesXpo
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer Implements IAnnualizedCashFlow.BudgetEntitiesId
        Get
            Return INDsleEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el listado de las vigencias del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityPopUpXpo As DevExpress.Xpo.XPCollection Implements IAnnualizedCashFlow.BudgetaryValidityPopUpXpo
        Get
            Return INDsleValidityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidityPopUp.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el datasource para entidades presupuestales del popup
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesPopUpXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IAnnualizedCashFlow.BudgetEntitiesPopUpXpo
        Get
            Return INDsleEntityPopUp.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleEntityPopUp.Properties.DataSource = value
        End Set
    End Property

    Dim _documentType As EItemType
    ''' <summary>
    ''' Tipo de documento si es ingreso o gasto
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ItemType As EItemType
        Get
            Return _documentType
        End Get
        Set(value As EItemType)
            _documentType = value
        End Set
    End Property

    ''' <summary>
    ''' Acciones sobre los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IAnnualizedCashFlow.ActionsOnControls
        Set(value As Boolean)
            INDlcPacEntry.BeginUpdate()
            INDsleEntity.Enabled = Not value
            INDbtnCodeCategory.Enabled = Not value
            INDtxtFinancialSource.Enabled = value
            INDtxtBudgetedValue.Enabled = value
            INDgcDetail.Enabled = value
            INDlcPacEntry.EndUpdate()
            If value = True Then
                INDtxtFinancialSource.Focus()
            Else
                If Object.Equals(INDsleEntity.EditValue, Nothing) = True Then
                    INDsleValidity.Focus()
                Else
                    INDsleEntity.Focus()
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' devuelve los layout controls
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As Controls.IndigoLayoutControl Implements IAnnualizedCashFlow.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Devuelve el tag del frontal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IAnnualizedCashFlow.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
#End Region

#Region "ICrud"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements IcrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Deshacer(Optional withBudgetEntity As Boolean = True)
        CleanControls(withBudgetEntity)
        If SearchMode = False Then
            'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        Else
            If indigo.UserViewMode = True Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
            End If
        End If
        INDsleEntity.Focus()
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    '''  METODO: Item Guardar del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If validateValue() = False Then
            Exit Sub
        End If
        Try
            Using model As New MAnnualizedCashFlow(MyTag)
                AsyncLoader(True)
                Dim result = Await model.SaveListAnnualizedCashFlow(ListAnnualizedCashFlow, status, 1)
                If result.StateResult = True Then
                    Select Case state
                        Case 1 'Guardar
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        Case 2 'Actualizar
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        Case 3 'Confirmar
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                        Case 4 ' Anular
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("AnnularCorrect")
                    End Select
                    'Me.RevenueType = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)

                    Select Case varImp
                        Case 1
                            Me.BarraBotones.PrintReport(PrintReportAction.Create, 0, 0, ListAnnualizedCashFlow)
                        Case 2
                            Me.BarraBotones.PrintReport(PrintReportAction.Update, 0, 0, ListAnnualizedCashFlow)
                        Case 3
                            Me.BarraBotones.PrintReport(PrintReportAction.Confirm, 0, 0, ListAnnualizedCashFlow)
                        Case 4
                            Me.BarraBotones.PrintReport(PrintReportAction.Cancel, 0, 0, ListAnnualizedCashFlow)
                    End Select

                    AsyncLoader(False)
                    Me.Deshacer(False)
                Else
                    AsyncLoader(False)
                    INDbtnCodeCategory.Enabled = False
                    If result.MessageResult(0) = ErrorConcurrencia Then
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorConcurrence")
                    Else
                        Mensaje(EeventViewerImages.MensajeError) = ResourceManager.GetString("ErrorUnknown")
                    End If
                End If
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCodeCategory.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    ''' <remarks></remarks>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
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
    ''' METODO: Item Nuevo del control de usuarios.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Nuevo() Implements IcrudBase.Nuevo
        'NewPacEntry()
    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesNoTienePermisos, Comunes)
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {New ColumnInfo With {.Caption = "Código", .FieldName = "Code"},
                              New ColumnInfo With {.Caption = "Nombre", .FieldName = "Name"},
                              New ColumnInfo With {.Caption = "Recurso", .FieldName = "FinancialSource"},
                              New ColumnInfo With {.Caption = "Valor Presupuestado", .FieldName = "BudgetValue"},
                              New ColumnInfo With {.Caption = "Valor Programado", .FieldName = "PACValue"},
                              New ColumnInfo With {.Caption = "Estado", .FieldName = "StatusText"}}.ToList()
            .ValorSolicitado = "Code"
            .SearchParameters = {BudgetaryValidityId, 1}
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListViewListAnnualizedCashFlow
            'BarraBotones.PrepareToolbar(eAction.OnlyNew)
            .FormParent = Me
            .ShowSearch()
        End With
        SearchMode = True
    End Sub

    ''' <summary>
    ''' Metodo para obtener el valor del formulario de busqueda
    ''' </summary>
    ''' <param name="ReturnValue"></param>
    ''' <param name="ReturnObject"></param>
    ''' <remarks></remarks>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        INDbtnCodeCategory.Text = ReturnValue
        If INDbtnCodeCategory.Text <> String.Empty Then
            Await LoadControls()
            If INDbtnCodeCategory.Enabled = False Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCodeCategory.Enabled = False
        End If
    End Sub

    Public Sub Deshacer1() Implements IcrudBase.Deshacer

    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Muestra las columnas de los detalles 
    ''' </summary>
    ''' <param name="value"></param>
    Public Sub ShowColumsGrid(ByVal value As Boolean)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvDetail.Columns
            Select Case col.Name
                Case "INDgcolDebitMod"
                    col.Visible = value
                    If value Then
                        col.VisibleIndex = 3
                    End If
                Case "INDgcolCreditMod"
                    col.Visible = value
                    If value Then
                        col.VisibleIndex = 4
                    End If
                Case "INDgcolDebitTrans"
                    col.Visible = value
                    If value Then
                        col.VisibleIndex = 5
                    End If
                Case "INDgcolCreditTrans"
                    col.Visible = value
                    If value Then
                        col.VisibleIndex = 6
                    End If
                Case "INDgcolValueExecuted"
                    col.Visible = value
                    If value Then
                        col.VisibleIndex = 7
                    End If
                Case "INDgcolBalance"
                    col.Visible = value
                    If value Then
                        col.VisibleIndex = 8
                    End If
            End Select
        Next
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
                _Year = item.Year
                statusValidity = item.StatusText
                _IncomeMonth = item.IncomeMonth
                PACControlFlag = item.PACControl
            End If
        End If
    End Sub

    ''' <summary>
    ''' metodo que oculta o muestra el grupo de vigencia
    ''' </summary>
    ''' <param name="Bandera"></param>
    ''' <remarks></remarks>
    Private Sub CleanGroups(ByVal Bandera As Boolean)
        If Bandera Then
            INDlcgAnnualizedCashFlow.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            If BudgetEntitiesPopUpXpo Is Nothing Then
                presenter.InitializeBudgetEntityPopUp()
            End If
            INDsleEntityPopUp.EditValue = BudgetEntitiesId
            INDsleValidityPopUp.EditValue = BudgetaryValidityId
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = False
            INDbtnCodeCategory.Focus()
        Else
            INDlcgAnnualizedCashFlow.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlcgMainData.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlcgDetail.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            Me.BarraBotones.StatusRecordVisible = False
            Me.BarraBotones.ControlHideStatus = True
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = CByte(1), .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(2), .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = CByte(3), .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        Me.BarraBotones.States = listStates
    End Sub

    ''' <summary>
    ''' metodo para limpiar controles
    ''' </summary>
    ''' <param name="withBudgetaryEntity">saber si se limpia tabn la entidad presupuestal o no</param>
    ''' <remarks></remarks>
    Private Sub CleanControls(Optional withBudgetaryEntity As Boolean = True)
        INDlcPacEntry.BeginUpdate()
        ActionsOnControls = False
        INDbtnCodeCategory.Text = String.Empty
        INDtxtFinancialSource.EditValue = String.Empty
        INDtxtBudgetedValue.Text = String.Empty
        Me._doc = Nothing
        INDgcDetail.DataSource = Nothing
        ListAnnualizedCashFlow = Nothing
        Me.BarraBotones.ControlHideStatus = False
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        If withBudgetaryEntity = True Then
            BudgetaryValidityId = Nothing
            INDsleValidityPopUp.EditValue = Nothing
            _Year = Nothing
            statusValidity = Nothing
            CleanGroups(False)
        End If
        INDlcPacEntry.EndUpdate()
    End Sub

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Me.BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Function
        End If

        Using Model As New MAnnualizedCashFlow(MyTag)
            AsyncLoader(True)
            INDlcPacEntry.BeginUpdate()
            Dim result = Await Model.GetAnnualizedCashFlowByValidityIdAndByCodeCategory(BudgetaryValidityId, Me.INDbtnCodeCategory.Text.Trim, 1)
            If result.StateResult = True Then
                ListAnnualizedCashFlow = result.ObjectEmbbeded
                Me.BarraBotones.StatusRecordVisible = True
                INDbtnCodeCategory.EditValue = Me.INDbtnCodeCategory.Text
                INDtxtFinancialSource.EditValue = result.MessageResult(0)
                INDtxtBudgetedValue.Text = result.MessageResult(1)
                If ListAnnualizedCashFlow(0).Id = 0 Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
                    Me.ReadOnlyControls(False)
                Else
                    If ListAnnualizedCashFlow(0).Status = 1 Then
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                        Me.ReadOnlyControls(False)
                        ShowColumsGrid(False)
                    Else
                        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                        Me.ReadOnlyControls(True)
                        ShowColumsGrid(True)
                    End If
                End If
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Me.BarraBotones.PrintReport(PrintReportAction.None, 0, 0, ListAnnualizedCashFlow)
                AsyncLoader(False)
                ActionsOnControls = True
                INDgcDetail.DataSource = ListAnnualizedCashFlow
                INDgcDetail.RefreshDataSource()
            Else
                AsyncLoader(False)
                Me.Mensaje(EeventViewerImages.Advertencia) = result.Message
                INDbtnCodeCategory.Text = String.Empty
                Deshacer(False)
                INDbtnCodeCategory.Focus()
            End If
        End Using
        INDlcPacEntry.EndUpdate()
    End Function

    ''' <summary>
    ''' Metodo para preparar la barra de usuario cuando el registro es nuevo
    ''' </summary>
    ''' <param name="code">The code.</param>
    Private Sub PrepareToolbar(code As String)
        'INDbtnCode.Text = code
        Me.ActionsOnControls = True
        Me.BarraBotones.StatusRecordVisible = True
        'Me.BarraBotones.StatusRecord = 1
        Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function validateValue() As Boolean
        Dim validate = True
        If ValidateControls() = False Then
            validate = False
        End If
        If ListAnnualizedCashFlow IsNot Nothing AndAlso ListAnnualizedCashFlow.Count > 0 Then
            Dim TotalPAC = ListAnnualizedCashFlow.Sum(Function(x) x.InitialValue)
            If INDtxtBudgetedValue.EditValue <> TotalPAC Then
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("ValidateValueBudgetAndValuePAC", NAME_MODULE)
                validate = False
            End If
        End If
        Return validate
    End Function

#End Region

#Region "Handles"

#Region "Load"
    ''' <summary>
    ''' Libera la memoria del frm al cerrar lo 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        SearchMode = Nothing
        presenter = Nothing
        _IncomeMonth = Nothing
        _Year = Nothing
        statusValidity = Nothing
        _EntityDescription = Nothing
        ListAnnualizedCashFlow = Nothing
        blockRecord = Nothing
        _sequense = Nothing
        _idOperativeUnit = Nothing
        state = Nothing
        listBudgetNew = Nothing
        PACControlFlag = Nothing
        status = Nothing
        varImp = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara cuando incia el form, carga los controles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPacEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlcPacEntry, True)
        Me._doc = Nothing
        _indigoSession = SessionValues.Instance
        presenter = New PAnnualizedCashFlow(Me)
        presenter.LoadDefinitionLayout()
        _idOperativeUnit = BarraBotones.OperatingUnitValue
        CleanGroups(False)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        LoadStatus()
        Deshacer(False)
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            _EntityDescription = INDsleEntity.Text
            BudgetaryValidityId = Nothing
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el campo de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If BudgetaryValidityId <> Nothing AndAlso BudgetaryValidityId <> 0 Then
            If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
                Dim item = (From l In BudgetaryValidityXpo Where l.Id = BudgetaryValidityId Select l).FirstOrDefault
                If item IsNot Nothing Then
                    _Year = item.Year
                    statusValidity = item.StatusText
                    _IncomeMonth = item.IncomeMonth
                    PACControlFlag = item.PACControl
                    If PACControlFlag Then
                        ctrTmp.RefreshInfo()
                        Me.BarraBotones.StatusRecordVisible = True
                        Me.BarraBotones.ControlHideStatus = False
                        CleanGroups(True)
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidityNotPACControl", NAME_MODULE))
                    End If
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el popup de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidityPopUp.EditValueChanged
        If INDsleValidityPopUp.EditValue IsNot Nothing Then
            If BudgetaryValidityPopUpXpo IsNot Nothing AndAlso BudgetaryValidityPopUpXpo.Count > 0 Then
                Dim item = (From l In BudgetaryValidityPopUpXpo Where l.Id = INDsleValidityPopUp.EditValue Select l).FirstOrDefault
                If item IsNot Nothing Then
                    If item.PACControl Then
                        BudgetEntitiesId = INDsleEntityPopUp.EditValue
                        BudgetaryValidityId = INDsleValidityPopUp.EditValue
                        If INDsleEntityPopUp.Text = String.Empty Then
                            _EntityDescription = INDsleEntity.Text
                        Else
                            _EntityDescription = INDsleEntityPopUp.Text
                        End If
                        _Year = item.Year
                        statusValidity = item.StatusText
                        _IncomeMonth = item.IncomeMonth
                        PACControlFlag = item.PACControl
                        ctrTmp.RefreshInfo()
                    Else
                        INDsleEntityPopUp.EditValue = BudgetEntitiesId
                        INDsleValidityPopUp.EditValue = BudgetaryValidityId
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidityNotPACControl", NAME_MODULE))
                    End If
                End If
            End If
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el pop up de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntityPopUp_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntityPopUp.EditValueChanged
        If INDsleEntityPopUp.EditValue IsNot Nothing Then
            'BudgetEntitiesId = INDsleEntityPopUp.EditValue
            'If INDsleEntityPopUp.Text <> String.Empty Then
            '    _EntityDescription = INDsleEntityPopUp.Text
            'End If
            INDsleValidityPopUp.EditValue = Nothing
            BudgetaryValidityPopUpXpo = Nothing
            presenter.InitializeValidityPopUp(INDsleEntityPopUp.EditValue)
            ctrTmp.RefreshInfo()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abrir busqueda con el boton en la caja de texto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnCodeCategory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDbtnCodeCategory.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' evento buttonclick que abre el formulario de registro de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            presenter.InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Abrir busqueda con el boton en la caja de texto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntityPopUp_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntityPopUp.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", Nothing, True)
            presenter.InitializeBudgetEntityPopUp()
        End If
    End Sub

#End Region

#Region "CustomDrawCell"
    ''' <summary>
    ''' customiza la regilla de detalles
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDgvDetail_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvDetail.CustomDrawCell
        If DirectCast(sender, DevExpress.XtraGrid.Views.Grid.GridView).Equals(Me.INDgvDetail) Then
            Dim obj = Nothing
            obj = CType(Me.INDgvDetail.GetRow(e.RowHandle), Domain.Entities.AnnualizedCashFlow)
            If obj IsNot Nothing Then
                If e.Column.Equals(Me.INDgcolMonth) Then
                    Select Case obj.Month
                        Case 1
                            e.DisplayText = "Enero"
                        Case 2
                            e.DisplayText = "Febrero"
                        Case 3
                            e.DisplayText = "Marzo"
                        Case 4
                            e.DisplayText = "Abril"
                        Case 5
                            e.DisplayText = "Mayo"
                        Case 6
                            e.DisplayText = "Junio"
                        Case 7
                            e.DisplayText = "Julio"
                        Case 8
                            e.DisplayText = "Agosto"
                        Case 9
                            e.DisplayText = "Septiembre"
                        Case 10
                            e.DisplayText = "Octubre"
                        Case 11
                            e.DisplayText = "Noviembre"
                        Case 12
                            e.DisplayText = "Diciembre"
                    End Select
                End If
            End If
        End If

    End Sub

#End Region

#Region "Keydown"

    ''' <summary>
    ''' Captura la tecla enter, para buscar un regitro por codigo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCodeCategory_KeyDown(sender As Object, e As KeyEventArgs) Handles INDbtnCodeCategory.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If INDsleEntity.EditValue IsNot Nothing AndAlso CStr(INDsleEntity.EditValue) <> "" Then
                Await Me.LoadControls()
            Else
                Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SeleccioneEntidadPresupuestal", NAME_MODULE)
            End If
        End If
    End Sub

#End Region

#End Region

#Region "BarraBotones"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
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
        SearchMode = False
        Deshacer(False)
        BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.DeshacerTodo) = False
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
        state = 1
        status = 1
        varImp = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        state = 2
        status = 1
        varImp = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Click Boton de activar o inactivar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickAnular() Handles BarraBotones.ClickAnular
        If MessageIndigo.Show(ResourceManager.GetString("AnnularMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 4
            status = 3
            varImp = 4
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense IsNot Nothing AndAlso Me._sequense.Scope.Equals("OU") AndAlso Me._sequense.BudgetSequenceDetail IsNot Nothing Then
            If Me._sequense.BudgetSequenceDetail.Any(Function(S) S.IdOperatingUnit = operatingUnit.Id) Then
                Me._idOperativeUnit = Me._sequense.BudgetSequenceDetail.Where(Function(s) s.IdOperatingUnit = operatingUnit.Id).SingleOrDefault().Id
                Me.BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            Else
                Me.Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("OperatingUnitUnassigned")
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
        End If
    End Sub

    Private Sub BarraBotones_Click_DeshacerTodo() Handles BarraBotones.Click_DeshacerTodo
        SearchMode = False
        Deshacer(True)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

    ''' <summary>
    ''' Barras the botones_ActualizarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 3
            status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Barras the botones_GuardarConfirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 3
            status = 2
            varImp = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, 0, 0, ListAnnualizedCashFlow)
    End Sub
    ''' <summary>
    ''' si el campo de entidades esta habilitado se enfoca 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDsleEntity.Enabled Then
            INDsleEntity.Focus()
        End If
    End Sub

#End Region

End Class