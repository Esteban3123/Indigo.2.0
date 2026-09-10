'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 05/08/2014
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
Imports Infrastructure.Data.Xpo.AccountingRepository
Imports Presentation.Base
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP
Imports Presentation.Maintenance.MVP

#End Region

Public Class PDistributionLines

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IDistributionLines

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IDistributionLines)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    Public Async Function GetSequense() As Task
        Using model As New MBlockRecordAndSequensePayments(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Function

    ''' <summary>
    ''' Inicializa el datasource de las cuentas contables
    ''' </summary>
    Public Sub InitializeMainAccount()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas contables provision
    ''' donde sea de nivel auxiliar (5) y maneja movimiento
    ''' </summary>
    Public Sub InitializeMainAccountCostProvision()
        Using model As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountCostProvision = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Inicializa el datasource de los conceptos de pago de acuerdo al id de la cuenta contable seleccionado
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializePaymentConcept(ByVal idMainAccount As Integer)
        Using model As New MBusqueda
            Dim filter() As Object = {3, idMainAccount}
            Me.View.PaymentConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListExpenseConceptsByBehaviorAndIdMainAccount, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAccountPayableConcept()
        Using model As New MBusqueda
            Dim filter() As Object = {True, True}
            Me.View.AccountPayableConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetention, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las unidades operativas
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeOperatingUnit()
        Using model As New MBusqueda
            Me.View.OperatingUnitXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListOperatingUnit)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los conceptos de pago
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeConcept()
        Using model As New MBusqueda
            Dim filter() As Object = {True, True, 2}
            Me.View.ConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountPayableConceptByHandlesRetentionAndConceptType, filter)
        End Using
    End Sub

    ''' <summary>
    ''' inicializa el Datasource del concepto de Nota de cuenta por pagar 
    ''' en estado Activo y las de tipo Especifico
    ''' </summary>
    Public Sub InitializeAccountPayableConceptNote()
        Using modelAccountPayableConceptNoteXPO As New MBusqueda
            Dim filter() As Object = {True, 1}
            Me.View.AccountPayableConceptNote = modelAccountPayableConceptNoteXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListConceptsNotesByConcepType, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de las cuentas contables
    ''' </summary>
    Public Function InitializeMainAccountsResult()
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).AccountingService.ListAccountsByLevel(5, True, 0, Nothing, 2)
    End Function
#End Region

End Class
