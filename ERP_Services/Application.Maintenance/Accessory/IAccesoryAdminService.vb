
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre la entidad sucursal
''' </summary>
''' <remarks></remarks>
Public Interface IAccesoryAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todos loas accesorios
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllAccessory() As List(Of Accessory)


    ''' <summary>
    ''' funcion que sirve para eliminar un accesorio
    ''' </summary>
    ''' <param name="Accessory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteAccessory(ByVal Accessory As Accessory, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar un accesorio
    ''' </summary>
    ''' <param name="Accessory"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveAccessory(ByVal Accessory As Accessory, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Accessory)

    ''' <summary>
    ''' funciona que sirve para listar un accesorio
    ''' </summary>
    ''' <param name="codeAccessory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAccessory(ByVal codeAccessory As String) As Accessory

    ''' <summary>
    ''' metodo para cambiar el estado de la entidad
    ''' </summary>
    ''' <param name="code">The code.</param>
    ''' <param name="state">if set to <c>true</c> [state].</param>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Accessory)
    Function ListEquipmentType() As List(Of FixedAssetEquipmentType)

End Interface
