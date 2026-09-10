'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 05-02-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.Data.ModelRepository
Imports Domain.Payroll
Imports Domain.Payroll.Entities

Public Class ContributorSubtypeRepository
    Inherits GenericRepository(Of ContributorSubtype)
    Implements IContributorSubtypeRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork)
        MyBase.New(context)
        _context = context
    End Sub

    ''' <summary>
    ''' Lista todos los subtipis de cotizantes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllContributorSubtype() As List(Of ContributorSubtype) Implements IContributorSubtypeRepository.ListAllContributorSubtype
        Dim contributorSubtype = From e In _context.ContributorSubtype
                                 Select e
        Return contributorSubtype.ToList
    End Function

    ''' <summary>
    ''' Obtiene por codigo el subtipo de cotizante
    ''' </summary>
    ''' <param name="Code">codigo </param>
    ''' <returns>Year</returns>
    ''' <remarks></remarks>
    Public Function GetContributorSubtypeByCode(ByVal Code As String, Optional tracking As Boolean = True) As ContributorSubtype Implements IContributorSubtypeRepository.GetContributorSubtypeByCode
        Dim contributorSubtype = (From e In _context.ContributorSubtype
                                  Where e.Code = Code
                                  Select e).FirstOrDefault()

        If contributorSubtype IsNot Nothing Then
            contributorSubtype.OriginalValue = (From e In _context.ContributorSubtype.AsNoTracking()
                                                Where e.Code = Code
                                                Select e).FirstOrDefault()
            Return contributorSubtype
        Else
            Return New ContributorSubtype()
        End If
    End Function

    ''' <summary>
    ''' Obtiene por id el subtipo de cotizante
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Public Function GetContributorSubtypeById(ByVal Id As Integer, Optional tracking As Boolean = True) As ContributorSubtype Implements IContributorSubtypeRepository.GetContributorSubtypeById
        If tracking Then
            Dim contributorSubtype = (From e In _context.ContributorSubtype
                                      Where e.Id = Id
                                      Select e).FirstOrDefault()

            If contributorSubtype IsNot Nothing Then
                Return contributorSubtype
            Else
                Return New ContributorSubtype()
            End If
        Else
            Dim contributorSubtype = (From e In _context.ContributorSubtype
                                      Where e.Id = Id
                                      Select e).FirstOrDefault()

            If contributorSubtype IsNot Nothing Then
                Return contributorSubtype
            Else
                Return New ContributorSubtype()
            End If
        End If
    End Function
End Class
