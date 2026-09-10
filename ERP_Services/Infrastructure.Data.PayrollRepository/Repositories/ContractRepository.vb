'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 18-02-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Domain.Base
Imports System.Linq.Expressions

Public Class ContractRepository
    Inherits GenericRepository(Of Contract)
    Implements IContractRepository


    'Devuelve el contexto en este repositorio 
    Private _context As IPayrollUnitOfWork

    ''' <summary>
    '''inicializa la neva instancia d clase.
    ''' </summary>
    ''' <param name="contex">el contexto.</param>
    Public Sub New(ByVal contex As IPayrollUnitOfWork)
        MyBase.New(contex)
        _context = contex
    End Sub

    Public Function SaveMassiveContract(pXMLObj As String, pCodeUser As String, pIdUser As Integer) As SP_SaveMassiveContract_DTO Implements IContractRepository.SaveMassiveContract
        Return ExecuteStoredProcedure(Of SP_SaveMassiveContract_DTO)("[Payroll].[SP_SaveMassiveContract]", {
            ("@pXMLObj", pXMLObj),
            ("@pCodeUser", pCodeUser),
            ("@pIdUser", pIdUser)
        }).FirstOrDefault()
    End Function

    Public Sub SaveMassiveContractExtension(pXMLObj As String) Implements IContractRepository.SaveMassiveContractExtension
        _context.SP_SaveMassiveContractExtension(pXMLObj)
    End Sub


    ''' <summary>
    ''' Obtiene un contrato por ID
    ''' </summary>
    ''' <param name="idContract"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractById(idContract As Integer, Optional tracking As Boolean = False) As Contract Implements IContractRepository.GetContractById
        Dim query As IQueryable(Of Contract)

        If tracking = True Then
            query = From e In _context.Contract.Include("FundContract").Include("Group").Include("ContractType")
                    Where e.Id = idContract
                    Select e
        Else
            query = From e In _context.Contract
                    Where e.Id = idContract
                    Select e
        End If
        If query.Count > 0 Then
            Return query.SingleOrDefault()
        Else
            Return New Contract()
        End If
    End Function

    ''' <summary>
    ''' Obtiene una lista de Contratos por Número Inicial 
    ''' </summary>
    ''' <param name="InitialNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetContractByInitialNumber(ContractId As Integer, EmployeeId As Integer, Optional tracking As Boolean = False) As List(Of Contract) Implements IContractRepository.GetContractByInitialNumber
        Dim query As IQueryable(Of Contract)
        Dim ObjContract As IQueryable(Of Contract)

        If ContractId > 0 Then
            ObjContract = From e In _context.Contract
                          Where e.InitialContractNumber = ContractId
                          Select e
        Else

            ObjContract = From e In _context.Contract
                          Where e.EmployeeId = EmployeeId
                          Select e
        End If


        Dim Contract = ObjContract.FirstOrDefault()

        If Contract IsNot Nothing Then
            If tracking = True Then

                If Contract.InitialContractNumber > 0 Then

                    query = From e In _context.Contract.Include("FundContract")
                            Where e.InitialContractNumber = Contract.InitialContractNumber
                            Select e
                Else
                    query = From e In _context.Contract.Include("FundContract")
                            Where e.Id = Contract.Id
                            Select e
                End If
            Else
                If Contract.InitialContractNumber > 0 Then
                    query = From e In _context.Contract
                            Where e.InitialContractNumber = Contract.InitialContractNumber
                            Select e
                Else
                    query = From e In _context.Contract
                            Where e.Id = Contract.Id
                            Select e
                End If
            End If
        End If
        If query.Count > 0 Then
            Return query.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function ValidateMassiveContract(pXMLObj As String) As List(Of SP_ValidateMassiveContract_Result) Implements IContractRepository.ValidateMassiveContract
        Return _context.SP_ValidateMassiveContract(pXMLObj).ToList()
    End Function

    Public Function GetMassiveContract(pXMLObj As String) As List(Of SP_GetMassiveContract_Result) Implements IContractRepository.GetMassiveContract
        Return _context.SP_GetMassiveContract(pXMLObj).ToList()
    End Function

    Public Function GetMassiveContractExtension(pXMLObj As String) As List(Of SP_GetMassiveContractExtension_Result) Implements IContractRepository.GetMassiveContractExtension
        Return _context.SP_GetMassiveContractExtension(pXMLObj).ToList()
    End Function

    Public Function ValidateMassiveContractExtension(pXMLObj As String) As List(Of SP_ValidateMassiveContractExtension_Result) Implements IContractRepository.ValidateMassiveContractExtension
        Return _context.SP_ValidateMassiveContractExtension(pXMLObj).ToList()
    End Function

    Public Function ValidateMassiveDependentRelatives(pXMLObj As String) As List(Of SP_ValidateMassiveDependentRelatives_Result) Implements IContractRepository.ValidateMassiveDependentRelatives
        Return _context.SP_ValidateMassiveDependentRelatives(pXMLObj).ToList()
    End Function

    Public Function ValidateMassiveExternalEntities(pXMLObj As String) As List(Of SP_ValidateMassiveExternalEntities_Result) Implements IContractRepository.ValidateMassiveExternalEntities
        Return _context.SP_ValidateMassiveExternalEntities(pXMLObj).ToList()
    End Function

    Public Function ValidateMassiveNovelties(pXMLObj As String) As List(Of SP_ValidateMassiveNovelties_Result) Implements IContractRepository.ValidateMassiveNovelties
        Return _context.SP_ValidateMassiveNovelties(pXMLObj).ToList()
    End Function

    Public Function ValidateMassiveEmployeeSchedule(pXMLObj As String, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String) As List(Of SP_ValidateMassiveEmployeeSchedule_Result) Implements IContractRepository.ValidateMassiveEmployeeSchedule
        Return _context.SP_ValidateMassiveEmployeeSchedule(pXMLObj, Action, Code, Description, Status, Id, Prefix).ToList()
    End Function

    Public Function GetEmployeeScheduleC(Code As String) As EmployeeScheduleC Implements IContractRepository.GetEmployeeScheduleC
        Dim query As IQueryable(Of EmployeeScheduleC)


        query = From e In _context.EmployeeScheduleC
                Where e.Code = Code
                Select e

        If query.Count > 0 Then
            Return query.FirstOrDefault()
        Else
            Return New EmployeeScheduleC()
        End If
    End Function

    Public Function GetContracEmployeeByStatus(IdGroup As Integer) As List(Of Contract) Implements IContractRepository.GetContracEmployeeByStatus

        Dim Query = From e In _context.Contract
                    Where e.Status = 1 AndAlso e.GroupId = IdGroup
                    Select e

        Return Query.ToList()
    End Function

End Class
