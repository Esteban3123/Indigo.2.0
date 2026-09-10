'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 19/04/2021
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Presentation.Base
Imports Presentation.Controls.MVP
Imports Infrastructure.CrossCutting.Resources
Imports DevExpress.Data.Linq
Imports Presentation.Accounting.MVP
Imports Presentation.Common.MVP
Imports Domain.Entities.Service
Imports Domain.Entities
Imports Domain.Base.Entities
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.TreasuryRepository
Imports Infrastructure.Data.Xpo
#End Region

Public Class PBankConciliationAutomatic

    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IBankConciliationAutomatic

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IBankConciliationAutomatic)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Function LoadDefinitionLayout() As Task
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Function

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequence = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Lista las cuentas bancarias
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEntityBankAccount()
        View.EntityBankAccountXpo = Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByStatusAndConciliationAccount(True, True)
    End Sub

    ''' <summary>
    ''' Calcula el saldo anterior del banco
    ''' </summary>
    ''' <returns></returns>
    Public Function CalculateBalance(EntityBankAccountId As Integer, ByVal DocumentDate As Date) As Decimal
        Dim DateEnd As Date = DocumentDate.AddDays(1)
        Return Infrastructure.Data.Xpo.XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.GetReportTreasuryEntityBank(EntityBankAccountId, DateEnd, "2, 4")
    End Function




    Public Async Function ListBankReconciliationAutomaticDetail(bankReconciliationId As Integer, EntityBankAccountId As Integer, ByVal DocumentDate As Date) As Task(Of ActionResult(Of List(Of BankReconciliationAutomaticDetail)))
        Using model As New MBankConciliationAutomatic(Me.View.MyTag)
            Dim criterias As New Dictionary(Of String, String)
            criterias.Add("BankReconciliationAutomaticId", bankReconciliationId)
            criterias.Add("EntityBankAccountId", EntityBankAccountId)
            criterias.Add("DocumentDate", DocumentDate)

            Return Await model.ListBankReconciliationAutomaticDetail(criterias)
        End Using
    End Function


    Public Async Function ListGetUploadBankStatementsDetailByEntityBankAccountAutomatic(bankReconciliationId As Integer, EntityBankAccountId As Integer, Month As Integer, Year As Integer) As Task(Of ActionResult(Of List(Of BankReconciliationAutomaticExtractDetail)))
        Using model As New MBankConciliationAutomatic(Me.View.MyTag)
            Dim criterias As New Dictionary(Of String, String)
            criterias.Add("BankReconciliationAutomaticId", bankReconciliationId)
            criterias.Add("EntityBankAccountId", EntityBankAccountId)
            criterias.Add("Month", Month)
            criterias.Add("Year", Year)

            Return Await model.GetUploadBankStatementsDetailByEntityBankAccountAutomatic(criterias)
        End Using
    End Function



End Class