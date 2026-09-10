'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 16/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IPharmacologicalGroupRepository
    Inherits IRepository(Of PharmacologicalGroup)

    ''' <summary>
    ''' Obtiene un grupo farmacologico por codigo
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmacologicalGroup(code As String) As PharmacologicalGroup

    ''' <summary>
    ''' Obtiene un grupo farmacologico por id
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPharmacologicalGroupById(id As Integer) As PharmacologicalGroup

    ''' <summary>
    ''' Función para Almacenar o actualizar el Grupo Farmacológico
    ''' </summary>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Status"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SavePharmacologicalGroup(Code As String, Description As String, Status As Boolean, CodeUser As String) As SP_SavePharmacologicalGroup_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeletePharmacologicalGroup(Id As Integer) As SP_DeletePharmacologicalGroup_Result

End Interface
