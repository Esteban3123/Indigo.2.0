
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la area
''' </summary>
''' <remarks></remarks>
Public Interface IEquipmentRegistrationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para lñistar todos los registros de equipo
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllEquipmentRegistration() As List(Of EquipmentRegistration)


    ''' <summary>
    ''' funcion que sirve para eliminar una registro de equipo
    ''' </summary>
    ''' <param name="EquipmentRegistration"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteEquipmentRegistration(ByVal EquipmentRegistration As EquipmentRegistration, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' Funcion para registra un equipo
    ''' </summary>
    ''' <param name="EquipmentRegistration">oobj equipo</param>
    ''' <param name="idSequense">id de la secuencia numerica que maneja el frontal de equipos</param>
    ''' <param name="audit">auditoria</param>
    ''' <returns>actionresult</returns>
    ''' <remarks></remarks>
    Function SaveEquipmentRegistration(EquipmentRegistration As EquipmentRegistration, idSequense As Long, audit As AuditMessage) As ActionResult(Of EquipmentRegistration)

    ''' <summary>
    ''' funciona que sirve para listar un registro de equipo
    ''' </summary>
    ''' <param name="codeEquipmentRegistration"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetEquipmentRegistration(ByVal codeEquipmentRegistration As String) As EquipmentRegistration

    ''' <summary>
    ''' funcion paranlistar los accesorios de un equipo 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAccesoryEquipmentType(Type As Integer) As List(Of AccesoryDetail)


    ''' <summary>
    ''' funcion paranlistar los consumibles de un equipo 
    ''' </summary>
    ''' <param name="Type"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListConsumableEquipmentType(Type As Integer) As List(Of ConsumableDetail)

    Function ListTechnicalLogDetailByFixedAssetPhysicalIdAndEquipmentRegistration(fixedAssetPhysicalAssetId As Integer, equipmentRegistration As Integer) As List(Of TechnicalLogDetail)
End Interface
