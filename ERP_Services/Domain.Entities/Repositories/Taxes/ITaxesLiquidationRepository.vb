'***********************************************************************
' Assembly         : Domain.Portfolio
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Base
Imports Domain.Entities


Public Interface ITaxesLiquidationRepository
    Inherits IRepository(Of TaxesLiquidation)

    ''' <summary>
    ''' Guarda la liquidación de impuestos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="CadastralIdentification"></param>
    ''' <param name="Address"></param>
    ''' <param name="OwnerId"></param>
    ''' <param name="PropertyType"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer, CodeUser As String) As SP_SaveTaxesLiquidation_Result

    ''' <summary>
    ''' Confirma la liquidación de impuestos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Ids"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SP_ConfirmTaxesLiquidation(Year As Integer, Ids As String, CodeUser As String) As SP_ConfirmTaxesLiquidation_Result

End Interface
