'***********************************************************************
' Assembly         : Infrastructure.Data.PortfolioRepository
' Author           : Diego Andrés Roldán Lozano
' Created          : 30-07-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Entities
Imports Infrastructure.Data.Base

Public Class FiscalizationRepository
    Inherits GenericRepository(Of ThirdParty)
    Implements IFiscalizationRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Valida el archivo
    ''' </summary>
    ''' <param name="Xml"></param>
    ''' <param name="Year"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SP_ValidateTaxBase(Xml As String, Year As Integer) As List(Of SP_ValidateTaxBase_Result) Implements IFiscalizationRepository.SP_ValidateTaxBase
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ValidateTaxBase(Xml, Year).ToList
    End Function

End Class
