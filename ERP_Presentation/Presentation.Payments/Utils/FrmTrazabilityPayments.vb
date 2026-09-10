#Region "Imports"

Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Presentation.Controls
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Entities
Imports DevExpress.Utils.Menu
Imports Domain.Base.Entities
Imports DevExpress.Data
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors
Imports DevExpress.XtraGrid.Views.Base
Imports System.Windows.Forms
Imports DevExpress.Data.PLinq
Imports System.Drawing
Imports System.Resources
Imports System.Text
Imports Presentation.Payments.MVP
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraBars
Imports Presentation.Maintenance
Imports DevExpress.Xpo

#End Region

Public Class FrmTrazabilityPayments
    Implements ITrazabilityPayments

#Region "Fields"

    ''' <summary>
    ''' Bandera para indicar que el frontal ya se cargo
    ''' </summary>
    Private _loaded As Boolean = False
    ''' <summary>
    ''' Referencia a los valores de sesion
    ''' </summary>
    Private _indigoSessionValues As SessionValues
    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private record As Domain.Entities.BlockRecord
    ''' <summary>
    ''' Variable bandera para el registro bloqueado
    ''' </summary>
    Private _recordFlag As Boolean
    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32
    ''' <summary>
    ''' Presentador
    ''' </summary>
    ''' <remarks></remarks>
    Private Presenter As PTrazabilitypayments
    ''' <summary>
    ''' Modelo 
    ''' </summary>
    ''' <remarks></remarks>
    Private _model As MTrazabilitypayments
    ''' <summary>
    ''' Listado de tipos
    ''' </summary>
    Private ListType As List(Of Tuple(Of Integer, String))

#End Region

#Region "ICRUD"
    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        cleancontrol()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub
    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region

#Region "Properties"
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String Implements IcrudBase.Mensaje
        Set(value As String)

        End Set
    End Property

    ''' <summary>
    ''' Aifgano o obtiene la lista de proveedores
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property SupplierXpo As DevExpress.Xpo.XPInstantFeedbackSource Implements ITrazabilityPayments.SupplierXpo
        Get
            Return CType(INDsleSuppliers.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleSuppliers.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' lista de cuentas por pagar
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AccountPayable As DevExpress.Xpo.XPInstantFeedbackSource Implements ITrazabilityPayments.AccountPayable
        Get
            Return CType(Me.INDslePayments.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            Me.INDslePayments.Properties.DataSource = value
        End Set
    End Property

    Public WriteOnly Property ActionsOnControls As Boolean Implements ITrazabilityPayments.ActionsOnControls
        Set(value As Boolean)

        End Set
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements ITrazabilityPayments.MyLayoutControl
        Get

        End Get
    End Property
    ''' <summary>
    ''' tag del formulario
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property MyTag As Object Implements ITrazabilityPayments.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    ''' <summary>
    ''' Obtiene o estbalece el id del proveedor
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IdSupplier As Integer? Implements ITrazabilityPayments.IdSupplier
        Get
            Return CInt(INDsleSuppliers.EditValue)
        End Get
        Set(value As Integer?)
            INDsleSuppliers.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Tipo
    ''' </summary>
    ''' <returns></returns>
    Public Property Type As Integer? Implements ITrazabilityPayments.Type
        Get
            Return INDsleType.EditValue
        End Get
        Set(value As Integer?)
            INDsleType.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource de los reembolsos
    ''' </summary>
    ''' <returns></returns>
    Public Property RefundsXpo As XPInstantFeedbackSource Implements ITrazabilityPayments.RefundsXpo
        Get
            Return INDsleRefunds.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleRefunds.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "BarButtons"
    ''' <summary>
    ''' Barra botones: Deshacer
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
    End Sub

#End Region

#Region "Methods"

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    ''' <summary>
    ''' Limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub cleancontrol()
        INDsleType.EditValue = Nothing
        INDsleSuppliers.EditValue = Nothing
        INDslePayments.EditValue = Nothing
        INDsleRefunds.EditValue = Nothing
        INDtxtUnitRadicateSource.Text = String.Empty
        INDgcTrazability.DataSource = Nothing

        INDlyItemSuppliers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemPayments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDlyItemRefunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

        INDsleType.Focus()

        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    ''' <summary>
    ''' Metodo para cargar las tuplas con la info
    ''' </summary>
    Private Sub InitializeTuples()
        ListType = New List(Of Tuple(Of Integer, String))
        ListType.Add(New Tuple(Of Integer, String)(1, "Cuenta por Pagar"))
        ListType.Add(New Tuple(Of Integer, String)(2, "Reembolsos"))
        INDsleType.Properties.DataSource = ListType.ToList()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _loaded = Nothing
        record = Nothing
        _recordFlag = Nothing
        _idOperativeUnit = Nothing
        Presenter = Nothing
        ListType = Nothing
    End Sub
    ''' <summary>
    ''' Load del frontal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmTrazabilityPayments_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.BarraBotones.PrepareToolbar(eAction.OnlyActionsGrid)
        Me.LayoutControls.SetIsCustomizable(Me.INDlycRoot, True)
        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me._funct = Nothing
        indigo = SessionValues.Instance
        _model = New MTrazabilitypayments(Me.Tag)
        Presenter = New PTrazabilitypayments(Me)
        Deshacer()
        IndigoGridView1.MoreInfoColunmns(INDgvTrazability)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDgvTrazability.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            ElseIf col.Name = "MoreInfo" Then
                col.Width = 50
            End If
        Next
        IndigoGridControl1.RefreshGrid(INDgcTrazability)
        InitializeTuples()
    End Sub

#End Region

#Region "Shown"

    Private Sub Frm_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        INDsleSuppliers.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Querypopup de pagos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePayments_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDslePayments.QueryPopUp
        If INDslePayments.Properties.DataSource Is Nothing Then
            Presenter.InitializeAccountPayable(IdSupplier)
        End If
    End Sub

    ''' <summary>
    ''' querypopup de proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSuppliers_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleSuppliers.QueryPopUp
        If INDsleSuppliers.Properties.DataSource Is Nothing Then
            Presenter.InitializeSupplier()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de reembolsos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRefunds_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRefunds.QueryPopUp
        If RefundsXpo Is Nothing Then
            Presenter.InitializeRefunds()
        End If
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Evento que se ejecuta para abrir el form de reembolsos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRefunds_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleRefunds.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(639, Nothing, True)
            Presenter.InitializeRefunds()
        End If
    End Sub

    ''' <summary>
    ''' Abrir formulario de pagos
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePayments_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDslePayments.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmAccountsPayable With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            If IdSupplier IsNot Nothing Then
                Presenter.InitializeAccountPayable(IdSupplier)
            End If
        End If
    End Sub

    ''' <summary>
    ''' abrir formulario de proveedores
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleSuppliers_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleSuppliers.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Dim size As System.Drawing.Size
            size.Width = 780
            size.Height = 768
            Using pop As New FrmTransparent(New FrmSupplier With {.ViewModeEditHold = True, .StartPosition = System.Windows.Forms.FormStartPosition.CenterParent, .Size = size}, False)
                pop.Show()
            End Using
            Presenter.InitializeSupplier()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de tipo
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleType_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleType.EditValueChanged
        If Type IsNot Nothing Then
            INDgcTrazability.DataSource = Nothing
            If Type = 1 Then 'Cuenta por pagar
                INDlyItemSuppliers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemPayments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDlyItemRefunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDcolThirdParty.Visible = True

                INDsleRefunds.EditValue = Nothing
            Else 'Reembolsos
                INDlyItemSuppliers.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemPayments.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyItemRefunds.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                INDcolThirdParty.Visible = False

                INDsleSuppliers.EditValue = Nothing
                INDslePayments.EditValue = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' cambio de cuenta por pagar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDslePayments_EditValueChanged(sender As Object, e As EventArgs) Handles INDslePayments.EditValueChanged
        If INDsleViewAccuntPayable.FocusedRowHandle >= 0 AndAlso INDslePayments.EditValue IsNot Nothing Then
            Dim objXPo = CType(INDsleViewAccuntPayable.GetRow(INDsleViewAccuntPayable.FocusedRowHandle), DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
            Dim Obj = CType(objXPo.OriginalRow, Infrastructure.Data.Xpo.PaymentsRepository.AccountPayableXpo)
            Dim AccountPayableCode As String = Obj.Code
            Dim BillNumber As String = Obj.BillNumber
            Using _Modeltmp As New MTrazabilitypayments(Me.Tag)
                Me.INDgcTrazability.DataSource = Nothing
                Me.INDtxtUnitRadicateSource.Text = String.Empty
                Me.INDgcTrazability.DataSource = _Modeltmp.GetPaymentsTrazability(AccountPayableCode, BillNumber)
                Me.INDgcTrazability.RefreshDataSource()
            End Using
            If Obj.FilingUnitId IsNot Nothing Then
                Me.INDtxtUnitRadicateSource.Text = Obj.FilingUnitId.Code & " - " & Obj.FilingUnitId.Name
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de reembolso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRefunds_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRefunds.EditValueChanged
        If INDsleRefunds.EditValue IsNot Nothing Then
            'Se obtiene el reembolso por id
            Dim refundXpo = Presenter.GetRefundById(INDsleRefunds.EditValue)
            If refundXpo IsNot Nothing Then 'Se valida si viene el reembolso
                INDgcTrazability.DataSource = Nothing
                INDtxtUnitRadicateSource.Text = String.Empty
                INDgcTrazability.DataSource = Presenter.GetRefundsTrazability(refundXpo.Code)
                INDgcTrazability.RefreshDataSource()
                INDtxtUnitRadicateSource.Text = refundXpo.FilingUnitId.CodeName
            End If
        End If
    End Sub

    ''' <summary>
    ''' cambio de proveedor
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgleSupplier_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleSuppliers.EditValueChanged
        Using _Modeltmp As New MTrazabilitypayments(Me.Tag)
            Me.INDslePayments.EditValue = Nothing
            Me.INDgcTrazability.DataSource = Nothing
            Me.INDgcTrazability.RefreshDataSource()
            Me.INDtxtUnitRadicateSource.Text = String.Empty
            If IdSupplier IsNot Nothing Then
                Presenter.InitializeAccountPayable(IdSupplier)
            End If
        End Using
    End Sub

#End Region

#Region "InvalidRowException"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvTrazability_InvalidRowException(sender As Object, e As InvalidRowExceptionEventArgs) Handles INDgvTrazability.InvalidRowException
        e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.Ignore
    End Sub

#End Region

#End Region

End Class