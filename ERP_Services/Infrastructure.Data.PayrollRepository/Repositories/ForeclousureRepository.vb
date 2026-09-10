'***********************************************************************
' Assembly         : Infrastructure.Data.PayrollRepository
' Author           : Daniel Eduardo Arévalo
' Created          : 12-09-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll
Imports Domain.Payroll.Entities
Imports Infrastructure.Data.Base

Public Class ForeclousureRepository
    Inherits GenericRepository(Of Foreclousure)
    Implements IForeclousureRepository

    ' Contexto de payroll
    Private _contex As IPayrollUnitOfWork

    Public Sub New(ByVal contexto As IPayrollUnitOfWork)
        MyBase.New(contexto)
        _contex = contexto
    End Sub


    Private Function GetForeclousure(consecutive As String, Optional tracking As Boolean = True) As Foreclousure Implements IForeclousureRepository.GetForeclousure

        Dim Foreclousure = From e In _contex.Foreclousure.Include("ForeclousureDetail")
                           Where e.Code = consecutive
                           Select e
        Dim ObjForeclousure = Foreclousure.FirstOrDefault()
        If Foreclousure.Count > 0 Then
            Dim ObjAUX As New Foreclousure()
            If tracking = False Then
                ObjAUX = (From e In _contex.Foreclousure.AsNoTracking
                          Where e.Code = consecutive
                          Select e).FirstOrDefault()

                If ObjAUX IsNot Nothing Then
                    Dim ThirdParty = (From c As ThirdParty In Me._contex.ThirdParty.AsNoTracking() Where c.Id = ObjAUX.IdApplicant Select c).FirstOrDefault
                    ObjForeclousure.NameThirdPartyApplicant = ThirdParty.Nit + " - " + ThirdParty.Name

                    Dim Concept = (From d As Concept In Me._contex.Concept.AsNoTracking() Where d.Id = ObjAUX.IdConcept Select d).FirstOrDefault
                    ObjForeclousure.NameConcept = Concept.Name

                    Dim Company = (From e As Company In Me._contex.Company.AsNoTracking() Where e.Id = ObjAUX.IdJudgment Select e).FirstOrDefault
                    ObjForeclousure.NameCompanyJudgment = Company.Name

                    Dim City = (From f As City In Me._contex.City.AsNoTracking() Where f.Id = ObjAUX.IdCity Select f).FirstOrDefault
                    ObjForeclousure.NameCity = City.Name

                    Dim ThirdPartyBeneficiary = (From g As ThirdParty In Me._contex.ThirdParty.AsNoTracking() Where g.Id = ObjAUX.IdBeneficiary Select g).FirstOrDefault
                    ObjForeclousure.NameThirdPartyBeneficiary = ThirdPartyBeneficiary.Name

                End If

            End If
            ObjForeclousure.ForeclousureAux = ObjAUX
            Return ObjForeclousure
        Else
            Return New Foreclousure
        End If
    End Function

    Public Function ListForeclousure() As List(Of Foreclousure) Implements IForeclousureRepository.ListForeclousure
        Dim Foreclousure = From e In _contex.Foreclousure
                           Select e
        Foreclousure.ToList()
        Return Foreclousure
    End Function

    Public Function LisForeclousureByEmployeeIdStarDate(EmployeeId As String, PayrollDate As Date, ConceptId As String) As List(Of Foreclousure) Implements IForeclousureRepository.LisForeclousureByEmployeeIdStarDate
        Dim Foreclousure = From e In _contex.Foreclousure.Include("ForeclousureDetail").Include("Company").Include("Company.ThirdParty")
                           Where e.IdEmployee = EmployeeId And e.InitialDate <= PayrollDate And e.IdConcept = ConceptId And e.State = 2

        If Foreclousure.Count() > 0 Then
            Return Foreclousure.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetForeclousureByEmployee(employeeId As Integer) As List(Of Foreclousure) Implements IForeclousureRepository.GetForeclousureByEmployee
        Dim QueryForeclosure = From e In _contex.Foreclousure
                               Where e.IdEmployee = employeeId
                               Select e
        Return QueryForeclosure.ToList()
    End Function

    Public Function GetForeclousureById(ForeclousureId As Integer) As Foreclousure Implements IForeclousureRepository.GetForeclousureById
        Dim QueryForeclousure = From e In _contex.Foreclousure.Include("Company").Include("Company.ThirdParty")
                                Where e.Id = ForeclousureId
                                Select e
        Return QueryForeclousure.FirstOrDefault()
    End Function

    Public Function GetForeclousureDetailByForeclousureIdPayrollDate(ForeclousureID As Integer, PayrollDate As Date) As ForeclousureDetail Implements IForeclousureRepository.GetForeclousureDetailByForeclousureIdPayrollDate
        Dim QueryForeclousureDetail = From e In _contex.ForeclousureDetail.Include("Foreclousure")
                                      Where e.IdForeclousure = ForeclousureID And e.DatePayment = PayrollDate And e.TypePayment = 1
                                      Select e
        Return QueryForeclousureDetail.FirstOrDefault()
    End Function

    Public Function ListForeclousureByDate(PayrollDate As Date) As List(Of Foreclousure) Implements IForeclousureRepository.ListForeclousureByDate
        Dim Foreclousure = From e In _contex.Foreclousure.Include("Company").Include("Company.ThirdParty").Include("Employee")
                           Where e.InitialDate <= PayrollDate And e.State = 2

        If Foreclousure.Count() > 0 Then
            Return Foreclousure.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function ListForeclousureByDateNoStatus(PayrollDate As Date) As List(Of Foreclousure) Implements IForeclousureRepository.ListForeclousureByDateNoStatus
        Dim Foreclousure = From e In _contex.Foreclousure.Include("Company").Include("Company.ThirdParty").Include("Employee")
                           Where e.InitialDate <= PayrollDate

        If Foreclousure.Count() > 0 Then
            Return Foreclousure.ToList()
        Else
            Return Nothing
        End If
    End Function

    Public Function GetForeclousureByEmployeeStatus(employeeId As Integer, Status As Integer) As List(Of Foreclousure) Implements IForeclousureRepository.GetForeclousureByEmployeeStatus
        Dim QueryForeclosure = From e In _contex.Foreclousure
                               Where e.IdEmployee = employeeId And e.State = Status
                               Select e
        Return QueryForeclosure.ToList()
    End Function

    Public Function GetForeclousureByEmployeeStatusVacation(employeeId As Integer, Status As Integer) As List(Of Foreclousure) Implements IForeclousureRepository.GetForeclousureByEmployeeStatusVacation
        Dim QueryForeclosure = From e In _contex.Foreclousure
                               Where e.IdEmployee = employeeId And e.State = Status And e.AffectVacation = True
                               Select e
        Return QueryForeclosure.ToList()
    End Function
End Class
