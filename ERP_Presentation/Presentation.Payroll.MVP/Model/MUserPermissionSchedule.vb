Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

Public Class MUserPermissionSchedule
    Implements IDisposable

    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Dim Indigo As SessionValues

    Private _tagForm As String

    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

    Public Function SavePermissionUser(ListFunctionalUnitResponsible As List(Of FunctionalUnitResponsible), ListDeleteFunctionalUnitResponsible As List(Of FunctionalUnitResponsible), ListPositionUser As List(Of PositionUser), ListDeletePositionUser As List(Of PositionUser)) As Task(Of ActionResult(Of String))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SavePermissionUserAsync(ListPositionUser, ListDeletePositionUser, ListFunctionalUnitResponsible, ListDeleteFunctionalUnitResponsible, Indigo)
    End Function

    Public Function SavePermissionRoll(pListPR As List(Of PositionRoll), ListDeletePositionRol As List(Of PositionRoll)) As Task(Of ActionResult(Of List(Of PositionRoll)))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SavePermissionRolAsync(pListPR, ListDeletePositionRol, Indigo)
    End Function

    Public Function GetListPositionByRoleID(pRoleID As Integer, RoleCode As String) As Task(Of List(Of PositionRoll))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPermissionRollAsync(RoleCode, pRoleID, Indigo)
    End Function

    Public Function GetListPositionUser(UserId As Integer) As Task(Of List(Of PositionUser))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetPositionUserAsync(UserId, Indigo)
    End Function

    Public Function GetListFunctionalUnitResponsible(UserId As Integer) As Task(Of List(Of FunctionalUnitResponsible))
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFunctionalUnitResponsibleAsync(UserId, Indigo)
    End Function
End Class
