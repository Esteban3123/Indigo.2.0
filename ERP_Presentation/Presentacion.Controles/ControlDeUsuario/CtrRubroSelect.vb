Imports Presentation.Base
Imports Domain.Entities
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Base
Imports System.ComponentModel
Imports Infrastructure.Data.Xpo.BudgetRepository

Public Class CtrRubroSelect

#Region "Events"

    ''' <summary>
    ''' Occurs when [close popup].
    ''' </summary>
    Public Event ReturnValue()

#End Region

#Region "Properties and Variables"

    ''' <summary>
    ''' valor que devuelve al dar doble click sobre las rejillas
    ''' </summary>
    Private _codeRubro As String

#End Region

#Region "Eventos Controles"

    ''' <summary>
    ''' Handles the Load event of the CtrRubroSelect control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub CtrRubroSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If DesignMode = False Then
            IndigoGridControl1.RefreshGrid(INDGcRubro)
            INDsleVigence.Enabled = False
            InitializeBudgetEntity()
        End If
    End Sub

    ''' <summary>
    ''' Handles the ButtonClick event of the INDsleVigence control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="DevExpress.XtraEditors.Controls.ButtonPressedEventArgs"/> instance containing the event data.</param>
    Private Sub INDsleVigence_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles INDsleVigence.ButtonClick
        If e.Button.Kind.ToString().Equals("OK") Then
            ListRubro()
            INDsleEntity.EditValue = Nothing
            INDsleVigence.EditValue = Nothing
            INDsleVigence.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Handles the EditValueChanged event of the INDsleEntity control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDsleEntity_EditValueChanged(sender As Object, e As EventArgs) Handles INDsleEntity.EditValueChanged
        If INDsleEntity.EditValue IsNot Nothing Then
            If INDsleEntity.EditValue.ToString <> "" Then
                INDsleVigence.Enabled = True
                If DesignMode = False Then
                    Using modelB As New MBusqueda
                        INDsleVigence.Properties.DataSource = modelB.ConsultarEntidades(eDataSource.ListValidityByEntity, CStr(INDsleEntity.EditValue))
                        SetFirstOrDefaultValidity()
                    End Using
                End If
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles the DoubleClick event of the INDGvRubro control.
    ''' </summary>
    ''' <param name="sender">The source of the event.</param>
    ''' <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    Private Sub INDGvRubro_DoubleClick(sender As Object, e As EventArgs) Handles INDGvRubro.DoubleClick
        'debe ir un condicional primero
        _codeRubro = CType(INDGvRubro.GetRow(INDGvRubro.FocusedRowHandle), RevenueType).Code
        SendKeys.Send("{ESC}")  'cierra el popup
        RaiseEvent ReturnValue()
    End Sub

#End Region

#Region "Methods and Functions"

    ''' <summary>
    ''' Adds the rubro.
    ''' </summary>
    Private Async Sub ListRubro()
        'Consulta rubro (Category) por Id de vigencia (BudgetaryValidity)
        Using Model As New MEarningsTypeControl()
            Dim Lista As List(Of RevenueType) = Await Model.ListEarningsTypeByValidityAsync(INDsleVigence.EditValue)
            Me.INDGcRubro.DataSource = Lista
        End Using
    End Sub

    ''' <summary>
    ''' Metodo para seleccionar por defecto el primer registro si solo hay uno en vigencias
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetFirstOrDefaultValidity()
        Dim listValidity As DevExpress.Xpo.XPServerCollectionSource = CType(INDsleVigence.Properties.DataSource, DevExpress.Xpo.XPServerCollectionSource)
        Dim Ilist As IListSource = TryCast(listValidity, IListSource)
        If Ilist.GetList().Count = 1 Then
            INDsleVigence.EditValue = CType(Ilist.GetList()(0), BudgetValidityXpo).Id
        End If
    End Sub

    ''' <summary>
    ''' Limpiars the controles.
    ''' </summary>
    Private Sub LimpiarControles()
        INDsleEntity.EditValue = Nothing
        INDsleVigence.EditValue = Nothing
    End Sub

    ''' <summary>
    ''' Initializes the budget entity.
    ''' </summary>
    Public Sub InitializeBudgetEntity()
        If DesignMode = False Then
            Using modelBusq As New MBusqueda
                'Budget Institution (Entidad presupuestal)
                Me.INDsleEntity.Properties.DataSource = modelBusq.ConsultarEntidades(eDataSource.ListBudgetEntitiesXPSCS)
            End Using
            SetFirstOrDefaultBudgetInstitution()
        End If
    End Sub

    ''' <summary>
    ''' Sets the first or default budget institution.
    ''' </summary>
    Private Sub SetFirstOrDefaultBudgetInstitution()
        Dim listBudget_Entities As DevExpress.Xpo.XPServerCollectionSource = CType(Me.INDsleEntity.Properties.DataSource, DevExpress.Xpo.XPServerCollectionSource)
        Dim Ilist As IListSource = TryCast(listBudget_Entities, IListSource)
        If Ilist.GetList().Count = 1 Then
            Me.INDsleEntity.EditValue = CType(Ilist.GetList()(0), BudgetBudgetInstitutionsXpo).Id
            INDsleVigence.Enabled = True
        End If
    End Sub

    ''' <summary>
    ''' Returns the value rubro.
    ''' </summary>
    ''' <returns></returns>
    Public Function ReturnValueRubro() As String
        Return _codeRubro
    End Function

#End Region

    Public Enum EItemType As Integer
        ''' <summary>
        ''' Ingreso
        ''' </summary>
        ''' <remarks></remarks>
        Earning = 1
        ''' <summary>
        ''' Gasto
        ''' </summary>
        ''' <remarks></remarks>
        Expense = 2
    End Enum

End Class
