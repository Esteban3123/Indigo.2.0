'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Juan Carlos Bermudez
' Created          : 25-09-2015
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Budget.MVP

#End Region

Public Class FrmExpenseMonthlyClosing
    Implements IExpenseMonthlyClosing

#Region "GLOBALS"
    Dim presenter As PExpenseMonthlyClosing

    Dim validity As BudgetaryValidity
#End Region

#Region "PROPERTIES"
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

    Public ReadOnly Property MyTag As Object Implements IExpenseMonthlyClosing.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public Property BudgetaryValidityXpo As XPCollection Implements IExpenseMonthlyClosing.BudgetaryValidityXpo
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    Public Property BudgetEntitiesXpo As XPInstantFeedbackSource Implements IExpenseMonthlyClosing.BudgetEntitiesXpo
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la entidad
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetEntitiesId As Integer
        Get
            Return INDsleEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property
#End Region

#Region "CRUD"
    ''' <summary>
    ''' Metodos Crud sin usar pero se dejan porque son implementaciondes de IcrudBase
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

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements IcrudBase.OpenSearch

    End Sub
#End Region

#Region "METHODS"
    ''' <summary>
    ''' Metodo de limpiar los controles de la vista
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        validity = Nothing
        INDsleEntity.EditValue = Nothing
        INDsleValidity.EditValue = Nothing
        INDTxtMonth.Text = String.Empty
        INDsleEntity.Focus()
    End Sub
    ''' <summary>
    ''' Metodo para obtener documentos no confirmados
    ''' </summary>
    ''' <returns></returns>

    Private Function GetDocumentsNotConfirmed() As List(Of Tuple(Of String, String, DateTime))
        Using model As New MConfirmationDocumentsExpenses(MyTag)
            Dim listDocumentsNotConfirmd As New List(Of Tuple(Of String, String, DateTime))
            Dim listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.AnnualizedCashFlowModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("PAC Modificaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If
            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.AnnualizedCashFlowTransfer, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("PAC Traslados", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.BudgetModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Presupuesto Modificaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.BudgetTransfer, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Presupuesto Traslados", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.Availability, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Disponibilidad", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.AvailabilityModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Modificación de Disponibilidades", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.Commitment, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Compromisos", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.CommitmentModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Modificación de Compromisos", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.Obligation, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Obligación", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.ObligationModification, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Modificación de Obligaciones", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If

            listDocument = model.ListBudgetDocumentsNotConfirmedExpenses(EBudgetDocumentTypeExpense.PaymentOrder, BudgetaryValidityId)
            If listDocument.Count > 0 Then
                For Each item In listDocument
                    Dim itemAdd = New Tuple(Of String, String, DateTime)("Ordenes de Pago", item.Code, item.DocumentDate)
                    listDocumentsNotConfirmd.Add(itemAdd)
                Next
            End If
            Return listDocumentsNotConfirmd
        End Using
    End Function

#End Region

#Region "HANDLES"
    ''' <summary>
    ''' Metodo al cerrar el frm el cual libera la memoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        validity = Nothing
    End Sub
    ''' <summary>
    ''' Metodo para cargar el frm FrmIncomeMonthlyClosing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>

    Private Sub FrmIncomeMonthlyClosing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PExpenseMonthlyClosing(Me)
    End Sub
    ''' <summary>
    ''' Metodo para mostrar el frm FrmIncomeMonthlyClosing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmIncomeMonthlyClosing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEntity.Focus()
    End Sub
    ''' <summary>
    ''' Evento al dar click en el boton de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleEntity.EditValue, True)
        End If
    End Sub

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

    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            BudgetaryValidityXpo = Nothing
            presenter.InitializeValidity(BudgetEntitiesId)
        End If
    End Sub

    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            Using model As New MExpenseMonthlyClosing(MyTag)
                validity = model.GetValidity(INDsleValidity.EditValue)
                If validity.ExpenseMonth <= 12 Then
                    INDtxtMonth.Text = DateAndTime.MonthName(validity.ExpenseMonth).ToUpper()
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Else
                    INDtxtMonth.Text = If(validity.ExpenseMonth = 13, "PERIODOS CERRADOS", "VIGENCIA CERRADA")
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                End If
            End Using

        End If
    End Sub
#End Region

#Region "BAR BUTTONS"
    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub

    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Try
            AsyncLoader(True)

            Dim list = GetDocumentsNotConfirmed()
            If list.Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No se puede cerrar el mes hasta que se confirmen los siguientes documentos"
                Using formDocuments As New FrmDocumentNotConfirmed
                    formDocuments.ListDocumet = list
                    formDocuments.Size = New System.Drawing.Size(800, 730)
                    formDocuments.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
                    Dim frmTransparent As New FrmTransparent(formDocuments, False)
                    frmTransparent.ShowDialog(Me)
                End Using
                AsyncLoader(False)
                Exit Sub
            End If

            Using model As New MExpenseMonthlyClosing(MyTag)
                validity.ExpenseMonth += 1
                validity.MarkAsModified()
                Dim result = Await model.CloseMonth(validity)
                If result.StateResult = True Then
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Informacion) = "Se cerro correctamente el mes de " + INDtxtMonth.Text
                    CleanControls()
                Else
                    AsyncLoader(False)
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
#End Region
End Class