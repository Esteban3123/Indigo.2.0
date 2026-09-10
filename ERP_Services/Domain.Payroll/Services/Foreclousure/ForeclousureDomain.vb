'***********************************************************************
' Assembly         : Application.Payroll
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 10-10-2018
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
Imports Domain.Common.Entities
Imports Domain.Base.Entities
Imports Domain.Payroll.Entities
Imports Domain.Payroll
Imports System.Text
Imports Infrastructure.CrossCutting.Base
Imports Domain.Entities

Public Class ForeclousureDomain
    Implements IForeclosureDomain

#Region "Fields"
    ''' <summary>
    ''' constante con el nombre del modulo
    ''' </summary>

    Private _thirdPartyRepository As IThirdPartyRepository

#End Region

    Public Sub New(thirdPartyRepository As IThirdPartyRepository)
        _thirdPartyRepository = thirdPartyRepository
    End Sub

    Public Function ForeclousureCalculate(ForeclousureEmployee As List(Of Foreclousure), payrollDateLiquidated As Date, conceptTotalValue As Double) As List(Of Foreclousure) Implements IForeclosureDomain.ForeclousureCalculate
        Dim ConceptValue As Double = 0
        Dim ForeclousureDetail As New ForeclousureDetail
        Dim ForeclousureList As New List(Of Foreclousure)

        If ForeclousureEmployee IsNot Nothing AndAlso ForeclousureEmployee.Count > 0 Then

            For Each ObjForeclousure As Foreclousure In ForeclousureEmployee

                If ObjForeclousure.CurrentBalance > 0 Then

                    Dim QuoteNumber As Integer = 0
                    If ObjForeclousure.QuoteNumber = 0 Then
                        QuoteNumber = 1
                    Else
                        QuoteNumber = ObjForeclousure.QuoteNumber
                    End If

                    ConceptValue = ObjForeclousure.QuoteValue / QuoteNumber

                    If ObjForeclousure.CurrentBalance < ConceptValue Then
                        ConceptValue = ObjForeclousure.CurrentBalance
                    End If

                End If

                If conceptTotalValue > ConceptValue Then
                    ConceptValue = conceptTotalValue
                End If

                If ObjForeclousure.ForeclousureDetail.Any(Function(x) x.DatePayment = payrollDateLiquidated And x.TypePayment = 3) = False Then

                    ForeclousureDetail.ShareValuePaid = ConceptValue
                    ForeclousureDetail.DatePayment = payrollDateLiquidated
                    ForeclousureDetail.TypePayment = 1 'Pago por Nómina
                    ForeclousureDetail.StateShare = "Pago registrado por Nómina en la Fecha " & payrollDateLiquidated.ToString()
                    ForeclousureDetail.MarkAsAdded()

                    ObjForeclousure.ForeclousureDetail.Add(ForeclousureDetail)

                    If ObjForeclousure.DiscountClass = 3 Or ObjForeclousure.DiscountClass = 4 Then
                        ObjForeclousure.CurrentBalance = ObjForeclousure.CurrentBalance - ConceptValue
                    End If

                    ObjForeclousure.MarkAsModified()

                    ForeclousureList.Add(ObjForeclousure)

                End If

            Next

        End If


        Return ForeclousureList
    End Function

    Public Function GenerateArchive(PayrollLiquidationDetail As List(Of LiquidationDetail), Company As Domain.Payroll.Entities.Company, ListForeclousure As List(Of Foreclousure)) As ActionMessageResult(Of StringBuilder) Implements IForeclosureDomain.GenerateArchive
        Dim ActionMessageReturn As New ActionMessageResult(Of StringBuilder)
        Dim result As New StringBuilder()
        Dim DateTransaction = Date.Now
        Dim DateNow As String = DateTransaction.ToString("yyyyMMdd")
        Dim TotalPaid As Double = 0

        ActionMessageReturn.StateResult = True

        Try

            If PayrollLiquidationDetail IsNot Nothing AndAlso PayrollLiquidationDetail.Count > 0 Then

                Dim lineHead As String = Utils.StringPad(" ", 23, " ", Utils.PadType.STR_PAD_LEFT) '1
                lineHead &= Utils.StringPad(PayrollLiquidationDetail.Count(), 10, 0, Utils.PadType.STR_PAD_LEFT) '2
                lineHead &= Utils.StringPad(" ", 40, " ", Utils.PadType.STR_PAD_LEFT) '3
                lineHead &= Utils.StringPad(3, 1, 0, Utils.PadType.STR_PAD_LEFT) '4
                lineHead &= Utils.StringPad(" ", 1, " ", Utils.PadType.STR_PAD_LEFT) '5
                lineHead &= Utils.StringPad(Company.ThirdParty.Nit + Company.ThirdParty.DigitVerification, 10, " ", Utils.PadType.STR_PAD_LEFT) '5.1
                lineHead &= Utils.StringPad(" ", 12, " ", Utils.PadType.STR_PAD_LEFT) '6
                lineHead &= Utils.StringPad(Company.ThirdParty.Name, 20, " ", Utils.PadType.STR_PAD_RIGHT) '7
                lineHead &= Utils.StringPad(" ", 20, " ", Utils.PadType.STR_PAD_RIGHT) '8
                lineHead &= Utils.StringPad(" ", 63, " ", Utils.PadType.STR_PAD_RIGHT) '9

                result.Append(lineHead)

                Dim Consecutive As Integer = 1

                For Each LiquidationEmployee As LiquidationDetail In PayrollLiquidationDetail

                    Dim lineDet As String = vbCrLf
                    Dim ObjForeclousure As Foreclousure = ListForeclousure.Where(Function(x) x.IdConcept = LiquidationEmployee.ConceptId And x.IdEmployee = LiquidationEmployee.Liquidation.EmployeeId).FirstOrDefault()

                    If ObjForeclousure.Company.CodeJudgmentAccount Is Nothing Or ObjForeclousure.Company.CodeJudgmentAccount = String.Empty Then
                        ActionMessageReturn.Message = "El Juzgado " + ObjForeclousure.Company.Nit + " - " + ObjForeclousure.Company.Name + " No tiene parametrizado el Código"
                        ActionMessageReturn.StateResult = False
                        Return ActionMessageReturn
                    End If

                    lineDet &= Utils.StringPad(Consecutive, 6, 0, Utils.PadType.STR_PAD_LEFT) '1
                    lineDet &= Utils.StringPad(DateNow, 8, " ", Utils.PadType.STR_PAD_RIGHT) '2
                    lineDet &= Utils.StringPad("0030", 4, " ", Utils.PadType.STR_PAD_RIGHT) '3
                    lineDet &= Utils.StringPad(ObjForeclousure.CodeDestinationOffice, 4, " ", Utils.PadType.STR_PAD_LEFT) '4

                    Dim ConceptType As Integer
                    Select Case ObjForeclousure.ForeclousureType
                        Case 1
                            ConceptType = 1
                        Case 2
                            ConceptType = 6
                        Case 3
                            ConceptType = 5
                    End Select

                    lineDet &= Utils.StringPad(ConceptType, 1, " ", Utils.PadType.STR_PAD_RIGHT) '5
                    lineDet &= Utils.StringPad(0, 10, 0, Utils.PadType.STR_PAD_RIGHT) '6 -- De acuerdo a lo conversado con el ingeniero Eduards Navarro y Gildardo de Nómina, este campo va en cero.
                    lineDet &= Utils.StringPad(ObjForeclousure.Company.CodeJudgmentAccount, 12, 0, Utils.PadType.STR_PAD_RIGHT) '7 
                    lineDet &= Utils.StringPad(0, 12, 0, Utils.PadType.STR_PAD_RIGHT) '8 -- De acuerdo a correo enviado por Gina, van solo CEROS (0)
                    lineDet &= Utils.StringPad(LiquidationEmployee.ConceptTotalValue.ToString() + ".00", 16, 0, Utils.PadType.STR_PAD_LEFT) '9

                    'Demandante
                    Dim ObjApplicantThirdParty = _thirdPartyRepository.GetThirdPartyById(ObjForeclousure.IdApplicant)

                    Dim ApplicantIdentificationType As Integer
                    Dim ApplicantNit As String
                    Select Case ObjApplicantThirdParty.Person.IdentificationType
                        Case 0
                            ApplicantIdentificationType = 1
                        Case 1
                            ApplicantIdentificationType = 2
                        Case 7
                            ApplicantIdentificationType = 3
                        Case 4
                            ApplicantIdentificationType = 4
                        Case 2
                            ApplicantIdentificationType = 5
                    End Select

                    If ApplicantIdentificationType = 3 Then
                        ApplicantNit = ObjApplicantThirdParty.Nit + ObjApplicantThirdParty.DigitVerification
                    Else
                        ApplicantNit = ObjApplicantThirdParty.Nit
                    End If


                    lineDet &= Utils.StringPad(ApplicantIdentificationType, 1, 0, Utils.PadType.STR_PAD_RIGHT) '10
                    lineDet &= Utils.StringPad(ApplicantNit, 11, 0, Utils.PadType.STR_PAD_LEFT) '11

                    'Demandado

                    Dim ObjEmployeeThirdParty = _thirdPartyRepository.GetThirdPartyById(ObjForeclousure.Employee.ThirdPartyId)

                    Dim EmployeeIdentificationType As Integer
                    Dim EmployeeNit As String
                    Select Case ObjEmployeeThirdParty.Person.IdentificationType
                        Case 0
                            EmployeeIdentificationType = 1
                        Case 1
                            EmployeeIdentificationType = 2
                        Case 7
                            EmployeeIdentificationType = 3
                        Case 4
                            EmployeeIdentificationType = 4
                        Case 2
                            EmployeeIdentificationType = 5
                    End Select

                    If EmployeeIdentificationType = 3 Then
                        EmployeeNit = ObjEmployeeThirdParty.Nit + ObjEmployeeThirdParty.DigitVerification
                    Else
                        EmployeeNit = ObjEmployeeThirdParty.Nit
                    End If

                    lineDet &= Utils.StringPad(EmployeeIdentificationType, 1, 0, Utils.PadType.STR_PAD_RIGHT) '12
                    lineDet &= Utils.StringPad(EmployeeNit, 11, 0, Utils.PadType.STR_PAD_LEFT) '13

                    If ApplicantIdentificationType = 3 Then
                        lineDet &= Utils.StringPad(ObjApplicantThirdParty.Name, 20, " ", Utils.PadType.STR_PAD_RIGHT) '14
                        lineDet &= Utils.StringPad(ObjApplicantThirdParty.Name, 20, " ", Utils.PadType.STR_PAD_RIGHT) '15
                    Else
                        lineDet &= Utils.StringPad(ObjApplicantThirdParty.Person.FirstLastName + " " + ObjApplicantThirdParty.Person.SecondLastName, 20, " ", Utils.PadType.STR_PAD_RIGHT) '14
                        lineDet &= Utils.StringPad(ObjApplicantThirdParty.Person.FirstName + " " + ObjApplicantThirdParty.Person.SecondName, 20, " ", Utils.PadType.STR_PAD_RIGHT) '15
                    End If

                    lineDet &= Utils.StringPad(ObjEmployeeThirdParty.Person.FirstLastName + " " + ObjEmployeeThirdParty.Person.SecondLastName, 20, " ", Utils.PadType.STR_PAD_RIGHT) '16
                    lineDet &= Utils.StringPad(ObjEmployeeThirdParty.Person.FirstName + " " + ObjEmployeeThirdParty.Person.SecondName, 20, " ", Utils.PadType.STR_PAD_RIGHT) '17
                    lineDet &= Utils.StringPad(ObjForeclousure.ProcessNumber, 23, 0, Utils.PadType.STR_PAD_LEFT) '18

                    result.Append(lineDet)

                    Consecutive += 1

                Next

            End If

            ActionMessageReturn.ObjectEmbbeded = result
            Return ActionMessageReturn

        Catch ex As Exception
            ActionMessageReturn.Message = ex.Message.ToString()
            ActionMessageReturn.StateResult = False
            Return ActionMessageReturn
        End Try

    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        disposedValue = True
    End Sub

    ' TODO: override Finalize() only if Dispose(disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
        Dispose(True)
        ' TODO: uncomment the following line if Finalize() is overridden above.
        ' GC.SuppressFinalize(Me)
    End Sub
#End Region


End Class


