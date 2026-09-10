Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Utils
Imports DevExpress.XtraGrid.Localization
Imports System.Drawing
Imports DevExpress.XtraGrid
Imports DevExpress.XtraEditors
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Base

Public Class GridViewXpoErrors
    Inherits DevExpress.XtraGrid.Views.Grid.GridView
    Private lastError As String = String.Empty

    Public Sub New()
        Me.New(Nothing)
    End Sub
    Public Sub New(ByVal grid As DevExpress.XtraGrid.GridControl)
        MyBase.New(grid)
    End Sub

    Protected Overrides ReadOnly Property ViewName() As String
        Get
            Return "MyGridView"
        End Get
    End Property

    'Protected Overrides Sub CheckDataControllerError()
    '    ' show a tooltip
    '    ShowDataControllerError()
    'End Sub

    Protected Overrides Sub OnDataControllerError(ByVal lastError As String)
        'Dim frm As XtraMessageBoxForm = New XtraMessageBoxForm
        'frm.TopMost = True
        'frm.ShowMessageBoxDialog(New XtraMessageBoxArgs(DevExpress.LookAndFeel.UserLookAndFeel.Default, Nothing, obtenerRecurso(XpoGridErrorMessage, Comunes), obtenerRecurso(XpoGridErrorTitle, Comunes), New DialogResult() {DialogResult.OK}, SystemIcons.Warning, 0))
        Using pop As New FrmTransparent(New FrmMensajeIndigo(obtenerRecurso(XpoGridErrorMessage, Comunes), Infrastructure.CrossCutting.Base.Botones.Ok, Infrastructure.CrossCutting.Base.MessageType.Warning), True)
            pop.Show()
        End Using
    End Sub


    Protected Shadows Sub ShowDataControllerError()
        If GridControl Is Nothing OrElse (Not GridControl.IsHandleCreated) Then
            Return
        End If

        If lastError <> DataController.LastErrorText Then
            lastError = DataController.LastErrorText
            If (Not String.IsNullOrEmpty(lastError)) Then
                OnDataControllerError(lastError)
            End If
        End If
        If DataController.LastErrorText = "" Then
            If GridControl.EditorHelper.RealToolTipController.ActiveObject Is Nothing Then
                HideHint()
            End If
            Return
        End If
        lastError = DataController.LastErrorText
    End Sub

    Public Shadows Property GridControl() As GridControlXpoErrors
        Get
            Return TryCast(MyBase.GridControl, GridControlXpoErrors)
        End Get
        Set(ByVal value As GridControlXpoErrors)
            MyBase.GridControl = value
        End Set
    End Property
End Class
