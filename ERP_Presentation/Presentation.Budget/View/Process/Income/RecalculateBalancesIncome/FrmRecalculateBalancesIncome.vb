#Region "Imports"

Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Budget.MVP

#End Region

Public Class FrmRecalculateBalancesIncome
#Region "Variables"

    Dim _validity As BudgetaryValidity

#End Region

#Region "Properties"
    ''' <summary>
    ''' Obtiene el tag del frm
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Obtiene o establece el id de las entidades
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesId As Integer
        Get
            Return INDsleEntity.EditValue
        End Get
        Set(value As Integer)
            INDsleEntity.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de vigencias
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"
    ''' <summary>
    ''' Obtiene o establece el datasource de entidades
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesXpo As XPInstantFeedbackSource
        Get
            Return INDsleEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleEntity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el datasource de vigencias
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityXpo As XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"
    ''' <summary>
    ''' Propiedad para mostar los mensajes en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
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

#End Region

#Region "Methods"
    ''' <summary>
    ''' Limpia los controles
    ''' </summary>
    Private Sub CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        _validity = Nothing
        INDsleEntity.EditValue = Nothing
        INDsleValidity.EditValue = Nothing
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Libera memoria al cerrar el frmm
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _validity = Nothing
    End Sub

#End Region

#Region "Shown"
    ''' <summary>
    ''' Enfoca el campo de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmExpenseRecalculateBalances_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "ButtonClick"
    ''' <summary>
    ''' Abre el frm de entidades presupuestales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDsleEntity.EditValue, True)
        End If
    End Sub

#End Region

#Region "QueryPopUp"
    ''' <summary>
    ''' Muestra el popup de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If INDsleEntity.Properties.DataSource Is Nothing Then
            INDsleEntity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' Evento al editar el valor de entidades
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        INDsleValidity.Properties.DataSource = Nothing
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            INDsleValidity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(BudgetEntitiesId)
        End If
    End Sub
    ''' <summary>
    ''' Evento al editar el valor de vigencias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            Using model As New MAnnuallyClosingIncome(MyTag)
                _validity = model.GetValidity(INDsleValidity.EditValue)
                If _validity.ExpenseMonth < 13 Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
                Else
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                End If
            End Using
        End If
    End Sub

#End Region

#End Region

#Region "Buttons Bar"
    ''' <summary>
    ''' Evento que carga la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.PrepareToolbar(Presentation.Controls.eAction.None)
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
        Me.BarraBotones.ChangeButtonName(EbuttonsWithoutPermission.Confirmar, "Procesar")
    End Sub
    ''' <summary>
    ''' Click en deshacer
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
    ''' <summary>
    ''' Click confirmar
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        Try
            Using model As New MAvailability(MyTag)
                AsyncLoader(True)
                Dim result = Await model.RecalculateBalances(_validity, 1)
                AsyncLoader(False)
                If result.StateResult Then
                    Mensaje(EeventViewerImages.Informacion) = result.Message
                    CleanControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = result.Message
                End If
            End Using
        Catch ex As Exception
            Throw ex
            AsyncLoader(False)
        End Try
    End Sub

#End Region

End Class