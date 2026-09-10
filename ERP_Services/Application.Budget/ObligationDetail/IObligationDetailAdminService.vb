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

Public Interface IObligationDetailAdminService
    Inherits IDisposable
    ''' <summary>
    ''' obtiene los detalle de la obligacion por id de la cabecera
    ''' </summary>
    ''' <param name="ObligationId "></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetObligationDetailByObligationId(ObligationId As Integer) As List(Of ObligationDetail)
End Interface
