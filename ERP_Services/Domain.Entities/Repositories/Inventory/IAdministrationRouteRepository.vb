'************************************************************
' Assembly         : Domain.Inventory.IGroupRepository
' Author           : Carlos Mario Arias Rubiano
' Created          : 12/09/2014
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Domain.Base
#End Region


Public Interface IAdministrationRouteRepository
    Inherits IRepository(Of AdministrationRoute)

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <param name="code"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdministrationRoute(code As String) As AdministrationRoute

    ''' <summary>
    ''' Obtiene un concepto de pago
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetAdministrationRouteById(id As Integer) As AdministrationRoute

    ''' <summary>
    ''' Función para Guardar o Actualizar las Vías de Administración
    ''' </summary>
    ''' <param name="PharmaceuticalFormId"></param>
    ''' <param name="Code"></param>
    ''' <param name="Description"></param>
    ''' <param name="Status"></param>
    ''' <param name="CodeUser"></param>
    ''' <returns></returns>
    Function SaveAdministrationRoute(PharmaceuticalFormId As Integer?, Code As String, Description As String, Status As Boolean, CodeUser As String) As SP_SaveAdministrationRoute_Result

    ''' <summary>
    ''' Elimina por Id
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <returns></returns>
    Function SP_DeleteAdministrationRoute(Id As Integer) As SP_DeleteAdministrationRoute_Result

End Interface
