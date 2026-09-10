'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 07/10/2016
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
Imports Infrastructure.Data.Xpo

#End Region

Public Class PContractAccountingStructure

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IContractAccountingStructure

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    Dim filter() As Object = {5, True}

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IContractAccountingStructure)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

#Region "Initialize MainAccounts"

    Public Sub InitializeAccountRecoveryFee()
        Using model As New MBusqueda
            Me.View.AccountRecoveryFeeIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeServicesPendingBilling()
        Using model As New MBusqueda
            Me.View.ServicesPendingBillingMainAccountXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountParticular()
        Using model As New MBusqueda
            Me.View.AccountParticularIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountWithoutRadicate()
        Using model As New MBusqueda
            Me.View.AccountWithoutRadicateIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountRadicate()
        Using model As New MBusqueda
            Me.View.AccountRadicateIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountObjectionRemedied()
        Using model As New MBusqueda
            Me.View.AccountObjectionRemediedIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountConciliation()
        Using model As New MBusqueda
            Me.View.AccountConciliationIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountLegalCollection()
        Using model As New MBusqueda
            Me.View.AccountLegalCollectionIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountHardCollection()
        Using model As New MBusqueda
            Me.View.AccountHardCollectionIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountDebitOrder()
        Using model As New MBusqueda
            Me.View.AccountDebitOrderIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeAccountCreditOrder()
        Using model As New MBusqueda
            Me.View.AccountCreditOrderIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeDebitAccountDeterioration()
        Using model As New MBusqueda
            Me.View.DebitAccountDeteriorationIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCreditAccountDeterioration()
        Using model As New MBusqueda
            Me.View.CreditAccountDeteriorationIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeReversalAccountDeterioration()
        Using model As New MBusqueda
            Me.View.ReversalAccountDeteriorationIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializePreviousPeriodReversalAccountDeterioration()
        Using model As New MBusqueda
            Me.View.PreviousPeriodReversalAccountDeteriorationIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCreditProvisionAccount()
        Using model As New MBusqueda
            Me.View.CreditProvisionAccountIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeDebitProvisionAccount()
        Using model As New MBusqueda
            Me.View.DebitProvisionAccountIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

#End Region

#End Region

End Class
