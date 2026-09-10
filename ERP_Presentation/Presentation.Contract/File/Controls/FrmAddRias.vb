'***********************************************************************
' Assembly         : Presentacion.Contract
' Author           : Carlos Mario Arias Rubiano
' Created          : 21/11/2018
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Resources
Imports System.ComponentModel
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports System.Text
Imports Presentation.Contract.MVP

#End Region

Public Class FrmAddRias

#Region "Event"

    ''' <summary>
    ''' Evento para agregar un detalle
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Public Event AddRIASArgs(sender As Object, e As AddCupsEntityRIAS)

#End Region

#Region "Variables"

    ''' <summary>
    ''' Lista de sexos
    ''' </summary>
    Dim ListSex As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Permite saber si se esta editando desde el form principal
    ''' </summary>
    Public EditModeRIAS As Boolean

    ''' <summary>
    ''' Permite saber si se esta modificando
    ''' </summary>
    Dim EditModePopup As Boolean = False

    ''' <summary>
    ''' Variable para la entidad que se va a editar en la rejilla
    ''' </summary>
    ''' <remarks></remarks>
    Public CupsEntityRIAS As CupsEntityRIAS

    ''' <summary>
    ''' Index del listado, sirve para eliminar el registro en el listado de comparación
    ''' </summary>
    Dim IndexOfEditPopup As Integer? = Nothing

    ''' <summary>
    ''' Presentador
    ''' </summary>
    Dim Presenter As PCupsEntity

    ''' <summary>
    ''' Variable que contiene la lista de tipos de datos
    ''' </summary>
    Dim ListRIPSConcept As New List(Of Tuple(Of String, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListYesNo As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListStatus As List(Of Tuple(Of Boolean, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListYearMonthDay As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListRules As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListFrequencyEndDateRealization As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista de tupla
    ''' </summary>
    Dim ListFrequencyCurrentPeriod As List(Of Tuple(Of Integer, String))

    ''' <summary>
    ''' Lista las reglas en la rejilla
    ''' </summary>
    Dim ListCupsEntityRIASDetail As List(Of CupsEntityRIASDetail)

    ''' <summary>
    ''' Lista de eliminados de las reglas en la rejilla
    ''' </summary>
    Dim ListDeleteCupsEntityRIASDetail As List(Of CupsEntityRIASDetail)

    ''' <summary>
    ''' Entidad de las reglas de la rejilla
    ''' </summary>
    Dim CupsEntityRIASDetail As CupsEntityRIASDetail = Nothing

    ''' <summary>
    ''' Listado para comparar las rias del formulario principal
    ''' </summary>
    Public ListRIASCompare As List(Of CupsEntityRIAS)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Propiedad para enviar mensajes al visor de eventos
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As Base.EeventViewerImages) As String
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
    ''' Obtiene o establece la regla seleccionada
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Rules As Integer?
        Get
            Return INDsleRules.EditValue
        End Get
        Set(value As Integer?)
            INDsleRules.EditValue = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece es estado establecido en la regla
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Status As Boolean
        Get
            Return INDsleStatus.EditValue
        End Get
        Set(value As Boolean)
            INDsleStatus.EditValue = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga los controles cuando se esta editando
    ''' </summary>
    Private Sub LoadControls()
        With CupsEntityRIAS
            INDsleRIAS.EditValue = .RiasId
            INDsleRIAS.Properties.NullText = .RiasDescription
            INDsleRIPSConcept.EditValue = .ConceptRIPS
            ListCupsEntityRIASDetail = .ListCupsEntityRIASDetail
            INDgcRules.DataSource = Nothing
            INDgcRules.DataSource = (From x In ListCupsEntityRIASDetail Where x.IsDelete = False)
        End With
        If ListCupsEntityRIASDetail IsNot Nothing AndAlso ListCupsEntityRIASDetail.Count > 0 Then
            INDsleUnit.EditValue = ListCupsEntityRIASDetail(0).Unit
            'INDsleUnit.Properties.ReadOnly = True
        End If
    End Sub

    ''' <summary>
    ''' Agrega la información de la rias a la rejilla del formulario principal
    ''' </summary>
    Private Sub AddRIASToPrincipalForm()
        'Se validan los controles del formulario
        Dim errors = ValidateControlsForm()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        'Se valida que el rias que se va a agregar no exista en el formulario principal
        If ListRIASCompare IsNot Nothing AndAlso ListRIASCompare.Count > 0 Then
            If (From x In ListRIASCompare Where x.RiasId = INDsleRIAS.EditValue Select x).Count > 0 Then
                Mensaje(EeventViewerImages.Advertencia) = "El RIAS " + INDsleRIAS.Text + " ya existe en la lista"
                INDsleRIAS.Focus()
                Exit Sub
            End If
        End If

        If ListDeleteCupsEntityRIASDetail IsNot Nothing AndAlso ListDeleteCupsEntityRIASDetail.Count > 0 Then
            ListDeleteCupsEntityRIASDetail.ForEach(Sub(item) ListCupsEntityRIASDetail.Add(item))
        End If

        If EditModeRIAS = False Then
            CupsEntityRIAS = New CupsEntityRIAS
        End If
        With CupsEntityRIAS
            .RiasId = INDsleRIAS.EditValue
            .RiasDescription = INDsleRIAS.Text
            .ConceptRIPS = INDsleRIPSConcept.EditValue
            .ListCupsEntityRIASDetail = ListCupsEntityRIASDetail
        End With

        Dim args As New AddCupsEntityRIAS
        args.CupsEntityRIAS = CupsEntityRIAS
        args.EditMode = EditModeRIAS
        RaiseEvent AddRIASArgs(Nothing, args)
        Me.Close()
    End Sub

    ''' <summary>
    ''' Elimina una regla
    ''' </summary>
    Private Sub DeleteRule()
        Dim entityDelete = DirectCast(INDviewRules.GetFocusedRow(), CupsEntityRIASDetail)
        If entityDelete.Id > 0 Then
            If ListDeleteCupsEntityRIASDetail Is Nothing Then
                ListDeleteCupsEntityRIASDetail = New List(Of CupsEntityRIASDetail)
            End If
            entityDelete.IsDelete = True
            ListDeleteCupsEntityRIASDetail.Add(entityDelete)
        End If
        ListCupsEntityRIASDetail.Remove(entityDelete)
        INDgcRules.DataSource = Nothing
        INDgcRules.DataSource = (From x In ListCupsEntityRIASDetail Where x.IsDelete = False)
        Mensaje(EeventViewerImages.Informacion) = "Regla eliminada de la rejilla correctamente"
        If ListCupsEntityRIASDetail.Count = 0 Then
            'INDsleUnit.Properties.ReadOnly = False
        End If
    End Sub

    ''' <summary>
    ''' Edita una regla
    ''' </summary>
    Private Sub EditRule()
        EditModePopup = True
        CupsEntityRIASDetail = DirectCast(INDviewRules.GetFocusedRow(), CupsEntityRIASDetail)
        IndexOfEditPopup = ListCupsEntityRIASDetail.IndexOf(CupsEntityRIASDetail)

        With CupsEntityRIASDetail
            INDseMinimunRange.EditValue = .MinimunAge
            INDseMaximunRange.EditValue = .MaximunAge
            INDsleUnit.EditValue = .Unit
            Rules = .Rule

            Select Case Rules
                Case 2 'Frecuencia por rango de edad
                    INDseRangeAge.EditValue = .Frequency
                Case 3 'Frecuencia por última fecha de realización
                    INDsePeriodQuantity.EditValue = .PeriodQuantity
                    INDsleFrequencyEndDateRealization.EditValue = .FrequencyUnit
                Case 4 'Frecuencia por periodo vigente
                    INDseFrequencyCurrentPeriod.EditValue = .Frequency
                    INDsleFrequencyOptions.EditValue = .FrequencyUnit
            End Select

            INDsleRequiresMedicalOrder.EditValue = .RequireMedicalOrder
            INDsleSex.EditValue = .Sex
            Status = .Status
        End With

        'INDsleUnit.Properties.ReadOnly = True
        INDsleRules.Properties.ReadOnly = True
        INDpceRules.ShowPopup()
    End Sub

    ''' <summary>
    ''' Limpia los controles del popup
    ''' </summary>
    Private Sub CleanControlsPopup(CleanUnit As Boolean)
        INDseMinimunRange.EditValue = Nothing
        INDseMaximunRange.EditValue = Nothing
        If CleanUnit Then
            INDsleUnit.EditValue = Nothing
        End If
        Rules = Nothing
        INDseRangeAge.EditValue = Nothing
        INDsePeriodQuantity.EditValue = Nothing
        INDsleFrequencyEndDateRealization.EditValue = Nothing
        INDseFrequencyCurrentPeriod.EditValue = Nothing
        INDsleFrequencyOptions.EditValue = Nothing
        INDsleRequiresMedicalOrder.EditValue = False
        EditModePopup = False
        CupsEntityRIASDetail = Nothing
        IndexOfEditPopup = Nothing
        INDsleSex.EditValue = 2
        INDsleRules.Properties.ReadOnly = False
        Status = True
    End Sub

    ''' <summary>
    ''' Agrega la regla en la rejilla
    ''' </summary>
    Private Sub AddRule()
        'Se valida los controles del poup
        Dim errors = ValidateControlsPopup()
        If errors.Length > 0 Then
            Mensaje(EeventViewerImages.Advertencia) = errors
            Exit Sub
        End If

        If ListCupsEntityRIASDetail Is Nothing Then 'Se valida que no sea nulo el listado
            ListCupsEntityRIASDetail = New List(Of CupsEntityRIASDetail)
        End If

        'Se valida si la regla se duplica y solapamiento de rangos
        'Solo se valida si NO estamos en modo edición, o si hay otras reglas iguales (excluyendo la que se edita)
        If Status Then
            'Construimos lista excluyendo el registro que se está editando usando el índice
            Dim listaParaValidar As New List(Of CupsEntityRIASDetail)
            For i As Integer = 0 To ListCupsEntityRIASDetail.Count - 1
                Dim item = ListCupsEntityRIASDetail(i)
                'Si estamos en modo edición y es el índice del registro que editamos, lo saltamos
                If EditModePopup AndAlso IndexOfEditPopup IsNot Nothing AndAlso i = IndexOfEditPopup Then
                    Continue For
                End If
                If item.Rule = Rules AndAlso item.Status = True AndAlso item.IsDelete = False Then
                    listaParaValidar.Add(item)
                End If
            Next
            If listaParaValidar.Any() Then
                If ValidateRangeAges() > 0 Then
                    Mensaje(EeventViewerImages.Advertencia) = "El rango de edad ya existe en una rejilla activa con la misma regla"
                    INDseMinimunRange.Focus()
                    Exit Sub
                End If
            End If
        End If

        'Se crea la entidad para asignarla al listado
        If EditModePopup = False Then
            CupsEntityRIASDetail = New CupsEntityRIASDetail
        End If
        With CupsEntityRIASDetail
            .Rule = Rules
            .MinimunAge = INDseMinimunRange.EditValue
            .MaximunAge = INDseMaximunRange.EditValue
            .Unit = INDsleUnit.EditValue
            .RequireMedicalOrder = INDsleRequiresMedicalOrder.EditValue
            .Sex = INDsleSex.EditValue
            .Status = Status

            .PeriodQuantity = Nothing
            .Frequency = Nothing
            .FrequencyUnit = Nothing

            If INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Frequency = INDseRangeAge.EditValue
                .FrequencyUnit = 9 'Valor único para Rango de Edad (evita conflicto con Trimestre=3)
            End If

            If INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Frequency = 1
                .PeriodQuantity = INDsePeriodQuantity.EditValue
            End If

            If INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .FrequencyUnit = INDsleFrequencyEndDateRealization.EditValue
            End If

            If INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .Frequency = INDseFrequencyCurrentPeriod.EditValue
            End If

            If INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
                .FrequencyUnit = INDsleFrequencyOptions.EditValue
            End If
        End With

        'Se asigna la entidad al listado
        If EditModePopup = False Then
            ListCupsEntityRIASDetail.Add(CupsEntityRIASDetail)
        End If

        INDgcRules.DataSource = Nothing
        INDgcRules.DataSource = (From x In ListCupsEntityRIASDetail Where x.IsDelete = False)
        If EditModePopup = False Then
            Mensaje(EeventViewerImages.Informacion) = "Regla agregada correctamente"
        Else
            Mensaje(EeventViewerImages.Informacion) = "Regla modificada correctamente"
        End If
        CleanControlsPopup(False)
        'INDsleUnit.Properties.ReadOnly = True
        INDseMinimunRange.Focus()
    End Sub

    ''' <summary>
    ''' Valida los rangos de edad para que no se solapen
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ValidateRangeAges() As Integer
        'Se convierten los rangos en días
        Dim _rangeAgeMin As Integer = 0
        Dim _rangeAgeMax As Integer = 0
        Select Case INDsleUnit.EditValue
            Case 1 'Días
                _rangeAgeMin = INDseMinimunRange.EditValue
                _rangeAgeMax = INDseMaximunRange.EditValue
            Case 2 'Meses
                _rangeAgeMin = INDseMinimunRange.EditValue * 30
                _rangeAgeMax = INDseMaximunRange.EditValue * 30
            Case 3 'Años
                _rangeAgeMin = INDseMinimunRange.EditValue * 360
                _rangeAgeMax = INDseMaximunRange.EditValue * 360
        End Select

        'Listado para comparar los rangos - SOLO los activos, con la misma regla, y excluyendo el registro que se está editando usando índice
        Dim ListComparePopup As New List(Of CupsEntityRIASDetail)
        For i As Integer = 0 To ListCupsEntityRIASDetail.Count - 1
            Dim item = ListCupsEntityRIASDetail(i)
            'Si estamos en modo edición y es el índice del registro que editamos, lo saltamos
            If EditModePopup AndAlso IndexOfEditPopup IsNot Nothing AndAlso i = IndexOfEditPopup Then
                Continue For
            End If
            If item.IsDelete = False AndAlso item.Rule = Rules AndAlso item.Status = True Then
                ListComparePopup.Add(item.CloneEntity())
            End If
        Next

        'Se convierte los rangos de las edades en días
        ListComparePopup.ForEach(Sub(item)
                                     Select Case item.Unit
                                         Case 1 'Días
                                             item.MinimunAgeDays = item.MinimunAge
                                             item.MaximunAgeDays = item.MaximunAge
                                         Case 2 'Meses
                                             item.MinimunAgeDays = item.MinimunAge * 30
                                             item.MaximunAgeDays = item.MaximunAge * 30
                                         Case 3 'Años
                                             item.MinimunAgeDays = item.MinimunAge * 360
                                             item.MaximunAgeDays = item.MaximunAge * 360
                                     End Select
                                 End Sub)

        Return ListComparePopup.FindAll(Function(item) ((_rangeAgeMin >= item.MinimunAgeDays AndAlso _rangeAgeMin <= item.MaximunAgeDays) OrElse
                                                    (_rangeAgeMax >= item.MinimunAgeDays AndAlso _rangeAgeMax <= item.MaximunAgeDays) OrElse
                                                    (_rangeAgeMin < item.MinimunAgeDays) AndAlso (_rangeAgeMax > item.MaximunAgeDays))).Count()
    End Function

    ''' <summary>
    ''' Valida los controles del popup
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsPopup() As String
        Dim errors As New StringBuilder

        If INDseMaximunRange.EditValue = Nothing OrElse INDseMaximunRange.EditValue = 0 Then
            errors.AppendLine("Debe ingresar un rango de edad máximo")
        End If

        If INDsleUnit.EditValue = Nothing Then
            errors.AppendLine("Debe seleccionar una unidad")
        End If

        If Rules = Nothing Then
            errors.AppendLine("Debe seleccionar una regla")
        End If

        If INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDseRangeAge.EditValue = Nothing Then
                errors.AppendLine("Debe ingresar una frecuencia")
            End If
        End If

        If INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsePeriodQuantity.EditValue = Nothing Then
                errors.AppendLine("Debe ingresar una frecuencia")
            End If
        End If

        If INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleFrequencyEndDateRealization.EditValue = Nothing Then
                errors.AppendLine("Debe seleccionar una frecuencia")
            End If
        End If

        If INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDseFrequencyCurrentPeriod.EditValue = Nothing Then
                errors.AppendLine("Debe ingresar una frecuencia")
            End If
        End If

        If INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always Then
            If INDsleFrequencyOptions.EditValue = Nothing Then
                errors.AppendLine("Debe seleccionar una opción")
            End If
        End If

        Return errors.ToString
    End Function

    ''' <summary>
    ''' Valida los controles del formulario
    ''' </summary>
    ''' <returns></returns>
    Private Function ValidateControlsForm() As String
        Dim errors As New StringBuilder

        If INDsleRIAS.EditValue = Nothing Then
            errors.AppendLine("Debe seleccionar una RIAS")
        End If

        If INDsleRIPSConcept.EditValue = Nothing Then
            errors.AppendLine("Debe seleccionar un concepto RIPS")
        End If

        If ListCupsEntityRIASDetail Is Nothing OrElse ListCupsEntityRIASDetail.Count = 0 Then
            errors.AppendLine("Debe ingresar una regla")
        End If

        Return errors.ToString()
    End Function

    ''' <summary>
    ''' Carga el datasource del search que maneja tupla
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitializeTuple()
        ListRIPSConcept = New List(Of Tuple(Of String, String))
        ListRIPSConcept.Add(New Tuple(Of String, String)("01", "Atención del parto (Puerperio) (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("02", "Atención del Recién Nacido (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("03", "Atención en planificación familiar (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("04", "Detección de alteraciones de crecimiento y desarrollo del menor  de diez años (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("05", "Detección de alteraciones del desarrollo del joven (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("06", "Detección de alteraciones del embarazo (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("07", "Detección de alteraciones del adulto (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("08", "Detección de alteraciones de la agudeza visual (AC - Finalidad consulta)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("09", "Diagnóstico (AP - Finalidad del procedimiento)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("10", "Terapéutico (AP - Finalidad del procedimiento)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("11", "Protección específica (AP - Finalidad del procedimiento)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("12", "Detección temprana de enfermedad general (AP - Finalidad del procedimiento)"))
        ListRIPSConcept.Add(New Tuple(Of String, String)("13", "Detección temprana de enfermedad laboral (AP - Finalidad del procedimiento)"))
        INDsleRIPSConcept.Properties.DataSource = ListRIPSConcept.ToList

        ListYesNo = New List(Of Tuple(Of Boolean, String))
        ListYesNo.Add(New Tuple(Of Boolean, String)(True, "Si"))
        ListYesNo.Add(New Tuple(Of Boolean, String)(False, "No"))
        INDsleRequiresMedicalOrder.Properties.DataSource = ListYesNo.ToList()

        ListYearMonthDay = New List(Of Tuple(Of Integer, String))
        ListYearMonthDay.Add(New Tuple(Of Integer, String)(1, "Días"))
        ListYearMonthDay.Add(New Tuple(Of Integer, String)(2, "Meses"))
        ListYearMonthDay.Add(New Tuple(Of Integer, String)(3, "Años"))
        INDsleUnit.Properties.DataSource = ListYearMonthDay.ToList()

        ListRules = New List(Of Tuple(Of Integer, String))
        ListRules.Add(New Tuple(Of Integer, String)(1, "Sin Regla"))
        ListRules.Add(New Tuple(Of Integer, String)(2, "Frecuencia por rango de edad"))
        ListRules.Add(New Tuple(Of Integer, String)(3, "Frecuencia por última fecha de realización"))
        ListRules.Add(New Tuple(Of Integer, String)(4, "Frecuencia por periodo vigente"))
        INDsleRules.Properties.DataSource = ListRules.ToList()

        ListFrequencyEndDateRealization = New List(Of Tuple(Of Integer, String))
        ListFrequencyEndDateRealization.Add(New Tuple(Of Integer, String)(1, "Año"))
        ListFrequencyEndDateRealization.Add(New Tuple(Of Integer, String)(2, "Semestre"))
        ListFrequencyEndDateRealization.Add(New Tuple(Of Integer, String)(3, "Trimestre"))
        ListFrequencyEndDateRealization.Add(New Tuple(Of Integer, String)(4, "Mes"))
        INDsleFrequencyEndDateRealization.Properties.DataSource = ListFrequencyEndDateRealization.ToList()

        ListFrequencyCurrentPeriod = New List(Of Tuple(Of Integer, String))
        ListFrequencyCurrentPeriod.Add(New Tuple(Of Integer, String)(5, "En el año vigente"))
        ListFrequencyCurrentPeriod.Add(New Tuple(Of Integer, String)(6, "En el semestre vigente"))
        ListFrequencyCurrentPeriod.Add(New Tuple(Of Integer, String)(7, "En el trimestre vigente"))
        ListFrequencyCurrentPeriod.Add(New Tuple(Of Integer, String)(8, "En el mes vigente"))
        INDsleFrequencyOptions.Properties.DataSource = ListFrequencyCurrentPeriod.ToList()

        ListSex = New List(Of Tuple(Of Integer, String))
        ListSex.Add(New Tuple(Of Integer, String)(0, "Masculino"))
        ListSex.Add(New Tuple(Of Integer, String)(1, "Femenino"))
        ListSex.Add(New Tuple(Of Integer, String)(2, "Ambos"))
        INDsleSex.Properties.DataSource = ListSex.ToList()
        INDsleSex.EditValue = 2

        ListStatus = New List(Of Tuple(Of Boolean, String))
        ListStatus.Add(New Tuple(Of Boolean, String)(True, "Activa"))
        ListStatus.Add(New Tuple(Of Boolean, String)(False, "Inactiva"))
        INDsleStatus.Properties.DataSource = ListStatus.ToList()
        Status = True
    End Sub

    ''' <summary>
    ''' Establece el ancho de la columna mas info
    ''' </summary>
    Private Sub WidthActionsColumns()
        For Each col As DevExpress.XtraGrid.Columns.GridColumn In INDviewRules.Columns
            If col.Name = "colActions" Then
                col.Width = 100
            End If
        Next
    End Sub

#End Region

#Region "Handlers"

#Region "KeyDown"

    ''' <summary>
    ''' Evento que se dispara al presionar escape al formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddRias_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al presionar teclas en el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceRules_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDpceRules.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Enter OrElse e.KeyCode = System.Windows.Forms.Keys.F4 Then
            INDpceRules.ShowPopup()
        End If
    End Sub

#End Region

#Region "Shown"

    ''' <summary>
    ''' Evento que se dispara al pintar el form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddRias_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDsleRIAS.Focus()
    End Sub

#End Region

#Region "Load"

    ''' <summary>
    ''' Load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FrmAddRias_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ListActions As New List(Of eAcciones)
        ListActions.Add(eAcciones.Edit)
        ListActions.Add(eAcciones.Remove)
        IndigoGridView1.SetListAcction(INDviewRules, ListActions)
        WidthActionsColumns()
        INDsleRequiresMedicalOrder.EditValue = False
        Presenter = New PCupsEntity()
        InitializeTuple()
        If EditModeRIAS Then 'Si se esta editando el RIAS desde el form principal
            LoadControls()
        End If
    End Sub

#End Region

#Region "QueryPopup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el control de RIAS
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRIAS_QueryPopUp(sender As Object, e As CancelEventArgs) Handles INDsleRIAS.QueryPopUp
        If INDsleRIAS.Properties.DataSource Is Nothing Then
            INDsleRIAS.Properties.DataSource = Presenter.InitializeRIAS()
        End If
    End Sub

#End Region

#Region "Popup"

    ''' <summary>
    ''' Evento que se dispara al desplegar el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceRules_Popup(sender As Object, e As EventArgs) Handles INDpceRules.Popup
        INDseMinimunRange.Focus()
    End Sub

#End Region

#Region "EditValueChanged"

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control de reglas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDsleRules_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleRules.EditValueChanged
        If INDsleRules.EditValue IsNot Nothing Then
            Select Case INDsleRules.EditValue
                Case 1 'Sin Regla
                    INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 2 'Frecuencia por rango de edad
                    INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 3 'Frecuencia por última fecha de realización
                    INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                Case 4 'Frecuencia por periodo vigente
                    INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            End Select
        Else
            INDlyItemFrequencyRangeAge.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemPeriodQuantity.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemFrequencyEndDateRealization.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemFrequencyCurrentPeriod.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            INDlyItemFrequencyOptions.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If
    End Sub

    ''' <summary>
    ''' Evento que se dispara al cambiar el valor del control del rango edad minima
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDseMinimunRange_EditValueChanged(sender As Object, e As EventArgs) Handles INDseMinimunRange.EditValueChanged
        If INDseMinimunRange.EditValue IsNot Nothing Then
            INDseMaximunRange.Properties.MinValue = INDseMinimunRange.EditValue + 1
        End If
    End Sub

#End Region

#Region "Click"

    ''' <summary>
    ''' Evento que se dispara al presionar click sobre el boton de agregar en el popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddRange_Click(sender As Object, e As EventArgs) Handles INDbtnAddRange.Click
        AddRule()
    End Sub

    ''' <summary>
    ''' Evento que se dispara al 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDbtnAddRIAS_Click(sender As Object, e As EventArgs) Handles INDbtnAddRIAS.Click
        AddRIASToPrincipalForm()
    End Sub

#End Region

#Region "MenuContextual"

    ''' <summary>
    ''' Crea el menu contextual
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_ContexMenuActions(sender As Object, e As EventArgs) Handles IndigoGridView1.ContexMenuActions
        Select Case (sender.Tag.ToString)
            Case "Edit"
                EditRule()
            Case "Remove"
                DeleteRule()
        End Select
    End Sub

    ''' <summary>
    ''' Evento que se dispara al dar click en la lista de acciones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub IndigoGridView1_Click_ButtonAction(sender As Object, e As EventArgs) Handles IndigoGridView1.Click_ButtonAction
        Dim btn As DevExpress.XtraEditors.SimpleButton
        btn = DirectCast(sender, DevExpress.XtraEditors.SimpleButton)
        Select Case btn.Tag.ToString
            Case "Edit"
                EditRule()
            Case "Remove"
                DeleteRule()
        End Select
    End Sub

#End Region

#Region "CustomColumnDisplayText"

    ''' <summary>
    ''' Evento que se dispara al pintar las columnas en la rejilla
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDviewRules_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDviewRules.CustomColumnDisplayText
        If e.Value Is Nothing OrElse e.Value.GetType.ToString = "DevExpress.Data.NotLoadedObject" Then
            Exit Sub
        End If
        If e.Column.Name = INDcolRule.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Sin Regla"
                Case 2
                    e.DisplayText = "Frecuencia por rango de edad"
                Case 3
                    e.DisplayText = "Frecuencia por última fecha de realización"
                Case 4
                    e.DisplayText = "Frecuencia por periodo vigente"
                Case Else
                    e.DisplayText = ""
            End Select
        End If
        If e.Column.Name = INDcolUnit.Name Then
            Select Case e.Value
                Case 1
                    e.DisplayText = "Días"
                Case 2
                    e.DisplayText = "Meses"
                Case 3
                    e.DisplayText = "Años"
                Case Else
                    e.DisplayText = ""
            End Select
        End If
        If e.Column.Name = INDcolSex.Name Then
            Select Case e.Value
                Case 0
                    e.DisplayText = "Masculino"
                Case 1
                    e.DisplayText = "Femenino"
                Case 2
                    e.DisplayText = "Ambos"
                Case Else
                    e.DisplayText = ""
            End Select
        End If
        If e.Column.Name = INDcolStatus.Name Then
            Select Case e.Value
                Case 0
                    e.DisplayText = "Inactivo"
                Case 1
                    e.DisplayText = "Activo"
                Case Else
                    e.DisplayText = ""
            End Select
        End If
    End Sub

#End Region

#Region "CloseUp"

    ''' <summary>
    ''' Evento que se dispara al cerrar el control de popup
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub INDpceRules_CloseUp(sender As Object, e As DevExpress.XtraEditors.Controls.CloseUpEventArgs) Handles INDpceRules.CloseUp
        If EditModePopup AndAlso ListCupsEntityRIASDetail IsNot Nothing AndAlso ListCupsEntityRIASDetail.Count > 0 Then
            CleanControlsPopup(False)
        ElseIf EditModePopup AndAlso (ListCupsEntityRIASDetail Is Nothing OrElse ListCupsEntityRIASDetail.Count = 0) Then
            CleanControlsPopup(True)
        End If
    End Sub

#End Region

#End Region

End Class

Public Class AddCupsEntityRIAS
    Inherits EventArgs

    Property CupsEntityRIAS As CupsEntityRIAS

    Property EditMode As Boolean

End Class
