'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************
#Region "Imports"

Imports Domain.Base
Imports Domain.Entities
Imports Domain.Base.Entities

#End Region

Public Interface IObligationDetailRepository
    Inherits IRepository(Of ObligationDetail)
    ''' <summary>
    ''' obtiene le detalle de la obligacion por id de la cabecera
    ''' </summary>
    ''' <param name="ObligationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationDetailByObligationId(ObligationId As Integer) As List(Of ObligationDetail)
    ''' <summary>
    ''' obtiene un detalle por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationDetailById(id As Integer) As ObligationDetail
End Interface
