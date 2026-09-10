'***********************************************************************
' Assembly         : Application.FixedAsset
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 04-02-2016
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
#End Region

Public Interface IFixedAssetResponsibleAdminService
    Inherits IDisposable
    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Responsible</returns>
    ''' <remarks></remarks>
    Function ListAllResponsible() As List(Of FixedAssetResponsible)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código del Responsible</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetResponsibleByCode(Code As String) As FixedAssetResponsible

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Responsible">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveResponsible(Responsible As FixedAssetResponsible, audit As AuditMessage, Optional idSequense As Long = 0) As ActionResult(Of FixedAssetResponsible)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Responsible">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteResponsible(Responsible As FixedAssetResponsible, audit As AuditMessage) As ActionResult

    '' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Responsible">Objeto Responsible</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function GetFunctionalUnitByCode(Code As String) As FunctionalUnit

    Function GetFuncionalUnitByIdUser(IdResponsible As Integer) As List(Of ResponsibleFunctionalUnit)

    Function ChangeStateFixedAssetResponsible(code As String, state As Boolean, audit As Infrastructure.CrossCutting.Base.AuditMessage) As Domain.Base.Entities.ActionResult(Of Domain.Entities.FixedAssetResponsible)
End Interface
