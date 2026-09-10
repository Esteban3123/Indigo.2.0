'***********************************************************************
' Assembly         : Presentacion.MixingStation
' Author           : Ruben Dario Castañeda Giraldo
' Created          : 05-08-2019
'
' Last Modified By :
' Last Modified On :
' Description      :
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports System.ComponentModel
Imports DevExpress.Xpo
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.MixingStationRepository
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Presentation.MixingStation.MVP

#End Region

Public Class FrmProductionLine
    Implements IProductionLine

#Region "Fields"

    ''' <summary>
    ''' Nombre del módulo al que pertenece el frontal
    ''' </summary>
    Private Const NAME_MODULE As String = "MixingStation"

    ''' <summary>
    ''' Variable que contiene la lista de las clases para las unidades unitarias
    ''' </summary>
    Private ListFuctionalUnit As New List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Representa la entidad de lineas de produccion
    ''' </summary>
    Private _productionLineEntity As ProductionLine
    ''' <summary>
    ''' Representa la entidad de Tipo de Dosis Unitaria
    ''' </summary>
    Private _productionLineUnitDoseTypeEntity As ProductionLineUnitDoseType

    ''' <summary>
    ''' Referencia la presentador
    ''' </summary>
    Private _presenter As PProductionLine

    ''' <summary>
    ''' Secuencia numerica del formulario
    ''' </summary>
    Private _sequence As MixingStationSequence

    ''' <summary>
    ''' Id de la unidad operativa seleccionada
    ''' </summary>
    Private _idOperativeUnit As Int32

    ''' <summary>
    ''' Id de la configuración de secuencia seleccionada
    ''' </summary>
    Private _idCurrentSequence As Int64

    ''' <summary>
    ''' Objeto registro bloqueado
    ''' </summary>
    Private _record As BlockRecordMixingStation

    ''' <summary>
    ''' Listado de dosis unitaria
    ''' </summary>
    ''' <remarks></remarks>
    Private ListProductionLineUnitDoseType As List(Of ProductionLineUnitDoseType)

    ''' <summary>
    ''' Listado de eliminados dosis unitaria
    ''' </summary>
    ''' <remarks></remarks>
    Private ListDeleteProductionLineUnitDoseType As List(Of ProductionLineUnitDoseType)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <returns></returns>
    Property ListProductionLineScheduleDelete As List(Of ProductionLineSchedule)

    ''' <summary>
    ''' Variable que contiene un ítem de horario
    ''' </summary>
    ''' <remarks></remarks>
    Private _productionLineSchedule As ProductionLineSchedule

    ''' <summary>
    ''' </summary>
    ''' <returns></returns>
    Property ListScheduleExceptionDelete As List(Of ProductionLineScheduleException)

    Dim Id_ProductionLine As Integer
#End Region

#Region "Propierties IProductionLine"
    ''' <summary>
    ''' Obtiene o establece el codigo del turno
    ''' </summary>
    Public Property Code As String Implements IProductionLine.Code
        Get
            If (INDbtnCode.Text.Trim().Equals(ResourceManager.GetString("LabelOrTextboxNew"))) Then
                Return String.Empty
            Else
                Return INDbtnCode.Text
            End If
        End Get
        Set(value As String)
            INDbtnCode.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el estado
    ''' </summary>
    ''' <value></value>
    Public Property State As Boolean Implements IProductionLine.State
        Get
            Return CBool(BarraBotones.StatusRecord)
        End Get
        Set(value As Boolean)
            If value = True Then
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Active
            Else
                Me.BarraBotones.StatusRecord = eActionsStatusRecords.Inactive
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la descripción del turno
    ''' </summary>
    ''' <value></value>
    Public Property Name As String Implements IProductionLine.Name
        Get
            Return INDtxtNombre.Text
        End Get
        Set(value As String)
            INDtxtNombre.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o asigna la secuencia numerica del formulario
    ''' </summary>
    ''' <value></value>
    Public Property Sequence As MixingStationSequence Implements IProductionLine.Sequence
        Get
            Return Me._sequence
        End Get
        Set(value As MixingStationSequence)
            Me._sequence = value
            If value IsNot Nothing AndAlso value.Id > 0 AndAlso Not value.IsManual AndAlso Not value.Sequential Then
                Me.DicSequense.Clear()
                For Each seq As Domain.Entities.MixingStationSequenceDetail In Me._sequence.MixingStationSequenceDetail
                    Me.DicSequense.Add(seq.Id, New List(Of String)())
                Next
            End If
        End Set
    End Property

    Property UnitDoseTypeDatasource As XPInstantFeedbackSource Implements IProductionLine.UnitDoseTypeDatasource
        Get
            Return CType(INDsleddlDosis.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleddlDosis.Properties.DataSource = value
        End Set
    End Property

    Property FuctionalUnitDatasource As XPInstantFeedbackSource Implements IProductionLine.FuctionalUnitDatasource
        Get
            Return CType(INDsleddlUnidad.Properties.DataSource, XPInstantFeedbackSource)
        End Get
        Set(value As XPInstantFeedbackSource)
            INDsleddlUnidad.Properties.DataSource = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece el tipo de dosis unitaria
    ''' </summary>
    Public Property UnitDoseTypeId As Integer Implements IProductionLine.UnitDoseTypeId
        Get
            Return CType(INDsleddlDosis.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleddlDosis.EditValue = value
        End Set
    End Property
    ''' <summary>
    ''' Obtiene o establece las unidades unitarias
    ''' </summary>
    Public Property FuctionalUnitId As Integer Implements IProductionLine.FuctionalUnitId
        Get
            Return CType(INDsleddlUnidad.EditValue, Integer)
        End Get
        Set(value As Integer)
            INDsleddlUnidad.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora o no 24 horas
    ''' </summary>
    Public Property Work24Hours As Boolean? Implements IProductionLine.Work24Hours
        Get
            Return CType(INDGleTime24.EditValue, Boolean?)
        End Get
        Set(value As Boolean?)
            INDGleTime24.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el horario inicial
    ''' </summary>
    Public Property StartTime As DateTime? Implements IProductionLine.StartTime
        Get
            Return CType(INDTmeStartTime.EditValue, DateTime?)
        End Get
        Set(value As DateTime?)
            INDTmeStartTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el horario final
    ''' </summary>
    Public Property EndTime As DateTime? Implements IProductionLine.EndTime
        Get
            Return CType(INDTmeEndTime.EditValue, DateTime?)
        End Get
        Set(value As DateTime?)
            INDTmeEndTime.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el lunes
    ''' </summary>
    Public Property WorkLunes As Boolean? Implements IProductionLine.WorkLunes
        Get
            Return CType(INDChkLunes.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkLunes.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el martes
    ''' </summary>
    Public Property WorkMartes As Boolean? Implements IProductionLine.WorkMartes
        Get
            Return CType(INDChkMartes.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkMartes.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el miercoles
    ''' </summary>
    Public Property WorkMiercoles As Boolean? Implements IProductionLine.WorkMiercoles
        Get
            Return CType(INDChkMiercoles.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkMiercoles.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el jueves
    ''' </summary>
    Public Property WorkJueves As Boolean? Implements IProductionLine.WorkJueves
        Get
            Return CType(INDChkJueves.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkJueves.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el viernes
    ''' </summary>
    Public Property WorkViernes As Boolean? Implements IProductionLine.WorkViernes
        Get
            Return CType(INDChkViernes.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkViernes.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el sabado
    ''' </summary>
    Public Property WorkSabado As Boolean? Implements IProductionLine.WorkSabado
        Get
            Return CType(INDChkSabado.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkSabado.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el domingo
    ''' </summary>
    Public Property WorkDomingo As Boolean? Implements IProductionLine.WorkDomingo
        Get
            Return CType(INDChkDomingo.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkDomingo.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece si labora el dia festivo
    ''' </summary>
    Public Property WorkFestivo As Boolean? Implements IProductionLine.WorkFestivo
        Get
            Return CType(INDChkFestivo.Checked, Boolean?)
        End Get
        Set(value As Boolean?)
            INDChkFestivo.Checked = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece la fecha en la cual no labora la línea de producción
    ''' </summary>
    Public Property StopDate As DateTime? Implements IProductionLine.StopDate
        Get
            Return CType(INDDteStopDate.EditValue, DateTime?)
        End Get
        Set(value As DateTime?)
            INDDteStopDate.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el motivo por el cual no labora la línea de producción
    ''' </summary>
    Public Property ReasonForStop As String Implements IProductionLine.ReasonForStop
        Get
            Return CType(INDMmeReasonForStop.EditValue, String)
        End Get
        Set(value As String)
            INDMmeReasonForStop.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Propiedad para habilitar o deshabilitar controles
    ''' </summary>
    Public WriteOnly Property ActionsOnControls As Boolean Implements IProductionLine.ActionsOnControls
        Set(value As Boolean)
            INDlycBase.BeginUpdate()

            INDbtnCode.Enabled = Not value
            INDtxtNombre.Enabled = value
            INDsleddlDosis.Enabled = value
            INDsleddlUnidad.Enabled = value
            INDbtnAdd.Enabled = value
            INDGleTime24.Enabled = value
            INDTmeStartTime.Enabled = value
            INDTmeEndTime.Enabled = value
            INDChkLunes.Enabled = value
            INDChkMartes.Enabled = value
            INDChkMiercoles.Enabled = value
            INDChkJueves.Enabled = value
            INDChkViernes.Enabled = value
            INDChkSabado.Enabled = value
            INDChkDomingo.Enabled = value
            INDChkFestivo.Enabled = value
            INDSmbAddSchedule.Enabled = value
            INDGvSchedules.OptionsBehavior.ReadOnly = Not value
            INDDteStopDate.Enabled = value
            INDMmeReasonForStop.Enabled = value
            INDSmbScheduleException.Enabled = value
            INDGvScheduleException.OptionsBehavior.ReadOnly = Not value
            INDlycBase.EndUpdate()
            If value Then
                INDtxtNombre.Focus()
            Else
                INDbtnCode.Focus()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el tag del formulario
    ''' </summary>
    ''' <returns>Tag del formulario</returns>
    Public ReadOnly Property MyTag As Object Implements IProductionLine.MyTag
        Get
            Return Me.Tag
        End Get
    End Property

    Public ReadOnly Property MyLayoutControl As IndigoLayoutControl Implements IProductionLine.MyLayoutControl
        Get
            Return Me.LayoutControls
        End Get
    End Property

#End Region

#Region "Propierties ICrudBase"

    ''' <summary>
    ''' Propiedad que establece los mensajes (Advertencias)
    ''' </summary>
    ''' <param name="icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(icono As EeventViewerImages) As String Implements ICrudBase.Mensaje
        Set(value As String)
            If icono = EeventViewerImages.Advertencia Then
                MessageIndigo.Show(value, MessageType.Warning, Me.Text)
            ElseIf icono = EeventViewerImages.Informacion Then
                MessageIndigo.Show(value, MessageType.Information, Me.Text)
            ElseIf icono = EeventViewerImages.MensajeError Then
                MessageIndigo.Show(value, MessageType.Errores, Me.Text, Botones.Aceptar, "")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Evento barra de botones Buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Buscar() Implements ICrudBase.Buscar
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Deshace los cambios hechos en el formulario
    ''' </summary>
    Public Sub Deshacer() Implements ICrudBase.Deshacer
        CleanControls()
    End Sub

    ''' <summary>
    ''' Elimina el tipo de dosis unitaria seleccionado
    ''' </summary>
    Public Async Sub Eliminar() Implements ICrudBase.Eliminar

        If Me._productionLineEntity IsNot Nothing AndAlso Me._productionLineEntity.Id > 0 Then
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
                Try
                    Using Model As New MProductionLine(Me.Tag.ToString())
                        AsyncLoader(True)
                        Dim result = Await Model.DeleteProductLineAsync(Me._productionLineEntity)
                        If result.StatusCode = eStatusResult.SUCCESS Then
                            Me.DeleteDocumentIndexed()
                            AsyncLoader(False)
                            Me.Deshacer()
                        Else
                            AsyncLoader(False)
                            INDbtnCode.Enabled = False
                        End If
                        ShowMessage(result.StatusCode) = result.Message
                    End Using
                Catch ex As Exception
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                    Throw ex
                End Try
            End If
        End If

    End Sub

    ''' <summary>
    ''' Guarda el turno
    ''' </summary>
    Public Async Sub Guardar() Implements ICrudBase.Guardar
        If Not ValidateControls() Then
            Exit Sub
        End If
        If (Not Me.Work24Hours) AndAlso (_productionLineEntity.ProductionLineSchedule Is Nothing OrElse _productionLineEntity.ProductionLineSchedule.Count = 0) Then
            Mensaje(EeventViewerImages.Advertencia) = "Especifique un horario para la línea de producción"
            Exit Sub
        End If
        If (Me.Work24Hours) AndAlso (Not Me.WorkLunes) AndAlso (Not Me.WorkMartes) AndAlso (Not Me.WorkMiercoles) AndAlso (Not Me.WorkJueves) AndAlso (Not Me.WorkViernes) _
            AndAlso (Not Me.WorkSabado) AndAlso (Not Me.WorkDomingo) AndAlso (Not Me.WorkFestivo) Then
            Mensaje(EeventViewerImages.Advertencia) = "Seleccione que días se trabaja las 24 horas"
            Exit Sub
        End If
        AssigningValues()
        Try
            Using Model As New MProductionLine(Me.Tag.ToString())
                AsyncLoader(True)
                Dim result As ActionResult(Of ProductionLine) = Await Model.SaveProductionLineAsync(Me._productionLineEntity, Me._idCurrentSequence)
                'Dim result2 As ActionResult(Of ProductionLineUnitDoseType) = Await Model.SaveProductionLineUnitDoseTypeAsync(Me._productionLineUnitDoseTypeEntity)
                If result.StatusCode = eStatusResult.SUCCESS Then
                    If _productionLineEntity.ChangeTracker.State = Domain.Base.Entities.ObjectState.Added Then
                        If Not Me._sequence.IsManual AndAlso Not Me._sequence.Sequential Then
                            Me.DicSequense(Me._idCurrentSequence).RemoveAt(0)
                        End If
                    End If
                    Me._productionLineEntity = result.ObjectEmbbeded
                    Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Id_ProductionLine = _productionLineEntity.Id
                    AsyncLoader(False)
                    Deshacer()
                Else
                    AsyncLoader(False)
                    INDbtnCode.Enabled = False
                End If
                ShowMessage(result.StatusCode) = result.Message
            End Using
        Catch ex As Exception
            AsyncLoader(False)
            INDbtnCode.Enabled = False
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Indica que el dato ya existe y se va a actualizar
    ''' </summary>
    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements ICrudBase.LogicaBotonActualizar
    End Sub

    ''' <summary>
    ''' Limpia el formulario para iniciar
    ''' </summary>
    Public Async Sub Nuevo() Implements ICrudBase.Nuevo
        If Me._sequence.IsManual Then
            Deshacer()
        Else
            Await NewProductionLine()
        End If
    End Sub

#End Region

#Region "Bar Button Events"

    ''' <summary>
    ''' Evento barra de botones Activo - Inactivo
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive
        Await ChangeState()
    End Sub

    ''' <summary>
    ''' Handles the Load event of the BarraBotones control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)

    End Sub

    ''' <summary>
    ''' Barras the botones_ click actualizar.
    ''' </summary>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        If ListProductionLineUnitDoseType Is Nothing OrElse ListProductionLineUnitDoseType.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar un tipo de dosis unitaria"
            Exit Sub
        End If
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click buscar.
    ''' </summary>
    Private Sub BarraBotones_ClickBuscar() Handles BarraBotones.ClickBuscar, INDbtnCode.ButtonClick
        OpenSearch()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
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
        If ListProductionLineUnitDoseType Is Nothing OrElse ListProductionLineUnitDoseType.Count = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe asignar un tipo de dosis unitaria"
            Exit Sub
        End If
        Guardar()
    End Sub

    ''' <summary>
    ''' Barras the botones_ click nuevo.
    ''' </summary>
    Private Sub BarraBotones_ClickNuevo() Handles BarraBotones.ClickNuevo
        Deshacer()
        Me.Nuevo()
    End Sub

    ''' <summary>
    ''' Barras the botones_ changue operating unit.
    ''' </summary>
    ''' <param name="operatingUnit">The operating unit.</param>
    Private Sub BarraBotones_ChangueOperatingUnit(operatingUnit As OperatingUnit) Handles BarraBotones.ChangueOperatingUnit
        If operatingUnit IsNot Nothing Then
            Me._idOperativeUnit = operatingUnit.Id
            If Me._sequence IsNot Nothing AndAlso Me._sequence.Scope.Equals("OU") AndAlso Me._sequence.MixingStationSequenceDetail IsNot Nothing Then
                If Not Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit = operatingUnit.Id) Then
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Events"

#Region "Load"

    ''' <summary>
    ''' Evento que se dispara al cargar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmProductionLine_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me._idOperativeUnit = Me.BarraBotones.OperatingUnitValue
        '****Inicializar variables*****'
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance
        _presenter = New PProductionLine(Me)
        _presenter.GetSequence()
        LoadStatus()
        Deshacer()
        Load_FuctionalUnit()
        AddActionsColumns()
        INDDteStopDate.Properties.MinValue = DateAndTime.Now
        INDMmeReasonForStop.Properties.MaxLength = 200
        Me.LayoutControls.SetIsCustomizable(Me.INDlycBase, True)

        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView3.SetListAcction(INDviewUnitDosesType, ListActions)
    End Sub
#End Region

#Region "Disposed"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        ListFuctionalUnit = Nothing
        ListProductionLineUnitDoseType = Nothing
        _presenter = Nothing
        _productionLineEntity = Nothing
        _productionLineUnitDoseTypeEntity = Nothing
        _record = Nothing
        _idOperativeUnit = Nothing
        _sequence = Nothing
        _idCurrentSequence = Nothing
        Me.ListProductionLineScheduleDelete = Nothing
        Me.ListScheduleExceptionDelete = Nothing
    End Sub
#End Region

#Region "Closing"

    ''' <summary>
    ''' Evento que se dispara al cerrar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmUnitDoseType_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        DeleteBlockedrecord()
    End Sub

#End Region

#Region "KeyDown"

    ''' <summary>
    ''' Evento para consultar un turno
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnCode_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDbtnCode.KeyDown

        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            If _sequence Is Nothing OrElse _sequence.Id = 0 Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("SequenceNotFound")
                Exit Sub
            End If
            If Me._sequence.IsManual Then
                If Not String.IsNullOrEmpty(Code.Trim()) Then
                    Await Me.LoadControls()
                Else
                    Mensaje(EeventViewerImages.Advertencia) = "La secuencia numérica esta configurada como manual, por favor digite un código"
                End If
            Else
                If String.IsNullOrEmpty(Code) Then
                    Await Me.NewProductionLine()
                Else
                    Await Me.LoadControls()
                End If
            End If
        ElseIf e.KeyCode = System.Windows.Forms.Keys.F4 Then
            OpenSearch()
        End If
    End Sub

#End Region

#Region "Shown"
    ''' <summary>
    ''' Evento que se dispara al activarse el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmUnitDoseType_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated, MyBase.Shown
        If INDbtnCode.Text Is String.Empty Then
            INDbtnCode.Focus()
        End If
    End Sub
#End Region

#Region "QueryPopUp"

    ''' <summary>
    ''' Maneja el evento QueryPopUp del control INDGleTime24
    ''' </summary>
    ''' <param name="sender">Referencia al objeto que lanza el evento.</param>
    ''' <param name="e">Instancia que contiene los datos del evento.</param>
    Private Sub INDGleTime24_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDGleTime24.QueryPopUp
        If INDGleTime24.Properties.DataSource Is Nothing Then
            Dim ListSiNo = New List(Of Tuple(Of Boolean, String))
            ListSiNo.Add(New Tuple(Of Boolean, String)(False, "No"))
            ListSiNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
            INDGleTime24.Properties.DataSource = ListSiNo.ToList
        End If
    End Sub

    ''' <summary>
    ''' Carga el datasource de dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleddlDosis_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleddlDosis.QueryPopUp
        If INDsleddlDosis.Properties.DataSource Is Nothing Then
            Load_UnitDoseType()
        End If
    End Sub

#End Region

#Region "EditValueChanged"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDGleTime24_EditValueChanged(sender As Object, e As EventArgs) Handles INDGleTime24.EditValueChanged
        If Me.Work24Hours Is Nothing OrElse Me.Work24Hours Then
            INDLciStartTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciEndTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciAddSchedule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciSchedules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never

            If Me.Work24Hours IsNot Nothing AndAlso Me.Work24Hours Then
                INDChkLunes.Enabled = False
                INDChkMartes.Enabled = False
                INDChkMiercoles.Enabled = False
                INDChkJueves.Enabled = False
                INDChkViernes.Enabled = False
                INDChkSabado.Enabled = False
                INDChkDomingo.Enabled = False
                INDChkFestivo.Enabled = False
                Me.WorkLunes = True
                Me.WorkMartes = True
                Me.WorkMiercoles = True
                Me.WorkJueves = True
                Me.WorkViernes = True
                Me.WorkSabado = True
                Me.WorkDomingo = True
                Me.WorkFestivo = True

                INDLciTime24.Text = "Labora 24 Horas x 7 días"
            End If
        Else
            INDLciStartTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciEndTime.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciAddSchedule.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciSchedules.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

            INDChkLunes.Enabled = True
            INDChkMartes.Enabled = True
            INDChkMiercoles.Enabled = True
            INDChkJueves.Enabled = True
            INDChkViernes.Enabled = True
            INDChkSabado.Enabled = True
            INDChkDomingo.Enabled = True
            INDChkFestivo.Enabled = True
            Me.WorkLunes = False
            Me.WorkMartes = False
            Me.WorkMiercoles = False
            Me.WorkJueves = False
            Me.WorkViernes = False
            Me.WorkSabado = False
            Me.WorkDomingo = False
            Me.WorkFestivo = False

            INDLciTime24.Text = "Labora 24 Horas"
        End If
    End Sub
#End Region

#Region "Click"

    ''' <summary>
    ''' Agrega un horario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbAddSchedule_Click(sender As Object, e As EventArgs) Handles INDSmbAddSchedule.Click
        If Me.Work24Hours IsNot Nothing AndAlso Not Me.Work24Hours Then
            If Me.StartTime Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione hora inicial"
                Exit Sub
            End If
            If Me.EndTime Is Nothing Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione hora final"
                Exit Sub
            End If
            If Me.StartTime.GetValueOrDefault.TimeOfDay >= Me.EndTime.GetValueOrDefault.TimeOfDay Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "La hora inicial debe ser menor a la hora final"
                Exit Sub
            End If
        End If
        If (Me.WorkLunes Is Nothing OrElse Not Me.WorkLunes) _
            AndAlso (Me.WorkMartes Is Nothing OrElse Not Me.WorkMartes) _
            AndAlso (Me.WorkMiercoles Is Nothing OrElse Not Me.WorkMiercoles) _
            AndAlso (Me.WorkJueves Is Nothing OrElse Not Me.WorkJueves) _
            AndAlso (Me.WorkViernes Is Nothing OrElse Not Me.WorkViernes) _
            AndAlso (Me.WorkSabado Is Nothing OrElse Not Me.WorkSabado) _
            AndAlso (Me.WorkDomingo Is Nothing OrElse Not Me.WorkDomingo) _
            AndAlso (Me.WorkFestivo Is Nothing OrElse Not Me.WorkFestivo) Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione un día"
            Exit Sub
        End If
        If _productionLineEntity IsNot Nothing AndAlso _productionLineEntity.ProductionLineSchedule IsNot Nothing AndAlso _productionLineEntity.ProductionLineSchedule.Count > 0 Then
            If ExisteCruce(Me.WorkLunes, 1, "Lunes") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkMartes, 2, "Martes") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkMiercoles, 3, "Miércoles") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkJueves, 4, "Jueves") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkViernes, 5, "Viernes") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkSabado, 6, "Sábado") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkDomingo, 7, "Domingo") Then
                Exit Sub
            ElseIf ExisteCruce(Me.WorkFestivo, 8, "Festivo") Then
                Exit Sub
            End If
        End If

        AgregarHorario(Me.WorkLunes, 1, "Lunes")
        AgregarHorario(Me.WorkMartes, 2, "Martes")
        AgregarHorario(Me.WorkMiercoles, 3, "Miércoles")
        AgregarHorario(Me.WorkJueves, 4, "Jueves")
        AgregarHorario(Me.WorkViernes, 5, "Viernes")
        AgregarHorario(Me.WorkSabado, 6, "Sábado")
        AgregarHorario(Me.WorkDomingo, 7, "Domingo")
        AgregarHorario(Me.WorkFestivo, 8, "Festivo")

        Me._productionLineSchedule = Nothing
        Me.StartTime = DateTime.Now.Date
        Me.EndTime = DateAdd(DateInterval.Second, -1, DateAdd(DateInterval.Day, 1, DateTime.Now.Date))
        INDChkLunes.Enabled = True
        INDChkMartes.Enabled = True
        INDChkMiercoles.Enabled = True
        INDChkJueves.Enabled = True
        INDChkViernes.Enabled = True
        INDChkSabado.Enabled = True
        INDChkDomingo.Enabled = True
        INDChkFestivo.Enabled = True
        Me.WorkLunes = False
        Me.WorkMartes = False
        Me.WorkMiercoles = False
        Me.WorkJueves = False
        Me.WorkViernes = False
        Me.WorkSabado = False
        Me.WorkDomingo = False
        Me.WorkFestivo = False
        INDSmbAddSchedule.Text = "Agregar"

        INDGcSchedules.DataSource = _productionLineEntity.ProductionLineSchedule.Where(Function(x) x.ChangeTracker.State <> ObjectState.Deleted)
        INDGcSchedules.RefreshDataSource()
    End Sub

    ''' <summary>
    ''' Agrega un excepcion del horario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDSmbScheduleException_Click(sender As Object, e As EventArgs) Handles INDSmbScheduleException.Click
        If Me.StopDate Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Seleccione fecha de excepción de horario"
            Exit Sub
        End If
        If Me.ReasonForStop Is Nothing Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "Ingrese el motivo de excepción de horario"
            Exit Sub
        End If
        If Me.ReasonForStop.Length < 10 Then
            Me.Mensaje(EeventViewerImages.Advertencia) = "La cantidad mínima de caracteres para el motivo de excepción de horario es 10"
            Exit Sub
        End If
        If _productionLineEntity IsNot Nothing AndAlso _productionLineEntity.ProductionLineScheduleException IsNot Nothing AndAlso _productionLineEntity.ProductionLineScheduleException.Count > 0 Then
            If _productionLineEntity.ProductionLineScheduleException.Any(Function(o) o.StopDate = Me.StopDate) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Ya se agregó la fecha de excepción de horario"
                Exit Sub
            End If
        End If

        If Work24Hours = False Then
            If _productionLineEntity Is Nothing OrElse _productionLineEntity.ProductionLineSchedule Is Nothing OrElse _productionLineEntity.ProductionLineSchedule.Count = 0 Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "Configure horarios para la línea de producción"
                Exit Sub
            End If
            Dim _dayId As Byte
            Select Case Me.StopDate.GetValueOrDefault.DayOfWeek
                Case DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday
                    _dayId = Me.StopDate.GetValueOrDefault.DayOfWeek
                Case DayOfWeek.Sunday
                    _dayId = 7
            End Select
            If Not _productionLineEntity.ProductionLineSchedule.Any(Function(s) s.DayId = _dayId) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "La fecha de excepción de horario no esta configurada como horario laboral de la línea de producción"
                Exit Sub
            End If
        End If

        Dim se As New ProductionLineScheduleException
        With se
            .ProductionLineId = _productionLineEntity.Id
            .StopDate = Me.StopDate
            .ReasonForStop = Me.ReasonForStop
        End With
        _productionLineEntity.ProductionLineScheduleException.Add(se)
        INDGcScheduleException.DataSource = _productionLineEntity.ProductionLineScheduleException
        INDGcScheduleException.RefreshDataSource()
        Me.StopDate = Nothing
        Me.ReasonForStop = Nothing
    End Sub

    ''' <summary>
    ''' Se ejecuta al dar click sobre agregar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDbtnAdd_Click(sender As Object, e As EventArgs) Handles INDbtnAdd.Click
        If INDsleddlDosis.EditValue Is Nothing OrElse INDsleddlDosis.EditValue = 0 Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar una dosis unitaria"
            Exit Sub
        End If

        If ListProductionLineUnitDoseType Is Nothing Then
            ListProductionLineUnitDoseType = New List(Of ProductionLineUnitDoseType)
        Else
            If (From x In ListProductionLineUnitDoseType Where x.Id_UnitDoseType = INDsleddlDosis.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El tipo de dosis unitaria ya se encuetra agregado"
                Exit Sub
            End If
        End If

        ' Validamos que no hayan dos centrales de mezclas con el mismo centro de atencion que contengan este mismo tipo de dosis unitaria en alguna línea de producción
        Dim unitDoseTypeId As Integer = INDsleddlDosis.EditValue

        If _productionLineEntity.Id > 0 Then
            AsyncLoader(True)

            Using model As New MProductionLine(Tag)
                Dim resValidation = Await model.ValidateProductionLineUnitDoseTypeAsync(_productionLineEntity.Id, unitDoseTypeId)
                AsyncLoader(False)
                If Not resValidation.StateResult Then
                    Mensaje(EeventViewerImages.Advertencia) = resValidation.Message
                    Return
                End If
            End Using
        End If

        Dim entity As New ProductionLineUnitDoseType
        With entity
            .Id_UnitDoseType = unitDoseTypeId
            .UnitDoseTypeCode = INDsleddlDosis.Text.Split("-")(0)
            .UnitDoseTypeName = INDsleddlDosis.Text.Split("-")(1)
        End With

        ListProductionLineUnitDoseType.Add(entity)
        INDgcUnitDoseType.DataSource = ListProductionLineUnitDoseType
        INDgcUnitDoseType.RefreshDataSource()
        INDsleddlDosis.EditValue = Nothing
        INDsleddlDosis.Focus()
    End Sub

#End Region

#Region "ButtonClick"

    ''' <summary>
    ''' Abre el form de dosis unitarias
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleddlDosis_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleddlDosis.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(2067, Nothing, True)
            Load_UnitDoseType()
        End If
    End Sub

    ''' <summary>
    ''' Abre el form de unidades funcionales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleddlUnidad_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleddlUnidad.ButtonClick
        If e.Button.Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus Then
            OpenForm(523, Nothing, True)
            Load_FuctionalUnit()
        End If
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Rejilla dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView3.Click_ButtonAction
        DeleteDosesUnitType()
    End Sub

    ''' <summary>
    ''' Rejilla dosis unitaria
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView3_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView3.ContexMenuActions
        DeleteDosesUnitType()
    End Sub

#End Region

#End Region

#Region "Methods"

    ''' <summary>
    ''' Elimina una dosis unitaria
    ''' </summary>
    Private Sub DeleteDosesUnitType()
        Dim entity = CType(INDviewUnitDosesType.GetFocusedRow, ProductionLineUnitDoseType)

        If entity.Id > 0 Then
            If ListDeleteProductionLineUnitDoseType Is Nothing Then
                ListDeleteProductionLineUnitDoseType = New List(Of ProductionLineUnitDoseType)
            End If
            ListDeleteProductionLineUnitDoseType.Add(entity)
        End If

        ListProductionLineUnitDoseType.Remove(entity)
        INDgcUnitDoseType.DataSource = Nothing
        INDgcUnitDoseType.DataSource = ListProductionLineUnitDoseType
        INDgcUnitDoseType.RefreshDataSource()
    End Sub

    '''' <summary>
    '''' Loads the datasource in dropboxes.
    '''' </summary>
    Sub Load_UnitDoseType()
        _presenter.InitializeUnitDoseType()
    End Sub

    Sub Load_FuctionalUnit()
        _presenter.InitializeFuctionalUnit()
    End Sub

    ''' <summary>
    ''' Metodo que retorna el valor de la busqueda
    ''' </summary>
    ''' <param name="ReturnValue">The return value.</param>
    ''' <param name="ReturnObject">The return object.</param>
    Private Async Sub ReturnValue(ByVal ReturnValue As String, ByVal ReturnObject As Object)
        Await DeleteBlockedrecord()
        Code = ReturnValue
        If Code IsNot String.Empty Then
            Await LoadControls()

            If Not INDbtnCode.Enabled Then
                BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Buscar) = True
            End If
            INDbtnCode.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Metodo que elimina el registro bloqueado
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Async Function DeleteBlockedrecord() As Task
        If _record IsNot Nothing AndAlso _record.Id > 0 AndAlso _record.CodUser.Equals(Me.indigo.UserIndigo) Then
            Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                Await model.DeleteBlockRecord(_record)
                _record = Nothing
            End Using
        End If
    End Function

    ''' <summary>
    ''' Metodo que se utiliza para consultar el registro y cargar los controles con los datos del registro
    ''' </summary>
    Private Async Function LoadControls() As Task
        If Not String.IsNullOrEmpty(Code) AndAlso Not String.IsNullOrWhiteSpace(Code) Then
            If Me.BarraBotones.PermiteConsultar = False Then
                Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
                Exit Function
            End If
            Try
                Using Model As New MProductionLine(CStr(Me.Tag))
                    AsyncLoader(True)
                    Dim resultOperation = Await Model.GetProductionLineAsync(INDbtnCode.Text.Trim)
                    INDlycBase.BeginUpdate()
                    _productionLineEntity = resultOperation.ObjectEmbbeded
                    If _productionLineEntity IsNot Nothing AndAlso _productionLineEntity.Id > 0 Then
                        Me.BarraBotones.StatusRecordVisible = True

                        Using ModelRecord As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            _record = Await ModelRecord.GetBlockRecord(CStr(Me.Tag), CStr(_productionLineEntity.Id))
                            With _productionLineEntity
                                LayoutControls.SetCustomFieldsValue(.CustomProperties)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), .CreationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), .CreationDate)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationUser"), .ModificationUser)
                                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("ModificationDate"), .ModificationDate)

                                'Llenar Entidad
                                Code = .Code
                                Name = .Name
                                State = .State
                                Id_ProductionLine = .Id
                                If .ProductionLineSchedule Is Nothing Then .ProductionLineSchedule = New Domain.Entities.TrackableCollection(Of ProductionLineSchedule)
                                Me.Work24Hours = .Work24Hours
                                If Me.Work24Hours IsNot Nothing Then
                                    INDGleTime24.Properties.NullText = IIf(.Work24Hours, "Si", "No")
                                    If .Work24Hours Then
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 1) Then
                                            Me.WorkLunes = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 2) Then
                                            Me.WorkMartes = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 3) Then
                                            Me.WorkMiercoles = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 4) Then
                                            Me.WorkJueves = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 5) Then
                                            Me.WorkViernes = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 6) Then
                                            Me.WorkSabado = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 7) Then
                                            Me.WorkDomingo = True
                                        End If
                                        If .ProductionLineSchedule.Any(Function(s) s.DayId = 8) Then
                                            Me.WorkFestivo = True
                                        End If

                                        INDLciTime24.Text = "Labora 24 Horas x 7 días"
                                    Else
                                        INDLciTime24.Text = "Labora 24 Horas"
                                    End If
                                End If
                                INDGcSchedules.DataSource = .ProductionLineSchedule

                                ListProductionLineUnitDoseType = .ProductionLineUnitDoseType.ToList()
                                INDgcUnitDoseType.DataSource = ListProductionLineUnitDoseType

                                INDGcScheduleException.DataSource = .ProductionLineScheduleException
                            End With

                            'Llenar NullText
                            Me.GetDocumentIndexed(Me.Tag.ToString() & "_" & Me._productionLineEntity.Code)
                            If _record.Id = 0 Then
                                _record = (Await ModelRecord.SaveBlockRecord(
                                    New BlockRecordMixingStation With {.BlockDate = Date.Now, .ChangeTracker = New ObjectChangeTracker() With {.State = ObjectState.Added},
                                        .NameUser = Me.indigo.UserIndigoName, .IdForm = Me.Tag, .CodUser = Me.indigo.UserIndigo, .IdRecord = _productionLineEntity.Id})
                                    ).ObjectEmbbeded
                            Else
                                Dim xtraMessage As String = String.Format(obtenerRecurso(RegistroBloqueado, Comunes), _record.CodUser, _record.NameUser, _record.BlockDate)
                                Me.BarraBotones.ShowXtraMessage(xtraMessage, ImagesXtraLabel.Warning, _record.CodUser)
                            End If
                            Me.BarraBotones.SetDocuments(_productionLineEntity.Id, Me.Tag.ToString(), Nothing, GetType(AccountPayableConceptNotes).Name)
                            Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdateOrDelete)
                            AsyncLoader(False)
                            ActionsOnControls = True

                            If _productionLineEntity.Work24Hours IsNot Nothing AndAlso _productionLineEntity.Work24Hours Then
                                INDChkLunes.Enabled = False
                                INDChkMartes.Enabled = False
                                INDChkMiercoles.Enabled = False
                                INDChkJueves.Enabled = False
                                INDChkViernes.Enabled = False
                                INDChkSabado.Enabled = False
                                INDChkDomingo.Enabled = False
                                INDChkFestivo.Enabled = False
                            End If
                        End Using
                    Else
                        AsyncLoader(False)
                        If Me._sequence.IsManual Then
                            Await Me.NewProductionLine()
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString(Me.GetType().Name & "_DontExists", NAME_MODULE)
                            Code = String.Empty
                            INDbtnCode.Focus()
                        End If
                    End If
                    INDlycBase.EndUpdate()
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        End If
    End Function

    ''' <summary>
    ''' Metodo que prepara los controles y realiza la logica para crear una nueva dependencia
    ''' </summary>
    Private Async Function NewProductionLine() As Task
        _productionLineEntity = New ProductionLine() With {.State = True}
        _productionLineEntity.ProductionLineSchedule = New Domain.Entities.TrackableCollection(Of ProductionLineSchedule)
        _productionLineEntity.ProductionLineScheduleException = New Domain.Entities.TrackableCollection(Of ProductionLineScheduleException)
        _productionLineUnitDoseTypeEntity = New ProductionLineUnitDoseType()
        If Me._sequence.IsManual Then
            Me.ActionsOnControls = True
            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
        Else
            If Me._sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail(0).Id
            ElseIf Me._sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If Me._sequence.MixingStationSequenceDetail.Any(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit) Then
                    Me._idCurrentSequence = Me._sequence.MixingStationSequenceDetail.SingleOrDefault(Function(o) o.IdOperatingUnit IsNot Nothing AndAlso o.IdOperatingUnit = Me._idOperativeUnit).Id
                Else
                    Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("OperatingUnitUnassigned")
                    Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
                    Exit Function
                End If
            End If
            If Me._sequence.Sequential Then
                Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                Me.ActionsOnControls = True
                Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
            Else
                If Me.DicSequense IsNot Nothing AndAlso Me.DicSequense.Count > 0 Then
                    If Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                        Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                        Me.ActionsOnControls = True
                        Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                        Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                    Else
                        AsyncLoader(True)
                        Using model As New MBlockRecordAndSequenceMixingStation(CStr(Me.Tag))
                            Me.DicSequense(CInt(Me._idCurrentSequence)) = Await model.GetNumericSequenceGroup(CInt(Me._idCurrentSequence))
                        End Using
                        AsyncLoader(False)
                        If Me.DicSequense(CInt(Me._idCurrentSequence)) IsNot Nothing AndAlso Me.DicSequense(CInt(Me._idCurrentSequence)).Count > 0 Then
                            Me.Code = Me.DicSequense(CInt(Me._idCurrentSequence))(0)
                            Me.ActionsOnControls = True
                            Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                            Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                        Else
                            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("InvalidPatternSequense")
                        End If
                    End If
                Else
                    Me.Code = ResourceManager.GetString("LabelOrTextboxNew")
                    Me.ActionsOnControls = True
                    Me.BarraBotones.PrepareToolbar(eAction.OnlySave)
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.GestionDocumental) = True
                    Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Auditoria) = True
                End If
            End If
        End If
    End Function

    ''' <summary>
    ''' Metodo que abre el frontal de busqueda
    ''' </summary>
    Public Sub OpenSearch() Implements ICrudBase.OpenSearch
        If BarraBotones.PermiteConsultar = False Then
            Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("NoPermissions")
            Exit Sub
        End If
        FormSearchObjects = New FrmBusqueda
        AddHandler FormSearchObjects.ReturnValue, AddressOf ReturnValue
        With FormSearchObjects
            .ListaColumnas = {
                              New ColumnInfo() With {.Caption = "Código", .FieldName = "Code", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)},
                              New ColumnInfo() With {.Caption = "Línea de Producción", .FieldName = "Name", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.4)},
                              New ColumnInfo() With {.Caption = "Estado", .FieldName = "StatusName", .ColumnWidth = CInt(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width * 0.3)}}.ToList
            .ValorSolicitado = "Code"
            .ListadoOrigenDatos = Infrastructure.CrossCutting.Base.eDataSource.ListProductionLine
            .FormParent = Me
            .ShowSearch()
        End With
    End Sub

    ''' <summary>
    ''' Metodo que cambia el estado de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Function ChangeState() As Task
        If Not String.IsNullOrEmpty(Me._productionLineEntity.Code) Then
            Try
                Using model As New MProductionLine(Me.Tag)
                    AsyncLoader(True)
                    Dim state As Boolean = Not Me._productionLineEntity.State
                    Dim result As ActionResult(Of ProductionLine) = Await model.ChangeState(Me._productionLineEntity.Code, state)
                    AsyncLoader(False)
                    If result.StatusCode = eStatusResult.SUCCESS Then
                        Me._productionLineEntity = result.ObjectEmbbeded
                        Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                    Else
                        INDbtnCode.Enabled = False
                    End If
                    ShowMessage(result.StatusCode) = result.Message
                End Using
            Catch ex As Exception
                AsyncLoader(False)
                INDbtnCode.Enabled = False
                Throw ex
            End Try
        Else
            Me.Mensaje(EeventViewerImages.Advertencia) = ResourceManager.GetString("CodeEmpty")
        End If
    End Function

    ''' <summary>
    ''' Metodo que asigna los valores
    ''' </summary>
    Private Sub AssigningValues()

        Dim Lista As New List(Of ProductionLineUnitDoseType)
        With _productionLineEntity
            .CustomProperties = LayoutControls.GetCustomFieldsValue()
            .Code = Code
            .Name = Name
            .Work24Hours = Me.Work24Hours

            If .Work24Hours IsNot Nothing AndAlso .Work24Hours Then
                Me.StartTime = DateTime.Now.Date
                Me.EndTime = DateAdd(DateInterval.Second, -1, DateAdd(DateInterval.Day, 1, DateTime.Now.Date))
                AgregarHorario24(Me.WorkLunes, 1, "Lunes")
                AgregarHorario24(Me.WorkMartes, 2, "Martes")
                AgregarHorario24(Me.WorkMiercoles, 3, "Miércoles")
                AgregarHorario24(Me.WorkJueves, 4, "Jueves")
                AgregarHorario24(Me.WorkViernes, 5, "Viernes")
                AgregarHorario24(Me.WorkSabado, 6, "Sábado")
                AgregarHorario24(Me.WorkDomingo, 7, "Domingo")
                AgregarHorario24(Me.WorkFestivo, 8, "Festivo")
            End If

            If ListProductionLineUnitDoseType IsNot Nothing AndAlso ListProductionLineUnitDoseType.Count > 0 Then
                ListProductionLineUnitDoseType.ForEach(Sub(x) .ProductionLineUnitDoseType.Add(x))
            End If

            If ListDeleteProductionLineUnitDoseType IsNot Nothing AndAlso ListDeleteProductionLineUnitDoseType.Count > 0 Then
                ListDeleteProductionLineUnitDoseType.ForEach(Sub(x) .ProductionLineUnitDoseType.Add(x.MarkAsDeleted()))
            End If

            For Each s In ListProductionLineScheduleDelete
                .ProductionLineSchedule.Add(s)
            Next
            For Each s In ListScheduleExceptionDelete
                .ProductionLineScheduleException.Add(s)
            Next
        End With

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="diaChequeado"></param>
    ''' <param name="dia"></param>
    ''' <param name="nombredia"></param>
    Private Sub AgregarHorario24(ByVal diaChequeado As Boolean?, ByVal dia As Byte, ByVal nombredia As String)
        If _productionLineEntity.ProductionLineSchedule.Any(Function(s) s.DayId = dia AndAlso ((Not diaChequeado) OrElse (s.StartTime.TimeOfDay <> Me.StartTime.GetValueOrDefault.TimeOfDay _
                            OrElse s.EndTime.TimeOfDay <> Me.EndTime.GetValueOrDefault.TimeOfDay))) Then
            For Each sa In _productionLineEntity.ProductionLineSchedule.Where(Function(s) s.DayId = dia AndAlso ((Not diaChequeado) OrElse (s.StartTime.TimeOfDay <> Me.StartTime.GetValueOrDefault.TimeOfDay _
                OrElse s.EndTime.TimeOfDay <> Me.EndTime.GetValueOrDefault.TimeOfDay))).ToList
                If sa.Id > 0 Then
                    sa.MarkAsDeleted()
                    ListProductionLineScheduleDelete.Add(sa)
                End If
                _productionLineEntity.ProductionLineSchedule.Remove(sa)
            Next
        End If
        If Not _productionLineEntity.ProductionLineSchedule.Any(Function(s) s.DayId = dia AndAlso s.StartTime.TimeOfDay = Me.StartTime.GetValueOrDefault.TimeOfDay _
                AndAlso s.EndTime.TimeOfDay = Me.EndTime.GetValueOrDefault.TimeOfDay) Then
            AgregarHorario(diaChequeado, dia, nombredia)
        End If
    End Sub

    ''' <summary>
    ''' Metodo que limpia los controles
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanControls()

        INDlycBase.BeginUpdate()

        ActionsOnControls = False
        Me.BarraBotones.StatusRecordVisible = False
        Me.BarraBotones.StatusRecord = Nothing
        Me.BarraBotones.EnableBarItems()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.ReassignOperatingUnit()
        Me._doc = Nothing
        State = True

        'Limpiar controles
        Code = String.Empty
        Name = String.Empty
        UnitDoseTypeId = Nothing
        FuctionalUnitId = Nothing
        Me.Work24Hours = Nothing
        INDGleTime24.Properties.NullText = String.Empty
        Me.StartTime = Nothing
        Me.EndTime = Nothing
        Me.WorkLunes = False
        Me.WorkMartes = False
        Me.WorkMiercoles = False
        Me.WorkJueves = False
        Me.WorkViernes = False
        Me.WorkSabado = False
        Me.WorkDomingo = False
        Me.WorkFestivo = False
        Me._productionLineSchedule = Nothing
        Me.ListProductionLineScheduleDelete = New List(Of ProductionLineSchedule)
        Me.StopDate = Nothing
        Me.ReasonForStop = Nothing
        Me.ListScheduleExceptionDelete = New List(Of ProductionLineScheduleException)

        INDgcUnitDoseType.DataSource = Nothing
        ListProductionLineUnitDoseType = Nothing
        ListDeleteProductionLineUnitDoseType = Nothing

        INDGcSchedules.DataSource = Nothing
        INDGcScheduleException.DataSource = Nothing

        INDLciTime24.Text = "Labora 24 Horas"

        If indigo.UserViewMode AndAlso Not Me.FormSearchObjects.IsDisposed Then
            Me.BarraBotones.PrepareToolbar(eAction.OnlyNew)
        Else
            Me.BarraBotones.PrepareToolbar(eAction.NewAndFind)
        End If
        INDlycBase.EndUpdate()
        DeleteBlockedrecord()
    End Sub

    ''' <summary>
    ''' Carga los estados de la barra
    ''' </summary>
    Private Sub LoadStatus()
        Me.BarraBotones.StatesWhitActions = {eActionsStatusRecords.Active, eActionsStatusRecords.Inactive}
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="diaChequeado"></param>
    ''' <param name="dia"></param>
    ''' <param name="nombredia"></param>
    ''' <returns></returns>
    Public Function ExisteCruce(ByVal diaChequeado As Boolean?, ByVal dia As Byte, ByVal nombredia As String) As String
        Dim _ExisteCruce As Boolean
        If diaChequeado IsNot Nothing AndAlso diaChequeado Then
            If _productionLineEntity.ProductionLineSchedule.Any(Function(s) s.DayId = dia AndAlso Me.StartTime.GetValueOrDefault.TimeOfDay <= s.EndTime.TimeOfDay _
                                                                    AndAlso Me.EndTime.GetValueOrDefault.TimeOfDay >= s.StartTime.TimeOfDay _
                                                                    AndAlso (_productionLineSchedule Is Nothing OrElse Not s.Equals(_productionLineSchedule))) Then
                Me.Mensaje(EeventViewerImages.Advertencia) = String.Format("Para el día {0} ya se configuro el horario o existe algún cruce", nombredia)
                _ExisteCruce = True
            End If
        End If
        Return _ExisteCruce
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="diaChequeado"></param>
    ''' <param name="dia"></param>
    ''' <param name="nombredia"></param>
    Public Sub AgregarHorario(ByVal diaChequeado As Boolean?, ByVal dia As Byte, ByVal nombredia As String)
        If diaChequeado IsNot Nothing AndAlso diaChequeado Then
            Dim o As New ProductionLineSchedule
            If _productionLineSchedule IsNot Nothing Then
                o = _productionLineSchedule
                If o.Id > 0 Then o.MarkAsModified()
            End If
            With o
                .ProductionLineId = _productionLineEntity.Id
                .StartTime = Me.StartTime
                .EndTime = Me.EndTime
                .DayId = dia
                .DayName = nombredia
            End With
            If _productionLineSchedule Is Nothing Then _productionLineEntity.ProductionLineSchedule.Add(o)
        End If

    End Sub

    ''' <summary>
    ''' Método para agregar acciones a la rejilla de productos
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddActionsColumns()
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView2.SetListAcction(INDGvScheduleException, ListActions)
        ListActions.Add(eAcciones.Edit)
        IndigoGridView1.SetListAcction(INDGvSchedules, ListActions)
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvSchedules.Columns
            If col.Name = "colActions" OrElse col.Name = "MoreInfo" Then
                col.Width = 75
            End If
        Next
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDGvScheduleException.Columns
            If col.Name = "colActions" OrElse col.Name = "MoreInfo" Then
                col.Width = 75
            End If
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit", "Editar"
                EditDetail()
            Case "Remove", "Eliminar"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub IndigoGridView2_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView2.Click_ButtonAction
        Dim _btnTag As String = String.Empty
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = TryCast(sender, DevExpress.XtraEditors.SimpleButton)
        If btn Is Nothing Then
            Dim btnEdit As DevExpress.XtraEditors.ButtonEdit
            btnEdit = TryCast(sender, DevExpress.XtraEditors.ButtonEdit)
            If btnEdit IsNot Nothing Then
                _btnTag = btnEdit.Text.ToString
            End If
        Else
            _btnTag = btn.Tag.ToString
        End If

        Select Case _btnTag
            Case "Remove", "Eliminar"
                DeleteException()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit", "Editar"
                EditDetail()
            Case "Remove", "Eliminar"
                DeleteDetail()
        End Select
    End Sub

    ''' <summary>
    ''' Accion de click derecho del mouse
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView2_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView2.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Remove", "Eliminar"
                DeleteException()
        End Select
    End Sub

    ''' <summary>
    ''' Editar el detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDetail()
        _productionLineSchedule = DirectCast(INDGvSchedules.GetFocusedRow(), ProductionLineSchedule)
        If _productionLineSchedule IsNot Nothing Then
            Me.StartTime = _productionLineSchedule.StartTime
            Me.EndTime = _productionLineSchedule.EndTime
            INDChkLunes.Enabled = False
            INDChkMartes.Enabled = False
            INDChkMiercoles.Enabled = False
            INDChkJueves.Enabled = False
            INDChkViernes.Enabled = False
            INDChkSabado.Enabled = False
            INDChkDomingo.Enabled = False
            INDChkFestivo.Enabled = False
            Me.WorkLunes = False
            Me.WorkMartes = False
            Me.WorkMiercoles = False
            Me.WorkJueves = False
            Me.WorkViernes = False
            Me.WorkSabado = False
            Me.WorkDomingo = False
            Me.WorkFestivo = False
            Select Case _productionLineSchedule.DayId
                Case 1
                    Me.WorkLunes = True
                Case 2
                    Me.WorkMartes = True
                Case 3
                    Me.WorkMiercoles = True
                Case 4
                    Me.WorkJueves = True
                Case 5
                    Me.WorkViernes = True
                Case 6
                    Me.WorkSabado = True
                Case 7
                    Me.WorkDomingo = True
                Case 8
                    Me.WorkFestivo = True
                Case Else
            End Select
            INDSmbAddSchedule.Text = "Editar"
        End If
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDetail()
        _productionLineSchedule = DirectCast(INDGvSchedules.GetFocusedRow(), ProductionLineSchedule)
        If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then
            If _productionLineSchedule.Id > 0 Then
                _productionLineSchedule.MarkAsDeleted()
                ListProductionLineScheduleDelete.Add(_productionLineSchedule)
            End If
            _productionLineEntity.ProductionLineSchedule.Remove(_productionLineSchedule)
            INDGcSchedules.RefreshDataSource()

        End If
        Me._productionLineSchedule = Nothing
    End Sub

    ''' <summary>
    ''' Eliminar Detalle
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteException()
        Dim o = DirectCast(INDGvScheduleException.GetFocusedRow(), ProductionLineScheduleException)
        If o IsNot Nothing Then
            If o.StopDate.Date < DateAndTime.Now.Date Then
                Me.Mensaje(EeventViewerImages.Advertencia) = "No se puede eliminar el registro, la fecha de excepción de horario es menor a la fecha actual"
                Exit Sub
            End If
            If MessageIndigo.Show(ResourceManager.GetString("DeleteRecord"), MessageType.Question, Me.Text, Botones.SiNo) = System.Windows.Forms.DialogResult.Yes Then

                If o.Id > 0 Then
                    o.MarkAsDeleted()
                    ListScheduleExceptionDelete.Add(o)
                End If
                _productionLineEntity.ProductionLineScheduleException.Remove(o)
                INDGcScheduleException.RefreshDataSource()

            End If
        End If
    End Sub



#End Region

End Class