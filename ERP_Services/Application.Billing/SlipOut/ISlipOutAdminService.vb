'***********************************************************************
' Assembly         : Application.Billing
' Author           : Carlos Ernesto Cordoba
' Created          : 13-11-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Public Interface ISlipOutAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Obtiene una boleta de salida por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSlipOutByCode(code As String, ByVal audit As AuditMessage) As SlipOut

    ''' <summary>
    ''' Obtiene una boleta de salida por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSlipOutById(Id As Integer) As SlipOut

    ''' <summary>
    ''' guarda una boleta de salida
    ''' </summary>
    ''' <param name="SlipOut"></param>
    ''' <param name="audit"></param>
    ''' <param name="idSequence"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSlipOut(SlipOut As SlipOut, ByVal audit As AuditMessage, Optional ByVal idSequence As Int64 = 0) As ActionResult(Of SlipOut)

    ''' <summary>
    ''' Obtiene una boleta de salida por numero de admisión
    ''' </summary>
    ''' <param name="numberAdmission"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSlipOutByNumberAdmission(numberAdmission As String, ByVal audit As AuditMessage) As SlipOut

End Interface
