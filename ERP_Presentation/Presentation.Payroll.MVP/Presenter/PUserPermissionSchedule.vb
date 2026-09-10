Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Domain.Payroll.Entities
Imports Presentation.Controls.MVP
Imports DevExpress.Xpo

Public Class PUserPermissionsSchedule

    Dim View As IUserPermissionSchedule

    Dim Indigo As SessionValues

    Public Sub New(ByRef iview As IUserPermissionSchedule)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
        Indigo = SessionValues.Instance
    End Sub

    Public Sub LoadForm()
        Dim modelo As New MBusqueda

        View.SLERoleDataSource = modelo.ConsultarEntidades(CType(Infrastructure.CrossCutting.Base.eDataSource.RolesCRUD, Infrastructure.CrossCutting.Base.eDataSource))
        View.SLEUserDataSource = modelo.ConsultarEntidades(CType(Infrastructure.CrossCutting.Base.eDataSource.ListUserContainer, Infrastructure.CrossCutting.Base.eDataSource))
        View.SLEFunctionalUnitDataSource = modelo.ConsultarEntidades(CType(Infrastructure.CrossCutting.Base.eDataSource.FunctionalUnit, Infrastructure.CrossCutting.Base.eDataSource))
        View.SLEPositionDataSource = modelo.ConsultarEntidades(CType(Infrastructure.CrossCutting.Base.eDataSource.Position, Infrastructure.CrossCutting.Base.eDataSource))
        View.SLEUserTypeDataSource = {"Roles", "Usuario"}

    End Sub
End Class
