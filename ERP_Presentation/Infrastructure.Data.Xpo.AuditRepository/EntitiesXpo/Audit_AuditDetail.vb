Imports System
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

Partial Public Class Audit_AuditDetail
    Public Sub New(ByVal session As Session)
        MyBase.New(session)
    End Sub
    Public Overrides Sub AfterConstruction()
        MyBase.AfterConstruction()
    End Sub
End Class
