Imports Presentation.Base
Imports Presentation.Budget.MVP
Imports Presentation.Controls.MVP
Imports System.Drawing
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports System.Text
Imports Domain.Base.Entities

''' <summary>
''' Formulario Show Dialog de modificacion de presupuesto
''' </summary>
''' <remarks></remarks>
Public Class FrmModificationShowDialog

#Region "NEW"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <remarks></remarks>
    Sub New(_itemType As EItemType, _tagForm As String, _validityId As Integer, listBudgetNew As List(Of Domain.Entities.Budget))
        Me._itemType = _itemType
        Me._tagForm = _tagForm
        Me._validityId = _validityId
        Me._listBudgetEntryNew = listBudgetNew

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

    End Sub
#End Region

#Region "Globals"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "Budget"

    ''' <summary>
    ''' Tipo, 1 ingreso, 2 gasto
    ''' </summary>
    ''' <remarks></remarks>
    Dim _itemType As EItemType

    ''' <summary>
    ''' tag form
    ''' </summary>
    ''' <remarks></remarks>
    Dim _tagForm As String

    ''' <summary>
    ''' Entidad xpo de rubros
    ''' </summary>
    ''' <remarks></remarks>
    Dim entityCategoryXpo As BudgetCategoryXpo

    ''' <summary>
    ''' Entidad xpo de tipos de ingreso
    ''' </summary>
    ''' <remarks></remarks>
    Dim entityRevenueTypeXpo As BudgetRevenueTypeXpo

    ''' <summary>
    ''' Id vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validityId As Integer
    ''' <summary>
    ''' lista de rubros agregados
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listBudgetEntryNew As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' listado de items del presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim _listBudget As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' propiedad que contiene el id de la cabecera del presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim budgetHeaderId As Integer

#End Region

#Region "Properties"
    ''' <summary>
    ''' Propiedad para mostrar mensajes en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
        Set(value As String)
            If Icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text, Me)
            ElseIf Icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property
    ''' <summary>
    ''' Variable para almacenar una lista de los detalles de modificacion presupuestal 
    ''' </summary>
    Dim _listModificationDetail As List(Of BudgetModificationDetail)

    ''' <summary>
    ''' Propiedad para obtener los detalles de modificacion
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListModificationDetail As List(Of BudgetModificationDetail)
        Get
            Return _listModificationDetail
        End Get
    End Property
    ''' <summary>
    ''' Propiedad que obtiene una lista de presupuestos nuevos
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property ListBudgetEntryNew As List(Of Domain.Entities.Budget)
        Get
            Return _listBudgetEntryNew
        End Get
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Adiciona la informacion a la rejilla del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInfo()
        Dim view As GridView = INDgvBudget

        Dim listHandlesSelected = view.GetSelectedRows
        If Not (listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item."
            Exit Sub
        End If
        _listModificationDetail = New List(Of Domain.Entities.BudgetModificationDetail)
        For i = 0 To listHandlesSelected.Count - 1
            Dim row As New Domain.Entities.Budget
            row = view.GetRow(listHandlesSelected(i))
            'If row.Id <> 0 Then
            '    row.MarkAsModified()
            'End If
            Dim budgetModification = New BudgetModificationDetail With {.BudgetId = row.Id, .RevenueTypeId = row.RevenueTypeId, .CodeCategory = row.CodeCategory, .NameCategory = row.NameCategory, .CategoryId = row.CategoryId, .CodeNameFinancialSource = row.CodeNameFinancialSource, .FinancialSourceId = row.FinancialSourceId, .CodeNameRevenueType = row.CodeNameRevenueType, .BalanceBudget = row.Balance, .Nature = 0, .Value = 0}
            If row.Id = 0 Then
                budgetModification.Budget = row
            End If
            _listModificationDetail.Add(budgetModification)
        Next
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Carga el datasource de la rejilla con el presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBudgetByValidityId(withBalance As Boolean)
        Using model As New MBusqueda
            '************Cargo los rubros*************'
            Dim filter() As Object = {_validityId, 2, 1, withBalance}
            Dim listXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBudgetByBudgetValidityId, filter)

            'Se valida que si hay presupuesto inicial confirmado
            If listXpo Is Nothing OrElse listXpo.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay presupuesto inicial confirmado con la vigencia seleccionada"
                Exit Sub
            End If

            LoadListBudget(listXpo)
            If withBalance = False Then
                If _listBudgetEntryNew IsNot Nothing Then
                    _listBudget.AddRange(_listBudgetEntryNew)
                End If
            End If
            INDgcBudget.DataSource = Nothing
            INDgcBudget.DataSource = _listBudget
            INDgcBudget.RefreshDataSource()
        End Using
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListBudget(listXpo As XPCollection)
        _listBudget = New List(Of Domain.Entities.Budget)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            For Each itemXpo As BudgetXpo In listXpo
                Dim budget As New Domain.Entities.Budget
                With budget

                    .Id = itemXpo.Id
                    .CategoryId = itemXpo.CategoryId.Id
                    .CodeCategory = itemXpo.CategoryId.Code
                    .NameCategory = itemXpo.CategoryId.Name

                    .FinancialSourceId = itemXpo.CategoryId.FinancialSourceId.Id
                    .CodeNameFinancialSource = itemXpo.CategoryId.FinancialSourceId.NameCode

                    .RevenueTypeId = itemXpo.RevenueTypeId.Id
                    .CodeNameRevenueType = itemXpo.RevenueTypeId.NameCode

                    If budgetHeaderId = Nothing Then
                        budgetHeaderId = itemXpo.BudgetHeaderId.Id
                    End If
                    .BudgetHeaderId = itemXpo.BudgetHeaderId.Id
                    .InitialValue = itemXpo.InitialValue
                    .DebitValueModification = itemXpo.DebitValueModification
                    .CreditValueModification = itemXpo.CreditValueModification
                    .DebitValueTransfer = itemXpo.DebitValueTransfer
                    .CreditValueTransfer = itemXpo.CreditValueTransfer
                    .TotalBudget = itemXpo.TotalBudget
                    .ExecutedValue = itemXpo.ExecutedValue
                    .SuspendedValue = itemXpo.SuspendedValue
                    .Balance = itemXpo.Balance
                    .CreationUser = itemXpo.CreationUser
                    .CreationDate = itemXpo.CreationDate
                End With
                _listBudget.Add(budget)
            Next
        End If
    End Sub

    ''' <summary>
    ''' Metodo que agrega un presupuesto a la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddBudgetToGrid()
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            INDsleCategory.Focus()
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If
        Dim result = ValidateListXpo()
        If result.StateResult = False Then
            If result.Message = "check" Then
                INDchkShowZeroBudget.EditValue = Nothing
                INDchkShowZeroBudget.EditValue = True
            End If
            Exit Sub
        End If
        Dim budget As New Domain.Entities.Budget
        With budget
            .CategoryId = entityCategoryXpo.Id
            .CodeCategory = entityCategoryXpo.Code
            .NameCategory = entityCategoryXpo.Name

            .FinancialSourceId = entityCategoryXpo.FinancialSourceId.Id
            .CodeNameFinancialSource = entityCategoryXpo.FinancialSourceId.NameCode

            .RevenueTypeId = entityRevenueTypeXpo.Id
            .CodeNameRevenueType = entityRevenueTypeXpo.NameCode

            .BudgetHeaderId = budgetHeaderId
            .InitialValue = 0
            .DebitValueModification = 0
            .CreditValueModification = 0
            .DebitValueTransfer = 0
            .CreditValueTransfer = 0
            .TotalBudget = 0
            .ExecutedValue = 0
            .SuspendedValue = 0
            .Balance = 0
        End With

        If _listBudgetEntryNew Is Nothing Then
            _listBudgetEntryNew = New List(Of Domain.Entities.Budget)
        End If
        _listBudgetEntryNew.Add(budget)
        INDchkShowZeroBudget.EditValue = Nothing
        INDchkShowZeroBudget.EditValue = True
        Mensaje(EeventViewerImages.Informacion) = "Item agregado correctamente."
        CleanControlsPopup()
        INDsleCategory.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        INDsleCategory.EditValue = Nothing
        entityCategoryXpo = Nothing
        INDtxtFinancialSource.EditValue = String.Empty
        INDsleRevenueType.EditValue = Nothing
        entityRevenueTypeXpo = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que valida los listados de budget y el listado nuevo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListXpo() As ActionResult
        Dim cont As Integer = 0
        Dim message As String = String.Empty

        If _listBudget IsNot Nothing AndAlso _listBudget.Count > 0 Then
            cont = (From l In _listBudget Where l.CategoryId = INDsleCategory.EditValue AndAlso l.RevenueTypeId = INDsleRevenueType.EditValue Select l).Count
        End If

        If _listBudgetEntryNew IsNot Nothing AndAlso _listBudgetEntryNew.Count > 0 AndAlso cont = 0 Then
            cont = (From l In _listBudgetEntryNew Where l.CategoryId = INDsleCategory.EditValue AndAlso l.RevenueTypeId = INDsleRevenueType.EditValue Select l).Count
        End If

        Using model As New MBusqueda
            '************Cargo los rubros*************'
            Dim filter() As Object = {_validityId, 2, 1, False}
            Dim listXpo As XPCollection = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBudgetByBudgetValidityId, filter)
            If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
                If cont = 0 Then
                    cont = (From l In listXpo Where l.CategoryId.Id = INDsleCategory.EditValue AndAlso l.RevenueTypeId.Id = INDsleRevenueType.EditValue Select l).Count
                    message = "check"
                End If
            End If
        End Using

            If cont > 0 Then
                INDsleCategory.Focus()
            Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar el rubro " + INDsleCategory.Text + " con el tipo de ingreso " + INDsleRevenueType.Text
                Return New ActionResult With {.StateResult = False, .Message = message}
            End If
            Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If Object.Equals(INDsleCategory.EditValue, Nothing) = True Then
            listErrors.AppendLine("Debe elegir un Rubro.")
        End If
        If Object.Equals(INDsleRevenueType.EditValue, Nothing) = True Then
            listErrors.AppendLine("Debe elegir un Tipo de Ingreso.")
        End If

        'Se valida que haya confirmado un presupuesto inicial para poder agregar el nuevo item
        If budgetHeaderId = Nothing OrElse budgetHeaderId = 0 Then
            listErrors.AppendLine("No hay presupuesto inicial confirmado con la vigencia seleccionada.")
        End If

        Return listErrors.ToString
    End Function

#End Region

#Region "Handles"

#Region "Load"
    ''' <summary>
    ''' Libera memoria del frm al cerrar lo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _itemType = Nothing
        _tagForm = Nothing
        entityCategoryXpo = Nothing
        entityRevenueTypeXpo = Nothing
        _validityId = Nothing
        _listBudgetEntryNew = Nothing
        _listBudget = Nothing
        budgetHeaderId = Nothing
    End Sub


    ''' <summary>
    ''' Load del show dialog
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmModificationShowDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        IndigoGridControl1.RefreshGrid(INDgcBudget)
        If _listBudgetEntryNew IsNot Nothing AndAlso _listBudgetEntryNew.Count > 0 Then
            INDchkShowZeroBudget.EditValue = Nothing
            INDchkShowZeroBudget.EditValue = True
        Else
            LoadBudgetByValidityId(True)
        End If
    End Sub

#End Region

#Region "Activated"
    ''' <summary>
    ''' Hace foco en el control INDpceAddCategory
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmModificationShowDialog_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
        INDpceAddCategory.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Cuando se selecciona un rubro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCategory_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCategory.EditValueChanged
        If INDsleCategory.EditValue IsNot Nothing Then
            entityCategoryXpo = DirectCast(DirectCast(INDgvCategory.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BudgetRepository.BudgetCategoryXpo)
            INDtxtFinancialSource.EditValue = INDgvCategory.GetFocusedRowCellValue("FinancialSourceId.NameCode")
        End If
    End Sub

    ''' <summary>
    ''' Cuando se checkea ver presupuesto con saldo en 0
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDchkShowZeroBudget_EditValueChanged(sender As Object, e As EventArgs) Handles INDchkShowZeroBudget.EditValueChanged
        If Object.Equals(INDchkShowZeroBudget.EditValue, Nothing) = True Then
            Exit Sub
        End If
        LoadBudgetByValidityId(Not INDchkShowZeroBudget.EditValue)
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRevenueType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRevenueType.EditValueChanged
        If INDsleRevenueType.EditValue IsNot Nothing Then
            entityRevenueTypeXpo = DirectCast(DirectCast(INDgvRenueveType.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BudgetRepository.BudgetRevenueTypeXpo)
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Click en incluir rubro a presupuesto
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAdd_Click(sender As Object, e As EventArgs) Handles INDsbAdd.Click
        AddBudgetToGrid()
    End Sub

    ''' <summary>
    ''' Evento click boton aceptar 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAccept_Click(sender As Object, e As EventArgs) Handles INDsbAccept.Click
        AddInfo()
    End Sub

#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' carga el datasource del search de rubros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCategory_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleCategory.QueryPopUp
        If INDsleCategory.Properties.DataSource Is Nothing Then
            Using model As New MBusqueda
                Dim filter() As Object = {True, _validityId, 1}
                INDsleCategory.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId, filter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource del search de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRevenueType_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDsleRevenueType.QueryPopUp
        If INDsleRevenueType.Properties.DataSource Is Nothing Then
            Using model As New MBusqueda
                Dim filter() As Object = {_validityId, 1, True}
                INDsleRevenueType.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRevenueTypeByBudgetValidityIdAndTypeAndStatus, filter)
            End Using
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCategory_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleCategory.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmBudgetItems With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Using model As New MBusqueda
                '************Cargo los rubros*************'
                Dim filter() As Object = {True, _validityId, CInt(_itemType)}
                INDsleCategory.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListBudgetCategoryByStatuAndValidityIdAndItemTypeAndFinancialSourceId, filter)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Manejador del evento que se dispara al darle click sobre el boton del control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleRevenueType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRevenueType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmEarningsType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Using model As New MBusqueda
                'cargo los tipos
                Dim filter() As Object = {_validityId, 1, True}
                INDsleRevenueType.Properties.DataSource = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRevenueTypeByBudgetValidityIdAndTypeAndStatus)
            End Using
        End If
    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceAddCategory_Popup(sender As Object, e As EventArgs) Handles INDpceAddCategory.Popup
        INDsleCategory.Focus()
    End Sub

#End Region

#Region "InvalidRowException"

    ''' <summary>
    ''' Controla la excepcion arrojada por la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvBudget_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles INDgvBudget.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

#End Region

#Region "RowClick"

    ''' <summary>
    ''' Doble click en el registro del grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvBudget_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles INDgvBudget.RowClick
        If e.Clicks = 2 AndAlso e.RowHandle >= 0 Then
            AddInfo()
        End If
    End Sub

#End Region

#End Region

End Class