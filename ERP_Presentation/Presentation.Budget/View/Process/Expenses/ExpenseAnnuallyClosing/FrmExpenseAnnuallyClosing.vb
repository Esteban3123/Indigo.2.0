#Region "Imports"

Imports System.Text
Imports DevExpress.Xpo
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Budget.MVP

#End Region

Public Class FrmExpenseAnnuallyClosing
    Implements IExpenseAnnuallyClosing

#Region "Variables"
    ''' <summary>
    ''' Variable apra el presentador
    ''' </summary>
    Dim _presenter As PExpenseAnnuallyClosing
    ''' <summary>
    ''' Variable para la entidad BudgetaryValidity
    ''' </summary>
    Dim _validity As BudgetaryValidity

#End Region

#Region "Properties"
    ''' <summary>
    ''' Devuelve el tag
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property MyTag As Object Implements IExpenseAnnuallyClosing.MyTag
        Get
            Return Me.Tag
        End Get
    End Property
    ''' <summary>
    ''' Establece o retorna el id de entidades presupuestales
    ''' </summary>
    ''' <returns></returns>

    Public Property BudgetEntitiesId As Integer
        Get
            Return INDSleEntity.EditValue
        End Get
        Set(value As Integer)
            INDSleEntity.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Establece o retorna el id de las vigencias presupuestales
    ''' </summary>
    ''' <returns></returns>

    Public Property BudgetaryValidityId As Integer
        Get
            Return INDSleValidity.EditValue
        End Get
        Set(value As Integer)
            INDSleValidity.EditValue = value
        End Set
    End Property

#End Region

#Region "Datasources"
    ''' <summary>
    ''' Propiedad para el dataSource de BudgetEntities
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetEntitiesXpo As XPInstantFeedbackSource Implements IExpenseAnnuallyClosing.BudgetEntitiesXpo
        Get
            Return INDSleEntity.Properties.DataSource
        End Get
        Set(value As XPInstantFeedbackSource)
            INDSleEntity.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Prpiedad para el datasource de Vigencias Presupuestales
    ''' </summary>
    ''' <returns></returns>
    Public Property BudgetaryValidityXpo As XPCollection Implements IExpenseAnnuallyClosing.BudgetaryValidityXpo
        Get
            Return INDSleValidity.Properties.DataSource
        End Get
        Set(value As XPCollection)
            INDSleValidity.Properties.DataSource = value
        End Set
    End Property

#End Region

#Region "ICrud"
    ''' <summary>
    ''' Metodos crud de IcrudBase sin usarse
    ''' </summary>
    Public Sub Buscar() Implements ICrudBase.Buscar
    End Sub

    Public Sub Deshacer() Implements ICrudBase.Deshacer
    End Sub

    Public Sub Eliminar() Implements ICrudBase.Eliminar
    End Sub

    Public Sub Guardar() Implements ICrudBase.Guardar
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub
    ''' <summary>
    ''' Propiedad para mostrar elmensaje en el visor
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
    ''' Metodo Nuevo sin usarse
    ''' </summary>
    Public Sub Nuevo() Implements ICrudBase.Nuevo
    End Sub
    ''' <summary>
    ''' Metodo para abrir busqueda sin usarse
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
    End Sub

#End Region

#Region "Methods"
    ''' <summary>
    ''' Limpia los controles 
    ''' </summary>

    Private Sub CleanControls()
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        _validity = Nothing
        INDSleEntity.EditValue = Nothing
        INDSleValidity.EditValue = Nothing
        INDSleEntity.Focus()
    End Sub

    ''' <summary>
    ''' Función para validar controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateDetail() As Boolean
        Dim listErrors As New StringBuilder

        If _validity.ExpenseMonth < 13 Then
            listErrors.AppendLine("Debe realizar primero todos los cierres mensuales.")
        End If

        If listErrors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = listErrors.ToString
            Return False
        End If

        Return True
    End Function

#End Region

#Region "Events"

#Region "Load"
    ''' <summary>
    ''' Carga el frm FrmExpenseAnnuallyClosing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmExpenseAnnuallyClosing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _presenter = New PExpenseAnnuallyClosing(Me)
    End Sub
    ''' <summary>
    ''' Metodo para liberar memoria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _presenter = Nothing
        _validity = Nothing
    End Sub

#End Region

#Region "Shown"
    ''' <summary>
    ''' Metodo para mostrar enfocat
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmExpenseAnnuallyClosing_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleEntity.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    Private Sub INDsleEntity_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDSleEntity.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm("200", INDSleEntity.EditValue, True)
        End If
    End Sub

#End Region

#Region "QueryPopUp"

    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDSleEntity.QueryPopUp
        If BudgetEntitiesXpo Is Nothing Then
            _presenter.InitializeBudgetEntity()
        End If
    End Sub

#End Region

#Region "EditValueChanged"

    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleEntity.EditValueChanged
        If BudgetEntitiesId <> Nothing AndAlso BudgetEntitiesId <> 0 Then
            BudgetaryValidityXpo = Nothing
            _presenter.InitializeValidity(BudgetEntitiesId)
        End If
    End Sub

    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleValidity.EditValueChanged
        If INDSleValidity.EditValue IsNot Nothing Then
            Using model As New MAnnuallyClosingIncome(MyTag)
                _validity = model.GetValidity(INDsleValidity.EditValue)
                If _validity.ExpenseMonth = 13 Then
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
    ''' Carga la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Anular) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Nuevo) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
    End Sub
    ''' <summary>
    ''' Evento al dar click en deshacer 
    ''' 
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        CleanControls()
    End Sub
    ''' <summary>
    ''' Evento al confirmar
    ''' </summary>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmar
        If Not ValidateDetail() Then
            Exit Sub
        End If

        Try
            Using model As New MAvailability(MyTag)
                AsyncLoader(True)
                Dim result = Await model.CloseYear(_validity, 2)
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