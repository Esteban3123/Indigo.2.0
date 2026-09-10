'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Carlos Mario Arias Rubiano
' Created          : 13/08/2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Xpo
Imports DevExpress.XtraGrid.Views.Grid
Imports Domain.Base.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo.BudgetRepository
Imports Presentation.Base
Imports Presentation.Budget.MVP
Imports Presentation.Controls
#End Region

Public Class FrmAddCategory
    Implements IAddCategory

#Region "Properties"

    ''' <summary>
    ''' Obtiene o establece el id de la cabecera del presupuesto
    ''' </summary>
    ''' <remarks></remarks>
    Private _budgetHeaderId As Integer
    Public Property BudgetHeaderId As Integer
        Get
            Return _budgetHeaderId
        End Get
        Set(value As Integer)
            _budgetHeaderId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Private _ValidityId As Integer
    Public Property ValidityId As Integer
        Get
            Return _ValidityId
        End Get
        Set(value As Integer)
            _ValidityId = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del rubro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryId As Integer? Implements IAddCategory.CategoryId
        Get
            Return INDsleCategory.EditValue
        End Get
        Set(value As Integer?)
            INDsleCategory.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del rubro
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CategoryXpo As XPInstantFeedbackSource Implements IAddCategory.CategoryXpo
        Get
            Return INDsleCategory.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleCategory.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene el layout
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IAddCategory.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

    ''' <summary>
    ''' Obtiene el tag
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements IAddCategory.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o establece el id del tipo de ingreso
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeId As Integer? Implements IAddCategory.TypeId
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Establece el datasource del tipo
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TypeXpo As XPInstantFeedbackSource Implements IAddCategory.TypeXpo
        Get
            Return INDsleType.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleType.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si se muestra el presupuesto con saldo cero
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CheckCategoryZero As Boolean Implements IAddCategory.CheckCategoryZero
        Get
            Return INDcheckCategory.EditValue
        End Get
        Set(value As Boolean)
            INDcheckCategory.EditValue = value
        End Set
    End Property

#End Region

#Region "Variables"

    ''' <summary>
    ''' Representa al presentador
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PAddCategory

    ''' <summary>
    ''' Listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListBudget As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Listado de nuevos de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Public ListNewBudget As List(Of Domain.Entities.Budget)

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
    ''' Evento publico para agregar un presupuesto en
    ''' la rejilla del form principal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddBudgetToGridFormPrincipal(sender As Object, e As AddInfoToGridFormPrincipal)

#End Region

#Region "Methods"

    ''' <summary>
    ''' Adiciona la informacion a la rejilla del form principal
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddInfo()
        Dim view As GridView = viewGridBudget
        Dim listHandlesSelected = view.GetSelectedRows
        If Not (listHandlesSelected IsNot Nothing AndAlso listHandlesSelected.Length > 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar al menos un item."
            Exit Sub
        End If
        Dim ListSend As New List(Of Domain.Entities.Budget)
        For i = 0 To listHandlesSelected.Count - 1
            Dim row As Domain.Entities.Budget = view.GetRow(listHandlesSelected(i))
            If row.Id > 0 Then
                row.MarkAsUnchanged()
            End If
            ListSend.Add(row)
        Next
        Dim args As New AddInfoToGridFormPrincipal With {.ListSend = ListSend, .ListNew = ListNewBudget}
        RaiseEvent AddBudgetToGridFormPrincipal(Nothing, args)
    End Sub

    ''' <summary>
    ''' Caraga el datasource de la rejilla con el presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadBudgetByValidityId(withBalance As Boolean)
        Dim listXpo = presenter.LoadBudgetByValidityId(ValidityId, 1, withBalance)
        LoadListBudget(listXpo)
        If withBalance = False Then
            If ListNewBudget IsNot Nothing Then
                ListBudget.AddRange(ListNewBudget)
            End If
        End If
        INDgcBudget.DataSource = Nothing
        INDgcBudget.DataSource = ListBudget
        INDgcBudget.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Metodo que carga el listado de presupuesto inicial
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadListBudget(listXpo As XPCollection)
        ListBudget = New List(Of Domain.Entities.Budget)
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

                    If BudgetHeaderId = Nothing Then
                        BudgetHeaderId = itemXpo.BudgetHeaderId.Id
                    End If
                    .BudgetHeaderId = itemXpo.BudgetHeaderId.Id
                End With
                ListBudget.Add(budget)
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
                CheckCategoryZero = Nothing
                CheckCategoryZero = True
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

            .BudgetHeaderId = BudgetHeaderId
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

        If ListNewBudget Is Nothing Then
            ListNewBudget = New List(Of Domain.Entities.Budget)
        End If
        ListNewBudget.Add(budget)
        CheckCategoryZero = Nothing
        CheckCategoryZero = True
        Mensaje(EeventViewerImages.Informacion) = "Item agregado correctamente."
        CleanControlsPopup()
        INDsleCategory.Focus()
    End Sub

    ''' <summary>
    ''' Metodo que valida los listados de budget y el listado nuevo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateListXpo() As ActionResult
        Dim cont As Integer = 0
        Dim message As String = String.Empty

        If ListBudget IsNot Nothing AndAlso ListBudget.Count > 0 Then
            cont = (From l In ListBudget Where l.CategoryId = CategoryId AndAlso l.RevenueTypeId = TypeId Select l).Count
        End If

        If ListNewBudget IsNot Nothing AndAlso ListBudget.Count > 0 AndAlso cont = 0 Then
            cont = (From l In ListNewBudget Where l.CategoryId = CategoryId AndAlso l.RevenueTypeId = TypeId Select l).Count
        End If

        Dim listXpo = presenter.LoadBudgetByValidityId(ValidityId, 1, False)
        If listXpo IsNot Nothing AndAlso listXpo.Count > 0 Then
            If cont = 0 Then
                cont = (From l In listXpo Where l.CategoryId.Id = CategoryId AndAlso l.RevenueTypeId.Id = TypeId Select l).Count
                message = "check"
            End If
        End If

        If cont > 0 Then
            INDsleCategory.Focus()
            Mensaje(EeventViewerImages.Advertencia) = "No se puede agregar el rubro " + INDsleCategory.Text + " con el tipo de ingreso " + INDsleType.Text
            Return New ActionResult With {.StateResult = False, .Message = message}
        End If
        Return New ActionResult With {.StateResult = True}
    End Function

    ''' <summary>
    ''' Metodo que limpia los controles del popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControlsPopup()
        CategoryId = Nothing
        entityCategoryXpo = Nothing
        INDsleFinancialSource.Properties.NullText = String.Empty
        TypeId = Nothing
        entityRevenueTypeXpo = Nothing
    End Sub

    ''' <summary>
    ''' Metodo que valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateControlsPopup() As String
        Dim listErrors As New StringBuilder
        If Object.Equals(CategoryId, Nothing) = True Then
            listErrors.AppendLine("Debe elegir un Rubro.")
        End If
        If Object.Equals(TypeId, Nothing) = True Then
            listErrors.AppendLine("Debe elegir un Tipo de Ingreso.")
        End If
        Return listErrors.ToString
    End Function

#End Region

#Region "ICrud Base"
    ''' <summary>
    ''' Metodos Crud sin usarse 
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
    ''' Slide de Mensajes
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
    ''' Metodo para categorias nuevas , sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
    ''' <summary>
    ''' Metodo para abrir la busqueda , sin usarse
    ''' </summary>
    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Libera memoria al cerrar el frm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        ListBudget = Nothing
        ListNewBudget = Nothing
        entityCategoryXpo = Nothing
        entityRevenueTypeXpo = Nothing
    End Sub


    ''' <summary>
    ''' Evento load del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PAddCategory(Me)
        IndigoGridControl1.RefreshGrid(INDgcBudget)
        If ListNewBudget IsNot Nothing AndAlso ListNewBudget.Count > 0 Then
            CheckCategoryZero = Nothing
            CheckCategoryZero = True
        Else
            LoadBudgetByValidityId(True)
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se dispara para abrir el form de rubros
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
            'OpenForm("206", Nothing, True)
            presenter.InitializeCategory(ValidityId, 1)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de fuentes de financiacion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleFinancialSource_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleFinancialSource.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmFinancialSource With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            'OpenForm("201", Nothing, True)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara para abrir el form de tipos de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleType_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleType.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmPrincipalEarningsType With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            'OpenForm("202", Nothing, True)
            presenter.InitializeType(ValidityId, 1)
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddCategory_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDpceCategory.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de rubros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCategory_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleCategory.QueryPopUp
        If CategoryXpo Is Nothing Then
            presenter.InitializeCategory(ValidityId, 1)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleType_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleType.QueryPopUp
        If TypeXpo Is Nothing Then
            presenter.InitializeType(ValidityId, 1)
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de rubros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleCategory_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleCategory.EditValueChanged
        If CategoryId IsNot Nothing Then
            entityCategoryXpo = DirectCast(DirectCast(viewCategory.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BudgetRepository.BudgetCategoryXpo)
            INDsleFinancialSource.Properties.NullText = viewCategory.GetFocusedRowCellValue("FinancialSourceId.NameCode")
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If TypeId IsNot Nothing Then
            entityRevenueTypeXpo = DirectCast(DirectCast(viewRevenueType.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, Infrastructure.Data.Xpo.BudgetRepository.BudgetRevenueTypeXpo)
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de check
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDcheckCategory_EditValueChanged(sender As Object, e As EventArgs) Handles INDcheckCategory.EditValueChanged
        If Object.Equals(CheckCategoryZero, Nothing) = True Then
            Exit Sub
        End If
        LoadBudgetByValidityId(Not CheckCategoryZero)
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se disparal al presionar enter o f4 sobre el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceCategory_KeyDown(sender As Object, e As KeyEventArgs) Handles INDpceCategory.KeyDown
        If e.KeyCode = Keys.Enter OrElse e.KeyCode = Keys.F4 Then
            INDpceCategory.ShowPopup()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar escape sobre el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmAddCategory_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDpceCategory_Popup(sender As Object, e As EventArgs) Handles INDpceCategory.Popup
        INDsleCategory.Focus()
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de aceptar en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddPopup_Click(sender As Object, e As EventArgs) Handles INDbtnAddPopup.Click
        AddBudgetToGrid()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de limpiar en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnClear_Click(sender As Object, e As EventArgs) Handles INDbtnClear.Click
        CleanControlsPopup()
        INDsleCategory.Focus()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton agregar del form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDbtnAddCategory_Click(sender As Object, e As EventArgs) Handles INDbtnAddCategory.Click
        AddInfo()
    End Sub

#End Region

#Region "RowClick"

    ''' <summary>
    ''' Evento que se dispara al presionar dobleClick sobre un registro
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewGridBudget_RowClick(sender As Object, e As DevExpress.XtraGrid.Views.Grid.RowClickEventArgs) Handles viewGridBudget.RowClick
        If e.Clicks = 2 AndAlso e.RowHandle >= 0 Then
            AddInfo()
        End If
    End Sub

#End Region

#Region "InvalidRowException"

    ''' <summary>
    ''' Controla la excepcion arrojada por la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub viewGridBudget_InvalidRowException(sender As Object, e As DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs) Handles viewGridBudget.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

#End Region

#End Region

End Class

Public Class AddInfoToGridFormPrincipal
    Inherits EventArgs

    ''' <summary>
    ''' Listado de seleccionados que se envian al form principal
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListSend As List(Of Domain.Entities.Budget)

    ''' <summary>
    ''' Listado de nuevos de presupuesto inicial
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ListNew As List(Of Domain.Entities.Budget)

End Class