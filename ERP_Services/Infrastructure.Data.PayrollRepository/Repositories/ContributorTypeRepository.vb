'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ContributorTypeRepository
    Inherits GenericRepository(Of ContributorType)
    Implements IContributorTypeRepository

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
    ''' Obtiene un tipo contribuyente
    ''' </summary>
    ''' <param name="code">Codigo del tipo contribuyente</param>
    ''' <returns>Tipo Contribuyente</returns>
    ''' <remarks></remarks>
    Public Function GetContributorType(code As String, Optional tracking As Boolean = True) As ContributorType Implements IContributorTypeRepository.GetContributorType
        Dim contributorType = From e In _context.ContributorType
                              Where e.Code = code
                              Select e
        If contributorType.Count > 0 Then
            Dim objContributorType = Nothing
            If tracking = False Then
                objContributorType = (From e In _context.ContributorType.AsNoTracking
                                     Where e.Code = code
                                     Select e).SingleOrDefault
            Else
                objContributorType = contributorType.SingleOrDefault()
            End If
            Return objContributorType

        Else
            Return New ContributorType()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los tipos contribuyentes
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAllContributorType() As List(Of ContributorType) Implements IContributorTypeRepository.ListAllContributorType
        Dim contributorType = From e In _context.ContributorType
                              Select e
        Return contributorType.ToList()
    End Function
End Class
