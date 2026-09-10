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

Public Class LowTaxLiquidationRepository
    Inherits GenericRepository(Of LowTaxLiquidation)
    Implements ILowTaxesLiquidationRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetLowTaxLiquidationByConsecutive(_consecutive As String) As LowTaxLiquidation Implements ILowTaxesLiquidationRepository.GetLowTaxLiquidationByConsecutive
        Dim res = (From e In _context.LowTaxLiquidation.Include("ThirdParty").Include("LowTaxLiquidationDetail") Where e.Consecutive = _consecutive Select e).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From e In _context.LowTaxLiquidation Where e.Consecutive = _consecutive Select e).FirstOrDefault()
            Return res
        End If
        Return New LowTaxLiquidation
    End Function


    ''' <summary>
    ''' Confirma la liquidación de impuestos menores
    ''' </summary>
    ''' <param name="idLowtaxesLiquidation"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ConfirmLowTaxesLiquidation(idLowtaxesLiquidation As Integer, ByVal codeUser As String) As SP_ConfirmLowTaxesLiquidation_Result Implements ILowTaxesLiquidationRepository.SP_ConfirmLowTaxesLiquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmLowTaxesLiquidation(idLowtaxesLiquidation, codeUser).SingleOrDefault
    End Function
End Class
