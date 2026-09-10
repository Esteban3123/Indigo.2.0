'***********************************************************************
' Assembly         : Presentacion.Treasury.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 17/01/2017
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Presentation.Base
Imports Presentation.Common.MVP

#End Region

Public Class PConstitutionCashSmaller
    ''' <summary>
    ''' variable para comunicar con la interfaz
    ''' </summary>
    Dim View As IConstitutionCashSmaller

    ''' <summary>
    ''' variable que obtiene los valores de la sesion
    ''' </summary>
    Dim Indigo As SessionValues

    ''' <summary>
    ''' Constructor que comunica con la interfaz
    ''' </summary>
    Public Sub New(ByRef iView As IConstitutionCashSmaller)
        If iView Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If
        View = iView
        Indigo = SessionValues.Instance
    End Sub

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    ''' <summary>
    ''' Obtiene la secuencia
    ''' </summary>
    Public Async Sub GetSequence()
        Using modelCommmonTreasury As New MCommonTreasury(View.MyTag)
            Me.View.Sequence = Await modelCommmonTreasury.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Datasource caja menor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCashRegisterSmaller()
        View.CashRegisterSmallerXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListCashRegisterByTypeAndStatus(1, True)
    End Sub

    ''' <summary>
    ''' Datasource caja mayor
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeCashRegister()
        View.CashRegisterXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListCashRegisterByCurrencyTypeAndStatus(View.SelectedCashRegisterSmaller.CurrencyId, 2, True)
    End Sub

    ''' <summary>
    ''' Datasource cuenta bancaria a entidades
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEntityBankAccount()
        View.EntityBankAccountXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).TreasuryService.ListEntityBankAccountByCurrency(View.SelectedCashRegisterSmaller.CurrencyId, True)
    End Sub

End Class