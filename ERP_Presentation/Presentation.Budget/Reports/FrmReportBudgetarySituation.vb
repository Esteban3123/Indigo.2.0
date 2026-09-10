#Region "Imports"
Imports Presentation.Reporter
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo
Imports DevExpress.Data.Linq
Imports Presentation.Base
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Columns
Imports Presentation.Common.MVP
Imports Infrastructure.Data.Xpo
Imports Presentation.Budget.MVP

#End Region

Public Class FrmReportBudgetarySituation

#Region "Fields"

    ''' <summary>
    ''' Referencia al modelo de PUC
    ''' </summary>
    Private _pucModel As MCommon

#End Region

#Region "properties"

    Public Property ProoftCloseXpoCategory As XPInstantFeedbackSource

    ''' <summary>
    ''' propiedad para registar el mensaje en el visor
    ''' </summary>
    ''' <param name="Icono"></param>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Mensaje(Icono As EeventViewerImages) As String 'Implements IcrudBase.Mensaje
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
    ''' Obtiene o establece el listado de las vigencias
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityXpo As DevExpress.Xpo.XPCollection
        Get
            Return INDsleValidity.Properties.DataSource
        End Get
        Set(value As DevExpress.Xpo.XPCollection)
            INDsleValidity.Properties.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Obtiene o establece el id de la vigencia al cual pertenece
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BudgetaryValidityId As Integer
        Get
            Return INDsleValidity.EditValue
        End Get
        Set(value As Integer)
            INDsleValidity.EditValue = value
        End Set
    End Property

    Private criteria As String = Nothing

#End Region

#Region "Methods"

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        If BudgetaryValidityXpo IsNot Nothing AndAlso BudgetaryValidityXpo.Count > 0 Then
            Dim item = (From l In BudgetaryValidityXpo Where l.Status = 2 Select l).FirstOrDefault
            If item IsNot Nothing Then
                BudgetaryValidityId = item.Id
                CleanControlsSelectValidity()
            End If
        End If
    End Sub
    ''' <summary>
    ''' Metodo para habilitar botones y limpiar campos
    ''' </summary>
    Sub CleanControlsSelectValidity()
        INDSleCategoryStart.Enabled = True
        INDSleCategoryEnd.Enabled = True
        INDSbGenerateReport.Enabled = True
        INDSleCategoryStart.EditValue = Nothing
        INDSleCategoryEnd.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' propiedad para Realizar las validaciones del formulario
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ValidateControlsReports()

        Dim Validations As Boolean = True
        If INDsleValidity.EditValue Is Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("FieldEmptyName", "Commons"), INDlciValidity.Text)
            Me.INDsleValidity.Focus()
            Validations = False
        End If
        'validaciones de rangos
        If INDSleCategoryStart.EditValue IsNot Nothing And INDSleCategoryEnd.EditValue Is Nothing Or INDSleCategoryStart.TextEditValue Is Nothing And INDSleCategoryEnd.TextEditValue IsNot Nothing Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("SelectRegisterStartEnd", "Commons"), INDLblCategory.Text)
            Me.INDSleCategoryStart.Focus()
            Validations = False
        ElseIf INDSleCategoryStart.EditValue > INDSleCategoryEnd.EditValue Then
            Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("RegisterEndStart", "Commons"), INDLblCategory.Text)
            Me.INDSleCategoryStart.Focus()
            Validations = False
        End If

        Return Validations

    End Function

    ''' <summary>
    ''' Carga el datasource de rubro inicial
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadXpoCategoryStart()
        criteria = "ItemType = 2 And Status = True And FinancialSourceId is not null"

        If INDsleValidity.EditValue IsNot Nothing Then
            criteria &= " And BudgetaryValidityId.Id =" & INDsleValidity.EditValue
        End If

        INDSleCategoryStart.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCategoryByFilter(criteria)
    End Sub

    ''' <summary>
    ''' carga el datasource de rubro final
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LoadXpoCategoryEnd()
        criteria = "ItemType = 2 And Status = True And FinancialSourceId is not null"

        If INDsleValidity.EditValue IsNot Nothing Then
            criteria &= " And BudgetaryValidityId.Id =" & INDsleValidity.EditValue
        End If

        INDSleCategoryEnd.Datasource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListCategoryByFilter(criteria)
    End Sub

#End Region

#Region "Events"

    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        _model.Dispose()
        _model = Nothing
    End Sub


    ''' <summary>
    ''' se ejecuta en el evento click del boton generar reporte
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSbGenerateReport_Click(sender As Object, e As EventArgs) Handles INDSbGenerateReport.Click
        If Me.ValidateControlsReports = True Then
            AsyncLoader(True)
            Dim reporte As New Presentation.Reporter.rptReportBudgetarySituation()
            reporte.ParametrosReporte = New Object() {INDsleValidity.EditValue,
                                                      INDSleCategoryStart.EditValue,
                                                      INDSleCategoryEnd.EditValue}

            INDDvViewReport.DocumentSource = reporte
            Await reporte.CargarDataSource1()
            reporte.CreateDocument(True)
            AsyncLoader(False)
            If reporte.DataSource IsNot Nothing Then
                Me.INDLcBase.Visible = False
                Me.INDCncNavigation.Visible = False
                Me.INDPcViewReport.Visible = True
                INDDvViewReport.Show()
            Else
                Mensaje(EeventViewerImages.Advertencia) = String.Format(ResourceManager.GetString("ValidateDataSource", "Commons"))
                Me.INDSleCategoryStart.Focus()
            End If
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento ClickBack en el control INDCnReturn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub INDCnReturn_ClickBack() Handles INDCnReturn.ClickBack
        Me.INDLcBase.Visible = True
        Me.INDCncNavigation.Visible = True
        Me.INDPcViewReport.Visible = False
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCategoryEnd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCategoryEnd_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCategoryEnd.QueryPopUp
        If INDSleCategoryEnd.Datasource Is Nothing Then
            LoadXpoCategoryEnd()
        End If
    End Sub

    ''' <summary>
    ''' se ejecuta en el evento querypopup del control INDSleCategoryStart
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSleCategoryStart_QueryPopUp(sender As Object, e As ComponentModel.CancelEventArgs) Handles INDSleCategoryStart.QueryPopUp
        If INDSleCategoryStart.Datasource Is Nothing Then
            LoadXpoCategoryStart()
        End If
    End Sub

    ''' <summary>
    ''' carga el datasource de entidad
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_QueryPopUp(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles INDsleEntity.QueryPopUp
        If INDsleEntity.Properties.DataSource Is Nothing Then
            INDsleEntity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListBudgetEntityByStatus(True)
        End If
    End Sub
    ''' <summary>
    ''' Funcion para cargar la barra de botones
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub BarraBotones_Load(sender As Object, e As EventArgs) Handles BarraBotones.Load
        BarraBotones.ActualizarPermisosBarra(MyBase.Tag)
    End Sub

    ''' <summary>
    ''' evento para cargar resolucion , valor y estado.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If INDsleEntity.EditValue IsNot Nothing Then
            INDsleValidity.Properties.DataSource = XpoServiceEx.Instance(indigo.TransactionalContainer).BudgetService.ListValidityByBudgetEntityId(INDsleEntity.EditValue)
            SetFirstOrDefaultValidity()
        End If
    End Sub

    ''' <summary>
    ''' se dispara al cambiar el valor de la vigencia
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDsleValidity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleValidity.EditValueChanged
        If INDsleValidity.EditValue IsNot Nothing Then
            CleanControlsSelectValidity()
            Me._pucModel.ValidityId = INDsleValidity.EditValue
            Dim item = (From l In BudgetaryValidityXpo Where l.Id = INDsleValidity.EditValue Select l).FirstOrDefault
        End If
    End Sub

    ''' <summary>
    ''' se inicializan los miembros
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmReportTrialBalance_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Inicializamos la referencia al modelo de puc
        Me._pucModel = New MCommon(Me.Tag)
        Me.INDSleCategoryStart.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryBudgetByCode
        Me.INDSleCategoryEnd.FuncQueryOnKeyEnterPressed = AddressOf Me._pucModel.GetCategoryBudgetByCode

        INDSleCategoryStart.View.OptionsView.ShowGroupPanel = False
        INDSleCategoryEnd.View.OptionsView.ShowGroupPanel = False
    End Sub

#End Region

End Class