#Region "Imports"

Imports DevExpress.XtraGrid.Views.Grid
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports Presentation.Base
Imports Presentation.Contract.MVP

#End Region

Public Class FrmDashboardAlertsPGP

#Region "Variables"

    Dim _presenter As PDashboardAlertsPGP

    Dim _strFilters As String

#End Region

#Region "Properties"

    Public ReadOnly Property MyTag As Object
        Get
            Return Me.Tag
        End Get
    End Property

    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String
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

#End Region

#Region "Datasource"

    Public Property RequestsXpo As Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPRequestsXpo
        Get
            Return CType(INDGcRequests.DataSource, Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPRequestsXpo)
        End Get
        Set(value As Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPRequestsXpo)
            INDGcRequests.DataSource = value
        End Set
    End Property

    Public Property ServiceOrdersXpo As Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo
        Get
            Return CType(INDGcServiceOrders.DataSource, Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo)
        End Get
        Set(value As Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo)
            INDGcServiceOrders.DataSource = value
        End Set
    End Property

    Public Property ServiceControlsXpo As Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo
        Get
            Return CType(INDGcServiceControls.DataSource, Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo)
        End Get
        Set(value As Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo)
            INDGcServiceControls.DataSource = value
        End Set
    End Property

#End Region

#Region "Methods"

    ''' <summary>
    ''' Carga las solicitudes
    ''' </summary>
    Private Sub LoadRequests()
        If INDGcRequests.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDGvRequests.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPRequestsXpo) = Nothing
                                  Try
                                      result = _presenter.ListRequests(_strFilters)
                                      INDGcRequests.BeginInvoke(Sub()
                                                                    If result IsNot Nothing AndAlso result.Count > 0 Then
                                                                        INDGcRequests.DataSource = result
                                                                        INDLblRequestsValue.Text = result.Sum(Function(r) r.TotalCME).MoneyFormat(0)
                                                                    End If
                                                                    INDGvRequests.HideLoadingPanel()
                                                                End Sub)
                                  Catch ex As Exception
                                      INDGcRequests.BeginInvoke(Sub()
                                                                    INDGvRequests.HideLoadingPanel()
                                                                    Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga las ordenes de servicio
    ''' </summary>
    Private Sub LoadServiceOrders()
        If INDGcServiceOrders.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDGvServiceOrders.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo) = Nothing
                                  Try
                                      Dim serviceOrderFilter = String.Format("{0} AND StatusName = 'Con Orden de Servicio'", _strFilters)
                                      result = _presenter.ListServices(serviceOrderFilter)
                                      INDGcServiceOrders.BeginInvoke(Sub()
                                                                         If result IsNot Nothing AndAlso result.Count > 0 Then
                                                                             INDGcServiceOrders.DataSource = result
                                                                             INDLblServiceOrdersValue.Text = result.Sum(Function(r) r.TotalCME).MoneyFormat(0)
                                                                         End If
                                                                         INDGvServiceOrders.HideLoadingPanel()
                                                                     End Sub)
                                  Catch ex As Exception
                                      INDGcServiceOrders.BeginInvoke(Sub()
                                                                         INDGvServiceOrders.HideLoadingPanel()
                                                                         Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                     End Sub)
                                  End Try
                              End Sub)
    End Sub

    ''' <summary>
    ''' Carga los controles de servicio
    ''' </summary>
    Private Sub LoadServiceControls()
        If INDGcServiceControls.DataSource IsNot Nothing Then
            Exit Sub
        End If
        INDGvServiceControls.ShowLoadingPanel()
        Task.Factory.StartNew(Sub()
                                  Dim result As List(Of Infrastructure.Data.Xpo.ContractRepository.ViewDashboardAlertsPGPServicesXpo) = Nothing
                                  Try
                                      Dim serviceOrderFilter = String.Format("{0} AND NOT StatusName = 'Con Orden de Servicio'", _strFilters)
                                      result = _presenter.ListServices(serviceOrderFilter)
                                      INDGcServiceControls.BeginInvoke(Sub()
                                                                           If result IsNot Nothing AndAlso result.Count > 0 Then
                                                                               INDGcServiceControls.DataSource = result
                                                                               INDLblServiceControlsValue.Text = result.Sum(Function(r) r.TotalCME).MoneyFormat(0)
                                                                           End If
                                                                           INDGvServiceControls.HideLoadingPanel()
                                                                       End Sub)
                                  Catch ex As Exception
                                      INDGcServiceControls.BeginInvoke(Sub()
                                                                           INDGvServiceControls.HideLoadingPanel()
                                                                           Mensaje(EeventViewerImages.Advertencia) = Utils.GetInnerExceptionMessageToString(ex)
                                                                       End Sub)
                                  End Try
                              End Sub)
    End Sub

    Private Sub BeginReloadDatasource(selectedPage As String)
        Dim strFilter As New List(Of String)()
        Dim filterList As New List(Of String)()

        If INDSleCareGroup.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = "Debe seleccionar un grupo de atención"
            Me.INDSleCareGroup.Focus()
            Exit Sub
        End If
        If INDDeDateStart.EditValue Is Nothing Or INDDeDateEnd.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateRangeDateReport", "Commons"))
            Me.INDDeDateStart.Focus()
            Exit Sub
        ElseIf Me.INDDeDateStart.EditValue > INDDeDateEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("CompareRangeDate", "Commons"))
            Me.INDDeDateStart.Focus()
            Exit Sub
        End If

        filterList.Add(String.Format("Grupo de Atención: {0}", INDSleCareGroup.Text.Trim()))
        filterList.Add(String.Format("Desde {0} Hasta {1}", INDDeDateStart.Text, INDDeDateEnd.Text))
        INDPceFilters.Properties.NullText = $"{String.Join(", ", filterList)}"

        strFilter.Add(String.Format("CareGroupId = {0}", INDSleCareGroup.EditValue))
        strFilter.Add(String.Format("GetDate(RequestDate) >= #{0}# AND GetDate(RequestDate) <= #{1}#", Format(INDDeDateStart.EditValue, "yyyy-MM-dd"), Format(INDDeDateEnd.EditValue, "yyyy-MM-dd")))
        _strFilters = String.Join(" AND ", strFilter)

        Select Case selectedPage
            Case INDLcgRequests.Name 'Solicitudes
                RequestsXpo = Nothing
                INDLblRequestsValue.Text = 0.MoneyFormat(0)
                LoadRequests()
            Case INDLcgServiceOrders.Name 'Ordenes de servicio
                ServiceOrdersXpo = Nothing
                INDLblServiceOrdersValue.Text = 0.MoneyFormat(0)
                LoadServiceOrders()
            Case INDLcgServiceControls.Name 'Controles de servicio
                ServiceControlsXpo = Nothing
                INDLblServiceControlsValue.Text = 0.MoneyFormat(0)
                LoadServiceControls()
        End Select

        INDPceFilters.ClosePopup()
    End Sub

#End Region

#Region "Handlers"

#Region "Load"

    Private Sub FrmDashboardAlertsPGP_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.ToolBar.Hide()

        _presenter = New PDashboardAlertsPGP()
    End Sub

#End Region

#Region "Shown"

    Private Sub FrmDashboardAlertsPGP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        INDTcgDashboardAlertsPGP.SelectedTabPageIndex = 0

        INDPceFilters.Focus()
    End Sub

#End Region

#Region "QueryPopup"

    Private Sub INDSleCareGroup_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCareGroup.QueryPopUp
        If INDSleCareGroup.Properties.DataSource Is Nothing Then
            INDSleCareGroup.Properties.DataSource = _presenter.InitializeCareGroupXpo()
        End If
    End Sub

#End Region

#Region "Click"

    Private Sub INDBtnAplicar_Click(sender As Object, e As EventArgs) Handles INDBtnAplicar.Click
        BeginReloadDatasource(INDTcgDashboardAlertsPGP.SelectedTabPageName)
    End Sub

#End Region

#Region "RowCellStyle"

    Private Sub INDView_RowStyle(sender As Object, e As RowStyleEventArgs) Handles INDGvRequests.RowStyle, INDGvServiceOrders.RowStyle, INDGvServiceControls.RowStyle
        Dim view = CType(sender, GridView)
        If e.RowHandle >= 0 OrElse view.GroupCount = 0 OrElse Not view.IsGroupRow(e.RowHandle) Then
            Exit Sub
        End If

        Dim ht = view.GetGroupSummaryValues(e.RowHandle)
        If ht Is Nothing Then
            Exit Sub
        End If

        Dim row = view.GetRow(e.RowHandle)
        If row.GetType().Name = GetType(DevExpress.Data.NotLoadedObject).Name Then
            Exit Sub
        End If

        Dim quantity = 0
        Dim total = 0
        For Each htv As DictionaryEntry In ht
            Dim ggsi = CType(htv.Key, DevExpress.XtraGrid.GridGroupSummaryItem)
            If ggsi.FieldName = "Quantity" Then
                quantity = htv.Value
            ElseIf ggsi.FieldName = "TotalCME" Then
                total = htv.Value
            End If
        Next

        If quantity < row.UserMin AndAlso total < row.TotalContract Then
            e.Appearance.BackColor = System.Drawing.Color.LightGreen
            e.HighPriority = True
        ElseIf (quantity >= row.UserMin AndAlso quantity <= row.UserMax) AndAlso total <= row.TotalContract Then
            e.Appearance.BackColor = System.Drawing.Color.LightYellow
            e.HighPriority = True
        Else
            e.Appearance.BackColor = System.Drawing.Color.Salmon
            e.HighPriority = True
        End If
    End Sub

#End Region

#End Region

End Class