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

Public Class TaxesPropertyRepository
    Inherits GenericRepository(Of TaxesProperty)
    Implements ITaxesPropertyRepository

    'Contexto de payroll
    Private _context As IGlobalModelUnitOfWork

    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub


    Public Function ValidateLoadPlaneCollection(xml As String) As ObjectResult(Of SP_ValidateLoadPlaneCollection_Result) Implements ITaxesPropertyRepository.ValidateLoadPlaneCollection
        Return _context.SP_ValidateLoadPlaneCollection(xml)
    End Function

    Public Function SaveLoadPlaneCollection(xml As String, user As String) As ObjectResult(Of SP_SaveLoadPlaneCollection_Result) Implements ITaxesPropertyRepository.SaveLoadPlaneCollection
        Return _context.SP_SaveLoadPlaneCollection(xml, user)
    End Function

    Public Function GetAllTaxedProperties() As List(Of TaxesProperty) Implements ITaxesPropertyRepository.GetAllTaxedProperties
        Dim result = (From p As TaxesProperty In _context.TaxesProperty.AsNoTracking().Include("ThirdParty").AsNoTracking() Where p.Status = 1 And p.Taxed And p.Latitude <> 0 And p.Longitude <> 0 Select p).Take(5000).ToList()
        Return result
    End Function

    ''' <summary>
    ''' Busca un taxesProperty atraves de su code
    ''' </summary>
    ''' <param name="code">Code del taxesProperty</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetTaxesPropertyByCode(code As String, Optional tracking As Boolean = True) As TaxesProperty Implements ITaxesPropertyRepository.GetTaxesPropertyByCode
        If tracking Then
            Dim thirdParty = (From e In _context.TaxesProperty
                         Where e.Code = code
                         Select e).FirstOrDefault
            If thirdParty IsNot Nothing Then

                Return thirdParty
            Else

                Return New TaxesProperty()
            End If
        Else
            Dim thirdParty = From e In _context.TaxesProperty.AsNoTracking
                         Where e.Code = code
                         Select e
            If thirdParty.Count > 0 Then
                Return thirdParty.SingleOrDefault
            Else
                Return New TaxesProperty()
            End If
        End If
    End Function
End Class
