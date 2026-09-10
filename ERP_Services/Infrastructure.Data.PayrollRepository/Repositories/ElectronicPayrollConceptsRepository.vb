'***********************************************************************
' Assembly         : DistributedServices.Common
' Author           : Andres Felipe Quintero Garcia
' Created          : 10-02-2025
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports Infrastructure.Data.ModelRepository
Imports System.Diagnostics
#End Region

Public Class ElectronicPayrollConceptsRepository
    Inherits GenericRepository(Of ElectronicPayrollConcepts)
    Implements IElectronicPayrollConceptsRepository, Inject

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
    ''' obtiene un Concepto de nómina electrónica por codigo
    ''' </summary>
    ''' <param name="code">codigo de la Enfermedad Diagnosticada</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetElectronicPayrollConcepts(code As String, Optional tracking As Boolean = True) As ElectronicPayrollConcepts Implements IElectronicPayrollConceptsRepository.GetElectronicPayrollConcepts
        Dim ElectronicPayrollConcepts = From e In _context.ElectronicPayrollConcepts
                                        Where e.Code = code
                                        Select e
        If ElectronicPayrollConcepts.Count > 0 Then
            Dim objElectronicPayrollConcepts = Nothing
            If tracking = False Then
                objElectronicPayrollConcepts = (From e In _context.ElectronicPayrollConcepts.AsNoTracking
                                                Where e.Code = code
                                                Select e).SingleOrDefault
            Else
                objElectronicPayrollConcepts = ElectronicPayrollConcepts.SingleOrDefault
            End If
            Return objElectronicPayrollConcepts
        Else
            Return New ElectronicPayrollConcepts
        End If
    End Function

    ''' <summary>
    ''' Lista todos los Concepto de nómina electrónica
    ''' </summary>
    ''' <returns>Lista de niveles de estudio</returns>
    ''' <remarks></remarks>
    Public Function ListAllElectronicPayrollConcepts() As List(Of Entities.ElectronicPayrollConcepts) Implements IElectronicPayrollConceptsRepository.ListAllElectronicPayrollConcepts
        Dim ElectronicPayrollConcepts = From e In _context.ElectronicPayrollConcepts
                                        Select e
        Return ElectronicPayrollConcepts.ToList()
    End Function
End Class
