
#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region


''' <summary>
''' interface que especifica las metodos y funciones que manejara todas las acciones sobre consumibles
''' </summary>
''' <remarks></remarks>

Public Interface IConsumableAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todos los consumibles
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllConsumable() As List(Of Consumable)


    ''' <summary>
    ''' funcion que sirve para eliminar un consumible
    ''' </summary>
    ''' <param name="Consumable"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteConsumable(ByVal Consumable As Consumable, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar un consumible
    ''' </summary>
    ''' <param name="Consumable"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveConsumable(ByVal Consumable As Consumable, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Domain.Entities.Consumable)

    ''' <summary>
    ''' funciona que sirve para listar un consumible
    ''' </summary>
    ''' <param name="codeConsumablee"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetConsumable(ByVal codeConsumablee As String) As Consumable

    ''' <summary>
    ''' Cambia el estado del registro
    ''' </summary>
    ''' <param name="code"></param>
    ''' <param name="state"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ChangeState(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Consumable)

End Interface
