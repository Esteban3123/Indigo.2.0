'***********************************************************************
' Assembly         : Domain.Payroll
' Author           : Daniel Eduardo Arévalo 
' Created          : 18-02-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Payroll.Entities


Public Interface IContractRepository
    Inherits IRepository(Of Contract)

    ''' <summary>
    ''' Obtiene un contrato por ID
    ''' </summary>
    ''' <param name="idContract"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractById(idContract As Integer, Optional tracking As Boolean = False) As Contract

    ''' <summary>
    ''' Obtiene una lista de Contratos por Número de Contrato Inicial
    ''' </summary>
    ''' <param name="InitialNumber"></param>
    ''' <param name="tracking"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetContractByInitialNumber(InitialNumber As Integer, EmployeeId As Integer, Optional tracking As Boolean = False) As List(Of Contract)

    Function ValidateMassiveContract(pXMLObj As String) As List(Of SP_ValidateMassiveContract_Result)

    Function GetMassiveContract(pXMLObj As String) As List(Of SP_GetMassiveContract_Result)

    Sub SaveMassiveContract(pXMLObj As String, pCodeUser As String, pIdUser As Integer)

    Function GetMassiveContractExtension(pXMLObj As String) As List(Of SP_GetMassiveContractExtension_Result)

    Function ValidateMassiveContractExtension(pXMLObj As String) As List(Of SP_ValidateMassiveContractExtension_Result)

    Sub SaveMassiveContractExtension(pXMLObj As String)

    Function ValidateMassiveDependentRelatives(pXMLObj As String) As List(Of SP_ValidateMassiveDependentRelatives_Result)

    Function ValidateMassiveExternalEntities(pXMLObj As String) As List(Of SP_ValidateMassiveExternalEntities_Result)

    Function ValidateMassiveNovelties(pXMLObj As String) As List(Of SP_ValidateMassiveNovelties_Result)

    Function ValidateMassiveEmployeeSchedule(pXMLObj As String, Action As String, Code As String, Description As String, Status As Byte, Id As Integer, Prefix As String) As List(Of SP_ValidateMassiveEmployeeSchedule_Result)

    Function GetEmployeeScheduleC(Code As String) As EmployeeScheduleC

    Function GetContracEmployeeByStatus(IdGroup As Integer) As List(Of Contract)

End Interface
