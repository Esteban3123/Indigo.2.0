'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Daniel Eduardo Arévalo
' Created          : 16-08-2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports System.Windows.Forms
Imports DevExpress.XtraEditors.Design
Imports DevExpress.XtraEditors
Imports DevExpress.Data
Imports Presentation.Payroll.MVP
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports System.Resources
Imports Presentation.Controls
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base.BaseClass
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions

Imports Domain.Base.Entities
Imports Presentation.Common

Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports ResourceManager = Infrastructure.CrossCutting.Resources.ResourceManager


#End Region


Public Class FrmBlockSchedule

    Implements IBlockSchedule

    Dim ListBlockSchedule As List(Of BlockSchedule)

    Dim ObjResultBlockSchedule As Tuple(Of BlockScheduleC, List(Of BlockSchedule))

    Dim ListBlockScheduleClass As New List(Of BlockScheduleClass)

    ''' <summary>
    ''' Variable para instanciar el presentador del funcional
    ''' </summary>
    Dim Presenter As PBlockSchedule

    Dim ListFunctionalUnit As List(Of FunctionalUnit)

    Dim ObjBlockScheduleC As BlockScheduleC

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Tipos de Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _BlockType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property BlockType As List(Of Tuple(Of Integer, String))
        Get
            If _BlockType Is Nothing Then
                _BlockType = New List(Of Tuple(Of Integer, String))
                _BlockType.Add(New Tuple(Of Integer, String)(1, "Automático por Fechas"))
                _BlockType.Add(New Tuple(Of Integer, String)(2, "Manual"))
            End If
            Return _BlockType
        End Get
    End Property

    ''' <summary>
    ''' Propiedad que se usa para cargar la Dupla de Datos de Tipos de Reporte
    ''' </summary>
    ''' <remarks></remarks>
    Private _PayrollType As List(Of Tuple(Of Integer, String))
    Private ReadOnly Property PayrollType As List(Of Tuple(Of Integer, String))
        Get
            If _PayrollType Is Nothing Then
                _PayrollType = New List(Of Tuple(Of Integer, String))
                _PayrollType.Add(New Tuple(Of Integer, String)(1, "Mensual"))
                _PayrollType.Add(New Tuple(Of Integer, String)(2, "Quincenal"))
            End If
            Return _PayrollType
        End Get
    End Property

#Region "METHODS"
    Private Sub CleanControls()
        'INDSleForm.EditValue = Nothing
        'INDGcDocuments.DataSource = Nothing
        'ColCode.Caption = "Código"
        'ColCode.FieldName = "DocumentNumber"
        'Me.BarraBotones.PrepareToolbar(eAction.OnlyFind)
        'Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Confirmar) = False
    End Sub

    ''' <summary>
    ''' metodo para establecer los documentos de tesoreria
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub SetFunctionalUnit()
        Try
            AsyncLoader(True)
            Using modelFunctionalUnit As New MFunctionalUnit(MFunctionalUnit.TAG)
                ListFunctionalUnit = Await modelFunctionalUnit.ListAllAsync()

                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
            End Using

            LoadControls()
            AsyncLoader(False)
        Catch ex As Exception
            AsyncLoader(False)
        End Try

    End Sub

#End Region
    Private Sub FrmBlockSchedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.LayoutControls.SetIsCustomizable(Me.INDLcBlockSchedule, True)
        Me._doc = Nothing
        Me.indigo = SessionValues.Instance

        Presenter = New PBlockSchedule(Me)
        '  Presenter.initializes()
        SetFunctionalUnit()

        INDSlBlockType.Properties.DataSource = BlockType()
        INDSlPayrollType.Properties.DataSource = PayrollType()

    End Sub

    Private Sub AssigningValues()

        For Each item In ListBlockScheduleClass
            If item.ApplyFunctionalUnit = True Then
                Dim BlockSchedule As New BlockSchedule
                BlockSchedule.FunctionalUnitId = item.IdFunctionalUnit
                ListBlockSchedule.Add(BlockSchedule)
            End If
        Next

        If INDLciPayrollType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then

            Dim MonthInitialBlockTime As Date
            Dim FirstFortnighHourBlockTyme As Date
            Dim SecondFortnighHourBlockTyme As Date

            If INDLcgBlockHourFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                FirstFortnighHourBlockTyme = DateTime.Parse(INDSpeBlockHourFirst.EditValue.ToString())
                SecondFortnighHourBlockTyme = DateTime.Parse(INDSpeBlockHourSecond.EditValue.ToString())
            End If

            If INDLciMonthDayBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                MonthInitialBlockTime = DateTime.Parse(INDSpeMonthHourBlock.EditValue.ToString())
            End If

            ObjBlockScheduleC.PayrollType = INDSlPayrollType.EditValue
            ObjBlockScheduleC.MonthBlockDay = CInt(INDSpMonthBlockDay.EditValue)
            ObjBlockScheduleC.MonthInitialBlockTime = MonthInitialBlockTime.TimeOfDay
            ObjBlockScheduleC.FirstFortnightDayBlockTime = CInt(INDSpeBlockDayFirst.EditValue)
            ObjBlockScheduleC.FirstFortnighHourBlockTyme = FirstFortnighHourBlockTyme.TimeOfDay
            ObjBlockScheduleC.SecondFortnightDayBlockTime = CInt(INDSpeBlockDaySecond.EditValue)
            ObjBlockScheduleC.SecondFortnighHourBlockTyme = SecondFortnighHourBlockTyme.TimeOfDay

        End If

        ObjBlockScheduleC.BlockType = INDSlBlockType.EditValue

    End Sub

    ''' <summary>
    ''' Loads the controls.
    ''' </summary>
    Private Async Sub LoadControls()

        Me.BarraBotones.StatusRecordVisible = True
        Me.BarraBotones.StatusRecord = True
        AsyncLoader(True)


        Using Model As New MBlockSchedule(CStr(Me.Tag))

            ObjResultBlockSchedule = Await Model.ListBlockScheduleAsync()

            If ObjResultBlockSchedule.Item1 Is Nothing Then
                ObjBlockScheduleC = New BlockScheduleC()
            Else

                ObjBlockScheduleC = ObjResultBlockSchedule.Item1

                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationUser"), ObjBlockScheduleC.CreationUser)
                Me.BarraBotones.AddAuditBasic(ResourceManager.GetString("CreationDate"), ObjBlockScheduleC.CreationDate)

            End If


            ListBlockSchedule = ObjResultBlockSchedule.Item2
        End Using


        ListBlockScheduleClass = New List(Of BlockScheduleClass)
        For Each ObjFunctionalUnit As FunctionalUnit In ListFunctionalUnit
            Dim BlockScheduleClass As New BlockScheduleClass

            If ListBlockSchedule.Any(Function(x) x.FunctionalUnitId = ObjFunctionalUnit.Id) = True Then
                BlockScheduleClass.ApplyFunctionalUnit = True
            Else
                BlockScheduleClass.ApplyFunctionalUnit = False
            End If

            BlockScheduleClass.IdFunctionalUnit = ObjFunctionalUnit.Id
            BlockScheduleClass.CodeFunctionalUnit = ObjFunctionalUnit.Code
            BlockScheduleClass.NameFunctionalUnit = ObjFunctionalUnit.Name


            ListBlockScheduleClass.Add(BlockScheduleClass)

        Next

        INDGcFunctionalUnit.DataSource = Nothing
        INDGcFunctionalUnit.DataSource = ListBlockScheduleClass

        INDSlBlockType.EditValue = ObjBlockScheduleC.BlockType
        INDSlPayrollType.EditValue = ObjBlockScheduleC.PayrollType
        INDSpMonthBlockDay.EditValue = ObjBlockScheduleC.MonthBlockDay
        INDSpeMonthHourBlock.EditValue = ObjBlockScheduleC.MonthInitialBlockTime
        INDSpeBlockDayFirst.EditValue = ObjBlockScheduleC.FirstFortnightDayBlockTime
        INDSpeBlockHourFirst.EditValue = ObjBlockScheduleC.FirstFortnighHourBlockTyme
        INDSpeBlockDaySecond.EditValue = ObjBlockScheduleC.SecondFortnightDayBlockTime
        INDSpeBlockHourSecond.EditValue = ObjBlockScheduleC.SecondFortnighHourBlockTyme

        AsyncLoader(False)
        ActionsOnControls = True
    End Sub

#Region "BarraBotones"
    ''' <summary>
    ''' Barra Botones: Activa o desactiva el estado
    ''' </summary>
    ''' <remarks></remarks>
    Private Async Sub BarraBotones_Click_ActiveInactive() Handles BarraBotones.Click_ActiveInactive

    End Sub


    ''' <summary>
    ''' Barra botones: Guardar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickGuardar() Handles BarraBotones.ClickGuardar
        Guardar()
    End Sub

    ''' <summary>
    ''' Barra botones: Actualizar
    ''' </summary>
    ''' <param name="operatingUnit"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_ClickActualizar() Handles BarraBotones.ClickActualizar
        Guardar()
    End Sub

    ''' <summary>
    ''' Load de la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        Me.BarraBotones.ActualizarPermisosBarra(MyBase.Tag.ToString)
    End Sub

    Private Sub BarraBotones_Deshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub

#End Region

#Region "ICrud"
    Public Sub Buscar() Implements Base.IcrudBase.Buscar

    End Sub

    Public Sub Deshacer() Implements Base.IcrudBase.Deshacer
        'CleanControls()
        Me.BarraBotones.CleanAuditBasic()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
    End Sub

    Public Sub Eliminar() Implements Base.IcrudBase.Eliminar

    End Sub

    Public Async Sub Guardar() Implements Base.IcrudBase.Guardar

        If ValidateControl() = False Then
            Exit Sub
        End If

        AssigningValues()

        Using model As New MBlockSchedule(CStr(Me.Tag))
            AsyncLoader(True)
            Dim Result = Await model.SaveBlockScheduleAsync(ObjBlockScheduleC, ListBlockSchedule)
            AsyncLoader(False)
            If Result.StateResult = True Then

                Mensaje(EeventViewerImages.Informacion) = obtenerRecurso(ComunesGuardado)

                Me.ObjResultBlockSchedule = Result.ObjectEmbbeded

                LoadControls()
                Me.UpdateIndexedDocument(Me.BarraBotones._listDocuments)
                AsyncLoader(False)

                Me.BarraBotones.CleanAuditBasic()
                Me.BarraBotones.PrepareToolbar(eAction.OnlyUpdate)
                Me.BarraBotones.OcultarBotonesSinPermisos(EbuttonsWithoutPermission.Deshacer) = True
            Else
                If Result.MessageResult(0) = ErrorConcurrencia Then
                    Mensaje(EeventViewerImages.MensajeError) = "Error de Concurrencia"
                Else
                    Mensaje(EeventViewerImages.MensajeError) = "Error Desconocido"
                End If
            End If
        End Using
        FlagSave = False
    End Sub

    Public Sub LogicaBotonActualizar(existeDatos As Boolean) Implements IcrudBase.LogicaBotonActualizar

    End Sub

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



    Public Sub Nuevo() Implements Base.IcrudBase.Nuevo

    End Sub

    Public Sub OpenSearch() Implements Base.IcrudBase.OpenSearch

    End Sub

    Private Sub RecargarUF()

    End Sub

#Region "EditValueChanged"
    Private Sub INDSlBlockType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlBlockType.EditValueChanged

        If INDSlBlockType.EditValue = 1 Then
            INDLcgFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            HideControl()
        Else
            INDLcgFunctionalUnit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            HideControlPayroll()
        End If

    End Sub

    Private Sub INDSlPayrollType_EditValueChanged(sender As Object, e As EventArgs) Handles INDSlPayrollType.EditValueChanged
        If INDLciPayrollType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            HideControl()
        End If
    End Sub

#End Region

    Private Sub HideControlPayroll()
        INDLcgBlockDayFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgBlockHourFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgBlockDaySecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLcgBlockHourSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciMonthDayBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciMonthHourBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        INDLciPayrollType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub

    Private Sub HideControl()

        INDLciPayrollType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        If INDSlPayrollType.EditValue = 1 Then
            'Nómina Mensual
            INDLcgBlockDayFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgBlockHourFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgBlockDaySecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLcgBlockHourSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciMonthDayBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciMonthHourBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            'Nómina Quincenal
            INDLcgBlockDayFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgBlockHourFirst.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgBlockDaySecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLcgBlockHourSecond.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            INDLciMonthHourBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDLciMonthDayBlock.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    Private Function ValidateControl() As Boolean

        If INDLciPayrollType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then

            If INDSlPayrollType.EditValue = 1 Then

                'Mensual
                If INDSpMonthBlockDay.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado el día de bloqueo del cuadro de Turnos"
                    INDSpMonthBlockDay.Focus()
                    Return False
                End If

                If INDSpeMonthHourBlock.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado la Hora Inicio de Bloqueo"
                    INDSpeMonthHourBlock.Focus()
                    Return False
                End If

            Else
                If INDSpeBlockDayFirst.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado el día de bloqueo del cuadro de Turnos de la primera quincena"
                    INDSpeBlockDayFirst.Focus()
                    Return False
                End If

                If INDSpeBlockHourFirst.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado la Hora Inicio de Bloqueo de la primera quincena"
                    INDSpeBlockHourFirst.Focus()
                    Return False
                End If

                If INDSpeBlockDaySecond.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado el día de bloqueo del cuadro de Turnos de la segunda quincena"
                    INDSpeBlockDaySecond.Focus()
                    Return False
                End If

                If INDSpeBlockHourSecond.EditValue Is Nothing Then
                    Mensaje(EeventViewerImages.Advertencia) = "No ha seleccionado la Hora Inicio de Bloqueo de la segunda quincena"
                    INDSpeBlockHourSecond.Focus()
                    Return False
                End If

            End If

        End If

        Return True

    End Function
#End Region
End Class