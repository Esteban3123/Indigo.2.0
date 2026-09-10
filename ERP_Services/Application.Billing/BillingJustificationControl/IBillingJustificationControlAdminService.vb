'***********************************************************************
' Assembly         : Application.Billing
' Author           : Cristian Camilo Bahamon
' Created          : 2022-07-29
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Domain.Base
Imports Infrastructure.CrossCutting.Exceptions
Imports Application.Base
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


Public Interface IBillingJustificationControlAdminService
    Inherits IDisposable


    ''' <summary>
    ''' Lista todos los paises.
    ''' </summary>
    ''' <returns></returns>
    Function ListAllJustificationControl() As List(Of BillingJustificationControl)

    ''' <summary>
    ''' Elimina una justificacion control de cuentas hospitalaria
    ''' </summary>
    ''' <param name="justificationControl">el pais</param>
    ''' <returns></returns>
    Function DeleteJustificationControl(ByVal justificationControl As BillingJustificationControl, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' graba una justificacion
    ''' </summary>
    ''' <param name="justificationControl">justificacion control</param>
    ''' <returns></returns>
    Function SaveJustificationControl(ByVal justificationControl As BillingJustificationControl, ByVal audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of BillingJustificationControl)
    ''' <summary>
    ''' consulta un Pais
    ''' </summary>
    ''' <param name="code">el codigo del pais</param>
    ''' <returns></returns>
    Function GetBillingJustification(ByVal code As String, ByVal audit As AuditMessage) As ActionResult(Of BillingJustificationControl)

    ''' <summary>
    ''' Devuelve un país por ID
    ''' </summary>
    ''' <param name="idCountry">Id del pais</param>
    ''' <returns>El pais</returns>
    ''' <remarks></remarks>
    Function GetBillingJustificationById(ByVal id As Integer) As ActionResult(Of BillingJustificationControl)
End Interface
