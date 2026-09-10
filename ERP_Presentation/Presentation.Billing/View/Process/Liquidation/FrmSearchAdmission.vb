'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Juan F. Tamayo
' Created          : 2014-11-12
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Presentation.Base
Imports Presentation.Billing.MVP
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Controls
Imports DevExpress.XtraLayout
Imports System.Drawing
Imports Presentation.Controls
Imports DevExpress.Xpo

#End Region

Public Class FrmSearchAdmission

#Region "Fields"

    ''' <summary>
    ''' Número del ingreso seleccionado
    ''' </summary>
    Private _admissionCode As Object

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _admissionCode = Nothing
    End Sub


    ''' <summary>
    ''' Se lanza cuando se cerrando el frontal
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Public Event SearchAdmissionClosing(ByVal sender As Object, ByVal e As EditValueChangedEventArgs)

#End Region

#Region "Properties"

    ''' <summary>
    ''' Obtiene el número de ingreso seleccionado
    ''' </summary>
    ''' <returns>Número de ingreso seleccionado</returns>
    Public ReadOnly Property AdmissionCode As Object
        Get
            Return Me._admissionCode
        End Get
    End Property

#End Region

#Region "Builders"

    ''' <summary>
    ''' Inicializa una nueva instancia de la clase
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Me._admissionCode = String.Empty
    End Sub

#End Region

#Region "Handlers"

    ''' <summary>
    ''' Se lanza el evento cuando el frontal se cierra
    ''' </summary>
    Private Sub FrmSearchAdmission_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.OnSearchAdmissionReturned(Me, New SearchAdmissionClosingEventArgs(Me._admissionCode))
    End Sub

    ''' <summary>
    ''' Aqui se le da estilo al panel de busqueda y se le asigna el foco
    ''' </summary>
    Private Sub FrmSearchAdmission_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.SetStyleFindPanel()
        Using m As New MLiquidation()
            Me.GdcSearch.DataSource = m.ListAdmissionsToLiquidation()
        End Using
    End Sub

    ''' <summary>
    ''' Ejecutamos el aceptar para seleccionar el ingreso
    ''' </summary>
    Private Sub GdvSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles GdvSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me.Acept()
        End If
    End Sub

    ''' <summary>
    ''' Aqui retornamos el número de ingreso del registro seleccionado
    ''' </summary>
    Private Sub GdvSearch_DoubleClick(sender As Object, e As EventArgs) Handles GdvSearch.DoubleClick
        Dim pMouse As Drawing.Point = Control.MousePosition
        Dim obj = Me.GdvSearch.CalcHitInfo(pMouse)
        If obj IsNot Nothing AndAlso obj.InDataRow Then
            Me.Acept()
        End If
    End Sub

    ''' <summary>
    ''' Aqui se le asigna el foco al control de busqueda
    ''' </summary>
    Private Sub GdvSearch_AsyncCompleted(sender As Object, e As EventArgs) Handles GdvSearch.AsyncCompleted
        'Me.SetFocusFindControl()
    End Sub

    ''' <summary>
    ''' Ejecuta el cerrado del frontal
    ''' </summary>
    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Me.Cancel()
    End Sub

    ''' <summary>
    ''' Ejecuta el aceptado de un ingreso
    ''' </summary>
    Private Sub BtnOpen_Click(sender As Object, e As EventArgs) Handles BtnOpen.Click
        Me.Acept()
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Lanza el evento cuando se ha seleccionado un ingreso y se retorna su número
    ''' </summary>
    ''' <param name="sender">Objeto quien lanza el evento</param>
    ''' <param name="e">Argumentos del evento</param>
    Protected Sub OnSearchAdmissionReturned(ByVal sender As Object, ByVal e As SearchAdmissionClosingEventArgs)
        RaiseEvent SearchAdmissionClosing(sender, New EditValueChangedEventArgs(Nothing, e.AdmissionObject))
    End Sub

    ''' <summary>
    ''' Cierra el frontal al presionar la tecla Escape
    ''' </summary>
    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Escape Then
            Me.Cancel()
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    ''' <summary>
    ''' Obtiene el número de ingreso seleccionado
    ''' </summary>
    Private Sub Acept()
        If Me.GdcSearch.DataSource IsNot Nothing Then
            Dim objTemp = Me.GdvSearch.GetFocusedRow()
            If objTemp IsNot Nothing Then
                Dim obj = DirectCast(objTemp, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread)
                If obj IsNot Nothing AndAlso obj.OriginalRow IsNot Nothing Then
                    Me._admissionCode = obj.OriginalRow
                    Me.Close()
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Cierra el frontal
    ''' </summary>
    Private Sub Cancel()
        Me.Close()
    End Sub

    ''' <summary>
    ''' Asigna el foco a la caja de busqueda
    ''' </summary>
    Private Sub SetFocusFindControl()
        Dim findPanel As FindControl = TryCast(Me.GdcSearch.Controls("FindControl"), FindControl)
        Dim layout As LayoutControl = TryCast(findPanel.Controls(0), LayoutControl)
        Dim item As LayoutControlItem = TryCast(layout.Items(2), LayoutControlItem)
        item.Control.Focus()
    End Sub

    ''' <summary>
    ''' Asigna estilo al panel de busqueda de la regilla
    ''' </summary>
    Private Sub SetStyleFindPanel()
        For Each c As Control In Me.GdcSearch.Controls
            If c.GetType().Equals(GetType(DevExpress.XtraGrid.Controls.FindControl)) Then
                CType(c, DevExpress.XtraGrid.Controls.FindControl).Appearance.BackColor = Color.White
                CType(c, DevExpress.XtraGrid.Controls.FindControl).FindEdit.Font = New Font("Segoe UI Light", 12.0!)
                CType(c, DevExpress.XtraGrid.Controls.FindControl).FindButton.Font = New Font("Segoe UI Light", 12.0!)
                CType(c, DevExpress.XtraGrid.Controls.FindControl).FindButton.Size = New Size(100, 36)
                CType(c, DevExpress.XtraGrid.Controls.FindControl).ClearButton.Font = New Font("Segoe UI Light", 12.0!)
                CType(c, DevExpress.XtraGrid.Controls.FindControl).ClearButton.Size = New Size(100, 36)
                CType(c, DevExpress.XtraGrid.Controls.FindControl).MinimumSize = New Size(CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Width, CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Height + 30)
                CType(c, DevExpress.XtraGrid.Controls.FindControl).MaximumSize = New Size(CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Width, CType(c, DevExpress.XtraGrid.Controls.FindControl).Size.Height + 30)
            End If
        Next
    End Sub

#End Region

End Class