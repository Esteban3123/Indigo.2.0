'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Rafael Eduardo Patiño
' Created          : 13-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports System.Data.Entity.Infrastructure
Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class AgreementsCRepository
    Inherits GenericRepository(Of AgreementsC)
    Implements IAgreementsCRepository



    ' Contexto de payroll
    Private _contex As IPayrollUnitOfWork

    Public Sub New(ByVal contexto As IPayrollUnitOfWork)
        MyBase.New(contexto)
        _contex = contexto
    End Sub

    ''' <summary>
    ''' Obtiene un convenio por el consecutivo
    ''' </summary>
    ''' <param name="consecutive">consecutivo</param>
    ''' <param name="tracking">control de segumiento de cambios</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsC(consecutive As String, Optional tracking As Boolean = True) As AgreementsC Implements IAgreementsCRepository.GetAgreementsC
        Dim AgreementsC = From e In _contex.AgreementsC.Include("AgreementsD").Include("Company").Include("Employee").Include("Employee.ThirdParty").Include("KindsAgreements")
                          Where e.Consecutive = consecutive
                          Select e
        Dim ObjAgreementsC = AgreementsC.SingleOrDefault
        If AgreementsC.Count > 0 Then
            If ObjAgreementsC.AccountReceivableAccountingId IsNot Nothing Then
                ObjAgreementsC.InvoiceNumber = (From ara In _contex.AccountReceivableAccounting.AsNoTracking.Include("AccountReceivable").AsNoTracking Where ara.Id = ObjAgreementsC.AccountReceivableAccountingId Select ara.AccountReceivable.InvoiceNumber).FirstOrDefault()
            End If

            Dim ObjAUX = Nothing
            If tracking = False Then
                ObjAUX = (From e In _contex.AgreementsC.AsNoTracking
                          Where e.Consecutive = consecutive
                          Select e).SingleOrDefault
            End If
            ObjAgreementsC.AgreementsCAux = ObjAUX
            Return ObjAgreementsC
        Else
            Return New AgreementsC
        End If
    End Function
    ''' <summary>
    ''' Lista de Convenios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListAgreementsC() As List(Of AgreementsC) Implements IAgreementsCRepository.ListAgreementsC
        Dim AgreementsC = From e In _contex.AgreementsC
                          Select e
        AgreementsC.ToList()
        Return AgreementsC
    End Function
    ''' <summary>
    ''' Lista de Empleados
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListEmployee() As List(Of Employee) Implements IAgreementsCRepository.ListEmployee
        Dim Employee = From e In _contex.Employee.Include("ThirdParty").Include("ThirdParty.Person")
                       Select e
        Employee.ToList().ForEach(Sub(i)
                                      If i.ThirdParty IsNot Nothing AndAlso i.ThirdParty.Person IsNot Nothing Then
                                          i.CodeNameConcatenated = i.ThirdParty.Person.IdentificationNumber & " - " & i.ThirdParty.Person.FirstName & " " & i.ThirdParty.Person.FirstLastName
                                      End If
                                  End Sub)
        Return Employee.ToList()
    End Function

    ''' <summary>
    ''' Lista de Convenios por ID del Empleado, Fecha Inicio del Convenio y Concepto
    ''' </summary>
    ''' <param name="EmployeeId">Id del Empleado</param>
    ''' <param name="PayrollDate">Fecha inicio Nómina</param>
    ''' <param name="ConceptId">Id del Concepto</param>
    ''' <returns>Agreements</returns>
    ''' <remarks></remarks>
    Public Function ListAgreementsCByEmployeeIdStarDate(EmployeeId As String, PayrollDate As Date, ConceptId As String) As List(Of AgreementsC) Implements IAgreementsCRepository.ListAgreementsCByEmployeeIdStarDate
        Dim AgreementsC = From e In _contex.AgreementsC.Include("AgreementsD").Include("Company").Include("Company.ThirdParty")
                          Where e.EmployeeId = EmployeeId And e.StartingDate <= PayrollDate And e.ConceptId = ConceptId And e.State = "2"

        If AgreementsC.Count() > 0 Then
            Return AgreementsC.ToList()
        Else
            Return Nothing
        End If

    End Function

    ''' <summary>
    ''' Obtiene los convenios que tenga el empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsByEmployee(employeeId As Integer) As List(Of AgreementsC) Implements IAgreementsCRepository.GetAgreementsByEmployee
        Dim QueryAgreementsC = From e In _contex.AgreementsC.AsNoTracking().Include("Company").AsNoTracking().Include("Company.ThirdParty").AsNoTracking().Include("AgreementsD").AsNoTracking()
                               Where e.EmployeeId = employeeId And e.State = "2"
                               Select e
        Return QueryAgreementsC.ToList()
    End Function

    ''' <summary>
    ''' Obtiene los convenios por ID
    ''' </summary>
    ''' <param name="AgreementId">Id del Convenio</param>
    ''' <returns>AgreementsC</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsById(AgreementId As Integer) As AgreementsC Implements IAgreementsCRepository.GetAgreementsById
        Dim QueryAgreementsC = From e In _contex.AgreementsC.Include("Company").Include("Company.ThirdParty").Include("KindsAgreements")
                               Where e.Id = AgreementId
                               Select e
        Return QueryAgreementsC.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los convenios por ID
    ''' </summary>
    ''' <param name="AgreementId">Id del Convenio</param>
    ''' <returns>AgreementsC</returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsDByAgreementsCIdPayrollDate(AgreementId As Integer, PayrollDate As Date) As AgreementsD Implements IAgreementsCRepository.GetAgreementsDByAgreementsCIdPayrollDate
        Dim QueryAgreementsD = From e In _contex.AgreementsD.Include("AgreementsC").Include("AgreementsC.KindsAgreements")
                               Where e.AgreementsCId = AgreementId And e.DatePayment = PayrollDate And e.TypePayment = 1
                               Select e
        Return QueryAgreementsD.FirstOrDefault()
    End Function

    ''' <summary>
    ''' Obtiene los convenios que tenga el empleado
    ''' </summary>
    ''' <param name="employeeId">Id del empleado</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetAgreementsByEmployeeStatusVacation(employeeId As Integer, Status As Integer, StartDate As Date) As List(Of AgreementsC) Implements IAgreementsCRepository.GetAgreementsByEmployeeStatusVacation
        Dim QueryAgreementsC = From e In _contex.AgreementsC.Include("Company")
                               Where e.EmployeeId = employeeId And e.State = Status And e.PaidVacation = True And e.StartingDate >= StartDate
                               Select e
        Return QueryAgreementsC.ToList()
    End Function

    Public Function GetAgreementsByEmployeeLiquidationContract(employeeId As Integer, Status As String) As List(Of AgreementsC) Implements IAgreementsCRepository.GetAgreementsByEmployeeLiquidationContract
        Dim QueryAgreementsC = From e In _contex.AgreementsC.Include("Company")
                               Where e.EmployeeId = employeeId And e.State = Status
                               Select e
        Return QueryAgreementsC.ToList()
    End Function

    ''' <summary>
    '''  Guarda un convenio
    ''' </summary>
    ''' <param name="EntityXml"></param>    
    ''' <param name="codeUser"></param>
    ''' <returns></returns>
    Public Function SP_SaveAgreement(EntityXml As String, codeUser As String) As SP_SaveAgreement_Result Implements IAgreementsCRepository.SP_SaveAgreement
        DirectCast(_contex, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _contex.SP_SaveAgreement(EntityXml, codeUser).SingleOrDefault
    End Function

End Class
