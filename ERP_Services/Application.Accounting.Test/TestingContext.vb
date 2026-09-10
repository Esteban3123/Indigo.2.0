'***********************************************************************
' Assembly         : Testing.Base
' Author           : Miguel A. Fonseca C.
' Created          : 2017-05-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DistributedServices.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Public Class TestingContext

#Region "Fields"
    Public _container As IUnityContainer

    Public _audit As New AuditMessage

    Public _session As New SessionValues

#End Region

#Region "Builder"
    Public Sub New()
        ServerSessionValues.Current.CurrentContainer = "VIEDev"
        ServerSessionValues.Current.CurrentHISContainer = "INDIGO001"
        ServerSessionValues.Current.CurrentInteropCostContainer = "DGEMPRES03"

        _container = Container.Current

        _audit = New AuditMessage With
        {
            .CodeUser = "999",
            .Company = "04",
            .CompanyType = 2,
            .ComputerName = "ITS29",
            .Functional = "755",
            .IdUser = 15130,
            .NameUser = "",
            .WindowsUser = "INDIGO\miguelfonseca"
        }

        _session = New SessionValues() With
        {
            .TransactionalContainer = ServerSessionValues.Current.CurrentContainer
        }
    End Sub
#End Region

End Class
