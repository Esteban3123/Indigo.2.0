'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Jeisson Herrera Peña
' Created          : 24/09/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"

Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities

#End Region

Public Interface IFixedAssetLocationAdminService
    Inherits IDisposable

    ''' <summary>
    ''' funcion que sirve para listar todos las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListAllLocation() As List(Of FixedAssetLocation)


    ''' <summary>
    ''' funcion que sirve para eliminar una ubicacion
    ''' </summary>
    ''' <param name="Location"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function DeleteLocation(ByVal Location As FixedAssetLocation, ByVal audit As AuditMessage) As Boolean


    ''' <summary>
    ''' funcion que sirve para guardar una ubicacion
    ''' </summary>
    ''' <param name="Location"></param>
    ''' <param name="audit"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function SaveLocation(ByVal Location As List(Of FixedAssetLocation), ByVal audit As AuditMessage) As Boolean

    ''' <summary>
    ''' funciona que sirve para listar una ubicacion
    ''' </summary>
    ''' <param name="codeLocation"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetLocation(ByVal codeLocation As String) As FixedAssetLocation

    ''' <summary>
    ''' funcion para almacenar todos las ubicaciones
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ListLocation() As List(Of FixedAssetLocation)

End Interface
