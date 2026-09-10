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
Imports Infrastructure.Data.Xpo.ContractRepository

#End Region

Public Class PDefinitionRate

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Private View As IDefinitionRate

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
    Public Sub New(ByRef iview As IDefinitionRate)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

    ''' <summary>
    ''' Constructor
    ''' </summary>
    ''' <exception cref="System.ArgumentException"></exception>
    Public Sub New()

    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequense = Await model.GetSequense()
        End Using
    End Sub

    ''' <summary>
    ''' Carga los detalles de la definición de tarifas
    ''' </summary>
    ''' <param name="definitionRateId"></param>
    ''' <returns></returns>
    Public Async Function ListViewListDefinitionRateDetailAsync(definitionRateId As Integer) As Task(Of List(Of ViewListDefinitionRateDetailXpo))
        Dim filter As String = "DefinitionRateId = " & definitionRateId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListDefinitionRateDetailXpo)(Nothing, filter).ToList())
    End Function

    ''' <summary>
    ''' Carga los procedimientos al detalle de la definición de tarifas
    ''' </summary>
    ''' <param name="definitionRateDetailId"></param>
    ''' <returns></returns>
    Public Async Function ListViewListDefinitionRateDetailSurgicalProceduresAsync(definitionRateDetailId As Integer) As Task(Of List(Of ViewListDefinitionRateDetailSurgicalProceduresXpo))
        Dim filter As String = "DefinitionRateDetailId = " & definitionRateDetailId
        Return Await Task.Run(Function() XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.GetCollection(Of ViewListDefinitionRateDetailSurgicalProceduresXpo)(Nothing, filter))
    End Function

    ''' <summary>
    ''' Lista las definiciones de tarifa para la rejilla de importar información
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ListDefinitionRateXpCollection() As DevExpress.Xpo.XPCollection
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).ContractService.ListDefinitionRateXpCollection()
    End Function

#End Region

End Class
