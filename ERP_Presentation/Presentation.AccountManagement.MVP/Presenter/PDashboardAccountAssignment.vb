Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.CrystalRepository

Public Class PDashboardAccountAssignment

#Region "Fields"
    ''' <summary>
    ''' Interfaz del formulario
    ''' </summary>
    Public View As IDashboardAccountAssignment

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Public Indigo As SessionValues
#End Region

#Region "Builder"
    Public Sub New(ByRef iView As IDashboardAccountAssignment)
        View = iView
        Indigo = SessionValues.Instance
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene la lista de todos los grupos de atención
    ''' </summary>
    Public Sub InitializeCareCenterXpo()
        View.CareCenterXpo = XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListCenters()
    End Sub

    ''' <summary>
    ''' Obtiene el datasource de los ingresos pendientes de asignación automática
    ''' </summary>
    ''' <param name="attentionCenterCode">Código del centro de atención</param>
    ''' <param name="typeIncome">Tipo de ingreso</param>
    Public Sub GetPendingAssignmentDatasource(attentionCenterCode As String, typeIncome As String)
        View.PendingAssignmentDatasource = XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountManagementService.ListAdmissionsPending(attentionCenterCode, typeIncome)
    End Sub

#End Region

End Class
