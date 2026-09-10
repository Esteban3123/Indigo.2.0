



'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Oscar Ivan Sierra
' Created          : 07-08-2013
'
' Copyright        : (c) . All rights reserved.
'************************************************************



#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region



''' <summary>
''' clase para definir cada una de la spropiedades y metodos que se van a persistir en la clase del repositorio
''' </summary>
''' <remarks></remarks>
Public Interface ITemplateEquipmentTypeRepository
    Inherits IRepository(Of TemplateEquipmentType)

    ''' <summary>
    ''' funcion que lista todas los plantilla de los tipos de equipos
    ''' </summary>
    ''' <returns>Lista de plnatilla de equipo de equipo</returns>
    Function ListAllTemplateEquipmentType() As List(Of TemplateEquipmentType)


    ''' <summary>
    ''' consulta para retornar una plnatilla de tipo de equipo teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeTemplateEquipmentType">el codigo de la plantilla de equipo de equipo</param>
    ''' <returns>Objeto plantilla de eqipo de equipo</returns>
    Function GetTemplateEquipmentType(ByVal codeTemplateEquipmentType As String, Optional ByVal tracking As Boolean = True) As TemplateEquipmentType

    ''' <summary>
    ''' funcion para almacenar una plnatilla de equipo de equipo
    ''' </summary>
    ''' <param name="TemplateEquipmentType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveTemplateEquipmentType(TemplateEquipmentType As TemplateEquipmentType) As Boolean

    ''' <summary>
    ''' consulta para retornar una plnatilla de tipo de equipo teniendo en cuenta el codigo
    ''' </summary>
    ''' <param name="codeTemplateEquipmentType">el codigo de la plantilla de equipo de equipo</param>
    ''' <returns>Objeto plantilla de eqipo de equipo</returns>
    Function GetTemplateEquipmentTypeByEquipmentTypeId(ByVal IdEquipmentType As Integer) As TemplateEquipmentType
End Interface
