'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 08-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll

Public Class JobBondingTypeRepository

    Inherits GenericRepository(Of JobBondingType)

    Implements IJobBondingTypeRepository

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
    ''' Obtiene un Tipo de Vinculación Laboral
    ''' </summary>
    ''' <param name="code">Código del Tipo de Vinculación Laboral</param>
    ''' <returns>Tipo de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Public Function GetJobBondingType(code As String, Optional tracking As Boolean = True) As JobBondingType Implements IJobBondingTypeRepository.GetJobBondingType
        Dim jobBondingType = From e In _context.JobBondingType
                          Where e.Code = code
                          Select e
        If jobBondingType.Count > 0 Then
            Dim objJobBondingType = Nothing
            If tracking = False Then
                objJobBondingType = (From e In _context.JobBondingType.AsNoTracking
                                    Where e.Code = code
                                    Select e).SingleOrDefault
            Else
                objJobBondingType = jobBondingType.SingleOrDefault()
            End If
            Return objJobBondingType
        Else
            Return New JobBondingType()
        End If
    End Function

    ''' <summary>
    ''' Lista Todos los Tipos de Vinculación Laboral
    ''' </summary>
    ''' <returns>Tipos de Vinculación Laboral</returns>
    ''' <remarks></remarks>
    Public Function ListAllJobBondingType() As List(Of JobBondingType) Implements IJobBondingTypeRepository.ListAllJobBondingType
        Dim jobBondingType = From e In _context.JobBondingType
                          Select e
        Return jobBondingType.ToList()
    End Function
End Class
