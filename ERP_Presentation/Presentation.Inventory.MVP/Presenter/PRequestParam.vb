'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Diego A. Roldán L.
' Created          : 2023-03-23
'
' Last Modified By : 
' Last Modified On : 
' Description      : 
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports DevExpress.Xpo
Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository
Imports Infrastructure.Data.Xpo.InventoryRepository.View
Imports Presentation.Base

#End Region

Public Class PRequestParam

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IRequestParam

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
    Public Sub New(ByRef iview As IRequestParam)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        End If

        Me.View = iview
    End Sub

#End Region

#Region "Methods"

    Public Async Sub GetSequense()
        Using model As New MBlockRecordAndSequense(Me.View.MyTag)
            Me.View.Sequence = Await model.GetSequense()
        End Using
    End Sub

    Public Function ListWarehouseUser(CodeWarehouse As String)
        Dim criteria = "CodeWarehouse = " + CodeWarehouse
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewWarehouseUserRequestParamXpo)(Nothing, criteria, False)
    End Function

    Public Function ListFunctionalUnitUser(CodeFunctionalUnit As String)
        Dim criteria = "CodeFunctionalUnit = '" + CodeFunctionalUnit + "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewFunctionalUnitUserRequestParamXpo)(Nothing, criteria, False)
    End Function

    Public Function ListRequestParamAuthUsers(CodeRequestParam As String)
        Dim criteria = "CodeRequestParam = '" + CodeRequestParam + "'"
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ViewRequestParamAuthUserXpo)(Nothing, criteria, False)
    End Function



#End Region

End Class

