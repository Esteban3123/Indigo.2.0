'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 14-03-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "imports"
Imports DevExpress.XtraScheduler
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler.UI
#End Region

''' <summary>
''' Clase que contiene vista de dialog ir a fecha
''' </summary>
''' <remarks></remarks>
Public Class FrmGoToDate
    Inherits GotoDateForm

#Region "Properties"
    ''' <summary>
    ''' Propiedad que contiene el valor de fecha del control
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property DateToGo As DateTime
        Get
            Return INDdeDate.DateTime
        End Get
        Set(value As DateTime)
            INDdeDate.DateTime = value
        End Set
    End Property
#End Region

#Region "Constructor"
    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <param name="view"></param>
    ''' <param name="_dateInput"></param>
    ''' <param name="viewType"></param>
    ''' <remarks></remarks>
    Sub New(view As SchedulerViewRepository, _dateInput As DateTime, viewType As SchedulerViewType)
        MyBase.New(view, _dateInput, viewType)
        InitializeComponent()
        DateToGo = _dateInput
    End Sub
#End Region

#Region "Eventos"
    ''' <summary>
    ''' Para validar la seleccion de fecha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub edtDate_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDdeDate.Validating
        Dim editor As DateEdit = TryCast(sender, DateEdit)
        If editor.EditValue Is Nothing Then
            editor.EditValue = Me.[Date]
        End If
        DateToGo = editor.DateTime
    End Sub

    ''' <summary>
    ''' Evento click del boton ir
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbOK_Click(sender As Object, e As EventArgs) Handles INDsbOK.Click
        Me.[Date] = DateToGo
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
    End Sub

    ''' <summary>
    ''' Evento click cancel del boton
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsbCancel_Click(sender As Object, e As EventArgs) Handles INDsbCancel.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    End Sub

    ''' <summary>
    ''' Para capturar cuando presionan enter y enviar a buscar fecha
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDdeDate_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles INDdeDate.KeyDown
        'If e.KeyCode = System.Windows.Forms.Keys.Enter Then
        '    INDsbOK.Focus()
        '    Me.DialogResult = System.Windows.Forms.DialogResult.OK
        'End If
    End Sub

    ''' <summary>
    ''' Evita el enter para cerrar el dialog, y pasa al boton de ir
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Overrides Sub OnPreviewKeyDown(e As System.Windows.Forms.PreviewKeyDownEventArgs)
        If e.KeyCode = System.Windows.Forms.Keys.Enter Then
            INDsbOK.Focus()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            'Return
        End If

        MyBase.OnPreviewKeyDown(e)
    End Sub
#End Region


    
End Class