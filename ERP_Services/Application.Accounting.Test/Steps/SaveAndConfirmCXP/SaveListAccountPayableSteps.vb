Imports System
Imports Domain.Base.Entities
Imports Domain.Entities

Imports Domain.Entities.Service
Imports TechTalk.SpecFlow

Namespace Application.Accounting.Test

    <Binding()>
    Public Class SaveListAccountPayableSteps
#Region "Fields"
        Private ReadOnly _AccountingContext As AccountingContext
        Private _ListAccountPayable As New List(Of Domain.Entities.AccountPayable)
        Private _listAccountPayableDetailConcept As New Domain.Entities.TrackableCollection(Of Domain.Entities.AccountPayableDetailConcept)
        Private _listAccountPayableShares As New Domain.Entities.TrackableCollection(Of Domain.Entities.AccountPayableShares)
        Private _actionResult As New ActionResult(Of List(Of String))
#End Region

#Region "Builder"
        Public Sub New(AccountingContext As AccountingContext)
            _AccountingContext = AccountingContext
        End Sub
#End Region
        <TechTalk.SpecFlow.Given("Genero los Maestros y Detalles")>
        Public Sub DadoGeneroLosMaestrosYDetalles()

            For i As Integer = 1 To 10
                Dim _AccountPayableDetailConcept As New Domain.Entities.AccountPayableDetailConcept With
                                                 {.IdConceptAccountPayable = 2,
                                                 .IdAccount = 4025,
                                                 .IdThirdParty = 59943,
                                                 .Nature = 1,
                                                 .BaseValue = 10000,
                                                 .BillingValue = 0,
                                                 .Value = 10000,
                                                 .Detail = "Prueba UnitTest"}


                Dim _accountPayable As New Domain.Entities.AccountPayable With
                    {.EntityName = "AccountPayable",
                     .IdSupplier = 1246,
                     .IdThirdParty = 59943,
                     .IdAccount = 4130,
                     .BillNumber = DateTime.Now.ToString("yyyyMMddhhmmssmmm") + CStr(i),
                     .BillDate = DateTime.Now,
                     .DocumentDate = DateTime.Now,
                     .ServicePeriodDate = DateTime.Now,
                     .FilingUnitId = 20,
                     .SupplierTypeId = 27,
                     .Term = 30,
                     .Coments = "Prueba UnitTest",
                     .Status = 1,
                     .InitialBalance = 0,
                     .PreviousBudget = 0,
                     .Shares = 1,
                     .InvoiceValue = 10000,
                     .Value = 10000,
                     .Balance = 10000,
                     .IdOperatingUnit = 14,
                     .IdSuppliersDistributionLines = 1257}
                _accountPayable.AccountPayableDetailConcept.Add(_AccountPayableDetailConcept)
                _accountPayable.ExpirationDate = PaymentServices.AddDaysDate(_accountPayable.Term, _accountPayable.DocumentDate)
                Dim _AccountPayableShares = PaymentServices.CreateShares(1, 10000, _accountPayable.ExpirationDate)

                For Each itemShares As AccountPayableShares In _AccountPayableShares
                    _accountPayable.AccountPayableShares.Add(itemShares)
                Next
                _ListAccountPayable.Add(_accountPayable)

            Next


        End Sub

        <TechTalk.SpecFlow.Given("Guardo y confirmo las CXP (.*)")>
        Public Sub DadoGuardoYConfirmoLasCXP(ByVal p0 As Int32)
            Dim _currentSequence As Long
            Dim sequence = _AccountingContext._paymentsSequenseAdminService.GetSequenseByIdForm("730")
            If sequence.Scope.Equals("O") Then 'El ambito es a nivel de organización
                _currentSequence = sequence.PaymentsSecuenceDetail(0).Id
            ElseIf sequence.Scope.Equals("OU") Then 'El ambito es a nivel de unidad operativa
                If sequence.PaymentsSecuenceDetail.Any(Function(S) S.IdOperatingUnit = _ListAccountPayable.FirstOrDefault.IdOperatingUnit) Then
                    _currentSequence = sequence.PaymentsSecuenceDetail.Where(Function(s) s.IdOperatingUnit = _ListAccountPayable.FirstOrDefault.IdOperatingUnit).SingleOrDefault().Id
                End If
            End If
            _actionResult = _AccountingContext._accountPayableAdminService.SaveListAccountPayable(_ListAccountPayable, Nothing, True, _AccountingContext._audit, _currentSequence)

            If Not _actionResult.StateResult Then
                Assert.Fail("Hubo un Error: " + _actionResult.Message)
            End If
        End Sub

        <TechTalk.SpecFlow.Then("Estas son almacenadas y Confirmadas")> _
        Public Sub EntoncesEstasSonAlmacenadasYConfirmadas()
            Assert.IsTrue(_actionResult.StateResult, _actionResult.Message)
        End Sub

    End Class

End Namespace
