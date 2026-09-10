'***********************************************************************
' Assembly         : Presentacion.Payroll
' Author           : Kevin Garay Rodriguez
' Created          : 06-11-2013
'
' Last Modified By :
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Presentation.Base
Imports Presentation.Base.BaseClass
Imports Presentation.Base.Eform
Imports Presentation.Base.Eresources
Imports Presentation.Controls
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.CrossCutting.Exceptions
Imports Domain.Payroll.Entities
Imports Presentation.Payroll.MVP
Imports DevExpress.Data.Async.Helpers
Imports Infrastructure.Data.Xpo.PayrollRepository

#End Region
''' <summary>
''' Clase que tiene el comportamiento de la vista en el formulario sucursales
''' </summary>
Public Class FrmBranchOfficeMain
    Implements IBranchOfficeMain

#Region "Variables"
    Dim dtFieldsCustomizables As DataTable
    ''' <summary>
    ''' Bandera que se utiliza para verificar si existe o no una definicion del funcional 
    ''' </summary>
    Dim ExistDefinitionFront As Boolean
    ''' <summary>
    ''' variable que se utiliza para instanciar los valores de session
    ''' </summary>
    Dim indigo As SessionValues = SessionValues.Instance
    ''' <summary>
    ''' Variable que contiene la ruta de las definiciones del layout
    ''' </summary>
    Dim PathFunctionalDefinitions As String
    ''' <summary>
    ''' Evento que se utiliza para cargar las definiciones del funcional
    ''' </summary>
    WithEvents LoadhronousDefinitions As BackgroundWorker
    ''' <summary>
    ''' VAriable para utilizar el presentador del frontal
    ''' </summary>
    ''' <remarks></remarks>
    Dim Presenter As PBranchOfficeMain
#End Region

#Region "Properties"
    ''' <summary>
    ''' Establece el datasource de las compañias
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public WriteOnly Property Company As DevExpress.Xpo.XPInstantFeedbackSource Implements IBranchOfficeMain.Company
        Set(value As DevExpress.Xpo.XPInstantFeedbackSource)
            INDSleCompany.Properties.DataSource = value
        End Set
    End Property
#End Region

#Region "Events"
    Private Sub Frm_Disposed(sender As Object, e As EventArgs) Handles MyBase.Disposed
        dtFieldsCustomizables = Nothing
        ExistDefinitionFront = Nothing
        PathFunctionalDefinitions = Nothing
        Presenter = Nothing
    End Sub
    ''' <summary>
    ''' Evento que controla el cambio de empresa y dibujar las sucursales
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Async Sub INDSleCompany_EditValueChanged(sender As Object, e As EventArgs) Handles INDSleCompany.EditValueChanged
        AsyncLoader(True)
        If INDSleCompany.EditValue IsNot Nothing Then
            Dim companyXPO As PayrollCompanyXpo = GetOriginalRow()
            Using model As New MBranchOffice
                INDgcBranchOffice.DataSource = Await model.GetBranchOfficeByCompanyIdAsync(companyXPO.Id)
            End Using
        End If
        AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Evento para pintar la cuidad y departamento 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDgvBranchOffice_CustomColumnDisplayText(sender As Object, e As DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs) Handles INDgvBranchOffice.CustomColumnDisplayText
        If e.Column.Name = INDColLocationBO.Name Then
            Dim _row As BranchOffice = INDgvBranchOffice.GetRow(e.ListSourceRowIndex)
            e.DisplayText = _row.City.Name '& " - " & _row.City.
        End If
        If e.Column.Name = INDColActions.Name Then
            e.DisplayText = RepLinkEdit.Caption
        End If
    End Sub

    ''' <summary>
    ''' Evento click de la columna de editar
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RepositoryItemHyperLinkEdit1_Click(sender As Object, e As EventArgs) Handles RepLinkEdit.Click
        Dim _row As BranchOffice = INDgvBranchOffice.GetRow(INDgvBranchOffice.FocusedRowHandle)
        If _row.Code Is String.Empty Then
            Exit Sub
        End If
        'AsyncLoader(True)
        Using formBranchOffice As New FrmBranchOffice
            formBranchOffice.ViewModeEditHold = True
            Dim transparent As New FrmTransparent(formBranchOffice, False)


            formBranchOffice.Deshacer()
            formBranchOffice.Code = _row.Code
            formBranchOffice.CompanyId = _row.CompanyId
            'formBranchOffice.ShowDialog()
            transparent.ShowDialog()
            INDSleCompany_EditValueChanged(Nothing, Nothing)
            INDgcBranchOffice.RefreshDataSource()
            formBranchOffice.Dispose()
        End Using
        ' AsyncLoader(False)
    End Sub

    ''' <summary>
    ''' Click de boton Nuevo para agregar una sucursal
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub INDSbNewBO_Click(sender As Object, e As EventArgs) Handles INDSbNewBO.Click
        Using formBranchOffice As New FrmBranchOffice
            If INDSleCompany.EditValue IsNot Nothing Then
                Dim companyXPO As PayrollCompanyXpo = GetOriginalRow()
                formBranchOffice.CompanyId = companyXPO.Id
            End If
            formBranchOffice.ViewModeEditHold = True
            Dim transparent As New FrmTransparent(formBranchOffice, False)
            transparent.ShowDialog()
            'WithOpacity()
            'formBranchOffice.ShowDialog()
            INDSleCompany_EditValueChanged(Nothing, Nothing)
            INDgcBranchOffice.RefreshDataSource()
            formBranchOffice.Dispose()
        End Using
        'WithOutOpacity()
    End Sub

    ''' <summary>
    ''' Evento load del formulario
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FrmBranchOfficeMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Cargamos de manera asincrona definiciones del funcional
        PathFunctionalDefinitions = String.Concat(indigo.CommonFilesPath, "\XML\FuncionalesCustomizables\", Me.Name, "\ModuloPayrollBranchOffice.", Me.Name, ".xml")
        LoadhronousDefinitions = New BackgroundWorker
        If LoadhronousDefinitions.IsBusy = False Then
            LoadhronousDefinitions.RunWorkerAsync()
        End If
        Presenter = New PBranchOfficeMain(Me)
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Metodo que Obtiene el registro de la rejilla
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetOriginalRow() As PayrollCompanyXpo
        Dim ObjectSelected = CType(INDSleGvCompany.GetRow(INDSleGvCompany.FocusedRowHandle()), ReadonlyThreadSafeProxyForObjectFromAnotherThread)
        Return CType(ObjectSelected.OriginalRow, PayrollCompanyXpo)
    End Function

    ''' <summary>
    ''' Metodo para limpiar controles
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CleanControls()
        INDgcBranchOffice.DataSource = Nothing
    End Sub

    Private Sub WithOpacity()
        Me.Opacity = 50
    End Sub

    Private Sub WithOutOpacity()
        Me.Opacity = 100
    End Sub
#End Region

End Class