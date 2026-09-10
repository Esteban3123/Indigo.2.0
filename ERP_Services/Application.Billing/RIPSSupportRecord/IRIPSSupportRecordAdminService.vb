Imports System.Threading.Tasks
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Public Interface IRIPSSupportRecordAdminService
    Inherits IDisposable

    Function GetRIPSSupportRecordByIdAsync(ByVal id As Integer) As Task(Of RIPSSupportRecord)
    Function GetRIPSSupportRecordByCodeAsync(ByVal code As String) As Task(Of RIPSSupportRecord)
    Function NewRIPSSupportRecordAsync(ByVal supportRecord As RIPSSupportRecord, ByVal operatingUnitId As Integer, ByVal audit As AuditMessage, Optional ByVal idSequence As Long = 0) As Task(Of ActionResult(Of RIPSSupportRecord))
    Function ConfirmRIPSSupportRecordAsync(ByVal id As Integer, ByVal audit As AuditMessage) As Task(Of ActionResult(Of RIPSSupportRecord))
    Function ListAllRIPSSupportRecordsAsync() As Task(Of List(Of RIPSSupportRecord))

End Interface
