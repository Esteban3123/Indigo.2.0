#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface ISettingsExogenousInformationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para Listar los Parámetros de la Información Exógena
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSettingsExogenousInformation() As List(Of SettingsExogenousInformation)


    ''' <summary>
    ''' funcion que sirve para guardar los Parámetros de la Información Exógena
    ''' </summary>
    ''' <param name="ListSettingsExogenousInformation">Lista de Parámetros de Información Exógena</param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveSettingsExogenousInformation(ByVal ListSettingsExogenousInformation As List(Of SettingsExogenousInformation), ByVal audit As AuditMessage) As ActionResult(Of List(Of SettingsExogenousInformation))

End Interface
