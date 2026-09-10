'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 31-05-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraTreeList
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Controls

#End Region

Public Class FrmInitialBudgetEntryExpense
    Implements IInitialBudgetEntry

#Region "Builder"

    Public ctrTmp As CtrInfoBudgetEntry

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ctrTmp = New CtrInfoBudgetEntry()
        ctrTmp.SetTotalValues(AddressOf getInfo)
        ctrTmp.RefreshInfo()
        ctrTmp.RefreshProgressBar()
        ctrTmp.Dock = DockStyle.Fill
        AdditionalControlPanel.Controls.Add(ctrTmp)
    End Sub

    Private Function getInfo() As Tuple(Of String, String, String, Decimal, Decimal)
        Return New Tuple(Of String, String, String, Decimal, Decimal)(INDsleBudgetEntity.Text, INDsleValidity.Text, StatusValidityName, valueValidity, valueDigitate)
    End Function

#End Region

#Region "Globals"

    ''' <summary>
    ''' Representa el presentador de presupuesto inicial 
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PInitialBudgetEntry

    ''' <summary>
    ''' Representa al listado de tipo de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListRevenueType As List(Of RevenueType)

    ''' <summary>
    ''' Establece el mensaje dependiendo de la accion
    ''' 1=Guardar, 2=Actualizar, 3=Confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Dim state As Integer

    ''' <summary>
    ''' Representa a la entidad cabecera del presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim budgetHeader As BudgetHeader

    ''' <summary>
    ''' Estado de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim StatusValidityName As String

    ''' <summary>
    ''' Variable que controla el registero bloqueado
    ''' </summary>
    ''' <remarks></remarks>
    Dim blockRecord As BlockRecordBudget

    ''' <summary>
    ''' Valor de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim valueValidity As Decimal

    ''' <summary>
    ''' Sumatoria de los valores digitados en los tipos de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim valueDigitate As Decimal

#End Region

#Region "Properties"

    ''' <summary>
    ''' Listado de categorias
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ListCategory As List(Of Category)
        Get
            Return INDtlCategory.DataSource
        End Get
        Set(value As List(Of Category))
            INDtlCategory.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' establece el estado de los controles
    ''' </summary>
    ''' <value>
    ''' <c>true</c> if [actions on controls]; otherwise, 
    ''' <c>false</c>.
    ''' </value>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IInitialBudgetEntry.ActionsOnControls
        Set(value As Boolean)
            INDlyInitialBudget.BeginUpdate()
            INDtlCategory.Enabled = value
            INDlyInitialBudget.EndUpdate()
            If value Then
                INDtlCategory.Focus()
            Else
                INDsleBudgetEntity.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets my layout control.
    ''' </summary>
    ''' <value>
    ''' My layout control.
    ''' </value>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IInitialBudgetEntry.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IInitialBudgetEntry.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad presupuestal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntityId As Integer? Implements IInitialBudgetEntry.BudgetEntityId
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
    Public Property BudgetEntityXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements IInitialBudgetEntry.BudgetEntityXpo
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
    Public Property ValidityId As Integer? Implements IInitialBudgetEntry.ValidityId
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
    Public Property ValidityXpo As DevExpress.Xpo.XPCollection Implements IInitialBudgetEntry.ValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud Base"

    ''' <summary>
    ''' METODO: Item buscar del control de usuarios.
    ''' </summary>
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    ''' <summary>
    ''' METODO: Item Deshacer del control de usuarios.
    ''' </summary>
    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
        BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' METODO: Item Eliminar del control de usuarios.
    ''' </summary>
    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    ''' <summary>
    ''' METODO: Item Guardar del control de usuarios.
    ''' </summary>
    Public Async Sub Guardar() Implements IcrudBase.Guardar
        If ValidateControls() = False Then
            Exit Sub
        End If
        If ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 Then
            Dim cont = ListCategory.FindAll(Function(item) item.Value > 0).Count
            If cont = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "Debe diligenciar al menos un tipo de ingreso de algún rubro."
                Exit Sub
            End If
        End If
        If valueValidity <> valueDigitate AndAlso state = 3 Then
            Mensaje(EeventViewerImages.Advertencia) = "No puede confirmar porque el valor total de los rubros no es igual al valor de la vigencia."
            Exit Sub
        End If
        Try
            AssigningValues()
            Using model As New MInitialBudgetEntry(Me.Tag)
                AsyncLoader(True)
                Dim Result = Await model.SaveBudgetAsync(budgetHeader, state)
                AsyncLoader(False)
                If Result.StateResult = True Then
                    Select Case state
                        Case 1 'Guardar
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("SaveMessage")
                        Case 2 'Actualizar
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("UpdateMessage")
                        Case 3 'Confirmar
                            Mensaje(EeventViewerImages.Informacion) = ResourceManager.GetString("ConfirmationMessage")
                    End Select
                    'Me.RevenueType = Result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    AsyncLoader(False)
                    Me.Deshacer()
                Else
                    If Result.MessageResult(0) = ErrorConcurrencia Then
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
    End Sub

    ''' <summary>
    ''' Metodo para establecer la logica para los permisos de Guardar y Actualizar True -&gt; Muestra Guardar | False -&gt; Muestra Actualizar
    ''' </summary>
    ''' <param name="existeDatos"></param>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
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
    ''' METODO: Item Nuevo del control de usuarios, sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    ''' <summary>
    ''' este metodo abre el frontal de busqueda, sin usarse
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Handlers"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        ListRevenueType = Nothing
        state = Nothing
        budgetHeader = Nothing
        StatusValidityName = Nothing
        blockRecord = Nothing
        valueValidity = Nothing
        valueDigitate = Nothing
    End Sub

    ''' <summary>
    ''' evento load del formulario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub FrmInitialBudgetEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.LayoutControls.SetIsCustomizable(Me.INDlyInitialBudget, True)
        presenter = New PInitialBudgetEntry(Me)
        IndigoGridControl1.RefreshGrid(INDgcIncomeType)
        IndigoGridControl1.RefreshGrid(INDgcSecondInfo)
        IndigoGridView1.MoreInfoColunmns(viewRevenueType)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In viewRevenueType.Columns
            If col.Name = "colActions" Then
                col.Width = 30
            End If
        Next
        LoadStatus()
        Deshacer()
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

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispar al cambiar el valor del control de entidad presupuestal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleBudgetEntity.EditValueChanged
        If BudgetEntityId IsNot Nothing Then
            ActionsOnControls = False
            INDtlCategory.DataSource = Nothing
            ValidityId = Nothing
            ValidityXpo = Nothing
            BarraBotones.StatusRecordVisible = False
            presenter.InitializeValidity(BudgetEntityId)
            SetFirstOrDefaultValidity()
            ctrTmp.RefreshInfo()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If ValidityId IsNot Nothing AndAlso BudgetEntityId IsNot Nothing Then
            SetStatus()
            ctrTmp.RefreshInfo()
            ActionsOnControls = True

            Dim listErrors As New StringBuilder
            Dim error1 As String = LoadStructure()
            If error1.Length > 0 Then
                listErrors.AppendLine(error1)
            End If
            Dim error2 As String = LoadListRevenueType()
            If error2.Length > 0 Then
                listErrors.AppendLine(error2)
            End If
            If listErrors.Length > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            End If

            Await LoadBudgetHeader()

            If ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 AndAlso ListRevenueType IsNot Nothing AndAlso ListRevenueType.Count > 0 AndAlso budgetHeader.Id = 0 _
                AndAlso StatusValidityName <> obtenerRecurso(Cerrada, Eform.BudgetEntities) Then
                BarraBotones.PrepareToolbar(eAction.OnlySaveConfirm)
            ElseIf ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 AndAlso ListRevenueType IsNot Nothing AndAlso ListRevenueType.Count > 0 AndAlso budgetHeader.Id > 0 AndAlso budgetHeader.Status = 1 _
                AndAlso StatusValidityName <> obtenerRecurso(Cerrada, Eform.BudgetEntities) Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateConfirmIntegratedAnnular)
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
            ElseIf ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 AndAlso ListRevenueType IsNot Nothing AndAlso ListRevenueType.Count > 0 AndAlso budgetHeader.Id > 0 AndAlso budgetHeader.Status = 2 _
                AndAlso StatusValidityName <> obtenerRecurso(Cerrada, Eform.BudgetEntities) Then
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            Else
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
            End If
            BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
            Me.BarraBotones.PrintReport(PrintReportAction.None, ValidityId.Value, 0, ValidityId.Value)

        End If
    End Sub

#End Region

#Region "EditValueChanging"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de la rejilla de tipos de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles INDrepTxtValue.EditValueChanging
        If e.NewValue IsNot String.Empty Then
            'Obtengo el item de la rejilla de tipo de ingreso
            Dim itemRevenueType As RevenueType = viewRevenueType.GetFocusedRow
            If itemRevenueType IsNot Nothing Then
                'Asigno los valores del mismo item obtenido anteriormente
                itemRevenueType.Value = e.NewValue
                itemRevenueType.TotalBudget = e.NewValue
                itemRevenueType.Balance = e.NewValue

                'Obtengo el item de donde se despliega los tipos de ingreso para actualizarles el valor
                Dim itemCategory As Category = CType(INDtlCategory.GetDataRecordByNode(INDtlCategory.FocusedNode), Category)
                If itemCategory IsNot Nothing Then
                    If itemCategory.ListRevenueType IsNot Nothing AndAlso itemCategory.ListRevenueType.Count > 0 Then

                        Dim rt = itemCategory.ListRevenueType.FindAll(Function(item) item.Id = itemRevenueType.Id).FirstOrDefault
                        rt.TotalBudget = e.NewValue
                        rt.Balance = e.NewValue

                        Dim sum = itemCategory.ListRevenueType.Sum(Function(item) item.Value)
                        'Asigno los valores al nodo que tiene el foco
                        itemCategory.Value = sum
                        itemCategory.TotalBudget = sum

                        'Actualizo los valores de todos los padres que hayan hacia arriba
                        UpdateValues(itemCategory.Id)
                        'Actulizo la barra de progreso
                        RefreshProgressBar()

                        'Refresco el footer que totaliza el presupuesto
                        INDtlCategory.ViewInfo.SummaryFooterInfo.NeedsRecalcAll = True
                        INDtlCategory.ViewInfo.CalcSummaryFooterInfo()
                        INDtlCategory.InvalidateSummaryFooterPanel()
                    End If
                End If
            End If
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el formulario de entidades presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleBudgetEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                presenter.InitializeBudgetEntity()
                If BudgetEntityId IsNot Nothing Then
                    presenter.InitializeValidity(BudgetEntityId)
                End If
            End Using
        End If
    End Sub

    ''' <summary>
    ''' evento para crear los rubros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs)
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmBudgetEntities
                Formulario.ViewModeEditHold = True
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
            End Using
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

    ''' <summary>
    ''' Evento que se dispara al desplegar el control para ver los tipos de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepPcIncomeType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDrepPcIncomeType.QueryPopUp
        Dim itemCategory As Category = CType(INDtlCategory.GetDataRecordByNode(INDtlCategory.FocusedNode), Category)
        If itemCategory IsNot Nothing Then
            If itemCategory.Auxiliary Then
                INDtabControl.TabPages(0).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDtabControl.SelectedTabPageIndex = 0
                INDgcIncomeType.DataSource = Nothing

                If ListRevenueType IsNot Nothing AndAlso ListRevenueType.Count > 0 Then
                    If itemCategory.ListRevenueType Is Nothing Then
                        itemCategory.ListRevenueType = New List(Of Domain.Entities.RevenueType)
                        For Each itemList As RevenueType In ListRevenueType
                            Dim revenueType As New RevenueType
                            With revenueType
                                .Id = itemList.Id
                                .Code = itemList.Code
                                .Name = itemList.Name

                                If budgetHeader IsNot Nothing AndAlso budgetHeader.Budget IsNot Nothing AndAlso budgetHeader.Budget.Count > 0 Then
                                    Dim itemBudget = (From b In budgetHeader.Budget Where b.CategoryId = itemCategory.Id AndAlso b.RevenueTypeId = itemList.Id Select b).FirstOrDefault
                                    If itemBudget IsNot Nothing Then
                                        .Value = itemBudget.InitialValue
                                        .Balance = itemBudget.Balance
                                        .TotalBudget = itemBudget.TotalBudget

                                        .CreditValueModification = itemBudget.CreditValueModification
                                        .DebitValueModification = itemBudget.DebitValueModification
                                        .CreditValueTransfer = itemBudget.CreditValueTransfer
                                        .DebitValueTransfer = itemBudget.DebitValueTransfer
                                    End If
                                End If

                            End With
                            itemCategory.ListRevenueType.Add(revenueType)
                        Next
                    End If
                    INDgcIncomeType.DataSource = itemCategory.ListRevenueType
                    viewRevenueType.FocusedRowHandle = 0
                End If
            Else
                INDtabControl.TabPages(0).Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al activar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInitialBudgetEntry_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleBudgetEntity.Focus()
    End Sub

#End Region

#Region "ShowingEditor"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas del treeList
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDtlCategory_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDtlCategory.ShowingEditor
        'Dim _item As Category = CType(INDtlCategory.GetDataRecordByNode(INDtlCategory.FocusedNode), Category)
        'Dim columnTreeList As TreeListColumn = INDtlCategory.Columns.ColumnByName("INDtColIncomeType")
        'If columnTreeList IsNot Nothing AndAlso _item.Auxiliary = True Then
        '    e.Cancel = False
        'Else
        '    e.Cancel = True
        'End If
    End Sub

#End Region

#Region "FormClosing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmInitialBudgetEntry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedRecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar enter en el control de valor
    ''' de la rejilla de tipos de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrepTxtValue_KeyDown(sender As Object, e As KeyEventArgs) Handles INDrepTxtValue.KeyDown
        If e.KeyCode = Keys.Enter Then
            viewRevenueType.FocusedRowHandle = viewRevenueType.FocusedRowHandle + 1
        End If
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que actualiza la barra de progreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RefreshProgressBar()
        If ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 Then
            Dim sum = (From l In ListCategory Where l.CategoryOwnerId Is Nothing Select l.Value).Sum
            valueDigitate = sum
            ctrTmp.RefreshProgressBar()
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el objeto bloqueado
    ''' </summary>
    Async Sub DeleteBlockedRecord()
        If blockRecord IsNot Nothing AndAlso blockRecord.Id > 0 AndAlso blockRecord.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using Model As New MInitialBudgetEntry(Me.Tag)
                Await Model.DeleteBlockRecord(blockRecord)
            End Using
            blockRecord = Nothing
        End If
    End Sub

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
                        StatusValidityName = obtenerRecurso(Registrada, Eform.BudgetEntities)
                    Case 2
                        StatusValidityName = obtenerRecurso(Activa, Eform.BudgetEntities)
                    Case 3
                        StatusValidityName = obtenerRecurso(Cerrada, Eform.BudgetEntities)
                    Case Else
                        StatusValidityName = String.Empty
                End Select
                valueValidity = item.ResolutionValue
            End If
        End If
    End Sub

    ''' <summary>
    ''' Metodo que consulta la cabecera con sus detalles del presupuesto inicial
    ''' para poder mostrar si la vigencia ya ha sido guardada en las tablas
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function LoadBudgetHeader() As Task
        Using model As New MInitialBudgetEntry(Me.Tag)
            Dim result = Await model.GetBudgetHeader(ValidityId, 2)
            If result.StateResult = False Then
                Mensaje(EeventViewerImages.Advertencia) = result.Message
                Exit Function
            End If

            budgetHeader = result.ObjectEmbbeded
            BarraBotones.StatusRecordVisible = True
            If budgetHeader.Id > 0 Then

                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), budgetHeader.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), budgetHeader.CreationDate)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), budgetHeader.ModificationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), budgetHeader.ModificationDate)

                BarraBotones.StatusRecord = budgetHeader.Status.ToString()

                Dim resultBlock = Await model.GetBlockRecord(Me.Tag, budgetHeader.Id)
                If resultBlock.Id = 0 Then
                    Me.BarraBotones.SetDocuments(budgetHeader.Id)
                    Dim state = New Domain.Base.Entities.ObjectChangeTracker
                    state.State = Domain.Base.Entities.ObjectState.Added
                    blockRecord = New BlockRecordBudget With {.BlockDate = Date.Now, .ChangeTracker = state, .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = budgetHeader.Id}
                    Dim operation = Await model.SaveBlockRecord(blockRecord)
                    blockRecord = operation.ObjectEmbbeded
                Else
                    Dim xtraMessage As String = String.Format(ResourceManager.GetString("RecordLocked"), resultBlock.CodUser, resultBlock.NameUser, resultBlock.BlockDate)
                    blockRecord = resultBlock
                    Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, resultBlock.CodUser)
                End If
            Else
                BarraBotones.StatusRecord = "1"
            End If

            If ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 AndAlso budgetHeader.Id > 0 AndAlso budgetHeader.Budget IsNot Nothing AndAlso budgetHeader.Budget.Count > 0 Then
                For Each item As Domain.Entities.Budget In budgetHeader.Budget
                    Dim category As Category = ListCategory.FindAll(Function(x) x.Id = item.CategoryId).FirstOrDefault
                    If category IsNot Nothing Then
                        If category.Auxiliary Then
                            Dim sum = (From l In budgetHeader.Budget Where l.CategoryId = item.CategoryId Select l.InitialValue).Sum
                            category.Value = sum
                            category.TotalBudget = sum
                            UpdateValues(category.Id)
                        End If
                    End If
                Next
                INDtlCategory.RefreshDataSource()
                RefreshProgressBar()
            End If
        End Using
    End Function

    ''' <summary>
    ''' Metodos para asignar valores a la entidad presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AssigningValues()
        If budgetHeader Is Nothing Then
            budgetHeader = New BudgetHeader
        End If
        With budgetHeader
            .BudgetaryValidityId = ValidityId
            If state = 1 OrElse state = 2 Then
                .Status = 1
            Else
                .Status = 2
            End If
            .Type = 2
        End With
        If ListCategory IsNot Nothing AndAlso ListCategory.Count > 0 Then
            For Each itemCategory As Category In ListCategory
                If itemCategory.ListRevenueType IsNot Nothing AndAlso itemCategory.ListRevenueType.Count > 0 Then
                    If itemCategory.ListRevenueType IsNot Nothing AndAlso itemCategory.ListRevenueType.Count > 0 Then
                        For Each itemRevenueType As RevenueType In itemCategory.ListRevenueType
                            Dim budget As Domain.Entities.Budget
                            budget = budgetHeader.Budget.ToList.FindAll(Function(x) x.CategoryId = itemCategory.Id AndAlso x.RevenueTypeId = itemRevenueType.Id).FirstOrDefault
                            If budget Is Nothing Then
                                budget = New Domain.Entities.Budget
                            End If
                            If (itemRevenueType.Value > 0 AndAlso budget.Id = 0) OrElse (budget.Id > 0) Then
                                With budget
                                    .CategoryId = itemCategory.Id
                                    .RevenueTypeId = itemRevenueType.Id
                                    .InitialValue = itemRevenueType.Value
                                    .DebitValueModification = itemRevenueType.DebitValueModification
                                    .CreditValueModification = itemRevenueType.CreditValueModification
                                    .DebitValueTransfer = itemRevenueType.DebitValueTransfer
                                    .CreditValueTransfer = itemRevenueType.CreditValueTransfer
                                    .TotalBudget = itemRevenueType.TotalBudget
                                    .ExecutedValue = itemRevenueType.ExecutedValue
                                    .SuspendedValue = itemRevenueType.SuspendedValue
                                    .Balance = itemRevenueType.Balance
                                End With
                                budgetHeader.Budget.Add(budget)
                            End If
                        Next
                    End If
                End If
            Next
        End If
        If budgetHeader.Id > 0 Then
            If budgetHeader.Budget IsNot Nothing AndAlso budgetHeader.Budget.Count > 0 Then
                Dim cont As Integer = 0
                While cont < budgetHeader.Budget.Count
                    If budgetHeader.Budget(cont).InitialValue = 0 AndAlso budgetHeader.Budget(cont).Id > 0 Then
                        budgetHeader.Budget(cont).MarkAsDeleted()
                        cont = 0
                        Continue While
                    End If
                    cont += 1
                End While
            End If
            budgetHeader.MarkAsModified()
        End If
    End Sub

    ''' <summary>
    ''' Metodo encargado de cargar el listado los tipos de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadListRevenueType() As String
        Dim listErrors As New StringBuilder
        Dim _listXpCollection = presenter.ListRevenueTypeByBudgetValidityIdAndType(ValidityId, 2)
        ListRevenueType = New List(Of Domain.Entities.RevenueType)
        If _listXpCollection IsNot Nothing AndAlso _listXpCollection.Count > 0 Then
            For Each itemXpo As BudgetRevenueTypeXpo In _listXpCollection
                Dim revenueType As New RevenueType
                With revenueType
                    .Id = itemXpo.Id
                    .Code = itemXpo.Code
                    .Name = itemXpo.Name
                End With
                ListRevenueType.Add(revenueType)
            Next
        End If
        If ListRevenueType.Count = 0 Then
            listErrors.AppendLine("La vigencia " + INDsleValidity.Text + " de la entidad " + INDsleBudgetEntity.Text + " no tiene parametrizado tipos de Gasto.")
        End If
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo encargado de cargar el datasource del treelist
    ''' </summary>
    ''' <remarks></remarks>
    Private Function LoadStructure() As String
        Dim listErrors As New StringBuilder
        Dim _structure As XPCollection = presenter.ListCategoryTreeList(ValidityId, 2)
        ListCategory = New List(Of Category)
        If _structure IsNot Nothing AndAlso _structure.Count > 0 Then
            For Each itemXpo As Infrastructure.Data.Xpo.BudgetRepository.BudgetCategoryXpo In _structure
                Dim _item As New Category
                With _item
                    .MoreInfo = String.Empty
                    .Id = itemXpo.Id
                    .Code = itemXpo.Code
                    .AlternativeCode = itemXpo.AlternativeCode
                    If itemXpo.FinancialSourceId IsNot Nothing Then
                        .FinancialSourceId = itemXpo.FinancialSourceId.Id
                        .FinancialSourceDescription = itemXpo.FinancialSourceId.NameCode
                    End If
                    .Name = itemXpo.Name
                    .Auxiliary = itemXpo.Auxiliary
                    .Used = itemXpo.Used
                    If itemXpo.CategoryOwnerId IsNot Nothing Then
                        .CategoryOwnerId = itemXpo.CategoryOwnerId.Id
                    End If
                End With
                ListCategory.Add(_item)
            Next

            'Se le asignan las propiedades al treeList para que muestre el footer
            INDtlCategory.OptionsView.ShowSummaryFooter = True
            'INDtlCategory.Columns("TotalBudget").AllNodesSummary = True
            INDtlCategory.Columns("TotalBudget").SummaryFooterStrFormat = "Total Sum {0:C0}"
            INDtlCategory.Columns("TotalBudget").SummaryFooter = SummaryItemType.Sum
        Else
            listErrors.AppendLine("La vigencia " + INDsleValidity.Text + " de la entidad " + INDsleBudgetEntity.Text + " no tiene parametrizado rubros de ingreso.")
        End If

        INDtlCategory.RefreshDataSource()
        INDtlCategory.ExpandAll()
        Return listErrors.ToString
    End Function

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        ActionsOnControls = False
        BudgetEntityId = Nothing
        ValidityId = Nothing
        ValidityXpo = Nothing
        ListCategory = Nothing
        ListRevenueType = Nothing
        budgetHeader = Nothing
        ctrTmp.RefreshInfo()
        valueDigitate = 0
        valueValidity = 0
        ctrTmp.RefreshProgressBar()
        BarraBotones.StatusRecordVisible = False
        DeleteBlockedRecord()
        Me.BarraBotones.CleanAuditBasic()
        INDsleBudgetEntity.Focus()
    End Sub

    ''' <summary>
    ''' Metodo recursivo que actualiza los valores de todos los padres
    ''' que hayan hacia arriba
    ''' </summary>
    ''' <param name="id"></param>
    ''' <remarks></remarks>
    Private Sub UpdateValues(id As Integer)
        'Obtengo el registro en el cual estoy parado
        Dim category = ListCategory.FindAll(Function(item) item.Id = id).FirstOrDefault
        If category.CategoryOwnerId IsNot Nothing Then
            'Obtengo el padre del registro en el cual estoy parado
            Dim categoryParent = ListCategory.FindAll(Function(item) item.Id = category.CategoryOwnerId).FirstOrDefault
            'Obtengo todos los hijos del padre obtenido
            Dim ListCategoryChields = (From l In ListCategory Where l.CategoryOwnerId = categoryParent.Id Select l).ToList
            'Sumo los valores
            Dim sum = ListCategoryChields.Sum(Function(item) item.Value)
            'Actualizo los valores del padre
            With categoryParent
                .Value = sum
                .TotalBudget = sum
            End With
            'Repito el proceso con el padre
            UpdateValues(categoryParent.Id)
        End If
    End Sub

    ''' <summary>
    ''' Carga la lista de estados en la barra de botones
    ''' </summary>
    Private Sub LoadStatus()
        Dim listStates As New List(Of StatusRecord)()
        listStates.Add(New StatusRecord With {.StatusValue = "1", .StatusName = ResourceManager.GetString("StateUnconfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "2", .StatusName = ResourceManager.GetString("StateConfirmed"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(122, Byte), Integer), CType(CType(204, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "3", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.FromArgb(CType(CType(231, Byte), Integer), CType(CType(76, Byte), Integer), CType(CType(60, Byte), Integer))})
        listStates.Add(New StatusRecord With {.StatusValue = "-1", .StatusName = ResourceManager.GetString("StatusCanceled"), .StatusColor = System.Drawing.Color.White})
        Me.BarraBotones.States = listStates
    End Sub

#End Region

#Region "ToolBar"

    ''' <summary>
    ''' Evento load de la barra de usuario
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyTag)
    End Sub

    ''' <summary>
    ''' Evento deshacer de la barra de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Evento guardar de la barra de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        state = 1
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento actualizar de la barra de usuario
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        state = 2
        Guardar()
    End Sub

    ''' <summary>
    ''' Evento guardarConfirmar de la barra de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_GuardarConfirmar() Handles BarraBotones.Click_GuardarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Evento actualizarConfirmar de la barra de usuarios
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Click_ActualizarConfirmar() Handles BarraBotones.Click_ActualizarConfirmar
        If MessageIndigo.Show(ResourceManager.GetString("ConfirmMessage"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            state = 3
            Guardar()
        End If
    End Sub

    ''' <summary>
    ''' Se ejecuta en al dar click sobre el boton imprimir de la barra
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, ValidityId.Value, 0, ValidityId.Value)
    End Sub

#End Region

End Class