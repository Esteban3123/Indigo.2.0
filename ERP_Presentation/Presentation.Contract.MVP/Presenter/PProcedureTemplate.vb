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
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.ContractRepository
Imports Presentation.Base

#End Region

Public Class PProcedureTemplate

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IProcedureTemplate

    ''' <summary>
    ''' Variable que se usa para instanciar la clase singleton
    ''' </summary>
    Dim Indigo As SessionValues = SessionValues.Instance

    ''' <summary>
    ''' Task
    ''' </summary>
    Dim taskCups As Task(Of List(Of CupsEntityXpo))
#End Region

#Region "Builder"

    ''' <summary>
    ''' Inicializa un nuevo constructor para permitir la comunicacion con la interfaz
    ''' </summary>
    ''' <param name="iview">Iview</param>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New(ByRef iview As IProcedureTemplate)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If

        taskCups = Task.Factory.StartNew(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetXPCollectionEntity(Of CupsEntityXpo)("Status = True")?.ToList())
    End Sub

    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    Public Async Function InitializeCUPSEntity() As Task
        View.AsyncLoader(True)
        View.CupsEntityXPO = Await taskCups
        View.AsyncLoader(False)
    End Function

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
