'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Antony F. Córdoba P.
' Created          : 20-12-2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class MaritalStatusRepository
    Inherits GenericRepository(Of MaritalStatus)
    Implements IMaritalStatusRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    Public Sub New(context As IPayrollUnitOfWork)
        MyBase.New(context)
    End Sub
    ''' <summary>
    ''' Lista Todos los Estados Civiles
    ''' </summary>
    ''' <returns>Lista de los Estados Civiles</returns>
    ''' <remarks></remarks>
    Public Function ListAllMaritalStatus() As List(Of MaritalStatus) Implements IMaritalStatusRepository.ListAllMaritalStatus
        Dim maritalStatus = From e In _context.MaritalStatus
                            Select e
        Return maritalStatus.ToList
    End Function
    ''' <summary>
    ''' Obtiene el estado civil por código
    ''' </summary>
    ''' <param name="code">Codigo del estado civil</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    Public Function GetMaritalStatusByCode(Code As String, Optional tracking As Boolean = True) As MaritalStatus Implements IMaritalStatusRepository.GetMaritalStatusByCode
        Dim maritalStatus = (From e In _context.MaritalStatus
                             Where e.Code = Code
                             Select e).FirstOrDefault()

        If maritalStatus IsNot Nothing Then
            maritalStatus.OriginalValue = (From e In _context.MaritalStatus.AsNoTracking()
                                           Where e.Code = Code
                                           Select e).FirstOrDefault()
            Return maritalStatus
        Else
            Return New MaritalStatus()
        End If
    End Function
    ''' <summary>
    ''' Obtiene el estado civil por ID
    ''' </summary>
    ''' <param name="Id">Id del Estado civil</param>
    ''' <returns>Grupo</returns>
    ''' <remarks></remarks>
    Public Function GetMaritalStatusById(Id As Integer, Optional tracking As Boolean = True) As MaritalStatus Implements IMaritalStatusRepository.GetMaritalStatusById
        If tracking Then
            Dim maritalStatus = (From e In _context.MaritalStatus
                                 Where e.Id = Id
                                 Select e).FirstOrDefault()

            If maritalStatus IsNot Nothing Then
                Return maritalStatus
            Else
                Return New MaritalStatus()
            End If
        Else
            Dim maritalStatus = (From e In _context.MaritalStatus
                                 Where e.Id = Id
                                 Select e).FirstOrDefault()

            If maritalStatus IsNot Nothing Then
                Return maritalStatus
            Else
                Return New MaritalStatus()
            End If
        End If
    End Function
End Class
