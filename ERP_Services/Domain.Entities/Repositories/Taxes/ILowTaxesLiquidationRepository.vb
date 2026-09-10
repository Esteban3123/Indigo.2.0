'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities


Public Interface ILowTaxesLiquidationRepository
    Inherits IRepository(Of LowTaxLiquidation)

    Function GetLowTaxLiquidationByConsecutive(_consecutive As String) As LowTaxLiquidation

    ''' <summary>
    ''' Confirma la liquidación de impuestos menores
    ''' </summary>
    ''' <param name="idLowtaxesLiquidation"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmLowTaxesLiquidation(idLowtaxesLiquidation As Integer, ByVal codeUser As String) As SP_ConfirmLowTaxesLiquidation_Result

End Interface
