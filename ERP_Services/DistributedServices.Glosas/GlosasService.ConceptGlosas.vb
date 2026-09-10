'***********************************************************************
' Assembly         : Application.Glosas
' Author           : Diego A. Roldán
' Created          : 2022-04-04
'
' Last Modified By : 
' Last Modified On : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Application.Glosas
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.CrossCutting.IOC

Partial Public Class GlosasService
    Implements IGlosasConceptGlosas

    Public Function GetConceptGlosasById(id As Integer, session As SessionValues) As ConceptGlosas Implements IGlosasConceptGlosas.GetConceptGlosasById
        Using service = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosaAdminService)()
            Return service.GetConceptGlosasById(id)
        End Using
    End Function

    Public Function GetConceptGlosasByCode(code As String, session As SessionValues) As ConceptGlosas Implements IGlosasConceptGlosas.GetConceptGlosasByCode
        Using service = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosaAdminService)()
            Return service.GetConceptGlosasByCode(code)
        End Using
    End Function

    Public Function SaveConceptGlosas(conceptGlosa As ConceptGlosas, session As SessionValues, Optional idSequense As Long = 0) As ActionResult(Of ConceptGlosas) Implements IGlosasConceptGlosas.SaveConceptGlosas
        Using service = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosaAdminService)()
            Return service.SaveConceptGlosas(conceptGlosa, session.AuditMessageWcf, idSequense)
        End Using
    End Function

    Public Function DeleteConceptGlosas(conceptGlosa As ConceptGlosas, session As SessionValues) As ActionResult Implements IGlosasConceptGlosas.DeleteConceptGlosas
        Using service = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosaAdminService)()
            Return service.DeleteConceptGlosas(conceptGlosa, session.AuditMessageWcf)
        End Using
    End Function

    Public Function ChangeConceptGlosas(code As String, state As Boolean, session As SessionValues) As ActionResult(Of ConceptGlosas) Implements IGlosasConceptGlosas.ChangeConceptGlosas
        Using service = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of IConceptGlosaAdminService)()
            Return service.ChangeConceptGlosas(code, state, session.AuditMessageWcf)
        End Using
    End Function
End Class
