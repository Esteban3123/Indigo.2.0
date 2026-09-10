'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Carlos Mario Arias Rubiano
' Created          : 26/08/2015
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
Imports Infrastructure.Data.Xpo
Imports DevExpress.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PAddRule

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Private View As IAddRule

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Private Indigo As SessionValues = SessionValues.Instance

#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IAddRule)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Inicializa el datasource de la rejilla de tipos de regla
    ''' dependiendo del tipo que escojan
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function InitializeDataSourceGridControlsRulesType(type As Integer) As Task(Of XPCollection)
        Select Case type
            Case 1 'IPSService
                Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListViewListIPSServiceWithHomologations(True))
            Case 2 'CUPS
                Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatusXpCollection(True))
            Case 3 'CupsSubGroup
                Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsSubGroupByStatusXpCollection(True))
            Case 4 'CupsGroup
                Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsGroupByStatusXpCollection(True))
            Case Else
                Return Nothing
        End Select
    End Function

    ''' <summary>
    ''' Lista las especialidades
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListSpecialties() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListSpecialties(True)
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListFunctionalUnit() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnit(True)
    End Function

    ''' <summary>
    ''' Lista los RIAS
    ''' </summary>
    Public Function ListRIAS() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListRIAS(1)
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListRateManualValidity() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListRateManualValidityByStatus(True)
    End Function

    ''' <summary>
    ''' Lista los manuales tarifarios
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ListRateManual() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListRateManualByStatus(True)
    End Function

    ''' <summary>
    ''' Lista las especialidades con xpCollection
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ListSpecialtyXpCollection() As Task
        View.SpecialtyPopupFirstConditionXpo = Await Task.Run(Function() XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListSpecialtyXpCollection(True))
    End Function

    ''' <summary>
    ''' Lista las unidades funcionales con xpCollection
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ListFunctionalUnitXpCollection() As Task
        View.FunctionalUnitPopupFirstConditionXpo = Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).PayrollService.ListFunctionalUnitXpCollection(True))
    End Function

    ''' <summary>
    ''' Lista las RIAS
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ListRIASXpCollection() As Task
        View.RIASPopupFirstContidionXpo = Await Task.Run(Function() XpoServiceEx.Instance(Indigo.HisContainer).CrystalService.ListRIASXpCollection(1))
    End Function

    ''' <summary>
    ''' Lista las descripciones
    ''' </summary>
    ''' <remarks></remarks>
    Public Async Function ListDescriptionsXpCollection() As Task
        View.DescriptionPopupFirstConditionXpo = Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractDescriptionsByStatusXpCollection(True))
    End Function

    ''' <summary>
    ''' Lista las descripciones
    ''' </summary>
    ''' <returns></returns>
    Public Function ListDescriptions() As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListContractDescriptionsByStatus(True)
    End Function

    ''' <summary>
    ''' Lista los procedimientos Qx que estan asociados al servicio ips
    ''' </summary>
    ''' <returns></returns>
    Public Async Function ListSurgicalProcedures(ipsServiceId As Integer) As Task(Of List(Of SurgicalProcedureServiceXpo))
        Dim filter As String = "IPSServiceParentId = " & ipsServiceId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of SurgicalProcedureServiceXpo)(Nothing, filter).ToList())
    End Function

    ''' <summary>
    ''' Obtiene un servicio IPS
    ''' </summary>
    ''' <param name="ServiceIPSId"></param>
    ''' <returns></returns>
    Public Async Function GetServiceIPS(ServiceIPSId As Integer) As Task(Of ContractIPSServiceXPO)
        Dim filter As String = $"Id={ServiceIPSId}"
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ContractIPSServiceXPO)(Nothing, filter).FirstOrDefault())
    End Function

#End Region

End Class
