'***********************************************************************
' Assembly         : Presentacion.Billing
' Author           : Carlos Ernesto Córdoba
' Created          : 27-11-2015
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Billing.MVP
Imports Presentation.Inventory.MVP
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Presentation.Base.BaseClass
Imports System.ComponentModel

#End Region

Public Class FrmAdmissionProduct

#Region "GLOBALS"

    ''' <summary>
    ''' objeto de la entidad de ingreso de crystal
    ''' </summary>
    ''' <remarks></remarks>
    Dim admission As Object
#End Region

#Region "HANDLES"

#Region "Load"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        admission = Nothing
    End Sub

    Private Sub FrmAdmissionProduct_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Using model As New MServiceOrder(Me.Tag.ToString())
            INDSleAdmissionNumber.Datasource = model.GetViewAdmissionServiceOrder()
        End Using
        INDSleAdmissionNumber.PopupContainerControl = INDPccMoreInfoAdmission

        Deshacer()
    End Sub
#End Region

#Region "NewSelectedValue"
    Dim dataSource As XPInstantFeedbackSource
    Private Function SetDatasource() As Task
        Return Task.Factory.StartNew(Sub()
                                         Using model As New MAdmissionProduct(Me.Tag)
                                             dataSource = model.ListViewAdmissionProductServer(admission.AdmissionCode)
                                         End Using
                                     End Sub)
    End Function

    Private Async Sub INDSleAdmissionNumber_NewSelectedValue(sender As Object, e As SearchAdmissionClosingEventArgs) Handles INDSleAdmissionNumber.NewSelectedValue
        Me.Cursor = ChangeCursorIndigo()
        AsyncLoader(True)
        SetAdmission(e.AdmissionObject)
        Await SetDatasource()
        IndigoGridControl1.AcceptXPO = True
        INDGcProducts.DataSource = dataSource
        'If dataSource.Count > 0 Then

        '    INDGvProducts.ExpandAllGroups()
        'Else
        '    INDGcProducts.DataSource = Nothing
        'End If
        AsyncLoader(False)
        Me.Cursor = ChageCursorDefault()
    End Sub
#End Region

   
#End Region

#Region "METHODS"
    Private Sub SetAdmission(record As Object)
        admission = record
        If admission IsNot Nothing Then
            With admission
                Me.INDSleAdmissionNumber.SetNullText(String.Format(ResourceManager.GetString("AdmissionResume", "IndigoCrystalHis"), admission.AdmissionCode.ToString().Trim(), If(admission.PatientCode Is Nothing, "", admission.PatientCode.ToString().Trim()), If(admission.PatientName Is Nothing, "", admission.PatientName.ToString().Trim())))

                INDTxtAdmissionCode.Text = .AdmissionCode.ToString().Trim()
                If .AdmissionDate IsNot Nothing Then
                    INDTxtAdmissionDate.Text = CDate(.AdmissionDate).ToLongDateString()

                End If
                If .AdmissionType IsNot Nothing Then
                    INDTxtAdmissionType.Text = ResourceManager.GetString(String.Concat("AdmissionType", .AdmissionType.ToString().Trim()))
                End If
                If .AuthorizationNumber IsNot Nothing Then
                    INDTxtAuthorizationNumber.Text = .AuthorizationNumber.ToString().Trim()
                End If
                If .AdmissionType.ToString().Trim() <> ResourceManager.GetString("OutpatientRevenue", "Billing") Then
                    INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    If .BedStay IsNot Nothing Then
                        INDTxtStay.Text = .BedStay.ToString().Trim()
                    End If
                Else
                    INDLciStay.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                End If

                If .CareGroupId IsNot Nothing AndAlso .CareGroupId > 0 Then
                    Using model As New Presentation.Contract.MVP.MCareGroup(Me.Tag)
                        Dim careGroupTmp = model.GetCareGroupByIdSimple(.CareGroupId).ObjectEmbbeded
                        If careGroupTmp IsNot Nothing AndAlso careGroupTmp.Id > 0 Then
                            INDTxtBenefitsPlan.Text = careGroupTmp.Code + " - " + careGroupTmp.Name
                        Else
                            INDTxtBenefitsPlan.Text = String.Empty
                        End If
                    End Using
                End If

                If .HealthAdministratorId IsNot Nothing AndAlso .HealthAdministratorId > 0 Then
                    Using model As New Presentation.Contract.MVP.MHealthAdministrator(Me.Tag)
                        Dim healthTmp = model.GetHealthAdministratorByIdSimple(.HealthAdministratorId).ObjectEmbbeded
                        If healthTmp IsNot Nothing AndAlso healthTmp.Id > 0 Then
                            INDTxtEntity.Text = healthTmp.Code + " - " + healthTmp.Name
                        Else
                            INDTxtEntity.Text = String.Empty
                        End If
                    End Using
                End If
                If .LiquidationType IsNot Nothing Then
                    INDTxtLiquidationType.Text = ResourceManager.GetString(String.Concat("LiquidationType", .LiquidationType.ToString().Trim()))
                End If
                If .PatientCode IsNot Nothing And .PatientName IsNot Nothing Then
                    INDTxtPatient.Text = .PatientCode.ToString().Trim() + " - " + .PatientName.ToString().Trim()
                End If
                If .PlaceEntry IsNot Nothing Then
                    INDTxtAdmissionPlace.Text = ResourceManager.GetString(String.Concat("PlaceEntry", .PlaceEntry.ToString().Trim()))
                End If
                If .ResponsibleName IsNot Nothing Then
                    INDTxtResponsibleName.Text = .ResponsibleName.ToString().Trim()
                End If
                If .ResponsiblePhone IsNot Nothing Then
                    INDTxtResponsiblePhone.Text = .ResponsiblePhone.ToString().Trim()
                End If
            End With
        End If
    End Sub

    Public Sub Deshacer()
        CleanControls()
        Me.BarraBotones.DisableBarDocument()
        Me.BarraBotones.PrepareToolbar(eAction.OnlyUndo)
    End Sub

    Private Sub CleanControls()
        Me.INDSleAdmissionNumber.SetNullText(String.Empty)
        INDGcProducts.DataSource = Nothing
        IndigoGridControl1.RefreshGrid(INDGcProducts)
    End Sub
#End Region

#Region "BAR BUTTONS"

    ''' <summary>
    ''' Barras the botones_ click deshacer.
    ''' </summary>
    Private Sub BarraBotones_ClickDeshacer() Handles BarraBotones.ClickDeshacer
        Deshacer()
    End Sub
#End Region

    Private Sub RepositoryItemPopupContainerEdit1_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles RepositoryItemPopupContainerEdit1.QueryPopUp

        Dim detail = DirectCast(DirectCast(INDGvProducts.GetFocusedRow, DevExpress.Data.Async.Helpers.ReadonlyThreadSafeProxyForObjectFromAnotherThread).OriginalRow, InventoryViewAdmissionProductXpo)
        'Dim detail = DirectCast(INDGvProducts.GetFocusedRow, InventoryViewAdmissionProductXpo)
        Using model As New MAdmissionProduct(Me.Tag)
            INDGcDetail.DataSource = model.ListViewAdmissionProductDetail(admission.AdmissionCode, detail.FunctionalUnitId, detail.ProductId)
        End Using
    End Sub
End Class


