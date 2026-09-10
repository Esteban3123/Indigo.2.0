'***********************************************************************
' Assembly         : Presentacion.Glosas.MVP
' Author           : Jorge Leonardo Vernaza
' Created          : 03-07-2013
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Librerias imporatadas"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Controls.MVP
Imports Domain.Entities
Imports DevExpress.XtraGrid.Views.Base
Imports Presentation.Base
#End Region

''' <summary>
''' Clase con la funcionalidad del control de trazabilidad de factura
''' </summary>
Public Class CtrTraceabilityControl

#Region "Propiedades"
    Private ListControlParameterTime As List(Of ControlParametersTime)

    Private _Invoice As String
    ''' <summary>
    ''' Propiedad que obtiene o establece el numero de factura que se muestra en la trazabilidad
    ''' </summary>
    ''' <value>
    ''' La factura
    ''' </value>
    Public Property Invoice As String
        Get
            Return _Invoice
        End Get
        Set(value As String)
            _Invoice = value
        End Set
    End Property

    Private _Entity As String
    ''' <summary>
    ''' Propiedad que obtiene o establece la entidad a la cual pertenece la trazabilidad
    ''' </summary>
    ''' <value>
    ''' La factura
    ''' </value>
    Public Property Entity As String
        Get
            Return _Entity
        End Get
        Set(value As String)
            _Entity = value
        End Set
    End Property


    Private _Process As String
    ''' <summary>
    ''' Propiedad que obtiene o establece el numero de factura que se muestra en la trazabilidad
    ''' </summary>
    ''' <value>
    ''' La factura
    ''' </value>
    Public Property Process As GlosasProcess
        Get
            Return _Process
        End Get
        Set(value As GlosasProcess)
            _Process = value
        End Set
    End Property
#End Region

#Region "Fields"

    ''' <summary>
    ''' Variable que contiene los valores de session
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Metodos"
    ''' <summary>
    ''' Evento load del control donde mandamos a cargar el control
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrTraceabilityControl_Load(sender As Object, e As EventArgs) Handles Me.Load
        CargarControl()
    End Sub

    ''' <summary>
    ''' Metodo para cargar el control y el listado
    ''' </summary>
    Public Async Sub CargarControl()
        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.LayoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDLycExtemporaneous.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.INDLycRemainingText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BackColor = Color.White
        Me.LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDLycExtemporaneous.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Dim ObjectControlParametersTime As ControlParametersTime = Nothing
        If Invoice IsNot Nothing Then
            Using Model As New MTraceabilityControl
                INDpeLoad.Visible = True
                ListControlParameterTime = Await Model.GetTraceability(Invoice, Entity, Indigo.IndigoOperatingUnitId)
                INDpeLoad.Visible = False
                INDpceTraceability.Visible = True
            End Using
            Dim ListRemove As New List(Of ControlParametersTime)
            If ListControlParameterTime IsNot Nothing Then
                For Each ControlParameter As ControlParametersTime In ListControlParameterTime
                    If ControlParameter.OperationDate = New Date Then
                        ListRemove.Add(ControlParameter)
                    End If
                Next
                For Each ControlParameter As ControlParametersTime In ListRemove
                    ListControlParameterTime.Remove(ControlParameter)
                Next
                INDgcTraceability.DataSource = ListControlParameterTime
                If ListControlParameterTime.Count > 0 Then
                    Select Case Process
                        Case GlosasProcess.Objection
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "01").SingleOrDefault
                        Case GlosasProcess.ObjectionEvaluation
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "02").SingleOrDefault
                        Case GlosasProcess.ObjectionCoordination
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "03").SingleOrDefault
                        Case GlosasProcess.Reiteration
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "04").SingleOrDefault
                        Case GlosasProcess.ReiterationEvaluation
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "05").SingleOrDefault
                        Case GlosasProcess.ReiterationCoordination
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "06").SingleOrDefault
                        Case GlosasProcess.Conciliation
                            ObjectControlParametersTime = ListControlParameterTime.Where(Function(x) x.Code = "07").SingleOrDefault
                    End Select
                    If ObjectControlParametersTime IsNot Nothing Then
                        INDlblInvoice.Text = Invoice
                        'INDlblDateStart.Text = ObjectControlParametersTime.OperationDate.ToString("dd De MMM yyyy")
                        INDdateEnd.Text = ObjectControlParametersTime.OperationDate.ToString("dd De MMM yyyy") & " - " & ObjectControlParametersTime.LimitDate.ToString("dd De MMM yyyy")
                        INDlblLimit.Text = ObjectControlParametersTime.TimeParameters

                        Dim calculateDays = (ObjectControlParametersTime.LimitDate - Date.Now).TotalDays
                        If calculateDays = 0 Or calculateDays < 0 Then
                            Me.INDlblRemaining.Text = 0
                            Me.INDLycExtemporaneous.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

                            Dim expirationDays As Integer = calculateDays * -1

                            If expirationDays = 1 Then
                                Me.INDlblRemainingText.Text = expirationDays.ToString + " día de vencida"
                            Else
                                Me.INDlblRemainingText.Text = expirationDays.ToString + " días de vencida"
                            End If
                        Else
                            INDlblRemaining.Text = ObjectControlParametersTime.RemainingTime.ToString
                            Select Case ObjectControlParametersTime.RemainingTime
                                Case 1
                                    Me.INDlblRemainingText.Text = "Mañana vence"
                                Case 2
                                    Me.INDlblRemainingText.Text = "Pasado mañana vence"
                                Case Else
                                    Me.INDlblRemainingText.Text = "En " + ObjectControlParametersTime.RemainingTime.ToString + " días vence"
                            End Select
                        End If
                    End If
                End If
            End If
        End If

        Me.LayoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlItem7.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.LayoutControlItem9.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Me.INDLycRemainingText.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always

        Me.LayoutControlItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(148, Byte), Integer), CType(CType(233, Byte), Integer))
    End Sub


    ''' <summary>
    ''' Metodo para mostrar la descripcion de cada codigo
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="CustomColumnDisplayTextEventArgs"/> instance containing the event data.</param>
    Private Sub INDgcvObjetions_CustomColumnDisplayText(sender As Object, e As CustomColumnDisplayTextEventArgs) Handles INDgcvTraceability.CustomColumnDisplayText
        If e.Column.FieldName = "Code" Then
            If e.Value IsNot Nothing Then
                Select Case e.Value.ToString.Trim()
                    Case "01"
                        e.DisplayText = "Objeción"
                    Case "02"
                        e.DisplayText = "Evaluación Glosa"
                    Case "03"
                        e.DisplayText = "Coordinación Glosa"
                    Case "04"
                        e.DisplayText = "Reiteración"
                    Case "05"
                        e.DisplayText = "Evaluación Reiteración"
                    Case "06"
                        e.DisplayText = "Coordinación Reiteración"
                    Case "07"
                        e.DisplayText = "Conciliación"
                End Select
            End If
        End If
    End Sub
#End Region

   
    Private Sub LayoutControlGroup1_Click(sender As Object, e As EventArgs) Handles LayoutControlGroup1.Click, INDdateEnd.Click, INDlblInvoice.Click, INDlblLimit.Click, INDlblRemaining.Click, LabelControl1.Click, LabelControl6.Click, LayoutControlGroup1.Click, LayoutControl1.Click, LayoutControlItem1.Click, LayoutControlItem4.Click, LayoutControlItem5.Click, LayoutControlItem6.Click, LayoutControlItem7.Click, LayoutControlItem9.Click, INDLycExtemporaneous.Click, INDLycRemainingText.Click, INDlblExtemporaneous.Click, INDlblRemainingText.Click
        INDpceTraceability.ShowPopup()
    End Sub

End Class
