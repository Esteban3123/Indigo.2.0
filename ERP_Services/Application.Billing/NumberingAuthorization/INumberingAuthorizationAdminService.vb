'***********************************************************************
' Assembly         : Application.Billing
' Author           : Andres Alarcon
' Created          : 2022-08-02
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

Public Interface INumberingAuthorizationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las resoluciones 
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListNumberingAuthorizationByUserCode(userCode As String) As List(Of NumberingAuthorization)


    ''' <summary>
    ''' elimina una autorización
    ''' </summary>
    ''' <param name="NumberingAuthorization">The billing authorization.</param>
    ''' <returns></returns>
    Function DeleteNumberingAuthorization(NumberingAuthorization As NumberingAuthorization, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda una autorización
    ''' </summary>
    ''' <param name="NumberingAuthorization">The billing authorization.</param>
    ''' <returns></returns>
    Function SaveNumberingAuthorization(NumberingAuthorization As NumberingAuthorization, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of NumberingAuthorization)

    ''' <summary>
    ''' Obtiene una autorizacion de documento por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNumberingAuthorizationById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of NumberingAuthorization)

    ''' <summary>
    ''' Obtiene una autorización de documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNumberingAuthorizationByCode(code As String, audit As AuditMessage) As ActionResult(Of NumberingAuthorization)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateNumberingAuthorization(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of NumberingAuthorization)

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetNumberingAuthorizationResolution(operatingUnitId As Integer, NumberingAuthorization As NumberingAuthorization, audit As AuditMessage) As ActionResult(Of Domain.Entities.NumberingAuthorization)

End Interface
