'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 06-07-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Public Class ContractTemplateRepository

    Inherits GenericRepository(Of ContractTemplate)

    Implements IContractTemplateRepository

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
    ''' Obtiene una Plantilla de Contrato
    ''' </summary>
    ''' <param name="code">Código de la Plantilla de Contrato</param>
    ''' <returns>Plantilla de Contrato</returns>
    ''' <remarks></remarks>
    Public Function GetContractTemplate(code As String, Optional tracking As Boolean = True) As ContractTemplate Implements IContractTemplateRepository.GetContractTemplate
        Dim contractTemplate = From e In _context.ContractTemplate
                         Where e.Code = code
                         Select e
        If contractTemplate.Count > 0 Then

            Dim objContractTemplate = Nothing
            If tracking = False Then
                objContractTemplate = (From e In _context.ContractTemplate.AsNoTracking
                                      Where e.Code = code
                                      Select e).SingleOrDefault
            Else
                objContractTemplate = contractTemplate.SingleOrDefault()
            End If
            Return objContractTemplate

        Else
            Return New ContractTemplate()
        End If
    End Function

    ''' <summary>
    ''' Lista Todas las Plantillas de Contrato
    ''' </summary>
    ''' <returns>Plantillas de Contrato</returns>
    ''' <remarks></remarks>
    Public Function ListAllContractTemplate() As List(Of ContractTemplate) Implements IContractTemplateRepository.ListAllContractTemplate
        Dim contractTemplate = From e In _context.ContractTemplate
                          Select e
        Return contractTemplate.ToList()
    End Function
End Class
