'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Carlos Mario Arias Rubiano
' Created          : 19/03/2014
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
Imports Presentation.Common.MVP
Imports Presentation.Controls.MVP

#End Region

Public Class PConceptsAccountsPayable

#Region "Variables and Constructors"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IConceptsAccountsPayable

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
    Public Sub New(ByRef iview As IConceptsAccountsPayable)
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
    ''' Inicializa el datasource de las ciudades
    ''' </summary>
    Public Sub Initialize()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {5, True}
            Me.View.AccountsXpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByLevel, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Inizilaiza el datasource de las cuentas contables para retencion 383 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAccountRetencion383()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {1, True, True}
            Me.View.Account383Xpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByRetentionAndFreelancerCategory, filter)
        End Using
    End Sub
    ''' <summary>
    ''' Inizilaiza el datasource de las cuentas contables para retencion 384
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeAccountRetencion384()
        Using modelAccountsXPO As New MBusqueda
            Dim filter() As Object = {1, True, True}
            Me.View.Account384Xpo = modelAccountsXPO.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListAccountsByRetentionAndFreelancerCategory, filter)
        End Using
    End Sub

    Public Sub InitializeAllRetentions()
        InitializeRetentionConcept()
        InitializeRetentionConceptRangeThree()
        InitializeRetentionConceptRangeFour()
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRetentionConcept()
        Using model As New MBusqueda
            Me.View.RetentionConceptXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConcept)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRetentionConceptRangeThree()
        Using model As New MBusqueda
            Dim filter() As Object = {2, True}
            Me.View.ThreeEightThreeRetentionConceptIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByTypeRetention, filter)
        End Using
    End Sub

    ''' <summary>
    ''' Inicializa el datasource de los conceptos de retencion
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InitializeRetentionConceptRangeFour()
        Using model As New MBusqueda
            Dim filter() As Object = {2, True}
            Me.View.ThreeEightFourRetentionConceptIdXpo = model.ConsultarEntidades(Infrastructure.CrossCutting.Base.eDataSource.ListRetentionConceptByTypeRetention, filter)
        End Using
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

#End Region

End Class
