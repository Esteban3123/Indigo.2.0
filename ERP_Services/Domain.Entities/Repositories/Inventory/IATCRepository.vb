'***********************************************************************
' Assembly         : Domain.Inventory
' Author           : Diego Andrés Roldán Lozano
' Created          : 01-12-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Entities

Public Interface IATCRepository
    Inherits IRepository(Of ATC)

    Function GetATCByCreateCrystalProduct(atcId As Integer) As ATC

    ''' <summary>
    ''' Obtiene un atc por codigo
    ''' </summary>
    Function GetATC(ByVal code As String) As ATC

    ''' <summary>
    ''' Obtiene un atc por id
    ''' </summary>
    Function GetATCById(id As Integer) As ATC

    ''' <summary>
    ''' Sp que se encarga de guardar el ATC ahora se llama medicamentos
    ''' </summary>
    ''' <returns></returns>
    Function SP_SaveATC(Xml As String, UserCode As String) As SP_SaveATC_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeleteMedicament(Id As Integer) As SP_DeleteMedicament_Result

End Interface