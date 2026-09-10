'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Cristhian Mauricio Salazar
' Created          : 02-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ContractGroupRepository
    Inherits GenericRepository(Of ContractGroup)
    Implements IContractGroupRepository

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
    ''' Obtiene un grupo de contrato especifico
    ''' </summary>
    ''' <param name="code">Codigo del grupo de contrato</param>
    ''' <returns>Grupo de contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractGroup(code As String, Optional tracking As Boolean = True) As ContractGroup Implements IContractGroupRepository.GetContractGroup
        Dim contractGroup = From e In _context.ContractGroup
                            Where e.Code = code
                            Select e
        If contractGroup.Count > 0 Then
            Dim objContractGroup = Nothing
            If tracking = False Then
                objContractGroup = (From e In _context.ContractGroup.AsNoTracking
                                    Where e.Code = code
                                    Select e).SingleOrDefault
            Else
                objContractGroup = contractGroup.SingleOrDefault()
            End If
            Return objContractGroup
        Else
            Return New ContractGroup()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los grupos de contratos
    ''' </summary>
    ''' <returns>Lista de grupos de contrato</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractGroup() As List(Of ContractGroup) Implements IContractGroupRepository.ListAllContractGroup
        Dim contractGroup = From e In _context.ContractGroup
                            Select e
        Return contractGroup.ToList()
    End Function
End Class
