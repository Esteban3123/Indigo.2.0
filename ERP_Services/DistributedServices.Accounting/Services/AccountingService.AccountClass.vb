#Region "Imports"
Imports Application.Accounting
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
Imports Domain.Entities
Imports System.ServiceModel
Imports Microsoft.Practices.Unity
#End Region

Partial Class AccountingService

#Region "Methods"

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="code">Código del documento a consultar</param>
    ''' <returns>
    ''' Tipo de documento consultado
    ''' </returns>
    Public Function GetAccountClassByCode(code As String, Optional tracking As Boolean = True) As Domain.Entities.MainAccountClasses Implements IAccountingAccountClass.GetAccountClassByCode
        Using service As IAccountClassAdminService = Container.Current.Resolve(Of IAccountClassAdminService)()
            Return service.GetAccountClassByCode(code, tracking)
        End Using
        'Return Me._accountClassAdminService.GetAccountClassByCode(code, tracking)
    End Function

    ''' <summary>
    ''' Obtiene un tipo de documento por su código
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns>
    ''' Tipo de documento consultado
    ''' </returns>
    Public Function GetAccountClassById(id As Integer, Optional tracking As Boolean = True) As Domain.Entities.MainAccountClasses Implements IAccountingAccountClass.GetAccountClassById
        Using service As IAccountClassAdminService = Container.Current.Resolve(Of IAccountClassAdminService)()
            Return service.GetAccountClassById(id, tracking)
        End Using
        'Return Me._accountClassAdminService.GetAccountClassById(id, tracking)
    End Function

    ''' <summary>
    ''' Graba un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a grabar</param>
    ''' <returns>Resultado de la acción</returns>
    Public Function SaveAccountClass(doc As Domain.Entities.MainAccountClasses) As Domain.Base.Entities.ActionResult(Of Domain.Entities.MainAccountClasses) Implements IAccountingAccountClass.SaveAccountClass
        Using service As IAccountClassAdminService = Container.Current.Resolve(Of IAccountClassAdminService)()
            Dim idSequence As Long = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of Long)(ConfigurationFile.SESS_IDSEQUENSE, ConfigurationFile.SESS_NAME_SPACE)
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.SaveAccountClass(doc, audit, idSequence)
        End Using
        'Return Me._accountClassAdminService.SaveAccountClass(doc, audit, idSequence)
    End Function

    ''' <summary>
    ''' Elimina un tipo de documento
    ''' </summary>
    ''' <param name="doc">Tipo de documento a eliminar</param>
    ''' <returns>
    ''' Resultado de la acción
    ''' </returns>
    Public Function DeleteAccountClass(doc As Domain.Entities.MainAccountClasses) As ActionResult Implements IAccountingAccountClass.DeleteAccountClass
        Using service As IAccountClassAdminService = Container.Current.Resolve(Of IAccountClassAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.DeleteAccountClass(doc, audit)
        End Using
        'Return Me._accountClassAdminService.DeleteAccountClass(doc, audit)
    End Function

    ''' <summary>
    ''' Gets all acount class.
    ''' </summary>
    ''' <returns></returns>
    Public Function GetAllAcountClass() As List(Of Domain.Entities.MainAccountClasses) Implements IAccountingAccountClass.GetAllAcountClass
        Using service As IAccountClassAdminService = Container.Current.Resolve(Of IAccountClassAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.GetAllAccountClass()
        End Using
        'Return Me._accountClassAdminService.GetAllAccountClass()
    End Function

    ''' <summary>
    ''' Updates the state card.
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Public Function UpdateStateAccountClass(code As String, state As Boolean) As ActionResult(Of MainAccountClasses) Implements IAccountingAccountClass.UpdateStateAccountClass
        Using service As IAccountClassAdminService = Container.Current.Resolve(Of IAccountClassAdminService)()
            Dim audit As AuditMessage = OperationContext.Current.IncomingMessageHeaders.GetHeader(Of AuditMessage)(ConfigurationFile.SESS_AUDITMESSAGE, ConfigurationFile.SESS_NAME_SPACE)
            Return service.UpdateStateAccountClass(code, state, audit)
        End Using
        'Return Me._accountClassAdminService.UpdateStateAccountClass(code, state, audit)
    End Function

#End Region

End Class
