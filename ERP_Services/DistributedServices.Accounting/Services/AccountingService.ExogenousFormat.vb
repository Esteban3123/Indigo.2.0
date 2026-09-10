#Region "Imports"

Imports Application.Accounting
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity

#End Region

Partial Public Class AccountingService

#Region "Methods"

    Public Function GetExogenousFormatById(id As Integer, audit As AuditMessage) As ActionResult(Of ExogenousFormat) Implements IAccountingExogenousFormat.GetExogenousFormatById
        Using service = Container.Current.Resolve(Of IExogenousFormatAdminService)()
            Return service.GetExogenousFormatById(id, audit)
        End Using
    End Function

    Public Function GetExogenousFormatByCode(code As String, audit As AuditMessage) As ActionResult(Of ExogenousFormat) Implements IAccountingExogenousFormat.GetExogenousFormatByCode
        Using service = Container.Current.Resolve(Of IExogenousFormatAdminService)()
            Return service.GetExogenousFormatByCode(code, audit)
        End Using
    End Function

    Public Function SaveExogenousFormat(ExogenousFormat As ExogenousFormat, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of ExogenousFormat) Implements IAccountingExogenousFormat.SaveExogenousFormat
        Using service = Container.Current.Resolve(Of IExogenousFormatAdminService)()
            Return service.SaveExogenousFormat(ExogenousFormat, audit, idSequense)
        End Using
    End Function

    Public Function ChangeStateExogenousFormat(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of ExogenousFormat) Implements IAccountingExogenousFormat.ChangeStateExogenousFormat
        Using service = Container.Current.Resolve(Of IExogenousFormatAdminService)()
            Return service.ChangeStateExogenousFormat(code, state, audit)
        End Using
    End Function

#End Region

End Class
