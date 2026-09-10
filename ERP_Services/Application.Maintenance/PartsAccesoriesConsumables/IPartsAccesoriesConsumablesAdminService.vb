'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IPartsAccesoriesConsumablesAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene todos PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function ListAllPartsAccesoriesConsumables() As List(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' Función que obtiene PartsAccesoriesConsumables por Código
    ''' </summary>
    ''' <param name="Code">Código de PartsAccesoriesConsumables</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function GetPartsAccesoriesConsumablesByCode(Code As String) As PartsAccesoriesConsumables

    ''' <summary>
    ''' Función para Almacenar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SavePartsAccesoriesConsumables(PartsAccesoriesConsumables As PartsAccesoriesConsumables, audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' Función para Eliminar PartsAccesoriesConsumables
    ''' </summary>
    ''' <param name="PartsAccesoriesConsumables">Objeto PartsAccesoriesConsumables</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeletePartsAccesoriesConsumables(PartsAccesoriesConsumables As PartsAccesoriesConsumables, audit As AuditMessage) As Boolean

End Interface
