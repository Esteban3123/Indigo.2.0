'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Carlos Ernesto Cordoba
' Created          : 01-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base
Imports Domain.Entities
#End Region

Public Interface ISettingPortfolioRepository
    Inherits IRepository(Of SettingPortfolio)

    Function GetSettingPortfolioByIdOperatingUnit(idOperatingUnit As Integer) As SettingPortfolio

End Interface
