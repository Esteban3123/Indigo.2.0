'***********************************************************************
' Assembly         : Testing.Base
' Author           : Miguel A. Fonseca C.
' Created          : 2017-05-19
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DistribuitedServices.Billing
Imports DistributedServices.Accounting
Imports DistributedServices.Inventory.Unity
Imports Infrastructure.CrossCutting.Base
Imports Microsoft.Practices.Unity
#End Region

Public Class TestingContext

#Region "Fields"
    Public _container As IUnityContainer

    Public _audit As New AuditMessage
#End Region

#Region "Builder"
    Public Sub New()
        ServerSessionValues.Current.CurrentContainer = "VIEDev"
        ServerSessionValues.Current.CurrentHISContainer = "INDIGO001"
        ServerSessionValues.Current.CurrentInteropCostContainer = "DGEMPRES03"

        _container = Container.Current

        _audit = New AuditMessage With {
            .CodeUser = "999",
            .Company = "04",
            .CompanyType = 2,
            .ComputerName = "ITS29",
            .Functional = "755",
            .IdUser = 15130,
            .NameUser = "",
            .WindowsUser = "INDIGO\miguelfonseca"
        }
    End Sub
#End Region

End Class
