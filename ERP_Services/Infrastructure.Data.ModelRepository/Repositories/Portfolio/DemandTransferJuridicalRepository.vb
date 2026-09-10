'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepositiry
' Author           : Hector Rodriguez
' Created          : 20-08-2019
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.Data.Base
#End Region


Public Class DemandTransferJuridicalRepository
    Inherits GenericRepository(Of DemandTransferJuridical)
    Implements IDemandTransferJuridicalRepository

        'contexto de cartera
    Private _context As IGlobalModelUnitOfWork

    #Region "Builder"
        Public Sub New(ByVal contex As IGlobalModelUnitOfWork)
            MyBase.New(contex)
            _context = contex
        End Sub
    #End Region
End Class
