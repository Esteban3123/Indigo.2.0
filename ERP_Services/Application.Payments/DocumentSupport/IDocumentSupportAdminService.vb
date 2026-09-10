'***********************************************************************
' Assembly         : Application.Payments
' Author           : Johan Sebastian Cuellar Esquivel
' Created          : 2021-01-14
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

Public Interface IDocumentSupportAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Lista todas las resoluciones 
    ''' por código de usuario
    ''' </summary>
    ''' <param name="userCode">Código de usuario</param>
    ''' <returns>Lista de resoluciones</returns>
    Function ListDocumentSupportByUserCode(userCode As String) As List(Of DocumentSupport)


    ''' <summary>
    ''' elimina una autorización
    ''' </summary>
    ''' <param name="DocumentSupport">The billing authorization.</param>
    ''' <returns></returns>
    Function DeleteDocumentSupport(DocumentSupport As DocumentSupport, audit As AuditMessage) As ActionResult

    ''' <summary>
    ''' Guarda una autorización
    ''' </summary>
    ''' <param name="DocumentSupport">The billing authorization.</param>
    ''' <returns></returns>
    Function SaveDocumentSupport(DocumentSupport As DocumentSupport, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of DocumentSupport)

    ''' <summary>
    ''' Obtiene una autorizacion de documento por id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentSupportById(ByVal Id As Integer, audit As AuditMessage) As ActionResult(Of DocumentSupport)

    ''' <summary>
    ''' Obtiene una autorización de documento por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentSupportByCode(code As String, audit As AuditMessage) As ActionResult(Of DocumentSupport)

    ''' <summary>
    ''' cambia el estado de la entidad
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeStateDocumentSupport(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of DocumentSupport)

    ''' <summary>
    ''' Obtiene la resolución de facturación emitidad por la DIAN
    ''' </summary>
    ''' <param name="operatingUnitId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDocumentSupportResolution(operatingUnitId As Integer, DocumentSupport As DocumentSupport, audit As AuditMessage) As ActionResult(Of Domain.Entities.DocumentSupport)

End Interface
