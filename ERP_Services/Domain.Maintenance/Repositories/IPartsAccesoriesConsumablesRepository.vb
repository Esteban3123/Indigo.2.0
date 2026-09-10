'************************************************************
' Assembly         : Domain.Maintenance
' Author           : Daniel Eduardo Arévalo
' Created          : 26-08-2015
'
' Copyright        : (c) . All rights reserved.
'************************************************************

#Region "Importar"
Imports Domain.Base.Entities
Imports Domain.Base
Imports Domain.Maintenance.Entities
#End Region

Public Interface IPartsAccesoriesConsumablesRepository

    Inherits IRepository(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' Función que obtiene todas las PartsAccesoriesConsumables
    ''' </summary>
    ''' <returns>Lista de PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function ListAllPartsAccesoriesConsumables() As List(Of PartsAccesoriesConsumables)

    ''' <summary>
    ''' Función que obtiene una Parte, Accesorio por Código
    ''' </summary>
    ''' <param name="Code">Código</param>
    ''' <returns>PartsAccesoriesConsumables</returns>
    ''' <remarks></remarks>
    Function GetPartsAccesoriesConsumablesByCode(Code As String, Optional desatach As Boolean = True) As PartsAccesoriesConsumables

End Interface
