'***********************************************************************
' Assembly         : Infrastructure.Data.InventoryRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 02-04-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities

Public Class CompanyTypeRepository
    Inherits GenericRepository(Of CompanyType)
    Implements ICompanyTypeRepository

    ''' <summary>
    ''' Contexto de payments
    ''' </summary>
    ''' <remarks></remarks>
    Private _context As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payments
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IGlobalModelUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    Public Function GetAllCompanyType() As List(Of CompanyType) Implements ICompanyTypeRepository.GetAllCompanyType
        Dim CompanyType = From e In _context.CompanyType
                          Select e
        Return CompanyType.ToList()
    End Function

    Public Function GetCompanyTypeByCode(code As String, Optional tracking As Boolean = True) As CompanyType Implements ICompanyTypeRepository.GetCompanyTypeByCode
        Dim res = (From bg In _context.CompanyType Where bg.Code = code Select bg).FirstOrDefault()
        If res IsNot Nothing Then
            res.OriginalValue = (From bg In _context.CompanyType.AsNoTracking() Where bg.Code = code Select bg).FirstOrDefault()
            Return res
        Else
            Return New CompanyType()
        End If
    End Function

    Public Function GetCompanyTypeById(id As Integer, Optional tracking As Boolean = True) As CompanyType Implements ICompanyTypeRepository.GetCompanyTypeById
        Dim CompanyType = From e In _context.CompanyType
                          Where e.Id = id
                          Select e
        If CompanyType.Count > 0 Then
            Dim Objdefects = Nothing
            If tracking = False Then
                Objdefects = (From e In _context.CompanyType.AsNoTracking
                              Where e.Id = id
                              Select e).SingleOrDefault
            Else
                Objdefects = CompanyType.SingleOrDefault
            End If
            Return Objdefects
        Else
            Return New CompanyType()
        End If
    End Function
End Class
