'************************************************************
' Assembly         : Domain.Billing
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 2022-07-29
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"

Imports Domain.Base
Imports Domain.Entities

#End Region


Public Interface IBillingJustificationControlRepository
    Inherits IRepository(Of BillingJustificationControl)

    ''' <summary>
    ''' Lista todas las Justificaciones control
    ''' </summary>
    ''' <returns>Lista de Justificaciones control</returns>
    Function ListAllJustificationControl() As List(Of BillingJustificationControl)

    ''' <summary>
    ''' Obtiene una Justificacion control por id
    ''' </summary>
    ''' <param name="Id">The identifier.</param>
    ''' <returns></returns>
    Function GetJustificationControlById(ByVal Id As Integer, Optional tracking As Boolean = True) As BillingJustificationControl

    ''' <summary>
    ''' Obtiene una Justificacion control por codigo
    ''' </summary>
    ''' <param name="code">The identifier.</param>
    ''' <returns></returns>
    Function GetJustificationControl(ByVal code As String) As BillingJustificationControl
End Interface
