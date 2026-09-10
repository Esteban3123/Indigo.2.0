Imports Infrastructure.CrossCutting.Base
Imports Domain.Payroll.Entities
Imports Presentation.CloudAgent
Imports Presentation.Base
Imports Domain.Base.Entities

Public Class MReligiousBeliefs
    Implements IDisposable

    Dim Indigo As SessionValues

    Private _tagForm As String

    Public Sub New(Tag As String)
        Me._tagForm = Tag
        Indigo = SessionValues.Instance
        Me.Indigo.AuditMessageWcf.Functional = Me._tagForm
    End Sub

    Public Async Function GetReligiousBeliefs(ByVal pCode As String, ByVal pTracking As Boolean) As Task(Of ActionResult(Of ReligiousBeliefs))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.GetReligiousBeliefsAsync(pCode, pTracking, Indigo)
    End Function

    Public Async Function SaveReligiousBeliefs(ByVal pReligiousBeliefs As ReligiousBeliefs, idSequense As Integer) As Task(Of ActionResult(Of ReligiousBeliefs))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.SaveReligiousBeliefsAsync(pReligiousBeliefs, Indigo, idSequense)
    End Function

    Public Async Function DeleteReligiousBeliefs(ByVal pReligiousBeliefs As ReligiousBeliefs) As Task(Of ActionResult)
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.DeleteReligiousBeliefsAsync(pReligiousBeliefs, Indigo)
    End Function

    Public Async Function ChangeStateReligiousBeliefs(ByVal pCode As String, ByVal pStatus As Boolean) As Task(Of ActionResult(Of ReligiousBeliefs))
        Return Await IndigoConecta.Instancia.CurrentCloud.IndigoPayroll.ChangeStateReligiousBeliefsAsync(pCode, pStatus, Indigo)
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
