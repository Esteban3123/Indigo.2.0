'***********************************************************************
' Assembly         : Application.Budget
' Author           : Carlos Ernesto Cordoba
' Created          : 19-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IAnnualizedCashFlowModificationDetailAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene le detalle de la modificacion por id de la cabecera
    ''' </summary>
    ''' <param name="pacModificationId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAnnualizedCashFlowModificationDetailByPACModificationId(pacModificationId As Integer) As List(Of AnnualizedCashFlowModificationDetail)
End Interface
