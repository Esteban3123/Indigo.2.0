'************************************************************
' Assembly         : Domain.Authorization
' Author           : Cristian Camilo Bahamon Castaño
' Created          : 20/09/2022
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region

Public Interface IDefectClassificationItemRepository
    Inherits IRepository(Of DefectClassificationItem)
    ''' <summary>
    ''' Obtiene todos los defectos
    ''' </summary>
    ''' <returns>Lista de los defectos</returns>
    ''' <remarks></remarks>
    Function ListAllDefectClassificationItem() As List(Of DefectClassificationItem)

    ''' <summary>
    ''' obtiene un defecto por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefectClassificationItemByCode(code As String, Optional tracking As Boolean = True) As DefectClassificationItem

    ''' <summary>
    ''' obtiene un defecto por id
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetDefectClassificationItemById(id As Integer, Optional tracking As Boolean = True) As DefectClassificationItem
End Interface
