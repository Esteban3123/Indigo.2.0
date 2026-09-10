'***********************************************************************
' Assembly         : Presentacion.Inventory
' Author           : Carlos Ernesto Córdoba
' Created          : 27-03-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Billing.MVP
#End Region

Public Class PopupApplyProcedure

#Region "EVENTS"
    Public Event SelectProcedure(sender As Object, e As SelectProcedureEventArgs)
#End Region

#Region "GLOBALS"
    Dim _admission As String

    Dim listServiceOrderDetail As List(Of ServiceOrderDetail)
#End Region

#Region "PROPERTIES"
    Public WriteOnly Property Admission As String
        Set(value As String)
            _admission = value
        End Set
    End Property
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub PopupApplyProcedure_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        INDGvProcedure.OptionsView.ShowAutoFilterRow = False
    End Sub
#End Region

#Region "Shown"
    Private Sub PopupCUM_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Using model As New MServiceOrder(Me.Tag)
            listServiceOrderDetail = model.ListServiceOrderDetailsByAdmissionNumber(_admission)
            If listServiceOrderDetail.Count > 0 Then
                INDGcProcedure.DataSource = Nothing
                INDGcProcedure.DataSource = listServiceOrderDetail
                INDGvProcedure.ExpandAllGroups()
            Else
                IndigoGridControl1.RefreshGrid(INDGcProcedure)
                INDLciAdd.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End Using
    End Sub
#End Region

#Region "Click"
    Private Sub INDBtnOk_Click(sender As Object, e As EventArgs) Handles INDBtnOk.Click
        Dim procedure = DirectCast(INDGvProcedure.GetFocusedRow, ServiceOrderDetail)
        If procedure IsNot Nothing Then
            Dim args As New SelectProcedureEventArgs
            args.ApplyProcedureId = procedure.Id
            args.CodeNameApplyProcedure = procedure.CodeNameCups
            args.StatusResult = True
            RaiseEvent SelectProcedure(Nothing, args)
        End If
        Me.Close()
    End Sub
#End Region

#Region "KeyDown"
    Private Sub PopupCUM_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = System.Windows.Forms.Keys.Escape Then
            If listServiceOrderDetail.Count > 0 Then
                e.SuppressKeyPress = True
            Else
                Dim args As New SelectProcedureEventArgs
                args.StatusResult = False
                RaiseEvent SelectProcedure(Nothing, args)
                Me.Close()
            End If
        End If
    End Sub
#End Region

#Region "DoubleClick"
    Private Sub INDGcProcedure_DoubleClick(sender As Object, e As EventArgs) Handles INDGcProcedure.DoubleClick
        Dim pMouse As System.Drawing.Point = MousePosition
        Dim hit = INDGvProcedure.CalcHitInfo(INDGcProcedure.PointToClient(pMouse))
        If hit IsNot Nothing AndAlso hit.InRow = True Then
            Dim procedure = DirectCast(INDGvProcedure.GetFocusedRow, ServiceOrderDetail)
            Dim args As New SelectProcedureEventArgs
            args.ApplyProcedureId = procedure.Id
            args.CodeNameApplyProcedure = procedure.CodeNameCups
            args.StatusResult = True
            RaiseEvent SelectProcedure(Nothing, args)
            Me.Close()
        End If
    End Sub
#End Region

#End Region

End Class

''' <summary>
''' Clase para el retorno del evento para aplicar un procedimiento
''' </summary>
''' <remarks></remarks>
Public Class SelectProcedureEventArgs
    Inherits EventArgs

    Property ApplyProcedureId As Integer?

    Property CodeNameApplyProcedure As String

    Property StatusResult As Boolean
End Class