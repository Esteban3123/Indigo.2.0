'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 17-10-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Infrastructure.Data.Base
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports Infrastructure.Data
Imports Infrastructure.Data.ModelRepository
Imports Domain.Entities
Imports System.Data.Entity.Infrastructure

Public Class AutoliquidationRepository

    Inherits GenericRepository(Of Autoliquidation)
    Implements IAutoliquidationRepository

    ''' <summary>
    ''' Contexto de payrrol
    ''' </summary>
    Private _context As IPayrollUnitOfWork

    Private _contextGlobal As IGlobalModelUnitOfWork

    ''' <summary>
    ''' Inicia el contexto de payrrol
    ''' </summary>
    ''' <param name="context">Contexto</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal context As IPayrollUnitOfWork, contextGlobal As IGlobalModelUnitOfWork)
        MyBase.New(context)

        _context = context
        _contextGlobal = contextGlobal
    End Sub

    Public Function GetAutoliquidationByGroupId(groupId As String) As List(Of Autoliquidation) Implements IAutoliquidationRepository.GetAutoliquidationByGroupId
        Dim Autoliquidation = From e In _context.Autoliquidation
                              Where e.GroupId = groupId

        If Autoliquidation.Count > 0 Then
            Return Autoliquidation
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Obtiene el listado de fechas de liquidacion de una empresa
    ''' </summary>
    ''' <param name="companyId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDateLiquidationCompany(companyId As Integer) As List(Of String) Implements IAutoliquidationRepository.GetDateLiquidationCompany
        Dim query = From e In _context.Liquidation
                    Where e.Group.CompanyId = companyId And e.RegisterStatus = "C"
                    Order By e.PayrollDateLiquidated Descending
                    Select e.PayrollDateLiquidated
        Dim listResult As New List(Of String)()
        query.ToList().ForEach(Sub(item)
                                   listResult.Add(item.ToString("yyyy-MM"))
                               End Sub)
        Return listResult.Distinct.ToList()
    End Function


    Public Function GenerateAutoliquidation(PayrollDate As Date, WorkCode As String) As List(Of Domain.Payroll.Entities.SP_AutoliquidationFile_Result) Implements IAutoliquidationRepository.GenerateAutoliquidation
        Return _context.SP_AutoliquidationFile(PayrollDate, WorkCode).ToList()
    End Function

    Public Function ListVerifyAutoliquidation(WorkCenterCode As String, PayrollDate As Date) As List(Of VerifyAutoliquidationFile) Implements IAutoliquidationRepository.ListVerifyAutoliquidation

        Dim ListAutoliquidation As New List(Of VerifyAutoliquidationFile)

        Dim Autoliquidation = From e In _context.VerifyAutoliquidationFile
                              Where (String.IsNullOrEmpty(WorkCenterCode) And e.PayrollDateLiquidated = PayrollDate _
                         Or (e.WorkCenter = WorkCenterCode And e.PayrollDateLiquidated = PayrollDate))
                              Select e

        If Autoliquidation.Count > 0 Then

            For Each ObjAutoliquidation As VerifyAutoliquidationFile In Autoliquidation
                Dim EmployeeClass = (From et In _context.Employee.Include("EmployeeType") Where et.Id = ObjAutoliquidation.EmployeeId Select et).FirstOrDefault
                If EmployeeClass IsNot Nothing Then
                    If {"02", "04", "58"}.Contains(EmployeeClass.EmployeeType?.EmployeeClass) Then
                        Continue For
                    End If
                End If

                Dim thirdId = (From ge In _context.Employee Where ge.Id = ObjAutoliquidation.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
                Dim thirdParty = (From ge In _context.ThirdParty Where ge.Id = thirdId Select ge).FirstOrDefault()
                Dim IdPerson = thirdParty.PersonId
                ObjAutoliquidation.NitEmployee = thirdParty.Nit
                ObjAutoliquidation.NameEmployee = thirdParty.Name

                Dim groupInfo = (From g In _context.Group.Include("PayrollParameter")
                                 Join c In _context.Contract On c.GroupId Equals g.Id
                                 Join e In _context.Employee On c.EmployeeId Equals e.Id
                                 Where e.Id = ObjAutoliquidation.EmployeeId
                                 Select g.SMMLVAmountExemption, g.PayrollParameter.LegalSalaryMinimum).FirstOrDefault()

                If groupInfo IsNot Nothing Then
                    ObjAutoliquidation.SMMLVAmountExemption = If(groupInfo.SMMLVAmountExemption, 0)
                    ObjAutoliquidation.LegalSalaryMinimum = groupInfo.LegalSalaryMinimum
                End If

                Dim person = (From t In _context.Person Where t.Id = IdPerson Select t).FirstOrDefault()
                ObjAutoliquidation.FirstName = person.FirstName
                ObjAutoliquidation.SecondName = person.SecondName
                ObjAutoliquidation.FirstLastName = person.FirstLastName
                ObjAutoliquidation.SecondLastName = person.SecondLastName
                ObjAutoliquidation.DocumentType = person.IdentificationType

                Dim Employee = (From ge In _context.Employee Where ge.Id = ObjAutoliquidation.EmployeeId).FirstOrDefault()
                Dim contributorSubId = (From et In _context.EmployeeType Where et.Id = Employee.EmployeeTypeId Select et.ContributorSubtypeId).FirstOrDefault()
                If contributorSubId IsNot Nothing Then
                    Dim ContributorSubtype = (From cst In _context.ContributorSubtype Where cst.Id = contributorSubId.Value).FirstOrDefault()
                    ObjAutoliquidation.ContributorSubtype = ContributorSubtype.Code - 1
                End If

                ListAutoliquidation.Add(ObjAutoliquidation)
            Next

            Return ListAutoliquidation
        Else
            Return Nothing
        End If
    End Function

    ''' <summary>
    ''' Función que se encarga de validar que todos los grupos a liquidar tengan parametrizado el SMMLVAmountExemption
    ''' </summary>
    ''' <param name="listLiquidation"></param>
    ''' <returns></returns>
    Public Function ValidateSMMLVAmountExemption(ByVal listLiquidation As List(Of Liquidation)) As String Implements IAutoliquidationRepository.ValidateSMMLVAmountExemptionParametrization
        Dim unparametrizedGroups As New List(Of String)
        Dim groupIdsValidated As New HashSet(Of Integer)()

        For Each liquidation As Liquidation In listLiquidation
            ' Si el GroupId ya fue procesado, continuar con el siguiente.
            If groupIdsValidated.Contains(liquidation.GroupId) Then
                Continue For
            End If

            ' Obtener el grupo y verificar si tiene parametrizado el SMMLVAmountExemption.
            Dim result = (From g In _context.Group Where g.Id = liquidation.GroupId
                          Select g.Code, g.Name, g.SMMLVAmountExemption).FirstOrDefault()

            ' Si el grupo no tiene SMMLVAmountExemption, agregarlo a la lista.
            If result IsNot Nothing AndAlso result.SMMLVAmountExemption Is Nothing Then
                unparametrizedGroups.Add(result.Code & " - " & result.Name)
            End If

            ' Marcar el GroupId como procesado para evitar duplicados.
            groupIdsValidated.Add(liquidation.GroupId)
        Next

        Return If(unparametrizedGroups.Any(), String.Join(", ", unparametrizedGroups), Nothing)
    End Function

    Public Function SP_ConfirmDisconfirmVerifyAutoliquidation(WorkCenterCode As String, PayrollDateLiquidated As Date, FlagConfirm As Boolean, codeUser As String) As SP_ConfirmDisconfirmVerifyAutoliquidation_Result Implements IAutoliquidationRepository.SP_ConfirmDisconfirmVerifyAutoliquidation
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_ConfirmDisconfirmVerifyAutoliquidation(WorkCenterCode, PayrollDateLiquidated, FlagConfirm, codeUser).SingleOrDefault
    End Function

    Public Function GetVerifyAutoliquidationById(IdVerifyAutoliquidation As Integer) As VerifyAutoliquidationFile Implements IAutoliquidationRepository.GetVerifyAutoliquidationById

        Dim Autoliquidation = From e In _context.VerifyAutoliquidationFile
                              Where e.Id = IdVerifyAutoliquidation

        If Autoliquidation IsNot Nothing AndAlso Autoliquidation.Count > 0 Then

            Dim ObjAutoliquidation = Autoliquidation.FirstOrDefault()

            Dim thirdId = (From ge In _context.Employee Where ge.Id = ObjAutoliquidation.EmployeeId Select ge.ThirdPartyId).FirstOrDefault()
            Dim IdPerson = (From ge In _context.ThirdParty Where ge.Id = thirdId Select ge.PersonId).FirstOrDefault()
            ObjAutoliquidation.NitEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Nit).FirstOrDefault()
            ObjAutoliquidation.NameEmployee = (From t In _context.ThirdParty Where t.Id = thirdId Select t.Name).FirstOrDefault()

            Return ObjAutoliquidation
        Else
            Return Nothing
        End If
    End Function

    Public Function SP_SaveMassiveVerifyAutoliquidation_Result(XMLObject As String, codeUser As String) As List(Of SP_SaveMassiveVerifyAutoliquidation_Result) Implements IAutoliquidationRepository.SP_SaveMassiveVerifyAutoliquidation_Result
        DirectCast(_context, IObjectContextAdapter).ObjectContext.CommandTimeout = 3600
        Return _context.SP_SaveMassiveVerifyAutoliquidation(XMLObject, codeUser).ToList()
    End Function
End Class
