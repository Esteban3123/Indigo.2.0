'***********************************************************************
' Assembly         : Domain.Common
' Author           : Cristhian Mauricio Salazar
' Created          : 26-04-2013
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

Imports Domain.Base
Imports Domain.Common.Entities

Public Interface IPhoneTypeRepository
    Inherits IRepository(Of PhoneType)

    ''' <summary>
    ''' Lista todos los tipos de telefonos
    ''' </summary>
    ''' <returns>Lista de tipo de telefono</returns>
    ''' <remarks></remarks>
    Function ListAllPhoneType() As List(Of PhoneType)

    ''' <summary>
    ''' Obtiene un tipo de telefono especifico
    ''' </summary>
    ''' <returns>Tipo de telefono</returns>
    ''' <remarks></remarks>
    Function GetPhoneType(ByVal code As String) As PhoneType

    ''' <summary>
    ''' Obtiene un centro de costo especifico
    ''' </summary>
    ''' <param name="code">Codigo del centro de costo</param>
    ''' <returns>Centro de costo</returns>
    ''' <remarks></remarks>
    Function GetPhonetypeByCode(ByVal code As String, Optional desatach As Boolean = True) As PhoneType

End Interface
