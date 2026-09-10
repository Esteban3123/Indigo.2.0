'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Jhossept Kevin Garay Rodriguez
' Created          : 11-12-2013
'
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports DevExpress.XtraEditors
Imports Presentation.Payroll.MVP
Imports Presentation.Common.MVP
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraEditors.ViewInfo
Imports DevExpress.XtraEditors.Drawing
Imports DevExpress.Utils.Drawing
Imports Domain.Payroll.Entities
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports DevExpress.XtraGrid.Views.Grid
Imports Presentation.Controls
Imports DevExpress.Utils.Design.DesignTimeTools
Imports System.Windows.Forms
#End Region

''' <summary>
''' Clase encargada de la logica del formulario en la capa de presentación
''' </summary>
''' <remarks></remarks>
Public Class FrmUnemployedLiquidation
    Implements IUnemployedLiquidation

#Region "Field and Globals"

    Dim ListUnemployedLiquidation As List(Of UnemployedLiquidation) = New List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Variable para almacenar liquidaciones consultadas con su estado de confirmación
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListConsultedUnemployedLiquidation As List(Of UnemployedLiquidation) = New List(Of UnemployedLiquidation)

    ''' <summary>
    ''' Variable para manejar el presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim presenter As PUnemployedLiquidation

    ''' <summary>
    ''' Variable para manejar el modelo del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim model As MUnemployedLiquidation

    ''' <summary>
    ''' Listado de los grupos checkeados para mandar a liquidar
    ''' </summary>
    ''' <remarks></remarks>
    Dim listGroupsChecks As List(Of Group)

    ''' <summary>
    ''' Variable para almacenar la liquidación de cesantías
    ''' </summary>
    ''' <remarks></remarks>
    Dim ListActionResultUnemployedLiquidation As List(Of ActionMessageResult(Of UnemployedLiquidation))

    ''' <summary>
    ''' Variable para controlar el empleado que se liquidara parcial
    ''' </summary>
    ''' <remarks></remarks>
    Dim Employee As Employee

    ''' <summary>
    ''' Variable que contiene los valores de sesion
    ''' </summary>
    ''' <remarks></remarks>
    Private Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Variable que contiene la CultureInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim ci As System.Globalization.CultureInfo = Indigo.Culture

    ''' <summary>
    ''' Variable que contiene el DateTimeFormatInfo
    ''' </summary>
    ''' <remarks></remarks>
    Dim dtfi As System.Globalization.DateTimeFormatInfo = Nothing

#End Region

#Region "Properties"

    ''' <summary>
    ''' establece el datasource de las compañias
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property datasourceCompany As DevExpress.Xpo.XPInstantFeedbackSource Implements IUnemployedLiquidation.datasourceCompany
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCompany.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' establece el datasource de los empleados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property datasourceEmployee As DevExpress.Data.Linq.LinqInstantFeedbackSource Implements IUnemployedLiquidation.datasourceEmployee
        Set(value As DevExpress.Data.Linq.LinqInstantFeedbackSource)
            INDSleEmployee.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Establece los grupos filtrados por empresa
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property datasourceGroups As List(Of Domain.Payroll.Entities.Group) Implements IUnemployedLiquidation.datasourceGroups
        Set(value As List(Of Domain.Payroll.Entities.Group))
            value = value.Where(Function(x) x.PayrollParameter.MaxPremiumByYear > 0).ToList()
            INDgcGroups.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Contiene el control del periodo o año
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property PeriodControl As ComboBoxEdit Implements IUnemployedLiquidation.PeriodControl
        Get
            Return INDCbeYear
        End Get
    End Property

    ''' <summary>
    ''' Habilita los controles si existen nominas liquidadas
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IUnemployedLiquidation.ActionsOnControls
        Set(value As Boolean)
            INDrgLiquidate.Enabled = value
            INDSleCompany.Enabled = value
            INDrgSanctionDays.Enabled = value
            INDCbeYear.Enabled = value
            INDgcGroups.Enabled = value
            INDrgLiquidate.Enabled = value
            INDrgConfirm.Enabled = value
            If value = False Then
                Dim xtraMessage As String = obtenerRecurso(NoHayNominasLiquidadas, Eform.LiquidacionCesantias)
                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, "")
            Else
                Me.BarraBotones.XtraLabelVisibility = False
            End If
        End Set
    End Property

    ''' <summary>
    ''' Esta propiedad se utiliza para Registrar en el visor de eventos
    ''' </summary>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String Implements Base.IcrudBase.Mensaje
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
    ''' Accion a realizar en los controles cuando se elige liquidación parcial 
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ActionsOnLiquidationPartial As Boolean
        Set(value As Boolean)
            INDdeUnemployedLiquidationDate.Enabled = value
            INDdeAuthorizationDate.Enabled = value
            INDmeeUnemployedRetirementReason.Enabled = value
            INDtxtResolutionNumber.Enabled = value
        End Set
    End Property

    ''' <summary>
    ''' Establece la accion a realizar cuando se liquide unas cesantías y se requiera la vista de los resultados
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property ViewResultLiquidation(ByVal actionProcess As EActionProcess) As Boolean
        Set(value As Boolean)
            If actionProcess = EActionProcess.Consult Then
                INDColState.Visible = False
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                
                ' Verificar si hay liquidaciones sin confirmar (Status = 0)
                Dim hasUnconfirmedLiquidations As Boolean = False
                If ListConsultedUnemployedLiquidation IsNot Nothing AndAlso ListConsultedUnemployedLiquidation.Count > 0 Then
                    hasUnconfirmedLiquidations = ListConsultedUnemployedLiquidation.Any(Function(x) Not x.Status)
                End If
                
                ' Si hay liquidaciones sin confirmar (Status = 0), mostrar botón de confirmar
                If hasUnconfirmedLiquidations Then
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = False
                Else
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndoAndPrint)
                End If
                'INDColMessages.OptionsColumn.AllowEdit = False
            ElseIf actionProcess = EActionProcess.Liquidate Then
                INDColState.Visible = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
                If INDrgConfirm.EditValue = True Then
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                End If
                'INDColMessages.OptionsColumn.AllowEdit = True
            End If

            If value = True Then
                INDlyGrLiquidationForm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyGrPartial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyGrYearly.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDLyGrLiquidationResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'Me.BarraBotones.PrepareToolbar(eAction.OnlyConfirmLiquidate)
                'If INDrgConfirm.EditValue = True Then
                '    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = True
                'End If
            Else
                INDLyGrLiquidationResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                INDlyGrLiquidationForm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                'Me.BarraBotones.RibbonPageProcesos.Visible = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Imprimir) = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
                If INDrgLiquidate.EditValue = False Then
                    INDlyGrYearly.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                Else
                    INDlyGrPartial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                End If
                ListActionResultUnemployedLiquidation = Nothing

            End If
        End Set
    End Property
    'Propiedad para establecer los valores de los intereses de cesantias dependiendo del combo Yes-No
    Public Property UnemployedInterestPaidWithPayroll As Boolean Implements IUnemployedLiquidation.UnemployedInterestPaidWithPayroll
        Get
            Return CType(INDgleUnemployedInterestPaidWithPayroll.EditValue, Boolean)
        End Get
        Set(value As Boolean)
            INDgleUnemployedInterestPaidWithPayroll.EditValue = value
        End Set
    End Property

#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        presenter = Nothing
        model = Nothing
        listGroupsChecks = Nothing
        ListActionResultUnemployedLiquidation = Nothing
        Employee = Nothing
        ci = Nothing
        dtfi = Nothing
    End Sub
    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub FrmUnemployedLiquidation_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        presenter = New PUnemployedLiquidation(Me)
        AsyncLoader(True)
        Await presenter.initialize()
        SetCurrencyFormat(presenter.LoadPayrollSettings().CurrencyId.Abbreviation)
        AsyncLoader(False)
        Me.BarraBotones.PrepareToolbar(eAction.OnlyLiquidate)
        Me.BarraBotones.RibbonPageRejillas.Visible = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
        'IndigoGridControl1.SetHoldSize(INDgcGroups, True)
        'IndigoGridControl1.SetHoldSize(INDgcLiquidationResult, True)
        IndigoGridControl1.SetHoldSize(INDgcMessages, True)
        '************ Obtener cultura en ejecucion*****************'
        If IsDesignMode = False Then
            dtfi = ci.DateTimeFormat
        Else
            dtfi = New Globalization.DateTimeFormatInfo
        End If

    End Sub

    ''' <summary>
    ''' Controla la accion que se realiza cuando se cambia de liquidación 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDrgLiquidate_SelectedIndexChanged(sender As Object, e As EventArgs) Handles INDrgLiquidate.SelectedIndexChanged
        If INDrgLiquidate.SelectedIndex = 1 Then 'liq parcial
            INDlyGrYearly.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyGrPartial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDliUnemployedInterestPaidWithPayroll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            CleanControlsPartial()
        ElseIf INDrgLiquidate.SelectedIndex = 0 Then 'liq anual
            INDlyGrYearly.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDlyGrPartial.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDliUnemployedInterestPaidWithPayroll.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            CleanControlsYearly()
        End If
    End Sub

    ''' <summary>
    ''' Controla las acciones que se realizan cuando se cambia las empresas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCompany.EditValueChanged
        If INDSleCompany.EditValue IsNot Nothing AndAlso CStr(INDSleCompany.EditValue) IsNot String.Empty AndAlso CInt(INDSleCompany.EditValue) > 0 Then
            AsyncLoader(True)
            Using modelGroup As New MGroups(MGroups.TAG)
                Dim listgroup As List(Of Group) = Await modelGroup.GetGroupsByCompanyId(CInt(INDSleCompany.EditValue))
                listgroup = listgroup.Where(Function(x) x.PayrollParameter.MaxPremiumByYear > 0).ToList()
                INDgcGroups.DataSource = listgroup
                'GridControl1.DataSource = listgroup
            End Using
            AsyncLoader(False)
        End If
    End Sub

    ''' <summary>
    ''' Cuando seleccionen un empleado, se caclula la fecha limite de liq de cesantías
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleEmployee_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleEmployee.EditValueChanged
        If INDSleEmployee.EditValue IsNot Nothing AndAlso CStr(INDSleEmployee.EditValue) IsNot String.Empty AndAlso CInt(INDSleEmployee.EditValue) > 0 Then
            Using modelEmployee As New MEmployee(MEmployee.TAG)
                AsyncLoader(True)
                Employee = Await modelEmployee.GetEmployeeByIdAsync(INDSleEmployee.EditValue)
                AsyncLoader(False)
                If Employee.Id > 0 Then
                    Dim _contract As Contract = Employee.Contract.Where(Function(x) x.Valid = True).FirstOrDefault()
                    If _contract IsNot Nothing AndAlso _contract.Id > 0 Then
                        ActionsOnLiquidationPartial = True
                        INDdeUnemployedLiquidationDate.Properties.MaxValue = Me.GetDateServer
                        INDdeUnemployedLiquidationDate.Properties.MinValue = New Date(DateTime.Now.Year, 1, 1)
                    Else ' no tiene contrato valido
                        ActionsOnLiquidationPartial = False
                        Mensaje(EeventViewerImages.Advertencia) = String.Format(obtenerRecurso(EmpleadoNoTieneContratoLaboral, Eform.LiquidacionCesantias), Employee.ThirdParty.Name)
                    End If
                End If
            End Using
        Else
            ActionsOnLiquidationPartial = False
        End If
    End Sub

    ''' <summary>
    ''' Evento que escribe el nombre del día el la subrejilla de los UnemployedLiquidationDetail
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvUnemployedLiquidationDetail_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs)
        If e.Column.Name = INDColULDMonth.Name Then
            If e.Value IsNot Nothing Then
                e.DisplayText = Microsoft.VisualBasic.Strings.StrConv(dtfi.GetMonthName(e.Value), Microsoft.VisualBasic.VbStrConv.ProperCase)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Evento que dibuja los estados de liquidación de cesantías en rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvLiquidationResult_CustomDrawCell(sender As Object, e As DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs) Handles INDgvLiquidationResult.CustomDrawCell
        If ListActionResultUnemployedLiquidation IsNot Nothing AndAlso ListActionResultUnemployedLiquidation.Count > 0 Then
            If e.Column.Name = INDColState.Name Then
                Dim _row As UnemployedLiquidation = INDgvLiquidationResult.GetRow(e.RowHandle)
                If _row IsNot Nothing Then
                    Dim _actRes As ActionMessageResult(Of UnemployedLiquidation) = ListActionResultUnemployedLiquidation.Find(Function(x) x.ObjectEmbbeded.EmployeeId = _row.EmployeeId)
                    If _actRes.MessageResult.Count > 0 Then
                        If _actRes.StateResult = True Then ' si liquido el empleado, con mensaje
                            Dim imagenDia = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
                            imagenDia.Image = My.Resources.amarillo_16x16
                        Else ' si no lo liquido
                            Dim imagenDia = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
                            imagenDia.Image = My.Resources.rojo_16x16
                        End If
                    Else ' si lo liquido sin mensaje
                        Dim imagenDia = CType(CType(e.Cell, GridCellInfo).ViewInfo, PictureEditViewInfo)
                        imagenDia.Image = My.Resources.verde_16x16
                    End If
                End If
            End If
        End If

    End Sub

    ''' <summary>
    ''' evento click que regresa a la forma de liquidación
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CtrNavigation1_ClickBack() Handles CtrNavigation1.ClickBack
        ViewResultLiquidation(EActionProcess.Liquidate) = False
        ' Limpiar lista de liquidaciones consultadas
        ListConsultedUnemployedLiquidation = New List(Of UnemployedLiquidation)
        AddHandler INDgvLiquidationResult.CustomDrawCell, AddressOf INDgvLiquidationResult_CustomDrawCell
    End Sub

    ''' <summary>
    ''' Evento para llenar el datasource de mensajes en el popup que se va a abrir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Private Sub INDRepPopupConEdit_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDRepPopupConEdit.QueryPopUp
    '    INDgcMessages.DataSource = Nothing
    '    Dim listMessageResult As List(Of MessageResult) = New List(Of MessageResult)
    '    Dim _row As UnemployedLiquidation = INDgvLiquidationResult.GetFocusedRow()
    '    If _row IsNot Nothing Then
    '        If ListActionResultUnemployedLiquidation IsNot Nothing AndAlso ListActionResultUnemployedLiquidation.Count > 0 Then
    '            Dim _actRes As ActionMessageResult(Of UnemployedLiquidation) = ListActionResultUnemployedLiquidation.Find(Function(x) x.ObjectEmbbeded.EmployeeId = _row.EmployeeId)
    '            If _actRes.MessageResult.Count > 0 Then
    '                For Each itemMess In _actRes.MessageResult
    '                    listMessageResult.Add(itemMess)
    '                Next
    '            End If
    '        End If
    '    End If
    '    If _row.UnemployedLiquidationType = True Then
    '        listMessageResult.Add(New MessageResult(INDlyItemAuthorizationDate.Text & " : " & _row.AuthorizationDate))
    '        listMessageResult.Add(New MessageResult(INDlyUnemployedRetirementReason.Text & " : " & _row.UnemployedRetirementReason))
    '        listMessageResult.Add(New MessageResult(INDlyItemResosultionNumber.Text & " : " & _row.ResolutionNumber))
    '    End If
    '    If listMessageResult Is Nothing Then
    '        e.Cancel = True
    '    Else
    '        INDgcMessages.DataSource = listMessageResult
    '    End If
    'End Sub

    ''' <summary>
    ''' Evento que escribe los mensajes en la rejilla según su código
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvMessages_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDgvMessages.CustomColumnDisplayText
        Dim _row As MessageResult = INDgvMessages.GetRow(e.ListSourceRowIndex)

        If _row IsNot Nothing Then
            If e.Column.Name = INDColMessage.Name Then
                If GetStringMessage(_row.CodeMessage, _row.Parameters) <> "" Then
                    e.DisplayText = GetStringMessage(_row.CodeMessage, _row.Parameters)
                End If
            End If
        End If

    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControls()
        INDrgLiquidate.SelectedIndex = 0
        INDrgSanctionDays.SelectedIndex = 0
        INDSleCompany.EditValue = Nothing
        INDCbeYear.SelectedIndex = -1
        INDSleEmployee.EditValue = Nothing
        INDdeUnemployedLiquidationDate.EditValue = Nothing
        INDdeUnemployedLiquidationDate.EditValue = Nothing
        INDmeeUnemployedRetirementReason.EditValue = Nothing
        INDtxtResolutionNumber.EditValue = Nothing
        INDgcGroups.DataSource = Nothing
        INDgleUnemployedInterestPaidWithPayroll.EditValue = 0
        ListConsultedUnemployedLiquidation = New List(Of UnemployedLiquidation)
        Me.presenter.LoadYears()
        ViewResultLiquidation(EActionProcess.Liquidate) = False
        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles cuando se escoge liq anual
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControlsYearly()
        INDSleCompany.EditValue = Nothing
        INDCbeYear.SelectedIndex = -1
        INDgcGroups.DataSource = Nothing
        Me.presenter.LoadYears()
    End Sub

    ''' <summary>
    ''' Metodo para limpiar controles cuando se escoge liq parcial
    ''' </summary>
    ''' <remarks></remarks>
    Sub CleanControlsPartial()
        RemoveHandler INDSleEmployee.EditValueChanged, AddressOf INDSleEmployee_EditValueChanged
        INDSleEmployee.EditValue = Nothing
        AddHandler INDSleEmployee.EditValueChanged, AddressOf INDSleEmployee_EditValueChanged
        INDdeUnemployedLiquidationDate.EditValue = Nothing
        INDdeUnemployedLiquidationDate.EditValue = Nothing
        INDmeeUnemployedRetirementReason.EditValue = Nothing
        INDtxtResolutionNumber.EditValue = Nothing
        ActionsOnLiquidationPartial = False
    End Sub

    ''' <summary>
    ''' Funcion para validar campos al liquidar de forma anual
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidateYearlyLiquidation() As Boolean
        If INDCbeYear.EditValue Is Nothing OrElse INDCbeYear.EditValue Is String.Empty Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesSeleccioneAño)
            INDCbeYear.Focus()
            Return False
        End If
        If INDgcGroups.DataSource Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionados, Eform.LiquidacionCesantias)
            Return False
        End If
        listGroupsChecks = CType(INDgcGroups.DataSource, List(Of Group)).FindAll(Function(x) x.Apply = True) ' obtengo listado de los grupos checkeados
        If listGroupsChecks.Count <= 0 Then
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionados, Eform.LiquidacionCesantias)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' Metodo para validar campos al liquidar de forma parcial
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ValidatePartialLiquidation() As Boolean
        If INDSleEmployee.EditValue Is Nothing Then
            INDSleEmployee.Focus()
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Return False
        ElseIf INDdeUnemployedLiquidationDate.EditValue Is Nothing Then
            INDdeUnemployedLiquidationDate.Focus()
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Return False
        ElseIf INDmeeUnemployedRetirementReason.EditValue Is Nothing Or INDmeeUnemployedRetirementReason.Text = "" Then
            INDmeeUnemployedRetirementReason.Focus()
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Return False

        ElseIf INDdeAuthorizationDate.EditValue Is Nothing Then
            INDdeAuthorizationDate.Focus()
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Return False
        ElseIf INDtxtResolutionNumber.Text = "" Then
            INDtxtResolutionNumber.Focus()
            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
            Return False
        Else
            Return True
        End If
    End Function

    ''' <summary>
    ''' Metodo que liquida las cesantías
    ''' </summary>
    ''' <remarks></remarks>
    Async Function UnemployeeLiquidate() As Task
        ListUnemployedLiquidation = New List(Of UnemployedLiquidation)
        Try


            ListActionResultUnemployedLiquidation = Nothing
            If INDrgLiquidate.SelectedIndex = 0 Then ' liq anual
                If ValidateYearlyLiquidation() = True Then 'valido que se pueda enviar a liquidar total
                    Using model = New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
                        AsyncLoader(True)
                        Dim result = Await model.UnemployedLiquidationAsync(Nothing, listGroupsChecks, New Date(INDCbeYear.EditValue, 1, 1), New Date(INDCbeYear.EditValue, 12, 31), INDrgConfirm.EditValue, INDrgSanctionDays.EditValue, Nothing, "", "", UnemployedInterestPaidWithPayroll)
                        AsyncLoader(False)
                        If result.StateResult = True Then

                            For Each item In result.ObjectEmbbeded
                                ListUnemployedLiquidation.Add(item)
                            Next
                            If ListUnemployedLiquidation.Count > 0 Then
                                OpenFormAddAssets(False)
                            End If
                            If INDrgConfirm.EditValue = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(CesantiasConfirmadasCorrectamente, Eform.LiquidacionCesantias)
                            End If
                        Else

                            If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                                TmpListEmployeeMessage = MessageLiquidation(result.MessageResult)
                                Dim frmMessage As FrmLiquidationMessage = New FrmLiquidationMessage()
                                frmMessage.StartPosition = FormStartPosition.CenterScreen
                                frmMessage.INDGcMessage.DataSource = TmpListEmployeeMessage
                                Dim frmTransparent As New FrmTransparent(frmMessage, False)
                                frmTransparent.ShowDialog()
                            End If

                            If result.ObjectEmbbeded IsNot Nothing AndAlso result.ObjectEmbbeded.Count > 0 Then

                                For Each item In result.ObjectEmbbeded
                                    ListUnemployedLiquidation.Add(item)
                                Next
                                If ListUnemployedLiquidation.Count > 0 Then
                                    OpenFormAddAssets(False)
                                End If
                            End If
                        End If

                    End Using
                End If
            ElseIf INDrgLiquidate.SelectedIndex = 1 Then ' liq parcial
                If ValidatePartialLiquidation() = True Then 'valido que se pueda enviar a liquidar parcial
                    Using model = New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
                        AsyncLoader(True)
                        Dim result = Await model.UnemployedLiquidationAsync(Employee, Nothing, New Date(DateTime.Now().Year, 1, 1), INDdeUnemployedLiquidationDate.EditValue, INDrgConfirm.EditValue, INDrgSanctionDays.EditValue, INDdeAuthorizationDate.EditValue, INDmeeUnemployedRetirementReason.Text, INDtxtResolutionNumber.Text)
                        AsyncLoader(False)
                        If result.StateResult = True Then

                            For Each item In result.ObjectEmbbeded
                                ListUnemployedLiquidation.Add(item)
                            Next
                            If ListUnemployedLiquidation.Count > 0 Then
                                OpenFormAddAssets(False)
                            End If
                            If INDrgConfirm.EditValue = True Then
                                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(CesantiasConfirmadasCorrectamente, Eform.LiquidacionCesantias)
                            End If
                        Else
                            If INDrgConfirm.EditValue = False Then
                                'mensaje de que no se liquido nada
                                Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoseLiquidaronCesantias, Eform.LiquidacionCesantias)
                            Else
                                'mensaje de que no se pudo confirmar las cesantias
                                Mensaje(EeventViewerImages.MensajeError) = obtenerRecurso(CesantiasNoSePudieronConfirmar, Eform.LiquidacionCesantias)
                            End If
                        End If
                    End Using
                End If
            End If
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = ex.Message.ToString()
        End Try
    End Function

    ''' <summary>
    ''' Metodo para confirmar las cesantías desde la vista de consulta
    ''' </summary>
    ''' <remarks></remarks>
    Async Function ConfirmFromConsultation() As Task
        Try
            ' Filtrar solo liquidaciones sin confirmar (Status = 0)
            Dim unconfirmedLiquidations = ListConsultedUnemployedLiquidation.Where(Function(x) Not x.Status).ToList()
            
            If unconfirmedLiquidations Is Nothing OrElse unconfirmedLiquidations.Count = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "No hay liquidaciones sin confirmar."
                Return
            End If


            Dim confirmMessage = String.Format("¿Está seguro de confirmar {0} liquidación(es) de cesantías?" & vbCrLf &
                                               "Esta acción generará los comprobantes contables.",
                                               unconfirmedLiquidations.Count)

            If MessageIndigo.Show(confirmMessage, MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Using model As New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
                    AsyncLoader(True)

                    Dim firstLiquidation = unconfirmedLiquidations.FirstOrDefault()
                    Dim ListUnemployementToSend = New List(Of UnemployedLiquidation)
                    ListUnemployementToSend.Add(New UnemployedLiquidation With {
                        .GroupId = firstLiquidation.GroupId,
                        .UnemployedInitialDate = firstLiquidation.UnemployedInitialDate,
                        .UnemployedEndingDate = firstLiquidation.UnemployedEndingDate
                    })

                    Dim result = Await model.ConfirmUnemployment(unconfirmedLiquidations)
                    AsyncLoader(False)
                    
                    If result.StateResult Then
                        Mensaje(EeventViewerImages.Informacion) = "Cesantías confirmadas exitosamente." & vbCrLf & result.Message

                        Await ConsultUnemployedLiquidation()
                    Else
                        Dim errorMessage = "Error al confirmar cesantías:"
                        If result.MessageResult IsNot Nothing AndAlso result.MessageResult.Count > 0 Then
                            errorMessage = errorMessage & vbCrLf & String.Join(vbCrLf, result.MessageResult)
                        End If
                        Mensaje(EeventViewerImages.MensajeError) = errorMessage
                    End If
                End Using
            End If
            
        Catch ex As Exception
            AsyncLoader(False)
            Mensaje(EeventViewerImages.MensajeError) = "Error al confirmar: " & ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Metodo para consultar la liquidación de cesantías, segun la información suministrada en el formulario
    ''' </summary>
    ''' <remarks></remarks>
    Async Function ConsultUnemployedLiquidation() As Task
        Using model As New MUnemployedLiquidation(MUnemployedLiquidation.TAG)
            If INDrgLiquidate.SelectedIndex = 0 Then ' liq anual
                If INDgcGroups.DataSource Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionados, Eform.LiquidacionCesantias)
                Else
                    listGroupsChecks = CType(INDgcGroups.DataSource, List(Of Group)).FindAll(Function(x) x.Apply = True) ' obtengo listado de los grupos checkeados
                    If listGroupsChecks.Count > 0 Then
                        AsyncLoader(True)
                        Dim result = Await model.ConsultUnemployedLiquidationAsync(Nothing, listGroupsChecks, INDCbeYear.EditValue)
                        AsyncLoader(False)
                        If result.Count > 0 Then
                            'RemoveHandler INDgvLiquidationResult.CustomDrawCell, AddressOf INDgvLiquidationResult_CustomDrawCell
                            ' Guardar las liquidaciones consultadas para verificar estado
                            ListConsultedUnemployedLiquidation = result
                            ViewResultLiquidation(EActionProcess.Consult) = True
                            INDgcLiquidationResult.DataSource = result
                            INDgvLiquidationResult.BestFitColumns()
                            INDgvUnemployedLiquidationDetail.BestFitColumns()

                            Me.BarraBotones.PrintReport(PrintReportAction.None, 1, 0, listGroupsChecks, INDCbeYear.EditValue, Me.BarraBotones.OperatingUnit)
                        Else
                            Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NohayCesantiasLiquidadas, Eform.LiquidacionCesantias)
                        End If
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NoHayGruposSeleccionados, Eform.LiquidacionCesantias)
                    End If
                End If
            ElseIf INDrgLiquidate.SelectedIndex = 1 Then 'liq parcial
                If INDSleEmployee.EditValue IsNot Nothing AndAlso Employee IsNot Nothing Then
                    AsyncLoader(True)
                    Dim result = Await model.ConsultUnemployedLiquidationAsync(Employee, Nothing)
                    AsyncLoader(False)
                    If result.Count > 0 Then
                        'RemoveHandler INDgvLiquidationResult.CustomDrawCell, AddressOf INDgvLiquidationResult_CustomDrawCell
                        ' Guardar las liquidaciones consultadas para verificar estado
                        ListConsultedUnemployedLiquidation = result
                        ViewResultLiquidation(EActionProcess.Consult) = True
                        INDgcLiquidationResult.DataSource = result
                        INDgvLiquidationResult.BestFitColumns()
                        INDgvUnemployedLiquidationDetail.BestFitColumns()
                    Else
                        Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(NohayCesantiasLiquidadas, Eform.LiquidacionCesantias)
                    End If
                Else
                    Mensaje(EeventViewerImages.Advertencia) = obtenerRecurso(ComunesFormIncompleto)
                    INDSleEmployee.Focus()
                End If
            End If
        End Using
    End Function

    ''' <summary>
    ''' Obtiene el string del mensaje de error que contiene un empleado
    ''' </summary>
    ''' <param name="ErrorCode"></param>
    ''' <param name="parameters"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStringMessage(ErrorCode As String, ByVal ParamArray parameters() As String) As String
        Select Case ErrorCode
            Case Is = "-001"
                Return String.Format(obtenerRecurso(EmpleadoNoTieneContrato, Eform.LiquidacionCesantias), parameters(0))
            Case Is = "-002"
                Return String.Format(obtenerRecurso(EmpleadoNoTieneFondoDeCesantia, Eform.LiquidacionCesantias), parameters(0))
            Case Is = "-003"
                Return String.Format(obtenerRecurso(EmpleadoNoTieneContratoLaboral, Eform.LiquidacionCesantias), parameters(0))
            Case Is = "-004"
                Return String.Format(obtenerRecurso(EmpleadoYaTieneCesantiaAnualLiquidada, Eform.LiquidacionCesantias), parameters(0), parameters(1))
            Case Is = "-005"
                Return String.Format(obtenerRecurso(EmpleadoYaTieneCesantiasLiquidadas, Eform.LiquidacionCesantias), parameters(0), parameters(1), parameters(2))
            Case Is = "-006"
                Return String.Format(obtenerRecurso(EmpleadoNoTieneNominasLiquidadas, Eform.LiquidacionCesantias), parameters(0), parameters(1), parameters(2))
            Case Else
                Return ""
        End Select
    End Function

    Private Sub OpenFormAddAssets(FlagSearch As Boolean)
        Using formulario As New FrmUnemployementDetail
            Me.Cursor = ChangeCursorIndigo()
            'AddHandler formulario.AddFixedAssetActiveOutputDetailEventArgs, AddressOf ReturnAddEventArgs
            formulario.ListUnemployement = ListUnemployedLiquidation
            formulario.Size = New System.Drawing.Size(1200, 696)
            formulario.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            formulario.FlagSearch = FlagSearch
            formulario.FlagParcial = INDrgLiquidate.SelectedIndex
            formulario.SanctionDays = INDrgSanctionDays.EditValue
            formulario.UnemployedLiquidationDate = INDdeUnemployedLiquidationDate.EditValue
            formulario.Employee = Employee
            formulario.AutorizationDatte = INDdeAuthorizationDate.EditValue
            formulario.ResolutionNumber = INDtxtResolutionNumber.Text
            formulario.RetirementReason = INDmeeUnemployedRetirementReason.Text
            formulario.listGroupsChecks = listGroupsChecks
            Dim transparent = New Base.FrmTransparent(formulario, False)
            Me.Cursor = System.Windows.Forms.Cursors.Default
            transparent.ShowDialog(Me)
        End Using
    End Sub

    Private Function MessageLiquidation(ByVal ListMessageResult As List(Of MessageResult)) As List(Of EmployeeMessage)

        Dim ListEmployeeMessage As New List(Of EmployeeMessage)

        For Each ObjMessage As MessageResult In ListMessageResult
            Dim ObjEmployeeMessage As New EmployeeMessage

            ObjEmployeeMessage.CodeError = ObjMessage.CodeMessage

            If ObjMessage.CodeMessage = "001: Empleado" Or ObjMessage.CodeMessage = "999: Parámetros de Nómina" Or ObjMessage.CodeMessage = "-999: Error" Then
                ObjEmployeeMessage.TypeMessage = "1"
            ElseIf ObjMessage.CodeMessage = "002: Cesantias" Or ObjMessage.CodeMessage = "003: Nomina" Then
                ObjEmployeeMessage.TypeMessage = "3"
            Else
                ObjEmployeeMessage.TypeMessage = "2"
            End If

            ObjEmployeeMessage.ErrorMessage = ObjMessage.Parameters(0)

            ListEmployeeMessage.Add(ObjEmployeeMessage)
        Next

        Return ListEmployeeMessage.GroupBy(Function(x) x.ErrorMessage).Select(Function(x) x.First).ToList()
    End Function

    ''' <summary>
    ''' Establece a  los controles la moneda parametrizada
    ''' </summary>
    ''' <param name="_currencyAbbreviation"></param>
    Private Sub SetCurrencyFormat(_currencyAbbreviation As String)
        If String.IsNullOrEmpty(_currencyAbbreviation) Then
            Mensaje(EeventViewerImages.Advertencia) = "La abreviación de la moneda está vacía."
            Exit Sub
        End If
        Dim numberFormat = _currencyAbbreviation.GetNumberFormat()
        changeNumericFormatByCurrency(numberFormat)
        INDColTotalUnemployed = Window.Utils.FormatGrid(INDColTotalUnemployed, _currencyAbbreviation)
        INDColUnemployedInterestTotal = Window.Utils.FormatGrid(INDColTotalUnemployed, _currencyAbbreviation)
        INDColULDUnemployedAverage = Window.Utils.FormatGrid(INDColULDUnemployedAverage, _currencyAbbreviation)
        INDColULDUnemployedInterestAverage = Window.Utils.FormatGrid(INDColULDUnemployedInterestAverage, _currencyAbbreviation)
    End Sub
#End Region

#Region "ICRUD BASE"
    Public Sub AbrirBusqueda() Implements IcrudBase.OpenSearch

    End Sub

    Public Sub Buscar() Implements IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements IcrudBase.Deshacer
        CleanControls()
    End Sub

    Public Sub Eliminar() Implements IcrudBase.Eliminar

    End Sub

    Public Sub Guardar() Implements IcrudBase.Guardar

    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

    Public Sub Nuevo() Implements IcrudBase.Nuevo

    End Sub
#End Region

#Region "Bar buttons events"

    ''' <summary>
    '''Evento load de la barra de usuarios.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(CStr(MyBase.Tag))
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
        CleanControls()
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
        Guardar()
    End Sub

    ''' <summary>
    ''' Click liquidar del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickLiquidar() Handles BarraBotones.ClickLiquidar
        Await UnemployeeLiquidate()
    End Sub

    ''' <summary>
    ''' Click consultar liquidacion del barrabotones
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConsultLiquidar() Handles BarraBotones.ClickConsultLiquidar
        Await ConsultUnemployedLiquidation()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        CleanControls()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click customizar.
    ''' </summary>
    Private Sub BarraBotones_ClickCustomizar() Handles BarraBotones.ClickCustomizar
        'CustomizationOpen()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ clic restablecer layout.
    ''' </summary>
    Private Sub BarraBotones_ClicRestablecerLayout() Handles BarraBotones.ClicRestablecerLayout
        'ResetLayout()
    End Sub

    ''' <summary>
    ''' Barras de botones click confirmar
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_ClickConfirmar() Handles BarraBotones.ClickConfirmLiquidation
        ' Verificar si estamos en modo consulta o liquidación
        If INDLyGrLiquidationResult.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always AndAlso INDlyGrLiquidationForm.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never Then
            ' Estamos en modo consulta, confirmar las liquidaciones consultadas
            Await ConfirmFromConsultation()
        Else
            ' Estamos en modo liquidación normal
            If MessageIndigo.Show(String.Format(obtenerRecurso(ConfirmarCesantias, Eform.LiquidacionCesantias)), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                INDrgConfirm.EditValue = True
                Await UnemployeeLiquidate()
                INDrgConfirm.EditValue = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Imprimir Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickImprimir() Handles BarraBotones.ClickImprimir
        Me.BarraBotones.PrintReport(PrintReportAction.DirectPrinting, 1, 0, listGroupsChecks, INDCbeYear.EditValue, Me.BarraBotones.OperatingUnit)
    End Sub

#End Region

End Class

#Region "Enums"
''' <summary>
''' enumeracion para controlar la accion de proceso que se realiza
''' </summary>
''' <remarks></remarks>
Public Enum EActionProcess
    ''' <summary>
    ''' Consuktar
    ''' </summary>
    ''' <remarks></remarks>
    Consult = 1
    ''' <summary>
    ''' Liquidar
    ''' </summary>
    ''' <remarks></remarks>
    Liquidate = 2
End Enum
#End Region