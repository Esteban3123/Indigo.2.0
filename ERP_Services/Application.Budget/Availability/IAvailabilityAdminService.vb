'***********************************************************************
' Assembly         : Application.Budget
' Author           : Jeisson Herrera Peña
' Created          : 25/08/2015
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

#Region "Imports"
Imports Domain.Entities
Imports Infrastructure.CrossCutting.Base
Imports Domain.Base.Entities
#End Region

Public Interface IAvailabilityAdminService
    Inherits IDisposable

#Region "Methods"

    ''' <summary>
    ''' Obtiene una disponibilidad por codigo
    ''' </summary>
    '''<param name="Code">Código de la disponibilidad</param>
    ''' <param name="ItemType">TIPO DE RUBRO (NINGUNO = 0,INGRESO = 1,GASTO = 2)</param>
    ''' <returns></returns>
    Function GetAvailability(Code As String, ItemType As Byte, BudgetaryValidityId As Integer, audit As AuditMessage) As ActionResult(Of Availability)

    ''' <summary>
    ''' Obtiene una disponibilidad por id
    ''' </summary>
    '''<param name="Id">Id de la disponibilidad</param>
    ''' <returns></returns>
    Function GetAvailabilityById(Id As Integer) As Availability

    ''' <summary>
    ''' Guarda o Actualiza una disponibilidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function SaveAvailability(ByVal availability As Availability, ByVal audit As AuditMessage, Optional ByVal idSequense As Int64 = 0) As ActionResult(Of Availability)

    ''' <summary>
    ''' Elimina una disponibilidad
    ''' </summary>
    ''' <param name="audit">The audit.</param>
    ''' <returns></returns>
    Function DeleteAvailability(ByVal availability As Availability, ByVal audit As AuditMessage) As ActionResult

#End Region

End Interface
