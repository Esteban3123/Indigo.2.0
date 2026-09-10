'***********************************************************************
' Assembly         : Application.Maintenance
' Author           : Daniel Eduardo Arévalo Bonilla
' Created          : 15-09-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Base.Entities
Imports Domain.Maintenance.Entities
Imports Infrastructure.CrossCutting.Base
#End Region
Public Interface ITrademarkAdminService
    Inherits IDisposable

    ''' <summary>
    ''' Función que obtiene todas las Marcas par alos Equipos
    ''' </summary>
    ''' <returns>Lista de Marcas</returns>
    ''' <remarks></remarks>
    Function ListAllTrademark() As List(Of Trademark)

    ''' <summary>
    ''' Función que obtiene una marca por Código
    ''' </summary>
    ''' <param name="Code">Código de la Marca</param>
    ''' <returns>Trademark</returns>
    ''' <remarks></remarks>
    Function GetTrademarkByCode(Code As String) As Trademark

    ''' <summary>
    ''' Función para Almacenar una Marca
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function SaveTrademark(Trademark As Trademark, audit As AuditMessage, Optional idSequence As Long = 0) As Domain.Base.Entities.ActionResult(Of Trademark)

    ''' <summary>
    ''' Función para Eliminar las Marcas
    ''' </summary>
    ''' <param name="Trademark">Objeto Trademark</param>
    ''' <param name="audit"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Function DeleteTrademark(Trademark As Trademark, audit As AuditMessage) As Domain.Base.Entities.ActionResult
    Function UpdateStateTrademark(code As String, state As Boolean, audit As AuditMessage) As ActionResult(Of Trademark)
End Interface
