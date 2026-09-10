'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Steven Rojas Rodriguez
' Created          : 26-10-2022
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Infrastructure.Data.Base
Imports Infrastructure.Data.ModelRepository
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class LicensingConceptsRepository
    Inherits GenericRepository(Of LicensingConcepts)
    Implements ILicensingConceptsRepository

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

    Public Function ListAllLicensingConcepts() As List(Of LicensingConcepts) Implements ILicensingConceptsRepository.ListAllLicensingConcepts
        Dim LicensingConcepts = From e In _context.LicensingConcepts
                                Select e
        Return LicensingConcepts.ToList
    End Function

    Public Function GetLicensingConcepts(code As String, Optional tracking As Boolean = True) As LicensingConcepts Implements ILicensingConceptsRepository.GetLicensingConcepts
        Dim licensingConcepts = From e In _context.LicensingConcepts
                                Where e.Code = code
                                Select e
        If licensingConcepts?.Any() Then
            Dim ObjLicensingConcepts = Nothing
            If tracking = False Then
                ObjLicensingConcepts = (From e In _context.LicensingConcepts.AsNoTracking
                                        Where e.Code = code
                                        Select e).SingleOrDefault
            Else
                ObjLicensingConcepts = licensingConcepts.SingleOrDefault
            End If
            Return ObjLicensingConcepts
        Else
            Return New LicensingConcepts()
        End If
    End Function

    Public Function GetLicensingConceptsById(Id As Integer, Optional tracking As Boolean = True) As LicensingConcepts Implements ILicensingConceptsRepository.GetLicensingConceptsById
        Dim licensingConcepts = From e In _context.LicensingConcepts
                                Where e.Id = Id
                                Select e
        If licensingConcepts?.Any() Then
            Dim ObjLicensingConcepts = Nothing
            If tracking = False Then
                ObjLicensingConcepts = (From e In _context.LicensingConcepts.AsNoTracking
                                        Where e.Id = Id
                                        Select e).SingleOrDefault
            Else
                ObjLicensingConcepts = licensingConcepts.SingleOrDefault
            End If
            Return ObjLicensingConcepts
        Else
            Return New LicensingConcepts()
        End If
    End Function
End Class
