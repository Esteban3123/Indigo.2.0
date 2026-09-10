'***********************************************************************
' Assembly         : Presentacion.Budget
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 07-04-2014
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.ComponentModel
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Budget.MVP
Imports Presentation.Common
Imports Presentation.Controls.MVP
#End Region

''' <summary>
''' Contiene la vista del show dialog de vigencias dentro del frontal de entidades presupuestales
''' </summary>
''' <remarks></remarks>
Public Class CtrValidity

#Region "Constructor"
    ' ''' <summary>
    ' ''' Constructor
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Sub New(Optional validity As BudgetaryValidity = Nothing)

    '    ' Llamada necesaria para el diseñador.
    '    InitializeComponent()

    '    ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    '    If validity IsNot Nothing Then
    '        Me.BudgetaryValidity = validity
    '    End If
    'End Sub

#End Region

#Region "Global Properties & Variables"

    ''' <summary>
    ''' odtine o estable el representante legal
    ''' </summary>
    ''' <value>
    ''' The legal representative xpo.
    ''' </value>
    Property LegalRepresentativeXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleLegalRepresentative.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleLegalRepresentative.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' obtiene o establece el jefe de presupuesto
    ''' </summary>
    ''' <value>
    ''' The budget boss xpo.
    ''' </value>
    Property BudgetBossXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleBudgetBoss.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleBudgetBoss.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' odtiene o establece el jefe financiero
    ''' </summary>
    ''' <value>
    ''' The financial boss xpo.
    ''' </value>
    Property FinancialBossXPO As DevExpress.Xpo.XPInstantFeedbackSource
        Get
            Return CType(INDsleFinancialBoss.Properties.DataSource, DevExpress.Xpo.XPInstantFeedbackSource)
        End Get
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDsleFinancialBoss.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o Esablece el objeto de vigencia 
    ''' </summary>
    ''' <remarks></remarks>
    Dim _validity As BudgetaryValidity
    Public Property BudgetaryValidity As BudgetaryValidity
        Get
            Return Me._validity
        End Get
        Set(value As BudgetaryValidity)
            Me._validity = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad que establece los mensajes 
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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
    ''' Establece el año minimo de registro
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    WriteOnly Property SetYearMaximum As Integer
        Set(value As Integer)
            'If _validity Is Nothing Then
            '    INDtxtYear.EditValue = value + 1
            'End If
            INDtxtYear.Properties.MinValue = value + 1
            INDtxtYear.EditValue = value + 1
        End Set
    End Property

    ''' <summary>
    ''' Evento Click Aceptar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ClickAccept(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Evento Click Cancelar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event ClickCancel(sender As Object, e As EventArgs)

    ''' <summary>
    ''' Evento que ocurre cuando se cierra el formulario de agregar un tercero
    ''' </summary>
    Public Event CloseFormThirdParty(sender As Object, e As EventArgs)

#End Region

#Region "Handles"
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmValidity_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CleanControls()
        InitializeThirParty()
    End Sub

    ''' <summary>
    ''' Click Boton Aceptar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAccept_Click(sender As Object, e As EventArgs) Handles INDsbAccept.Click
        AddValidity()
    End Sub

    ''' <summary>
    ''' Actualizar Vigencia Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbAcceptU_Click(sender As Object, e As EventArgs) Handles INDsbAcceptU.Click
        AddValidity()
    End Sub

    ''' <summary>
    ''' Eliminar Vigencia Button
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbDelete_Click(sender As Object, e As EventArgs) Handles INDsbDelete.Click
        DeleteValidy()
    End Sub

    ''' <summary>
    ''' Abre el formulario de Entidades Presupuestales en un pop-up
    ''' </summary>
    Private Sub ThirdParty_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleBudgetBoss.ButtonClick, INDsleFinancialBoss.ButtonClick, INDsleLegalRepresentative.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            Using Formulario As New FrmThirdParty
                Formulario.ViewModeEditHold = True
                Formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
                Dim transparent As New FrmTransparent(Formulario, False)
                transparent.ShowDialog()
                RaiseEvent CloseFormThirdParty(Me, New EventArgs)
                InitializeThirParty()
            End Using
        End If
    End Sub

    ''' <summary>
    ''' metodo que se dispara al tratar de editar una celda y valida que el campo usado este en false para permitir la edicion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDgvEarning_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDgvEarning.ShowingEditor
        Dim row As BudgetAccountParameters = INDgvEarning.GetFocusedRow()
        If row.Item5 = True Then
            e.Cancel = True
        End If
    End Sub

    ''' <summary>
    ''' ''' metodo que se dispara al tratar de editar una celda y valida que el campo usado este en false para permitir la edicion
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CancelEventArgs"/> instance containing the event data.</param>
    Private Sub INDgvExpense_ShowingEditor(sender As Object, e As CancelEventArgs) Handles INDgvExpense.ShowingEditor
        Dim row As BudgetAccountParameters = INDgvExpense.GetFocusedRow()
        If row.Item5 = True Then
            e.Cancel = True
        End If
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' metodo para cargar los search con los terceros
    ''' </summary>
    Private Sub InitializeThirParty()
        If Not DesignMode Then
            Using model As New MBusqueda
                LegalRepresentativeXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
                FinancialBossXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
                BudgetBossXPO = model.ConsultarEntidades(eDataSource.ThirdParty)
            End Using
        End If
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        If Not DesignMode Then
            INDtxtYear.Focus()
            INDgleEarningsProcessMonth.Properties.DataSource = Nothing
            INDgleEarningsProcessMonth.Properties.DataSource = BudgetHelper.Months
            INDgleExpenseProcessMonth.Properties.DataSource = Nothing
            INDgleExpenseProcessMonth.Properties.DataSource = BudgetHelper.Months
            INDgleValidityStatus.Properties.DataSource = Nothing
            INDgleValidityStatus.Properties.DataSource = BudgetHelper.ValidityStatus
            INDgcEarning.DataSource = Nothing
            INDgcExpense.DataSource = Nothing
            INDgcEarning.DataSource = BudgetHelper.EarningsParameters.FindAll(Function(x) x.Item1 > 0).ToList
            INDgcExpense.DataSource = BudgetHelper.ExpenseParameters.FindAll(Function(x) x.Item1 > 0).ToList

            If Me.BudgetaryValidity Is Nothing Then
                If INDtxtYear.EditValue = 0 Then
                    INDtxtYear.EditValue = DateTime.Now().Year
                End If
                '**********Limpiar Controles**********'
                INDgleEarningsProcessMonth.EditValue = Nothing
                INDgleExpenseProcessMonth.EditValue = Nothing
                INDtxtResolutionNumber.Text = String.Empty
                INDtxtValue.Text = String.Empty
                INDgleValidityStatus.EditValue = 1
                INDrgPACControl.SelectedIndex = -1
                INDrgDatesAndConsecutivesControl.SelectedIndex = -1
                INDsleLegalRepresentative.EditValue = Nothing
                INDsleBudgetBoss.EditValue = Nothing
                INDsleFinancialBoss.EditValue = Nothing

                'INDColConsecutive1.OptionsColumn.AllowEdit = True
                'INDColConsecutive2.OptionsColumn.AllowEdit = True

                '*********Habilitar controles********'
                INDtxtYear.Enabled = True
                INDgleEarningsProcessMonth.Enabled = True
                INDgleExpenseProcessMonth.Enabled = True
                INDtxtResolutionNumber.Enabled = True
                INDtxtValue.Enabled = True
                INDgleValidityStatus.Enabled = False
                INDrgPACControl.Enabled = True
                INDrgDatesAndConsecutivesControl.Enabled = True
                INDsleLegalRepresentative.Enabled = True
                INDsleBudgetBoss.Enabled = True
                INDsleFinancialBoss.Enabled = True
            Else
                LoadControls()
                'INDpcUpdate.Visible = True
            End If
            INDpcAccept.Visible = True
        End If
    End Sub

    ''' <summary>
    ''' Carga los controles cuando se va a editar una vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadControls()
        INDtxtYear.Text = Me.BudgetaryValidity.Year
        INDgleEarningsProcessMonth.EditValue = Me.BudgetaryValidity.IncomeMonth
        INDgleExpenseProcessMonth.EditValue = Me.BudgetaryValidity.ExpenseMonth
        INDtxtResolutionNumber.Text = Me.BudgetaryValidity.ResolutionNumber
        INDtxtValue.EditValue = Me.BudgetaryValidity.ResolutionValue
        INDgleValidityStatus.EditValue = Me.BudgetaryValidity.Status
        INDrgPACControl.EditValue = Me.BudgetaryValidity.PACControl
        INDrgDatesAndConsecutivesControl.EditValue = Me.BudgetaryValidity.ControlDateConsecutive
        INDsleLegalRepresentative.EditValue = Me.BudgetaryValidity.LegalRepresentativeId
        INDsleBudgetBoss.EditValue = Me.BudgetaryValidity.ChiefBudgetOfficerId
        INDsleFinancialBoss.EditValue = Me.BudgetaryValidity.ChiefFinancialOfficerId

        'Dim dataSourceEarnings As List(Of BudgetAccountParameters) = INDgcEarning.DataSource
        'Dim dataSourceExpense As List(Of BudgetAccountParameters) = INDgcExpense.DataSource
        'For Each itemEarnings In dataSourceEarnings
        '    If BudgetaryValidity.ConsecutiveBudget IsNot Nothing Then
        '        Dim _consecutive As ConsecutiveBudget = BudgetaryValidity.ConsecutiveBudget.Where(Function(x) x.FormId = itemEarnings.Item1 And x.ProcessType = 1).FirstOrDefault
        '        If _consecutive IsNot Nothing Then
        '            itemEarnings.Item4 = _consecutive.NumberConsecutive
        '            itemEarnings.Item5 = _consecutive.Used
        '        End If
        '    End If
        'Next

        'For Each itemExpense In dataSourceExpense
        '    If BudgetaryValidity.ConsecutiveBudget IsNot Nothing Then
        '        Dim _consecutive As ConsecutiveBudget = BudgetaryValidity.ConsecutiveBudget.Where(Function(x) x.FormId = itemExpense.Item1 And x.ProcessType = 2).FirstOrDefault
        '        If _consecutive IsNot Nothing Then
        '            itemExpense.Item4 = _consecutive.NumberConsecutive
        '            itemExpense.Item5 = _consecutive.Used
        '        End If
        '    End If
        'Next
        'INDgcEarning.RefreshDataSource()
        'INDgcExpense.RefreshDataSource()


        With Me.BudgetaryValidity
            With Me.BudgetaryValidity
                '*********Consecutivos ingreso********'
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 208).Item4 = .ConsecutiveModPTOIncome
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 209).Item4 = .ConsecutiveTrasPTOIncome
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 211).Item4 = .ConsecutiveModPACIncome
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 212).Item4 = .ConsecutiveTrasPACIncome
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 213).Item4 = .ConsecutiveRecognition
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 214).Item4 = .ConsecutiveModRecognition
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 215).Item4 = .ConsecutiveCollection
                TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 216).Item4 = .ConsecutiveModCollection
                '*********Consecutivos gasto********'
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 208).Item4 = .ConsecutiveModPTOExpense
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 209).Item4 = .ConsecutiveTrasPTOExpense
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 211).Item4 = .ConsecutiveModPACExpense
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 212).Item4 = .ConsecutiveTrasPACExpense
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 228).Item4 = .ConsecutiveCDP
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 229).Item4 = .ConsecutiveModCDP
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 231).Item4 = .ConsecutiveRP
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 232).Item4 = .ConsecutiveModRP
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 234).Item4 = .ConsecutiveLiabilities
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 235).Item4 = .ConsecutiveModLiabilities
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 237).Item4 = .ConsecutiveODP
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 230).Item4 = .ConsecutiveExtendedCDP
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 236).Item4 = .ConsecutiveResourceRelease
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 238).Item4 = .ConsecutiveReinstatement
                TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 224).Item4 = .ConsecutiveLiftingPTO
            End With
        End With

        If INDgleValidityStatus.EditValue = 1 Then 'si es registrada
            INDtxtYear.Enabled = True
            INDgleEarningsProcessMonth.Enabled = False
            INDgleExpenseProcessMonth.Enabled = False
            INDtxtResolutionNumber.Enabled = True
            INDtxtValue.Enabled = True
            INDgleValidityStatus.Enabled = True
            INDrgPACControl.Enabled = False
            INDrgDatesAndConsecutivesControl.Enabled = False
            INDsleLegalRepresentative.Enabled = True
            INDsleBudgetBoss.Enabled = True
            INDsleFinancialBoss.Enabled = True
        ElseIf INDgleValidityStatus.EditValue = 3 Then 'si es cerrada
            INDtxtYear.Enabled = False
            INDgleEarningsProcessMonth.Enabled = False
            INDgleExpenseProcessMonth.Enabled = False
            INDtxtResolutionNumber.Enabled = False
            INDtxtValue.Enabled = False
            INDgleValidityStatus.Enabled = False
            INDrgPACControl.Enabled = False
            INDrgDatesAndConsecutivesControl.Enabled = False
            INDsleLegalRepresentative.Enabled = True
            INDsleBudgetBoss.Enabled = True
            INDsleFinancialBoss.Enabled = True
        Else 'si es activa
            INDtxtYear.Enabled = False
            INDgleEarningsProcessMonth.Enabled = False
            INDgleExpenseProcessMonth.Enabled = False
            INDtxtResolutionNumber.Enabled = False
            INDtxtValue.Enabled = False
            INDgleValidityStatus.Enabled = True
            INDrgPACControl.Enabled = False
            INDrgDatesAndConsecutivesControl.Enabled = False
            INDsleLegalRepresentative.Enabled = False
            INDsleBudgetBoss.Enabled = False
            INDsleFinancialBoss.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Funcion Para Validar Controles antes de enviar a guardar una vigencia
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateControls() As Boolean
        ValidateControls = True
        If String.IsNullOrEmpty(INDtxtYear.Text) Or INDtxtYear.Text.Length <> 4 Then
            If INDtxtYear.EditValue < 0 Then
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemYear.Text)
                INDtxtYear.Focus()
                Return False
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemYear.Text)
                INDtxtYear.Focus()
                Return False
            End If
        End If

        If INDgleEarningsProcessMonth.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemEarningsProcessMonth.Text)
            INDgleEarningsProcessMonth.Focus()
            Return False
        End If

        If INDgleExpenseProcessMonth.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemExpenseProcessMonth.Text)
            INDgleExpenseProcessMonth.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(INDtxtResolutionNumber.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemResolutionNumber.Text)
            INDtxtResolutionNumber.Focus()
            Return False
        End If

        If String.IsNullOrEmpty(INDtxtValue.Text) Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemValue.Text)
            INDtxtValue.Focus()
            Return False
        End If

        If INDgleValidityStatus.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemValidityStatus.Text)
            INDgleValidityStatus.Focus()
            Return False
        End If

        If INDrgPACControl.SelectedIndex = -1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemPACControl.Text)
            INDrgPACControl.Focus()
            Return False
        End If

        If INDrgDatesAndConsecutivesControl.SelectedIndex = -1 Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemDatesAndConsecutivesControl.Text)
            INDrgDatesAndConsecutivesControl.Focus()
            Return False
        End If

        If INDsleLegalRepresentative.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemLegalRepresentative.Text)
            INDsleLegalRepresentative.Focus()
            Return False
        End If

        If INDsleBudgetBoss.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDlyItemBudgetBoss.Text)
            INDsleBudgetBoss.Focus()
            Return False
        End If

        If INDsleFinancialBoss.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(FaltaCampo, Incapacidades), INDsleFinancialBoss.Text)
            INDsleFinancialBoss.Focus()
            Return False
        End If
    End Function

    ''' <summary>
    ''' Adiciona una nueva vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Sub AddValidity()
        If ValidateControls() = False Then
            Exit Sub
        End If
        If INDgleValidityStatus.EditValue = 2 Then
            If Not (MessageIndigo.Show(obtenerRecurso(ComunesPreguntaConfirmar), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If
        If INDgleValidityStatus.EditValue = 3 Then
            If Not (MessageIndigo.Show(obtenerRecurso(ComunesPreguntaCerrarReg), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes) Then
                Exit Sub
            End If
        End If
        Dim newListConsecutives As Domain.Entities.TrackableCollection(Of ConsecutiveBudget) = New Domain.Entities.TrackableCollection(Of ConsecutiveBudget)
        If Me._validity Is Nothing Then
            Me.BudgetaryValidity = New BudgetaryValidity
            Dim dataSourceEarnings As List(Of BudgetAccountParameters) = INDgcEarning.DataSource
            Dim dataSourceExpense As List(Of BudgetAccountParameters) = INDgcExpense.DataSource

            'For Each itemEarnings In dataSourceEarnings
            '    Dim newConsecutive As ConsecutiveBudget = New ConsecutiveBudget() With { _
            '        .FormId = itemEarnings.Item1, _
            '        .NumberConsecutive = itemEarnings.Item4, _
            '        .ProcessType = 1}
            '    newListConsecutives.Add(newConsecutive)
            'Next

            'For Each itemExpense In dataSourceExpense
            '    Dim newConsecutive As ConsecutiveBudget = New ConsecutiveBudget() With { _
            '       .FormId = itemExpense.Item1, _
            '       .NumberConsecutive = itemExpense.Item4, _
            '       .ProcessType = 2}
            '    newListConsecutives.Add(newConsecutive)
            'Next
            'Me.BudgetaryValidity.ConsecutiveBudget = newListConsecutives
        End If
        With Me.BudgetaryValidity
            .LegalRepresentativeId = INDsleLegalRepresentative.EditValue
            .ChiefBudgetOfficerId = INDsleBudgetBoss.EditValue
            .ChiefFinancialOfficerId = INDsleFinancialBoss.EditValue
            .Year = INDtxtYear.EditValue
            .IncomeMonth = INDgleEarningsProcessMonth.EditValue
            .ExpenseMonth = INDgleExpenseProcessMonth.EditValue
            .ResolutionNumber = INDtxtResolutionNumber.Text
            .ResolutionValue = CDec(INDtxtValue.Text)
            .Status = INDgleValidityStatus.EditValue
            .PACControl = INDrgPACControl.EditValue
            .ControlDateConsecutive = INDrgDatesAndConsecutivesControl.EditValue
            '*********Consecutivos ingreso********'
            .ConsecutiveModPTOIncome = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 208).Item4
            .ConsecutiveTrasPTOIncome = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 209).Item4
            .ConsecutiveModPACIncome = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 211).Item4
            .ConsecutiveTrasPACIncome = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 212).Item4
            .ConsecutiveRecognition = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 213).Item4
            .ConsecutiveModRecognition = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 214).Item4
            .ConsecutiveCollection = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 215).Item4
            .ConsecutiveModCollection = TryCast(INDgcEarning.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 216).Item4
            '*********Consecutivos gasto********'
            .ConsecutiveModPTOExpense = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 208).Item4
            .ConsecutiveTrasPTOExpense = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 209).Item4
            .ConsecutiveModPACExpense = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 211).Item4
            .ConsecutiveTrasPACExpense = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 212).Item4
            .ConsecutiveCDP = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 228).Item4
            .ConsecutiveModCDP = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 229).Item4
            .ConsecutiveRP = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 231).Item4
            .ConsecutiveModRP = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 232).Item4
            .ConsecutiveLiabilities = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 234).Item4
            .ConsecutiveModLiabilities = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 235).Item4
            .ConsecutiveODP = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 237).Item4
            .ConsecutiveExtendedCDP = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 230).Item4
            .ConsecutiveResourceRelease = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 236).Item4
            .ConsecutiveReinstatement = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 238).Item4
            .ConsecutiveLiftingPTO = TryCast(INDgcExpense.DataSource, List(Of BudgetAccountParameters)).Find(Function(x) x.Item1 = 224).Item4
        End With
        RaiseEvent ClickAccept(Me, New EventArgs)
    End Sub

    ''' <summary>
    ''' Metodo para eliminar la vigencia
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub DeleteValidy()
        If MessageIndigo.Show(obtenerRecurso(ComunesEliminarRegistro), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            Dim index As Integer = 0
            While BudgetaryValidity.ConsecutiveBudget.Count > 0
                index = BudgetaryValidity.ConsecutiveBudget.Count - 1
                'itemUL.UnemployedLiquidationDetail.Item(indexULD).MarkAsDeleted()
                If BudgetaryValidity.ConsecutiveBudget.Item(index).ChangeTracker.State = ObjectState.Added Then
                    BudgetaryValidity.ConsecutiveBudget.Remove(BudgetaryValidity.ConsecutiveBudget.Item(index))
                Else
                    BudgetaryValidity.ConsecutiveBudget.Item(index).MarkAsDeleted()
                End If
            End While
            Me.BudgetaryValidity.MarkAsDeleted()
            'Me.DialogResult = System.Windows.Forms.DialogResult.OK

            RaiseEvent ClickAccept(Nothing, EventArgs.Empty)
        End If
    End Sub

    ''' <summary>
    ''' Inicializa los controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InizaliteControler(ByRef _validity As BudgetaryValidity, Optional lastYear As Integer = 0)
        Me.BudgetaryValidity = _validity
        INDpcAccept.Visible = False
        INDpcUpdate.Visible = False
        CleanControls()
        INDtxtYear.Focus()
    End Sub

#End Region


    

    
End Class