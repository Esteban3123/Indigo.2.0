Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

Public Class MEthnicGroups
    Implements IDisposable

    Dim Indigo As SessionValues

    Private _tagForm As String

    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

    Public Async Function GetEthnicGroups(ByVal pCode As String, ByVal pTracking As Boolean) As Task(Of ActionResult(Of EthnicGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetEthnicGroupsAsync(pCode, pTracking, Indigo)
    End Function

    Public Async Function SaveEthnicGroups(ByVal pEthnicGroups As EthnicGroups, pIDSequence As Integer) As Task(Of ActionResult(Of EthnicGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveEthnicGroupsAsync(pEthnicGroups, Indigo, pIDSequence)
    End Function

    Public Async Function DeleteEthnicGroups(ByVal pEthnicGroups As EthnicGroups) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteEthnicGroupsAsync(pEthnicGroups, Indigo)
    End Function

    Public Async Function ChangeStateEthnicGroups(ByVal pCode As String, ByVal state As Boolean) As Task(Of ActionResult(Of EthnicGroups))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateEthnicGroupsAsync(pCode, state, Indigo)
    End Function

    Public Function GetFieldsNULL() As DataSet
        Return IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetFieldsNULL("SportPractice", Indigo)
    End Function

    Private disposedValue As Boolean ' To detect redundant calls

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

            End If

        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose

        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class
