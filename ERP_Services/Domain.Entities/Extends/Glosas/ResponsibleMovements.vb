'***********************************************************************
' Assembly         : Domain.Entities
' Author           : Juan Diego Diaz Mosquera
' Created          : 27-01-2014
'
' Copyright        : (c) . All rights reserved.
'***********************************************************************

''' <summary>
''' Clase Movimientos por Responsables
''' </summary>
Public Class ResponsibleMovements

    ''' <summary>
    ''' Id
    ''' </summary>
    Property Id As Integer

    ''' <summary>
    ''' Id del Responsable
    ''' </summary>
    Property IdResponsible As Integer

    ''' <summary>
    ''' Código del Responsable
    ''' </summary>
    Property CodeResponsible As String

    ''' <summary>
    ''' Nombre del Responsable
    ''' </summary>
    Property NameResponsible As String

    ''' <summary>
    ''' Código y Nombre del Responsable
    ''' </summary>
    Property CodeNameResponsible As String

    ''' <summary>
    ''' Numero Factura
    ''' </summary>
    Property InvoiceNumber As String

    ''' <summary>
    ''' Proceso
    ''' </summary>
    Property Proceso As Integer

    ''' <summary>
    ''' Id del Concepto de Glosa
    ''' </summary>
    Property IdConceptGlosa As String

    ''' <summary>
    ''' Concepto Código Nombre
    ''' </summary>
    Property ConceptGlosaCodeName As String

    ''' <summary>
    ''' Entidad asociada a la factura
    ''' </summary>
    Property Entity As String

    ''' <summary>
    ''' Numero de radicado consecutivo
    ''' </summary>
    Property Radicated As Integer?

    ''' <summary>
    ''' Causante de la glosa
    ''' </summary>
    Property ResponsibleThirdPartyId As Integer?

End Class
