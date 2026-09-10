'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 30/07/2015
'
' Modified         : 
' DateModified     : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmPrincipalEarningsType
    Implements IPrincipalEarningsType

#Region "Builder"

    Public ctrTmp As CtrInfo

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfo()
        ctrTmp.SetTotalValues(AddressOf getInfo)
        ctrTmp.RefreshInfo()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub
    ''' <summary>
    ''' Funcion que obtiene la informacion por medio de duplas
    ''' </summary>
    ''' <returns></returns>
    Private Function getInfo() As Tuple(Of String, String, String)
        Return New Tuple(Of String, String, String)(INDsleBudgetEntity.Text, INDsleValidity.Text, Status)
    End Function

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el estado de la vigencia para visualizarlo
    ''' en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private _Status As String
    Public Property Status As String
        Get
            Return _Status
        End Get
        Set(value As String)
            _Status = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityId As Integer? Implements IPrincipalEarningsType.BudgetEntityId
        Get
            Return INDsleBudgetEntity.EditValue
        End Get
        Set(value As Integer?)
            INDsleBudgetEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IPrincipalEarningsType.BudgetEntityXpo
        Get
            Return INDsleBudgetEntity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityId As Integer? Implements IPrincipalEarningsType.ValidityId
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer?)
            INDsleValidity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la vigencia
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property ValidityXpo As DevExpress.Xpo.XPCollection Implements IPrincipalEarningsType.ValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "Consts"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PPrincipalEarningsType

    ''' <summary>
    ''' Listado de tipo xpCollection para el datasource de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRevenueTypeXpCollection As DevExpress.Xpo.XPCollection

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' Metodos de CRUD que implementa la interfaz IcrudBase
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer

    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub
    ''' <summary>
    ''' propiedad para mostrar los mensajes en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 Then
            Dim item = (From l In ValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                ValidityId = item.Id
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que coloca el estado en el control de información
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetStatus()
        If ValidityXpo IsNot Nothing AndAlso ValidityXpo.Count > 0 AndAlso ValidityId IsNot Nothing Then
            Dim item = (From l In ValidityXpo Where l.Id = ValidityId Select l).FirstOrDefault
            If item IsNot Nothing Then
                Select Case item.Status
                    Case 1
                        Status = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        Status = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        Status = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        Status = String.Empty
                End Select
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que edita el registro de la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditEarningType()
        Dim itemXpo As BudgetRevenueTypeXpo = CType(viewEarningType.GetFocusedRow, BudgetRevenueTypeXpo)
        If itemXpo IsNot Nothing Then
            OpenShowDialog(itemXpo.Code)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que abre el formulario de tipo de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenShowDialog(CodeEarningType As String)
        If Status = obtenerRecurso(Cerrada, Eform.BudgetEntities) Then
            Mensaje(EeventViewerImages.Advertencia) = "No puede manipular un tipo de ingreso cuando la vigencia está Cerrada."
            Exit Sub
        End If
        Using Formulario As New FrmEarningsType()
            AddHandler Formulario.UpdateDatasource, AddressOf SetDataSourceGrid
            Formulario.BudgetaryValidityId = ValidityId
            Formulario.CodeEarningTypeFormPrincipal = CodeEarningType
            Formulario.BudgetaryEntityCodeName = INDsleBudgetEntity.Text
            Formulario.ValidityYear = INDsleValidity.Text
            Formulario.StatusValidity = Status
            Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Formulario.Width = 780
            Formulario.Height = 768
            Dim frm As New FrmTransparent(Formulario, False)
            frm.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        ActionOnControls = False
        BudgetEntityId = Nothing
        ValidityId = Nothing
        ValidityXpo = Nothing
        ListRevenueTypeXpCollection = Nothing
        INDgcEarningType.DataSource = Nothing
        BarraBotones.StatusRecordVisible = False
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
        BarraBotones.StatusRecordVisible = False
        Status = String.Empty
        ctrTmp.RefreshInfo()
        INDsleBudgetEntity.Focus()
    End Sub

    ''' <summary>
    ''' Accion que se realizaran en los controles
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionOnControls As Boolean
        Set(value As Boolean)
            INDlyPrincipalEarningsType.BeginUpdate()
            INDbtnAddEarningType.Enabled = value
            INDgcEarningType.Enabled = value
            INDlyPrincipalEarningsType.EndUpdate()
            If value Then
                INDbtnAddEarningType.Focus()
            Else
                INDsleBudgetEntity.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource de la rejilla del listado
    ''' de tipos de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSourceGrid()
        ListRevenueTypeXpCollection = presenter.ListRevenueTypeByBudgetValidityIdAndType(ValidityId)
        INDgcEarningType.DataSource = Nothing
        INDgcEarningType.DataSource = ListRevenueTypeXpCollection
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ctrTmp = Nothing
        presenter = Nothing
        ListRevenueTypeXpCollection = Nothing
    End Sub


    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPrincipalEarningsType_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PPrincipalEarningsType(Me)
        IndigoGridControl1.RefreshGrid(INDgcEarningType)
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(viewEarningType, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewEarningType.Columns
            If col.Name = "colActions" Then
                col.Width = 80
            End If
        Next
        CleanControls()
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmPrincipalEarningsType_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntityId IsNot Nothing Then
            BarraBotones.StatusRecordVisible = True
            ValidityId = Nothing
            ValidityXpo = Nothing
            ListRevenueTypeXpCollection = Nothing
            INDgcEarningType.DataSource = Nothing
            ActionOnControls = False
            presenter.InitializeValidity(BudgetEntityId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If ValidityId IsNot Nothing AndAlso BudgetEntityId IsNot Nothing Then
            SetStatus()
            ActionOnControls = True
            SetDataSourceGrid()
            ctrTmp.RefreshInfo()
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
            Me.BarraBotones.PrintReport(PrintReportAction.None, ValidityId.Value, 0, ValidityId.Value)
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleBudgetEntity.QueryPopUp
        If BudgetEntityXpo Is Nothing Then
            presenter.InitializeBudgetEntity()
        End If
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas del control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvValidity_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvValidity.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDColVStatus.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = obtenerRecurso(Registrada, Eform.BudgetEntities)
                Case 2
                    e.DisplayText = obtenerRecurso(Activa, Eform.BudgetEntities)
                Case 3
                    e.DisplayText = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                Case Else

            End Select
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas de la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewEarningType_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles viewEarningType.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolIncomeSource.Name Then
            e.DisplayText = ResourceManager.GetString("IncomeSource" + e.Value.ToString, NAME_MODULE)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddEarningType_Click(sender As Object, e As EventArgs) Handles INDbtnAddEarningType.Click
        OpenShowDialog(String.Empty)
    End Sub

#End Region

#Region "ContextMenu"
    ''' <summary>
    ''' evento al dar click en la cuadricula 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        EditEarningType()
    End Sub
    ''' <summary>
    ''' evento al seleccionar una opcion del menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        EditEarningType()
    End Sub

#End Region

#End Region

#Region "Barra Botones"

    ''' <summary>
    ''' Click deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Click Boton Imprimir
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, ValidityId.Value, 0, ValidityId.Value)
    End Sub

    ''' <summary>
    ''' Load Barra Botones
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

#End Region

End Class