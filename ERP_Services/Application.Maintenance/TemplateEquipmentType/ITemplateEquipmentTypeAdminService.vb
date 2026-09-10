
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre registro tecnico
''' </summary>
''' <remarks></remarks>
Public Interface ITemplateEquipmentTypeAdminService
    Inherits IDisposable
    ''' <summary>
    ''' funcion que sirve para listar todas los registros de plantillas de tipos de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllTemplateEquipmentType() As List(Of TemplateEquipmentType)
    ''' <summary>
    ''' funcion que sirve para eliminar una plantilla de tipo de equipo
    ''' </summary>
    ''' <param name="TemplateEquipmentType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteTemplateEquipmentType(ByVal TemplateEquipmentType As TemplateEquipmentType, ByVal audit As AuditMessage) As ActionResult
    ''' <summary>
    ''' funcion que sirve para guardar una plantilla de tipo de equipo
    ''' </summary>
    ''' <param name="TemplateEquipmentType"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveTemplateEquipmentType(ByVal TemplateEquipmentType As TemplateEquipmentType, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of TemplateEquipmentType)
    ''' <summary>
    ''' funciona que sirve para listar una plantilla de eqipo de equipo
    ''' </summary>
    ''' <param name="CodeTemplateEquipmentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTemplateEquipmentType(ByVal CodeTemplateEquipmentType As String, audit As AuditMessage) As TemplateEquipmentType
    ''' <summary>
    ''' Metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <returns></returns>
    Function ChangeStateTemplate(ByVal code As String, ByVal state As Boolean, ByVal audit As AuditMessage) As ActionResult(Of TemplateEquipmentType)

    Function GetTemplateEquipmentTypeByEquipmentTypeId(IdEquipmentType As Integer) As TemplateEquipmentType
End Interface
