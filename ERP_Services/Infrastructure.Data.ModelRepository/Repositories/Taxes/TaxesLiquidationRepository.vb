'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Infrastructure

Public Class TaxesLiquidationRepository
    Inherits GenericRepository(Of TaxesLiquidation)
    Implements ITaxesLiquidationRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

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
    Public Function SP_SaveTaxesLiquidation(Year As Integer, CadastralIdentification As String, CadastralIdentification2 As String, Address As String, Address2 As String, OwnerId As Integer, PropertyType As Integer, CodeUser As String) As SP_SaveTaxesLiquidation_Result Implements ITaxesLiquidationRepository.SP_SaveTaxesLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveTaxesLiquidation(Year, CadastralIdentification, CadastralIdentification2, Address, Address2, OwnerId, PropertyType, CodeUser).SingleOrDefault
    End Function

    ''' <summary>
    ''' Confirma la liquidación de impuestos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Ids"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConfirmTaxesLiquidation(Year As Integer, Ids As String, CodeUser As String) As SP_ConfirmTaxesLiquidation_Result Implements ITaxesLiquidationRepository.SP_ConfirmTaxesLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmTaxesLiquidation(Year, Ids, CodeUser).SingleOrDefault
    End Function

End Class
