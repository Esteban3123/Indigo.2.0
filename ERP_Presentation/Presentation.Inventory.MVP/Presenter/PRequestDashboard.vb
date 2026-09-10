'***********************************************************************
' Assembly         : Presentacion.Inventory.MVP
' Author           : Andres Alarcon
' Created          : 03/11/2023
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Infrastructure.CrossCutting.Base
Imports Infrastructure.Data.Xpo
Imports Infrastructure.Data.Xpo.InventoryRepository

#End Region

''' <summary>
''' Este presentador captura toda la logica aplicada en el frontal 
''' </summary>
Public Class PRequestDashboard

#Region "Variables"

    ''' <summary>
    ''' Se utiliza para instanciar la clase singleton
    ''' </summary>
    Public _sessionValues As SessionValues

#End Region

#Region "Builder"

    Public Sub New()
        Me._sessionValues = SessionValues.Instance
    End Sub

#End Region

#Region "Methods"

    ''' <summary>
    ''' Obtiene el historial de los detalles de la solicitud utilizados en una orden de traslado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewRequestDetailAuthorizedXpo(Type As Integer, ByVal RequestType As String, OperatingUnitIds As String, Optional InventoryRequestDetailOtherId As Integer = 0) As List(Of ViewListRequestDetailAuthorizedXpo)
        Dim filter = String.Format("RequestType IN (" + RequestType + ") AND OperatingUnitId IN(" + OperatingUnitIds + ")")
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollection(Of ViewListRequestDetailAuthorizedXpo)(Nothing, filter).ToList()
    End Function

    ''' <summary>
    ''' Obtiene el subdetalle del historial de los detalles de la solicitud utilizados en una orden de traslado
    ''' </summary>
    ''' <returns></returns>
    Public Function ListViewRequestDetailOtherDetailAuthorizedXpo() As List(Of ViewListRequestDetailAuthorizedXpo)
        Dim filter = String.Format("RequestType IS NULL")
        Return XpoServiceEx.Instance(_sessionValues.TransactionalContainer).InventoryService.GetCollectionAsList(Of ViewListRequestDetailAuthorizedXpo)(Nothing, filter)
    End Function

#End Region

End Class
