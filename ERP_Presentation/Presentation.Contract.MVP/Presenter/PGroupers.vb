'***********************************************************************
' Assembly         : Presentacion.Contract.MVP
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 11/07/2017
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
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base

#End Region

Public Class PGroupers

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IGroupers

    ''' <summary>
    ''' Variable que se usa para tratar la corporacion como un objeto
    ''' </summary>
    Dim Corporation As Object

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
    Public Sub New(ByRef iview As IGroupers)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Loads the definition layout.
    ''' </summary>
    Public Async Sub LoadDefinitionLayout()
        Await Me.View.MyLayoutControl.LoadDefinitionAsync()
    End Sub

    Public Sub InitializeGroupers()
        View.GroupersXpo = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListGroupersByStatus(True)
    End Sub

    Public Sub InitializeCUPSEntity()
        View.CupsEntityXPO = XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListCupsEntityByStatus(True)
    End Sub

    Public Sub InitializeActivities()
        View.ActivitiesXpo = XpoServiceEx.Instance(Me.Indigo.HisContainer).CrystalService.GetAllAGACTIMED()
    End Sub

    Public Function ListCupsEntityWithDescriptions(listCupsIds As List(Of Integer)) As List(Of ViewListCupsEntityWithDescriptionsXpo)
        Dim stringIds = String.Join(",", listCupsIds.ToArray())
        Dim filter As String = "CUPSEntityId in (" & stringIds & ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListCupsEntityWithDescriptionsXpo)(Nothing, filter).ToList()
    End Function

    Public Function CupsWithDescriptionsId(stringCupsIds As String) As List(Of Integer)
        Dim filter As String = "CUPSEntityId in (" & stringCupsIds & ")"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListCupsEntityWithDescriptionsXpo)(Nothing, filter).Select(Function(l) l.CUPSEntityId).Distinct().ToList
    End Function

#End Region

End Class
