'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Ernesto Cordoba
' Created          : 07/10/2014
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

#End Region

Public Class PIPSServiceGroup
#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IIPSServiceGroup
    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IIPSServiceGroup)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"
    Public Async Sub GetSequense()
        Using model As New Presentation.Billing.MVP.MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ' ''' <summary>
    ' ''' inicializa las cuenta de descuento
    ' ''' </summary>
    Public Sub InitializeDiscountAccountXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.DiscountAccountXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' inicializa cuenta contable de ingresos de la entidad
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeEntityIncomeAccountXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.EntityIncomeAccountXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' inicializa cuenta de gastos de honorarios
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeFeesExpensesAccountXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.FeesExpensesAccountXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' inciializa cuenta contable para ingresos a particulares
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeIndividualIncomeAccountXPO()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.IndividualIncomeAccountXPO = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inciializa cuenta contable nif para reconocimiento de ingresos
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeIncomeRecognition()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.IncomeRecognitionPendingBillingMainAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeCostCenterSpecific()
        Using model As New MIPSServiceGroup("")
            View.CostCenterSpecificXpo = model.ListCostCenterByStatus()
        End Using
    End Sub

    Public Sub InitializeIVAAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.IVAAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingTaxAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingTaxAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeWithholdingICAAccount()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingICAAccountXpo = ModelXpo.ConsultarEntidades(eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    Public Sub InitializeIVA()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.IVAXpo = ModelXpo.ConsultarEntidades(eDataSource.ListGeneralLedgerIva)
        End Using
    End Sub

    Public Sub InitializeWithholdingTaxConcept()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingTaxConceptXpo = ModelXpo.ConsultarEntidades(eDataSource.ListRetentionConcept)
        End Using
    End Sub

    Public Sub InitializeWithholdingICAConcept()
        Using ModelXpo As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.WithholdingICAConceptXpo = ModelXpo.ConsultarEntidades(eDataSource.ListRetentionConcept)
        End Using
    End Sub

    Public Sub InitializeBranchOffice()
        Using model As New MIPSServiceGroup("")
            View.BranchOfficeXpo = model.ListBranchOffice()
        End Using
    End Sub

    Public Sub InitializeFunctionalUnit()
        Using model As New MIPSServiceGroup("")
            View.FunctionalUnitXpo = model.ListFunctionalUnit()
        End Using
    End Sub

    Public Sub InitializeCostCenter()
        Using model As New MIPSServiceGroup("")
            View.CostCenterXpo = model.ListCostCenterByStatus()
        End Using
    End Sub

    Public Sub InitializeAssociatedMainService(Id As Integer)
        Using model As New MIPSServiceGroup("")
            View.AssociatedMainServiceXpo = model.ListAssociatedMainServiceId(TypeService:=0, Id:=Id, type:=1)
        End Using
    End Sub

#End Region
End Class
