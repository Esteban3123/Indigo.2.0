'***********************************************************************
' Assembly         : DistributedServices.Payroll
' Author           : Cristhian Mauricio Salazar
' Created          : 25-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Payroll.Entities
Imports Infrastructure.CrossCutting.IOC
Imports Application.Payroll
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

Partial Class PayrollService

    ''' <summary>
    ''' Elimina un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto Auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function DeleteLanguage(language As Domain.Payroll.Entities.Language, session As SessionValues) As ActionMessageResult(Of Domain.Payroll.Entities.Language) Implements IPayrollLanguage.DeleteLanguage
        Using languageAdmin As ILanguageAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILanguageAdminService)()
            Return languageAdmin.DeleteLanguage(language, session.AuditMessageWcf)
        End Using
    End Function

    ''' <summary>
    ''' Obtiene un idioma especifico
    ''' </summary>
    ''' <param name="code">Codigo del idioma</param>
    ''' <returns>Idioma</returns>
    ''' <remarks></remarks>
    Public Function GetLanguage(code As String, session As SessionValues) As Domain.Payroll.Entities.Language Implements IPayrollLanguage.GetLanguage
        Using languageAdmin As ILanguageAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILanguageAdminService)()
            Return languageAdmin.GetLanguage(code)
        End Using
    End Function

    ''' <summary>
    ''' Lista todos los idiomas
    ''' </summary>
    ''' <returns>Lista de idiomas</returns>
    ''' <remarks></remarks>
    Public Function ListAllLanguage(session As SessionValues) As List(Of Domain.Payroll.Entities.Language) Implements IPayrollLanguage.ListAllLanguage
        Using languageAdmin As ILanguageAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILanguageAdminService)()
            Return languageAdmin.ListAllLanguage()
        End Using
    End Function

    ''' <summary>
    ''' Graba o Actualiza un idioma
    ''' </summary>
    ''' <param name="language">Idioma</param>
    ''' <param name="audit">Objeto auditoria</param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function SaveLanguage(language As Domain.Payroll.Entities.Language, session As SessionValues, idSequence As Long) As ActionResult(Of Language) Implements IPayrollLanguage.SaveLanguage
        Using languageAdmin As ILanguageAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILanguageAdminService)()
            Return languageAdmin.SaveLanguage(language, session.AuditMessageWcf, idSequence)
        End Using
    End Function

    Public Function UpdateStateLanguage(code As String, state As Boolean, session As SessionValues) As ActionResult(Of Language) Implements IPayrollLanguage.UpdateStateLanguage
        Using languageAdmin As ILanguageAdminService = IocFactory.Instance(session.TransactionalContainer).CurrentContainer.Resolve(Of ILanguageAdminService)()
            Return languageAdmin.UpdateStateLanguage(code, state, session.AuditMessageWcf)
        End Using
    End Function

End Class
