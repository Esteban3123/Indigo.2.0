'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ContractTypeRepository
    Inherits GenericRepository(Of ContractType)

    Implements IContractTypeRepository

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
    ''' Obtiene un Tipo de Contrato
    ''' </summary>
    ''' <param name="code">Código del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractType(code As String, Optional tracking As Boolean = True) As ContractType Implements IContractTypeRepository.GetContractType
        Dim contractType = From e In _context.ContractType
                           Where e.Code = code
                           Select e
        If contractType.Count > 0 Then

            Dim objContractType = Nothing
            If tracking = False Then
                objContractType = (From e In _context.ContractType.AsNoTracking
                                   Where e.Code = code
                                   Select e).SingleOrDefault
            Else
                objContractType = contractType.SingleOrDefault()
            End If
            Return objContractType
        Else
            Return New ContractType()
        End If
    End Function

    ''' <summary>
    ''' Lista todos los Tipos de Contratos
    ''' </summary>
    ''' <returns>Tipos de Contratos</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractType() As List(Of ContractType) Implements IContractTypeRepository.ListAllContractType
        Dim contractType = From e In _context.ContractType
                           Select e
        Return contractType.ToList()
    End Function

    ''' <summary>
    ''' Obtiene un Tipo de Contrato por ID
    ''' </summary>
    ''' <param name="code">ID del Tipo de Contrato</param>
    ''' <returns>Tipo de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractTypeById(ID As String) As ContractType Implements IContractTypeRepository.GetContractTypeById
        Dim contractType = From e In _context.ContractType
                           Where e.Id = ID
                           Select e
        If contractType.Count > 0 Then
            Return contractType.SingleOrDefault()
        Else
            Return New ContractType()
        End If
    End Function
End Class
