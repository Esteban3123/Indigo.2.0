'***********************************************************************
' Assembly         : DistributedServices.Payments
' Author           : Pablo Alexander Salazar Sanchez
' Created          : 21/12/2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountingService
    'IMainAccountLevelsAdminService
    'IAccountingMainAccountLevels
    'MainAccountLevels
#Region "Methods"

    Public Function DeleteMainAccountLevels(MainAccountLevels As MainAccountLevels, audit As AuditMessage) As ActionResult Implements IAccountingMainAccountLevels.DeleteMainAccountLevels
        Using service As IMainAccountLevelsAdminService = Container.Current.Resolve(Of IMainAccountLevelsAdminService)()
            Return service.DeleteMainAccountLevels(MainAccountLevels, audit)
        End Using
    End Function

    Public Function GetAllMainAccountLevels(audit As AuditMessage) As List(Of MainAccountLevels) Implements IAccountingMainAccountLevels.GetAllMainAccountLevels
        Using service As IMainAccountLevelsAdminService = Container.Current.Resolve(Of IMainAccountLevelsAdminService)()
            Return service.GetAllMainAccountLevels(audit)
        End Using
    End Function

    Public Function GetMainAccountLevelsByCode(code As String, audit As AuditMessage) As ActionResult(Of MainAccountLevels) Implements IAccountingMainAccountLevels.GetMainAccountLevelsByCode
        Using service As IMainAccountLevelsAdminService = Container.Current.Resolve(Of IMainAccountLevelsAdminService)()
            Return service.GetMainAccountLevelsByCode(code, audit)
        End Using
    End Function

    Public Function GetMainAccountLevelsById(id As Integer, audit As AuditMessage) As ActionResult(Of MainAccountLevels) Implements IAccountingMainAccountLevels.GetMainAccountLevelsById
        Using service As IMainAccountLevelsAdminService = Container.Current.Resolve(Of IMainAccountLevelsAdminService)()
            Return service.GetMainAccountLevelsById(id, audit)
        End Using
    End Function

    Public Function SaveMainAccountLevels(MainAccountLevels As MainAccountLevels, idSequense As Int64, audit As AuditMessage) As ActionResult(Of MainAccountLevels) Implements IAccountingMainAccountLevels.SaveMainAccountLevels
        Using service As IMainAccountLevelsAdminService = Container.Current.Resolve(Of IMainAccountLevelsAdminService)()
            Return service.SaveMainAccountLevels(MainAccountLevels, audit, idSequense)
        End Using
    End Function

#End Region

End Class
