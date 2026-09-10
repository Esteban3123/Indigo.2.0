'***********************************************************************
' Assembly         : Presentacion.Payments
' Author           : Juan Carlos Bermudez
' Created          : 25/06/2015
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

Public Class PClosedMonthInventory

#Region "Variables"

    ''' <summary>
    ''' Variable que se usa para instanciar la interfaz
    ''' </summary>
    Dim View As IClosedMonthInventory

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
    Public Sub New(ByRef iview As IClosedMonthInventory)
        If iview Is Nothing Then
            Throw New ArgumentException(BaseClass.obtenerExcepcion(EexceptionsResources.MensajeConstructorPresentador))
        Else
            Me.View = iview
        End If
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Retorna objeto con los datos de conciliacion de movimientos
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    Public Function ListViewConciliationMovements(Year As Integer, Month As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of ViewClosedMonthConciliationMovementsXpo)($"Year = {Year} AND Month = {Month}", Nothing, "EntityName")
    End Function

    ''' <summary>
    ''' Retorna objeto con los datos de inventario valorizado
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    Public Function ListViewValuedInventory(Year As Integer, Month As Integer) As XPInstantFeedbackSource
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.ListXPInstantFeedbackSource(Of ViewClosedMonthValuedInventoryXpo)($"Year = {Year} AND Month = {Month}", Nothing, "CodeNameWarehouse")
    End Function


    ''' <summary>
    ''' Valida si ya se hizo un cierre con el periodo enviado
    ''' </summary>
    ''' <param name="Year"></param>
    ''' <param name="Month"></param>
    ''' <returns></returns>
    Public Function GetClosedMonth(Year As Integer, Month As Integer)
        Return XpoServiceEx.Instance(Indigo.TransactionalContainer).InventoryService.GetCollection(Of ClosedMonthModulesConciliationXpo)(Nothing, $"Year = {Year} AND Month = {Month}").ToList()
    End Function

#End Region

End Class
