'***********************************************************************
' Assembly         : DistributedServices.Portfolio
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/06/2017
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Portfolio
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class PortfolioService

    Public Function GetLawyerByCode(code As String, audit As AuditMessage) As ActionResult(Of Lawyer) Implements IPortfolioLawyer.GetLawyerByCode
        Using service As ILawyerAdminService = Container.Current.Resolve(Of ILawyerAdminService)()
            Return service.GetLawyerByCode(code, audit)
        End Using
        'Return _lawyerAdminService.GetLawyerByCode(code, audit)
    End Function

    Public Function GetLawyerById(id As Integer) As ActionResult(Of Lawyer) Implements IPortfolioLawyer.GetLawyerById
        Using service As ILawyerAdminService = Container.Current.Resolve(Of ILawyerAdminService)()
            Return service.GetLawyerById(id)
        End Using
        'Return _lawyerAdminService.GetLawyerById(id)
    End Function

    Public Function SaveLawyer(Lawyer As Lawyer, idSequence As Int64, audit As AuditMessage) As ActionResult(Of Lawyer) Implements IPortfolioLawyer.SaveLawyer
        Using service As ILawyerAdminService = Container.Current.Resolve(Of ILawyerAdminService)()
            Return service.SaveLawyer(Lawyer, audit, idSequence)
        End Using
        'Return _lawyerAdminService.SaveLawyer(Lawyer, audit, idSequence)
    End Function

    Public Function DeleteLawyer(Lawyer As Lawyer, audit As AuditMessage) As ActionResult Implements IPortfolioLawyer.DeleteLawyer
        Using service As ILawyerAdminService = Container.Current.Resolve(Of ILawyerAdminService)()
            Return service.DeleteLawyer(Lawyer, audit)
        End Using
        'Return _lawyerAdminService.DeleteLawyer(Lawyer, audit)
    End Function

    Public Function ChangeStateLawyer(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Lawyer) Implements IPortfolioLawyer.ChangeStateLawyer
        Using service As ILawyerAdminService = Container.Current.Resolve(Of ILawyerAdminService)()
            Return service.ChangeStateLawyer(code, state, audit)
        End Using
        'Return _lawyerAdminService.ChangeStateLawyer(code, state, audit)
    End Function

End Class
